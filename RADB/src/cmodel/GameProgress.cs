using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Drawing;
using Newtonsoft.Json;

namespace RADB
{
    public class GameProgress
    {
        public int GameID { get; set; }
        public string Title { get; set; }
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; }
        
        public int MaxPossible { get; set; }
        [JsonProperty("MAX(aw.HardcoreMode)")]
        public int HardcoreMode { get; set; }

        public int NumAwarded { get; set; }
        public int NumAwardedHC { get; set; }
        public float PctWon { get; set; }
        public float PctWonHC { get; set; }

        #region _ImageIcon_
        private string _ImageIcon { get; set; }
        public string ImageIcon { get { return _ImageIcon; } set { _ImageIcon = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageIconFile { get { return new DownloadFile(RA.IMAGE_HOST + ImageIcon, Folder.Icons(ConsoleID) + ImageIcon); } }
        public Bitmap ImageIconBitmap { get; set; }
        public void SetImageIconBitmap()
        {
            if (ImageIconBitmap != RA.DefaultIcon) { return; }
            ImageIconBitmap = Picture.Create(ImageIconFile.Path, RA.ErrorIcon).Bitmap;
        }
        #endregion

        public GameProgress()
        {
            ImageIconBitmap = RA.DefaultIcon;
        }
    }
}
