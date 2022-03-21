using System;
using System.Collections.Generic;
//
using System.Net;
using System.Text;

namespace RADB
{
    public static class Config
    {
        public static bool useProxy = false;
        private static WebClient web;
        public static string URI_API = "http://retroachievements.org/API/";
        public static string AuthQS = "?z=FBiDev&y=uBuG840fXTyKSQvS8MFKX5d40fOelJ29";
        public static WebProxy Proxy = new WebProxy
        {
            Address = new Uri("http://cohab-proxy.cohabct.com.br:3128"),
            BypassProxyOnLocal = true,
            BypassList = new string[] { },
            Credentials = new NetworkCredential("fbirnfeld", "zumbie")
        };

        public static void WebStart()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            web = new WebClient() { };
            if (useProxy) { web.Proxy = Config.Proxy; }

            //web.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.15) Gecko/20110303 Firefox/3.6.15";
        }

        public static string DownloadString(string URL)
        {
            var content = web.DownloadString(URL);
            byte[] bytes = Encoding.Default.GetBytes(content);
            content = Encoding.UTF8.GetString(bytes);
            return content;
        }

        public static byte[] DownloadData(string URL)
        {
            byte[] data = web.DownloadData(URL);
            return data;
        }

    }
}