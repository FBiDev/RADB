using System.Drawing;
using GNX.Desktop;

namespace RADB
{
    public static class Theme
    {
        public static bool DesignMode = true;

        public static bool ToggleDarkTheme()
        {
            if (DesignMode) return false;

            var result = ThemeBase.ToggleDarkMode();
            CustomColors();
            return result;
        }

        public static void SetTheme()
        {
            DesignMode = Session.MainForm.isDesignMode;
            if (DesignMode) return;

            if (Session.Options.DarkMode)
            {
                ThemeBase.SetTheme(ThemeBase.eTheme.Dark);
            }
            else
            {
                ThemeBase.SetTheme(ThemeBase.eTheme.Light);
            }

            CustomColors();
        }

        public static Color CheevoTitle;
        public static Color CheevoDescription;

        static void CustomColors()
        {
            if (Session.Options.DarkMode)
            {
                CheevoTitle = Color.FromArgb(204, 153, 0);
                CheevoDescription = Color.FromArgb(44, 151, 250);
            }
            else
            {
                CheevoTitle = Color.FromArgb(204, 153, 0);
                CheevoDescription = Color.FromArgb(44, 151, 250);
            }
        }
    }
}