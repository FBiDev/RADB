using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public class Game
    {
        private static readonly GameDao DAO = new GameDao();

        private DownloadFile _extendFile;

        public Game()
        {
            // ImageIconBitmap = RA.DefaultIcon;
            ImageIconGridBitmap = RA.DefaultIconGrid;
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public int ConsoleID { get; set; }

        public string ConsoleName { get; set; }

        public string ConsoleNameShort { get; set; }

        public int NumAchievements { get; set; }

        public int Points { get; set; }

        public int NumLeaderboards { get; set; }

        public DateTime? DateModified { get; set; }

        public string ReleasedDate { get; set; }

        public string Year { get; set; }

        public int? ForumTopicID { get; set; }

        #region _ImageIcon_
        private string _ImageIcon { get; set; }

        public string ImageIcon
        {
            get { return _ImageIcon; }
            set { _ImageIcon = value.Replace(@"/Images/", string.Empty); }
        }

        public DownloadFile ImageIconFile
        {
            get { return new DownloadFile(RAMedia.GameImageBaseUrl + ImageIcon, Folder.Icons(ConsoleID) + ImageIcon); }
        }

        public Bitmap ImageIconBitmap { get; set; }

        public Bitmap ImageIconGridBitmap { get; set; }

        public static string MergedIconsPath(string consoleName = "")
        {
            return Folder.MergedIcons + Archive.MakeValidFileName(consoleName) + "_Icons";
        }

        public static async Task<bool> SaveList(IList<Game> list)
        {
            return await DAO.InsertList(list);
        }

        public static async Task<bool> Delete(int consoleID)
        {
            return await DAO.Delete(new Game { ConsoleID = consoleID });
        }

        public static async Task<Game> Find(int id)
        {
            return await DAO.Find(id);
        }

        public static async Task<List<Game>> Search(int consoleID, bool allTables = false)
        {
            var obj = new Game { ConsoleID = consoleID };
            return await DAO.Search(obj, allTables);
        }

        public static async Task<List<Game>> ListNotInReleasedDate(int consoleID)
        {
            return await DAO.ListNotInReleasedDate(consoleID);
        }

        public static async Task<List<Game>> ListToPlay()
        {
            return await DAO.ListToPlay();
        }

        public static async Task<List<Game>> ListToHide()
        {
            return await DAO.ListToHide();
        }

        public void SetYear(DateTime? date)
        {
            if (date.HasValue)
            {
                ReleasedDate = date.Value.ToString("yyyy-MM-dd");
                Year = date.Value.Year.ToString();
                return;
            }

            Year = string.Empty;
        }

        public void SetImageIconBitmap()
        {
            if (ImageIconBitmap != null)
            {
                return;
            }

            // ImageIconBitmap = Picture.Create(ImageIconFile.Path).Bitmap;
            ImageIconBitmap = BitmapExtension.SuperFastLoad(ImageIconFile.Path);
        }

        public void SetImageIconGridBitmap()
        {
            if (ImageIconGridBitmap != RA.DefaultIconGrid)
            {
                return;
            }

            ImageIconGridBitmap = BitmapExtension.FromFile(ImageIconFile.Path, new Size(32, 32));
        }
        #endregion

        public DownloadFile ExtendFile
        {
            get
            {
                if (_extendFile == null)
                {
                    var ra = new RA();
                    _extendFile = ra.API_File_GameExtend(this);
                }

                return _extendFile;
            }
        }

        public override string ToString()
        {
            return ID + " - " + Title;
        }

        public string MergedBadgesPath()
        {
            return Folder.MergedBadges + ConsoleName + "(" + ConsoleID + ")_" + Archive.MakeValidFileName(Title) + "(" + ID + ")_Badges";
        }

        public async Task<bool> SaveReleasedDate()
        {
            return await DAO.InsertReleasedDate(this);
        }

        public async Task<bool> SaveToPlay()
        {
            return await DAO.InsertToPlay(this);
        }

        public async Task<bool> DeleteFromPlay()
        {
            return await DAO.DeleteFromPlay(this);
        }

        public async Task<bool> SaveToHide()
        {
            return await DAO.InsertToHide(this);
        }

        public async Task<bool> DeleteFromHide()
        {
            return await DAO.DeleteFromHide(this);
        }
    }
}