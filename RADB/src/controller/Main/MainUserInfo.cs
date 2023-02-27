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
            BIND.OnTabMainChanged += () => { if (BIND.SelectedTab == form.tabUserInfo) { txtUsername.Focus(); } };
            //f.Shown += User_Shown;
            txtUsername.KeyDown += txtUsername_KeyDown;
            btnGetUserInfo.Click += btnGetUserInfo_Click;
            btnUserPage.Click += OnButtonUserPageClicked;
            lnkUserRank.LinkClicked += lnkUserRank_LinkClicked;

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
            if (BIND.User.ID > 0)
                Process.Start(RA.User_URL(txtUsername.Text));
        }

        static void lnkUserRank_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var rankOffset = (BIND.User.Rank - 1) / 25 * 25;
            Process.Start(RA.HOST_URL + "globalRanking.php?s=5&t=2&o=" + rankOffset);
        }

        static async void btnGetUserInfo_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();

            if (txtUsername.Text.Length < 2)
            {
                MessageBox.Show("Username need 2 letters or more.");
                return;
            }

            btnGetUserInfo.Enabled = false;
            btnUserPage.Enabled = false;

            //UserInfo
            BIND.User = await RA.GetUserInfo(txtUsername.Text.Trim());
            btnGetUserInfo.Enabled = true;
            if (BIND.User.Invalid) return;

            //Valid User
            btnUserPage.Enabled = true;

            lsvGameAwards.Items.Clear();
            lblUserCompletion.Text = "Loading...";
            picUserLastGame.Image = null;
            picLoaderUserAwards.Visible = true;

            //Set Basic Info
            lblUserStatus.Text = BIND.User.Status;
            lblUserName.Text = BIND.User.Name;
            lblUserMotto.Text = BIND.User.Motto;

            lblUserMemberSince.Text = BIND.User.MemberSinceString;
            lblUserLastActivity.Text = BIND.User.LastupdateString;
            lblUserAccountType.Text = BIND.User.AccountType;

            lblUserHCPoints.Text = BIND.User.TotalPointsString;
            lnkUserRank.Text = BIND.User.RankString;
            lnkUserRank.Size = lnkUserRank.PreferredSize;
            lnkUserRank.LinkArea = new LinkArea(0, BIND.User.RankLength);

            lblUserRetroRatio.Text = BIND.User.RetroRatioString;
            lblUserSoftPoints.Text = BIND.User.TotalSoftcorePointsString;
            lblUserSoftRank.Text = BIND.User.RankSoft;

            //UserPciture
            BIND.User = await RA.GetUserInfoPic(BIND.User);
            picUserName.Image = BIND.User.UserPicBitmap;

            //UserLastGame
            BIND.User = await RA.GetUserInfoLastGame(BIND.User);
            picUserLastGame.Image = BIND.User.LastGameImage;
            lblUserLastConsole.Text = BIND.User.LastGame.ConsoleName;
            lblUserLastGame.Text = BIND.User.LastGameTitle;

            //--Reallocate RichPresence
            lblUserRichPresence.Text = BIND.User.RichPresenceMsg;
            lblUserRichPresence.Location = new Point(lblUserRichPresence.Location.X, lblUserLastGame.Location.Y + lblUserLastGame.Size.Height + lblUserRichPresence.Margin.Top);

            //UserAwards
            {
                BIND.User = await RA.GetUserInfoAwards(BIND.User);

                lblUserCompletion.Text = BIND.User.AverageCompletionString;
                var completedGames = BIND.User.PlayedGames.Where(x => x.PctWon.Equals(1.0f));

                var dl = new Download() { Overwrite = false };
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
            Point point = lsvGameAwards.PointToClient(pos);

            var hitInfo = lsvGameAwards.HitTest(point);

            if (hitInfo.Item == null || hitInfo.Item.Index < 0)
            {
                pnlAwardFloating.Visible = false;
                return;
            }

            var itemIndex = hitInfo.Item.Index;

            Point pointParent = form.PointToClient(pos);
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
