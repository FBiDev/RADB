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
    }
}
