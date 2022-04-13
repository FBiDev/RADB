using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RADB
{
    public class Game
    {
        //Duplicate Info
        //public string Console { get; set; }
        //public string GameIcon { get; set; }
        //public string GameTitle { get; set; }

        //GameInfo
        public string Title { get; set; }
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }

        public string Released { get; set; }
        public DateTime? ReleasedDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Released)) return null;

                DateTime d;

                if (DateTime.TryParse(Released, out d)) { return d; }
                if (DateTime.TryParseExact(Released, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d)) { return d; }

                return d;
            }
        }

        //Icon
        private string _ImageIcon = string.Empty;
        public string ImageIcon
        {
            get { return _ImageIcon; }
            set
            {
                _ImageIcon = value.Replace(@"/Images/", "");
                if (File.Exists(ImageIconPath) && new FileInfo(ImageIconPath).Length > 0)
                {
                    ImageIconBitmap = new Picture(ImageIconPath).Bitmap;
                }
            }
        }
        public string ImageIconPath { get { return Folder.ImageIcon(ConsoleID) + ImageIcon; } }
        public Bitmap ImageIconBitmap { get; set; }

        public string ImageTitle { get; set; }
        public string ImageIngame { get; set; }
        public string ImageBoxArt { get; set; }

        public int? ForumTopicID { get; set; }
        public string Flags { get; set; }

        //GameInfoExtended
        public List<Achievement> AchievementsList { get; set; }
        public int ID { get; set; }
        public bool IsFinal { get; set; }
        public int NumAchievements { get; set; }
        public int NumDistinctPlayersCasual { get; set; }
        public int NumDistinctPlayersHardcore { get; set; }
        public string RichPresencePatch { get; set; }

        public Game()
        {
            AchievementsList = new List<Achievement>();

            ImageIconBitmap = new Picture(96, 96).Bitmap;
        }

        public void SetAchievements(JToken result)
        {
            //if (result.ContainsKey("Achievements"))
            //{
            foreach (JProperty cheevo in result)
            {
                AchievementsList.Add(JsonConvert.DeserializeObject<Achievement>(cheevo.Value.ToString()));
            }
            //result.Remove("Achievements");
            AchievementsList.ForEach(c => { c.GameID = ID; c.ConsoleID = ConsoleID; });
            //}
        }

        public List<string> AchievementsFiles()
        {
            List<string> files = new List<string>();
            AchievementsList.ForEach(c => { files.Add(c.BadgeFile()); });
            return files;
        }

        public int AchievementsCount
        {
            get { return AchievementsList.Count(); }
        }

        public string AchievementsPoints
        {
            get { return AchievementsList.Sum(x => x.Points) + " (" + AchievementsList.Sum(x => x.TrueRatio) + ")"; }
        }

        public string LastUpdate
        {
            get
            {
                if (AchievementsList.Count > 0)
                {
                    var date = AchievementsList.Max(x => x.DateModified).ToString("dd MMM yyyy");
                    return date.Substring(0, 3) + char.ToUpper(date[3]) + date.Substring(4);
                }
                return string.Empty;
            }
        }

        public Picture _TitleImage = null;
        public Bitmap _TitleImageBitmap
        {
            get
            {
                return _TitleImage.Bitmap;
            }
        }
        public Picture TitleImage
        {
            get
            {
                if (ImageTitle == null) { return _TitleImage = new Picture(96, 96); }

                string path = Folder.ImageTitle(ConsoleID) + ImageTitle.Replace(@"/Images/", "");
                if (File.Exists(path) == false) { return _TitleImage = new Picture(96, 96, path); }

                return _TitleImage = new Picture(path);
            }
        }

        public string BadgesMergedFile()
        {
            return Folder.Achievements(ConsoleID, ID) + "_Badges";
        }
    }
}
