using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RADB
{
    public static class Browser
    {
        public static bool useProxy = true;
        public static WebProxy Proxy
        {
            get
            {
                if (useProxy == false) { return new WebProxy(); }

                return new WebProxy
                {
                    Address = new Uri("http://cohab-proxy.cohabct.com.br:3128"),
                    BypassProxyOnLocal = true,
                    BypassList = new string[] { },
                    Credentials = new NetworkCredential("fbirnfeld", "zumbie")
                };
            }
        }

        public static Encoding Encoding = UTF8Encoding.UTF8;

        public static void Load()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 128;
            //web.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.15) Gecko/20110303 Firefox/3.6.15";
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