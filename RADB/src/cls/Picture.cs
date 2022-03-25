using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

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
        public ImageFormat Format { get; set; }
        private long _Quality;
        public long Quality
        {
            get { return _Quality; }
            set
            {
                Parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, value);
                Parameters.Param[1] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, (long)EncoderValue.ColorTypeCMYK);
                _Quality = value;
            }
        }

        public Picture(int width, int height)
        {
            Bitmap = new Bitmap(width, height);

            Parameters = new EncoderParameters(2);

            //Default Jpeg Quality
            Quality = 91L;//90%

            //Default Background
            using (Graphics g = Graphics.FromImage(Bitmap)) { g.Clear(Color.Magenta); }
        }

        public void Save(string fileName, PictureFormat format = PictureFormat.Jpg)
        {
            string cmdLine = string.Empty;
            fileName = fileName + "." + format.ToString().ToLower();

            byte[] exeResource = new byte[0];
            string exeCompressImage = string.Empty;

            //Default Format
            switch (format)
            {
                case PictureFormat.Jpg:
                    Format = ImageFormat.Jpeg;
                    cmdLine += "jpegoptim.exe " + fileName;

                    exeResource = Properties.Resources.jpegoptim;
                    exeCompressImage = @"jpegoptim.exe";
                    break;
                case PictureFormat.Png:
                    Format = ImageFormat.Png;
                    cmdLine += "pngcrush_1_8_11_w64.exe -ow " + fileName;

                    exeResource = Properties.Resources.pngcrush_1_8_11_w64;
                    exeCompressImage = @"pngcrush_1_8_11_w64.exe";
                    break;
            }

            if (File.Exists(fileName)) { File.Delete(fileName); }
            Bitmap.Save(fileName, GetEncoder(Format), Parameters);
            Dispose();

            using (FileStream exeFile = new FileStream(exeCompressImage, FileMode.CreateNew))
            {
                exeFile.Write(exeResource, 0, exeResource.Length);
            }

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + cmdLine;
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit();
            process.Dispose();

            File.Delete(exeCompressImage);
        }

        public void Dispose()
        {
            Bitmap.Dispose();
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
    }
}
