using System;
using System.Collections.Generic;
using System.Linq;
//
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.IO;
using System.Drawing;
using RADB.Properties;

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

        public string ImageIcon { get; set; }
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

        public string ImageIconName
        {
            get
            {
                return ImageIcon.Replace(@"/Images/", "");
            }
        }

        public string ImageIconFile()
        {
            return Folder.ImageIcon(ConsoleID) + ImageIconName;
        }

        public Bitmap ImageIconBitmap
        {
            get
            {
                if (File.Exists(ImageIconFile())) { return new Bitmap(ImageIconFile()); }
                return new Bitmap(36, 36);
            }
        }

        public string BadgesMergedFile()
        {
            return Folder.Achievements(ConsoleID, ID) + "_Badges";
        }

        //public string FolderID
        //{
        //    get
        //    {
        //        return Folder.GameID(ID);
        //    }
        //}

        //public string BadgesFolder
        //{
        //    get
        //    {
        //        return Folder.Badges(ID);
        //    }
        //}
    }
}
