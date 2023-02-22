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
            //f.Shown += GamesToHide_Shown;
            mniRemoveGameToHide.MouseDown += mniRemoveGameToHide_MouseDown;

            dgvGamesToHide.AutoGenerateColumns = false;
            dgvGamesToHide.DataSource = lstGamesToHide;

            dgvGamesToHide.Columns.Format(CellStyle.StringCenter, 0);
            dgvGamesToHide.Columns.Format(CellStyle.Image, 1);
            dgvGamesToHide.Columns.Format(CellStyle.NumberCenter, 4, 5, 6);
            dgvGamesToHide.Columns.Format(CellStyle.DateCenter, 7);

            dgvGamesToHide.MouseDown += (sender, e) => dgvGamesToHide.ShowContextMenu(e, mnuGamesToHide);
            dgvGamesToHide.CellDoubleClick += MainCommon.ChangeBindGame;
            //dgvGamesToHide.MouseWheel += dgvGames_MouseWheel;
            //dgvGamesToHide.Scroll += dgvGames_Scroll;
            //dgvGamesToHide.Sorted += dgvGames_Sorted;

            BIND.lstDgvGames.Add(dgvGamesToHide);

            await GamesToHide_Shown(null, null);
        }

        static async Task GamesToHide_Shown(object sender, EventArgs e)
        {
            await LoadGamesToHide();
        }

        static async Task LoadGamesToHide()
        {
            lstGamesToHide.Clear();
            lstGamesToHide.AddRange(await Game.ListToHide());

            lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
            dgvGamesToHide.Refresh();
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
