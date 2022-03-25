using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
//
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RADB
{
    public static class Browser
    {
        public static WebClient client;
        private static bool useProxy = true;

        private static WebProxy Proxy = new WebProxy
        {
            Address = new Uri("http://cohab-proxy.cohabct.com.br:3128"),
            BypassProxyOnLocal = true,
            BypassList = new string[] { },
            Credentials = new NetworkCredential("fbirnfeld", "zumbie")
        };

        public static void Load()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            client = new WebClient()
            {
                Encoding = UTF8Encoding.UTF8,
            };

            if (useProxy)
            {
                client.Proxy = Browser.Proxy;
            }

            //web.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.15) Gecko/20110303 Firefox/3.6.15";
        }

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

        public static void startDownload(Download download)
        {
            if (client.IsBusy == false)
            {
                StartBar(download.ProgressBar, ProgressBarStyle.Marquee);
                download.LabelBytes.Text = "Opening URL... ";

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                //client.DownloadFileAsync(new Uri(RA.GetURL("API_GetGameList.php", "&i=", "12")), RA.Local_JsonFolder + "GameList.json", new List<object> { lbl, bar });
                //client.DownloadFileAsync(new Uri(@"file:///C:/Users/fbirnfeld/Downloads/DOCS/Projects/GitHub/RADB/RADB/bin/Debug/src/rsc/json/Files.json"), RA.Local_JsonFolder + "GameList.json");

                //client.DownloadFileAsync(new Uri("https://drive.google.com/u/0/uc?id=1_C8I5Vt62xbpcFF6otwRtHczXm-NY3Y8&export=download"), RA.Local_JsonFolder + "GameList.json", new List<object> { lbl, bar });
                //client.DownloadFileAsync(new Uri("http://www.cohabct.com.br/userfiles/file/Concovados/2020/classificacao_julho_2020.pdf"), RA.Local_JsonFolder + "GameList.json", new List<object> { lbl, bar });
                client.DownloadFileAsync(new Uri(download.URL), download.FileName, download);
                client.DownloadFileAsync(new Uri(download.URL), download.FileName, download);

                //client.OpenRead("http://www.cohabct.com.br/userfiles/file/Concovados/2020/classificacao_julho_2020.pdf");
                //Int64 bytes_total = Convert.ToInt64(Browser.web.ResponseHeaders["Content-Length"]);
            }
        }

        private static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Download download = e.UserState as Download;

            download.Form.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                download.LabelBytes.Text = "Downloaded " + DownloadedProgress(bytesIn, totalBytes);

                if (totalBytes == -1) { download.ProgressBar.Style = ProgressBarStyle.Marquee; return; }

                download.ProgressBar.Style = ProgressBarStyle.Blocks;

                double percentage = bytesIn / totalBytes * 100;
                int barValue = int.Parse(Math.Truncate(percentage).ToString());
                if (barValue > 0 && barValue <= download.ProgressBar.Maximum && barValue > download.ProgressBar.Value)
                {
                    download.ProgressBar.Value = barValue;
                }
            });
        }

        private static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Download download = e.UserState as Download;
            download.Form.BeginInvoke((MethodInvoker)delegate
            {
                StoptBar(download.ProgressBar);
                download.LabelTime.Text = File.GetLastWriteTime(download.FileName).ToString();
            });
        }

        private static string DownloadedProgress(double bytesIn, double bytesTotal)
        {
            if (bytesTotal == -1) { return CalculateFileSize(bytesIn); }
            return CalculateFileSize(bytesIn) + " of " + CalculateFileSize(bytesTotal);
        }

        private static string CalculateFileSize(double _bytes)
        {
            string unitSimbol = _bytes < 1024 ? "bytes" :
                _bytes < 1048576 ? "KB" : "MB";

            double unitSize = _bytes < 1024 ? _bytes :
                _bytes < 1048576 ? _bytes / 1024 : _bytes / 1024 / 1024;

            if (unitSize < 10) { return (Math.Floor(unitSize * 100) / 100).ToString("n2") + " " + unitSimbol; }
            if (unitSize < 100) { return (Math.Floor(unitSize * 10) / 10).ToString("n1") + " " + unitSimbol; }
            return Math.Floor(unitSize) + " " + unitSimbol;
        }

        private static void StartBar(ProgressBar bar, ProgressBarStyle style = ProgressBarStyle.Continuous, int maximum = 100)
        {
            bar.Style = style;
            bar.MarqueeAnimationSpeed = 1;
            bar.Maximum = maximum;
            bar.Value = 0;
        }

        private static void StepBar(ProgressBar bar)
        {
            bar.Value += bar.Step;
        }

        private static void StoptBar(ProgressBar bar)
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

        public static JObject ToJObject(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) { return new JObject(); }

            string content = path;
            if (path.IndexOf("http://") >= 0 || path.IndexOf("https://") >= 0)
            {
                content = DownloadString(path);
            }
            JObject result = JsonConvert.DeserializeObject<JObject>(content);
            return result;
        }

        //public static T ToObject<T>(string path) where T : new()
        //{
        //    if (string.IsNullOrWhiteSpace(path)) { return new T(); }

        //    string content = path;
        //    if (path.IndexOf("http://") >= 0 || path.IndexOf("https://") >= 0)
        //    {
        //        content = DownloadString(path);
        //    }
        //    T result = JsonConvert.DeserializeObject<T>(content);
        //    return result;
        //}

        //public static T ToObjectFromFile<T>(string path) where T : new()
        //{
        //    if (string.IsNullOrWhiteSpace(path)) { return new T(); }

        //    string content = path;
        //    if (path.IndexOf("http://") >= 0 || path.IndexOf("https://") >= 0)
        //    {
        //        content = DownloadString(path);
        //    }
        //    T result = JsonConvert.DeserializeObject<T>(content);
        //    return result;
        //}
    }
}