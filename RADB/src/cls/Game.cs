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
        public DownloadFile ImageIconFile { get { return new DownloadFile(RA.URL_Images + ImageIcon, Folder.Icons(ConsoleID) + ImageIcon); } }
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
