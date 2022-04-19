using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RADB
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.3", true);
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.2", true);
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures", true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RADB());
        }
    }
}
