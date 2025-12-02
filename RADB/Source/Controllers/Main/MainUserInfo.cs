using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core.Desktop;

namespace RADB
{
    public static partial class MainUserInfo
    {
        private static RA ra = new RA();
        private static int mouseMoves = 1;

        #region UserInfo
        public static async Task User_Init()
        {
            Session.OnTabMainChanged += () =>
            {
                if (Session.SelectedTab == Page.tabUserInfo)
                {
                    txtUsername.Focus();
                }
            };
            txtUsername.KeyDown += TxtUsername_KeyDown;
            btnGetUserInfo.Click += BtnGetUserInfo_Click;
            btnUserPage.Click += OnButtonUserPageClicked;
            lnkUserRank.LinkClicked += OnLinkUserRankClicked;

            await User_Shown(null, null);
        }

        private static Task User_Shown(object sender, EventArgs e)
        {
            return Task.FromResult(0);
        }

        private static void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                BtnGetUserInfo_Click(null, null);
            }
        }

        private static void OnButtonUserPageClicked(object sender, EventArgs e)
        {
            if (Session.UserSelected.ID > 0)
            {
                Process.Start(RA.User_URL(txtUsername.Text));
            }
        }

        private static void OnLinkUserRankClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var rankOffset = (Session.UserSelected.Rank - 1) / 25 * 25;
            Process.Start(RA.SiteURL + "globalRanking.php?s=5&t=2&o=" + rankOffset);
        }

        private static void EnablePanelUser()
        {
            btnGetUserInfo.Enabled = true;
            btnUserPage.Enabled = true;
        }

        private static void DisablePanelUser()
        {
            btnGetUserInfo.Enabled = false;
            btnUserPage.Enabled = false;
        }

        private static async void BtnGetUserInfo_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();

            if (txtUsername.Text.Length < 2)
            {
                MessageBox.Show("Username need 2 letters or more.");
                return;
            }

            DisablePanelUser();

            // UserInfo
            Session.UserSelected = await ra.GetUserInfo(txtUsername.Text.Trim());
            if (Session.UserSelected.Invalid)
            {
                EnablePanelUser();
                return;
            }

            // Valid User
            txtUsername.Text = Session.UserSelected.Name;
            txtUsername.SelectionLength = 0;
            txtUsername.SelectionStart = txtUsername.Text.Length;

            lsvGameAwards.Items.Clear();
            lblUserCompletion.Text = "Loading...";
            picUserLastGame.Image = null;
            picLoaderUserAwards.Visible = true;

            // Set Basic Info
            lblUserStatus.Text = Session.UserSelected.Status;
            lblUserName.Text = Session.UserSelected.Name;
            lblUserMotto.Text = Session.UserSelected.Motto;

            lblUserMemberSince.Text = Session.UserSelected.MemberSinceString;
            lblUserLastActivity.Text = Session.UserSelected.LastupdateString;
            lblUserAccountType.Text = Session.UserSelected.AccountType;

            lblUserHCPoints.Text = Session.UserSelected.TotalPointsString;
            lnkUserRank.Text = Session.UserSelected.RankString;
            lnkUserRank.Size = lnkUserRank.PreferredSize;
            lnkUserRank.LinkArea = new LinkArea(0, Session.UserSelected.RankLength);

            lblUserRetroRatio.Text = Session.UserSelected.RetroRatioString;
            lblUserSoftPoints.Text = Session.UserSelected.TotalSoftcorePointsString;
            lblUserSoftRank.Text = Session.UserSelected.RankSoft;

            // UserPciture
            Session.UserSelected = await ra.GetUserInfoPic(Session.UserSelected);
            picUserName.Image = Session.UserSelected.UserPicBitmap;

            // UserLastGame
            Session.UserSelected = await ra.GetUserInfoLastGame(Session.UserSelected);
            picUserLastGame.Image = Session.UserSelected.LastGameImage;
            lblUserLastConsole.Text = Session.UserSelected.LastGame.ConsoleName;
            lblUserLastGame.Text = Session.UserSelected.LastGameTitle;

            // Reallocate RichPresence
            lblUserRichPresence.Text = Session.UserSelected.RichPresenceMsg;
            lblUserRichPresence.Location = new Point(lblUserRichPresence.Location.X, lblUserLastGame.Location.Y + lblUserLastGame.Size.Height + lblUserRichPresence.Margin.Top);

            // UserAwards
            {
                Session.UserSelected = await ra.GetUserInfoAwards(Session.UserSelected);

                lblUserCompletion.Text = Session.UserSelected.AverageCompletionString;
                var completedGames = Session.UserSelected.PlayedGames.Where(x => x.PctWon.Equals(1.0f));

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

                // lsvGameAwards.MouseEnter += pinnedAppsListBox_MouseEnter;
                lsvGameAwards.MouseLeave += LsvGameAwards_MouseLeave;
                lsvGameAwards.MouseMove += LsvGameAwards_MouseMove;
                lsvGameAwards.Scroll += LsvGameAwards_Scroll;
                lsvGameAwards.ImagesBorderColor = Color.Gold;
                lsvGameAwards.ImagesBorder = 2;
                lsvGameAwards.ImagesMargin = 6;

                await lsvGameAwards.AddImageList(images, new Size(52, 52), titles, descs);
                picLoaderUserAwards.Visible = false;
            }

            EnablePanelUser();
        }

        private static void LsvGameAwards_MouseLeave(object sender, EventArgs e)
        {
            pnlAwardFloating.Visible = false;
        }

        private static void LsvGameAwards_Scroll(object sender, ScrollEventArgs e)
        {
            mouseMoves = 1;
            LsvGameAwards_MouseMove(null, null);
        }

        private static void LsvGameAwards_MouseMove(object sender, EventArgs e)
        {
            // Very Slow Draw
            // Cursor.Current = Cursors.Hand;
            mouseMoves--;
            if (mouseMoves > 0)
            {
                return;
            }

            mouseMoves = 4;

            pnlAwardFloating.Parent = Page;
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

            var pointParent = Page.PointToClient(pos);
            pointParent.X += 15;
            pointParent.Y += 5;

            // Do any action with the item
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
