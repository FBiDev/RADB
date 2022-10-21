using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;
using System.Net;

namespace RADB
{
    public class WebClientExtend : WebClient
    {
        private bool GZipContent
        {
            get
            {
                if (HeaderExist("Content-Encoding"))
                {
                    return ResponseHeaders[HttpResponseHeader.ContentEncoding] == "gzip";
                }
                return false;
            }
        }

        public bool HeaderExist(string headerName)
        {
            if (ResponseHeaders != null && Convert.ToBoolean(ResponseHeaders.AllKeys.Where(h => h.ToLower() == headerName.ToLower()).Count()))
            {
                return true;
            }
            return false;
        }

        private bool _GZipEnable { get; set; }
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
        private string GZipExtension { get { return ".gz"; } }
        private long GZipSize { get; set; }
        private long GZipSizeUncompressed { get; set; }
        public DownloadFile FileDownloaded;

        private bool _Error;
        public bool Error { get { return _Error; } }
        public string ErrorMessage;

        public CookieContainer CookieContainer { get; private set; }

        public WebClientExtend()
            : base()
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

            string ContentType = this.ResponseHeaders[HttpResponseHeader.ContentType];

            if (ContentType.IndexOf("ISO-8859-1", 0, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                Encoding utf8 = Encoding.UTF8;

                byte[] isoBytes = data;
                byte[] utfBytes = Encoding.Convert(iso, utf8, isoBytes);
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
            HttpWebResponse response = null;
            byte[] data = new byte[0];

            try
            {
                _Error = false;
                data = await base.DownloadDataTaskAsync(new Uri(address));
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
                    response = we.Response as HttpWebResponse;
                    ErrorMessage = "Download Error: \r\n\r\n" + "Status Code: " + (int)response.StatusCode + " " + response.StatusDescription;
                }
            }

            if (GZipContent)
            {
                GetGZipSize(data);
                //string base64 = Convert.ToBase64String(data);
                data = DecodeGZip(data);
            }
            return data;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
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
                        ErrorMessage = "Error to Access: \r\n\r\n" + response.ResponseUri.AbsoluteUri;
                    }
                    else
                    {
                        ErrorMessage = "Error to download: \r\n\r\n" + FileDownloaded.URL;
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

            if (GZipContent)
            {
                FileInfo fileToDecompress = new FileInfo(FileDownloaded.Path);

                string oldName = fileToDecompress.FullName;
                //string NoExtensionName = oldName.Remove(oldName.Length - fileToDecompress.Extension.Length);
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

        private string SetTempFile(string fileName)
        {
            return fileName + ".tmp";
        }

        private void RevertTempFile(string fileName)
        {
            string newFile = fileName.Substring(0, fileName.Length - 4);
            FileDownloaded.Path = newFile;

            if (File.Exists(newFile))
            {
                File.Delete(newFile);
            }
            File.Move(fileName, newFile);
        }

        private void GetGZipSize(byte[] data)
        {
            if (GZipContent)
            {
                GZipSize = data.Length;

                var last4 = new byte[4];
                last4[0] = data[data.Length - 4];
                last4[1] = data[data.Length - 3];
                last4[2] = data[data.Length - 2];
                last4[3] = data[data.Length - 1];
                GZipSizeUncompressed = BitConverter.ToUInt32(last4, 0);

                //var gz = Archive.CalculateSize(GZipSize);
                //var gzUn = Archive.CalculateSize(GZipSizeUncompressed);
            }
        }

        private byte[] DecodeGZip(byte[] gzBuffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
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
                //return Encoding.UTF8.GetString(data);
                return data;
            }
        }
    }
}
