using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public partial class MainGame
    {
        static RA RA = new RA();

        static List<Game> lstGamesAll = new List<Game>();
        static ListBind<Game> lstGamesByFilters = new ListBind<Game>();
        static IEnumerable<Game> lstGamesByPlataform;

        #region Games
        public static async Task Games_Init()
        {
            Session.OnConsoleChanged += ResetGamesLabels;
            Session.OnConsoleChanged += LoadGames;
            Session.OnTabMainChanged += () =>
            {
                if (Session.SelectedTab == form.tabGames)
                {
                    pnlGamesConsoleName.Visible = Session.Console.NotNull();
                    dgvGames.Focus();
                }
            };
            Session.OnAddGames += (game) =>
            {
                lstGamesAll.Insert(0, game);
                lstGamesByFilters.Insert(0, game);
                return true;
            };

            mniMergeGameBadges.MouseDown += mniMergeGameBadges_MouseDown;
            mniPlayGame.MouseDown += mniPlayGame_MouseDown;
            mniHideGame.MouseDown += mniHideGame_MouseDown;

            btnUpdateGameList.Click += btnUpdateGameList_Click;
            btnGameFilters.Click += btnGameFilters_Click;

            dgvGames.AutoGenerateColumns = false;

            dgvGames.Columns.Format(ColumnFormat.StringCenter, cols: 0);
            dgvGames.Columns.Format(ColumnFormat.Image, cols: 1);
            dgvGames.Columns.Format(ColumnFormat.NumberCenter, cols: new[] { 4, 5, 6, 7 });
            dgvGames.Columns.Format(ColumnFormat.DateCenter, cols: 8);

            dgvGames.MouseDown += (sender, e) => dgvGames.ShowContextMenu(e, mnuGames);
            dgvGames.CellDoubleClick += MainCommon.ChangeBindGame;
            dgvGames.KeyPress += dgvGames_KeyPress;
            dgvGames.KeyDown += dgvGames_KeyDown;

            dgvGames.DataSourceChanged += LoadGamesIcons;
            dgvGames.Sorted += LoadGamesIcons;
            dgvGames.MouseWheel += dgvGames_MouseWheel;
            dgvGames.Scroll += dgvGames_Scroll;

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

            await Games_Shown(null, null);
        }

        static async Task Games_Shown(object sender, EventArgs e)
        {
            RASite.dlGames.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            RASite.dlGamesIcon.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            RASite.dlGamesBadges.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            RASite.dlGameExtendList.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);

            Session.lstDgvGames.Add(dgvGames);

            HideDownloadControls();
            lstGamesAll = await Game.Search(0);
        }

        static void DisablePanelGames()
        {
            pnlDownloadGameList.Enabled = false;
            pnlGamesConsoleName.Enabled = false;
            lblNotFoundGameList.Visible = false;
            picLoaderGameList.Visible = true;
            dgvGames.Enabled = false;
        }

        static void EnablePanelGames()
        {
            pnlDownloadGameList.Enabled = true;
            pnlGamesConsoleName.Enabled = true;
            lblNotFoundGameList.Visible = (lstGamesByFilters.Count == 0);
            picLoaderGameList.Visible = false;
            dgvGames.Enabled = true;
        }

        static void HideDownloadControls()
        {
            lblUpdateGameList.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            pgbGameList.Value = 0;
            pgbGameList.Visible = false;
        }

        static Task ResetGamesLabels()
        {
            if (Session.LastConsole.NotNull() && Session.Console.ID == Session.LastConsole.ID)
            {
                MainCommon.ChangeTab(form.tabGames);
                return Task.CompletedTask;
            }

            DisablePanelGames();
            lstGamesByFilters.Clear();

            UpdateConsoleLabels();

            HideDownloadControls();

            txtSearchGames.TextChanged -= txtSearchGames_TextChanged;
            txtSearchGames.Text = "";
            txtSearchGames.TextChanged += txtSearchGames_TextChanged;

            dgvGames.Columns["gConsole"].Visible = Session.Console.ID == 0;

            MainCommon.ChangeTab(form.tabGames);

            return Task.CompletedTask;
        }

        static async Task LoadGames()
        {
            if (Session.Console.IsNull() || Session.LastConsole.NotNull() && Session.Console.ID == Session.LastConsole.ID) { return; }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            lstGamesByPlataform = lstGamesAll;
            if (Session.Console.ID > 0)
                lstGamesByPlataform = lstGamesAll.Where(x => x.ConsoleID == Session.Console.ID);

            if (lstGamesByPlataform.IsEmpty())
            {
                var fileName = Folder.GameData + Session.Console.Name + ".json";
                if (File.Exists(fileName) == false)
                {
                    btnUpdateGameList_Click(null, null);
                }
            }

            await Task.Run(() => FilterGameList());

            dgvGames.Focus();
            stopwatch.Stop();

            MainCommon.WriteOutput("Games Loaded in: " + stopwatch.ElapsedMilliseconds + " Milliseconds");
        }

        static async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (Session.Console.IsNull()) { MessageBox.Show("No Console Selected"); return; }

            DisablePanelGames();
            lstGamesByFilters.Clear();

            await RA.DownloadGameList(Session.Console);
            await RA.DownloadGamesIcon(Session.Console, RASite.dlGamesIcon);

            if (Session.Console.ID > 0)
                lstGamesAll.RemoveAll(x => x.ConsoleID == Session.Console.ID);
            else
                lstGamesAll.Clear();

            var gamesFiltered = await Game.Search(Session.Console.ID);
            lstGamesAll.AddRange(gamesFiltered);

            //var gameExList = await RA.DownloadGameExtendList(gamesFiltered, Browser.dlGameExtendList);

            MainCommon.WriteOutput("[" + DateTime.Now.ToTimeLong() + "] " + Session.Console.Name + " Updated!");
            Session.GameListChanged();

            await LoadGames();
        }

        static void UpdateConsoleLabels()
        {
            if (Session.Console == null) return;

            var numGames = lstGamesByFilters.Count(g => g.NumAchievements > 0);
            var totalGames = lstGamesByFilters.Count();
            lblConsoleName.Text = Session.Console.Name;
            lblConsoleGamesTotal.Text = numGames + " of " + totalGames + " Games";

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

        static Task FilterGameList()
        {
            string search = txtSearchGames.Text;
            bool WithoutAchievements = !chkWithoutAchievements.Checked;

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
                { chkDemoted, new[]{RA.GameType.Demoted }}
            };

            if (Session.Console.ID == 0 && search.Length == 0 && WithoutAchievements == false && gameTypes.All(x => x.Key.Checked))
            {
                lstGamesByFilters = new ListBind<Game>(lstGamesAll);
                SetDataSource(lstGamesByFilters);
                return Task.CompletedTask;
            }

            var predicates = new List<Predicate<Game>>();
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

            lstGamesByFilters = new ListBind<Game>();
            foreach (Game obj in lstGamesByPlataform)
            {
                bool title = (obj.Title.HasValue() && obj.Title.ContainsExtend(search));
                bool noCheevos = WithoutAchievements && obj.NumAchievements == 0;

                if (title && !noCheevos && predicates.Any(p => p(obj)))
                {
                    lstGamesByFilters.Add(obj);
                }
            }

            SetDataSource(lstGamesByFilters);
            return Task.CompletedTask;
        }

        static void SetDataSource(ListBind<Game> listGames)
        {
            dgvGames.InvokeIfRequired(() =>
            {
                dgvGames.DataSource = listGames;

                UpdateConsoleLabels();
                EnablePanelGames();
            });
        }

        static async void LoadGamesIcons(object sender, EventArgs e)
        {
            await MainCommon.LoadGridIcons(dgvGames);
        }

        static async void txtSearchGames_TextChanged(object sender, EventArgs e)
        {
            await FilterGameList();
        }

        static void txtSearchGames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dgvGames.Focus();
            }
        }

        static void btnGameFilters_Click(object sender, EventArgs e)
        {
            pnlFilters.Visible = !pnlFilters.Visible;
            dgvGames.Focus();
        }

        static async void chkUpdateDataGrid(object sender, EventArgs e)
        {
            await FilterGameList();
            dgvGames.Focus();
        }

        static int gamesWheelCounter;
        static void dgvGames_MouseWheel(object sender, MouseEventArgs e) { gamesWheelCounter = 1; }
        static void dgvGames_Scroll(object sender, ScrollEventArgs e)
        {
            if (gamesWheelCounter > 0 && gamesWheelCounter < 3) { gamesWheelCounter++; return; }
            gamesWheelCounter = 0;
            LoadGamesIcons(sender, e);
        }

        static void dgvGames_KeyPress(object sender, KeyPressEventArgs e)
        {
            MainCommon.GridViewKeyPress(sender, e, "gTitle");
        }

        static void dgvGames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                if (((DataGridView)sender).CurrentRow != null)
                    MainCommon.ChangeBindGame(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
            }

            if (e.KeyData == Keys.Escape) { txtSearchGames.Focus(); }
            if (e.KeyData == Keys.F5) { btnUpdateGameList_Click(null, null); }
        }

        static async void mniPlayGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGames.GetCurrentRowObject<Game>();

            if (await game.SaveToPlay() && Session.AddGamesToPlay(game))
            {
                lstGamesAll.Remove(game);
                lstGamesByFilters.Remove(game);
            }
        }

        static async void mniHideGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGames.GetCurrentRowObject<Game>();

            if (await game.SaveToHide() && Session.AddGamesToHide(game))
            {
                lstGamesAll.Remove(game);
                lstGamesByFilters.Remove(game);
            }
        }

        static async void mniMergeGameBadges_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGames.GetCurrentRowObject<Game>();

            DisablePanelGames();
            await RA.MergeGameBadges(game);
            EnablePanelGames();
        }
        #endregion
    }
}