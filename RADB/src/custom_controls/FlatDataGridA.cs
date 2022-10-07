using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RADB
{
    public partial class FlatDataGridA : GNX.FlatDataGrid
    {
        public FlatDataGridA()
        {
            InitializeComponent();

            ColorBackground = ColorTranslator.FromHtml("#F4F4F4");
        }
    }
}
