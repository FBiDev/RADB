using System.Drawing;
using App.Core;
using App.Core.Desktop;
using App.File.Json;

namespace RADB
{
    public class GameProgress
    {
        public GameProgress()
        {
            ImageIconBitmap = RA.DefaultIcon;
        }

        public int GameID { get; set; }

        public string Title { get; set; }

        public int ConsoleID { get; set; }

        public string ConsoleName { get; set; }

        public int? MaxPossible { get; set; }

        public int NumAwarded { get; set; }

        public float? PctWon { get; set; }

        [JsonConverter(JsonType.Boolean)]
        public bool HardcoreMode { get; set; }

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

        public void SetImageIconBitmap()
        {
            if (ImageIconBitmap != RA.DefaultIcon)
            {
                return;
            }

            ImageIconBitmap = BitmapExtension.SuperFastLoad(ImageIconFile.Path);
        }
        #endregion
    }
}