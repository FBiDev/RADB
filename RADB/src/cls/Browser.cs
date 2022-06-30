using System;
using System.Collections.Generic;
using System.Linq;
//
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

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
            WebClientExtend client = new WebClientExtend();
        }

        public async static Task<string> Download(string url)
        {
            return await Task<string>.Run(() =>
            {
                string data = string.Empty;

                using (WebClientExtend client = new WebClientExtend())
                {
                    url = url + "&random=" + new Random().Next();

                    var dataBytes = client.DownloadData(url);
                    data = Encoding.UTF8.GetString(dataBytes);

                    if (client.HeaderExist("X-Cache") && client.ResponseHeaders["X-Cache"] != "HIT")
                    {
                        var a = 1;
                    }
                }

                return data;
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