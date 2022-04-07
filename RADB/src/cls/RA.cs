using System;
using System.Collections.Generic;
using System.Linq;
//
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//
using System.IO;
using System.Windows.Forms;

namespace RADB
{
    public static class RA
    {
        //Folders
        private static string FolderBase { get { return @"data\"; } }
        public static string FolderJson = FolderBase + @"json\";

        //URLs
        private static string API_URL = "http://retroachievements.org/API/";
        private static string AuthQS = "?z=FBiDev&y=uBuG840fXTyKSQvS8MFKX5d40fOelJ29";
        public static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";
        public static string URL_Images = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Images/";

        //API
        public static string API_ConsoleIDs = "API_GetConsoleIDs.php";
        public static string API_GameList = "API_GetGameList.php";
        public static string API_GameExtended = "API_GetGameExtended.php";


        //JSON
        public static string JSN_ConsoleIDs = Folder.Consoles + "Consoles.json";
        public static string JSN_GameList(string consoleName) { return Folder.Consoles + consoleName + " GameList.json"; }
        public static string JSN_GameInfoExtend(int consoleID, int gameID) { return Folder.GameInfoExtendConsole(consoleID) + gameID + ".json"; }

        //Images
        public static string Format_Badges = ".png";
        public static string Format_BadgesLocal = ".png";

        public static string GetRAURL(string target, string parames = "")
        {
            return API_URL + target + AuthQS + "&" + parames;
        }

        public async static Task<Game> GetGameInfoExtended(int gameID)
        {
            if (gameID <= 0) { return null; }

            string fileName = Folder.GameInfoExtend + gameID + ".json";
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

        public async static Task DownloadBadges(int gameID)
        {
            string fileGameList = Folder.Json + "GameList" + "12" + ".json";
            Download dl = new Download()
            {
                Overwrite = true,
                Files = new List<DownloadFile>() { new DownloadFile(GetRAURL("API_GetGameList.php", "i=" + 12.ToString()), fileGameList) },
                ProgressBarName = "pgbUpdates",
                LabelBytesName = "lblUpdateProgress",
                LabelTimeName = "lblUpdateConsoles",
            };
            await dl.Start();
            return;

            List<Game> Games = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(fileGameList));
            List<Game> GamesWithCheevos = new List<Game>();

            List<Achievement> gCheevos = new List<Achievement>();
            List<DownloadFile> gFiles = new List<DownloadFile>();

            int FilesDownloaded = 0;
            foreach (Game g in Games)
            {
                Game game = await GetGameInfoExtended(g.ID);
                if (game.AchievementsList.Count > 0)
                {
                    GamesWithCheevos.Add(game);
                }
            }

            //GamesWithCheevos.ForEach(gc => gCheevos.AddRange(gc.AchievementsList));
            GamesWithCheevos.ForEach(gc => gFiles.AddRange(gc.AchievementsList.Select(a => new DownloadFile(a.BadgeURL(), a.BadgeFile())).ToList().Distinct().ToList()));

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
                Files = gFiles,
                //Files = gameX.AchievementsList.Select(a => new DownloadFile(a.BadgeURL, a.BadgeFile)).ToList(),
                //Files = new List<DownloadFile>() { new DownloadFile("https://dl18.cdromance.com/download.php?file=Megaman_Powered_Up_USA_PSP-DMU.7z&id=251&platform=psp&key=6299971769", "MM.7z") },
                Overwrite = false,
                ProgressBarName = "pgbUpdates",
                LabelBytesName = "lblUpdateProgress",
                LabelTimeName = "lblUpdateConsoles",
            };

            await dlGameBadges.Start();

            FilesDownloaded += dlGameBadges.FilesCompleted;
            var L = (Application.OpenForms[0].Controls.Find("lblUpdateConsoles", true).First() as Label);
            L.Text = FilesDownloaded.ToString();
            //}

            List<string> afiles = gFiles.Select(x => x.Path).ToList();
            Picture pic = new Picture(afiles, true, 110);
            //Picture pic = new Picture(gameX.AchievementsFiles());
            pic.Save(Folder.GameInfoExtend + "badges", PictureFormat.Jpg);

            return;
        }

        public static void DownloadFile(string url, string filePath)
        {
            //Download and Save file
            byte[] data = Browser.DownloadData(url);
            File.WriteAllBytes(filePath, data);
        }

        public static List<T> FileToList<T>(string filePath)
        {
            //Read and Convert File
            string text = File.ReadAllText(filePath);
            List<T> objList = JsonConvert.DeserializeObject<List<T>>(text);
            return objList;
        }

        public static void UpdateFile<T>(T list, string path)
        {
            //Serialize List and Save Json File
            string jsonData = JsonConvert.SerializeObject(list);
            File.WriteAllText(path, jsonData);
        }

        public static FileUpdate FindFileName(List<FileUpdate> list, string name)
        {
            return list.Find(o => o.Name == name);
        }
    }
}
