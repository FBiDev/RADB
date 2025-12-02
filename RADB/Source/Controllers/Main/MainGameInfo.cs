using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;
using App.File.Json;

namespace RADB
{
    public static partial class MainGameInfo
    {
        private static RA ra = new RA();
        private static ListBind<Achievement> lstAchievs = new ListBind<Achievement>();
        private static ListBind<Achievement> lstAchievsSearch = new ListBind<Achievement>();

        #region GameInfo
        public static async Task GameInfo_Init()
        {
            Session.OnRALoggedChanged += GameInfo_Login;
            Session.OnGameChanged += LoadSelectedGame;
            Session.OnTabMainChanged += () =>
            {
                if (Session.SelectedTab == Page.tabGameInfo)
                {
                    pnlInfoScroll.Focus();
                }
            };

            btnUpdateInfo.Click += BtnUpdateInfo_Click;
            btnGamePage.Click += OnButtonGamePageClicked;
            btnHashes.Click += OnButtonHashesClicked;

            txtSearchAchiev.TextChanged += TxtSearchAchiev_TextChanged;
            txtSearchAchiev.KeyDown += TxtSearchAchiev_KeyDown;

            dgvAchievements.AutoGenerateColumns = false;
            dgvAchievements.DataSourceChanged += DgvAchievements_DataSourceChanged;
            dgvAchievements.CellPainting += DgvAchievements_CellPainting;

            await GameInfo_Shown(null, null);
        }

        private static Task GameInfo_Shown(object sender, EventArgs e)
        {
            RASite.DLGameExtend.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
            RASite.DLGameExtendImages.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
            HideDownloadControls();
            return Task.CompletedTask;
        }

        private static void HideDownloadControls()
        {
            lblUpdateInfo.Text = string.Empty;
            lblProgressInfo.Text = string.Empty;
            pgbInfo.Value = 0;
            pgbInfo.Visible = false;
        }

        private static async Task LoadSelectedGame()
        {
            if (Session.GameSelected.IsNull())
            {
                return;
            }

            if (Session.GameExtendSelected.NotNull() && Session.GameSelected.ID == Session.GameExtendSelected.ID)
            {
                Page.tabMain.SelectedTab = Page.tabGameInfo;
                return;
            }

            HideDownloadControls();

            pnlInfoScroll.AutoScrollPosition = new Point(pnlInfoScroll.AutoScrollPosition.X, 0);
            pnlInfoScroll.VerticalScroll.Value = 0;

            // var emptyImage = RA.ErrorIcon;
            // picInfoIcon.Image = emptyImage;
            // picInfoTitle.Image = emptyImage;
            // picInfoInGame.Image = emptyImage;
            // picInfoBoxArt.Image = emptyImage;
            // dgvAchievements.DataSource = null;
            LoadGameExtendBase();
            await LoadGameExtend();

            Page.tabMain.SelectedTab = Page.tabGameInfo;

            // Update GameExtend
            if (Session.GameExtendSelected.IsNull() || Session.GameExtendSelected.ConsoleID == 0)
            {
                BtnUpdateInfo_Click(null, null);
            }

            dgvAchievements.Focus();
        }

        private static void LoadGameExtendBase()
        {
            lblInfoName.Text = Session.GameSelected.Title + " (" + Session.GameSelected.ConsoleName + ")";
            Session.GameSelected.SetImageIconBitmap();
            picInfoIcon.Image = Session.GameSelected.ImageIconBitmap;

            lblInfoAchievements.Text = Session.GameSelected.NumAchievements.ToString() + " Trophies: " + Session.GameSelected.Points + " points";
        }

        private static async Task LoadGameExtend()
        {
            if (Session.GameSelected.IsNull())
            {
                return;
            }

            Session.GameExtendSelected = await GameExtend.Find(Session.GameSelected.ID);

            lblInfoDeveloper1.Text = Session.GameExtendSelected.Developer;
            lblInfoPublisher1.Text = Session.GameExtendSelected.Publisher;
            lblInfoGenre1.Text = Session.GameExtendSelected.Genre;
            lblInfoReleased1.Text = Session.GameExtendSelected.Released;

            Session.GameExtendSelected.SetImagesBitmap();

            // Scale Boxes
            {
                picInfoTitle.Image = Session.GameExtendSelected.ImageTitleBitmap;
                picInfoInGame.Image = Session.GameExtendSelected.ImageIngameBitmap;

                var bigHeight = picInfoTitle.Height > picInfoInGame.Height ? picInfoTitle.Height : picInfoInGame.Height;
                var picMargin = 12;

                pnlInfoImages.Height = bigHeight + picMargin;

                pnlInfoBoxArt.Height = (pnlInfoImages.Location.Y + pnlInfoImages.Height) - pnlInfoBoxArt.Location.Y;
                picInfoBoxArt.MaximumSize = new Size(pnlInfoBoxArt.Width - picMargin, pnlInfoBoxArt.Height - picMargin);
                picInfoBoxArt.Image = Session.GameExtendSelected.ImageBoxArtBitmap;

                gpbInfo.Height = gpbInfo.PreferredSize.Height - 13;
                gpbInfoAchievements.Location = new Point(gpbInfoAchievements.Location.X, (gpbInfo.Height - pnlInfoScroll.VerticalScroll.Value) + 9);
            }

            var lstCheevos = new ListBind<Achievement>();
            dgvAchievements.DataSource = lstCheevos;

            if (File.Exists(Session.GameSelected.ExtendFile.Path))
            {
                var allText = File.ReadAllText(Session.GameSelected.ExtendFile.Path);
                var cheevos = allText.GetBetween("\"Achievements\":{", "}}");
                cheevos = "{" + cheevos + "}";

                var jcheevos = Json.DeserializeObject<JToken>(cheevos);
                Session.GameExtendSelected.SetAchievements(jcheevos);

                lstCheevos = new ListBind<Achievement>(Session.GameExtendSelected.AchievementsList);
                dgvAchievements.DataSource = lstCheevos;
            }

            lstAchievs = lstCheevos;
        }

