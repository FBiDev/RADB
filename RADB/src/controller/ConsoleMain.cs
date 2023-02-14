using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public partial class ConsoleMain
    {
        RA RA = new RA();
        ListBind<Console> lstConsoles = new ListBind<Console>();

        #region Consoles
        public ConsoleMain()
        {
            f = BIND.f;
            Consoles_Init();
        }

        void Consoles_Init()
        {
            BIND.OnGameListChanged += UpdateConsoleAllGames;
            BIND.OnTabMainChanged += () => { if (BIND.SelectedTab == f.tabConsoles) { dgvConsoles.Focus(); } };

            f.Shown += Consoles_Shown;
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
        }

        async void Consoles_Shown(object sender, EventArgs e)
        {
            Browser.dlConsoles.SetControls(lblProgressConsoles, pgbConsoles, lblUpdateConsoles);
            Browser.dlConsolesGamesIcon.SetControls(lblProgressConsoles, pgbConsoles, lblUpdateConsoles);

            ResetConsolesLabels();
            await LoadConsoles();
        }

        Task UpdateConsoleAllGames()
        {
            var console = lstConsoles.Where(x => x.Name == "All Games");
            console.First().NumGames = lstConsoles.Except(console).Sum(x => x.NumGames);
            console.First().TotalGames = lstConsoles.Except(console).Sum(x => x.TotalGames);
            return null;
        }

        void DisablePanelConsoles()
        {
            pnlDownloadConsoles.Enabled = false;
            lblNotFoundConsoles.Visible = false;
            picLoaderConsole.Visible = true;
            dgvConsoles.Enabled = false;
        }

        void EnablePanelConsoles()
        {
            pnlDownloadConsoles.Enabled = true;
            lblNotFoundConsoles.Visible = (dgvConsoles.RowCount == 0);
            picLoaderConsole.Visible = false;
            dgvConsoles.Enabled = true;
        }

        void ResetConsolesLabels()
        {
            lblUpdateConsoles.Text = string.Empty;
            lblProgressConsoles.Text = string.Empty;
            pgbConsoles.Value = 0;
            pgbConsoles.Visible = false;
        }

        async Task LoadConsoles()
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

        async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            DisablePanelConsoles();
            lstConsoles.Clear();
            await RA.DownloadConsoles();
            await LoadConsoles();
            EnablePanelConsoles();

            f.lblOutput.Text = "[" + DateTime.Now.ToTimeLong() + "] " + "Consoles Updated!" + Environment.NewLine + f.lblOutput.Text;
        }

        void dgvConsoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            Console ConsoleSelected = dgvGetCurrentItem<Console>(sender);
            if (BIND.Console.IsNull() || ConsoleSelected.ID != BIND.Console.ID)
                BIND.Console = ConsoleSelected;
        }

        void dgvConsoles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                dgvConsoles_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
            }
        }

        void dgvConsoles_KeyPress(object sender, KeyPressEventArgs e)
        {
            dgv_KeyPress(sender, e, "cName");
        }

        async void mniMergeGamesIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var console = dgvGetCurrentItem<Console>(dgvConsoles);

            DisablePanelConsoles();
            await RA.MergeGamesIcon(console);
            EnablePanelConsoles();
        }

        async void mniMergeGamesIconBadSize_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var console = dgvGetCurrentItem<Console>(dgvConsoles);

            DisablePanelConsoles();
            await RA.MergeGamesIcon(console, true);
            EnablePanelConsoles();
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
