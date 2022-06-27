using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
//
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

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

        [DllImport("shlwapi.dll", EntryPoint = "PathFileExistsW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _PathFileExists([MarshalAs(UnmanagedType.LPWStr)]string pszPath);

        public bool PathFileExists(string fileName) { return _PathFileExists(fileName); }

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
            BytesToReceive = 0;
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
            string progressBytes = string.Empty;
            string progressFiles = string.Empty;

            foreach (DownloadFile file in FilesToDownload)
            {
                if (file == null) { continue; }

                using (var client = new WebClientExtend())
                {
                    client.DownloadProgressChanged += (sender, args) =>
                    {
                        file.BytesReceived = args.BytesReceived;
                        file.TotalBytesToReceive = args.TotalBytesToReceive;
                        file.ProgressPercentage = args.ProgressPercentage;

                        BytesReceived = 0;
                        BytesToReceive = 0;
                        float ProgressPercentage = 0;

                        foreach (DownloadFile f in FilesToDownload)
                        {
                            BytesReceived += f.BytesReceived;
                            BytesToReceive += f.TotalBytesToReceive;
                            ProgressPercentage += (f.ProgressPercentage / FilesTotal);
                        }

                        if (BytesToReceive < -1) { BytesToReceive = -1; }

                        Percentage = ProgressPercentage > 100 ? 100 : (int)(Math.Ceiling(ProgressPercentage));

                        progressBytes = "Downloaded " + DownloadedProgress(BytesReceived, BytesToReceive);
                        Result = progressBytes + progressFiles;

                        Status = DownloadStatus.ProgressChanged;
                        ProgressChanged();
                    };

                    client.DownloadFileCompleted += (sender, args) =>
                    {
                        //Downloaded All Files
                        if (Percentage == 100)
                        {
                            FilesCompleted = FilesTotal;

                            TimeElapsed = new TimeSpan(DateTime.Now.Ticks - TimeStart.Ticks);
                            TimeCompleted = DateTime.Now;

                            Status = DownloadStatus.Completed;
                        }
                        else
                        {
                            FilesCompleted++;
                            Status = DownloadStatus.FileDownloaded;
                        }

                        progressFiles = " (" + FilesCompleted + "/" + FilesTotal + ")";
                        Result = progressBytes + progressFiles;

                        ProgressChanged();
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

                if (Status != DownloadStatus.Completed)
                {
                    Status = DownloadStatus.Stopped;
                    Result = Result == connecting ? "Files already exist" : Result;
                    ProgressChanged();
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        private string DownloadedProgress(double bytesIn, double bytesTotal)
        {
            if (bytesTotal <= 0) { return Archive.CalculateSize(bytesIn); }
            return Archive.CalculateSize(bytesIn) + " of " + Archive.CalculateSize(bytesTotal);
        }
    }
}
