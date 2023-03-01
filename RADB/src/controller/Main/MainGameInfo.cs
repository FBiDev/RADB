using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GNX;

namespace RADB
{
    public static partial class MainGameInfo
    {
        static RA RA = new RA();
        static ListBind<Achievement> lstAchievs = new ListBind<Achievement>();
        static ListBind<Achievement> lstAchievsSearch = new ListBind<Achievement>();

        #region GameInfo
        public static async Task GameInfo_Init()
        {
            BIND.OnRALoggedChanged += GameInfo_Login;
            BIND.OnGameChanged += LoadSelectedGame;
            BIND.OnTabMainChanged += () => { if (BIND.SelectedTab == form.tabGameInfo) { pnlInfoScroll.Focus(); } };

            btnUpdateInfo.Click += btnUpdateInfo_Click;
            btnGamePage.Click += OnButtonGamePageClicked;
            btnHashes.Click += OnButtonHashesClicked;

            txtSearchAchiev.TextChanged += txtSearchAchiev_TextChanged;
            txtSearchAchiev.KeyDown += txtSearchAchiev_KeyDown;

            dgvAchievements.AutoGenerateColumns = false;
            dgvAchievements.DataSourceChanged += dgvAchievements_DataSourceChanged;
            dgvAchievements.CellPainting += dgvAchievements_CellPainting;

            await GameInfo_Shown(null, null);
        }

        static Task GameInfo_Shown(object sender, EventArgs e)
        {
            Browser.dlGameExtend.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
            Browser.dlGameExtendImages.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
            HideDownloadControls();
            return Task.CompletedTask;
        }

        static void HideDownloadControls()
        {
            lblUpdateInfo.Text = string.Empty;
            lblProgressInfo.Text = string.Empty;
            pgbInfo.Value = 0;
            pgbInfo.Visible = false;
        }

        static async Task LoadSelectedGame()
        {
            if (BIND.Game.IsNull()) { return; }

            HideDownloadControls();

            pnlInfoScroll.AutoScrollPosition = new Point(pnlInfoScroll.AutoScrollPosition.X, 0);
            pnlInfoScroll.VerticalScroll.Value = 0;

            LoadGameExtendBase();
            await LoadGameExtend();

            //Update GameExtend
            if (BIND.GameExtend.IsNull() || BIND.GameExtend.ConsoleID == 0)
            {
                btnUpdateInfo_Click(null, null);
            }

            form.tabMain.SelectedTab = form.tabGameInfo;

            dgvAchievements.Focus();
        }

        static void LoadGameExtendBase()
        {
            lblInfoName.Text = BIND.Game.Title + " (" + BIND.Game.ConsoleName + ")";
            picInfoIcon.Image = BIND.Game.ImageIconBitmap;

            lblInfoAchievements.Text = BIND.Game.NumAchievements.ToString() + " Trophies: " + BIND.Game.Points + " points";
        }

