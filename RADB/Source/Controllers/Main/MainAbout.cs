using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public static partial class MainAbout
    {
        static RA RA = new RA();

        #region About
        public static async Task About_Init()
        {
            btnRALogin.Click += btnRALogin_Click;
            btnRAProfileAbout.Click += btnRAProfileAbout_Click;
            btnUserCheevos.Click += btnUserCheevos_Click;

            //Initial Value
            chkDarkMode.Checked = Session.Options.DarkMode;
            chkDarkMode.CheckedChanged += (sender, e) => Session.Options.ToggleDarkMode();

            chkDebugMode.Checked = Session.Options.DebugMode;
            chkDebugMode.CheckedChanged += (sender, e) => Session.Options.ToggleDebugMode();

            await About_Shown(null, null);
        }

        static Task About_Shown(object sender, EventArgs e)
        {
            btnRALogin_Click(null, null);
            return Task.FromResult(0);
        }

        static async void btnRALogin_Click(object sender, EventArgs e)
        {
            //lblRALogin.ForeColor = Color.Coral;
            lblRALogin.ForeColorType = LabelType.primary;
            lblRALogin.Text = "logging in...";

            btnRALogin.Enabled = false;
            await Browser.SystemLogin();
            btnRALogin.Enabled = true;

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

        static void btnRAProfileAbout_Click(object sender, EventArgs e)
        {
            Process.Start(RA.User_URL("FBiDev"));
        }

        static bool UserCheevosIsRunning;
        static UserProgress LastUser = new UserProgress();
        static async void btnUserCheevos_Click(object sender, EventArgs e)
        {
            if (Session.Game.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }
            if (form.txtUsername.Text.Length < 2)
            {
                MessageBox.Show("Username need 2 letters or more.");
                return;
            }

            if (UserCheevosIsRunning) { return; }

            UserCheevosIsRunning = true;
            btnUserCheevos.Enabled = false;
            lblUserCheevos.Text = string.Empty;

            do
            {
                UserProgress user = await RA.GetUserProgress(form.txtUsername.Text, Session.Game.ID);
                picUserCheevos.Image = Session.Game.ImageIconBitmap;
                lblUserCheevos.Text = user.NumAchieved + " / " + Session.Game.NumAchievements;

                if (user.SameProgress(LastUser))
                {
                    lblCheevoLoopUpdate.BackColor = Color.Orange;
                }
                else
                {
                    lblCheevoLoopUpdate.BackColor = Color.LightGreen;
                    LastUser = user;
                }

                await Task.Delay(500);
                lblCheevoLoopUpdate.BackColor = Color.Transparent;
                await Task.Delay(2500);

            } while (chkUserCheevos.Checked);

            UserCheevosIsRunning = false;
            btnUserCheevos.Enabled = true;
        }
        #endregion
    }
}
