using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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

        //===Downloads
        public static Download dlConsoles = new Download { Overwrite = true, FolderBase = Folder.Console, };
        public static Download dlConsolesGamesIcon = new Download() { Overwrite = false, FolderBase = Folder.IconsBase, };

        public static Download dlGames = new Download { Overwrite = true, FolderBase = Folder.GameData, };
        public static Download dlGamesIcon = new Download() { Overwrite = false, FolderBase = Folder.IconsBase, };
        public static Download dlGamesBadges = new Download() { Overwrite = false, FolderBase = Folder.BadgesBase, };

        public static Download dlGameExtend = new Download { Overwrite = true, FolderBase = Folder.GameDataExtendBase, };
        public static Download dlGameExtendImages = new Download { Overwrite = false, FolderBase = Folder.Images, };

        public static WebClientExtend RALogin = new WebClientExtend();

        public async static void Load()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 128;

            await LoginTest();
        }

        public static async Task LoginTest()
        {
            //wclient.Credentials = new NetworkCredential("", "");
            var html = await RALogin.DownloadString(RA.HOST);
            var token = html.GetBetween("_token\" value=\"", "\">");

            var values = new NameValueCollection
            {
                {"_token", token},
                { "u", "RADatabase" },
                { "p", "RADatabase123" }
            };

            await RALogin.UploadValuesTaskAsync(new Uri(RA.HOST + "request/auth/login.php"), values);
            if (RALogin.Error)
            {
                MessageBox.Show(RALogin.ErrorMessage);
            }
        }

        private static Random rand = new Random();
        public async static Task<string> DownloadString(string url, bool addRandomNumber = false)
        {
            return await Task<string>.Run(async () =>
            {
                string data = string.Empty;

                using (WebClientExtend client = new WebClientExtend())
                {
                    url = addRandomNumber ? url +=
                        (url.IndexOf("?") < 0 ? "?" : "&") + "random=" + rand.Next()
                        : (url);

                    data = await client.DownloadString(url);

                    if (client.Error)
                    {
                        MessageBox.Show(client.ErrorMessage);
                    }
                    //if (client.HeaderExist("X-Cache") && client.ResponseHeaders["X-Cache"] != "HIT")
                    //{ var a = 1; }
                }

                return data;
            });
        }
    }
}