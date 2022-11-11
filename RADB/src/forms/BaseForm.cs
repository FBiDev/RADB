using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Drawing;
using RADB.Properties;

namespace RADB
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public void Init(Form frm)
        {
            Icon = GNX.cConvert.ToIco(Resources.iconForm, new Size(250, 250));

            Theme.CheckTheme(frm);
        }
    }
}
