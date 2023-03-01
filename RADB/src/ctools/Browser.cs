using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GNX;

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
                if (useProxy == false) return new WebProxy();

                return new WebProxy
                {
                    Address = new Uri("http://cohab-proxy.cohabct.com.br:3128"),
                    BypassProxyOnLocal = true,
                    BypassList = new string[] { },
                    Credentials = new NetworkCredential("fbirnfeld", "zumbie")
                };
            }
        }

        //===Downloads
        public static Download dlConsoles = new Download { Overwrite = true, FolderBase = Folder.Console, };
        public static Download dlConsolesGamesIcon = new Download() { Overwrite = false, FolderBase = Folder.IconsBase, };

        public static Download dlGames = new Download { Overwrite = true, FolderBase = Folder.GameData, };
        public static Download dlGamesIcon = new Download() { Overwrite = false, FolderBase = Folder.IconsBase, };
        public static Download dlGamesBadges = new Download() { Overwrite = false, FolderBase = Folder.BadgesBase, };

        public static Download dlGameExtend = new Download { Overwrite = true, FolderBase = Folder.GameDataExtendBase, };
        public static Download dlGameExtendImages = new Download { Overwrite = false, FolderBase = Folder.Images, };

        public static WebClientExtend RALogin = new WebClientExtend();

        public static void Load()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 128;

            var j = JsonConvert.DeserializeObject<JObject>("{\"LoadJsonDLL\":\"...\"}");
        }

        public static async Task SystemLogin()
        {
            BIND.RALogged = false;

            var html = await RALogin.DownloadString(RA.HOST_URL);
            var token = html.GetBetween("_token\" value=\"", "\">");

            var values = new NameValueCollection
            {
                {"_token", token},
                { "u", "RADatabase" },
                { "p", "RADatabase123" }
            };

            await RALogin.UploadValuesTaskAsync(new Uri(RA.Login_URL), values);
            if (RALogin.Error)
            {
                MessageBox.Show(RALogin.ErrorMessage);
            }

            var html2 = await RALogin.DownloadString(RA.HOST_URL);
            var login = html2.GetBetween("request/auth/login", "php");

            BIND.RALogged = login != ".";
        }

        static readonly Random rand = new Random();
        public async static Task<string> DownloadString(string url, bool addRandomNumber = false)
        {
            return await Task.Run(async () =>
            {
                string data = string.Empty;

                using (var client = new WebClientExtend())
                {
                    url = addRandomNumber ? url +=
                          (url.IndexOf("?", StringComparison.Ordinal) < 0 ? "?" : "&") + "random=" + rand.Next()
                        : (url);

                    data = await client.DownloadString(url);

                    if (client.Error)
                    {
                        MessageBox.Show(client.ErrorMessage);
                    }
                    //if (client.HeaderValue("X-Cache") != "HIT") { var a = 1; }
                }

                return data;
            });
        }
    }
}