using System;
using System.Collections.Generic;
//
using System.Net;

namespace RADB
{
    public static class Config
    {
        public static string URI_API = "http://retroachievements.org/API/";
        public static string AuthQS = "?z=FBiDev&y=uBuG840fXTyKSQvS8MFKX5d40fOelJ29";
        public static WebProxy Proxy = new WebProxy
        {
            Address = new Uri("http://cohab-proxy.cohabct.com.br:3128"),
            BypassProxyOnLocal = true,
            BypassList = new string[] { },
            Credentials = new NetworkCredential("fbirnfeld", "zumbie")
        };
    }
}