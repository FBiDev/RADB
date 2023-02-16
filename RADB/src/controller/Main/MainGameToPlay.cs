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

            mniRemoveGameToPlay.MouseDown += mniRemoveGameToPlay_MouseDown;

            dgvGamesToPlay.AutoGenerateColumns = false;
            dgvGamesToPlay.DataSource = lstGamesToPlay;

            //dgvGamesToPlay.CellPainting += MainCommon.GridViewAdjustImageQuality;
            dgvGamesToPlay.MouseDown += (sender, e) => dgvGamesToPlay.ShowContextMenu(e, mnuGamesToPlay);

            dgvGamesToPlay.CellDoubleClick += MainCommon.ChangeBindGame;
            //dgvGamesToPlay.MouseWheel += dgvGames_MouseWheel;
            //dgvGamesToPlay.Scroll += dgvGames_Scroll;
            //dgvGamesToPlay.Sorted += dgvGames_Sorted;

            BIND.lstDgvGames.Add(dgvGamesToPlay);

            await GamesToPlay_Shown(null, null);
        }

        static async Task GamesToPlay_Shown(object sender, EventArgs e)
        {
            await LoadGamesToPlay();
        }

        static async Task LoadGamesToPlay()
        {
            lstGamesToPlay.Clear();
            lstGamesToPlay.AddRange(await Game.ListToPlay());
            lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
            //await MainCommon.LoadGamesIcon();
        }

        static async void mniRemoveGameToPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGamesToPlay.GetSelectedItem<Game>();

            if (await game.DeleteFromPlay())
            {
                if (BIND.Console.NotNull() && BIND.Console.ID == game.ConsoleID)
                {
                    //lstGames.Insert(0, game);
                    //lstGamesSearch.Insert(0, game);
                }

                lstGamesToPlay.Remove(game);
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
                //await LoadGamesIcon();
            }
        }
        #endregion
    }
}
