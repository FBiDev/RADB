using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using RADB.Properties;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public enum PictureFormat
    {
        Jpg = 0x2E6A7067,//.png
        Png = 0x2E706E67,//.jpg
    }

    public class Picture
    {
        public Bitmap Bitmap { get; set; }
        Size _Size { get; set; }
        public Size Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                Width = value.Width;
                Height = value.Height;
            }
        }
        public int Width { get; set; }
        public int Height { get; set; }
        EncoderParameters Parameters { get; set; }
        long _Quality;
        public long Quality
        {
            get { return _Quality; }
            set
            {
                Parameters.Param[0] = new EncoderParameter(Encoder.Quality, value);
                _Quality = value;
            }
        }

        public string Path { get; set; }
        string _Name { get; set; }
        public string Name
        {
            get
            {
                if (_Name == null)
                    _Name = System.IO.Path.GetFileName(Path);
                return _Name;
            }
        }
        ImageFormat Format { get; set; }
        PictureFormat FormatEnum { get; set; }

        public Size Scale(Size maxSize)
        {
            float factor = Bitmap.Width >= Bitmap.Height ? Bitmap.Height / (float)Bitmap.Width : Bitmap.Width / (float)Bitmap.Height;

            int width = Bitmap.Width;
            int height = Bitmap.Height;

            if (width >= height)
            {
                if (width > maxSize.Width) { width = maxSize.Width; }
                height = (int)Math.Ceiling(width * factor);
            }
            else if (width < height)
            {
                if (height > maxSize.Height) { height = maxSize.Height; }
                width = (int)Math.Ceiling(height * factor);
            }
            return new Size(width, height);
        }

        List<string> ImageFiles { get; set; }
        public int ImagesPerRow { get; set; }
        public Size FixedPerImage { get; set; }
        public bool StretchImage { get; set; }

        public string Error { get; set; }

        void DefaultValues()
        {
            Path = string.Empty;
            ImageFiles = new List<string>();
            Error = string.Empty;

            Parameters = new EncoderParameters(1);

            //Default Jpeg Quality
            Quality = 91L;//90%

            //ImagesPerRow = 11;
        }

        public Picture(Size size) : this(size.Width, size.Height) { }

        public Picture(int width, int height)
        {
            DefaultValues();

            Bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);

            Size = Bitmap.Size;

            //Default Background
            using (Graphics g = Graphics.FromImage(Bitmap)) { g.Clear(Color.Magenta); }
        }

        public Picture(Bitmap bitmap)
        {
            DefaultValues();

            Bitmap = bitmap;
            Size = Bitmap.Size;
        }

        public Picture(string fileName, PictureFormat format = PictureFormat.Jpg)
        {
            DefaultValues();
            Path = fileName;
            FormatEnum = format;

            Bitmap = BitmapExtension.SuperFastLoad(Path, RA.ErrorIcon);
            Size = Bitmap.Size;
        }

        public static Picture Create(string fileName)
        {
            return new Picture(fileName);
        }

        public Picture(List<string> imagesToMerge, bool merge = true, int imagesPerRow = 11, Size fixedPerImage = default(Size), bool stretchImage = false)
        {
            DefaultValues();

            ImageFiles = imagesToMerge;
            ImagesPerRow = imagesPerRow;
            FixedPerImage = fixedPerImage;
            StretchImage = stretchImage;

            BlankBitmap();

            if (merge) { MergeImages(); }
        }

        #region Saves
        ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void SaveGrayscale(string fileName, PictureFormat format = PictureFormat.Jpg)
        {
            Bitmap = MakeGrayscale(Bitmap);
            Save(fileName, format);
        }

        public void Save(string fileName, PictureFormat format = PictureFormat.Jpg, bool compress = false)
        {
            if (Bitmap == null || (Bitmap is Bitmap) == false) { return; }

            Path = fileName + format.ToStringHex();
            FormatEnum = format;

            switch (FormatEnum)
            {
                case PictureFormat.Jpg: Format = ImageFormat.Jpeg; break;
                case PictureFormat.Png: Format = ImageFormat.Png; break;
            }

            if (File.Exists(Path)) { File.Delete(Path); }

            //Max image Size is 65.000 x 65.000
            Bitmap.Save(Path, GetEncoder(Format), Parameters);
            Bitmap.Dispose();

            if (compress) { Compress(); }
        }

        public string Compress()
        {
            byte[] exeResource = new byte[0];
            string exeFilePath = Folder.Temp;
            string exeCmd = string.Empty;

            switch (FormatEnum)
            {
                case PictureFormat.Jpg:
                    exeResource = Resources.jpegoptim_1_5_5;
                    exeFilePath += "jpegoptim_1.5.5.exe";
                    exeCmd = exeFilePath + " -w max \"" + Path + "\"";
                    break;
                case PictureFormat.Png:
                    exeResource = Resources.pngcrush_1_8_13_w64;
                    exeFilePath += "pngcrush_1_8_13_w64.exe";
                    exeCmd = exeFilePath + " -ow -speed " + "\"" + Path + "\"";
                    break;
            }

            using (FileStream exeNewFile = new FileStream(exeFilePath, FileMode.Create))
            {
                exeNewFile.Write(exeResource, 0, exeResource.Length);
            }

            var output = CMD.Execute(exeCmd);

            File.Delete(exeFilePath);

            return output;
        }
        #endregion

        #region MergeImages
        void BlankBitmap()
        {
            int width = 0;
            int maxWidth = 0;
            int height = 0;
            int maxHeight = 0;

            int index = 1;
            int imagesPerRow = ImagesPerRow;

            string FileNotFound = string.Empty;
            int FileNotFoundIndex = 1;

            foreach (string imageFile in ImageFiles)
            {
                if (!File.Exists(imageFile))
                {
                    if (FileNotFoundIndex <= 30)
                    {
                        FileNotFound += "[" + FileNotFoundIndex + "] " + imageFile + Environment.NewLine;
                    }

                    FileNotFoundIndex++;
                    continue;
                }

                var image = BitmapExtension.SuperFastLoad(imageFile, null);

                //update the width of the final bitmap
                if (index <= imagesPerRow && width <= maxWidth)
                {
                    width += FixedPerImage.Width == 0 ? image.Width : FixedPerImage.Width;
                }

                if (width > maxWidth) { maxWidth = width; }

                if (index > imagesPerRow)
                {
                    maxHeight += height;
                    height = 0;
                    width = 0;
                    index = 1;
                }

                //update the height of the final bitmap
                if (image.Height > height)
                {
                    height = FixedPerImage.Height == 0 ? image.Height : FixedPerImage.Height;
                }
                index++;

                image.Dispose();
            }

            if (string.IsNullOrWhiteSpace(FileNotFound) == false)
            {
                if (FileNotFoundIndex > 30) { FileNotFound += Environment.NewLine + "and more... total = " + (FileNotFoundIndex - 1); }

                Error = "File Not Found: " + Environment.NewLine + FileNotFound;
                //MessageBox.Show("File Not Found: " + Environment.NewLine + FileNotFound, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            maxHeight += height;

            if (maxWidth == 0 || maxHeight == 0) { return; }

            Bitmap = new Picture(maxWidth, maxHeight).Bitmap;
            Size = Bitmap.Size;
        }

        void MergeImages()
        {
            if (Bitmap == null || (Bitmap is Bitmap) == false) { return; }

            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                //If not resize
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;

                //prevents ghosting around the image borders
                var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                int offsetW = 0;
                int offsetH = 0;
                int offsetHLine = 0;

                int index = 1;
                int imagesPerRow = ImagesPerRow;

                foreach (string imageFile in ImageFiles)
                {
                    var image = BitmapExtension.SuperFastLoad(imageFile, null);

                    if (index > imagesPerRow)
                    {
                        offsetH += offsetHLine;
                        offsetHLine = 0;
                        offsetW = 0;
                        index = 1;
                    }
                    if (image.Height > offsetHLine)
                    {
                        offsetHLine = FixedPerImage.Height == 0 ? image.Height : FixedPerImage.Height;
                    }
                    index++;

                    int newWidth = image.Width > FixedPerImage.Width ? FixedPerImage.Width : image.Width;
                    int newHeight = image.Height > FixedPerImage.Height ? FixedPerImage.Height : image.Height;

                    if (StretchImage)
                    {
                        newWidth = FixedPerImage.Width;
                        newHeight = FixedPerImage.Height;
                    }

                    bool newSize = (newWidth == FixedPerImage.Width || newHeight == FixedPerImage.Height);

                    //Resize
                    if (StretchImage || newSize)
                    {
                        g.InterpolationMode = (image.Height > FixedPerImage.Height) || (image.Width > FixedPerImage.Width) ? InterpolationMode.HighQualityBicubic : InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = (image.Height > FixedPerImage.Height) || (image.Width > FixedPerImage.Width) ? PixelOffsetMode.HighQuality : PixelOffsetMode.Half;

                        g.DrawImage(image, new Rectangle(offsetW, offsetH, newWidth, newHeight), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                    else
                    {
                        //Original Size
                        g.DrawImage(image, new Rectangle(offsetW, offsetH, image.Width, image.Height));
                    }

                    offsetW += FixedPerImage.Width == 0 ? image.Width : FixedPerImage.Width;

                    wrapMode.Dispose();
                    image.Dispose();
                }
            }
        }
        #endregion

        #region Effects Grayscale
        Bitmap MakeGrayscale(Bitmap original)
        {
            var newBitmap = new Bitmap(original.Width, original.Height);

            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                //create the grayscale ColorMatrix
                var colorMatrix = new ColorMatrix(
                    new float[][]
                    {
                        new float[] {.3f, .3f, .3f, 0, 0},
                        new float[] {.59f, .59f, .59f, 0, 0},
                        new float[] {.11f, .11f, .11f, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                    }
                );

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                                                    0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }
        #endregion
    }
}