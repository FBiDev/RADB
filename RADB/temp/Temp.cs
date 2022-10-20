using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
//using System.Drawing.Imaging;
//using PhotoSauce.MagicScaler;//Need System.Memory Reference

namespace RADB.temp
{
    class Temp
    {
        public Temp()
        {
            client = new WebClient()
            {
                Encoding = UTF8Encoding.UTF8,
            };
        }
        //Browser
        public static WebClient client;
        public static string DownloadString(string URL)
        {
            string data = client.DownloadString(URL);
            return data;
        }

        public static byte[] DownloadData(string URL)
        {
            byte[] data = client.DownloadData(URL);
            return data;
        }


        //Download
        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs downloadProgressChangedEventArgs)
        {
            //bytes += downloadProgressChangedEventArgs.BytesReceived;
            //DownloadingProgress = new Tuple<DateTime, long, long>(DateTime.Now, DownloadingProgress.Item2 + downloadProgressChangedEventArgs.TotalBytesToReceive, (DownloadingProgress.Item3 + downloadProgressChangedEventArgs.BytesReceived));
        }

        /////////////////////////////////
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
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

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //Form.BeginInvoke((MethodInvoker)delegate
            //{

            //StoptBar(ProgressBar);
            //LabelTime.Text = File.GetLastWriteTime(FileName).ToString();

            //client.Dispose();
            //return;
            //});
        }

        private void StepBar(ProgressBar bar)
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

        public static void DownloadFile(string url, string filePath)
        {
            //Download and Save file
            //byte[] data = Browser.DownloadData(url);
            //File.WriteAllBytes(filePath, data);
        }

        public static List<T> FileToList<T>(string filePath)
        {
            //Read and Convert File
            string text = File.ReadAllText(filePath);
            List<T> objList = JsonConvert.DeserializeObject<List<T>>(text);
            return objList;
        }

        public static void UpdateFile<T>(T list, string path)
        {
            //Serialize List and Save Json File
            string jsonData = JsonConvert.SerializeObject(list);
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
        private void btnDownloadBadges_Click(object sender, EventArgs e)
        {
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

        public void loginTest()
        {
            //HttpClientHandler httpClientHandler = new HttpClientHandler()
            //{
            //    Proxy = Proxy,
            //    PreAuthenticate = true,
            //    UseDefaultCredentials = false,
            //};
            //HttpClient client = new HttpClient(httpClientHandler);

            //string url = RA.HOST;

            //using (HttpResponseMessage response = await client.GetAsync(url))
            //{
            //    using (HttpContent content = response.Content)
            //    {
            //        var json = await content.ReadAsStringAsync();
            //        token = json.GetBetween("_token\" value=\"", "\">");
            //    }
            //}

            //var postParams = new Dictionary<string, string>();
            //postParams.Add("_token", token);
            //postParams.Add("u", "");
            //postParams.Add("p", "");
            //postParams.Add("submit", "Login");

            //using (var postContent = new FormUrlEncodedContent(postParams))
            //using (HttpResponseMessage response = await client.PostAsync(RA.HOST + "request/auth/login.php", postContent))
            //{
            //    response.EnsureSuccessStatusCode(); // Throw if httpcode is an error
            //    //using (HttpContent content = response.Content)
            //    //{
            //    //    string result = await content.ReadAsStringAsync();
            //    //}
            //}

            //using (HttpResponseMessage response = await client.GetAsync(RA.HOST + "linkedhashes.php?g=1"))
            //{
            //    using (HttpContent content = response.Content)
            //    {
            //        var json = await content.ReadAsStringAsync();
            //        var li = json.GetBetween("unique hashes registered for it:<br><br><ul>", "</ul>");
            //        var game = li.GetBetween("<p class='embedded'><b>", "</b>");
            //        var hash = li.GetBetween("<code>", "</code>");
            //    }
            //}

            //var entries = new List<Tuple<string, string>>().Select(t => new { Title = t.Item1, Hash = t.Item2 }).ToList();
            //entries.Add(new { Title = title, Hash = hash });
        }
    }
}
