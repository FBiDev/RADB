using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace RADB
{
    public class FlatTextBoxA : GNX.FlatTextBox
    {
        public FlatTextBoxA()
        {

        }

        public void DarkMode()
        {
            BackgroundColor = ColorTranslator.FromHtml("#191919");
            BackgroundColorFocus = BackgroundColor;
            TextBoxColor = BackgroundColor;
            TextColor = ColorTranslator.FromHtml("#D2D2D2");
            TextColorFocus = TextColor;
            BorderColor = ColorTranslator.FromHtml("#a0a0a0");
        }
    }
}
