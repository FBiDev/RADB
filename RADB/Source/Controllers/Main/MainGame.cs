using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public partial class MainGame
    {
        private static RA ra = new RA();

        private static List<Game> lstGamesAll = new List<Game>();
        private static ListBind<Game> lstGamesByFilters = new ListBind<Game>();
        private static IEnumerable<Game> lstGamesByPlataform;
        private static int gamesWheelCounter;

        #region Games
        public static async Task Games_Init()
        {
            Session.OnConsoleChanged += ResetGamesLabels;
            Session.OnConsoleChanged += LoadGames;
            Session.OnTabMainChanged += () =>
            {
                if (Session.SelectedTab == Page.tabGames)
                {
                    pnlGamesConsoleName.Visible = Session.ConsoleSelected.NotNull();
                    dgvGames.Focus();
                }
            };
            Session.OnAddGames += (game) =>
            {
                lstGamesAll.Insert(0, game);
                lstGamesByFilters.Insert(0, game);
                return true;
            };

            mniMergeGameBadges.MouseDown += MniMergeGameBadges_MouseDown;
            mniPlayGame.MouseDown += MniPlayGame_MouseDown;
            mniHideGame.MouseDown += MniHideGame_MouseDown;

            btnUpdateGameList.Click += BtnUpdateGameList_Click;
            btnGameFilters.Click += BtnGameFilters_Click;

            dgvGames.AutoGenerateColumns = false;

            dgvGames.Columns.Format(ColumnFormat.StringCenter, cols: 0);
            dgvGames.Columns.Format(ColumnFormat.Image, cols: 1);
            dgvGames.Columns.Format(ColumnFormat.NumberCenter, cols: new[] { 4, 5, 6, 7 });
            dgvGames.Columns.Format(ColumnFormat.DateCenter, cols: 8);

            dgvGames.MouseDown += (sender, e) => dgvGames.ShowContextMenu(e, mnuGames);
            dgvGames.CellDoubleClick += MainCommon.ChangeBindGame;
            dgvGames.KeyPress += DgvGames_KeyPress;
            dgvGames.KeyDown += DgvGames_KeyDown;

            dgvGames.DataSourceChanged += LoadGamesIcons;
            dgvGames.Sorted += LoadGamesIcons;
            dgvGames.MouseWheel += DgvGames_MouseWheel;
            dgvGames.Scroll += DgvGames_Scroll;

            txtSearchGames.TextChanged += TxtSearchGames_TextChanged;
            txtSearchGames.KeyDown += TxtSearchGames_KeyDown;

            var filterCheckBoxes = new List<FlatCheckBoxA>
            {
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
                checkBox.CheckedChanged += ChkUpdateDataGrid;
            }

            await Games_Shown(null, null);
        }

        private static async Task Games_Shown(object sender, EventArgs e)
        {
            RASite.DLGames.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            RASite.DLGamesIcon.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            RASite.DLGamesBadges.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            RASite.DLGameExtendList.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);

            Session.MainGameList.Add(dgvGames);

            HideDownloadControls();
            lstGamesAll = await Game.Search(0);
        }

        private static void DisablePanelGames()
        {
            pnlDownloadGameList.Enabled = false;
            pnlGamesConsoleName.Enabled = false;
            lblNotFoundGameList.Visible = false;
            picLoaderGameList.Visible = true;
            dgvGames.Enabled = false;
        }

        private static void EnablePanelGames()
        {
            pnlDownloadGameList.Enabled = true;
            pnlGamesConsoleName.Enabled = true;
            lblNotFoundGameList.Visible = lstGamesByFilters.Count == 0;
            picLoaderGameList.Visible = false;
            dgvGames.Enabled = true;
        }

        private static void HideDownloadControls()
        {
            lblUpdateGameList.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            pgbGameList.Value = 0;
            pgbGameList.Visible = false;
        }

        private static Task ResetGamesLabels()
        {
            if (Session.LastConsole.NotNull() && Session.ConsoleSelected.ID == Session.LastConsole.ID)
            {
                MainCommon.ChangeTab(Page.tabGames);
                return Task.CompletedTask;
            }

            DisablePanelGames();
            lstGamesByFilters.Clear();

            UpdateConsoleLabels();

            HideDownloadControls();

            txtSearchGames.TextChanged -= TxtSearchGames_TextChanged;
            txtSearchGames.Text = string.Empty;
            txtSearchGames.TextChanged += TxtSearchGames_TextChanged;

            dgvGames.Columns["gConsole"].Visible = Session.ConsoleSelected.ID == 0;
            btnUpdateGameList.Visible = Session.ConsoleSelected.ID > 0;

            MainCommon.ChangeTab(Page.tabGames);

            return Task.CompletedTask;
        }

        private static async Task LoadGames()
        {
            if (Session.ConsoleSelected.IsNull() || (Session.LastConsole.NotNull() && Session.ConsoleSelected.ID == Session.LastConsole.ID))
            {
                return;
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            lstGamesByPlataform = lstGamesAll;
            if (Session.ConsoleSelected.ID > 0)
            {
                lstGamesByPlataform = lstGamesAll.Where(x => x.ConsoleID == Session.ConsoleSelected.ID);
            }

            if (lstGamesByPlataform.IsEmpty())
            {
                var fileName = Folder.GameData + Session.ConsoleSelected.Name + ".json";
                if (File.Exists(fileName) == false)
                {
                    BtnUpdateGameList_Click(null, null);
                }
            }

            await Task.Run(() => FilterGameList());

            dgvGames.Focus();
            stopwatch.Stop();

            MainCommon.WriteOutput("Games Loaded in: " + stopwatch.ElapsedMilliseconds + " Milliseconds");
        }

        private static async void BtnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (Session.ConsoleSelected.IsNull())
            {
                MessageBox.Show("No Console Selected");
                return;
            }

            DisablePanelGames();
            lstGamesByFilters.Clear();

            await ra.DownloadGameList(Session.ConsoleSelected);
            await ra.DownloadGamesIcon(Session.ConsoleSelected, RASite.DLGamesIcon);

            if (Session.ConsoleSelected.ID > 0)
            {
                lstGamesAll.RemoveAll(x => x.ConsoleID == Session.ConsoleSelected.ID);
            }
            else
            {
                lstGamesAll.Clear();
            }

            var gamesFiltered = await Game.Search(Session.ConsoleSelected.ID);
            lstGamesAll.AddRange(gamesFiltered);

            // var gameExList = await RA.DownloadGameExtendList(gamesFiltered, Browser.dlGameExtendList);
            MainCommon.WriteOutput("[" + DateTime.Now.ToTimeLong() + "] " + Session.ConsoleSelected.Name + " Updated!");
            Session.GameListChanged();

            await LoadGames();
        }

        private static void UpdateConsoleLabels()
        {
            if (Session.ConsoleSelected.IsNull())
            {
                return;
            }

            var numGames = lstGamesByFilters.Count(g => g.NumAchievements > 0);
            var totalGames = lstGamesByFilters.Count();
            lblConsoleName.Text = Session.ConsoleSelected.Name;
            lblConsoleGamesTotal.Text = numGames + " of " + totalGames + " Games";

            int scrollPosition = dgvGames.FirstDisplayedScrollingRowIndex;
            bool maintainScroll = true;
            if (maintainScroll)
            {
                bool txtFocus = txtSearchGames.Focused;

                if (dgvGames.RowCount > 0 && scrollPosition > -1)
                {
                    if (scrollPosition >= dgvGames.RowCount)
                    {
                        dgvGames.FirstDisplayedScrollingRowIndex = dgvGames.RowCount - 1;
                    }
                    else
                    {
                        dgvGames.FirstDisplayedScrollingRowIndex = scrollPosition;
                    }
                }

                if (txtFocus)
                {
                    txtSearchGames.Focus();
                }
            }
        }

        private static Task FilterGameList()
        {
            string search = txtSearchGames.Text;
            bool withoutAchievements = !chkWithoutAchievements.Checked;

            var gameTypes = new Dictionary<FlatCheckBoxA, string[]>
            {
                { chkOfficial, RA.GameType.NotOfficial },
                { chkPrototype, new[] { RA.GameType.Prototype } },
                { chkUnlicensed, new[] { RA.GameType.Unlicensed } },
                { chkDemo, new[] { RA.GameType.Demo } },
                { chkHack, new[] { RA.GameType.Hack } },
                { chkHomebrew, new[] { RA.GameType.Homebrew } },
                { chkSubset, new[] { RA.GameType.Subset } },
                { chkTestKit, new[] { RA.GameType.TestKit } },
                { chkDemoted, new[] { RA.GameType.Demoted } }
            };

            if (Session.ConsoleSelected.ID == 0 && search.Length == 0 && withoutAchievements == false && gameTypes.All(x => x.Key.Checked))
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
                    {
                        predicates.Add(g => g.Title.NotContains(gameType.Value));
                    }
                    else
                    {
                        predicates.Add(g => g.Title.ContainsExtend(gameType.Value[0]));
                    }
                }
            }

            lstGamesByFilters = new ListBind<Game>();
            foreach (Game obj in lstGamesByPlataform)
            {
                bool title = obj.Title.HasValue() && obj.Title.ContainsExtend(search);
                bool noCheevos = withoutAchievements && obj.NumAchievements == 0;

                if (title && !noCheevos && predicates.Any(p => p(obj)))
                {
                    lstGamesByFilters.Add(obj);
                }
            }

            SetDataSource(lstGamesByFilters);
            return Task.CompletedTask;
        }

        private static void SetDataSource(ListBind<Game> listGames)
        {
            dgvGames.InvokeIfRequired(() =>
            {
                dgvGames.DataSource = listGames;

                UpdateConsoleLabels();
                EnablePanelGames();
            });
        }

        private static async void LoadGamesIcons(object sender, EventArgs e)
        {
            await MainCommon.LoadGridIcons(dgvGames);
        }

        private static async void TxtSearchGames_TextChanged(object sender, EventArgs e)
        {
            await FilterGameList();
        }

        private static void TxtSearchGames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dgvGames.Focus();
            }
        }

        private static void BtnGameFilters_Click(object sender, EventArgs e)
        {
            pnlFilters.Visible = !pnlFilters.Visible;
            dgvGames.Focus();
        }

        private static async void ChkUpdateDataGrid(object sender, EventArgs e)
        {
            await FilterGameList();
            dgvGames.Focus();
        }

        private static void DgvGames_MouseWheel(object sender, MouseEventArgs e)
        {
            gamesWheelCounter = 1;
        }

        private static void DgvGames_Scroll(object sender, ScrollEventArgs e)
        {
            if (gamesWheelCounter > 0 && gamesWheelCounter < 3)
            {
                gamesWheelCounter++;
                return;
            }

            gamesWheelCounter = 0;
            LoadGamesIcons(sender, e);
        }

        private static void DgvGames_KeyPress(object sender, KeyPressEventArgs e)
        {
            MainCommon.GridViewKeyPress(sender, e, "gTitle");
        }

        private static void DgvGames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                if (((DataGridView)sender).CurrentRow != null)
                {
                    MainCommon.ChangeBindGame(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
                }
            }

            if (e.KeyData == Keys.Escape)
            {
                txtSearchGames.Focus();
            }

            if (e.KeyData == Keys.F5)
            {
                BtnUpdateGameList_Click(null, null);
            }
        }

        private static async void MniPlayGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var game = dgvGames.GetCurrentRowObject<Game>();

            if (await game.SaveToPlay() && Session.AddGamesToPlay(game))
            {
                lstGamesAll.Remove(game);
                lstGamesByFilters.Remove(game);
            }
        }

        private static async void MniHideGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var game = dgvGames.GetCurrentRowObject<Game>();

            if (await game.SaveToHide() && Session.AddGamesToHide(game))
            {
                lstGamesAll.Remove(game);
                lstGamesByFilters.Remove(game);
            }
        }

        private static async void MniMergeGameBadges_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var game = dgvGames.GetCurrentRowObject<Game>();

            DisablePanelGames();
            await ra.MergeGameBadges(game);
            EnablePanelGames();
        }
        #endregion
    }
}