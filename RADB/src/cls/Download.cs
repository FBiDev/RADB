using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RADB
{
    public class Download
    {
        public List<DownloadFile> Files { get; set; }
        public int FilesCompleted { get; set; }
        public bool Overwrite { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public long BytesReceived { get; set; }
        public long TotalBytesToReceive { get; set; }
        public float ProgressPercentage { get; set; }

        public ProgressBar ProgressBar { get; set; }
        public string ProgressBarName { set { ProgressBar = Form.Controls.Find(value, true).First() as ProgressBar; } }

        public Label LabelBytes { get; set; }
        public string LabelBytesName { set { LabelBytes = Form.Controls.Find(value, true).First() as Label; } }

        public Label LabelTime { get; set; }
        public string LabelTimeName { set { LabelTime = Form.Controls.Find(value, true).First() as Label; } }

        public string URL { get; set; }
        public string FileName { get; set; }
        public Form Form { get; set; }
        public WebClient client;

        public Download()
        {
            Files = new List<DownloadFile>();
            Overwrite = true;

            //URL = GetDaoClassAndMethod(2);
            Form = Application.OpenForms[0];

            //ProgressBar = new ProgressBar();

            //LabelTime = new Label();
            //LabelBytes = new Label();

            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //client = new WebClient()
            //{
            //    Encoding = UTF8Encoding.UTF8,
            //};

            //if (Browser.useProxy)
            //{
            //    client.Proxy = Browser.Proxy;
            //}
        }

        //private readonly SemaphoreSlim _mutex = new SemaphoreSlim(10);

        public Task Start()
        {
            List<Task> Tasks = new List<Task>();

            //Remove Files with same URL
            Files = Files.Distinct().ToList();

            StartBar(ProgressBar);
            TimeSpan initialTime = new TimeSpan(DateTime.Now.Ticks);
            ToolTip ttip = new ToolTip();

            foreach (DownloadFile file in Files)
            {
                if (File.Exists(file.Path) && Archive.IsFileLocked(file.Path) || File.Exists(file.Path) && Overwrite == false)
                { continue; }

                try
                {
                    //await _mutex.WaitAsync();
                    using (WebClient client = new WebClient())
                    {
                        if (Browser.useProxy)
                        {
                            client.Proxy = Browser.Proxy;
                        }

                        client.DownloadProgressChanged += (sender, args) =>
                        {
                            file.BytesReceived = args.BytesReceived;
                            file.TotalBytesToReceive = args.TotalBytesToReceive;
                            file.ProgressPercentage = args.ProgressPercentage;

                            BytesReceived = 0;
                            TotalBytesToReceive = 0;
                            ProgressPercentage = 0;

                            Files.ForEach(f =>
                            {
                                BytesReceived += f.BytesReceived;
                                TotalBytesToReceive += f.TotalBytesToReceive;
                                ProgressPercentage += f.ProgressPercentage / Files.Count;
                            });

                            LabelBytes.Text = "Downloaded " + DownloadedProgress(BytesReceived, TotalBytesToReceive);

                            ProgressBar.Value = (int)(Math.Round(ProgressPercentage));

                            ttip.SetToolTip(ProgressBar, (TotalBytesToReceive == -1 ? "?" : ProgressBar.Value.ToString()) + " %");
                            ProgressBar.Style = (TotalBytesToReceive == -1 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous);
                        };

                        client.DownloadFileCompleted += (sender, args) =>
                        {
                            FilesCompleted++;

                            //Downloaded All Files
                            if (FilesCompleted == Files.Count)
                            {
                                LabelTime.Text = DateTime.Now.ToString();
                                StoptBar(ProgressBar);
                                ElapsedTime = new TimeSpan(DateTime.Now.Ticks) - initialTime;
                            }
                        };

                        Tasks.Add(client.DownloadFileTaskAsync(new Uri(file.URL), file.Path));
                    }
                }
                finally
                {
                    //_mutex.Release();
                }
            }


            return Task.WhenAll(Tasks);

        }





        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs downloadProgressChangedEventArgs)
        {
            //bytes += downloadProgressChangedEventArgs.BytesReceived;
            //DownloadingProgress = new Tuple<DateTime, long, long>(DateTime.Now, DownloadingProgress.Item2 + downloadProgressChangedEventArgs.TotalBytesToReceive, (DownloadingProgress.Item3 + downloadProgressChangedEventArgs.BytesReceived));
        }

        /////////////////////////////////
        public async Task StartX()
        {
            using (WebClient client = new WebClient() { Proxy = Browser.Proxy })
            {
                //if (client.IsBusy == false)
                //{
                //StartBar(ProgressBar, ProgressBarStyle.Marquee);
                //LabelBytes.Text = "Opening URL... ";

                //client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                //client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                //client.DownloadFileAsync(new Uri(RA.GetURL("API_GetGameList.php", "&i=", "12")), RA.Local_JsonFolder + "GameList.json", new List<object> { lbl, bar });
                //client.DownloadFileAsync(new Uri(@"file:///C:/Users/fbirnfeld/Downloads/DOCS/Projects/GitHub/RADB/RADB/bin/Debug/src/rsc/json/Files.json"), RA.Local_JsonFolder + "GameList.json");

                //client.DownloadFileAsync(new Uri("https://drive.google.com/u/0/uc?id=1_C8I5Vt62xbpcFF6otwRtHczXm-NY3Y8&export=download"), RA.Local_JsonFolder + "GameList.json", new List<object> { lbl, bar });
                //client.DownloadFileAsync(new Uri("http://www.cohabct.com.br/userfiles/file/Concovados/2020/classificacao_julho_2020.pdf"), RA.Local_JsonFolder + "GameList.json", new List<object> { lbl, bar });
                await client.DownloadFileTaskAsync(new Uri(URL), FileName);
                //client.OpenRead("http://www.cohabct.com.br/userfiles/file/Concovados/2020/classificacao_julho_2020.pdf");
                //Int64 bytes_total = Convert.ToInt64(Browser.web.ResponseHeaders["Content-Length"]);
                //}
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //Form.BeginInvoke((MethodInvoker)delegate
            //{
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            LabelBytes.Text = "Downloaded " + DownloadedProgress(bytesIn, totalBytes);

            if (totalBytes == -1) { ProgressBar.Style = ProgressBarStyle.Marquee; return; }

            ProgressBar.Style = ProgressBarStyle.Blocks;

            double percentage = bytesIn / totalBytes * 100;
            int barValue = int.Parse(Math.Truncate(percentage).ToString());
            if (barValue > 0 && barValue <= ProgressBar.Maximum && barValue > ProgressBar.Value)
            {
                ProgressBar.Value = barValue;
            }
            //});
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //Form.BeginInvoke((MethodInvoker)delegate
            //{
            StoptBar(ProgressBar);
            LabelTime.Text = File.GetLastWriteTime(FileName).ToString();
            //client.Dispose();
            //return;
            //});
        }

        private string DownloadedProgress(double bytesIn, double bytesTotal)
        {
            if (bytesTotal == -1) { return CalculateFileSize(bytesIn); }
            return CalculateFileSize(bytesIn) + " of " + CalculateFileSize(bytesTotal);
        }

        private string CalculateFileSize(double _bytes)
        {
            string unitSimbol = _bytes < 1024 ? "bytes" :
                _bytes < 1048576 ? "KB" : "MB";

            double unitSize = _bytes < 1024 ? _bytes :
                _bytes < 1048576 ? _bytes / 1024 : _bytes / 1024 / 1024;

            if (unitSize < 10) { return (Math.Floor(unitSize * 100) / 100).ToString("n2") + " " + unitSimbol; }
            if (unitSize < 100) { return (Math.Floor(unitSize * 10) / 10).ToString("n1") + " " + unitSimbol; }
            return Math.Floor(unitSize) + " " + unitSimbol;
        }

        private void StartBar(ProgressBar bar, ProgressBarStyle style = ProgressBarStyle.Continuous, int maximum = 100)
        {
            bar.Style = style;
            bar.MarqueeAnimationSpeed = 1;
            bar.Maximum = maximum;
            bar.Value = 0;
        }

        private void StepBar(ProgressBar bar)
        {
            bar.Value += bar.Step;
        }

        private void StoptBar(ProgressBar bar)
        {
            if (bar.Style == ProgressBarStyle.Marquee)
            {
                bar.MarqueeAnimationSpeed = 0;
                bar.Style = ProgressBarStyle.Continuous;
            }

            //Hack for Win7
            bar.Maximum++;
            bar.Value = bar.Maximum;
            bar.Value--;
            bar.Maximum--;
        }
    }
}
