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

namespace RADB
{
    public class RA
    {
        #region _Main
        //URLs
        public static string URL_Images = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Images/";
        public static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";

        private string API_URL = "http://retroachievements.org/API/";
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

        public static Size GamesIconSize { get { return new Size(96, 96); } }

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
        public string GamesFile(string consoleName) { return (Folder.Consoles + consoleName + " GameList.json").Replace("/", "-"); }

        public async Task DownloadGames(Download dlGames, Console console)
        {
            dlGames.File = new DownloadFile(GetURL("API_GetGameList.php", "i=" + console.ID), GamesFile(console.Name));
            await (dlGames.Start());

            await Game.Excluir(console.ID);
            await Game.IncluirLista(await GetGames(console));
        }

        public async Task DownloadGamesIcon(Download dlGameIcons, Console console)
        {
            List<Game> games = await Game.Listar(new Game() { ConsoleID = console.ID });
            dlGameIcons.Files = games.Select(g => g.ImageIconDownload()).ToList();
            await (dlGameIcons.Start());
        }

        public Task<List<Game>> GetGames(Console console)
        {
            return Task<List<Game>>.Run(() =>
            {
                return JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(GamesFile(console.Name)));
            });
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

        static bool FilesAreEqual_Hash(List<DownloadFile> list)
        {
            List<FileInfo> fList = new List<FileInfo>();
            bool equal = false;
            for (int f = 0; f < list.Count; f++)
            {
                FileInfo first = new FileInfo(list[f].Path);
                byte[] firstHash = MD5.Create().ComputeHash(first.OpenRead());

                for (int s = f + 1; s < list.Count; s++)
                {
                    FileInfo second = new FileInfo(list[s].Path);
                    byte[] secondHash = MD5.Create().ComputeHash(second.OpenRead());

                    equal = true;
                    for (int i = 0; i < firstHash.Length; i++)
                    {
                        if (firstHash[i] != secondHash[i])
                        {
                            equal = false;
                            break;
                            //return false;
                        }
                    }
                    if (equal)
                    {
                        fList.Add(second);
                    }
                    //return true;
                }
            }
            fList = fList.Distinct().ToList();
            return false;

            //byte[] firstHash = MD5.Create().ComputeHash(first.OpenRead());
            //byte[] secondHash = MD5.Create().ComputeHash(second.OpenRead());
        }

        static void QueryDuplicates(List<DownloadFile> list)
        {
            var a = list.Select(f =>
            {
                using (var fs = new FileStream(f.Path, FileMode.Open, FileAccess.Read))
                {
                    return new
                    {
                        FileName = f,
                        FileHash = BitConverter.ToString(SHA1.Create().ComputeHash(fs))
                    };
                }
            });
            // Change the root drive or folder if necessary  
            //string startFolder = @"c:\program files\Microsoft Visual Studio 9.0\";

            // Take a snapshot of the file system.  
            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            // This method assumes that the application has discovery permissions  
            // for all folders under the specified path.  
            IEnumerable<System.IO.FileInfo> fileList = list.Select(i => new FileInfo(i.Path));

            // used in WriteLine to keep the lines shorter  
            //int charsToSkip = startFolder.Length;

            // var can be used for convenience with groups.  
            var queryDupNames =
                from file in fileList
                group file.FullName by file.Name into fileGroup
                where fileGroup.Count() > 1
                select fileGroup;

            // Pass the query to a method that will  
            // output one page at a time.  
            //PageOutput<string, string>(queryDupNames);
        }

        //public static void DeleteDuplicateFiles(string searchPath)
        //{
        //    foreach (var file in FindDuplicateFiles(searchPath))
        //        TryDeleteFile(file);
        //}

        //private static IEnumerable<string> FindDuplicateFiles(List<DownloadFile> lst)
        //{
        //    var hashedFiles = new List<FileInfo>(lst.Select(f => new FileInfo(f.Path)))
        //        .AsParallel()
        //        .Select(x => new { Path = x, Hash = MD5.Create().ComputeHash(x.OpenRead()) });

        //    return FindDuplicates(hashedFiles);
        //}

        public void Find(List<DownloadFile> lst)
        {
            var hashedFiles = lst.Select(x => new { Path = x.Path, Hash = MD5.Create().ComputeHash(new FileInfo(x.Path).OpenRead()) });

            foreach (var item in hashedFiles)
            {
                var duplicates = hashedFiles.Where(i => i.Hash == item.Hash);
            }
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
            //List<Game> GamesWithCheevos = new List<Game>();

            //List<Achievement> gCheevos = new List<Achievement>();
            List<DownloadFile> gFiles = new List<DownloadFile>();

            int FilesDownloaded = 0;

            //Get ManyGames
            List<Game> Games = await Game.Listar(58);
            Games.ForEach(g => gFiles.Add(g.ImageIconDownload()));
            QueryDuplicates(gFiles);
            //foreach (Game g in Games)
            //{
            //Game game = await GetGameInfoExtended(g.ID);
            //if (game.AchievementsList.Count > 0)
            //{
            //    game.AchievementsList.ForEach(a => gFiles.Add(new DownloadFile(a.BadgeURL(), a.BadgeFile())));
            //}
            //}

            //GamesWithCheevos.ForEach(gc => gCheevos.AddRange(gc.AchievementsList));
            ////GamesWithCheevos.ForEach(gc => gFiles.AddRange(gc.AchievementsList.Select(a => new DownloadFile(a.BadgeURL(), a.BadgeFile())).ToList().Distinct().ToList()));

            //gFiles = gCheevos.Select(a => new DownloadFile(a.BadgeURL, a.BadgeFile)).ToList().Distinct().ToList();

            //var query = gCheevos.GroupBy(x => new { x.BadgeURL, x.GameID }).Where(g => g.Count() > 1)
            //  .Select(y => new { Element = y.Key, Counter = y.Count() })
            //  .ToList().OrderBy(x => x.Element.BadgeURL).ToList();

            //var queryTotal = gCheevos.Count - (query.Select(x => x.Counter).Sum() - query.Count);

            //foreach (Game game in GamesWithCheevos)
            //{

            //Game gameX = await GetGameInfoExtended(gameID);
            Download dlGameBadges = new Download()
            {
                Files = gFiles,
                //Files = gameX.AchievementsList.Select(a => new DownloadFile(a.BadgeURL(), a.BadgeFile())).ToList(),
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

            List<string> afiles = gFiles.Select(x => x.Path).ToList();
            Picture pic = new Picture(afiles, true, 11, GamesIconSize);
            pic.Save(Folder.Temp + "badges", PictureFormat.Png);

            //Picture pic = new Picture(gameX.AchievementsFiles());
            //pic.Save(Folder.Achievements(gameX.ConsoleID, gameX.ID) + "badges", PictureFormat.Png);

            return;
        }
    }
}
