using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RADB.Properties;
using GNX;

namespace RADB
{
    public partial class frmImageViewer : Form
    {
        private Size OriginalSize;
        private Size MinimumClientSize = new Size(192, 192);
        private Size UnitImageSize;
        private int picW;
        private int picH;
        private double zoomFactor = 0.25;
        private double zoomPercent = 1.0;
        private int zoomTimes = 8;

        public frmImageViewer()
        {
            InitializeComponent();
            Icon = GNX.cConvert.ToIco(Resources.iconForm, new Size(250, 250));

            Shown += frmImageViewer_Shown;
            MouseWheel += frmImageViewer_MouseWheel;
        }

        void frmImageViewer_Shown(object sender, EventArgs e)
        {
            picW = picImage.Width;
            picH = picImage.Height;
        }

        void frmImageViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            //Up = 1 Down = -1
            int mousedelta = Math.Sign(e.Delta);
            double maxZoom = (1.0 + (zoomTimes * zoomFactor));

            if (mousedelta == 1 && zoomPercent >= maxZoom || mousedelta == -1 && zoomPercent <= zoomFactor) { return; }

            zoomPercent += mousedelta * zoomFactor;

            picImage.Width = (int)(picW * zoomPercent);
            picImage.Height = (int)(picH * zoomPercent);

            //Add ScrollH space
            if (zoomPercent > 1 && Height == OriginalSize.Height && picImage.Width > ClientSize.Width)
                Height += SystemInformation.HorizontalScrollBarHeight;
            else if (zoomPercent == 1 && Height > OriginalSize.Height)
                Height -= SystemInformation.HorizontalScrollBarHeight;
        }

        public void SetImage(string imagePath, Size perImageSize)
        {
            UnitImageSize = perImageSize;

            Picture p = new Picture(imagePath);
            picImage.ScaleTo(p.Bitmap);

            if (picImage.Height <= MinimumClientSize.Height)
            {
                if (picImage.Width <= MinimumClientSize.Width)
                {
                    ClientSize = new Size(MinimumClientSize.Width, MinimumClientSize.Height);
                }
                else
                {
                    ClientSize = new Size(UnitImageSize.Width * (picImage.Width / UnitImageSize.Width), MinimumClientSize.Height);
                }
            }
            else
            {
                ClientSize = picImage.Size;
            }

            //Add ScrollW space
            if (ClientSize.Height > MaximumSize.Height)
            {
                Width += SystemInformation.VerticalScrollBarWidth;
            }

            OriginalSize = this.Size;
        }
    }
}
