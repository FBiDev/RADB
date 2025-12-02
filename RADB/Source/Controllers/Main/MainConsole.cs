using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public static partial class MainConsole
    {
        private static RA ra = new RA();
        private static ListBind<Console> lstConsoles = new ListBind<Console>();

        #region Consoles
        public static async Task Console_Init()
        {
            Session.OnGameListChanged += UpdateConsoleList;
            Session.OnTabMainChanged += () =>
            {
                if (Session.SelectedTab == Page.tabConsoles)
                {
                    ConsolesDataGridView.Focus();
                }
            };

            MergeGamesIconMenuItem.MouseDown += MergeGamesIconMenuItem_MouseDown;
            MergeGamesIconBadSizeMenuItem.MouseDown += MergeGamesIconBadSizeMenuItem_MouseDown;

            UpdateConsolesButton.Click += UpdateConsolesButton_Click;

            ConsolesDataGridView.AutoGenerateColumns = true;
            ConsolesDataGridView.DataSource = lstConsoles;

            ConsolesDataGridView.Columns.Format(ColumnFormat.StringCenter, cols: 0);
            ConsolesDataGridView.Columns.Format(ColumnFormat.NumberCenter, cols: new[] { 3, 4 });

            ConsolesDataGridView.MouseDown += (sender, e) => ConsolesDataGridView.ShowContextMenu(e, mnuConsoles);
            ConsolesDataGridView.CellDoubleClick += ConsolesDataGridView_CellDoubleClick;
            ConsolesDataGridView.KeyPress += ConsolesDataGridView_KeyPress;
            ConsolesDataGridView.KeyDown += ConsolesDataGridView_KeyDown;

            await Console_Shown(null, null);
        }

        private static async Task Console_Shown(object sender, EventArgs e)
        {
            RASite.DLConsoles.SetControls(lblProgressConsoles, pgbConsoles, lblUpdateConsoles);
            RASite.DLConsolesGamesIcon.SetControls(lblProgressConsoles, pgbConsoles, lblUpdateConsoles);

            MainCommon.ChangeTab(Page.tabConsoles);
            ResetConsolesLabels();
            await LoadConsoles();
        }

        private static async Task UpdateConsoleList()
        {
            lstConsoles = new ListBind<Console>(await Console.List());
            ConsolesDataGridView.DataSource = lstConsoles;

            var console = lstConsoles.First(x => x.Name == "All Games");
            console.NumGames = lstConsoles.Except(new[] { console }).Sum(x => x.NumGames);
            console.TotalGames = lstConsoles.Except(new[] { console }).Sum(x => x.TotalGames);
        }

        private static void DisablePanelConsoles()
        {
            pnlDownloadConsoles.Enabled = false;
            lblNotFoundConsoles.Visible = false;
            picLoaderConsole.Visible = true;
            ConsolesDataGridView.Enabled = false;
        }

        private static void EnablePanelConsoles()
        {
            pnlDownloadConsoles.Enabled = true;
            lblNotFoundConsoles.Visible = ConsolesDataGridView.RowCount == 0;
            picLoaderConsole.Visible = false;
            ConsolesDataGridView.Enabled = true;
        }

        private static void ResetConsolesLabels()
        {
            lblUpdateConsoles.Text = string.Empty;
            lblProgressConsoles.Text = string.Empty;
            pgbConsoles.Value = 0;
            pgbConsoles.Visible = false;
        }

        private static async Task LoadConsoles()
        {
            DisablePanelConsoles();

            lstConsoles = new ListBind<Console>(await Console.List());
            ConsolesDataGridView.DataSource = lstConsoles;

            EnablePanelConsoles();

            if (lstConsoles.IsEmpty())
            {
                UpdateConsolesButton_Click(null, null);
            }

            if (Session.SelectedTab == Page.tabConsoles)
            {
                ConsolesDataGridView.Focus();
            }
        }

        private static async void UpdateConsolesButton_Click(object sender, EventArgs e)
        {
            DisablePanelConsoles();
            lstConsoles.Clear();
            await ra.DownloadConsoles();
            await LoadConsoles();
            EnablePanelConsoles();

            MainCommon.WriteOutput("[" + DateTime.Now.ToTimeLong() + "] " + "Consoles Updated!");
        }

        private static void ConsolesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader())
            {
                return;
            }

            var consoleSelected = ConsolesDataGridView.GetCurrentRowObject<Console>();
            Session.LastConsole = Session.ConsoleSelected;
            Session.ConsoleSelected = consoleSelected;
        }

        private static void ConsolesDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            MainCommon.GridViewKeyPress(sender, e, "cName");
        }

        private static void ConsolesDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                ConsolesDataGridView_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
            }
        }

        private static async void MergeGamesIconMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var console = ConsolesDataGridView.GetCurrentRowObject<Console>();

            DisablePanelConsoles();
            await ra.MergeGamesIcon(console);
            EnablePanelConsoles();
        }

        private static async void MergeGamesIconBadSizeMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var console = ConsolesDataGridView.GetCurrentRowObject<Console>();

            DisablePanelConsoles();
            await ra.MergeGamesIcon(console, true);
            EnablePanelConsoles();
        }
        #endregion
    }
}
