using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public static partial class MainCommon
    {
        #region MAIN
        public static void Main_Init(Main formDesign)
        {
            BIND.f = formDesign;
            form.Init(form);

            form.Load += Main_Load;
            form.Shown += Main_Shown;
            form.KeyDown += Main_KeyDown;
            form.Resize += Main_Resize;
            //KeyPreview = true;

            tabMain.KeyDown += tabMain_KeyDown;
            tabMain.SelectedIndexChanged += tabMain_SelectedIndexChanged;

            //Internet
            Browser.Load();
            //Folders
            Folder.CreateFolders();
        }

        static void Main_Load(object sender, EventArgs e) { }

        static async void Main_Shown(object sender, EventArgs e)
        {
            await Task.Delay(10);

            await MainConsole.Console_Init();
            await MainGame.Games_Init();
            await MainGameInfo.GameInfo_Init();
            await MainGameToPlay.GamesToPlay_Init();
            await MainGameToHide.GamesToHide_Init();
            await MainUserInfo.User_Init();
            await MainAbout.About_Init();

            await LoadAllGamesIcon();
        }

        static void Main_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Modifiers == Keys.Alt;
        }

        static FormWindowState? LastWindowState;
        static async void Main_Resize(object sender, EventArgs e)
        {
            if (form.WindowState != LastWindowState)
            {
                if (form.WindowState == FormWindowState.Maximized)
                {
                    await LoadAllGamesIcon();
                }
                else if (form.WindowState == FormWindowState.Normal) { }
                LastWindowState = form.WindowState;
            }
        }
        #endregion

        #region TABMAIN
        static void tabMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Alt) { return; }
            if (e.KeyCode == Keys.Right && tabMain.SelectedIndex < tabMain.TabPages.Count) { tabMain.SelectedIndex += 1; }
            if (e.KeyCode == Keys.Left && tabMain.SelectedIndex > 0) { tabMain.SelectedIndex -= 1; }
        }

        static void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = sender as TabControl;
            BIND.TabMainChanged(tab.SelectedTab);
        }
        #endregion

        #region Common
        public static void ChangeTab(TabPage tab)
        {
            form.tabMain.SelectedTab = tab;
            tabMain_SelectedIndexChanged(tabMain, null);
            form.tabMain.Refresh();
        }

        public static void WriteOutput(string text)
        {
            form.lblOutput.Text = text + Environment.NewLine + form.lblOutput.Text;
        }

        public static void ChangeBindGame(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;
            var dgv = sender as FlatDataGridA;
            BIND.Game = dgv.GetSelectedItem<Game>();
        }

        public static async Task LoadGridIcons(DataGridView dgv)
        {
            await Task.Run(() =>
            {
                if (dgv.DataSource.IsNull() || dgv.RowCount == 0) { return; }

                int index = dgv.FirstDisplayedScrollingRowIndex;
                int nItems = (int)Math.Ceiling((double)(dgv.Height - 29) / 37) + 12;

                var list = dgv.DataSource as ListBind<Game>;

                if (list == null) return;

                for (int i = index; i < index + nItems; i++)
                {
                    if (i >= list.Count) { break; }

                    list[i].SetImageIconBitmap();
                }
            });
            dgv.Refresh();
        }

        public static async Task LoadAllGamesIcon()
        {
            foreach (DataGridView dgv in BIND.lstDgvGames)
            {
                await LoadGridIcons(dgv);
            }
        }

        public static void GridViewKeyPress(object sender, KeyPressEventArgs e, string columnName)
        {
            var dgv = sender as DataGridView;
            var typedChar = e.KeyChar;

            if (char.IsLetter(typedChar) == false) return;

            int firstIndex = -1;
            int nextIndex = -1;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[columnName].Value.ToString().StartsWith(typedChar.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    if (firstIndex == -1) { firstIndex = row.Index; }
                    if (nextIndex == -1 && dgv.CurrentRow.Index < row.Index) { nextIndex = row.Index; }
                }
            }

            if (nextIndex == -1) { nextIndex = firstIndex; }
            dgv.Rows[nextIndex].Cells[0].Selected = true;
        }
        #endregion
    }
}