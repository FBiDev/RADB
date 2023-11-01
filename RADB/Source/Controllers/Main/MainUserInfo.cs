using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace RADB
{
    public static partial class MainUserInfo
    {
        static RA RA = new RA();

        #region UserInfo
        public static async Task User_Init()
        {
            Session.OnTabMainChanged += () => { if (Session.SelectedTab == form.tabUserInfo) { txtUsername.Focus(); } };
            txtUsername.KeyDown += txtUsername_KeyDown;
            btnGetUserInfo.Click += btnGetUserInfo_Click;
            btnUserPage.Click += OnButtonUserPageClicked;
            lnkUserRank.LinkClicked += OnLinkUserRankClicked;

            await User_Shown(null, null);
        }

        static Task User_Shown(object sender, EventArgs e) { return Task.FromResult(0); }

        static void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                btnGetUserInfo_Click(null, null);
            }
        }

        static void OnButtonUserPageClicked(object sender, EventArgs e)
        {
            if (Session.User.ID > 0)
                Process.Start(RA.User_URL(txtUsername.Text));
        }

        static void OnLinkUserRankClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var rankOffset = (Session.User.Rank - 1) / 25 * 25;
            Process.Start(RA.HOST_URL + "globalRanking.php?s=5&t=2&o=" + rankOffset);
        }

        static void EnablePanelUser()
        {
            btnGetUserInfo.Enabled = true;
            btnUserPage.Enabled = true;
        }

        static void DisablePanelUser()
        {
            btnGetUserInfo.Enabled = false;
            btnUserPage.Enabled = false;
        }

        static async void btnGetUserInfo_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();

            if (txtUsername.Text.Length < 2)
            {
                MessageBox.Show("Username need 2 letters or more.");
                return;
            }

            DisablePanelUser();

            //UserInfo
            Session.User = await RA.GetUserInfo(txtUsername.Text.Trim());
            if (Session.User.Invalid)
            {
                EnablePanelUser();
                return;
            }

            //Valid User
            txtUsername.Text = Session.User.Name;
            txtUsername.SelectionLength = 0;
            txtUsername.SelectionStart = txtUsername.Text.Length;

            lsvGameAwards.Items.Clear();
            lblUserCompletion.Text = "Loading...";
            picUserLastGame.Image = null;
            picLoaderUserAwards.Visible = true;

            //Set Basic Info
            lblUserStatus.Text = Session.User.Status;
            lblUserName.Text = Session.User.Name;
            lblUserMotto.Text = Session.User.Motto;

            lblUserMemberSince.Text = Session.User.MemberSinceString;
            lblUserLastActivity.Text = Session.User.LastupdateString;
            lblUserAccountType.Text = Session.User.AccountType;

            lblUserHCPoints.Text = Session.User.TotalPointsString;
            lnkUserRank.Text = Session.User.RankString;
            lnkUserRank.Size = lnkUserRank.PreferredSize;
            lnkUserRank.LinkArea = new LinkArea(0, Session.User.RankLength);

            lblUserRetroRatio.Text = Session.User.RetroRatioString;
            lblUserSoftPoints.Text = Session.User.TotalSoftcorePointsString;
            lblUserSoftRank.Text = Session.User.RankSoft;

            //UserPciture
            Session.User = await RA.GetUserInfoPic(Session.User);
            picUserName.Image = Session.User.UserPicBitmap;

            //UserLastGame
            Session.User = await RA.GetUserInfoLastGame(Session.User);
            picUserLastGame.Image = Session.User.LastGameImage;
            lblUserLastConsole.Text = Session.User.LastGame.ConsoleName;
            lblUserLastGame.Text = Session.User.LastGameTitle;

            //--Reallocate RichPresence
            lblUserRichPresence.Text = Session.User.RichPresenceMsg;
            lblUserRichPresence.Location = new Point(lblUserRichPresence.Location.X, lblUserLastGame.Location.Y + lblUserLastGame.Size.Height + lblUserRichPresence.Margin.Top);

            //UserAwards
            {
                Session.User = await RA.GetUserInfoAwards(Session.User);

                lblUserCompletion.Text = Session.User.AverageCompletionString;
                var completedGames = Session.User.PlayedGames.Where(x => x.PctWon.Equals(1.0f));

                var dl = new Download { Overwrite = false };
                dl.Files = completedGames.Select(x => x.ImageIconFile).ToList();
                await dl.Start();

                var titles = new List<string>();
                var descs = new List<string>();

                foreach (var game in completedGames)
                {
                    game.SetImageIconBitmap();

                    titles.Add(game.Title);
                    descs.Add(game.ConsoleName + "\r\n\r\n" + "Mastered on " + "11 Sep 2022, 01:21");
                }

                var images = completedGames.Select(g => g.ImageIconBitmap).ToList();

                lsvGameAwards.MouseLeave += lsvGameAwards_MouseLeave;
                //lsvGameAwards.MouseEnter += pinnedAppsListBox_MouseEnter;
                lsvGameAwards.MouseMove += lsvGameAwards_MouseMove;
                lsvGameAwards.Scroll += lsvGameAwards_Scroll;
                lsvGameAwards.ImagesBorderColor = Color.Gold;
                lsvGameAwards.ImagesBorder = 2;
                lsvGameAwards.ImagesMargin = 6;

                await lsvGameAwards.AddImageList(images, new Size(52, 52), titles, descs);
                picLoaderUserAwards.Visible = false;
            }

            EnablePanelUser();
        }

        static void lsvGameAwards_MouseLeave(object sender, EventArgs e)
        {
            pnlAwardFloating.Visible = false;
        }

        static int mouseMoves = 1;
        static void lsvGameAwards_Scroll(object sender, ScrollEventArgs e)
        {
            mouseMoves = 1;
            lsvGameAwards_MouseMove(null, null);
        }

        static void lsvGameAwards_MouseMove(object sender, EventArgs e)
        {
            //Very Slow Draw
            //Cursor.Current = Cursors.Hand;

            mouseMoves--;
            if (mouseMoves > 0) { return; }
            mouseMoves = 4;

            pnlAwardFloating.Parent = form;
            pnlAwardFloating.BringToFront();

            var pos = Cursor.Position;
            var point = lsvGameAwards.PointToClient(pos);

            var hitInfo = lsvGameAwards.HitTest(point);

            if (hitInfo.Item == null || hitInfo.Item.Index < 0)
            {
                pnlAwardFloating.Visible = false;
                return;
            }

            var itemIndex = hitInfo.Item.Index;

            var pointParent = form.PointToClient(pos);
            pointParent.X += 15;
            pointParent.Y += 5;

            //Do any action with the item
            var currentImage = lsvGameAwards.ImagesOriginal[itemIndex];
            picAwardFloating.Image = currentImage;
            lblAwardFloatingTitle.Text = lsvGameAwards.Titles[itemIndex];
            lblAwardFloatingDesc.Text = lsvGameAwards.Descriptions[itemIndex];

            pnlAwardFloating.Location = pointParent;
            pnlAwardFloating.Visible = true;
        }
        #endregion
    }
}
