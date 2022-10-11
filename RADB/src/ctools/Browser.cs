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

        public static void Load()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 128;

            //var html = await DownloadString(RA.HOST);

            //await LoginTest();
        }

        public static async Task LoginTest()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = Proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false,
            };
            HttpClient client = new HttpClient(httpClientHandler);

            string url = RA.HOST;
            string token = "";
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                using (HttpContent content = response.Content)
                {
                    var json = await content.ReadAsStringAsync();
                    token = json.GetBetween("_token\" value=\"", "\">", false, false);
                }
            }

            var postParams = new Dictionary<string, string>();
            postParams.Add("_token", token);
            postParams.Add("u", "");
            postParams.Add("p", "");
            postParams.Add("submit", "Login");

            using (var postContent = new FormUrlEncodedContent(postParams))
            using (HttpResponseMessage response = await client.PostAsync("https://retroachievements.org/request/auth/login.php", postContent))
            {
                response.EnsureSuccessStatusCode(); // Throw if httpcode is an error
                //using (HttpContent content = response.Content)
                //{
                //    string result = await content.ReadAsStringAsync();
                //}
            }

            using (HttpResponseMessage response = await client.GetAsync(RA.HOST + "linkedhashes.php?g=1"))
            {
                using (HttpContent content = response.Content)
                {
                    var json = await content.ReadAsStringAsync();
                    var li = json.GetBetween("unique hashes registered for it:<br><br><ul>", "</ul>", false, false);
                    var game = li.GetBetween("<p class='embedded'><b>", "</b>", false, false);
                    var hash = li.GetBetween("<code>", "</code>", false, false);
                }
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

                    //client.Credentials = new NetworkCredential("", "");
                    //var values = new NameValueCollection
                    //{
                    //    { "u", "" },
                    //    { "p", "" }
                    //};
                    //client.UploadValues(new Uri("URL"), values);
                    //var aa = await client.DownloadString("URL");

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