using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Threading.Tasks;

namespace RADB
{
    public class Download
    {
        public List<DownloadFile> Files { get; set; }
        public List<DownloadFile> FilesToDownload { get; set; }
        public int FilesCompleted { get; set; }
        public bool Overwrite { get; set; }

        public DateTime TimeStart { get; set; }
        public DateTime TimeCompleted { get; set; }
        public TimeSpan TimeElapsed { get; set; }

        public long BytesReceived { get; set; }
        public long BytesToReceive { get; set; }
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
            TimeStart = DateTime.Now;
            TimeCompleted = TimeStart;
            TimeElapsed = default(TimeSpan);

            List<Task> Tasks = new List<Task>();
            //Remove Files with same URL
            Files = Files.Distinct().ToList();
            FilesCompleted = 0;

            BytesReceived = 0;
            BytesToReceive = 0;
            Percentage = 0;

            Result = "Connecting...";

            Status = DownloadStatus.Connecting;
            ProgressChanged();

            //Add files that not already downloaded
            FilesToDownload.Clear();

            foreach (DownloadFile file in Files)
            {
                if (File.Exists(file.Path) == false || new FileInfo(file.Path).Length == 0 || Overwrite == true)
                {
                    FilesToDownload.Add(file);
                }
            }

            int FilesTotal = FilesToDownload.Count;

            foreach (DownloadFile file in FilesToDownload)
            {
                if (file == null) { continue; }

                using (var client = new WebClientExtend())
                {
                    string progressBytes = string.Empty;

                    client.DownloadProgressChanged += (sender, args) =>
                    {
                        file.BytesReceived = args.BytesReceived;
                        file.TotalBytesToReceive = args.TotalBytesToReceive;
                        file.ProgressPercentage = args.ProgressPercentage;

                        BytesReceived = 0;
                        BytesToReceive = 0;
                        float ProgressPercentage = 0;

                        FilesToDownload.ForEach(f =>
                        {
                            BytesReceived += f.BytesReceived;
                            BytesToReceive += f.TotalBytesToReceive;
                            ProgressPercentage += (f.ProgressPercentage / FilesTotal);
                        });

                        progressBytes = "Downloaded " + DownloadedProgress(BytesReceived, BytesToReceive);
                        Result = progressBytes;
                        Percentage = ProgressPercentage > 100 ? 100 : (int)(Math.Ceiling(ProgressPercentage));

                        Status = DownloadStatus.ProgressChanged;
                        ProgressChanged();
                    };

                    client.DownloadFileCompleted += (sender, args) =>
                    {
                        FilesCompleted++;
                        Result = progressBytes + " (" + FilesCompleted + "/" + FilesTotal + ")";

                        Status = DownloadStatus.FileDownloaded;
                        ProgressChanged();

                        //Downloaded All Files
                        if (FilesCompleted == FilesTotal)
                        {
                            TimeElapsed = new TimeSpan(DateTime.Now.Ticks - TimeStart.Ticks);
                            TimeCompleted = DateTime.Now;

                            Status = DownloadStatus.Completed;
                            ProgressChanged();
                        }
                    };

                    Tasks.Add(client.DownloadFileTaskAsync(new Uri(file.URL), file.Path));

                    if (Tasks.Count == Browser.MaxConnections)
                    {
                        Result = "Connecting...";

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

                Result = Result == "Connecting..." ? "Files already exist" : Result;

                Status = DownloadStatus.Stopped;
                ProgressChanged();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        private string DownloadedProgress(double bytesIn, double bytesTotal)
        {
            if (bytesTotal == -1) { return Archive.CalculateSize(bytesIn); }
            return Archive.CalculateSize(bytesIn) + " of " + Archive.CalculateSize(bytesTotal);
        }
    }
}
