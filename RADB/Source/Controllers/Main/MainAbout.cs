using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public static partial class MainAbout
    {
        private static RA ra = new RA();
        private static bool userCheevosIsRunning;
        private static UserProgress lastUser = new UserProgress();

        #region About
        public static async Task About_Init()
        {
            RALoginButton.Click += RALoginButton_Click;
            RAProfileButton.Click += RAProfileButton_Click;
            UserCheevosButton.Click += UserCheevosButton_Click;

            // Initial Value
            chkDarkMode.Checked = Session.Options.IsDarkMode;
            chkDarkMode.CheckedChanged += (sender, e) => Session.Options.ToggleDarkMode();

            chkDebugMode.Checked = Session.Options.IsDebugMode;
            chkDebugMode.CheckedChanged += (sender, e) => Session.Options.ToggleDebugMode();

            await About_Shown(null, null);
        }

        private static Task About_Shown(object sender, EventArgs e)
        {
            // RALoginButton_Click(null, null);
            return Task.FromResult(0);
        }

        private static async void RALoginButton_Click(object sender, EventArgs e)
        {
            // lblRALogin.ForeColor = Color.Coral;
            lblRALogin.ForeColorType = LabelType.primary;
            lblRALogin.Text = "logging in...";

            RALoginButton.Enabled = false;
            await RASite.Login();
            RALoginButton.Enabled = true;

            if (Session.RALogged)
            {
                lblRALogin.ForeColorType = LabelType.success;
                lblRALogin.Text = "logged in!";
            }
            else
            {
                lblRALogin.ForeColorType = LabelType.danger;
                lblRALogin.Text = "not logged in";
            }
        }

        private static void RAProfileButton_Click(object sender, EventArgs e)
        {
            Process.Start(RA.User_URL("FBiDev"));
        }

        private static async void UserCheevosButton_Click(object sender, EventArgs e)
        {
            if (Session.GameSelected.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            if (Page.txtUsername.Text.Length < 2)
            {
                MessageBox.Show("Username need 2 letters or more.");
                return;
            }

            if (userCheevosIsRunning)
            {
                return;
            }

            userCheevosIsRunning = true;
            UserCheevosButton.Enabled = false;
            lblUserCheevos.Text = string.Empty;

            do
            {
                UserProgress user = await ra.GetUserProgress(Page.txtUsername.Text, Session.GameSelected.ID);
                picUserCheevos.Image = Session.GameSelected.ImageIconBitmap;
                lblUserCheevos.Text = user.NumAchieved + " / " + Session.GameSelected.NumAchievements;

                if (user.SameProgress(lastUser))
                {
                    lblCheevoLoopUpdate.BackColor = Color.Orange;
                }
                else
                {
                    lblCheevoLoopUpdate.BackColor = Color.LightGreen;
                    lastUser = user;
                }

                await Task.Delay(500);
                lblCheevoLoopUpdate.BackColor = Color.Transparent;
                await Task.Delay(2500);
            }
            while (chkUserCheevos.Checked);

            userCheevosIsRunning = false;
            UserCheevosButton.Enabled = true;
        }
        #endregion
    }
}
