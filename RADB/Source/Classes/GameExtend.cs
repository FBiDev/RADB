using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public class GameExtend
    {
        //GameExtended
        public int ID { get; set; }
        public int ConsoleID { get; set; }

        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }

        #region _Images
        string _ImageTitle { get; set; }
        public string ImageTitle { get { return _ImageTitle; } set { _ImageTitle = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageTitleFile { get { return new DownloadFile(RA.IMAGE_HOST + ImageTitle, Folder.Titles(ConsoleID) + ImageTitle); } }
        public Bitmap ImageTitleBitmap { get; set; }

        string _ImageIngame { get; set; }
        public string ImageIngame { get { return _ImageIngame; } set { _ImageIngame = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageIngameFile { get { return new DownloadFile(RA.IMAGE_HOST + ImageIngame, Folder.Ingame(ConsoleID) + ImageIngame); } }
        public Bitmap ImageIngameBitmap { get; set; }

        string _ImageBoxArt { get; set; }
        public string ImageBoxArt { get { return _ImageBoxArt; } set { _ImageBoxArt = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageBoxArtFile { get { return new DownloadFile(RA.IMAGE_HOST + ImageBoxArt, Folder.BoxArt(ConsoleID) + ImageBoxArt); } }
        public Bitmap ImageBoxArtBitmap { get; set; }

        public void SetImagesBitmap()
        {
            if (ImageTitleBitmap == RA.DefaultTitleImage) { ImageTitleBitmap = BitmapExtension.SuperFastLoad(ImageTitleFile.Path); }
            if (ImageIngameBitmap == RA.DefaultIngameImage) { ImageIngameBitmap = BitmapExtension.SuperFastLoad(ImageIngameFile.Path); }
            if (ImageBoxArtBitmap == RA.DefaultBoxArtImage) { ImageBoxArtBitmap = BitmapExtension.SuperFastLoad(ImageBoxArtFile.Path); }
        }
        #endregion

        public string Flags { get; set; }

        string _Released { get; set; }
        public string Released
        {
            get
            {
                return _Released;
            }
            set
            {
                _Released = value;

                if (string.IsNullOrWhiteSpace(Released)) return;

                //value = value.Trim();
                //value = value.Replace("st,", ",");
                //value = value.Replace("th,", ",");
                //value = value.Replace("nd,", ",");

                DateTime d;
                if (DateTime.TryParse(value, out d)) { ReleasedDate = d; return; }
                if (DateTime.TryParseExact(value, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out d)) { ReleasedDate = d; }
            }
        }
        public DateTime? ReleasedDate { get; set; }

        public bool IsFinal { get; set; }
        public int NumDistinctPlayersCasual { get; set; }
        public int NumDistinctPlayersHardcore { get; set; }
        public string RichPresencePatch { get; set; }

        public List<Achievement> AchievementsList { get; set; }

        public GameExtend()
        {
            ImageTitleBitmap = RA.DefaultTitleImage;
            ImageIngameBitmap = RA.DefaultIngameImage;
            ImageBoxArtBitmap = RA.DefaultBoxArtImage;
            AchievementsList = new List<Achievement>();
        }

        public void SetAchievements(JToken result)
        {
            if (AchievementsList.IsNull()) { return; }

            foreach (JProperty cheevo in result)
            {
                AchievementsList.Add(JsonConvert.DeserializeObject<Achievement>(cheevo.Value.ToString()));
            }

            AchievementsList.ForEach(c => { c.GameID = ID; c.ConsoleID = ConsoleID; });
        }

        public async Task<bool> Save()
        {
            return await GameExtendDao.Insert(this);
        }

        public async Task<bool> Delete()
        {
            return await GameExtendDao.Delete(this);
        }

        public async static Task<List<GameExtend>> List()
        {
            return await GameExtendDao.List();
        }

        public async static Task<GameExtend> Find(int gameID)
        {
            return await GameExtendDao.Find(gameID);
        }
    }
}