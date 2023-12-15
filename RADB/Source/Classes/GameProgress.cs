using System.Drawing;
using Newtonsoft.Json;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public class GameProgress
    {
        public int GameID { get; set; }
        public string Title { get; set; }
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; }

        public int? MaxPossible { get; set; }
        public int NumAwarded { get; set; }
        public float? PctWon { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        public bool HardcoreMode { get; set; }

        #region _ImageIcon_
        string _ImageIcon { get; set; }
        public string ImageIcon { get { return _ImageIcon; } set { _ImageIcon = value.Replace(@"/Images/", ""); } }
        public DownloadFile ImageIconFile { get { return new DownloadFile(RA.IMAGE_HOST + ImageIcon, Folder.Icons(ConsoleID) + ImageIcon); } }
        public Bitmap ImageIconBitmap { get; set; }
        public void SetImageIconBitmap()
        {
            if (ImageIconBitmap != RA.DefaultIcon) { return; }
            ImageIconBitmap = BitmapExtension.SuperFastLoad(ImageIconFile.Path);
        }
        #endregion

        public GameProgress()
        {
            ImageIconBitmap = RA.DefaultIcon;
        }
    }
}