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
        public static Color BackColor1;
        public static Color BackColor2;
        public static Color BorderColor;

        public static Color FontColor1;
        public static Color FontColor2;

        public static Color GroupBoxColor1;

        public static Color PanelColor1;
        public static Color PanelColor2;

        public static Color TabBackColor1;
        public static Color TabBackColor2;
        public static Color TabBorderColor;


        public static Color CheevoTitle = FlatDataGridA.DefaultForeColor;
        public static Color CheevoDescription = FlatDataGridA.DefaultForeColor;





        public static void LightMode(Form f)
        {
            BackColor1 = ColorTranslator.FromHtml("#F4F4F4");
            BackColor2 = ColorTranslator.FromHtml("#F4F4F4");

            BorderColor = ColorTranslator.FromHtml("#A0A0A0");

            FontColor1 = ColorTranslator.FromHtml("#000000");
            FontColor2 = ColorTranslator.FromHtml("#4169E1");

            GroupBoxColor1 = ColorTranslator.FromHtml("#F4F4F4");
            PanelColor1 = ColorTranslator.FromHtml("#F4F4F4");
            PanelColor2 = ColorTranslator.FromHtml("#E6E6E6");

            TabBackColor1 = ColorTranslator.FromHtml("#F4F4F4");
            TabBackColor2 = ColorTranslator.FromHtml("#D4D0C8");


            TabBorderColor = ColorTranslator.FromHtml("#A0A0A0");

            ChangeTheme(f);
        }

        public static void DarkMode(Form f)
        {
            BackColor1 = ColorTranslator.FromHtml("#353535");
            BackColor2 = ColorTranslator.FromHtml("#191919");

            BorderColor = ColorTranslator.FromHtml("#424242");

            FontColor1 = ColorTranslator.FromHtml("#D2D2D2");
            FontColor2 = ColorTranslator.FromHtml("#A3B2DC");

            GroupBoxColor1 = ColorTranslator.FromHtml("#343434");
            PanelColor1 = BackColor1;
            PanelColor2 = ColorTranslator.FromHtml("#242424");

            TabBackColor1 = ColorTranslator.FromHtml("#353535");
            TabBackColor2 = ColorTranslator.FromHtml("#191919");
            TabBorderColor = ColorTranslator.FromHtml("#424242");

            CheevoTitle = Color.FromArgb(204, 153, 0);
            CheevoDescription = Color.FromArgb(44, 151, 250);

            ChangeTheme(f);
        }

        private static void ChangeTheme(Form f)
        {
            f.BackColor = BackColor1;

            //Controls
            foreach (Control ctl in GetAllControlsRecusrvive<Label>(f))
            {
                if (ctl.Name == "lblConsoleName" || ctl.Name == "lblConsoleGamesTotal")
                    ((Label)ctl).ForeColor = FontColor2;
            }

            foreach (Control ctl in GetAllOfType<Panel>(f))
            {
                if (ctl.Name == "pnlInfoImages" || ctl.Name == "pnlInfoBoxArt")
                    ((Panel)ctl).BackColor = PanelColor2;
            }

            foreach (Control ctl in GetAllControlsRecusrvive<GNX.PanelBorder>(f))
            {
                ((GNX.PanelBorder)ctl).BackColor = BackColor2;
                ((GNX.PanelBorder)ctl).BorderColor = BorderColor;
                ((GNX.PanelBorder)ctl).ForeColor = FontColor1;
            }

            foreach (Control ctl in GetAllControlsRecusrvive<FlatGroupBox>(f))
            {
                ((FlatGroupBox)ctl).ForeColor = FontColor1;
                ((FlatGroupBox)ctl).BackColor = GroupBoxColor1;
                ((FlatGroupBox)ctl).BorderColor = BorderColor;
            }

            foreach (Control ctl in GetAllControlsRecusrvive<FlatTabControl.FlatTabControl>(f))
            {
                ((FlatTabControl.FlatTabControl)ctl).myBackColor = TabBackColor1;
                ((FlatTabControl.FlatTabControl)ctl).myBackColor2 = TabBackColor2;
                ((FlatTabControl.FlatTabControl)ctl).myBorderColor = TabBorderColor;
                foreach (TabPage page in ((FlatTabControl.FlatTabControl)ctl).TabPages)
                {
                    page.ForeColor = FontColor1;
                    page.BackColor = TabBackColor1;
                }
            }

            foreach (Control ctl in GetAllControlsRecusrvive<FlatButtonA>(f))
            {
                if (Config.DarkMode)
                    ((FlatButtonA)ctl).DarkMode();
            }

            foreach (Control ctl in GetAllControlsRecusrvive<CheckBoxBlueA>(f))
            {
                //if (Config.DarkMode)
                    //((CheckBoxBlueA)ctl).BackColor = ColorTranslator.FromHtml("#353535");
            }

            foreach (Control ctl in GetAllControlsRecusrvive<FlatTextBoxA>(f))
            {
                if (Config.DarkMode)
                    ((FlatTextBoxA)ctl).DarkMode();
            }

            foreach (Control ctl in GetAllControlsRecusrvive<DataGridView>(f))
            {
                if (Config.DarkMode)
                    ((FlatDataGridA)ctl).DarkMode();
            }
        }

        private static IEnumerable<T> GetAllOfType<T>(Control rootControl)
        {
            return rootControl.Controls.OfType<T>().
                   Concat(rootControl.Controls.OfType<Control>().SelectMany(GetAllOfType<T>));

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
