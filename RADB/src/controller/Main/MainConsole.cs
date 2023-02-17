using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public static partial class MainConsole
    {
        static RA RA = new RA();
        static ListBind<Console> lstConsoles = new ListBind<Console>();

        #region Consoles
        public static async Task Console_Init()
        {
            BIND.OnGameListChanged += UpdateConsoleAllGames;
            BIND.OnTabMainChanged += () => { if (BIND.SelectedTab == form.tabConsoles) { dgvConsoles.Focus(); } };

            //form.Shown += Console_Shown;
            mniMergeGamesIcon.MouseDown += mniMergeGamesIcon_MouseDown;
            mniMergeGamesIconBadSize.MouseDown += mniMergeGamesIconBadSize_MouseDown;

            btnUpdateConsoles.Click += btnUpdateConsoles_Click;

            dgvConsoles.AutoGenerateColumns = true;
            //dgvConsoles.DataSource = lstConsoles;

            dgvConsoles.Columns.Format(CellStyle.StringCenter, 0);
            dgvConsoles.Columns.Format(CellStyle.NumberCenter, 3, 4);

            dgvConsoles.MouseDown += (sender, e) => dgvConsoles.ShowContextMenu(e, mnuConsoles);
            dgvConsoles.CellDoubleClick += dgvConsoles_CellDoubleClick;
            dgvConsoles.KeyPress += dgvConsoles_KeyPress;
            dgvConsoles.KeyDown += dgvConsoles_KeyDown;

            await Console_Shown(null, null);
        }

        static async Task Console_Shown(object sender, EventArgs e)
        {
            Browser.dlConsoles.SetControls(lblProgressConsoles, pgbConsoles, lblUpdateConsoles);
            Browser.dlConsolesGamesIcon.SetControls(lblProgressConsoles, pgbConsoles, lblUpdateConsoles);

            ResetConsolesLabels();
            await LoadConsoles();
        }

        static Task UpdateConsoleAllGames()
        {
            var console = lstConsoles.First(x => x.Name == "All Games");
            console.NumGames = lstConsoles.Except(new[] { console }).Sum(x => x.NumGames);
            console.TotalGames = lstConsoles.Except(new[] { console }).Sum(x => x.TotalGames);
            return Task.FromResult(0);
        }

        static void DisablePanelConsoles()
        {
            pnlDownloadConsoles.Enabled = false;
            lblNotFoundConsoles.Visible = false;
            picLoaderConsole.Visible = true;
            dgvConsoles.Enabled = false;
        }

        static void EnablePanelConsoles()
        {
            pnlDownloadConsoles.Enabled = true;
            lblNotFoundConsoles.Visible = (dgvConsoles.RowCount == 0);
            picLoaderConsole.Visible = false;
            dgvConsoles.Enabled = true;
        }

        static void ResetConsolesLabels()
        {
            lblUpdateConsoles.Text = string.Empty;
            lblProgressConsoles.Text = string.Empty;
            pgbConsoles.Value = 0;
            pgbConsoles.Visible = false;
        }

        static async Task LoadConsoles()
        {
            DisablePanelConsoles();

            //Not Block UI
            //await Task.Run(async () => { lstConsoles = new ListBind<Console>(await Console.List()); });
            lstConsoles = new ListBind<Console>(await Console.List());

            dgvConsoles.DataSource = lstConsoles;

            EnablePanelConsoles();

            if (lstConsoles.Empty())
            {
                btnUpdateConsoles_Click(null, null);
            }

            dgvConsoles.Focus();
        }

        static async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            DisablePanelConsoles();
            lstConsoles.Clear();
            await RA.DownloadConsoles();
            await LoadConsoles();
            EnablePanelConsoles();

            MainCommon.WriteOutput("[" + DateTime.Now.ToTimeLong() + "] " + "Consoles Updated!");
        }

        static void dgvConsoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            Console ConsoleSelected = dgvConsoles.GetSelectedItem<Console>();
            if (BIND.Console.IsNull() || ConsoleSelected.ID != BIND.Console.ID)
                BIND.Console = ConsoleSelected;
        }

        static void dgvConsoles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                dgvConsoles_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
            }
        }

        static void dgvConsoles_KeyPress(object sender, KeyPressEventArgs e)
        {
            MainCommon.GridViewKeyPress(sender, e, "cName");
        }

        static async void mniMergeGamesIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var console = dgvConsoles.GetSelectedItem<Console>();

            DisablePanelConsoles();
            await RA.MergeGamesIcon(console);
            EnablePanelConsoles();
        }

        static async void mniMergeGamesIconBadSize_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var console = dgvConsoles.GetSelectedItem<Console>();

            DisablePanelConsoles();
            await RA.MergeGamesIcon(console, true);
            EnablePanelConsoles();
        }
        #endregion
    }
}
