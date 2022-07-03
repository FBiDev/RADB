using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Net;
using System.Windows.Forms;

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
        public static Download dlGames = new Download { Overwrite = true, FolderBase = Folder.GameData, };
        public static Download dlGamesIcon = new Download() { Overwrite = false, FolderBase = Folder.IconsBase, };
        public static Download dlGameExtend = new Download { Overwrite = true, FolderBase = Folder.GameDataExtendBase, };
        public static Download dlGameExtendImages = new Download { Overwrite = false, FolderBase = Folder.Images, };

        public static void Load()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 128;
        }

        private static Random rand = new Random();
        public async static Task<string> DownloadString(string url, bool addRandomNumber = false)
        {
            return await Task<string>.Run(async () =>
            {
                string data = string.Empty;

                using (WebClientExtend client = new WebClientExtend())
                {
                    if (addRandomNumber)
                    {
                        if (url.IndexOf("?") < 0)
                        { url += "?"; }
                        else { url += "&"; }

                        url += "random=" + rand.Next();
                    }

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