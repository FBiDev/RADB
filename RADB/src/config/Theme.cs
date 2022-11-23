using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Drawing;
//
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
            BorderColor = Color.FromArgb(213, 223, 229);

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
            foreach (var c in f.GetControls<FlatLabelA>())
                c.ForeColor = FontColor1;

            foreach (var c in f.GetControls<FlatLabelB>())
                c.ForeColor = FontColor2;

            foreach (var c in f.GetControls<Panel>())
            {
                
            }

            foreach (var c in f.GetControls<RichTextBox>())
            {
                if (c.Name == "txtHashes")
                {
                    c.BackColor = PanelColor2;
                    c.ForeColor = FontColor1;
                }
            }

            foreach (var c in f.GetControls<FlatPanelA>())
            {
                c.BackColor = BackColor1;
                c.BorderColor = BorderColor;
                c.ForeColor = FontColor1;

                if (c.Name == "pnlBottomOutput" || c.Name == "pnlGamesConsoleName")
                {
                    c.BackColor = BackColor2;
                }

                if (c.Name == "pnlInfoImages" || c.Name == "pnlInfoBoxArt" ||
                    c.Name == "pnlInfoInGame" || c.Name == "pnlInfoTitle")
                    c.BackColor = PanelColor2;
            }

            foreach (var c in f.GetControls<FlatGroupBoxA>())
            {
                c.ForeColor = FontColor1;
                c.BackColor = GroupBoxColor1;
                c.BorderColor = BorderColor;
            }

            foreach (var c in f.GetControls<FlatTabControlA>())
            {
                c.myBackColor = TabBackColor1;
                c.myBackColor2 = TabBackColor2;
                c.myBorderColor = TabBorderColor;

                foreach (TabPage page in c.TabPages)
                {
                    page.ForeColor = FontColor1;
                    page.BackColor = TabBackColor1;
                }
            }

            foreach (var c in f.GetControls<FlatButtonA>())
            {
                if (Config.DarkMode)
                    c.DarkMode();
            }

            foreach (var c in f.GetControls<FlatCheckBoxA>())
            {
                if (Config.DarkMode)
                    c.DarkMode();
                else
                    c.LightMode();
            }

            foreach (var c in f.GetControls<FlatTextBoxA>())
            {
                if (Config.DarkMode)
                    c.DarkMode();
            }

            foreach (var c in f.GetControls<FlatDataGridA>())
            {
                if (Config.DarkMode)
                    c.DarkMode();
            }
        }
    }
}
