using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using RADB.Properties;
using System.Windows.Forms;
using System.Globalization;

namespace RADB
{
    public enum PictureFormat
    {
        Jpg,
        Png
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

        public string FileName { get; set; }
        public ImageFormat Format { get; set; }
        public PictureFormat FormatEnum { get; set; }

        public Picture(int width, int height)
        {
            Bitmap = new Bitmap(width, height);

            Parameters = new EncoderParameters(1);

            //Default Jpeg Quality
            Quality = 91L;//90%

            //Default Background
            using (Graphics g = Graphics.FromImage(Bitmap)) { g.Clear(Color.Magenta); }
        }

        public void Save(string fileName, PictureFormat format = PictureFormat.Jpg)
        {
            FileName = fileName + "." + format.ToString().ToLower();
            FormatEnum = format;

            switch (FormatEnum)
            {
                case PictureFormat.Jpg: Format = ImageFormat.Jpeg; break;
                case PictureFormat.Png: Format = ImageFormat.Png; break;
            }

            if (File.Exists(FileName)) { File.Delete(FileName); }
            Bitmap.Save(FileName, GetEncoder(Format), Parameters);
            Dispose();

            CompressCMD();
        }

        public void SaveGrayscale(string fileName, PictureFormat format = PictureFormat.Jpg)
        {
            Bitmap = MakeGrayscale(Bitmap);
            Save(fileName, format);
        }

        public void CompressCMD()
        {
            Directory.CreateDirectory(RA.FolderTemp);

            byte[] exeResource = new byte[0];
            string exeFile = RA.FolderTemp;
            string exeCmd = string.Empty;

            switch (FormatEnum)
            {
                case PictureFormat.Jpg:
                    exeResource = Resources.jpegoptim_1_4_6;
                    exeFile += "jpegoptim_1_4_6.exe";
                    //exeCmd = "\"" + RA.FolderTemp + exeFileName + "\"\" " + FileName;
                    exeCmd = exeFile + " " + FileName;
                    break;
                case PictureFormat.Png:
                    exeResource = Resources.pngcrush_1_8_11_w64;
                    exeFile += "pngcrush_1_8_11_w64.exe";
                    exeCmd = exeFile + " -ow " + FileName;
                    break;
            }

            using (FileStream exeNewFile = new FileStream(exeFile, FileMode.Create))
            {
                exeNewFile.Write(exeResource, 0, exeResource.Length);
            }

            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage),
                    StandardErrorEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage),
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    FileName = "cmd.exe",
                    Arguments = "/C " + exeCmd,
                    //WorkingDirectory = "",
                },
            };
            process.Start();

            string output = process.StandardError.ReadToEnd() + process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Dispose();

            File.Delete(exeFile);
        }

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

        public void Dispose()
        {
            Bitmap.Dispose();
        }
    }
}
