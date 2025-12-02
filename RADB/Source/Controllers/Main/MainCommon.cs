using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public static partial class MainCommon
    {
        private static FormWindowState? lastWindowState;

        #region MAIN
        public static void Main_Init(Main formDesign)
        {
            Session.MainFormRA = formDesign;
            Page.Init();

            Page.Load += Main_Load;
            Page.Shown += Main_Shown;
            Page.KeyDown += Main_KeyDown;
            Page.Resize += Main_Resize;
            ////KeyPreview = true;

            tabMain.KeyDown += MainTab_KeyDown;
            tabMain.SelectedIndexChanged += MainTab_SelectedIndexChanged;
        }

        #region Common
        public static void ChangeTab(TabPage tab)
        {
            Page.tabMain.SelectedTab = tab;
            MainTab_SelectedIndexChanged(tabMain, null);
            Page.tabMain.Refresh();
        }

        public static void WriteOutput(string text)
        {
            Page.lblOutput.InvokeIfRequired(() =>
            {
                Page.lblOutput.Text = text + Environment.NewLine + Page.lblOutput.Text;
            });
        }

        public static void ChangeBindGame(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader())
            {
                return;
            }

            var dgv = sender as FlatDataGridA;
            Session.GameSelected = dgv.GetCurrentRowObject<Game>();
        }

        public static async Task LoadGridIcons(DataGridView dgv)
        {
            await Task.Run(() =>
            {
                if (dgv.DataSource.IsNull() || dgv.RowCount == 0)
                {
                    return;
                }

                int index = dgv.FirstDisplayedScrollingRowIndex;
                int nItems = (int)Math.Ceiling((double)(dgv.Height - 29) / 37) + 12;

                var list = dgv.DataSource as ListBind<Game>;

                if (list == null)
                {
                    return;
                }

                for (int i = index; i < index + nItems; i++)
                {
                    if (i >= list.Count)
                    {
                        break;
                    }

                    list[i].SetImageIconGridBitmap();
                }
            });
            dgv.Refresh();
        }

        public static async Task LoadAllGamesIcon()
        {
            foreach (DataGridView dgv in Session.MainGameList)
            {
                await LoadGridIcons(dgv);
            }
        }

        public static void GridViewKeyPress(object sender, KeyPressEventArgs e, string columnName)
        {
            var dgv = sender as DataGridView;
            var typedChar = e.KeyChar;

            if (char.IsLetter(typedChar) == false)
            {
                return;
            }

            int firstIndex = -1;
            int nextIndex = -1;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[columnName].Value.ToString().StartsWith(typedChar.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    if (firstIndex == -1)
                    {
                        firstIndex = row.Index;
                    }

                    if (nextIndex == -1 && dgv.CurrentRow.Index < row.Index)
                    {
                        nextIndex = row.Index;
                    }
                }
            }

            if (nextIndex == -1)
            {
                nextIndex = firstIndex;
            }

            dgv.Rows[nextIndex].Cells[0].Selected = true;
        }
        #endregion

        private static void Main_Load(object sender, EventArgs e)
        {
        }

        private static async void Main_Shown(object sender, EventArgs e)
        {
            Theme.SetTheme(Session.MainFormRA.IsDesignMode);
            Session.MainFormRA.CenterWindow();

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

        private static void Main_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Modifiers == Keys.Alt;
        }

        private static async void Main_Resize(object sender, EventArgs e)
        {
            if (Page.WindowState != lastWindowState)
            {
                if (Page.WindowState == FormWindowState.Maximized)
                {
                    await LoadAllGamesIcon();
                }
                else if (Page.WindowState == FormWindowState.Normal)
                {
                }

                lastWindowState = Page.WindowState;
            }
        }
        #endregion

        #region MainTab
        private static void MainTab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Alt)
            {
                return;
            }

            if (e.KeyCode == Keys.Right && tabMain.SelectedIndex < tabMain.TabPages.Count)
            {
                tabMain.SelectedIndex += 1;
            }

            if (e.KeyCode == Keys.Left && tabMain.SelectedIndex > 0)
            {
                tabMain.SelectedIndex -= 1;
            }
        }

        private static void MainTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = sender as TabControl;
            Session.TabMainChanged(tab.SelectedTab);
        }
        #endregion
    }
}