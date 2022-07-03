using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IO;
using System.Windows.Forms;

namespace RADB
{
    public class Download
    {
        public string FolderBase { get; set; }
        public List<DownloadFile> Files { get; set; }
        public List<DownloadFile> FilesToDownload { get; set; }
        public int FilesCompleted { get; set; }
        public bool Overwrite { get; set; }

        public DateTime TimeStart { get; set; }
        public DateTime TimeCompleted { get; set; }
        public TimeSpan TimeElapsed { get; set; }

        public long BytesReceived { get; set; }
        public long TotalBytesToReceive { get; set; }
        public int Percentage { get; set; }

        public string Result { get; set; }
        public event Action ProgressChanged = delegate { };
        public DownloadStatus Status { get; set; }

        public enum DownloadStatus
        {
            Connecting,
            ProgressChanged,
            FileDownloaded,
            NextFiles,
            Completed,
            Stopped,
        }

        public Download()
        {
            Overwrite = true;
            Files = new List<DownloadFile>();
            FilesToDownload = new List<DownloadFile>();
        }

        public async Task Start()
        {
            if (string.IsNullOrWhiteSpace(FolderBase)) { FolderBase = @".\"; }

            TimeStart = DateTime.Now;
            TimeCompleted = TimeStart;
            TimeElapsed = default(TimeSpan);

            List<Task> Tasks = new List<Task>();
            //Remove Files with same URL
            Files = Files.Distinct().ToList();
            FilesCompleted = 0;

            BytesReceived = 0;
            TotalBytesToReceive = 0;
            Percentage = 0;

            string connecting = "Connecting..." + Environment.NewLine;
            Result = connecting;

            Status = DownloadStatus.Connecting;
            ProgressChanged();

            FilesToDownload.Clear();

            if (Overwrite == false)
            {
                DirectoryInfo di = new DirectoryInfo(FolderBase);
                IEnumerable<string> physicalFiles = di.GetFiles("*.*", SearchOption.AllDirectories)
                                    .Where(fs => fs.Length > 0)
                                    .Select(f => Archive.RelativePath(f.FullName) + Path.GetFileName(f.Name));

                var fileNames = Files.Select(f => f.Path);

                var setOfFiles = new HashSet<string>(physicalFiles);
                var notPresent = (from name in fileNames
                                  where setOfFiles.Contains(name) == false
                                  select name).ToList();

                notPresent.Sort();
                Files = Files.OrderBy(f => f.Path).ToList();

                //Add files that not already downloaded
                foreach (DownloadFile file in Files)
                {
                    foreach (string nt in notPresent)
                    {
                        if (file.Path.Equals(nt))
                        {
                            FilesToDownload.Add(file);
                            break;
                        }
                    }
                }
            }
            else
            {
                FilesToDownload.AddRange(Files);
            }

            int FilesTotal = FilesToDownload.Count;

            foreach (DownloadFile file in FilesToDownload)
            {
                if (file == null) { continue; }

                using (var client = new WebClientExtend())
                {
                    client.DownloadProgressChanged += (sender, args) =>
                    {
                        if (args.TotalBytesToReceive > 0 && args.TotalBytesToReceive > file.TotalBytesToReceive)
                        {
                            TotalBytesToReceive += (args.TotalBytesToReceive - file.TotalBytesToReceive);
                            file.TotalBytesToReceive = args.TotalBytesToReceive;
                        }

                        if (args.BytesReceived > 0 && args.BytesReceived > file.BytesReceived)
                        {
                            BytesReceived += (args.BytesReceived - file.BytesReceived);
                            file.BytesReceived = args.BytesReceived;
                        }

                        float ProgressPercentage = 0;
                        file.ProgressPercentage = args.ProgressPercentage;

                        foreach (DownloadFile f in FilesToDownload)
                        {
                            ProgressPercentage += (f.ProgressPercentage / FilesTotal);
                        }

                        Percentage = ProgressPercentage > 100 ? 100 : (int)(Math.Ceiling(ProgressPercentage));

                        CalculateResult();

                        Status = DownloadStatus.ProgressChanged;
                        ProgressChanged();
                    };

                    client.DownloadFileCompleted += (sender, args) =>
                    {
                        FilesCompleted++;
                        Status = DownloadStatus.FileDownloaded;

                        //Downloaded All Files
                        if (Percentage == 100)
                        {
                            Status = DownloadStatus.Completed;
                        }
                        else
                        {
                            CalculateResult();
                        }
                    };

                    Tasks.Add(client.DownloadFileTaskAsync(new Uri(file.URL), file.Path));

                    if (Tasks.Count == Browser.MaxConnections)
                    {
                        Status = DownloadStatus.NextFiles;
                        ProgressChanged();

                        await Task.WhenAll(Tasks);
                        Tasks.Clear();
                    }
                }
            }

            try
            {
                await Task.WhenAll(Tasks.Where(i => i != null));

                if (Percentage == 100)
                {
                    BytesReceived = 0;
                    TotalBytesToReceive = 0;
                    foreach (DownloadFile f in FilesToDownload)
                    {
                        BytesReceived += f.BytesReceived;
                        TotalBytesToReceive += f.TotalBytesToReceive;
                    }

                    FilesCompleted = FilesTotal;

                    TimeElapsed = new TimeSpan(DateTime.Now.Ticks - TimeStart.Ticks);
                    TimeCompleted = DateTime.Now;

                    CalculateResult();
                }
                else
                {
                    Status = DownloadStatus.Stopped;
                    Result = Result == connecting ? "Files already exist" : Result;
                }

                ProgressChanged();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        private void CalculateResult()
        {
            int FilesTotal = FilesToDownload.Count;
            string progressBytes = string.Empty;
            string progressFiles = string.Empty;

            progressBytes = "Downloaded " + DownloadedProgress(BytesReceived, TotalBytesToReceive);
            progressFiles = " (" + FilesCompleted + "/" + FilesTotal + ")";
            Result = progressBytes + progressFiles;
        }

        private string DownloadedProgress(double bytesIn, double bytesTotal)
        {
            string bytesProgress = Archive.CalculateSize(bytesIn);
            double bytesCalc = bytesTotal <= 0 ? bytesIn : bytesTotal;
            bytesProgress += " of " + Archive.CalculateSize(bytesCalc);

            return bytesProgress;
        }

        //==========
        //===Desktop
        //==========
        public void SetControls(Label resultLabel, ProgressBar resultBar, Label resultTime)
        {
            this.ProgressChanged += () => DownloadChanged(resultLabel, resultBar, resultTime);
        }

        private void DownloadChanged(Label resultLabel, ProgressBar resultBar, Label resultTime)
        {
            resultLabel.InvokeIfRequired(() =>
            {
                resultLabel.Text = this.Result;
            });

            switch (this.Status)
            {
                case Download.DownloadStatus.Connecting:

                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStart(resultBar, ProgressBarStyle.Marquee);
                    });
                    break;
                case Download.DownloadStatus.ProgressChanged:
                    resultBar.InvokeIfRequired(() =>
                    {
                        resultBar.Value = this.Percentage;
                        resultBar.Style = (this.TotalBytesToReceive == -1 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous);
                    });
                    break;
                case Download.DownloadStatus.FileDownloaded:
                    break;
                case Download.DownloadStatus.NextFiles:
                    break;
                case Download.DownloadStatus.Completed:
                    resultTime.InvokeIfRequired(() =>
                    {
                        resultTime.Text = this.TimeCompleted.ToString();
                    });
                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStop(resultBar);
                    });
                    break;
                case Download.DownloadStatus.Stopped:
                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStop(resultBar);
                    });
                    break;
                default:
                    break;
            }
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
