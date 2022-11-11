using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Threading;

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
            //AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.3", true);
            //AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.2", true);
            //AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures", true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GNX.cApp.SingleProcess(Config.Singleton, new Mutex(true, Config.SystemName));
            Config.Start();

            Application.Run(new Main());
        }
    }
}
