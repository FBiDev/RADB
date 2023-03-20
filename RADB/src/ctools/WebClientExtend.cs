using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IO;
using System.IO.Compression;
using System.ComponentModel;
using System.Net;

namespace RADB
{
    public class WebClientExtend : WebClient
    {
        public bool HeaderExist(string headerName)
        {
            return ResponseHeaders != null && ResponseHeaders.AllKeys.Any(h => h.ToLower() == headerName.ToLower());
        }

        public string HeaderValue(string headerName)
        {
            return HeaderExist(headerName) ? ResponseHeaders.AllKeys.Single(h => h.ToLower() == headerName.ToLower()) : string.Empty;
        }

        bool IsGZipContent
        {
            get { return HeaderExist("Content-Encoding") && ResponseHeaders[HttpResponseHeader.ContentEncoding] == "gzip"; }
        }

        bool _GZipEnable { get; set; }
        public bool GZipEnable
        {
            get { return _GZipEnable; }
            set
            {
                _GZipEnable = value;
                if (_GZipEnable)
                {
                    Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate, br";
                }
                else
                {
                    Headers[HttpRequestHeader.AcceptEncoding] = "";
                }
            }
        }
        const string GZipExtension = ".gz";
        long GZipSize { get; set; }
        long GZipSizeUncompressed { get; set; }
        public DownloadFile FileDownloaded;

        public static Dictionary<string, string> CustomErrorMessages = new Dictionary<string, string> { };

