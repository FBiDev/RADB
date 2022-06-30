using System;
using System.Collections.Generic;
using System.Linq;
//
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RADB
{
    public static class Browser
    {
        public static int MaxConnections { get { return ServicePointManager.DefaultConnectionLimit; } }
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

            var j = JsonConvert.DeserializeObject<JObject>("{\"LoadJsonDLL\":\"...\"}");
        }

        public async static Task<string> DownloadData(string url)
        {
            return await Task<string>.Run(() =>
            {
                using (var client = new WebClientExtend())
                {
                    client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    client.Headers.Add("Cache-Control", "max-age=0");

                    byte[] data = client.DownloadData(url + "&rn=" + new Random());
                    return client.Encoding.GetString(data);
                }
            });
        }

        public async static Task<string> DownloadString(string url)
        {
            return await Task<string>.Run(() =>
            {
                using (var client = new WebClientExtend() { GZipEnable = false })
                {
                    client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    client.Headers.Add("Cache-Control", "max-age=0");

                    string data = client.DownloadString(url + "&rn=" + new Random());
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