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

        public static Task<Game> GetGameInfoExtended(int gameID)
        {
            if (gameID <= 0) { return null; }

            return Task.Run(() =>
            {
                JObject result = Browser.ToJObject(API_URL("API_GetGameExtended.php", "&i=", gameID.ToString()));
                Game game = result.ToObject<Game>();
                game.SetAchievements(result);
                return game;
            });
        }

        public async static Task DownloadBadges(int gameID)
        {
            Game game = await GetGameInfoExtended(gameID);

            if (game.AchievementsList.Count == 0) { return; }

            List<DownloadFile> ll = new List<global::RADB.DownloadFile>();
            ll.Add(new DownloadFile("http://www.cohabct.com.br/userfiles/file/Concovados/2020/classificacao_julho_2020.pdf", "t1"));
            ll.Add(new DownloadFile("http://www.cohabct.com.br/userfiles/file/Concovados/2020/classificacao_agost_2020.pdf", "t2"));
            ll.Add(new DownloadFile("http://www.cohabct.com.br/userfiles/file/Concovados/2020/classificacao_set_2020.pdf", "t3"));
            ll.Add(new DownloadFile("http://www.cohabct.com.br/userfiles/file/Inscri%C3%A7%C3%A3o%20Class%2010183%2010185.pdf", "t4"));
            ll.Add(new DownloadFile("http://www.cohabct.com.br/userfiles/file/Inscri%C3%A7%C3%A3o%20Class%2010190%2010192.pdf", "t5"));

            Download dl = new Download()
            {
                Files = game.AchievementsList.Select(a => new DownloadFile(a.BadgeURL, a.BadgeFile)).ToList(),
                //Files = ll,
                //Files = new List<DownloadFile>() { new DownloadFile(RA.API_URL("API_GetGameList.php", "&i=", ""), "GameList.json") },
                Overwrite = true,
                ProgressBarName = "pgbUpdates",
                LabelBytesName = "lblUpdateProgress",
                LabelTimeName = "lblUpdateConsoles",
            };

            await dl.Start();

            Picture pic = new Picture(game.AchievementsFiles());
            pic.Save(game.BadgesMergedFile, PictureFormat.Jpg);

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
