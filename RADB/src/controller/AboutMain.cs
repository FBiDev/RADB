using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public partial class AboutMain
    {
        RA RA = new RA();

        public AboutMain()
        {
            f = BIND.f;
            About_Init();
        }

        #region About
        void About_Init()
        {
            f.Shown += About_Shown;

            btnRALogin.Click += btnRALogin_Click;
            btnRAProfileAbout.Click += btnRAProfileAbout_Click;

            btnUserCheevos.Click += btnUserCheevos_Click;
        }

        void About_Shown(object sender, EventArgs e)
        {
            btnRALogin_Click(null, null);
        }

        async void btnRALogin_Click(object sender, EventArgs e)
        {
            lblRALogin.ForeColor = Color.Coral;
            lblRALogin.Text = "logging in...";

            btnRALogin.Enabled = false;
            await Browser.SystemLogin();
            btnRALogin.Enabled = true;

            if (BIND.RALogged)
            {
                lblRALogin.ForeColor = Color.Green;
                lblRALogin.Text = "logged in!";
            }
            else
            {
                lblRALogin.ForeColor = Color.Firebrick;
                lblRALogin.Text = "not logged in";
            }
        }

        void btnRAProfileAbout_Click(object sender, EventArgs e)
        {
            Process.Start(RA.User_URL("FBiDev"));
        }

        bool UserCheevosIsRunning;
        static UserProgress LastUser = new UserProgress();
        async void btnUserCheevos_Click(object sender, EventArgs e)
        {
            if (BIND.Game.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }
            if (UserCheevosIsRunning) { return; }

            UserCheevosIsRunning = true;
            btnUserCheevos.Enabled = false;
            lblUserCheevos.Text = string.Empty;

            do
            {
                UserProgress user = await RA.GetUserProgress(f.txtUsername.Text, BIND.Game.ID);
                picUserCheevos.Image = BIND.Game.ImageIconBitmap;
                lblUserCheevos.Text = user.NumAchieved + " / " + BIND.Game.NumAchievements;

                if (user.SameProgress(LastUser))
                {
                    lblCheevoLoopUpdate.BackColor = Color.Orange;
                }
                else
                {
                    lblCheevoLoopUpdate.BackColor = Color.LightGreen;
                    LastUser = user;
                }

                await Task.Run(() => { Thread.Sleep(500); });

                lblCheevoLoopUpdate.BackColor = Color.Transparent;

                await Task.Run(() => { Thread.Sleep(2500); });

            } while (chkUserCheevos.Checked);

            UserCheevosIsRunning = false;
            btnUserCheevos.Enabled = true;
        }
        #endregion
    }
}
