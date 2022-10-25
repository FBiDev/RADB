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
        int picW;
        int picH;
        double zoomFactor = 0.25;
        double zoomPercent = 1.0;

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

            if (picH == 96)
            {
                zoomFactor = 0.28;
                if (zoomPercent <= zoomFactor && mousedelta == -1 || zoomPercent >= (1 + (3 * zoomFactor)) && mousedelta == 1) { return; }
            }
            else
            {
                zoomFactor = 0.25;
                if (zoomPercent <= zoomFactor && mousedelta == -1 || zoomPercent >= (1 + (7 * zoomFactor)) && mousedelta == 1) { return; }
            }

            zoomPercent += mousedelta * zoomFactor;

            picImage.Width = (int)(picW * zoomPercent);
            picImage.Height = (int)(picH * zoomPercent);
        }

        public void SetImage(string imagePath)
        {
            Picture p = new Picture(imagePath);
            picImage.ScaleTo(p.Bitmap);

            Size = new Size(this.PreferredSize.Width, this.PreferredSize.Height);

            if (Size.Height == MaximumSize.Height)
            {
                Width += 17;
            }
        }
    }
}
