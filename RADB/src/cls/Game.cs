using System;
using System.Collections.Generic;
using System.Linq;
//
using System.IO;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
//
using GNX;

namespace RADB
{
    public class Game
    {
        //GameInfo
        public int ID { get; set; }
        public string Title { get; set; }
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; }
        public int NumAchievements { get; set; }
        public int Points { get; set; }
        public int NumLeaderboards { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ForumTopicID { get; set; }

        [JsonProperty("ImageIcon")]
        public string Icon { get; set; }
        public Bitmap IconBitmap { get; set; }

        //GameInfoExtended
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }

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
        public string ImageTitlePath { get { return string.IsNullOrWhiteSpace(ImageTitle) ? string.Empty : Folder.ImageTitle(ConsoleID) + ImageTitle; } }
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
        public string ImageIngamePath { get { return string.IsNullOrWhiteSpace(ImageIngame) ? string.Empty : Folder.ImageIngame(ConsoleID) + ImageIngame; } }
        public Bitmap ImageIngameBitmap { get; set; }
        public Picture ImageIngamePicture = null;
        #endregion

        public string ImageBoxArt { get; set; }
        public string Flags { get; set; }

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

        public bool IsFinal { get; set; }
        public int NumDistinctPlayersCasual { get; set; }
        public int NumDistinctPlayersHardcore { get; set; }
        public string RichPresencePatch { get; set; }

        public List<Achievement> AchievementsList { get; set; }

        public Game()
        {
            //AchievementsList = new List<Achievement>();
            //IconBitmap = RA.DefaultIconImage.Bitmap;

            ImageTitlePicture = RA.DefaultTitleImage;
            ImageTitleBitmap = ImageTitlePicture.Bitmap;

            ImageIngamePicture = RA.DefaultIngameImage;
            ImageIngameBitmap = ImageIngamePicture.Bitmap;
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

        public static string IconsMerged(string consoleName = "")
        {
            consoleName = consoleName.Replace("/", "-").Replace(" ", "-");
            return Folder.Temp + consoleName + "_Icons";
        }

        public bool Incluir()
        {
            return GameDao.Incluir(this);
        }

        public async static Task<bool> IncluirLista(IList<Game> list)
        {
            return await GameDao.IncluirLista(list);
        }

        public async Task<bool> Excluir()
        {
            return await GameDao.Excluir(this);
        }

        public async static Task<bool> Excluir(int ConsoleID)
        {
            return await GameDao.Excluir(new Game() { ConsoleID = ConsoleID });
        }

        public async static Task<List<Game>> Listar(int consoleID)
        {
            return await GameDao.Listar(new Game() { ConsoleID = consoleID });
        }

        public async static Task<List<Game>> Listar(Game obj = null)
        {
            return await GameDao.Listar(obj);
        }

        public async static Task<ListBind<Game>> ListarBind(int consoleID)
        {
            return new ListBind<Game>(await GameDao.Listar(new Game() { ConsoleID = consoleID }));
        }
    }
}
