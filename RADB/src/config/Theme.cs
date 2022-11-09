using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Drawing;
using System.Windows.Forms;
using GNX;

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


        public static void CheckTheme(Form f)
        {
            if (Config.DarkMode)
            {
                DarkMode(f);

            }
            else
            {
                LightMode(f);
            }
        }


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
            foreach (Label ctl in cForm.GetControls<Label>(f))
            {
                if (ctl.Name == "lblConsoleName" || ctl.Name == "lblConsoleGamesTotal")
                    ctl.ForeColor = FontColor2;
            }

            foreach (Panel ctl in cForm.GetControls<Panel>(f))
            {
                if (ctl.Name == "pnlInfoImages" || ctl.Name == "pnlInfoBoxArt")
                    ctl.BackColor = PanelColor2;
            }

            foreach (RichTextBox ctl in cForm.GetControls<RichTextBox>(f))
            {
                if (ctl.Name == "txtHashes")
                {
                    ctl.BackColor = PanelColor2;
                    ctl.ForeColor = FontColor1;
                }
            }

            foreach (FlatPanel ctl in cForm.GetControls<FlatPanel>(f))
            {
                ctl.BackColor = BackColor1;
                ctl.BorderColor = BorderColor;
                ctl.ForeColor = FontColor1;

                if (ctl.Name == "pnlBottomOutput" || ctl.Name == "pnlGamesConsoleName")
                {
                    ctl.BackColor = BackColor2;
                }
            }

            foreach (FlatGroupBox ctl in cForm.GetControls<FlatGroupBox>(f))
            {
                ctl.ForeColor = FontColor1;
                ctl.BackColor = GroupBoxColor1;
                ctl.BorderColor = BorderColor;
            }

            foreach (FlatTabControl.FlatTabControl ctl in cForm.GetControls<FlatTabControl.FlatTabControl>(f))
            {
                ctl.myBackColor = TabBackColor1;
                ctl.myBackColor2 = TabBackColor2;
                ctl.myBorderColor = TabBorderColor;

                foreach (TabPage page in ctl.TabPages)
                {
                    page.ForeColor = FontColor1;
                    page.BackColor = TabBackColor1;
                }
            }

            foreach (FlatButtonA ctl in cForm.GetControls<FlatButtonA>(f))
            {
                if (Config.DarkMode)
                    ctl.DarkMode();
            }

            foreach (Control ctl in cForm.GetControls<CheckBoxBlueA>(f))
            {
                //if (Config.DarkMode)
                //((CheckBoxBlueA)ctl).BackColor = ColorTranslator.FromHtml("#353535");
            }

            foreach (FlatTextBoxA ctl in cForm.GetControls<FlatTextBoxA>(f))
            {
                if (Config.DarkMode)
                    ctl.DarkMode();
            }

            foreach (FlatDataGridA ctl in cForm.GetControls<FlatDataGridA>(f))
            {
                if (Config.DarkMode)
                    ctl.DarkMode();
            }
        }
    }
}