        private static async void BtnUpdateInfo_Click(object sender, EventArgs e)
        {
            // Download GameExtend
            if (Session.GameSelected.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            btnUpdateInfo.Enabled = false;
            txtSearchAchiev.Enabled = false;

            // Download GameExtend
            await ra.DownloadGameExtend(Session.GameSelected, RASite.DLGameExtend);

            // Download game images
            await ra.DownloadGameExtendImages(Session.GameSelected);

            // Load Game
            await LoadGameExtend();
            btnUpdateInfo.Enabled = true;
            txtSearchAchiev.Enabled = true;
            pnlInfoScroll.Focus();

            MainCommon.WriteOutput("[" + DateTime.Now.ToLongTimeString() + "] Game " + Session.GameSelected.ID + " Updated!");
        }

        private static void GameInfo_Login()
        {
            btnHashes.Enabled = Session.RALogged;
        }

        private static void OnButtonGamePageClicked(object sender, EventArgs e)
        {
            if (Session.GameSelected.IsNull())
            {
                return;
            }

            Process.Start(RA.Game_URL(Session.GameSelected.ID));
        }

        private static async void OnButtonHashesClicked(object sender, EventArgs e)
        {
            if (Session.GameSelected.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            await HashViewerCommon.Open(Session.GameSelected);
        }

        private static void TxtSearchAchiev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dgvAchievements.Focus();
            }
        }

        private static void TxtSearchAchiev_TextChanged(object sender, EventArgs e)
        {
            var newSearch = new ListBind<Achievement>();
            foreach (Achievement obj in lstAchievs)
            {
                bool title = obj.Title != null && (obj.Title.IndexOf(txtSearchAchiev.Text, StringComparison.CurrentCultureIgnoreCase) > -1);
                bool desc = obj.Description != null && (obj.Description.IndexOf(txtSearchAchiev.Text, StringComparison.CurrentCultureIgnoreCase) > -1);

                if (title || desc)
                {
                    newSearch.Add(obj);
                }
            }

            int scrollPosition = pnlInfoScroll.VerticalScroll.Value;

            lstAchievsSearch = newSearch;
            dgvAchievements.DataSource = lstAchievsSearch;

            if (scrollPosition < gpbInfo.Height + 4)
            {
                scrollPosition = gpbInfo.Height + 4;
            }

            if (scrollPosition > pnlInfoScroll.VerticalScroll.Maximum)
            {
                scrollPosition = pnlInfoScroll.VerticalScroll.Maximum;
            }

            bool maintainScroll = true;
            if (maintainScroll)
            {
                bool txtFocus = txtSearchAchiev.Focused;

                pnlInfoScroll.VerticalScroll.Value = scrollPosition;

                if (txtFocus)
                {
                    txtSearchAchiev.Focus();
                }
            }

            dgvAchievements.Refresh();
        }

        private static void DgvAchievements_DataSourceChanged(object sender, EventArgs e)
        {
            dgvAchievements.Height = dgvAchievements.PreferredSize.Height - 16;
            gpbInfoAchievements.Height = gpbInfoAchievements.PreferredSize.Height - 13;
        }

        private static void DgvAchievements_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowHeader() || e.Value == null || e.ColumnIndex != 2)
            {
                return;
            }

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
                    {
                        e.Graphics.DrawString(ach.Title, fnt, cellForeBrush, rect1);
                    }

                    using (Brush cellForeBrush2 = new SolidBrush(Theme.CheevoDescription))
                    {
                        e.Graphics.DrawString(Environment.NewLine + ach.Description, fnt, cellForeBrush2, rect1);
                    }
                }
            }
        }
        #endregion
    }
}
