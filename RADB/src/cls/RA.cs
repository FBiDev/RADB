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

namespace RADB
{
    public static class RA
    {
        //Folders
        private static string FolderBase { get { return @"data\"; } }
        public static string FolderTemp { get { return FolderBase + @"temp\"; } }
        public static string FolderJson = FolderBase + @"json\";
        private static string FolderGame = FolderBase + @"game\";
        private static string FolderBadges = FolderGame;

        //URLs
        private static string URL_API = "http://retroachievements.org/API/";
        private static string URL_Auth = "?z=FBiDev&y=uBuG840fXTyKSQvS8MFKX5d40fOelJ29";
        private static string URL_Badges = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";

        //API
        public static string API_ConsoleIDs = "API_GetConsoleIDs.php";

        //JSON
        public static string JSN_Consoles = "Consoles.json";
        public static string JSN_GameList = "GameList.json";
        //public static string Update_JsonFile = "Files.json";

        //Images
        private static string FormatBadgesURL = ".png";
        private static string FormatBadgesLocal = ".png";

        private static List<string> LocalJsonFiles = new List<string>() {
            JSN_Consoles, "Teste"
        };

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

        public static string GetURL(string page, string param1Name = "", string param1Value = "")
        {
            return URL_API + page + URL_Auth + param1Name + param1Value;
        }

        public static void UpdateConsolesFile(Download download)
        {
            download.Form.BeginInvoke((MethodInvoker)delegate
            {
                download.URL = GetURL(API_ConsoleIDs);
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






        public static Task<Game> GetGameInfoExtended(int gameID)
        {
            if (gameID <= 0) { return null; }

            return Task.Run(() =>
            {
                JObject result = Browser.ToJObject(GetURL("API_GetGameExtended.php", "&i=", gameID.ToString()));
                Game game = result.ToObject<Game>();
                game.SetAchievements(result);
                return game;
            });
        }

        public static void SetGameFolder(int gameID)
        {
            FolderGame = FolderBase + @"game\" + gameID + @"\";
            FolderBadges = FolderGame + @"badges\";

            Directory.CreateDirectory(FolderGame);
            Directory.CreateDirectory(FolderBadges);
        }

        public static string GetBadgeFile(Achievement achievement)
        {

            return FolderBadges + achievement.BadgeName + FormatBadgesLocal;
        }

        public static string GetBadgesMergedFile()
        {
            return FolderBadges + "_Badges";
        }

        public async static Task<bool> DownloadBadges(int gameID)
        {
            SetGameFolder(gameID);

            Game game = await GetGameInfoExtended(gameID);

            if (game.AchievementsList.Count == 0) { return false; }

            foreach (Achievement achievement in game.AchievementsList)
            {
                //byte[] badgeFile = Browser.DownloadData(URL_Badges + achievement.BadgeName + FormatBadgesURL);
                //File.WriteAllBytes(GetBadgeFile(achievement), badgeFile);
            }

            return MergeBadges(game.AchievementsList);
        }

        public static bool MergeBadges(List<Achievement> achievements)
        {
            //read all images into memory
            List<Bitmap> images = new List<Bitmap>();
            //Bitmap finalImage = null;
            Picture finalImage = null;

            try
            {
                int width = 0;
                int maxWidth = 0;
                int height = 0;
                int maxHeight = 0;

                int index = 1;
                int imagesPerRow = 11;

                string FileNotFound = string.Empty;
                int FileNotFoundIndex = 1;

                foreach (Achievement achievement in achievements)
                {
                    if (!File.Exists(GetBadgeFile(achievement)))
                    {
                        if (FileNotFoundIndex <= 30)
                        {
                            FileNotFound += "[" + FileNotFoundIndex + "] " + GetBadgeFile(achievement) + Environment.NewLine;
                        }

                        FileNotFoundIndex++;

                        continue;
                    }
                    //create a Bitmap from the file and add it to the list
                    Bitmap bitmap = new Bitmap(GetBadgeFile(achievement));

                    //update the size of the final bitmap
                    if (index <= imagesPerRow && width <= maxWidth)
                    {
                        width += bitmap.Width;
                        //if (bitmap.Height > height) 
                        //{
                        //    height = bitmap.Height;
                        //}
                        //height = bitmap.Height > height ? bitmap.Height : height;
                    }
                    if (width > maxWidth) { maxWidth = width; }

                    if (bitmap.Height > height) { height = bitmap.Height; }

                    if (index == imagesPerRow)
                    {
                        maxHeight += height;
                        height = 0;
                        width = 0;
                        index = 1;
                    }
                    index++;

                    images.Add(bitmap);
                }

                if (string.IsNullOrWhiteSpace(FileNotFound) == false)
                {
                    if (FileNotFoundIndex > 30)
                    {
                        FileNotFound += Environment.NewLine + "and more... total = " + (FileNotFoundIndex - 1); ;
                    }
                    MessageBox.Show("File Not Found: " + Environment.NewLine + FileNotFound, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                maxHeight += height;
                //create a bitmap to hold the combined image
                //finalImage = new Bitmap(maxWidth, height);
                finalImage = new Picture(maxWidth, maxHeight);

                //get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage.Bitmap))
                {
                    //set background color
                    //g.Clear(Color.Magenta);

                    //go through each image and draw it on the final image
                    int offsetW = 0;
                    int offsetH = 0;
                    int offsetHLine = 0;
                    index = 1;

                    foreach (Bitmap image in images)
                    {
                        if (index > imagesPerRow)
                        {
                            offsetH += offsetHLine;
                            offsetHLine = 0;
                            offsetW = 0;
                            index = 1;
                        }
                        if (image.Height > offsetHLine)
                        {
                            offsetHLine = image.Height;
                        }
                        index++;

                        g.DrawImage(image, new Rectangle(offsetW, offsetH, image.Width, image.Height));
                        offsetW += image.Width;
                    }
                }

                //ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
                //EncoderParameters parameters = new EncoderParameters(1)
                //{
                //Param = new EncoderParameter[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 91L) }
                //};

                //if (File.Exists(Local_BadgesFolder + fileName)) { File.Delete(Local_BadgesFolder + fileName); }

                //finalImage.Save(Local_BadgesFolder + fileName, encoder, parameters);
                finalImage.Save(GetBadgesMergedFile(), PictureFormat.Png);
                return true;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
                if (finalImage != null)
                    finalImage.Dispose();
            }
        }
    }
}
