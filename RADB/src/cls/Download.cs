using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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

        public T FindControl<T>(string controlName) where T : Control, new()
        {
            var c = FormObj.Controls.Find(controlName, true) as Control[];
            return c.Count() == 0 ? new T() : (T)c.First();
        }

        public string ProgressBarName { set { ProgressBar = FindControl<ProgressBar>(value); } }

        public Label LabelBytes { get; set; }
        public string LabelBytesName { set { LabelBytes = FindControl<Label>(value); } }

        public Label LabelTime { get; set; }
        public string LabelTimeName { set { LabelTime = FindControl<Label>(value); } }

        private ToolTip Tip { get; set; }

        public string URL { get; set; }
        public string FileName { get; set; }
        public Form FormObj { get; set; }
        public string FormName { set { FormObj = Application.OpenForms[value]; } }
        public WebClient client;

        public Download(string URL, string fileName)
        {
            Files = new List<DownloadFile>() { new DownloadFile(URL, fileName) };
            Overwrite = true;
            FormObj = Application.OpenForms[0];
        }

        public Download()
        {
            Files = new List<DownloadFile>();
            Overwrite = true;
            FormObj = Application.OpenForms[0];
        }

        //private readonly SemaphoreSlim _mutex = new SemaphoreSlim(3);
        public async Task Start()
        {
            List<Task> Tasks = new List<Task>();

            //Remove Files with same URL
            Files = Files.Distinct().ToList();
            int TotalFilesToDownload = Files.Count;

            StartBar(ProgressBar, ProgressBarStyle.Marquee);
            //Tip.RemoveAll();
            LabelBytes.Text = "Connecting...";
            TimeSpan initialTime = new TimeSpan(DateTime.Now.Ticks);


            foreach (DownloadFile file in Files)
            {
                //File.Exists(file.Path) && Archive.IsFileLocked(file.Path) ||
                if (File.Exists(file.Path) && new FileInfo(file.Path).Length > 0 && Overwrite == false)
                { TotalFilesToDownload--; continue; }

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
                                ProgressPercentage += (f.ProgressPercentage / TotalFilesToDownload);
                            });

                            LabelBytes.Text = "Downloaded " + DownloadedProgress(BytesReceived, TotalBytesToReceive);

                            ProgressBar.Value = ProgressPercentage > 100 ? 100 : (int)(Math.Ceiling(ProgressPercentage));

                            //Tip.SetToolTip(ProgressBar, (TotalBytesToReceive == -1 ? "?" : ProgressBar.Value.ToString()) + " %");
                            ProgressBar.Style = (TotalBytesToReceive == -1 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous);
                        };

                        client.DownloadFileCompleted += (sender, args) =>
                        {
                            FilesCompleted++;
                            LabelTime.Text = FilesCompleted.ToString();
                            //Downloaded All Files
                            if (FilesCompleted == TotalFilesToDownload)
                            {
                                LabelTime.Text = DateTime.Now.ToString();
                                StoptBar(ProgressBar);
                                ElapsedTime = new TimeSpan(DateTime.Now.Ticks) - initialTime;
                            }
                        };

                        Tasks.Add(client.DownloadFileTaskAsync(new Uri(file.URL), file.Path));
                        if (Tasks.Count == ServicePointManager.DefaultConnectionLimit)
                        {
                            await Task.WhenAll(Tasks);
                            Tasks.Clear();
                        }
                    }
                }
                finally
                {
                    //_mutex.Release();
                }
            }

            await Task.WhenAll(Tasks);
            StoptBar(ProgressBar);
            LabelBytes.Text = LabelBytes.Text == "Connecting..." ? "" : LabelBytes.Text; 
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
            bar.MarqueeAnimationSpeed = 50;
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
                //bar.MarqueeAnimationSpeed = 0;
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
