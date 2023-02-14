using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public partial class GameMain
    {
        RA RA = new RA();

        public List<Game> lstGames = new List<Game>();
        public ListBind<Game> lstGamesSearch = new ListBind<Game>();

        public GameMain()
        {
            f = BIND.f;
            Games_Init();
        }

        #region Games
        void Games_Init()
        {
            BIND.OnConsoleChanged += ResetGamesLabels;
            BIND.OnConsoleChanged += LoadGames;

            f.Shown += Games_Shown;
            mniMergeGameBadges.MouseDown += mniMergeGameBadges_MouseDown;
            mniPlayGame.MouseDown += mniPlayGame_MouseDown;
            mniHideGame.MouseDown += mniHideGame_MouseDown;

            btnUpdateGameList.Click += btnUpdateGameList_Click;
            btnGameFilters.Click += btnGameFilters_Click;

            dgvGames.AutoGenerateColumns = false;

            dgvGames.Columns.Format(CellStyle.StringCenter, 0);
            dgvGames.Columns.Format(CellStyle.Image, 1);
            dgvGames.Columns.Format(CellStyle.NumberCenter, 4, 5, 6);
            dgvGames.Columns.Format(CellStyle.DateCenter, 7);

            dgvGames.CellPainting += dgvGames_CellPainting;
            dgvGames.DataSourceChanged += dgvGames_DataSourceChanged;

            dgvGames.MouseDown += (sender, e) => dgvGames.ShowContextMenu(e, mnuGames);
            dgvGames.CellDoubleClick += dgvGames_CellDoubleClick;
            dgvGames.KeyPress += dgvGames_KeyPress;
            dgvGames.KeyDown += dgvGames_KeyDown;

            dgvGames.MouseWheel += dgvGames_MouseWheel;
            dgvGames.Scroll += dgvGames_Scroll;
            dgvGames.Sorted += dgvGames_Sorted;

            //txtSearchGames.TextChanged += async (sender, e) => await txtSearchGames_TextChanged(sender, e);
            txtSearchGames.TextChanged += txtSearchGames_TextChanged;
            txtSearchGames.KeyDown += txtSearchGames_KeyDown;

            var filterCheckBoxes = new List<FlatCheckBoxA>{
                chkWithoutAchievements,
                chkOfficial,
                chkPrototype,
                chkUnlicensed,
                chkDemo,
                chkHack,
                chkHomebrew,
                chkSubset,
                chkTestKit,
                chkDemoted
            };

            foreach (var checkBox in filterCheckBoxes)
            {
                checkBox.CheckedChanged += chkUpdateDataGrid;
            }
        }

        async void Games_Shown(object sender, EventArgs e)
        {
            Browser.dlGames.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            Browser.dlGamesIcon.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            Browser.dlGamesBadges.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);

            BIND.lstDgvGames.Add(dgvGames);

            //ResetGamesLabels(false);
            //Load All Games //Not Block UI
            lstGames = await Game.Search(0);
            //});
        }

        void DisablePanelGames()
        {
            pnlDownloadGameList.Enabled = false;
            pnlGamesConsoleName.Enabled = false;
            lblNotFoundGameList.Visible = false;
            picLoaderGameList.Visible = true;
            dgvGames.Enabled = false;
        }

        void EnablePanelGames()
        {
            pnlDownloadGameList.Enabled = true;
            pnlGamesConsoleName.Enabled = true;
            lblNotFoundGameList.Visible = (dgvGames.RowCount == 0);
            picLoaderGameList.Visible = false;
            dgvGames.Enabled = true;
        }

        Task ResetGamesLabels()
        {
            DisablePanelGames();
            lstGamesSearch.Clear();

            UpdateConsoleLabels();

            lblUpdateGameList.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            pgbGameList.Value = 0;
            pgbGameList.Visible = false;

            txtSearchGames.TextChanged -= txtSearchGames_TextChanged;
            txtSearchGames.Text = "";
            txtSearchGames.TextChanged += txtSearchGames_TextChanged;

            dgvGames.Columns["gConsole"].Visible = BIND.Console.ID == 0;

            ChangeTab(f.tabGames);

            return null;
        }

        void ChangeTab(TabPage tab)
        {
            f.tabMain.SelectedTab = tab;
            f.tabMain.Refresh();
        }

        Task LoadGames()
        {
            if (BIND.Console.IsNull()) { return null; }
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            //lstGames = await Game.Search(SESSION.Console.ID);

            //TODO: update to filter by console
            if (lstGames.Count == 0)
            {
                var fileExist = File.Exists(Folder.GameData + BIND.Console.Name + ".json");
                if (fileExist == false)
                {
                    btnUpdateGameList_Click(null, null);
                }
            }

            txtSearchGames_TextChanged(null, null);
            EnablePanelGames();

            dgvGames.Focus();
            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
            f.lblOutput.Text = "Games Loaded in: " + Convert.ToInt32(fim0.TotalMilliseconds) + " Milliseconds" + Environment.NewLine + f.lblOutput.Text;
            return null;
        }

        async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (BIND.Console.IsNull()) { MessageBox.Show("No Console Selected"); return; }

            DisablePanelGames();
            lstGamesSearch.Clear();
            //Download GameList
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGameList(BIND.Console);
            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;

            //Download game icons
            TimeSpan ini1 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGamesIcon(BIND.Console, Browser.dlGamesIcon);
            TimeSpan fim1 = new TimeSpan(DateTime.Now.Ticks) - ini1;

            //Load Games
            TimeSpan ini2 = new TimeSpan(DateTime.Now.Ticks);
            //TODO Reload list of games
            await LoadGames();
            //await LoadGamesToPlay();
            //await LoadGamesToHide();

            //UpdateConsoleLabels();
            TimeSpan fim2 = new TimeSpan(DateTime.Now.Ticks) - ini2;

            //Update ConsoleBind
            BIND.Console.NumGames = lstGames.Count(g => g.NumAchievements > 0);
            BIND.Console.TotalGames = lstGames.Count();

            f.lblOutput.Text = "[" + DateTime.Now.ToTimeLong() + "] " + BIND.Console.Name + " Updated!" + Environment.NewLine + f.lblOutput.Text;

            BIND.GameListChanged();
        }

        void UpdateConsoleLabels()
        {
            if (BIND.Console == null) return;

            //Update Console
            var numGames = lstGamesSearch.Count(g => g.NumAchievements > 0);
            var totalGames = lstGamesSearch.Count();
            lblConsoleName.Text = BIND.Console.Name;
            lblConsoleGamesTotal.Text = numGames + " of " + totalGames + " Games";
        }

        void txtSearchGames_TextChanged(object sender, EventArgs e)
        {
            //if (txtSearchGames.Text.Count() > 0 && txtSearchGames.Text.Count() < 3) { return; }
            ListBind<Game> newSearch = new ListBind<Game>();
            List<Predicate<Game>> predicates = new List<Predicate<Game>>();

            var gameTypes = new Dictionary<FlatCheckBoxA, string[]>
            {
                { chkOfficial, RA.GameType.NotOfficial },
                { chkPrototype, new[]{ RA.GameType.Prototype }},
                { chkUnlicensed, new[]{RA.GameType.Unlicensed }},
                { chkDemo, new[]{RA.GameType.Demo }},
                { chkHack, new[]{RA.GameType.Hack }},
                { chkHomebrew, new[]{RA.GameType.Homebrew }},
                { chkSubset, new[]{RA.GameType.Subset }},
                { chkTestKit, new[]{RA.GameType.TestKit }},
                { chkDemoted, new[]{RA.GameType.Demoted }},
            };

            foreach (var gameType in gameTypes)
            {
                if (gameType.Key.Checked)
                {
                    if (gameType.Key == chkOfficial)
                        predicates.Add(g => g.Title.NotContains(gameType.Value));
                    else
                        predicates.Add(g => g.Title.ContainsExtend(gameType.Value[0]));
                }
            }

            string search = txtSearchGames.Text;
            bool WithoutAchievements = !chkWithoutAchievements.Checked;

            IEnumerable<Game> nList;
            if (BIND.Console.ID > 0)
                nList = lstGames.Where(x => x.ConsoleID == BIND.Console.ID);
            else
                nList = lstGames;

            foreach (Game obj in nList)
            {
                bool title = (obj.Title.HasValue() && obj.Title.ContainsExtend(search));
                bool noCheevos = WithoutAchievements && obj.NumAchievements == 0;

                if (title && !noCheevos && predicates.Any(p => p(obj)))
                {
                    newSearch.Add(obj);
                }
            }

            lstGamesSearch = newSearch;
            dgvGames.DataSource = lstGamesSearch;

            UpdateConsoleLabels();

            int scrollPosition = dgvGames.FirstDisplayedScrollingRowIndex;
            bool maintainScroll = true;
            if (maintainScroll)
            {
                bool txtFocus = txtSearchGames.Focused;

                if (dgvGames.RowCount > 0 && scrollPosition > -1)
                {
                    if (scrollPosition >= dgvGames.RowCount)
                        dgvGames.FirstDisplayedScrollingRowIndex = dgvGames.RowCount - 1;
                    else
                        dgvGames.FirstDisplayedScrollingRowIndex = scrollPosition;
                }

                if (txtFocus) { txtSearchGames.Focus(); }
            }
        }

        void txtSearchGames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dgvGames.Focus();
            }
        }

        void btnGameFilters_Click(object sender, EventArgs e)
        {
            pnlFilters.Visible = !pnlFilters.Visible;
            dgvGames.Focus();
        }

        void chkUpdateDataGrid(object sender, EventArgs e)
        {
            txtSearchGames_TextChanged(null, null);
            dgvGames.Focus();
        }

        void dgvGames_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bilinear;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;

            if (e.ColumnIndex != 1 || e.RowHeader()) { return; }

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //e.Graphics.PixelOffsetMode = PixelOffHalf;
        }

        int wheel;
        void dgvGames_MouseWheel(object sender, MouseEventArgs e)
        {
            wheel = 1;
        }

        async void dgvGames_Scroll(object sender, ScrollEventArgs e)
        {
            if (wheel > 0 && wheel < 3) { wheel++; return; }
            wheel = 0;

            ((DataGridView)sender).Focus();
            await LoadGamesIcon();
        }

        async void dgvGames_Sorted(object sender, EventArgs e)
        {
            await LoadGamesIcon();
        }

        async void dgvGames_DataSourceChanged(object sender, EventArgs e)
        {
            await LoadGamesIcon();
        }

        void dgvGames_KeyPress(object sender, KeyPressEventArgs e)
        {
            dgv_KeyPress(sender, e, "gTitle");
        }

        void dgvGames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                dgvGames_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
            }

            if (e.KeyData == Keys.Escape) { txtSearchGames.Focus(); }
            if (e.KeyData == Keys.F5) { btnUpdateGameList_Click(null, null); }
        }

        void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            BIND.Game = dgvGetCurrentItem<Game>(sender);

            //pnlInfoScroll.AutoScrollPosition = new Point(pnlInfoScroll.AutoScrollPosition.X, 0);
            //pnlInfoScroll.VerticalScroll.Value = 0;

            //LoadGameExtendBase();
            //await LoadGameExtend();

            ////Update GameExtend
            //if (GameExtendBind.IsNull() || GameExtendBind.ConsoleID == 0)
            //{
            //    btnUpdateInfo_Click(null, null);
            //}

            //f.tabMain.SelectedTab = f.tabGameInfo;

            //dgvAchievements.Focus();
        }

        async Task LoadGamesIcon()
        {
            foreach (DataGridView dgv in BIND.lstDgvGames)
            {
                await Task.Run(() =>
                {
                    if (dgv.DataSource.IsNull() || dgv.RowCount == 0) { return; }

                    int index = dgv.FirstDisplayedScrollingRowIndex;
                    int nItems = (int)Math.Ceiling((double)(dgv.Height - 29) / 37) + 12;

                    var list = dgv.DataSource as ListBind<Game>;
                    //var list = dgv.DataSource as DataTable;

                    if (list == null) return;

                    for (int i = index; i < index + nItems; i++)
                    {
                        if (i >= list.Count) { break; }

                        list[i].SetImageIconBitmap();
                    }
                    dgv.InvokeIfRequired(dgv.Refresh);
                });
            }
        }

        async void mniPlayGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGetCurrentItem<Game>(dgvGames);

            if (await game.InsertToPlay())
            {
                lstGames.Remove(game);
                lstGamesSearch.Remove(game);
                //lstGamesToPlay.Insert(0, game);
                await LoadGamesIcon();
                //lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
            }
        }

        async void mniHideGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGetCurrentItem<Game>(dgvGames);

            if (await game.InsertToHide())
            {
                //CurrencyManager cMnger = (CurrencyManager)BindingContext[dgvGames.DataSource];
                //cMnger.SuspendBinding();
                //dgvGames.SelectedRows[0].Visible = false;
                //cMnger.ResumeBinding();

                lstGames.Remove(game);
                lstGamesSearch.Remove(game);
                //lstGamesToHide.Insert(0, game);
                await LoadGamesIcon();
                //lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
            }
        }

        async void mniMergeGameBadges_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGetCurrentItem<Game>(dgvGames);

            DisablePanelGames();
            await RA.MergeGameBadges(game);
            EnablePanelGames();
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
