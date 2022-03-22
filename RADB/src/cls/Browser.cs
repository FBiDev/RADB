using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
//
using System.Net;
using System.Text;

namespace RADB
{
    public static class Browser
    {
        private static bool useProxy = true;
        private static WebClient web;

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
            web = new WebClient()
            {
                Encoding = UTF8Encoding.UTF8,
            };

            if (useProxy)
            {
                web.Proxy = Browser.Proxy;
            }

            //web.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.15) Gecko/20110303 Firefox/3.6.15";
        }

        public static string DownloadString(string URL)
        {
            string data = web.DownloadString(URL);
            return data;
        }

        public static byte[] DownloadData(string URL)
        {
            byte[] data = web.DownloadData(URL);
            return data;
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