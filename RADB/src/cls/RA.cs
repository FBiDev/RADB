using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
//
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Collections;

namespace RADB
{
    public static class RA
    {
        //Folders
        private static string FolderBase { get { return @"data\"; } }
        public static string FolderJson = FolderBase + @"json\";

        //URLs
        private static string URL_API = "http://retroachievements.org/API/";
        private static string URL_Auth = "?z=FBiDev&y=uBuG840fXTyKSQvS8MFKX5d40fOelJ29";
        public static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";

        //API
        public static string API_ConsoleIDs = "API_GetConsoleIDs.php";

        //JSON
        public static string JSN_Consoles = "Consoles.json";
        public static string JSN_GameList = "GameList.json";

        //Images
        public static string Format_Badges = ".png";
        public static string Format_BadgesLocal = ".png";

        public static string API_URL(string page, string param1Name = "", string param1Value = "")
        {
            return URL_API + page + URL_Auth + param1Name + param1Value;
        }

        private static List<string> LocalJsonFiles = new List<string>() {
            JSN_Consoles, "Teste"
        };

        public async static Task<Game> GetGameInfoExtended(int gameID)
        {
            if (gameID <= 0) { return null; }

            string fileName = Folder.Game + gameID + ".json";
            //JObject result = Browser.ToJObject(API_URL("API_GetGameExtended.php", "&i=", gameID.ToString()));
            Download dl = new Download()
            {
                Overwrite = false,
                Files = new List<DownloadFile>() { new DownloadFile(API_URL("API_GetGameExtended.php", "&i=", gameID.ToString()), fileName) },
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
                Overwrite = false,
                Files = new List<DownloadFile>() { new DownloadFile(API_URL("API_GetGameList.php", "&i=", 12.ToString()), fileGameList) },
                ProgressBarName = "pgbUpdates",
                LabelBytesName = "lblUpdateProgress",
                LabelTimeName = "lblUpdateConsoles",
            };
            await dl.Start();

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
            GamesWithCheevos.ForEach(gc => gFiles.AddRange(gc.AchievementsList.Select(a => new DownloadFile(a.BadgeURL, a.BadgeFile)).ToList().Distinct().ToList()));

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
            pic.Save(Folder.Game + "badges", PictureFormat.Jpg);

            return;
        }

        public static void UpdateConsolesFile(Download download)
        {
            download.Form.BeginInvoke((MethodInvoker)delegate
            {
                download.URL = API_URL(API_ConsoleIDs);
                download.FileName = FolderJson + JSN_Consoles;
                Browser.startDownload(download);

                //string fileLocal = Local_JsonFolder + JSN_Consoles;

                //string fileUpdate = Local_JsonFolder + Update_JsonFile;

                //DownloadFile(GetURL(API_ConsoleIDs), fileLocal);

                //List<FileUpdate> objList = FileToList<FileUpdate>(fileUpdate);
                //FileUpdate obj = FindFileName(objList, JSN_Consoles);

                //if (obj is object)
                //{
                //    obj.Update = DateTime.Now;
                //}

                //UpdateFile<List<FileUpdate>>(objList, fileUpdate);
                //return obj;
                //return File.GetLastWriteTime(fileLocal);
            });
        }

        public static void CheckLocalFiles()
        {
            Directory.CreateDirectory(FolderJson);
            foreach (string json in LocalJsonFiles)
            {
                if (!File.Exists(FolderJson + json))
                {
                    File.WriteAllBytes(FolderJson + json, new byte[0]);
                }
            }
            //string file1 = Local_JsonFolder + Update_JsonFile;
            //List<string> updateFiles = new List<string>
            //{
            //    JSN_Consoles,
            //};

            //if (!File.Exists(file1))
            //{
            //    int index = 1;
            //    List<FileUpdate> list = new List<FileUpdate>();
            //    foreach (var file in updateFiles)
            //    {
            //        FileUpdate fileObj = new FileUpdate
            //        {
            //            ID = index++,
            //            Name = file,
            //        };
            //        list.Add(fileObj);
            //    }

            //    UpdateFile<List<FileUpdate>>(list, file1);
            //    return;
            //}
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