        bool _Error;
        public bool Error { get { return _Error; } }
        string _ErrorMessage { get; set; }
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                var link = CustomErrorMessages.SingleOrDefault(x => value.Contains(x.Key));
                if (link.Value != null)
                    _ErrorMessage += link.Value;
                else
                    _ErrorMessage = value;
            }
        }

        public CookieContainer CookieContainer { get; private set; }

        public WebClientExtend()
        {
            GZipEnable = true;

            Encoding = Encoding.UTF8;
            Proxy = Browser.Proxy;

            CookieContainer = new CookieContainer();
        }

        public new Task DownloadFileTaskAsync(string address, string fileName)
        {
            return DownloadFileTaskAsync(new Uri(address), fileName);
        }

        public new Task DownloadFileTaskAsync(Uri address, string fileName)
        {
            FileDownloaded = new DownloadFile(address.ToString(), fileName);

            if (string.IsNullOrWhiteSpace(FileDownloaded.Name)) { return null; }

            fileName = SetTempFile(fileName);
            FileDownloaded = new DownloadFile(address.ToString(), fileName);

            return base.DownloadFileTaskAsync(address, fileName);
        }

        public new async Task<byte[]> UploadValuesTaskAsync(Uri address, NameValueCollection data)
        {
            byte[] x = default(byte[]);
            try
            {
                x = await base.UploadValuesTaskAsync(address, data);
            }
            catch (WebException we)
            {
                _Error = true;
                ErrorMessage = we.Message;
            }
            return x;
        }

        public async new Task<string> DownloadString(string address)
        {
            string msg = string.Empty;
            byte[] data = await DownloadData(address);

            if (_Error) { return msg; }

            if (ResponseHeaders == null) { return msg; }

            string ContentType = ResponseHeaders[HttpResponseHeader.ContentType];

            if (ContentType.IndexOf("ISO-8859-1", 0, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var iso = Encoding.GetEncoding("ISO-8859-1");
                Encoding utf8 = Encoding.UTF8;

                byte[] isoBytes = data;
                var utfBytes = Encoding.Convert(iso, utf8, isoBytes);
                msg = utf8.GetString(utfBytes);
            }
            else if (ContentType.IndexOf("image/", 0, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                msg = "data:" + ContentType + ";base64," + Convert.ToBase64String(data);
            }
            else
            {
                msg = Encoding.GetString(data);
            }

            return msg;
        }

        public async new Task<byte[]> DownloadData(string address)
        {
            byte[] data = new byte[0];

            try
            {
                _Error = false;
                data = await DownloadDataTaskAsync(new Uri(address));
            }
            catch (WebException we)
            {
                if (_Error) { return data; }

                _Error = true;
                if (we.Response == null)
                {
                    ErrorMessage = "Download Error: \r\n\r\n" + "Status: " + we.Status + "\r\n\r\n" + we.Message + "\r\n\r\n" + we.InnerException.Message;
                }
                else
                {
                    var response = we.Response as HttpWebResponse;
                    ErrorMessage = "Download Error: \r\n\r\n" + "Status Code: " + (int)response.StatusCode + " " + response.StatusDescription;
                }
            }

            if (IsGZipContent)
            {
                GetGZipSize(data);
                data = DecodeGZip(data);
            }
            return data;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address) as HttpWebRequest;
            request.CookieContainer = CookieContainer;

            if (FileDownloaded == null)
            {
                //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            return request;
        }

        //For DownloadString
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            return base.GetWebResponse(request);
        }

        //For DownloadFile
        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            HttpWebResponse response = null;
            try
            {
                _Error = false;
                response = base.GetWebResponse(request, result) as HttpWebResponse;
            }
            catch (WebException we)
            {
                _Error = true;
                if (we.Response == null)
                {
                    ErrorMessage = "Download Error: \r\n\r\n" + "Status: " + we.Status + "\r\n\r\n" + we.Message;
                }
                else
                {
                    response = we.Response as HttpWebResponse;
                    if (FileDownloaded == null)
                    {
                        ErrorMessage = "Error to Access: \r\n\r\n";
                        ErrorMessage += response.ResponseUri.AbsoluteUri;
                    }
                    else
                    {
                        ErrorMessage = "Error to download: \r\n\r\n";
                        ErrorMessage += FileDownloaded.URL;
                    }
                    ErrorMessage += "\r\n\r\n" + "Status Code: " + (int)response.StatusCode + " " + response.StatusDescription;
                }
            }

            return response;
        }

        protected override void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
        {
            if (FileDownloaded != null)
            {
                FileDownloaded.BytesReceived = e.BytesReceived;
                FileDownloaded.TotalBytesToReceive = e.TotalBytesToReceive;
                FileDownloaded.ProgressPercentage = e.ProgressPercentage;
            }

            base.OnDownloadProgressChanged(e);
        }

        protected override void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
        {
            if (_Error)
            {
                File.Delete(FileDownloaded.Path);
                base.OnDownloadFileCompleted(e);
                return;
            }

            RevertTempFile(FileDownloaded.Path);

            if (IsGZipContent)
            {
                var fileToDecompress = new FileInfo(FileDownloaded.Path);

                string oldName = fileToDecompress.FullName;
                string gzName = oldName + GZipExtension;

                File.Delete(gzName);
                File.Move(oldName, gzName);
                fileToDecompress = new FileInfo(gzName);

                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        originalFileStream.CopyTo(ms);
                        originalFileStream.Position = 0;
                        GetGZipSize(ms.ToArray());
                    }
                    using (FileStream decompressedFileStream = File.Create(oldName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                        }
                    }
                }
                File.Delete(gzName);
            }

            base.OnDownloadFileCompleted(e);
            return;
        }

        string SetTempFile(string fileName)
        {
            return fileName + ".tmp";
        }

        void RevertTempFile(string fileName)
        {
            var newFile = fileName.Substring(0, fileName.Length - 4);
            FileDownloaded.Path = newFile;

            if (File.Exists(newFile))
            {
                File.Delete(newFile);
            }
            File.Move(fileName, newFile);
        }

        void GetGZipSize(byte[] data)
        {
            if (IsGZipContent)
            {
                GZipSize = data.Length;

                byte[] last4 = new byte[4];
                last4[0] = data[data.Length - 4];
                last4[1] = data[data.Length - 3];
                last4[2] = data[data.Length - 2];
                last4[3] = data[data.Length - 1];
                GZipSizeUncompressed = BitConverter.ToUInt32(last4, 0);
            }
        }

        byte[] DecodeGZip(byte[] gzBuffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 0, gzBuffer.Length);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                int length;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    length = zip.Read(buffer, 0, buffer.Length);
                }

                byte[] data = new byte[length];
                Array.Copy(buffer, data, length);
                return data;
            }
        }
    }
}