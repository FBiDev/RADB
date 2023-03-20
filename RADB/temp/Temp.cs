using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
//using System.Drawing.Imaging;
//using PhotoSauce.MagicScaler;//Need System.Memory Reference

namespace RADB.temp
{
    class Temp
    {
        public Temp()
        {
            client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
        }
        //Browser
        public static WebClient client;
        public static string DownloadString(string URL)
        {
            var data = client.DownloadString(URL);
            return data;
        }

        public static byte[] DownloadData(string URL)
        {
            var data = client.DownloadData(URL);
            return data;
        }

        //Download
        void client_DownloadProgressChanged()
        {
            //object sender, DownloadProgressChangedEventArgs e
            //Form.BeginInvoke((MethodInvoker)delegate
            //{

            //double bytesIn = double.Parse(e.BytesReceived.ToString());
            //double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            //LabelBytes.Text = "Downloaded " + DownloadedProgress(bytesIn, totalBytes);

            //if (totalBytes == -1) { ProgressBar.Style = ProgressBarStyle.Marquee; return; }

            //ProgressBar.Style = ProgressBarStyle.Blocks;

            //double percentage = bytesIn / totalBytes * 100;
            //int barValue = int.Parse(Math.Truncate(percentage).ToString());
            //if (barValue > 0 && barValue <= ProgressBar.Maximum && barValue > ProgressBar.Value)
            //{
            //    ProgressBar.Value = barValue;
            //}

            //});
        }

        void StepBar(ProgressBar bar)
        {
            bar.Value += bar.Step;
        }

        //Game
        public string IconName
        {
            get
            {
                //return ImageIcon.Replace(@"/Images/", "");
                return string.Empty;
            }
        }

        public string IconPath()
        {
            //return Folder.ImageIcon(ConsoleID) + IconName;
            return string.Empty;
        }


        //RA
        public static List<T> FileToList<T>(string filePath)
        {
            //Read and Convert File
            var text = File.ReadAllText(filePath);
            var objList = JsonConvert.DeserializeObject<List<T>>(text);
            return objList;
        }

        public static void UpdateFile<T>(T list, string path)
        {
            //Serialize List and Save Json File
            var jsonData = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, jsonData);
        }

        public static FileUpdate FindFileName(List<FileUpdate> list, string name)
        {
            return list.Find(o => o.Name == name);
        }

        //RADB
        //private void btnDownloadBadges_Click(object sender, EventArgs e)
        //{
        //await RA.DownloadBadges(0);
        //txtOutput.Text = "Badges Downloaded!";
        //}

        //TESTS to resize Image with PhotoSauce.MagicScaler
        void btnDownloadBadges_Click()
        {
            //object sender, EventArgs e
            return;
            //var file = @"Data\Temp\W2.png";
            //var file2 = @"Data\Temp\W2_RS2.png";
            //var file3 = @"Data\Temp\W2_RS2_NEW.png";

            //var file4 = @"Data\Temp\W2_RS0.png";

            //var otp = await Task.Run(() =>
            //{
            //    //var Encoder = new JpegEncoderOptions(98, ChromaSubsampleMode.Subsample444, true);
            //    var Encoder = new PngEncoderOptions(PngFilter.None, false);
            //    CropScaleMode rs = CropScaleMode.Stretch;

            //    MagicImageProcessor.ProcessImage(file, file2, new ProcessImageSettings
            //    {
            //        Width = 96,
            //        Height = 96,
            //        ResizeMode = rs,
            //        EncoderOptions = Encoder,
            //    });


            //    var b = new Bitmap(96 * 2, 96 * 2);
            //    using (Graphics g = Graphics.FromImage(b))
            //    {
            //        g.Clear(Color.White);

            //        Bitmap image1 = new Bitmap(file);
            //        Bitmap image2 = new Bitmap(file2);
            //        Bitmap image4 = new Bitmap(file4);
            //        g.DrawImage(image1, new Rectangle(0, 0, image1.Width, image1.Height));
            //        g.DrawImage(image2, new Rectangle(0, image1.Height, image2.Width, image2.Height));
            //        g.DrawImage(image4, new Rectangle(image2.Width, image1.Height, image4.Width, image4.Height));
            //        image1.Dispose();
            //        image2.Dispose();
            //        image4.Dispose();
            //    }
            //    b.Save(file3, ImageFormat.Png);

            //    var pic = new Picture(file2, PictureFormat.Png);
            //    return pic.Compress();
            //});
        }
    }
}