using System.Drawing;
using App.Core.Desktop;

namespace RADB
{
    public static class Theme
    {
        private static bool isDesignMode = true;

        public static Color CheevoTitle { get; private set; }

        public static Color CheevoDescription { get; private set; }

        public static bool ToggleDarkTheme()
        {
            if (isDesignMode)
            {
                return false;
            }

            var result = ThemeBase.ToggleDarkMode();
            CustomColors();
            return result;
        }

        public static void SetTheme(bool pageIsDesignMode)
        {
            isDesignMode = pageIsDesignMode;

            if (isDesignMode)
            {
                return;
            }

            if (Session.Options.IsDarkMode)
            {
                ThemeBase.SetTheme(ThemeBase.ThemeNames.Dark);
            }
            else
            {
                ThemeBase.SetTheme(ThemeBase.ThemeNames.Light);
            }

            CustomColors();
        }

        private static void CustomColors()
        {
            if (Session.Options.IsDarkMode)
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