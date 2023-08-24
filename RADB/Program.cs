using System;
using System.Windows.Forms;
using System.Threading;

namespace RADB
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.3", true);
            //AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.2", true);
            //AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures", true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GNX.Desktop.AppManager.SingleProcess(Config.Singleton, new Mutex(true, Config.SystemName), Config.SystemName);
            Config.Start();

            //Application.Run(new TestForm());
            Application.Run(new Main());
        }
    }
}