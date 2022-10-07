using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
//
using RADB.Properties;

namespace RADB
{
    public enum PictureFormat
    {
        Jpg = 0x2E6A7067,   //.png
        Png = 0x2E706E67,   //.jpg
    }

    public class Picture
    {
        public Bitmap Bitmap { get; set; }
        private EncoderParameters Parameters { get; set; }
        private long _Quality;
        public long Quality
        {
            get { return _Quality; }
            set
            {
                Parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, value);
                _Quality = value;
            }
        }

        public string Path { get; set; }
        public string Name { get { return System.IO.Path.GetFileName(Path); } }
        private ImageFormat Format { get; set; }
        private PictureFormat FormatEnum { get; set; }

        public Size Scale(Size maxSize)
        {
            float factor = Bitmap.Width >= Bitmap.Height ? (float)Bitmap.Height / (float)Bitmap.Width : (float)Bitmap.Width / (float)Bitmap.Height;

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

        private List<string> ImageFiles { get; set; }
        public int ImagesPerRow { get; set; }
        public Size FixedPerImage { get; set; }
        public bool StretchImage { get; set; }

        public string Error { get; set; }

        private string FolderExe
        {
            get
            {
                return Folder.Temp;
            }
        }

        private void DefaultValues()
        {
            Path = string.Empty;
            ImageFiles = new List<string>();
            Error = string.Empty;

            Parameters = new EncoderParameters(1);

            //Default Jpeg Quality
            Quality = 91L;//90%

            //ImagesPerRow = 11;
        }

        public Picture(Size size)
            : this(size.Width, size.Height)
        { }

        public Picture(int width, int height)
        {
            DefaultValues();

            Bitmap = new Bitmap(width, height);

            //Default Background
            using (Graphics g = Graphics.FromImage(Bitmap)) { g.Clear(Color.Magenta); }
        }

        public Picture(Bitmap bitmap)
        {
            DefaultValues();

            Bitmap = bitmap;
        }

        public Picture(string fileName, PictureFormat format = PictureFormat.Jpg)
        {
            DefaultValues();
            Path = fileName;
            FormatEnum = format;

            try
            {
                Bitmap = FromFile(Path);
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
        }

        public Picture(List<string> imagesToMerge, bool merge = true, int imagesPerRow = 11, Size fixedPerImage = default(Size), bool stretchImage = false)
        {
            DefaultValues();

            ImageFiles = imagesToMerge;
            ImagesPerRow = imagesPerRow;
            FixedPerImage = fixedPerImage;
            StretchImage = stretchImage;

            BlankBitmap();

            if (merge)
            {
                MergeImages();
            }
        }

        public static Picture Create(string fileName, Bitmap errorBitmap = null)
        {
            if (File.Exists(fileName) == false || new FileInfo(fileName).Length == 0)
            {
                return new Picture(errorBitmap);
            }
            return new Picture(fileName);
        }

        public Bitmap FromFile(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            var ms = new MemoryStream(bytes);
            //var img = Image.FromStream(ms);
            return (Bitmap)Image.FromStream(ms);
        }

        #region Saves
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
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

            if (compress)
            {
                Compress();
            }
        }

        public string Compress()
        {
            byte[] exeResource = new byte[0];
            string exeFile = FolderExe;
            string exeCmd = string.Empty;

            switch (FormatEnum)
            {
                case PictureFormat.Jpg:
                    exeResource = Resources.jpegoptim_1_4_7;
                    exeFile += "jpegoptim_1_4_7.exe";
                    exeCmd = exeFile + " " + Path;
                    break;
                case PictureFormat.Png:
                    exeResource = Resources.pngcrush_1_8_11_w64;
                    exeFile += "pngcrush_1_8_11_w64.exe";
                    exeCmd = exeFile + " -ow " + Path;
                    break;
            }

            using (FileStream exeNewFile = new FileStream(exeFile, FileMode.Create))
            {
                exeNewFile.Write(exeResource, 0, exeResource.Length);
            }

            string output = CMD.Execute(exeCmd);

            File.Delete(exeFile);

            return output;
        }
        #endregion

        #region MergeImages
        private void BlankBitmap()
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

                //create a Bitmap from the file and add it to the list
                Bitmap image = FromFile(imageFile);
                //Bitmap image = new Bitmap(imageFile);

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

            //create a bitmap to hold the combined image
            Bitmap = new Picture(maxWidth, maxHeight).Bitmap;
        }

        private void MergeImages()
        {
            if (Bitmap == null || (Bitmap is Bitmap) == false) { return; }

            //get a graphics object from the image so we can draw on it
            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                //copy in High Quality
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //prevents ghosting around the image borders
                var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                //go through each image and draw it on the final image
                int offsetW = 0;
                int offsetH = 0;
                int offsetHLine = 0;

                int index = 1;
                int imagesPerRow = ImagesPerRow;

                foreach (string imageFile in ImageFiles)
                {
                    Bitmap image = FromFile(imageFile);
                    //Bitmap image = new Bitmap(imageFile);

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

                    if (StretchImage)
                    {
                        //Resize
                        g.DrawImage(image, new Rectangle(offsetW, offsetH, FixedPerImage.Width, FixedPerImage.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
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
        private Bitmap MakeGrayscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                    new float[][] 
                    {
                        new float[] {.3f, .3f, .3f, 0, 0},
                        new float[] {.59f, .59f, .59f, 0, 0},
                        new float[] {.11f, .11f, .11f, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}
                    }
                );

                //create some image attributes
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    //set the color matrix attribute
                    attributes.SetColorMatrix(colorMatrix);

                    //draw the original image on the new image
                    //using the grayscale color matrix
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                                0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }
        #endregion
    }
}
