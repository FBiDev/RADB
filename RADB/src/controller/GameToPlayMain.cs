using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public partial class GameToPlayMain
    {
        public ListBind<Game> lstGamesToPlay = new ListBind<Game>();

        public GameToPlayMain()
        {
            f = BIND.f;
            GamesToPlay_Init();
        }

        #region GamesToPlay
        void GamesToPlay_Init()
        {
            f.Shown += GamesToPlay_Shown;
            mniRemoveGameToPlay.MouseDown += mniRemoveGameToPlay_MouseDown;
            BIND.OnTabMainChanged += () => { if (BIND.SelectedTab == f.tabConsoles) { dgvGamesToPlay.Focus(); } };

            dgvGamesToPlay.AutoGenerateColumns = false;
            dgvGamesToPlay.DataSource = lstGamesToPlay;

            //dgvGamesToPlay.CellPainting += dgvGames_CellPainting;
            dgvGamesToPlay.MouseDown += (sender, e) => dgvGamesToPlay.ShowContextMenu(e, mnuGamesToPlay);

            //dgvGamesToPlay.CellDoubleClick += dgvGames_CellDoubleClick;
            //dgvGamesToPlay.MouseWheel += dgvGames_MouseWheel;
            //dgvGamesToPlay.Scroll += dgvGames_Scroll;
            //dgvGamesToPlay.Sorted += dgvGames_Sorted;

            BIND.lstDgvGames.Add(dgvGamesToPlay);
        }

        async void GamesToPlay_Shown(object sender, EventArgs e)
        {
            await LoadGamesToPlay();
        }

        async Task LoadGamesToPlay()
        {
            lstGamesToPlay.Clear();
            lstGamesToPlay.AddRange(await Game.ListToPlay());
            lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
            //await LoadGamesIcon();
        }

        async void mniRemoveGameToPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGetCurrentItem<Game>(dgvGamesToPlay);

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

        #region Common
        T dgvGetCurrentItem<T>(object sender) where T : class
        {
            return MainLogic.dgvGetCurrentItem<T>(sender);
        }

        void dgv_KeyPress(object sender, KeyPressEventArgs e, string columnName)
        {
            MainLogic.dgv_KeyPress(sender, e, columnName);
        }
        #endregion
    }
}
