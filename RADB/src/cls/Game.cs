using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Drawing;

namespace RADB
{
    public class Game
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; }
        public int NumAchievements { get; set; }
        public int Points { get; set; }
        public int NumLeaderboards { get; set; }
        public DateTime? DateModified { get; set; }
        public int? ForumTopicID { get; set; }

        #region _ImageIcon_
        private string _ImageIcon { get; set; }
        //[JsonProperty("ImageIcon")]
        public string ImageIcon { get { return _ImageIcon; } set { _ImageIcon = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageIconFile { get { return new DownloadFile(RA.IMAGE_HOST + ImageIcon, Folder.Icons(ConsoleID) + ImageIcon); } }
        public Bitmap ImageIconBitmap { get; set; }
        public void SetImageIconBitmap()
        {
            if (ImageIconBitmap != RA.DefaultIcon) { return; }
            ImageIconBitmap = Picture.Create(ImageIconFile.Path, RA.ErrorIcon).Bitmap;
        }
        #endregion

        public Game()
        {
            ImageIconBitmap = RA.DefaultIcon;
        }

        public static string IconsMergedPath(string consoleName = "")
        {
            return Folder.Temp + Archive.MakeValidFileName(consoleName) + "_Icons";
        }

        public string BadgesMergedPath()
        {
            return Folder.Temp + ConsoleName + "(" + ConsoleID + ")_" + Archive.MakeValidFileName(Title) + "(" + ID + ")_Badges";
        }

        //public bool Insert()
        //{
        //    return GameDao.Insert(this);
        //}

        public async static Task<bool> InsertList(IList<Game> list)
        {
            return await GameDao.InsertList(list);
        }

        public async static Task<bool> Delete(int ConsoleID)
        {
            return await GameDao.Delete(new Game { ConsoleID = ConsoleID });
        }

        //public async static Task<List<Game>> List()
        //{
        //    return (await GameDao.List());
        //}

        //public async static Task<Game> Find(int id)
        //{
        //    return (await GameDao.Find(id));
        //}

        public async static Task<List<Game>> Search(int consoleID, bool allTables = false)
        {
            var obj = new Game { ConsoleID = consoleID };
            return (await GameDao.Search(obj, allTables));
        }

        public async static Task<List<Game>> ListToHide()
        {
            return (await GameDao.ListToHide());
        }

        public async Task<bool> InsertToHide()
        {
            return await GameDao.InsertToHide(this);
        }

        public async Task<bool> DeleteFromHide()
        {
            return await GameDao.DeleteFromHide(this);
        }

        public async static Task<List<Game>> ListToPlay()
        {
            return await GameDao.ListToPlay();
        }

        public async Task<bool> InsertToPlay()
        {
            return await GameDao.InsertToPlay(this);
        }

        public async Task<bool> DeleteFromPlay()
        {
            return await GameDao.DeleteFromPlay(this);
        }
    }
}
