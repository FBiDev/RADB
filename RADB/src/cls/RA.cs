using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GNX;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Collections;
using System.Windows.Forms;

namespace RADB
{
    public class RA
    {
        #region _Main
        //URLs
        public static string URL_Images = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Images/";
        public static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";

        public static Size GamesIconSize { get { return new Size(96, 96); } }

        public static Picture DefaultIconImage = new Picture(GamesIconSize);
        public static Picture ErrorIcon = new Picture(GamesIconSize);
        public static Picture DefaultTitleImage = new Picture(200, 150);
        public static Picture DefaultIngameImage = new Picture(200, 150);

        private string API_URL = "https://retroachievements.org/API/";
        private string API_UserName;
        private string API_Key;

        public RA()
        {
            API_UserName = "FBiDev";
            API_Key = "uBuG840fXTyKSQvS8MFKX5d40fOelJ29";
        }

        private string AuthQS()
        {
            return "?z=" + API_UserName + "&y=" + API_Key;
        }

        private string GetURL(string target, string parames = "")
        {
            return API_URL + target + AuthQS() + "&" + parames;
        }
        #endregion

        #region _Consoles
        public string ConsolesFile() { return Folder.Consoles + "Consoles.json"; }

        public async Task DownloadConsoles(Download dlConsoles)
        {
            dlConsoles.File = new DownloadFile(GetURL("API_GetConsoleIDs.php"), ConsolesFile());
            await dlConsoles.Start();

            await Console.Excluir();
            await Console.IncluirLista(await GetConsoles());
        }

        public Task<List<Console>> GetConsoles()
        {
            return Task<List<Console>>.Run(() =>
            {
                List<Console> consoles = JsonConvert.DeserializeObject<List<Console>>(File.ReadAllText(ConsolesFile()));
                return consoles.OrderBy(x => x.ID).ToList();
            });
        }
        #endregion

        #region _Games
        public string GamesFile(string consoleName)
        {
            return (Folder.Consoles + consoleName + " GameList.json").Replace("/", "-");
        }

        public async Task DownloadGames(Download dlGames, Console console)
        {
            dlGames.File = new DownloadFile(GetURL("API_GetGameList.php", "i=" + console.ID), GamesFile(console.Name));
            await (dlGames.Start());

            await Game.Excluir(console.ID);
            await Game.IncluirLista(await DeserializeGameList(console.Name));
        }

        private Task<List<Game>> DeserializeGameList(string consoleName)
        {
            return Task<List<Game>>.Run(() =>
            {
                List<Game> list = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(GamesFile(consoleName)));
                list.ForEach(g => { g.Icon = g.Icon.Replace(@"/Images/", ""); });
                return list;
            });
        }

        public string IconPath(Game g)
        {
            return Folder.Icons(g.ConsoleID) + g.Icon;
        }

        public void SetIcon(Game g)
        {
            if (g.IconBitmap.IsNull())
            {
                g.IconBitmap = Picture.Create(IconPath(g), ErrorIcon).Bitmap;
            }
        }

        private DownloadFile IconFile(Game g)
        {
            return new DownloadFile(URL_Images + g.Icon, IconPath(g));
        }

        public async Task DownloadGamesIcon(Download dlGameIcons, Console console)
        {
            List<Game> games = await Game.Listar(console.ID);
            dlGameIcons.Files = games.Select(g => IconFile(g)).ToList();
            await (dlGameIcons.Start());
        }

        public string ImageTitlePath(Game g)
        {
            return string.IsNullOrWhiteSpace(g.ImageTitle) ? string.Empty : Folder.ImageTitle(g.ConsoleID) + g.ImageTitle;
        }

        public void SetImageTitle(Game g)
        {
            if (g.ImageTitleBitmap.IsNull())
            {
                g.ImageTitleBitmap = Picture.Create(ImageTitlePath(g), ErrorIcon).Bitmap;
            }
        }
        #endregion

        public DownloadFile DownloadGameInfoExtended(Game game)
        {
            return new DownloadFile(GetURL("API_GetGameExtended.php", "i=" + game.ID), JSN_GameInfoExtend(game.ConsoleID, game.ID));
        }
        public string FileGameInfoExtended(int consoleID, int gameID)
        {
            return Folder.GameInfoExtendConsole(consoleID) + gameID + ".json";
        }

