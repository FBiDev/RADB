using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RADB
{
    public partial class frmImageViewer : Form
    {
        private Size FormInitialSize;
        private Size MinimumClientSize = new Size(192, 192);//96*2 x 96*2
        private Size MaximumClientSize = new Size(1056, 576);//96*11 x 96*6
        private Size UnitImageSize;
        private Picture PictureInitial;
        private Picture PictureSmall;
        private double zoomFactor = 0.25;
        private double zoomPercent = 1.0;

        public frmImageViewer()
        {
            InitializeComponent();
            Icon = GNX.cConvert.ToIco(Properties.Resources.iconForm, new Size(250, 250));

            MouseWheel += frmImageViewer_MouseWheel;
            FormClosing += frmImageViewer_FormClosing;

            VerticalScroll.SmallChange = 16;
            HorizontalScroll.SmallChange = 16;

            Theme.CheckTheme(this);
        }

        void frmImageViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            PictureInitial.Bitmap.Dispose();
        }

        void frmImageViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            //Up = 1 Down = -1
            int mousedelta = Math.Sign(e.Delta);
            int zoomTimes = (int)(((ClientSize.Height / UnitImageSize.Height) - 1) / zoomFactor);
            double maxZoom = (1.0 + (zoomTimes * zoomFactor));

            if (mousedelta == 1 && zoomPercent >= maxZoom || mousedelta == -1 && zoomPercent <= zoomFactor) { return; }

            zoomPercent += mousedelta * zoomFactor;

            Size newSize = new Size((int)(PictureInitial.Width * zoomPercent), (int)(PictureInitial.Height * zoomPercent));

            if (mousedelta == -1 && zoomPercent <= zoomFactor)
            {
                //Decrease Size with other interpolation
                picImage.Image = PictureSmall.Bitmap;
            }
            else if (mousedelta == 1 && zoomPercent > zoomFactor)
            {
                //Increase Size
                picImage.Image = PictureInitial.Bitmap;
            }

            picImage.Size = newSize;

            SetScrollSize();
        }

        public void SetImage(Picture pic, Size perImageSize = default(Size))
        {
            PictureInitial = new Picture(pic.Path);

            //SmallPicture
            Size sizeSmall = new Size((int)(pic.Width * zoomFactor), (int)(pic.Height * zoomFactor));
            PictureSmall = new Picture(new List<string> { pic.Path }, true, 1, sizeSmall, true);

            Text += " - " + PictureInitial.Name;

            picImage.Image = PictureInitial.Bitmap;
            picImage.Size = PictureInitial.Size;

            UnitImageSize = perImageSize == default(Size) ? new Size(64, 64) : perImageSize;

            int cliW = picImage.Width <= MinimumClientSize.Width ? MinimumClientSize.Width :
                                        (picImage.Width >= MaximumClientSize.Width) ? MaximumClientSize.Width : picImage.Width;

            int cliH = picImage.Height <= MinimumClientSize.Height ? MinimumClientSize.Height :
                                        (picImage.Height >= MaximumClientSize.Height) ? MaximumClientSize.Height : picImage.Height;

            ClientSize = new Size(cliW, cliH);

            //Add ScrollW space
            if (picImage.Height > MaximumClientSize.Height)
            {
                Width += SystemInformation.VerticalScrollBarWidth;
            }

            FormInitialSize = this.Size;

            SetScrollSize();
        }

        public void SetScrollSize()
        {
            //Add ScrollH space
            int ScrollH = HorizontalScroll.Visible && Height == FormInitialSize.Height ? SystemInformation.HorizontalScrollBarHeight :
                          !HorizontalScroll.Visible && Height > FormInitialSize.Height ? -SystemInformation.HorizontalScrollBarHeight : 0;

            Height += ScrollH;
        }
    }
}
