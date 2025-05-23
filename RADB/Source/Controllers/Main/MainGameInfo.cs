﻿using System;
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
        static RA RA = new RA();
        static ListBind<Achievement> lstAchievs = new ListBind<Achievement>();
        static ListBind<Achievement> lstAchievsSearch = new ListBind<Achievement>();

        #region GameInfo
        public static async Task GameInfo_Init()
        {
            Session.OnRALoggedChanged += GameInfo_Login;
            Session.OnGameChanged += LoadSelectedGame;
            Session.OnTabMainChanged += () => { if (Session.SelectedTab == form.tabGameInfo) { pnlInfoScroll.Focus(); } };

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
            RASite.dlGameExtend.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
            RASite.dlGameExtendImages.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
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
            if (Session.Game.IsNull()) { return; }
            if (Session.GameExtend.NotNull() && Session.Game.ID == Session.GameExtend.ID)
            {
                form.tabMain.SelectedTab = form.tabGameInfo;
                return;
            }

            HideDownloadControls();

            pnlInfoScroll.AutoScrollPosition = new Point(pnlInfoScroll.AutoScrollPosition.X, 0);
            pnlInfoScroll.VerticalScroll.Value = 0;

            //var emptyImage = RA.ErrorIcon;
            //picInfoIcon.Image = emptyImage;
            //picInfoTitle.Image = emptyImage;
            //picInfoInGame.Image = emptyImage;
            //picInfoBoxArt.Image = emptyImage;
            //dgvAchievements.DataSource = null;

            LoadGameExtendBase();
            await LoadGameExtend();

            form.tabMain.SelectedTab = form.tabGameInfo;

            //Update GameExtend
            if (Session.GameExtend.IsNull() || Session.GameExtend.ConsoleID == 0)
            {
                btnUpdateInfo_Click(null, null);
            }

            dgvAchievements.Focus();
        }

        static void LoadGameExtendBase()
        {
            lblInfoName.Text = Session.Game.Title + " (" + Session.Game.ConsoleName + ")";
            Session.Game.SetImageIconBitmap();
            picInfoIcon.Image = Session.Game.ImageIconBitmap;

            lblInfoAchievements.Text = Session.Game.NumAchievements.ToString() + " Trophies: " + Session.Game.Points + " points";
        }

        static async Task LoadGameExtend()
        {
            if (Session.Game.IsNull()) { return; }

            Session.GameExtend = await GameExtend.Find(Session.Game.ID);

            lblInfoDeveloper1.Text = Session.GameExtend.Developer;
            lblInfoPublisher1.Text = Session.GameExtend.Publisher;
            lblInfoGenre1.Text = Session.GameExtend.Genre;
            lblInfoReleased1.Text = Session.GameExtend.Released;

            Session.GameExtend.SetImagesBitmap();

            {//Scale Boxes
                picInfoTitle.Image = Session.GameExtend.ImageTitleBitmap;
                picInfoInGame.Image = Session.GameExtend.ImageIngameBitmap;

                var bigHeight = picInfoTitle.Height > picInfoInGame.Height ? picInfoTitle.Height : picInfoInGame.Height;
                var picMargin = 12;

                pnlInfoImages.Height = bigHeight + picMargin;

                pnlInfoBoxArt.Height = (pnlInfoImages.Location.Y + pnlInfoImages.Height) - pnlInfoBoxArt.Location.Y;
                picInfoBoxArt.MaximumSize = new Size(pnlInfoBoxArt.Width - picMargin, pnlInfoBoxArt.Height - picMargin);
                picInfoBoxArt.Image = Session.GameExtend.ImageBoxArtBitmap;

                gpbInfo.Height = gpbInfo.PreferredSize.Height - 13;
                gpbInfoAchievements.Location = new Point(gpbInfoAchievements.Location.X, (gpbInfo.Height - pnlInfoScroll.VerticalScroll.Value) + 9);
            }

            var lstCheevos = new ListBind<Achievement>();
            dgvAchievements.DataSource = lstCheevos;

            if (File.Exists(Session.Game.ExtendFile.Path))
            {
                var AllText = File.ReadAllText(Session.Game.ExtendFile.Path);
                var cheevos = AllText.GetBetween("\"Achievements\":{", "}}");
                cheevos = "{" + cheevos + "}";

                var jcheevos = Json.DeserializeObject<JToken>(cheevos);
                Session.GameExtend.SetAchievements(jcheevos);

                lstCheevos = new ListBind<Achievement>(Session.GameExtend.AchievementsList);
                dgvAchievements.DataSource = lstCheevos;
            }
            lstAchievs = lstCheevos;
        }

        static async void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Download GameExtend
            if (Session.Game.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            btnUpdateInfo.Enabled = false;
            txtSearchAchiev.Enabled = false;
            //Download GameExtend
            await RA.DownloadGameExtend(Session.Game, RASite.dlGameExtend);

            //Download game images
            await RA.DownloadGameExtendImages(Session.Game);

            //Load Game
            await LoadGameExtend();
            btnUpdateInfo.Enabled = true;
            txtSearchAchiev.Enabled = true;
            pnlInfoScroll.Focus();

            MainCommon.WriteOutput("[" + DateTime.Now.ToLongTimeString() + "] Game " + Session.Game.ID + " Updated!");
        }

        static void GameInfo_Login()
        {
            btnHashes.Enabled = Session.RALogged;
        }

        static void OnButtonGamePageClicked(object sender, EventArgs e)
        {
            if (Session.Game.IsNull()) { return; }
            Process.Start(RA.Game_URL(Session.Game.ID));
        }

        static async void OnButtonHashesClicked(object sender, EventArgs e)
        {
            if (Session.Game.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            await HashViewerCommon.Open(Session.Game);
        }

        static void txtSearchAchiev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                dgvAchievements.Focus();
        }

        static void txtSearchAchiev_TextChanged(object sender, EventArgs e)
        {
            var newSearch = new ListBind<Achievement>();
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
