using System;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
//
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GNX;

namespace RADB
{
    public partial class Main : BaseForm
    {
        #region Init
        MainLogic logic;
        RA RA = new RA();

        public static Console ConsoleBind;
        public Game GameBind;
        GameExtend GameExtendBind;
        User UserBind = new User();

        public ListBind<Game> lstGames = new ListBind<Game>();
        public ListBind<Game> lstGamesSearch = new ListBind<Game>();

        public ListBind<Game> lstGamesToHide = new ListBind<Game>();
        public ListBind<Game> lstGamesToPlay = new ListBind<Game>();

        public List<DataGridView> lstDgvGames = new List<DataGridView>();

        public ListBind<Achievement> lstAchievs = new ListBind<Achievement>();
        public ListBind<Achievement> lstAchievsSearch = new ListBind<Achievement>();

        public Main()
        {
            InitializeComponent();
            Init(this);

            logic = new MainLogic(this);

            dgvAchievements.AutoGenerateColumns = false;
            dgvAchievements.DataSourceChanged += dgvAchievements_DataSourceChanged;
            dgvAchievements.CellPainting += dgvAchievements_CellPainting;

            txtSearchAchiev.TextChanged += txtSearchAchiev_TextChanged;
            txtSearchAchiev.KeyDown += txtSearchAchiev_KeyDown;

            //Reset placeholders
            lblProgressInfo.Text = string.Empty;
            lblUpdateInfo.Text = string.Empty;
        }
        #endregion

        #region GameInfo
        public void LoadGameExtendBase()
        {
            if (GameBind.IsNull()) { return; }

            lblInfoName.Text = GameBind.Title + " (" + GameBind.ConsoleName + ")";
            picInfoIcon.Image = GameBind.ImageIconBitmap;

            lblInfoAchievements.Text = GameBind.NumAchievements.ToString() + " Trophies: " + GameBind.Points + " points";
        }

        public async Task LoadGameExtend()
        {
            if (GameBind.IsNull()) { return; }

            GameExtendBind = await GameExtend.Find(GameBind.ID);

            lblInfoDeveloper1.Text = GameExtendBind.Developer;
            lblInfoPublisher1.Text = GameExtendBind.Publisher;
            lblInfoGenre1.Text = GameExtendBind.Genre;
            lblInfoReleased1.Text = GameExtendBind.Released;

            GameExtendBind.SetImagesBitmap();

            picInfoTitle.ScaleTo(GameExtendBind.ImageTitleBitmap);
            picInfoInGame.ScaleTo(GameExtendBind.ImageIngameBitmap);
            picInfoBoxArt.ScaleTo(GameExtendBind.ImageBoxArtBitmap);

            {//Scale Boxes
                pnlInfoTitle.Height = (picInfoTitle.Height > picInfoInGame.Height ? picInfoTitle.Height : picInfoInGame.Height);
                if (pnlInfoTitle.Height < pnlInfoImages.MinimumSize.Height - 12) pnlInfoTitle.Height = pnlInfoImages.MinimumSize.Height - 12;
                pnlInfoInGame.Height = pnlInfoTitle.Height;

                pnlInfoImages.Height = pnlInfoTitle.Height + 12;
                pnlInfoBoxArt.Height = pnlInfoImages.Location.Y + pnlInfoImages.Height - 19;

                picInfoBoxArt.MaximumSize = new Size(pnlInfoBoxArt.Width - 12, pnlInfoBoxArt.Height - 12);
                picInfoBoxArt.ScaleTo(GameExtendBind.ImageBoxArtBitmap);

                picInfoTitle.Location = new Point(pnlInfoTitle.Width / 2 - picInfoTitle.Width / 2, (pnlInfoTitle.Height / 2) - (picInfoTitle.Height / 2));
                picInfoInGame.Location = new Point(pnlInfoInGame.Width / 2 - picInfoInGame.Width / 2, (pnlInfoInGame.Height / 2) - (picInfoInGame.Height / 2));
                picInfoBoxArt.Location = new Point(pnlInfoBoxArt.Width / 2 - picInfoBoxArt.Width / 2, (pnlInfoBoxArt.Height / 2) - (picInfoBoxArt.Height / 2));

                gpbInfo.Height = gpbInfo.PreferredSize.Height - 13;
                gpbInfoAchievements.Location = new Point(gpbInfoAchievements.Location.X, (gpbInfo.Height - pnlInfoScroll.VerticalScroll.Value) + 9);
            }

            ListBind<Achievement> lstCheevos = new ListBind<Achievement>();
            dgvAchievements.DataSource = lstCheevos;
            if (File.Exists(RA.API_File_GameExtend(GameBind).Path))
            {
                //gx.SetAchievements(resultInfo["Achievements"]);
                string AllText = File.ReadAllText(RA.API_File_GameExtend(GameBind).Path);
                string cheevos = AllText.GetBetween("\"Achievements\":{", "}}");
                cheevos = "{" + cheevos + "}";

                JToken jcheevos = JsonConvert.DeserializeObject<JToken>(cheevos);

                GameExtendBind.SetAchievements(jcheevos);
                lstCheevos = new ListBind<Achievement>(GameExtendBind.AchievementsList);
                dgvAchievements.DataSource = lstCheevos;
            }
            lstAchievs = lstCheevos;
        }

