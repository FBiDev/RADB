using System.Drawing;

namespace RADB
{
    public class FlatButtonA : App.Core.Desktop.FlatButton
    {
        public void DarkMode()
        {
            BackgroundColor = ColorTranslator.FromHtml("#505050");
            BackgroundColorFocus = BackgroundColor;
            TextColor = ColorTranslator.FromHtml("#D2D2D2");
            BorderColor = ColorTranslator.FromHtml("#262626");
        }
    }
}