        public async Task<UserProgress> GetUserProgress(int gameID)
        {
            string userData = await Browser.DownloadString(GetURL("API_GetUserProgress.php", "u=" + API_UserName + "&i=" + gameID));
            userData = userData.GetBetween(":{", "}}");
            userData = "{" + userData + "}";

            UserProgress user = null;
            if (string.IsNullOrWhiteSpace(userData) == false)
            {
                user = JsonConvert.DeserializeObject<UserProgress>(userData);
            }
            return user;
        }

        public static string API_GameExtended = "API_GetGameExtended.php";

        //JSON
        public static string JSN_GameInfo(int consoleID, int gameID) { return Folder.GameInfoConsole(consoleID) + gameID + ".json"; }
        public static string JSN_GameInfoExtend(int consoleID, int gameID) { return Folder.GameInfoExtendConsole(consoleID) + gameID + ".json"; }

        public async Task<Game> GetGameInfoExtended(int gameID)
        {
            if (gameID <= 0) { return null; }

            string fileName = Folder.GameInfoExtend + 1 + @"\" + gameID + ".json";
            //JObject result = Browser.ToJObject(API_URL("API_GetGameExtended.php", "&i=", gameID.ToString()));
            Download dl = new Download()
            {
                Overwrite = false,
                Files = new List<DownloadFile>() { new DownloadFile(GetURL("API_GetGameExtended.php", "i=" + gameID.ToString()), fileName) },
                ProgressBarName = "pgbUpdates",
                LabelBytesName = "lblUpdateProgress",
                LabelTimeName = "lblUpdateConsoles",
            };
            await dl.Start();

            return await Task.Run(() =>
            {
                JObject result = Browser.ToJObject(fileName);

                Game game = result.ToObject<Game>();
                JToken a = result["Achievements"];
                game.SetAchievements(result["Achievements"]);
                return game;
            });
            //return new Game();
        }

        public async Task DownloadBadges(int gameID)
        {
            if (Main.ConsoleBind.IsNull()) { MessageBox.Show("No Console Selected"); return; }

            //int FilesDownloaded = 0;

            //Get ManyGames
            int cID = 58;
            if (Main.ConsoleBind.NotNull()) { cID = Main.ConsoleBind.ID; }
            List<Game> Games = await Game.Listar(cID);
            List<DownloadFile> gFiles = new List<DownloadFile>(Games.Where(a => a.NumAchievements > 0).Select(g => IconFile(g)));

            //var query = gCheevos.GroupBy(x => new { x.BadgeURL, x.GameID }).Where(g => g.Count() > 1)
            //  .Select(y => new { Element = y.Key, Counter = y.Count() })
            //  .ToList().OrderBy(x => x.Element.BadgeURL).ToList();

            //var queryTotal = gCheevos.Count - (query.Select(x => x.Counter).Sum() - query.Count);

            //Game gameX = await GetGameInfoExtended(gameID);
            //gFiles = gameX.AchievementsList.Select(a => new DownloadFile(a.BadgeURL(), a.BadgeFile())).ToList();
            //gFiles = new List<DownloadFile>() { new DownloadFile("https://dl18.cdromance.com/download.php?file=Megaman_Powered_Up_USA_PSP-DMU.7z&id=251&platform=psp&key=6299971769", "MM.7z") };
            Download dlGameBadges = new Download()
            {
                Files = gFiles,
                Overwrite = false,
                ProgressBarName = "pgbUpdates",
                LabelBytesName = "lblUpdateProgress",
                LabelTimeName = "lblUpdateConsoles",
            };
            await dlGameBadges.Start();

            int FilesDownloaded = dlGameBadges.FilesCompleted;

            List<string> afiles = Archive.RemoveDuplicates(gFiles.Select(f => f.Path).ToList());
            if (afiles.Count > 0)
            {
                Picture pic = new Picture(afiles, true, 11, GamesIconSize, true);
                pic.Save(Game.IconsMerged(Main.ConsoleBind.Name), PictureFormat.Png, true);
            }

            return;
        }
    }
}
