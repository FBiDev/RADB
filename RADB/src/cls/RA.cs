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

namespace RADB
{
    public class RA
    {
        private const string API_URL = "http://retroachievements.org/API/";
        public string user;
        public string api_key;

        public RA()
        {
            user = "FBiDev";
            api_key = "uBuG840fXTyKSQvS8MFKX5d40fOelJ29";
        }

        private string AuthQS()
        {
            return "?z=" + user + "&y=" + api_key;
        }

        public string GetRAURL(string target, string parames = "")
        {
            return API_URL + target + AuthQS() + "&" + parames;
        }

        public DownloadFile DownloadConsoles()
        {
            return new DownloadFile(GetRAURL("API_GetConsoleIDs.php"), FileConsoles());
        }
        public string FileConsoles()
        {
            return Folder.Consoles + "Consoles.json";
        }
        public Task<ListBind<Console>> ListConsoles()
        {
            return Task<ListBind<Console>>.Run(() =>
            {
                List<Console> consoles = JsonConvert.DeserializeObject<List<Console>>(File.ReadAllText(FileConsoles()));
                return new ListBind<Console>(consoles.OrderBy(x => x.Name).ToList());
            });
        }

        public DownloadFile DownloadGameList(Console console)
        {
            return new DownloadFile(GetRAURL("API_GetGameList.php", "i=" + console.ID), FileGameList(console.Name));
        }
        public string FileGameList(string consoleName)
        {
            consoleName = consoleName.Replace("/", "-");
            return Folder.Consoles + consoleName + " GameList.json";
        }
        public Task<ListBind<Game>> ListGameList(Console console)
        {
            return Task<ListBind<Game>>.Run(() =>
            {
                List<Game> GameList = new List<Game>();
                //TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
                GameList = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(FileGameList(console.Name)));
                //TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
                //return new ListBind<Game>(GameList);
                //List<Game> LCheevos = new List<Game>();
                //List<Game> LNotOffical = new List<Game>();
                //List<Game> LNoCheevos = new List<Game>();
                //List<Game> LNotOfficalNoCheevos = new List<Game>();

                //TimeSpan ini = new TimeSpan(DateTime.Now.Ticks);
                foreach (Game game in GameList)
                {
                    //string infoFile = RA.JSN_GameInfo(game.ConsoleID, game.ID);

                    //if (File.Exists(infoFile) == false) continue;

                    //JObject resultInfo = Browser.ToJObject(infoFile);
                    //Game gameInfo = resultInfo.ToObject<Game>();

                    //gameInfo.SetAchievements(resultInfo["Achievements"]);
                    //game.AchievementsList = gameInfo.AchievementsList;

                    //game.Developer = gameInfo.Developer;
                    //game.Publisher = gameInfo.Publisher;
                    //game.Genre = gameInfo.Genre;
                    //game.Released = gameInfo.Released;

                    //game.ImageTitle = gameInfo.ImageTitle;
                    //game.ImageIngame = gameInfo.ImageIngame;
                }

                List<string> prefixNotOffical = new List<string> { 
                        "~Demo~", "~Hack~", "~Homebrew~", "~Prototype~", "~Test Kit~", "~Unlicensed~", "~Z~" };

                //Get NotOffical
                List<Game> LNotOffical = GameList.Where(x => prefixNotOffical.Any(y => x.Title.IndexOf(y) >= 0)).ToList();
                //Remove NotOffical from Main List
                GameList = GameList.Except(LNotOffical).ToList();
                //Get Game with no cheevos from NotOffical
                List<Game> LNotOfficalNoCheevos = LNotOffical.Where(x => x.NumAchievements == 0).ToList();
                //Get Games Has Cheevos
                LNotOffical = LNotOffical.Where(x => x.NumAchievements > 0).ToList();
                //Get Game with no cheevos from Main List
                List<Game> LNoCheevos = GameList.Where(x => x.NumAchievements == 0).ToList();
                //Remove Games no Cheevos from Main List
                GameList = GameList.Except(LNoCheevos).ToList();

                //TimeSpan fim = new TimeSpan(DateTime.Now.Ticks) - ini;
                //Join Ordered Lists
                GameList = GameList.OrderBy(x => x.Title).ToList();
                GameList.AddRange(LNotOffical.OrderBy(x => x.Title).ToList());
                GameList.AddRange(LNoCheevos.OrderBy(x => x.Title).ToList());
                GameList.AddRange(LNotOfficalNoCheevos.OrderBy(x => x.Title).ToList());

                return new ListBind<Game>(GameList);
            });
        }

        public DownloadFile DownloadGameInfoExtended(Game game)
        {
            return new DownloadFile(GetRAURL("API_GetGameExtended.php", "i=" + game.ID), JSN_GameInfoExtend(game.ConsoleID, game.ID));
        }
        public string FileGameInfoExtended(int consoleID, int gameID)
        {
            return Folder.GameInfoExtendConsole(consoleID) + gameID + ".json";
        }

