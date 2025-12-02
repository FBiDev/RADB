using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public static partial class MainGameToPlay
    {
        private static RA ra = new RA();
        private static ListBind<Game> lstGamesToPlay = new ListBind<Game>();
        private static int gamesToPlayWheelCounter;

        #region GamesToPlay
        public static async Task GamesToPlay_Init()
        {
            Session.OnTabMainChanged += () =>
            {
                if (Session.SelectedTab == Page.tabGamesToPlay)
                {
                    dgvGamesToPlay.Focus();
                }
            };
            Session.OnGameListChanged += LoadGamesToPlay;
            Session.OnAddGamesToPlay += (game) =>
            {
                lstGamesToPlay.Insert(0, game);
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.IsEmpty();
                return true;
            };

            mniRemoveGameToPlay.MouseDown += MniRemoveGameToPlay_MouseDown;

            dgvGamesToPlay.AutoGenerateColumns = false;

            // dgvGamesToPlay.DataSource = lstGamesToPlay;
            dgvGamesToPlay.Columns.Format(ColumnFormat.StringCenter, cols: 0);
            dgvGamesToPlay.Columns.Format(ColumnFormat.Image, cols: 1);
            dgvGamesToPlay.Columns.Format(ColumnFormat.NumberCenter, cols: new[] { 4, 5, 6, 7 });
            dgvGamesToPlay.Columns.Format(ColumnFormat.DateCenter, cols: 8);

            dgvGamesToPlay.MouseDown += (sender, e) => dgvGamesToPlay.ShowContextMenu(e, mnuGamesToPlay);
            dgvGamesToPlay.CellDoubleClick += MainCommon.ChangeBindGame;

            dgvGamesToPlay.DataSourceChanged += LoadGamesToPlayIcons;
            dgvGamesToPlay.Sorted += LoadGamesToPlayIcons;
            dgvGamesToPlay.MouseWheel += DgvGamesToPlay_MouseWheel;
            dgvGamesToPlay.Scroll += DgvGamesToPlay_Scroll;

            Session.MainGameList.Add(dgvGamesToPlay);

            await GamesToPlay_Shown(null, null);
        }

        private static async Task GamesToPlay_Shown(object sender, EventArgs e)
        {
            await LoadGamesToPlay();
        }

        private static async Task LoadGamesToPlay()
        {
            lstGamesToPlay = new ListBind<Game>(await Game.ListToPlay());
            dgvGamesToPlay.DataSource = lstGamesToPlay;

            lblNotFoundGamesToPlay.Visible = lstGamesToPlay.IsEmpty();
        }

        private static async void LoadGamesToPlayIcons(object sender, EventArgs e)
        {
            await MainCommon.LoadGridIcons(dgvGamesToPlay);
        }

        private static void DgvGamesToPlay_MouseWheel(object sender, MouseEventArgs e)
        {
            gamesToPlayWheelCounter = 1;
        }

        private static void DgvGamesToPlay_Scroll(object sender, EventArgs e)
        {
            if (gamesToPlayWheelCounter > 0 && gamesToPlayWheelCounter < 3)
            {
                gamesToPlayWheelCounter++;
                return;
            }

            gamesToPlayWheelCounter = 0;
            LoadGamesToPlayIcons(sender, e);
        }

        private static async void MniRemoveGameToPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var game = dgvGamesToPlay.GetCurrentRowObject<Game>();

            if (await game.DeleteFromPlay())
            {
                lstGamesToPlay.Remove(game);
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.IsEmpty();

                if (Session.ConsoleSelected.NotNull() && (Session.ConsoleSelected.ID == game.ConsoleID || Session.ConsoleSelected.ID == 0))
                {
                    Session.AddGames(game);
                }
            }
        }
        #endregion
    }
}
