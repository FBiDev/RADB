using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using GNX;

namespace RADB
{
    public partial class MainLogic
    {
        #region Properties
        protected readonly Main f;

        RA RA = new RA();

        public List<DataGridView> lstDgvGames = new List<DataGridView>();

        public List<Game> lstGames = new List<Game>();
        public ListBind<Game> lstGamesSearch = new ListBind<Game>();
        public Game GameBind;
        GameExtend GameExtendBind;

        public ListBind<Game> lstGamesToPlay = new ListBind<Game>();
        public ListBind<Game> lstGamesToHide = new ListBind<Game>();

        public ListBind<Achievement> lstAchievs = new ListBind<Achievement>();
        public ListBind<Achievement> lstAchievsSearch = new ListBind<Achievement>();

        User UserBind = new User();
        #endregion

        #region MAIN
        public MainLogic(Main form)
        {
            f = form;
            BIND.f = form;

            Main_Init();
            var c1 = new ConsoleMain();
            var c2 = new GameMain();
            GameInfo_Init();
            var c4 = new GameToPlayMain();
            GamesToHide_Init();
            User_Init();
            var c7 = new AboutMain();
        }

        void Main_Init()
        {
            f.KeyDown += Main_KeyDown;
            f.Resize += Main_Resize;
            f.Load += Main_Load;
            f.Shown += Main_Shown;
            //KeyPreview = true;

            tabMain.KeyDown += tabMain_KeyDown;
            tabMain.SelectedIndexChanged += tabMain_SelectedIndexChanged;

            //Internet
            Browser.Load();
            //Folders
            Folder.CreateFolders();
        }

        void Main_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Modifiers == Keys.Alt;
        }

        FormWindowState? LastWindowState;
        void Main_Resize(object sender, EventArgs e)
        {
            if (f.WindowState != LastWindowState)
            {
                if (f.WindowState == FormWindowState.Maximized)
                {
                    //await LoadGamesIcon();
                }
                else if (f.WindowState == FormWindowState.Normal) { }
                LastWindowState = f.WindowState;
            }
        }

        void Main_Load(object sender, EventArgs e)
        {
            var j = JsonConvert.DeserializeObject<JObject>("{\"LoadJsonDLL\":\"...\"}");
        }

        void Main_Shown(object sender, EventArgs e)
        {
        }
        #endregion

