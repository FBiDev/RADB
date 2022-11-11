using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Drawing;

namespace RADB
{
    public partial class FlatButtonA : GNX.FlatButton
    {
        public FlatButtonA()
        {
            //this.BackgroundColor = Color.FromArgb(0, 132, 255);
            //this.BackgroundColorFocus = this.BackgroundColor;
            //TextColor = Color.White;
        }

        public void DarkMode()
        {
            BackgroundColor = ColorTranslator.FromHtml("#505050");
            BackgroundColorFocus = BackgroundColor;
            TextColor = ColorTranslator.FromHtml("#D2D2D2");
            BorderColor = ColorTranslator.FromHtml("#262626");
        }
    }
}
