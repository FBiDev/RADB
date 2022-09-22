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
        public frmImageViewer()
        {
            InitializeComponent();
            Icon = GNX.cConvert.ToIco(Resources.iconForm, new Size(250, 250));
        }

        public void SetImage(string imagePath)
        {
            Picture p = new Picture(imagePath);
            picImage.ScaleTo(p.Bitmap);

            Size = new Size(this.PreferredSize.Width + 6, this.PreferredSize.Height + 6);

            if(Size.Height == MaximumSize.Height)
            {
                Width += 17;
            }
        }
    }
}