        #region TABMAIN
        void tabMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Alt) { return; }
            if (e.KeyCode == Keys.Right && tabMain.SelectedIndex < tabMain.TabPages.Count) { tabMain.SelectedIndex += 1; }
            if (e.KeyCode == Keys.Left && tabMain.SelectedIndex > 0) { tabMain.SelectedIndex -= 1; }
        }

        void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = sender as TabControl;
            BIND.TabMainChanged(tab.SelectedTab);

            if (tab.SelectedTab == tabGames)
            {
                pnlGamesConsoleName.Visible = !BIND.Console.IsNull();
                dgvGames.Focus(); return;
            }
            //if (tab.SelectedTab == tabGamesToPlay) { dgvGamesToPlay.Focus(); return; }
            if (tab.SelectedTab == tabGamesToHide) { dgvGamesToHide.Focus(); return; }
            if (tab.SelectedTab == tabGameInfo) { pnlInfoScroll.Focus(); return; }
            if (tab.SelectedTab == tabUserInfo) { txtUsername.Focus(); return; }
        }
        #endregion



        #region GameInfo
        void GameInfo_Init()
        {
            BIND.OnRALoggedChanged += GameInfo_Login;

            f.Shown += GameInfo_Shown;

            btnUpdateInfo.Click += btnUpdateInfo_Click;
            btnGamePage.Click += OnButtonGamePageClicked;
            btnHashes.Click += OnButtonHashesClicked;

            txtSearchAchiev.TextChanged += txtSearchAchiev_TextChanged;
            txtSearchAchiev.KeyDown += txtSearchAchiev_KeyDown;

            dgvAchievements.AutoGenerateColumns = false;
            dgvAchievements.DataSourceChanged += dgvAchievements_DataSourceChanged;
            dgvAchievements.CellPainting += dgvAchievements_CellPainting;
        }

        void GameInfo_Shown(object sender, EventArgs e)
        {
            Browser.dlGameExtend.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
            Browser.dlGameExtendImages.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);

            btnHashes.Enabled = false;
        }

        void LoadGameExtendBase()
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

        void GameInfo_Login()
        {
            btnHashes.Enabled = BIND.RALogged;
        }

        void OnButtonGamePageClicked(object sender, EventArgs e)
        {
            if (GameBind.IsNull()) { return; }
            Process.Start(RA.Game_URL(GameBind.ID));
        }

        async void OnButtonHashesClicked(object sender, EventArgs e)
        {
            if (GameBind.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            await HashViewer.Open(GameBind);
        }

        void txtSearchAchiev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                dgvAchievements.Focus();
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

        void dgvAchievements_DataSourceChanged(object sender, EventArgs e)
        {
            dgvAchievements.Height = dgvAchievements.PreferredSize.Height - 16;
            gpbInfoAchievements.Height = gpbInfoAchievements.PreferredSize.Height - 13;
        }

        void dgvAchievements_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowHeader() || e.Value == null || e.ColumnIndex != 2) { return; }

            var dgv = sender as DataGridView;

            if (!e.Handled)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected);
            }

            if ((e.PaintParts & DataGridViewPaintParts.ContentForeground) != DataGridViewPaintParts.None)
            {
                var ach = dgv.Rows[e.RowIndex].DataBoundItem as Achievement;
                var rect1 = new Rectangle(e.CellBounds.Location, e.CellBounds.Size);

                using (var fnt = new Font(new FontFamily("Verdana"), 9.75f, FontStyle.Regular))
                {
                    using (Brush cellForeBrush = new SolidBrush(Theme.CheevoTitle))
                        e.Graphics.DrawString(ach.Title, fnt, cellForeBrush, rect1);

                    using (Brush cellForeBrush2 = new SolidBrush(Theme.CheevoDescription))
                        e.Graphics.DrawString(Environment.NewLine + ach.Description, fnt, cellForeBrush2, rect1);
                }
            }
        }
        #endregion

        #region GamesToHide
        void GamesToHide_Init()
        {
            f.Shown += GamesToHide_Shown;
            mniRemoveGameToHide.MouseDown += mniRemoveGameToHide_MouseDown;

            dgvGamesToHide.AutoGenerateColumns = false;
            dgvGamesToHide.DataSource = lstGamesToHide;

            dgvGamesToHide.Columns.Format(CellStyle.StringCenter, 0);
            dgvGamesToHide.Columns.Format(CellStyle.Image, 1);
            dgvGamesToHide.Columns.Format(CellStyle.NumberCenter, 4, 5, 6);
            dgvGamesToHide.Columns.Format(CellStyle.DateCenter, 7);

            //dgvGamesToHide.CellPainting += dgvGames_CellPainting;
            dgvGamesToHide.MouseDown += (sender, e) => dgvGamesToHide.ShowContextMenu(e, mnuGamesToHide);
            //dgvGamesToHide.CellDoubleClick += dgvGames_CellDoubleClick;
            //dgvGamesToHide.MouseWheel += dgvGames_MouseWheel;
            //dgvGamesToHide.Scroll += dgvGames_Scroll;
            //dgvGamesToHide.Sorted += dgvGames_Sorted;

            lstDgvGames.Add(dgvGamesToHide);
        }

        async void GamesToHide_Shown(object sender, EventArgs e)
        {
            await LoadGamesToHide();
        }

        async Task LoadGamesToHide()
        {
            lstGamesToHide.Clear();
            lstGamesToHide.AddRange(await Game.ListToHide());

            //await LoadGamesIcon();

            lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
            dgvGamesToHide.Refresh();
        }

        async void mniRemoveGameToHide_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGetCurrentItem<Game>(dgvGamesToHide);

            if (await game.DeleteFromHide())
            {
                if (BIND.Console.NotNull() && BIND.Console.ID == game.ConsoleID)
                {
                    lstGames.Insert(0, game);
                    lstGamesSearch.Insert(0, game);
                }

                lstGamesToHide.Remove(game);
                lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();

                //await LoadGamesIcon();
            }
        }
        #endregion

        #region UserInfo
        void User_Init()
        {
            f.Shown += User_Shown;
            txtUsername.KeyDown += txtUsername_KeyDown;
            btnGetUserInfo.Click += btnGetUserInfo_Click;
            btnUserPage.Click += OnButtonUserPageClicked;
            lnkUserRank.LinkClicked += lnkUserRank_LinkClicked;
        }

        void User_Shown(object sender, EventArgs e)
        {
        }

        void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                btnGetUserInfo_Click(null, null);
            }
        }

        void OnButtonUserPageClicked(object sender, EventArgs e)
        {
            if (UserBind.ID > 0)
                Process.Start(RA.User_URL(txtUsername.Text));
        }

        void lnkUserRank_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UserBind.RankInvalid) { return; }

            var rankOffset = (UserBind.Rank - 1) / 25 * 25;
            Process.Start(RA.HOST_URL + "globalRanking.php?s=5&t=2&o=" + rankOffset);
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

            //UserPciture
            UserBind = await RA.GetUserInfoPic(UserBind);
            picUserName.Image = UserBind.UserPicBitmap;

            //UserLastGame
            UserBind = await RA.GetUserInfoLastGame(UserBind);
            picUserLastGame.Image = UserBind.LastGameImage;
            lblUserLastConsole.Text = UserBind.LastGame.ConsoleName;
            lblUserLastGame.Text = UserBind.LastGameTitle;

            //--Reallocate RichPresence
            lblUserRichPresence.Text = UserBind.RichPresenceMsg;
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

        void lsvGameAwards_MouseLeave(object sender, EventArgs e)
        {
            pnlAwardFloating.Visible = false;
        }

        int mouseMoves = 1;
        void lsvGameAwards_Scroll(object sender, ScrollEventArgs e)
        {
            mouseMoves = 1;
            lsvGameAwards_MouseMove(null, null);
        }

        void lsvGameAwards_MouseMove(object sender, EventArgs e)
        {
            //Very Slow Draw
            //Cursor.Current = Cursors.Hand;

            mouseMoves--;
            if (mouseMoves > 0) { return; }
            mouseMoves = 4;

            pnlAwardFloating.Parent = f;
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

            Point pointParent = f.PointToClient(pos);
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



        #region Common
        public static T dgvGetCurrentItem<T>(object sender) where T : class
        {
            var dgv = sender as DataGridView;
            if (dgv.CurrentRow.NotNull())
                return dgv.CurrentRow.DataBoundItem as T;
            return null;
        }

        public static void dgv_KeyPress(object sender, KeyPressEventArgs e, string columnName)
        {
            DataGridView dgv = (DataGridView)sender;
            char typedChar = e.KeyChar;

            if (char.IsLetter(typedChar))
            {
                if (typedChar == (char)Keys.Left || typedChar == (char)Keys.Right ||
                    typedChar == (char)Keys.Up || typedChar == (char)Keys.Down)
                {
                    return;
                }

                for (int i = 0; i < (dgv.RowCount); i++)
                {
                    if (dgv.Rows[i].Cells[columnName].Value.ToString().StartsWith(typedChar.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (dgv.Rows[dgv.CurrentRow.Index].Cells[columnName].Value.ToString().StartsWith(typedChar.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (i <= dgv.CurrentRow.Index) continue;
                        }

                        dgv.Rows[i].Cells[0].Selected = true;
                        return;
                    }
                }
            }
        }
        #endregion
    }
}