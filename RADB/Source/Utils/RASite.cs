using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public static class RASite
    {
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
        }

        public static WebClientExtend Client;
        static string UserToken;

        public static async Task Login()
        {
            Session.RALogged = false;

            var html = await Client.DownloadString(RA.LOGIN_URL);
            if (string.IsNullOrWhiteSpace(html)) { ShowError(Client); return; }

            UserToken = html.GetBetween("_token\" value=\"", "\"");

            var values = new NameValueCollection
            {
                {"_token", UserToken},
                { "User", "RADatabase" },
                { "password", "RADatabase123" }
            };

            var html2 = await Client.UploadValuesTaskAsync(new Uri(RA.LOGIN_URL), values);
            Session.RALogged = CheckLogin(html2);
        }

        public static async Task Logout()
        {
            var values = new NameValueCollection
            {
                {"_token", UserToken}
            };

            var html = await Client.UploadValuesTaskAsync(new Uri(RA.LOGOUT_URL), values);
            Session.RALogged = CheckLogin(html);
        }

        public static bool CheckLogin(string htmlLogin)
        {
            if (string.IsNullOrWhiteSpace(htmlLogin)) { ShowError(Client); return false; }

            var login = htmlLogin.GetBetween("<form action=\"https://retroachievements.org/", "\"");

            return login != "login";
        }

        static void ShowError(WebClientExtend client)
        {
            if (client.Error)
            {
                MessageBox.Show(client.ErrorMessage);
            }
        }

        //===Downloads
        public static Download dlConsoles = new Download { Overwrite = true, FolderBase = Folder.Console };
        public static Download dlConsolesGamesIcon = new Download { Overwrite = false, FolderBase = Folder.IconsBase };

        public static Download dlGames = new Download { Overwrite = true, FolderBase = Folder.GameData };
        public static Download dlGamesIcon = new Download { Overwrite = false, FolderBase = Folder.IconsBase };
        public static Download dlGamesBadges = new Download { Overwrite = false, FolderBase = Folder.BadgesBase };

        public static Download dlGameExtend = new Download { Overwrite = true, FolderBase = Folder.GameDataExtendBase };
        public static Download dlGameExtendImages = new Download { Overwrite = false, FolderBase = Folder.Images };
        public static Download dlGameExtendList = new Download { Overwrite = false, FolderBase = Folder.GameDataExtendBase };
    }
}