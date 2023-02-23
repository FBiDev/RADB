using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public static partial class MainGameToHide
    {
        static RA RA = new RA();
        static ListBind<Game> lstGamesToHide = new ListBind<Game>();

        #region GamesToHide
        public static async Task GamesToHide_Init()
        {
            BIND.OnTabMainChanged += () => { if (BIND.SelectedTab == form.tabGamesToHide) { dgvGamesToHide.Focus(); } };
            BIND.OnGameListChanged += LoadGamesToHide;
            BIND.OnAddGamesToHide += (game) =>
            {
                lstGamesToHide.Insert(0, game);
                lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
                return true;
            };

            mniRemoveGameToHide.MouseDown += mniRemoveGameToHide_MouseDown;

            dgvGamesToHide.AutoGenerateColumns = false;
            //dgvGamesToHide.DataSource = lstGamesToHide;

            dgvGamesToHide.Columns.Format(CellStyle.StringCenter, 0);
            dgvGamesToHide.Columns.Format(CellStyle.Image, 1);
            dgvGamesToHide.Columns.Format(CellStyle.NumberCenter, 4, 5, 6);
            dgvGamesToHide.Columns.Format(CellStyle.DateCenter, 7);

            dgvGamesToHide.MouseDown += (sender, e) => dgvGamesToHide.ShowContextMenu(e, mnuGamesToHide);
            dgvGamesToHide.CellDoubleClick += MainCommon.ChangeBindGame;

            dgvGamesToHide.DataSourceChanged += LoadGamesToHideIcons;
            dgvGamesToHide.Sorted += LoadGamesToHideIcons;
            dgvGamesToHide.MouseWheel += dgvGamesToHide_MouseWheel;
            dgvGamesToHide.Scroll += dgvGamesToHide_Scroll;

            BIND.lstDgvGames.Add(dgvGamesToHide);

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

            lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
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

            var game = dgvGamesToHide.GetSelectedItem<Game>();

            if (await game.DeleteFromHide())
            {
                lstGamesToHide.Remove(game);
                lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();

                if (BIND.Console.NotNull() && (BIND.Console.ID == game.ConsoleID || BIND.Console.ID == 0))
                {
                    BIND.AddGames(game);
                }
            }
        }
        #endregion
    }
}
