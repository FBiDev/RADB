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

            GNX.Desktop.AppManager.SingleProcess(Session.Singleton, new Mutex(true, Session.SystemName), Session.SystemName);
            Session.Start();

            //Application.Run(new TestForm());
            Application.Run(new Main());
        }
    }
}