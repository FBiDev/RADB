using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public static partial class MainGameToPlay
    {
        static RA RA = new RA();
        static ListBind<Game> lstGamesToPlay = new ListBind<Game>();

        #region GamesToPlay
        public async static Task GamesToPlay_Init()
        {
            BIND.OnTabMainChanged += () => { if (BIND.SelectedTab == form.tabGamesToPlay) { dgvGamesToPlay.Focus(); } };
            BIND.OnGameListChanged += LoadGamesToPlay;
            BIND.OnAddGamesToPlay += (game) =>
            {
                lstGamesToPlay.Insert(0, game);
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.IsEmpty();
                return true;
            };

            mniRemoveGameToPlay.MouseDown += mniRemoveGameToPlay_MouseDown;

            dgvGamesToPlay.AutoGenerateColumns = false;
            //dgvGamesToPlay.DataSource = lstGamesToPlay;

            dgvGamesToPlay.Columns.Format(CellStyle.StringCenter, 0);
            dgvGamesToPlay.Columns.Format(CellStyle.Image, 1);
            dgvGamesToPlay.Columns.Format(CellStyle.NumberCenter, 4, 5, 6, 7);
            dgvGamesToPlay.Columns.Format(CellStyle.DateCenter, 8);

            dgvGamesToPlay.MouseDown += (sender, e) => dgvGamesToPlay.ShowContextMenu(e, mnuGamesToPlay);
            dgvGamesToPlay.CellDoubleClick += MainCommon.ChangeBindGame;

            dgvGamesToPlay.DataSourceChanged += LoadGamesToPlayIcons;
            dgvGamesToPlay.Sorted += LoadGamesToPlayIcons;
            dgvGamesToPlay.MouseWheel += dgvGamesToPlay_MouseWheel;
            dgvGamesToPlay.Scroll += dgvGamesToPlay_Scroll;

            BIND.lstDgvGames.Add(dgvGamesToPlay);

            await GamesToPlay_Shown(null, null);
        }

        static async Task GamesToPlay_Shown(object sender, EventArgs e)
        {
            await LoadGamesToPlay();
        }

        static async Task LoadGamesToPlay()
        {
            lstGamesToPlay = new ListBind<Game>(await Game.ListToPlay());
            dgvGamesToPlay.DataSource = lstGamesToPlay;

            lblNotFoundGamesToPlay.Visible = lstGamesToPlay.IsEmpty();
        }

        static async void LoadGamesToPlayIcons(object sender, EventArgs e)
        {
            await MainCommon.LoadGridIcons(dgvGamesToPlay);
        }

        static int gamesToPlayWheelCounter;
        static void dgvGamesToPlay_MouseWheel(object sender, MouseEventArgs e) { gamesToPlayWheelCounter = 1; }
        static void dgvGamesToPlay_Scroll(object sender, EventArgs e)
        {
            if (gamesToPlayWheelCounter > 0 && gamesToPlayWheelCounter < 3) { gamesToPlayWheelCounter++; return; }
            gamesToPlayWheelCounter = 0;
            LoadGamesToPlayIcons(sender, e);
        }

        static async void mniRemoveGameToPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGamesToPlay.GetSelectedItem<Game>();

            if (await game.DeleteFromPlay())
            {
                lstGamesToPlay.Remove(game);
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.IsEmpty();

                if (BIND.Console.NotNull() && (BIND.Console.ID == game.ConsoleID || BIND.Console.ID == 0))
                {
                    BIND.AddGames(game);
                }
            }
        }
        #endregion
    }
}
