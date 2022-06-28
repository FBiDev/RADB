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
using RADB.Properties;

namespace RADB
{
    public class RA
    {
        #region _Main
        //URLs
        public static string URL_Images = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Images/";
        public static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";

        private static Size GameIconSize { get { return new Size(96, 96); } }

        public static Bitmap DefaultIcon = new Picture(GameIconSize).Bitmap;
        public static Bitmap ErrorIcon = Resources.notfound;

        public static Bitmap DefaultTitleImage = new Picture(200, 150).Bitmap;
        public static Bitmap DefaultIngameImage = new Picture(200, 150).Bitmap;
        public static Bitmap DefaultBoxArtImage = new Picture(200, 150).Bitmap;

        private string API_URL = "https://retroachievements.org/API/";
        private string API_UserName;
        private string API_Key;

        public RA()
        {
            API_UserName = "RADatabase";
            API_Key = "GRaWk9onm4B0LSWSFaDt5a2dQE3N8Yme";
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
        public string ConsolesPath()
        {
            return Folder.Console + "Consoles.json";
        }

        public async Task DownloadConsoles(Download dlConsoles)
        {
            dlConsoles.Files = new List<DownloadFile>() { new DownloadFile(GetURL("API_GetConsoleIDs.php"), ConsolesPath()) };
            await dlConsoles.Start();

            await Console.Excluir();
            await Console.IncluirLista(await DeserializeConsoles());
        }

        private Task<List<Console>> DeserializeConsoles()
        {
            return Task<List<Console>>.Run(() =>
            {
                List<Console> consoles = JsonConvert.DeserializeObject<List<Console>>(File.ReadAllText(ConsolesPath()));
                return consoles.OrderBy(x => x.ID).ToList();
            });
        }
        #endregion

        #region _GameList
        public string GameListPath(string consoleName)
        {
            return (Folder.GameData + consoleName + ".json").Replace("/", "-");
        }

        public async Task DownloadGameList(Download dlGames, Console console)
        {
            await Task.Run(async () =>
            {
                dlGames.Files = new List<DownloadFile>() { new DownloadFile(GetURL("API_GetGameList.php", "i=" + console.ID), GameListPath(console.Name)) };
                await dlGames.Start();

                await Game.Excluir(console.ID);
                List<Game> list = await DeserializeGameList(console.Name);
                await Game.IncluirLista(list);
            });
        }

        private Task<List<Game>> DeserializeGameList(string consoleName)
        {
            return Task<List<Game>>.Run(() =>
            {
                List<Game> list = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(GameListPath(consoleName)));
                return list;
            });
        }

        public async Task DownloadGamesIcon(Download dlGameIcons, Console console)
        {
            await Task.Run(async () =>
            {
                List<Game> games = await Game.Listar(console.ID);
                dlGameIcons.Files = games.Select(g => g.ImageIconFile).ToList();
                await (dlGameIcons.Start());
            });
        }
        #endregion

        #region _GameExtend
        public string GameExtendPath(Game game)
        {
            return (Folder.GameDataExtend(game.ConsoleID) + game.ID + ".json");
        }

        public async Task DownloadGameExtend(Download dlGameExtend, Game game)
        {
            dlGameExtend.Files = new List<DownloadFile>() { new DownloadFile(GetURL("API_GetGameExtended.php", "i=" + game.ID), GameExtendPath(game)) };
            await (dlGameExtend.Start());

            GameExtend obj = (await DeserializeGameExtend(game));
            obj.ID = game.ID;
            obj.ConsoleID = game.ConsoleID;
            await obj.Excluir();
            obj.Incluir();
        }

        private Task<GameExtend> DeserializeGameExtend(Game game)
        {
            return Task<GameExtend>.Run(() =>
            {
                string AllText = File.ReadAllText(GameExtendPath(game));
                string gameData = AllText.GetBetween("{", ",\"Achievements\":");
                string cheevos = AllText.GetBetween("\"Achievements\":{", "}}");
                gameData = "{" + gameData + "}";
                cheevos = "{" + cheevos + "}";

                GameExtend obj = JsonConvert.DeserializeObject<GameExtend>(gameData);

                JToken jcheevos = JsonConvert.DeserializeObject<JToken>(cheevos);

                obj.SetAchievements(jcheevos);

                return obj;
            });
        }

        public async Task DownloadGameExtendImages(Download dlGameExtendImages, Game game)
        {
            GameExtend gamex = await GameExtend.Listar(game.ID);
            dlGameExtendImages.Files = new List<DownloadFile>() {
                gamex.ImageTitleFile,
                gamex.ImageIngameFile,
                gamex.ImageBoxArtFile,
            };
            await (dlGameExtendImages.Start());
        }
        #endregion

        public DownloadFile DownloadGameInfoExtended(Game game)
        {
            return new DownloadFile(GetURL("API_GetGameExtended.php", "i=" + game.ID), JSN_GameInfoExtend(game.ConsoleID, game.ID));
        }
        public string FileGameInfoExtended(int consoleID, int gameID)
        {
            return Folder.GameDataExtend(consoleID) + gameID + ".json";
        }

        public async Task<UserProgress> GetUserProgress(string username, int gameID)
        {
            string userData = await Browser.DownloadString(GetURL("API_GetUserProgress.php", "u=" + username + "&i=" + gameID));
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
        public static string JSN_GameInfoExtend(int consoleID, int gameID) { return Folder.GameDataExtend(consoleID) + gameID + ".json"; }

        public async Task<GameExtend> GetGameInfoExtended(int gameID)
        {
            if (gameID <= 0) { return null; }

            string fileName = Folder.GameDataExtend(1) + gameID + ".json";
            //JObject result = Browser.ToJObject(API_URL("API_GetGameExtended.php", "&i=", gameID.ToString()));
            Download dl = new Download()
            {
                Overwrite = false,
                Files = new List<DownloadFile>() { new DownloadFile(GetURL("API_GetGameExtended.php", "i=" + gameID.ToString()), fileName) },
                //ProgressBarName = "pgbUpdates",
                //LabelBytesName = "lblUpdateProgress",
                //LabelTimeName = "lblUpdateConsoles",
            };
            await dl.Start();

            return await Task.Run(() =>
            {
                JObject result = Browser.ToJObject(fileName);

                GameExtend game = result.ToObject<GameExtend>();
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
            List<DownloadFile> gFiles = new List<DownloadFile>(Games.Where(a => a.NumAchievements > 0).Select(g => g.ImageIconFile));

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
                //ProgressBarName = "pgbUpdates",
                //LabelBytesName = "lblUpdateProgress",
                //LabelTimeName = "lblUpdateConsoles",
            };
            await dlGameBadges.Start();

            int FilesDownloaded = dlGameBadges.FilesCompleted;

            List<string> afiles = Archive.RemoveDuplicates(gFiles.Select(f => f.Path).ToList());
            if (afiles.Count > 0)
            {
                Picture pic = new Picture(afiles, true, 11, GameIconSize, false);
                pic.Save(Game.IconsMerged(Main.ConsoleBind.Name), PictureFormat.Png, false);
            }

            return;
        }
    }
}
