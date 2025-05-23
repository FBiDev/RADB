﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public static partial class MainGameToHide
    {
        static RA RA = new RA();
        static ListBind<Game> lstGamesToHide = new ListBind<Game>();

        #region GamesToHide
        public static async Task GamesToHide_Init()
        {
            Session.OnTabMainChanged += () => { if (Session.SelectedTab == form.tabGamesToHide) { dgvGamesToHide.Focus(); } };
            Session.OnGameListChanged += LoadGamesToHide;
            Session.OnAddGamesToHide += (game) =>
            {
                lstGamesToHide.Insert(0, game);
                lblNotFoundGamesToHide.Visible = lstGamesToHide.IsEmpty();
                return true;
            };

            mniRemoveGameToHide.MouseDown += mniRemoveGameToHide_MouseDown;

            dgvGamesToHide.AutoGenerateColumns = false;
            //dgvGamesToHide.DataSource = lstGamesToHide;

            dgvGamesToHide.Columns.Format(ColumnFormat.StringCenter, cols: 0);
            dgvGamesToHide.Columns.Format(ColumnFormat.Image, cols: 1);
            dgvGamesToHide.Columns.Format(ColumnFormat.NumberCenter, cols: new[] { 4, 5, 6, 7 });
            dgvGamesToHide.Columns.Format(ColumnFormat.DateCenter, cols: 8);

            dgvGamesToHide.MouseDown += (sender, e) => dgvGamesToHide.ShowContextMenu(e, mnuGamesToHide);
            dgvGamesToHide.CellDoubleClick += MainCommon.ChangeBindGame;

            dgvGamesToHide.DataSourceChanged += LoadGamesToHideIcons;
            dgvGamesToHide.Sorted += LoadGamesToHideIcons;
            dgvGamesToHide.MouseWheel += dgvGamesToHide_MouseWheel;
            dgvGamesToHide.Scroll += dgvGamesToHide_Scroll;

            Session.lstDgvGames.Add(dgvGamesToHide);

            await GamesToHide_Shown(null, null);
        }

        static async Task GamesToHide_Shown(object sender, EventArgs e)
        {
            await LoadGamesToHide();
        }

        static async Task LoadGamesToHide()
        {
            lstGamesToHide = new ListBind<Game>(await Game.ListToHide());
            dgvGamesToHide.DataSource = lstGamesToHide;

            lblNotFoundGamesToHide.Visible = lstGamesToHide.IsEmpty();
        }

        static async void LoadGamesToHideIcons(object sender, EventArgs e)
        {
            await MainCommon.LoadGridIcons(dgvGamesToHide);
        }

        static int gamesToHideWheelCounter;
        static void dgvGamesToHide_MouseWheel(object sender, MouseEventArgs e) { gamesToHideWheelCounter = 1; }
        static void dgvGamesToHide_Scroll(object sender, EventArgs e)
        {
            if (gamesToHideWheelCounter > 0 && gamesToHideWheelCounter < 3) { gamesToHideWheelCounter++; return; }
            gamesToHideWheelCounter = 0;
            LoadGamesToHideIcons(sender, e);
        }

        static async void mniRemoveGameToHide_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGamesToHide.GetCurrentRowObject<Game>();

            if (await game.DeleteFromHide())
            {
                lstGamesToHide.Remove(game);
                lblNotFoundGamesToHide.Visible = lstGamesToHide.IsEmpty();

                if (Session.Console.NotNull() && (Session.Console.ID == game.ConsoleID || Session.Console.ID == 0))
                {
                    Session.AddGames(game);
                }
            }
        }
        #endregion
    }
}