        //URLs
        public static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";
        public static string URL_Images = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Images/";

        public static string API_GameExtended = "API_GetGameExtended.php";

        //JSON
        public static string JSN_GameInfo(int consoleID, int gameID) { return Folder.GameInfoConsole(consoleID) + gameID + ".json"; }
        public static string JSN_GameInfoExtend(int consoleID, int gameID) { return Folder.GameInfoExtendConsole(consoleID) + gameID + ".json"; }

        public Game UserProgress(int gameID)
        {
            Game obj = new Game();
            JObject result = new JObject();
            using (WebClient wc = new WebClient() { Proxy = Browser.Proxy })
            {
                string x = wc.DownloadString(GetRAURL("API_GetUserProgress.php", "u=" + user + "&i=" + gameID));
                result = JsonConvert.DeserializeObject<JObject>(x);
                obj = JsonConvert.DeserializeObject<Game>(result[gameID.ToString()].ToString());
            }

            return obj;
        }

        public async Task<Game> GetGameInfoExtended(int gameID)
        {
            if (gameID <= 0) { return null; }

            string fileName = Folder.GameInfoExtend + 1 + @"\" + gameID + ".json";
            //JObject result = Browser.ToJObject(API_URL("API_GetGameExtended.php", "&i=", gameID.ToString()));
            Download dl = new Download()
            {
                Overwrite = false,
                Files = new List<DownloadFile>() { new DownloadFile(GetRAURL("API_GetGameExtended.php", "i=" + gameID.ToString()), fileName) },
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
            ////string fileGameList = Folder.Json + "GameList" + "12" + ".json";
            ////Download dl = new Download()
            ////{
            ////    Overwrite = true,
            ////    Files = new List<DownloadFile>() { new DownloadFile(GetRAURL("API_GetGameList.php", "i=" + 12.ToString()), fileGameList) },
            ////    ProgressBarName = "pgbUpdates",
            ////    LabelBytesName = "lblUpdateProgress",
            ////    LabelTimeName = "lblUpdateConsoles",
            ////};
            ////await dl.Start();
            ////return;

            ////List<Game> Games = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(fileGameList));
            ////List<Game> GamesWithCheevos = new List<Game>();

            ////List<Achievement> gCheevos = new List<Achievement>();
            List<DownloadFile> gFiles = new List<DownloadFile>();

            int FilesDownloaded = 0;
            ////foreach (Game g in Games)
            ////{
            ////    Game game = await GetGameInfoExtended(g.ID);
            ////    if (game.AchievementsList.Count > 0)
            ////    {
            ////        GamesWithCheevos.Add(game);
            ////    }
            ////}

            //GamesWithCheevos.ForEach(gc => gCheevos.AddRange(gc.AchievementsList));
            ////GamesWithCheevos.ForEach(gc => gFiles.AddRange(gc.AchievementsList.Select(a => new DownloadFile(a.BadgeURL(), a.BadgeFile())).ToList().Distinct().ToList()));

            //gFiles = gCheevos.Select(a => new DownloadFile(a.BadgeURL, a.BadgeFile)).ToList().Distinct().ToList();

            //var query = gCheevos.GroupBy(x => new { x.BadgeURL, x.GameID }).Where(g => g.Count() > 1)
            //  .Select(y => new { Element = y.Key, Counter = y.Count() })
            //  .ToList().OrderBy(x => x.Element.BadgeURL).ToList();

            //var queryTotal = gCheevos.Count - (query.Select(x => x.Counter).Sum() - query.Count);

            //foreach (Game game in GamesWithCheevos)
            //{
            Game gameX = await GetGameInfoExtended(gameID);
            Download dlGameBadges = new Download()
            {
                ////Files = gFiles,
                Files = gameX.AchievementsList.Select(a => new DownloadFile(a.BadgeURL(), a.BadgeFile())).ToList(),
                //Files = new List<DownloadFile>() { new DownloadFile("https://dl18.cdromance.com/download.php?file=Megaman_Powered_Up_USA_PSP-DMU.7z&id=251&platform=psp&key=6299971769", "MM.7z") },
                Overwrite = false,
                ProgressBarName = "pgbUpdates",
                LabelBytesName = "lblUpdateProgress",
                LabelTimeName = "lblUpdateConsoles",
            };

            await dlGameBadges.Start();

            FilesDownloaded += dlGameBadges.FilesCompleted;
            //var L = (Application.OpenForms[0].Controls.Find("lblUpdateConsoles", true).First() as Label);
            //L.Text = FilesDownloaded.ToString();
            //}

            //List<string> afiles = gFiles.Select(x => x.Path).ToList();
            //Picture pic = new Picture(afiles, true, 110);
            Picture pic = new Picture(gameX.AchievementsFiles());
            pic.Save(Folder.Achievements(gameX.ConsoleID, gameX.ID) + "badges", PictureFormat.Jpg);

            return;
        }
    }
}
