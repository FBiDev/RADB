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

        #region _ImageIcon
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
        #endregion

        #region _ImageTitle
        private string _ImageTitle = string.Empty;
        public string ImageTitle
        {
            get { return _ImageTitle; }
            set
            {
                _ImageTitle = value.Replace(@"/Images/", "");
                if (File.Exists(ImageTitlePath) && new FileInfo(ImageTitlePath).Length > 0)
                {
                    ImageTitlePicture = new Picture(ImageTitlePath);
                    ImageTitleBitmap = ImageTitlePicture.Bitmap;
                }
            }
        }
        public string ImageTitlePath { get { return Folder.ImageTitle(ConsoleID) + ImageTitle; } }
        public Bitmap ImageTitleBitmap { get; set; }
        public Picture ImageTitlePicture = null;
        #endregion

        #region _ImageIngame
        private string _ImageIngame = string.Empty;
        public string ImageIngame
        {
            get { return _ImageIngame; }
            set
            {
                _ImageIngame = value.Replace(@"/Images/", "");
                if (File.Exists(ImageIngamePath) && new FileInfo(ImageIngamePath).Length > 0)
                {
                    ImageIngamePicture = new Picture(ImageIngamePath);
                    ImageIngameBitmap = ImageIngamePicture.Bitmap;
                }
            }
        }
        public string ImageIngamePath { get { return Folder.ImageIngame(ConsoleID) + ImageIngame; } }
        public Bitmap ImageIngameBitmap { get; set; }
        public Picture ImageIngamePicture = null;
        #endregion

        public string ImageBoxArt { get; set; }

        public int? ForumTopicID { get; set; }
        public string Flags { get; set; }

        //GameInfoExtended
        public int ID { get; set; }
        public bool IsFinal { get; set; }
        public int NumAchievements { get; set; }
        public int NumLeaderboards { get; set; }
        public int Points { get; set; }
        public int NumDistinctPlayersCasual { get; set; }
        public int NumDistinctPlayersHardcore { get; set; }
        public string RichPresencePatch { get; set; }

        public List<Achievement> AchievementsList { get; set; }

        public Game()
        {
            AchievementsList = new List<Achievement>();

            ImageIconBitmap = new Picture(96, 96).Bitmap;

            ImageTitlePicture = new Picture(200, 150);
            ImageTitleBitmap = ImageTitlePicture.Bitmap;

            ImageIngamePicture = new Picture(200, 150);
            ImageIngameBitmap = ImageIngamePicture.Bitmap;
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

        public string BadgesMergedFile()
        {
            return Folder.Achievements(ConsoleID, ID) + "_Badges";
        }

        public bool Incluir()
        {
            return GameDao.Incluir(this);
        }

        public bool Excluir()
        {
            return GameDao.Excluir(this);
        }
    }
}
