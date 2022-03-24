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
        //URLs
        private static string URI_API = "http://retroachievements.org/API/";
        private static string AuthQS = "?z=FBiDev&y=uBuG840fXTyKSQvS8MFKX5d40fOelJ29";

        //JSON
        public static string Update_JsonFile = "Files.json";
        public static string JSN_Consoles = "Consoles.json";
        public static string JSN_GameList = "GameList.json";

        //API
        public static string API_ConsoleIDs = "API_GetConsoleIDs.php";

        //Folders
        private static string URL_BadgesFolder = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";
        private static string Local_BaseFolder = "src/rsc/game/";
        private static string Local_GameFolder = Local_BaseFolder;
        private static string Local_BadgesFolder = Local_BaseFolder;
        public static string Local_JsonFolder = "src/rsc/json/";

        //Images
        private static string URL_BadgesFormat = ".png";
        private static string Local_BadgesFormat = ".png";

        private static List<string> LocalJsonFiles = new List<string>() {
            JSN_Consoles, "Teste"
        };

        public static void CheckLocalFiles()
        {
            foreach (string json in LocalJsonFiles)
            {
                if (!File.Exists(Local_JsonFolder + json))
                {
                    File.WriteAllBytes(Local_JsonFolder + json, new byte[0]);
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
            return URI_API + page + AuthQS + param1Name + param1Value;
        }

        public static void UpdateConsolesFile(Download download)
        {
            download.Form.BeginInvoke((MethodInvoker)delegate
            {
                download.URL = GetURL(API_ConsoleIDs);
                download.FileName = Local_JsonFolder + JSN_Consoles;
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

        public static DateTime FileModifiedTime(string fileName)
        {
            return File.GetLastWriteTime(Local_JsonFolder + fileName);
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

        public async static Task<bool> DownloadBadges(int gameID)
        {
            Game game = await GetGameInfoExtended(gameID);

            if (game.AchievementsList.Count == 0) { return false; }

            Local_GameFolder += gameID + "/";
            Local_BadgesFolder = Local_GameFolder + "badges/";

            if (!Directory.Exists(Local_GameFolder)) { Directory.CreateDirectory(Local_GameFolder); }
            if (!Directory.Exists(Local_BadgesFolder)) { Directory.CreateDirectory(Local_BadgesFolder); }

            foreach (Achievement achievement in game.AchievementsList)
            {
                byte[] badgeFile = Browser.DownloadData(URL_BadgesFolder + achievement.BadgeName + URL_BadgesFormat);
                File.WriteAllBytes(Local_BadgesFolder + achievement.BadgeName + Local_BadgesFormat, badgeFile);
            }

            MergeBadges(game.AchievementsList);
            return true;
        }

        public static void MergeBadges(List<Achievement> achievements)
        {
            //read all images into memory
            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 64;
                int index = 1;
                int imagesPerRow = 11;
                int maxWidth = 0;

                foreach (Achievement achievement in achievements)
                {
                    //create a Bitmap from the file and add it to the list
                    Bitmap bitmap = new Bitmap(Local_BadgesFolder + achievement.BadgeName + Local_BadgesFormat);

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

                    if (index > imagesPerRow)
                    {
                        height += 64;
                        //height += bitmap.Height > height ? bitmap.Height : height;
                        width = 0;
                        index = 0;
                    }
                    index++;

                    images.Add(bitmap);
                }

                //create a bitmap to hold the combined image
                finalImage = new Bitmap(maxWidth, height);

                //get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(Color.Magenta);

                    //go through each image and draw it on the final image
                    int offsetW = 0;
                    int offsetH = 0;
                    index = 1;

                    foreach (Bitmap image in images)
                    {
                        if (index > imagesPerRow)
                        {
                            offsetH += image.Height;
                            offsetW = 0;
                            index = 0;
                        }
                        index++;

                        g.DrawImage(image,
                          new Rectangle(offsetW, offsetH, image.Width, image.Height));
                        offsetW += image.Width;
                    }
                }

                ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
                EncoderParameters parameters = new EncoderParameters(1)
                {
                    Param = new EncoderParameter[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 91L) }
                };

                string fileName = "_Badges.jpg";

                if (File.Exists(Local_BadgesFolder + fileName)) { File.Delete(Local_BadgesFolder + fileName); }

                finalImage.Save(Local_BadgesFolder + fileName, encoder, parameters);
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
                finalImage.Dispose();
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