        static async Task LoadGameExtend()
        {
            if (BIND.Game.IsNull()) { return; }

            BIND.GameExtend = await GameExtend.Find(BIND.Game.ID);

            lblInfoDeveloper1.Text = BIND.GameExtend.Developer;
            lblInfoPublisher1.Text = BIND.GameExtend.Publisher;
            lblInfoGenre1.Text = BIND.GameExtend.Genre;
            lblInfoReleased1.Text = BIND.GameExtend.Released;

            BIND.GameExtend.SetImagesBitmap();

            picInfoTitle.ScaleTo(BIND.GameExtend.ImageTitleBitmap);
            picInfoInGame.ScaleTo(BIND.GameExtend.ImageIngameBitmap);
            picInfoBoxArt.ScaleTo(BIND.GameExtend.ImageBoxArtBitmap);

            {//Scale Boxes
                pnlInfoTitle.Height = (picInfoTitle.Height > picInfoInGame.Height ? picInfoTitle.Height : picInfoInGame.Height);
                if (pnlInfoTitle.Height < pnlInfoImages.MinimumSize.Height - 12) pnlInfoTitle.Height = pnlInfoImages.MinimumSize.Height - 12;
                pnlInfoInGame.Height = pnlInfoTitle.Height;

                pnlInfoImages.Height = pnlInfoTitle.Height + 12;
                pnlInfoBoxArt.Height = pnlInfoImages.Location.Y + pnlInfoImages.Height - 19;

                picInfoBoxArt.MaximumSize = new Size(pnlInfoBoxArt.Width - 12, pnlInfoBoxArt.Height - 12);
                picInfoBoxArt.ScaleTo(BIND.GameExtend.ImageBoxArtBitmap);

                picInfoTitle.Location = new Point(pnlInfoTitle.Width / 2 - picInfoTitle.Width / 2, (pnlInfoTitle.Height / 2) - (picInfoTitle.Height / 2));
                picInfoInGame.Location = new Point(pnlInfoInGame.Width / 2 - picInfoInGame.Width / 2, (pnlInfoInGame.Height / 2) - (picInfoInGame.Height / 2));
                picInfoBoxArt.Location = new Point(pnlInfoBoxArt.Width / 2 - picInfoBoxArt.Width / 2, (pnlInfoBoxArt.Height / 2) - (picInfoBoxArt.Height / 2));

                gpbInfo.Height = gpbInfo.PreferredSize.Height - 13;
                gpbInfoAchievements.Location = new Point(gpbInfoAchievements.Location.X, (gpbInfo.Height - pnlInfoScroll.VerticalScroll.Value) + 9);
            }

            var lstCheevos = new ListBind<Achievement>();
            dgvAchievements.DataSource = lstCheevos;

            if (File.Exists(BIND.Game.ExtendFile.Path))
            {
                string AllText = File.ReadAllText(BIND.Game.ExtendFile.Path);
                string cheevos = AllText.GetBetween("\"Achievements\":{", "}}");
                cheevos = "{" + cheevos + "}";

                JToken jcheevos = JsonConvert.DeserializeObject<JToken>(cheevos);
                BIND.GameExtend.SetAchievements(jcheevos);

                lstCheevos = new ListBind<Achievement>(BIND.GameExtend.AchievementsList);
                dgvAchievements.DataSource = lstCheevos;
            }
            lstAchievs = lstCheevos;
        }

        static async void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Download GameExtend
            if (BIND.Game.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            txtSearchAchiev.Enabled = false;
            //Download GameExtend
            await RA.DownloadGameExtend(BIND.Game, Browser.dlGameExtend);

            //Download game images
            await RA.DownloadGameExtendImages(BIND.Game);

            //Load Game
            await LoadGameExtend();
            txtSearchAchiev.Enabled = true;
            pnlInfoScroll.Focus();

            MainCommon.WriteOutput("[" + DateTime.Now.ToLongTimeString() + "] Game " + BIND.Game.ID + " Updated!");
        }

        static void GameInfo_Login()
        {
            btnHashes.Enabled = BIND.RALogged;
        }

        static void OnButtonGamePageClicked(object sender, EventArgs e)
        {
            if (BIND.Game.IsNull()) { return; }
            Process.Start(RA.Game_URL(BIND.Game.ID));
        }

        static async void OnButtonHashesClicked(object sender, EventArgs e)
        {
            if (BIND.Game.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            await HashViewer.Open(BIND.Game);
        }

        static void txtSearchAchiev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                dgvAchievements.Focus();
        }

        static void txtSearchAchiev_TextChanged(object sender, EventArgs e)
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

        static void dgvAchievements_DataSourceChanged(object sender, EventArgs e)
        {
            dgvAchievements.Height = dgvAchievements.PreferredSize.Height - 16;
            gpbInfoAchievements.Height = gpbInfoAchievements.PreferredSize.Height - 13;
        }

        static void dgvAchievements_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
    }
}
