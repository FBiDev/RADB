using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace RADB
{
    public class Game
    {
        public int ID { get; set; }

        public string Title { get; set; }
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; }
        public string ConsoleNameShort { get; set; }
        public int NumAchievements { get; set; }

        public int Points { get; set; }
        public int NumLeaderboards { get; set; }
        public DateTime? DateModified { get; set; }
        public void SetYear(DateTime? date)
        {
            if (date.HasValue)
            {
                ReleasedDate = date.Value.ToString("yyyy-MM-dd");
                Year = date.Value.Year.ToString();
                return;
            }
            Year = "";
        }
        public string ReleasedDate { get; set; }
        public string Year { get; set; }
        public int? ForumTopicID { get; set; }

        #region _ImageIcon_
        string _ImageIcon { get; set; }
        public string ImageIcon { get { return _ImageIcon; } set { _ImageIcon = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageIconFile { get { return new DownloadFile(RA.IMAGE_HOST + ImageIcon, Folder.Icons(ConsoleID) + ImageIcon); } }
        public Bitmap ImageIconBitmap { get; set; }
        public void SetImageIconBitmap()
        {
            if (ImageIconBitmap != RA.DefaultIcon) { return; }
            ImageIconBitmap = Picture.Create(ImageIconFile.Path, RA.ErrorIcon).Bitmap;
        }
        #endregion

        DownloadFile _ExtendFile;
        public DownloadFile ExtendFile
        {
            get
            {
                if (_ExtendFile == null)
                {
                    var RA = new RA();
                    _ExtendFile = RA.API_File_GameExtend(this);
                }
                return _ExtendFile;
            }
        }

        public Game()
        {
            ImageIconBitmap = RA.DefaultIcon;
        }

        public static string MergedIconsPath(string consoleName = "")
        {
            return Folder.MergedIcons + Archive.MakeValidFileName(consoleName) + "_Icons";
        }

        public string MergedBadgesPath()
        {
            return Folder.MergedBadges + ConsoleName + "(" + ConsoleID + ")_" + Archive.MakeValidFileName(Title) + "(" + ID + ")_Badges";
        }

        public async Task<bool> SaveReleasedDate()
        {
            return await GameDao.InsertReleasedDate(this);
        }

        public async static Task<bool> SaveList(IList<Game> list)
        {
            return await GameDao.InsertList(list);
        }

        public async static Task<bool> Delete(int ConsoleID)
        {
            return await GameDao.Delete(new Game { ConsoleID = ConsoleID });
        }

        public async static Task<List<Game>> Search(int consoleID, bool allTables = false)
        {
            var obj = new Game { ConsoleID = consoleID };
            return (await GameDao.Search(obj, allTables));
        }

        public async static Task<List<Game>> ListNotInReleasedDate(int consoleID)
        {
            return await GameDao.ListNotInReleasedDate(consoleID);
        }

        public async static Task<List<Game>> ListToPlay()
        {
            return await GameDao.ListToPlay();
        }

        public async Task<bool> SaveToPlay()
        {
            return await GameDao.InsertToPlay(this);
        }

        public async Task<bool> DeleteFromPlay()
        {
            return await GameDao.DeleteFromPlay(this);
        }

        public async static Task<List<Game>> ListToHide()
        {
            return (await GameDao.ListToHide());
        }

        public async Task<bool> SaveToHide()
        {
            return await GameDao.InsertToHide(this);
        }

        public async Task<bool> DeleteFromHide()
        {
            return await GameDao.DeleteFromHide(this);
        }
    }
}