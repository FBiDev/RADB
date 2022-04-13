using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace RADB
{
    public class Download
    {
        public List<DownloadFile> Files { get; set; }
        public DownloadFile File
        {
            get
            {
                if (Files.Count > 0)
                {
                    return Files[0];
                }
                return null;
            }
            set
            {
                Files = new List<DownloadFile>() { value };
            }
        }
        public int FilesCompleted { get; set; }
        public bool Overwrite { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public long BytesReceived { get; set; }
        public long TotalBytesToReceive { get; set; }
        public float ProgressPercentage { get; set; }

        public T FindControl<T>(string controlName) where T : Control, new()
        {
            var c = FormObj.Controls.Find(controlName, true) as Control[];
            var b = c.Count() == 0 ? new T() : (T)c.First();
            return b;
        }

        public ProgressBar ProgressBar { get; set; }
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
            FilesCompleted = 0;

            //Remove Files with same URL
            Files = Files.Distinct().ToList();
            int TotalFilesToDownload = Files.Count;

            BarStart(ProgressBar, ProgressBarStyle.Marquee);
            //Tip.RemoveAll();
            LabelBytes.Text = "Connecting...";
            TimeSpan initialTime = new TimeSpan(DateTime.Now.Ticks);

            foreach (DownloadFile file in Files)
            {
                //File.Exists(file.Path) && Archive.IsFileLocked(file.Path) ||
                if (System.IO.File.Exists(file.Path) && new FileInfo(file.Path).Length > 0 && Overwrite == false)
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
                                //if (TotalBytesToReceive > 0) { ProgressPercentage = (float)((float)(BytesReceived / TotalBytesToReceive) * 100); }
                            });

                            LabelBytes.Text = "Downloaded " + DownloadedProgress(BytesReceived, TotalBytesToReceive);

                            ProgressBar.Value = ProgressPercentage > 100 ? 100 : (int)(Math.Ceiling(ProgressPercentage));

                            //Tip.SetToolTip(ProgressBar, (TotalBytesToReceive == -1 ? "?" : ProgressBar.Value.ToString()) + " %");
                            ProgressBar.Style = (TotalBytesToReceive == -1 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous);
                        };

                        client.DownloadFileCompleted += (sender, args) =>
                        {
                            FilesCompleted++;
                            LabelBytes.Text += " (" + FilesCompleted + "/" + TotalFilesToDownload + ")";
                            //Downloaded All Files
                            if (FilesCompleted == TotalFilesToDownload)
                            {
                                LabelTime.Text = DateTime.Now.ToString();
                                BarStop(ProgressBar);
                                ElapsedTime = new TimeSpan(DateTime.Now.Ticks) - initialTime;
                            }
                        };

                        Tasks.Add(client.DownloadFileTaskAsync(new Uri(file.URL), file.Path));
                        if (Tasks.Count == ServicePointManager.DefaultConnectionLimit)
                        {
                            LabelBytes.Text = "Connecting...";
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
            BarStop(ProgressBar);
            LabelBytes.Text = LabelBytes.Text == "Connecting..." ? "Files already exist" : LabelBytes.Text;
        }

        private string DownloadedProgress(double bytesIn, double bytesTotal)
        {
            if (bytesTotal == -1) { return UnitSize(bytesIn); }
            return UnitSize(bytesIn) + " of " + UnitSize(bytesTotal);
        }

        private string UnitSize(double _bytes)
        {
            string unitSimbol = _bytes < 1024 ? "bytes" :
                _bytes < 1048576 ? "KB" : "MB";

            double unitSize = _bytes < 1024 ? _bytes :
                _bytes < 1048576 ? _bytes / 1024 : _bytes / 1024 / 1024;

            if (unitSize < 10) { return (Math.Floor(unitSize * 100) / 100).ToString("n2") + " " + unitSimbol; }
            if (unitSize < 100) { return (Math.Floor(unitSize * 10) / 10).ToString("n1") + " " + unitSimbol; }
            return Math.Floor(unitSize) + " " + unitSimbol;
        }

        private void BarStart(ProgressBar bar, ProgressBarStyle style = ProgressBarStyle.Continuous, int maximum = 100)
        {
            bar.Maximum = maximum;
            bar.Value = 0;
            bar.MarqueeAnimationSpeed = 50;
            bar.Style = style;
        }

        private void BarStop(ProgressBar bar)
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
