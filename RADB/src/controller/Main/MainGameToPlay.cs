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
            BIND.OnAddGamesToPlay += (game) =>
            {
                lstGamesToPlay.Insert(0, game);
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
                return true;
            };

            mniRemoveGameToPlay.MouseDown += mniRemoveGameToPlay_MouseDown;

            dgvGamesToPlay.AutoGenerateColumns = false;
            dgvGamesToPlay.DataSource = lstGamesToPlay;

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
        }

        static async void mniRemoveGameToPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGamesToPlay.GetSelectedItem<Game>();

            if (await game.DeleteFromPlay())
            {
                lstGamesToPlay.Remove(game);
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();

                if (BIND.Console.NotNull() && BIND.Console.ID == game.ConsoleID)
                {
                    BIND.AddGames(game);
                }
            }
        }
        #endregion
    }
}
