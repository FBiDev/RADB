using System;
using System.Collections.Generic;
using System.Linq;
//
using System.Drawing;
using System.Threading.Tasks;
//
using GNX;

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

        private GameDao Dao = new GameDao() { };

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

        //public bool Incluir()
        //{
        //    return GameDao.Incluir(this);
        //}

        public async static Task<bool> IncluirLista(IList<Game> list)
        {
            return await GameDao.IncluirLista(list);
        }

        //public async Task<bool> Excluir()
        //{
        //    return await GameDao.Excluir(this);
        //}

        public async static Task<bool> Excluir(int ConsoleID)
        {
            return await GameDao.Delete(new Game() { ConsoleID = ConsoleID });
        }

        //public async static Task<List<Game>> Listar()
        //{
        //    return (await GameDao.Listar());
        //}

        //public async static Task<Game> Buscar(int id)
        //{
        //    return (await GameDao.Buscar(id));
        //}

        public async static Task<List<Game>> Pesquisar(int consoleID, bool allTables = false)
        {
            var obj = new Game { ConsoleID = consoleID };
            return (await GameDao.Pesquisar(obj, allTables));
        }

        public async static Task<List<Game>> ListarToHide()
        {
            return (await GameDao.ListToHide());
        }

        public async Task<bool> IncluirToHide()
        {
            return await GameDao.InsertToHide(this);
        }

        public async Task<bool> ExcluirFromHide()
        {
            return await GameDao.DeleteFromHide(this);
        }

        public async static Task<List<Game>> ListarToPlay()
        {
            return (await GameDao.ListToPlay());
        }

        public async Task<bool> IncluirToPlay()
        {
            return await GameDao.InsertToPlay(this);
        }

        public async Task<bool> ExcluirFromPlay()
        {
            return await GameDao.DeleteFromPlay(this);
        }
    }
}
