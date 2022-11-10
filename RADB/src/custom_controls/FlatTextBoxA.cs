using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Drawing;

namespace RADB
{
    public class FlatTextBoxA : GNX.FlatTextBox
    {
        public FlatTextBoxA()
        {
            //BackgroundColorFocus = BackgroundColor;
        }

        public void DarkMode()
        {
            BackgroundColor = ColorTranslator.FromHtml("#191919");
            BackgroundColorFocus = BackgroundColor;
            LabelTextColor = ColorTranslator.FromHtml("#A3B2DC"); 
            TextColor = ColorTranslator.FromHtml("#D2D2D2");
            TextColorFocus = TextColor;
            BorderColor = ColorTranslator.FromHtml("#424242");
            //BorderColor = BorderColorFocus;
        }
    }
}
