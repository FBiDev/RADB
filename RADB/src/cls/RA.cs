using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RADB.Properties;
using GNX;
using System.Diagnostics;
using System.Security.Cryptography;

namespace RADB
{
    public class RA
    {
        #region _Main
        private string API_URL = "https://retroachievements.org/API/";
        private string API_UserName;
        private string API_Key;

        //URLs
        public static string URL_Images = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Images/";
        public static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";

        private static Size GameIconSize { get { return new Size(96, 96); } }

        public static Bitmap DefaultIcon = new Picture(GameIconSize).Bitmap;
        public static Bitmap ErrorIcon = Resources.notfound;

        public static Bitmap DefaultTitleImage = new Picture(200, 150).Bitmap;
        public static Bitmap DefaultIngameImage = new Picture(200, 150).Bitmap;
        public static Bitmap DefaultBoxArtImage = new Picture(200, 150).Bitmap;

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

        public async Task DownloadConsoles()
        {
            Browser.dlConsoles.Files = new List<DownloadFile>() { new DownloadFile(GetURL("API_GetConsoleIDs.php"), ConsolesPath()) };
            await Browser.dlConsoles.Start();

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

        public async Task DownloadGameList(Console console)
        {
            await Task.Run(async () =>
            {
                Browser.dlGames.Files = new List<DownloadFile>() { new DownloadFile(GetURL("API_GetGameList.php", "i=" + console.ID), GameListPath(console.Name)) };
                await Browser.dlGames.Start();

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

        public async Task DownloadGamesIcon(Console console)
        {
            await Task.Run(async () =>
            {
                List<Game> games = await Game.Listar(console.ID);
                Browser.dlGamesIcon.Files = games.Select(g => g.ImageIconFile).ToList();
                await (Browser.dlGamesIcon.Start());
            });
        }
        #endregion

        #region _GameExtend
        public string GameExtendPath(Game game)
        {
            return (Folder.GameDataExtend(game.ConsoleID) + game.ID + ".json");
        }

        public async Task DownloadGameExtend(Game game)
        {
            await Task.Run(async () =>
            {
                Browser.dlGameExtend.Files = new List<DownloadFile>() { new DownloadFile(GetURL("API_GetGameExtended.php", "i=" + game.ID), GameExtendPath(game)) };
                await (Browser.dlGameExtend.Start());

                GameExtend obj = (await DeserializeGameExtend(game));
                obj.ID = game.ID;
                obj.ConsoleID = game.ConsoleID;
                await obj.Excluir();
                obj.Incluir();
            });
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

        public async Task DownloadGameExtendImages(Game game)
        {
            await Task.Run(async () =>
            {
                GameExtend gamex = await GameExtend.Listar(game.ID);
                Browser.dlGameExtendImages.Files = new List<DownloadFile>() {
                    gamex.ImageTitleFile,
                    gamex.ImageIngameFile,
                    gamex.ImageBoxArtFile,
                };
                await (Browser.dlGameExtendImages.Start());
            });
        }
        #endregion

        #region _UserInfo
        public async Task<UserProgress> GetUserProgress(string username, int gameID)
        {
            return await Task.Run(async () =>
            {
                string userData = await Browser.DownloadString(GetURL("API_GetUserProgress.php", "u=" + username + "&i=" + gameID), true);
                userData = userData.GetBetween(":{", "}}");
                userData = "{" + userData + "}";

                UserProgress user = null;
                if (string.IsNullOrWhiteSpace(userData) == false)
                {
                    user = JsonConvert.DeserializeObject<UserProgress>(userData);
                    user.UserName = username;
                    user.GameID = gameID;
                }
                return user;
            });
        }
        #endregion

        public async Task DownloadBadges(int gameID)
        {
            if (Main.ConsoleBind.IsNull()) { MessageBox.Show("No Console Selected"); return; }
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);

            //int FilesDownloaded = 0;

            //Get ManyGames
            int cID = 58;
            if (Main.ConsoleBind.NotNull() && Main.ConsoleBind.ID > 0) { cID = Main.ConsoleBind.ID; }

            //List<Game> Games = (await Game.Listar(cID)).Where(a => a.NumAchievements > 0).ToList();


            List<Game> gs = (await Game.Listar(cID)).Where(a =>
            {
                //return true;
                //return a.NumAchievements > 0;
                return a.NumAchievements > 0
                    && new Picture(a.ImageIconFile.Path).Bitmap.Size != GameIconSize;

            }).ToList();
            //IEnumerable<DownloadFile> gFiles = gs.Select(g => g.ImageIconFile);
            await DownloadGamesIcon(Main.ConsoleBind);
            //int FilesDownloaded = Browser.dlGamesIcon.FilesCompleted;

            //var aFiles = gs.Select(g => g.ImageIconFile.Path).ToList();
            var aFiles = Archive.RemoveDuplicates(gs.Select(g => g.ImageIconFile.Path)).ToList();





            //var query = gCheevos.GroupBy(x => new { x.BadgeURL, x.GameID }).Where(g => g.Count() > 1)
            //  .Select(y => new { Element = y.Key, Counter = y.Count() })
            //  .ToList().OrderBy(x => x.Element.BadgeURL).ToList();

            //var queryTotal = gCheevos.Count - (query.Select(x => x.Counter).Sum() - query.Count);

            //Game gameX = await GetGameInfoExtended(gameID);
            //gFiles = gameX.AchievementsList.Select(a => new DownloadFile(a.BadgeURL(), a.BadgeFile())).ToList();
            //gFiles = new List<DownloadFile>() { new DownloadFile("https://dl18.cdromance.com/download.php?file=Megaman_Powered_Up_USA_PSP-DMU.7z&id=251&platform=psp&key=6299971769", "MM.7z") };
            //Browser.dlGamesIcon = new Download()
            //{
            //    Files = gFiles,
            //    Overwrite = false,
            //};
            //await dlGamesIcon.Start();




            //List<string> afiles = Archive.RemoveDuplicates(gFiles.Select(f => f.Path).ToList());
            //afiles = Archive.RemoveImageSize(afiles, GameIconSize);

            if (gs.Any())
            {
                Picture pic = new Picture(aFiles, true, 11, GameIconSize, false);
                pic.Save(Game.IconsMerged(Main.ConsoleBind.Name), PictureFormat.Png, false);

                Archive.SaveListToFile(gs, aFiles, Main.ConsoleBind.Name + "_IDs.txt");
                Process.Start(@"" + pic.Path);
            }
            else
            {
                MessageBox.Show("No Icons Found");
            }

            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
            MessageBox.Show(fim0.TotalSeconds + "s");
            return;
        }
    }
}