        public async void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Download GameExtend
            if (GameBind.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            txtSearchAchiev.Enabled = false;
            //Download GameExtend
            await RA.DownloadGameExtend(GameBind, Browser.dlGameExtend);

            //Download game images
            await RA.DownloadGameExtendImages(GameBind);

            //Load Game
            await LoadGameExtend();
            txtSearchAchiev.Enabled = true;

            lblOutput.Text = "[" + DateTime.Now.ToLongTimeString() + "] Game " + GameBind.ID + " Updated!" + Environment.NewLine + lblOutput.Text;

            pnlInfoScroll.Focus();
        }

        void txtSearchAchiev_TextChanged(object sender, EventArgs e)
        {
            ListBind<Achievement> newSearch = new ListBind<Achievement>();
            foreach (Achievement obj in lstAchievs)
            {
                bool title = (obj.Title != null && (obj.Title.IndexOf(txtSearchAchiev.Text, StringComparison.CurrentCultureIgnoreCase) > -1));
                bool desc = (obj.Description != null && (obj.Description.IndexOf(txtSearchAchiev.Text, StringComparison.CurrentCultureIgnoreCase) > -1));

                if (title || desc)
                {
                    newSearch.Add(obj);
                }
            }

            int scrollPosition = pnlInfoScroll.VerticalScroll.Value;

            lstAchievsSearch = newSearch;
            dgvAchievements.DataSource = lstAchievsSearch;

            if (scrollPosition < gpbInfo.Height + 4) scrollPosition = gpbInfo.Height + 4;
            if (scrollPosition > pnlInfoScroll.VerticalScroll.Maximum) scrollPosition = pnlInfoScroll.VerticalScroll.Maximum;

            bool maintainScroll = true;
            if (maintainScroll)
            {
                bool txtFocus = txtSearchAchiev.Focused;

                pnlInfoScroll.VerticalScroll.Value = scrollPosition;

                if (txtFocus) { txtSearchAchiev.Focus(); }
            }

            dgvAchievements.Refresh();
        }

