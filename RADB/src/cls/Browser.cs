using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.IO.Compression;
using System.ComponentModel;

namespace RADB
{
    public class MyWebClient : WebClient
    {
        private string FileDownloaded;
        private string GzipExtension { get { return ".gz"; } }
        private bool GzipContent { get { return ResponseHeaders[HttpResponseHeader.ContentEncoding] == "gzip"; } }

        public MyWebClient()
            : base()
        {
            Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate, br";
            Encoding = Encoding.UTF8;
            Proxy = Browser.Proxy;
        }

        public new Task DownloadFileTaskAsync(Uri address, string fileName)
        {
            FileDownloaded = fileName;
            return base.DownloadFileTaskAsync(address, fileName);
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = base.GetWebResponse(request, result);
            return response;
        }

        protected override void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
        {
            if (GzipContent)
            {
                FileInfo fileToDecompress = new FileInfo(FileDownloaded);

                string oldName = fileToDecompress.FullName;
                string gzName = oldName.Remove(oldName.Length - fileToDecompress.Extension.Length);
                gzName += GzipExtension;

                File.Delete(gzName);
                File.Move(oldName, gzName);
                fileToDecompress = new FileInfo(gzName);

                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    using (FileStream decompressedFileStream = File.Create(oldName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                        }
                    }
                }
            }

            base.OnDownloadFileCompleted(e);
            return;
        }

        public string DecodeGzip(string str)
        {
            byte[] gzBuffer = Encoding.GetBytes(str);

            using (MemoryStream ms = new MemoryStream())
            {
                long msgLength = BitConverter.ToInt64(gzBuffer, 0);
                ms.Write(gzBuffer, 0, gzBuffer.Length);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                int length;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    length = zip.Read(buffer, 0, buffer.Length);
                }

                var data = new byte[length];
                Array.Copy(buffer, data, length);
                return Encoding.UTF8.GetString(data);

            }
        }
    }

    public static class Browser
    {
        public static int MaxConnections { get { return ServicePointManager.DefaultConnectionLimit; } }
        public static Encoding Encoding = Encoding.UTF8;

        public static bool useProxy { get { return Environment.MachineName.Equals("COHAB-CT0920"); } }

        public static WebProxy Proxy
        {
            get
            {
                if (useProxy)
                {
                    return new WebProxy
                    {
                        Address = new Uri("http://cohab-proxy.cohabct.com.br:3128"),
                        BypassProxyOnLocal = true,
                        BypassList = new string[] { },
                        Credentials = new NetworkCredential("fbirnfeld", "zumbie")
                    };
                }

                return new WebProxy();
            }
        }

        public static void Load()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 128;
        }

        public static Task<string> DownloadString(string url)
        {
            return Task<string>.Run(() =>
            {
                using (MyWebClient client = new MyWebClient())
                {
                    string data = client.DownloadString(url);
                    var d = client.DecodeGzip(data);
                    return data;
                }
            });
        }

        public static JObject ToJObject(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) { return new JObject(); }

            string content = path;
            if (path.IndexOf("http://") >= 0 || path.IndexOf("https://") >= 0)
            {
                //content = DownloadString(path);
            }
            else
            {
                using (StreamReader file = new StreamReader(path))
                {
                    content = file.ReadToEnd();
                }
            }
            JObject result = JsonConvert.DeserializeObject<JObject>(content);
            return result;
        }
    }
}