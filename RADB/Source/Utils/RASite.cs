using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public static class RASite
    {
        private static string userToken;

        public static WebClientExtend Client { get; set; }

        // Downloads
        public static Download DLConsoles { get; private set; }

        public static Download DLConsolesGamesIcon { get; private set; }

        public static Download DLGames { get; private set; }

        public static Download DLGamesIcon { get; private set; }

        public static Download DLGamesBadges { get; private set; }

        public static Download DLGameExtend { get; private set; }

        public static Download DLGameExtendImages { get; private set; }

        public static Download DLGameExtendList { get; private set; }

        public static void Load()
        {
            Browser.UseProxy = Environment.MachineName.Equals("cohab-ct0157", StringComparison.InvariantCultureIgnoreCase);
            Browser.DefaultProxy = new WebProxy
            {
                Address = new Uri("http://cohab-proxy.cohabct.com.br:3128"),
                BypassProxyOnLocal = true,
                BypassList = new string[] { },
                Credentials = new NetworkCredential("fbirnfeld", "zumbie")
            };

            Client = new WebClientExtend();

            DLConsoles = new Download { Overwrite = true, FolderBase = Folder.Console };
            DLConsolesGamesIcon = new Download { Overwrite = false, FolderBase = Folder.IconsBase };

            DLGames = new Download { Overwrite = true, FolderBase = Folder.GameData };
            DLGamesIcon = new Download { Overwrite = false, FolderBase = Folder.IconsBase };
            DLGamesBadges = new Download { Overwrite = false, FolderBase = Folder.BadgesBase };

            DLGameExtend = new Download { Overwrite = true, FolderBase = Folder.GameDataExtendBase };
            DLGameExtendImages = new Download { Overwrite = false, FolderBase = Folder.Images };
            DLGameExtendList = new Download { Overwrite = false, FolderBase = Folder.GameDataExtendBase };
        }

        public static async Task Login()
        {
            Session.RALogged = false;

            var html = await Client.DownloadString(RA.SiteLogin);
            if (string.IsNullOrWhiteSpace(html))
            {
                ShowError(Client);
                return;
            }

            userToken = html.GetBetween("_token\" value=\"", "\"");

            var values = new NameValueCollection
            {
                { "_token", userToken },
                { "User", "RADatabase" },
                { "password", "RADatabase123" }
            };

            var html2 = await Client.UploadValuesTaskAsync(new Uri(RA.SiteLogin), values);
            Session.RALogged = CheckLogin(html2);
        }

        public static async Task Logout()
        {
            var values = new NameValueCollection
            {
                { "_token", userToken }
            };

            var html = await Client.UploadValuesTaskAsync(new Uri(RA.SiteLogout), values);
            Session.RALogged = CheckLogin(html);
        }

        public static bool CheckLogin(string htmlLogin)
        {
            if (string.IsNullOrWhiteSpace(htmlLogin))
            {
                ShowError(Client);
                return false;
            }

            var login = htmlLogin.GetBetween("<form action=\"https://retroachievements.org/", "\"");

            return login != "login";
        }

        private static void ShowError(WebClientExtend client)
        {
            if (client.Error)
            {
                MessageBox.Show(client.ErrorMessage);
            }
        }
    }
}