        void txtSearchAchiev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dgvAchievements.Focus();
            }
        }
        #endregion

        void dgvAchievements_DataSourceChanged(object sender, EventArgs e)
        {
            dgvAchievements.Height = dgvAchievements.PreferredSize.Height - 16;
            gpbInfoAchievements.Height = gpbInfoAchievements.PreferredSize.Height - 13;
        }

        public async void btnGetUserInfo_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();
            btnGetUserInfo.Enabled = false;
            btnUserPage.Enabled = false;

            //UserInfo
            UserBind = await RA.GetUserInfo(txtUsername.Text.Trim());
            btnGetUserInfo.Enabled = true;
            if (UserBind.Invalid) return;

            //Valid User
            btnUserPage.Enabled = true;

            lsvGameAwards.Items.Clear();
            lblUserCompletion.Text = "Loading...";
            picUserLastGame.Image = null;
            picLoaderUserAwards.Visible = true;

            //Set Basic Info
            lblUserStatus.Text = UserBind.Status;
            lblUserName.Text = UserBind.Name;
            lblUserMotto.Text = UserBind.Motto;

            lblUserMemberSince.Text = UserBind.MemberSinceString;
            lblUserLastActivity.Text = UserBind.LastupdateString;
            lblUserAccountType.Text = UserBind.AccountType;

            lblUserHCPoints.Text = UserBind.TotalPointsString;
            lnkUserRank.Text = UserBind.RankString;
            lnkUserRank.Size = lnkUserRank.PreferredSize;

            if (UserBind.RankInvalid)
                lnkUserRank.LinkArea = new LinkArea(0, 0);
            else
                lnkUserRank.LinkArea = new LinkArea(0, UserBind.Rank.ToString().Count());

            lblUserRetroRatio.Text = UserBind.RetroRatioString;
            lblUserSoftPoints.Text = UserBind.TotalSoftcorePointsString;
            lblUserSoftRank.Text = UserBind.RankSoft;

            lblUserRichPresence.Text = UserBind.RichPresenceMsg;

            //UserPciture
            UserBind = await RA.GetUserInfoPic(UserBind);
            picUserName.Image = UserBind.UserPicBitmap;

            //UserLastGame
            UserBind = await RA.GetUserInfoLastGame(UserBind);
            picUserLastGame.Image = UserBind.LastGameImage;
            lblUserLastConsole.Text = UserBind.LastGame.ConsoleName;
            lblUserLastGame.Text = UserBind.LastGameTitle;

            //--Reallocate RichPresence
            lblUserRichPresence.Location = new Point(lblUserRichPresence.Location.X, lblUserLastGame.Location.Y + lblUserLastGame.Size.Height + lblUserRichPresence.Margin.Top);

            //UserAwards
            {
                UserBind = await RA.GetUserInfoAwards(UserBind);

                lblUserCompletion.Text = UserBind.AverageCompletionString;
                var completedGames = UserBind.PlayedGames.Where(x => x.PctWon.Equals(1.0f));

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

        void lsvGameAwards_Scroll(object sender, ScrollEventArgs e)
        {
            mouseMoves = 1;
            lsvGameAwards_MouseMove(null, null);
        }

        void lsvGameAwards_MouseLeave(object sender, EventArgs e)
        {
            pnlAwardFloating.Visible = false;
        }

        int mouseMoves = 1;
        void lsvGameAwards_MouseMove(object sender, EventArgs e)
        {
            //Very Slow Draw
            //Cursor.Current = Cursors.Hand;

            mouseMoves--;
            if (mouseMoves > 0) { return; }
            mouseMoves = 4;

            pnlAwardFloating.Parent = this;
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

            Point pointParent = PointToClient(pos);
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

        void dgvAchievements_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowHeader() || e.Value == null || e.ColumnIndex != 2) { return; }

            DataGridView dgv = (DataGridView)sender;

            if (!e.Handled)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected);
            }

            if ((e.PaintParts & DataGridViewPaintParts.ContentForeground) != DataGridViewPaintParts.None)
            {
                Achievement ach = (Achievement)dgv.Rows[e.RowIndex].DataBoundItem;

                Rectangle rect1 = new Rectangle(e.CellBounds.Location, e.CellBounds.Size);

                using (Font fnt = new Font(new FontFamily("Verdana"), 9.75f, FontStyle.Regular))
                {
                    using (Brush cellForeBrush = new SolidBrush(Theme.CheevoTitle))
                        e.Graphics.DrawString(ach.Title, fnt, cellForeBrush, rect1);

                    using (Brush cellForeBrush2 = new SolidBrush(Theme.CheevoDescription))
                        e.Graphics.DrawString(Environment.NewLine + ach.Description, fnt, cellForeBrush2, rect1);
                }
            }
        }
    }
}