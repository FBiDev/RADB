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

        public Picture _Icon = null;
        public Bitmap IconBitmap
        {
            get
            {
                return Icon.Bitmap;
            }
        }
        public Picture Icon
        {
            get
            {
                if (_Icon is Picture) { return _Icon; }
                //if (File.Exists(IconPath())) { return new Bitmap(IconPath()); }
                string path = Folder.ImageIcon(ConsoleID) + ImageIcon.Replace(@"/Images/", "");

                if (File.Exists(path)) { return new Picture(path); }
                return new Picture(96, 96);
            }
        }

        public string BadgesMergedFile()
        {
            return Folder.Achievements(ConsoleID, ID) + "_Badges";
        }
    }
}
