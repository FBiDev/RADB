using System;
using System.Collections.Generic;
using System.Linq;
//
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace RADB
{
    public class WebClientExtend : WebClient
    {
        private bool GZipContent
        {
            get
            {
                if (ResponseHeaders != null && ResponseHeaders.AllKeys.Contains("Content-Encoding"))
                {
                    return ResponseHeaders[HttpResponseHeader.ContentEncoding] == "gzip";
                }
                return false;
            }
        }
        private string GZipExtension { get { return ".gz"; } }
        private long GZipSize { get; set; }
        private long GZipSizeUncompressed { get; set; }
        private string FileDownloaded;
        private bool Error;

        public WebClientExtend()
            : base()
        {
            Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate, br";
            Encoding = Encoding.UTF8;
            Proxy = Browser.Proxy;
        }

        public new Task DownloadFileTaskAsync(string address, string fileName)
        {
            return DownloadFileTaskAsync(new Uri(address), fileName);
        }

        public new Task DownloadFileTaskAsync(Uri address, string fileName)
        {
            FileDownloaded = fileName;
            return base.DownloadFileTaskAsync(address, fileName);
        }

        public new byte[] DownloadData(string address)
        {
            var data = base.DownloadData(address);

            GetGZipSize(data);

            if (GZipContent)
            {
                //string base64 = Convert.ToBase64String(data);
                data = DecodeGZip(data);
            }
            return data;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
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
                response = base.GetWebResponse(request, result) as HttpWebResponse;
            }
            catch (WebException we)
            {
                response = we.Response as HttpWebResponse;
                Error = true;
                MessageBox.Show("Error to download: " + FileDownloaded + "\r\n\r\n" + "Status Code: " + (int)response.StatusCode + " " + response.StatusDescription);
            }

            return response;
        }

        protected override void OnDownloadFileCompleted(AsyncCompletedEventArgs e)
        {
            if (Error)
            {
                File.Delete(FileDownloaded);
                base.OnDownloadFileCompleted(e);
                return;
            }

            if (GZipContent)
            {
                FileInfo fileToDecompress = new FileInfo(FileDownloaded);

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

                            //GZipSize = originalFileStream.Length;
                            //GZipSizeUncompressed = decompressedFileStream.Length;
                        }
                    }
                }
            }

            base.OnDownloadFileCompleted(e);
            return;
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
