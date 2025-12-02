using System;
using System.Windows.Forms;

namespace RADB
{
    internal static class Program
    {
        [STAThread]
        internal static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (App.Core.Desktop.AppManager.SingleProcess(Session.SystemLock, Session.SystemName))
            {
                Session.Start();
                Application.Run(new Main());

                // Application.Run(new MainForm());
                // Application.Run(new TestForm());
            }
        }
    }
}