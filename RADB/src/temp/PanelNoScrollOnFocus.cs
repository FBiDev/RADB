using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RADB
{
    public class PanelNoScrollOnFocus : Panel
    {
        protected override System.Drawing.Point ScrollToControl(Control activeControl)
        {
            return DisplayRectangle.Location;
        }
    }
}
