using System;
using System.Collections.Generic;
using System.Linq;
//
using System.Drawing;
using System.Threading.Tasks;
using System.Globalization;
//
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GNX;

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
        private string _ImageTitle { get; set; }
        public string ImageTitle { get { return _ImageTitle; } set { _ImageTitle = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageTitleFile { get { return new DownloadFile(RA.URL_Images + ImageTitle, Folder.Titles(ConsoleID) + ImageTitle); } }
        public Bitmap ImageTitleBitmap { get; set; }

        private string _ImageIngame { get; set; }
        public string ImageIngame { get { return _ImageIngame; } set { _ImageIngame = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageIngameFile { get { return new DownloadFile(RA.URL_Images + ImageIngame, Folder.Ingame(ConsoleID) + ImageIngame); } }
        public Bitmap ImageIngameBitmap { get; set; }

        private string _ImageBoxArt { get; set; }
        public string ImageBoxArt { get { return _ImageBoxArt; } set { _ImageBoxArt = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageBoxArtFile { get { return new DownloadFile(RA.URL_Images + ImageBoxArt, Folder.BoxArt(ConsoleID) + ImageBoxArt); } }
        public Bitmap ImageBoxArtBitmap { get; set; }

        public void SetImagesBitmap()
        {
            if (ImageTitleBitmap == RA.DefaultTitleImage) { ImageTitleBitmap = Picture.Create(ImageTitleFile.Path, RA.ErrorIcon).Bitmap; }
            if (ImageIngameBitmap == RA.DefaultIngameImage) { ImageIngameBitmap = Picture.Create(ImageIngameFile.Path, RA.ErrorIcon).Bitmap; }
            if (ImageBoxArtBitmap == RA.DefaultBoxArtImage) { ImageBoxArtBitmap = Picture.Create(ImageBoxArtFile.Path, RA.ErrorIcon).Bitmap; }
        }
        #endregion

        public string Flags { get; set; }

        private string _Released { get; set; }
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

                DateTime d;
                if (DateTime.TryParse(value, out  d)) { ReleasedDate = d; return; }
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

        public List<string> AchievementsFiles()
        {
            List<string> files = new List<string>();
            AchievementsList.ForEach(c => { files.Add(c.BadgeFile()); });
            return files;
        }

        public static string BadgesMerged(int consoleID = 0, string gameTitle = "")
        {
            return Folder.Temp + consoleID + "_" + gameTitle + "_Badges";
        }

        public bool Incluir()
        {
            return GameExtendDao.Incluir(this);
        }

        public async Task<bool> Excluir()
        {
            return await GameExtendDao.Excluir(this);
        }

        public async static Task<GameExtend> Listar(int ID)
        {
            return await GameExtendDao.Listar(new GameExtend() { ID = ID });
        }

        public async static Task<GameExtend> Listar(GameExtend obj = null)
        {
            return await GameExtendDao.Listar(obj);
        }
    }
}
