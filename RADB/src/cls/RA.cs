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

namespace RADB
{
    public static class RA
    {
        public static string CreateURL(string page, string param1Name = "", string param1Value = "")
        {
            //var page = "API_GetGameExtended.php";
            //URL = URI_API + page + AuthQS + "&i=" + txtID.Text;
            return Config.URI_API + page + Config.AuthQS + param1Name + param1Value;
        }

        public static Game GetGameInfoExtended(int id)
        {
            var URL = CreateURL("API_GetGameExtended.php", "&i=", id.ToString());
            var content = Config.DownloadString(URL);
            JObject result = JsonConvert.DeserializeObject<JObject>(content);
            Game game = result.ToObject<Game>();
            game.SetAchievements(result);
            return game;
        }

        private static string URL_BadgesFolder = "https://s3-eu-west-1.amazonaws.com/i.retroachievements.org/Badge/";
        private static string Local_GameFolder = string.Empty;
        private static string Local_BadgesFolder = string.Empty;
        private static string URL_BadgesFormat = ".png";
        private static string Local_BadgesFormat = ".png";

        public static void DownloadBadges(int gameID)
        {
            Game game = GetGameInfoExtended(gameID);

            if (game.AchievementsList.Count == 0) { return; }

            Local_GameFolder = "src/rsc/game/" + gameID + "/";
            Local_BadgesFolder = Local_GameFolder + "badges/";

            if (!Directory.Exists(Local_GameFolder)) { Directory.CreateDirectory(Local_GameFolder); }
            if (!Directory.Exists(Local_BadgesFolder)) { Directory.CreateDirectory(Local_BadgesFolder); }

            MergeBadges(game.AchievementsList);
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
                    var imageFile = Config.DownloadData(URL_BadgesFolder + achievement.BadgeName + URL_BadgesFormat);

                    using (MemoryStream mem = new MemoryStream(imageFile))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            // If you want it as Png
                            yourImage.Save(Local_BadgesFolder + achievement.BadgeName + Local_BadgesFormat, ImageFormat.Png);

                            // If you want it as Jpeg
                            //yourImage.Save("path_to_your_file.jpg", ImageFormat.Jpeg);
                        }
                    }

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
