using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Drawing;
using System.Windows.Forms;

namespace RADB
{
    public class Theme
    {
        public static Color CheevoTitle = FlatDataGridA.DefaultForeColor;
        public static Color CheevoDescription = FlatDataGridA.DefaultForeColor;

        public static Color FormBackColor = ColorTranslator.FromHtml("#F4F4F4");
        public static Color FormFontColor = ColorTranslator.FromHtml("#D2D2D2");
        
        public static Color TabBackColor = ColorTranslator.FromHtml("#F4F4F4");

        public static void DarkMode(Form f)
        {
            CheevoTitle = Color.FromArgb(204, 153, 0);
            CheevoDescription = Color.FromArgb(44, 151, 250);

            FormBackColor = ColorTranslator.FromHtml("#212121");
            TabBackColor = FormBackColor;

            //Controls
            foreach (Control ctl in GetAllControlsRecusrvive<DataGridView>(f))
            {
                ((FlatDataGridA)ctl).DarkMode();
            }

            foreach (Control ctl in GetAllControlsRecusrvive<FlatTabControl.FlatTabControl>(f))
            {
                ((FlatTabControl.FlatTabControl)ctl).myBackColor = TabBackColor;
                ((FlatTabControl.FlatTabControl)ctl).myBackColor2 = ColorTranslator.FromHtml("#353535");
                foreach (TabPage page in ((FlatTabControl.FlatTabControl)ctl).TabPages)
                {
                    page.ForeColor = FormFontColor;
                }
            }

            f.BackColor = FormBackColor;
        }

        public static IList<T> GetAllControlsRecusrvive<T>(Control control) where T : Control
        {
            var rtn = new List<T>();
            foreach (Control item in control.Controls)
            {
                var ctr = item as T;
                if (ctr != null)
                {
                    rtn.Add(ctr);
                }
                else
                {
                    rtn.AddRange(GetAllControlsRecusrvive<T>(item));
                }

            }
            return rtn;
        }
    }
}
