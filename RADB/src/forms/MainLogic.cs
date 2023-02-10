using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RADB
{
    public partial class MainLogic
    {
        #region Properties
        protected readonly Main f;

        struct SESSION
        {
            internal static event AsyncAction ConsoleChanged = delegate { return Task.Run(() => { }); };

            static Console _Console;
            public static Console Console
            {
                get { return _Console; }
                set
                {
                    _Console = value;
                    ConsoleChanged();
                }
            }
        }

        RA RA = new RA();
        bool _RALogged;
        bool RALogged
        {
            get { return _RALogged; }
            set
            {
                _RALogged = value;
                RALoggedChanged(this, EventArgs.Empty);
            }
        }

        public ListBind<Console> lstConsoles = new ListBind<Console>();

        public ListBind<Game> lstGames = new ListBind<Game>();
        public Game GameBind;
        public ListBind<Game> lstGamesSearch = new ListBind<Game>();

        public List<DataGridView> lstDgvGames = new List<DataGridView>();

        public ListBind<Game> lstGamesToPlay = new ListBind<Game>();
        public ListBind<Game> lstGamesToHide = new ListBind<Game>();

        bool _GamesUpdated;
        public bool GamesUpdated
        {
            get { return _GamesUpdated; }
            set
            {
                _GamesUpdated = value;
                GamesUpdatedChanged(this, EventArgs.Empty);
            }
        }

        GameExtend GameExtendBind;

        User UserBind = new User();
        #endregion

        #region Events
        delegate Task AsyncEvent(object sender, EventArgs e);
        delegate Task AsyncAction();

        event EventHandler RALoggedChanged = delegate { };
        event EventHandler ConsoleGridChanged = delegate { };

        event EventHandler GamesUpdatedChanged = delegate { };
        event Action OnExec = delegate { };
        #endregion

        #region MAIN
        public MainLogic(Main form)
        {
            f = form;

            Main_Init();
            Consoles_Init();
            Games_Init();
            GameInfo_Init();
            GamesToPlay_Init();
            GamesToHide_Init();
            User_Init();
            About_Init();
        }

        void Main_Init()
        {
            f.KeyDown += Main_KeyDown;
            f.Resize += Main_Resize;
            f.Load += Main_Load;
            f.Shown += Main_Shown;
            //KeyPreview = true;

            tabMain.KeyDown += tabMain_KeyDown;
            tabMain.SelectedIndexChanged += tabMain_SelectedIndexChanged;

            //Internet
            Browser.Load();
            //Folders
            Folder.CreateFolders();
        }

        void Main_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Modifiers == Keys.Alt;
        }

        FormWindowState? LastWindowState;
        async void Main_Resize(object sender, EventArgs e)
        {
            if (f.WindowState != LastWindowState)
            {
                if (f.WindowState == FormWindowState.Maximized)
                {
                    await LoadGamesIcon();
                }
                else if (f.WindowState == FormWindowState.Normal) { }
                LastWindowState = f.WindowState;
            }
        }

        void Main_Load(object sender, EventArgs e)
        {
            var j = JsonConvert.DeserializeObject<JObject>("{\"LoadJsonDLL\":\"...\"}");
        }

        void Main_Shown(object sender, EventArgs e)
        {
        }
        #endregion

        #region TABMAIN
        void tabMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Alt) { return; }
            if (e.KeyCode == Keys.Right && tabMain.SelectedIndex < tabMain.TabPages.Count) { tabMain.SelectedIndex += 1; }
            if (e.KeyCode == Keys.Left && tabMain.SelectedIndex > 0) { tabMain.SelectedIndex -= 1; }
        }

        void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = (sender as TabControl);

            if (tab.SelectedTab == tabConsoles) { dgvConsoles.Focus(); return; }
            if (tab.SelectedTab == tabGames)
            {
                pnlGamesConsoleName.Visible = !SESSION.Console.IsNull();
                dgvGames.Focus(); return;
            }
            if (tab.SelectedTab == tabGamesToPlay) { dgvGamesToPlay.Focus(); return; }
            if (tab.SelectedTab == tabGamesToHide) { dgvGamesToHide.Focus(); return; }
            if (tab.SelectedTab == tabGameInfo) { pnlInfoScroll.Focus(); return; }
            if (tab.SelectedTab == tabUserInfo) { txtUsername.Focus(); return; }
        }
        #endregion

        #region Consoles
        void Consoles_Init()
        {
            GamesUpdatedChanged += UpdateConsoleAllGames;

            f.Shown += Consoles_Shown;
            mniMergeGamesIcon.MouseDown += mniMergeGamesIcon_MouseDown;
            mniMergeGamesIconBadSize.MouseDown += mniMergeGamesIconBadSize_MouseDown;

            btnUpdateConsoles.Click += btnUpdateConsoles_Click;

            dgvConsoles.AutoGenerateColumns = true;
            dgvConsoles.DataSource = lstConsoles;

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

        void UpdateConsoleAllGames(object sender, EventArgs e)
        {
            var console = lstConsoles.Where(x => x.Name == "All Games");
            console.First().NumGames = lstConsoles.Except(console).Sum(x => x.NumGames);
            console.First().TotalGames = lstConsoles.Except(console).Sum(x => x.TotalGames);
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
            lstConsoles.Clear();
            DisablePanelConsoles();

            lstConsoles.AddRange(new ListBind<Console>(await Console.List()));
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

            lblOutput.Text = "[" + DateTime.Now.ToTimeLong() + "] " + "Consoles Updated!" + Environment.NewLine + lblOutput.Text;
        }

        void dgvConsoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            Console ConsoleSelected = dgvGetCurrentItem<Console>(sender);
            if (SESSION.Console.IsNull() || ConsoleSelected.ID != SESSION.Console.ID)
                SESSION.Console = ConsoleSelected;
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

        #region Games
        void Games_Init()
        {
            SESSION.ConsoleChanged += LoadGames;

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

        void Games_Shown(object sender, EventArgs e)
        {
            Browser.dlGames.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            Browser.dlGamesIcon.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            Browser.dlGamesBadges.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);

            lstDgvGames.Add(dgvGames);

            ResetGamesLabels();

            //await LoadGames(sender, e);
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

        void ResetGamesLabels()
        {
            lblUpdateGameList.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            pgbGameList.Value = 0;
            pgbGameList.Visible = false;
            txtSearchGames.Text = string.Empty;
        }

        async Task LoadGames()
        {
            if (SESSION.Console.IsNull()) { return; }

            lstGamesSearch.Clear();
            ResetGamesLabels();
            UpdateConsoleLabels();

            tabMain.SelectedTab = tabGames;

            DisablePanelGames();
            dgvGames.Columns["gConsole"].Visible = SESSION.Console.ID == 0;
            lstGames = new ListBind<Game>(await Game.Search(SESSION.Console.ID));

            //Update GameList
            if (lstGames.Count == 0 && !File.Exists(Folder.GameData + SESSION.Console.Name + ".json"))
            {
                btnUpdateGameList_Click(null, null);
            }

            txtSearchGames_TextChanged(null, null);
            EnablePanelGames();

            dgvGames.Focus();
        }

        async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (SESSION.Console.IsNull()) { MessageBox.Show("No Console Selected"); return; }

            DisablePanelGames();
            lstGamesSearch.Clear();
            //Download GameList
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGameList(SESSION.Console);
            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;

            //Download game icons
            TimeSpan ini1 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGamesIcon(SESSION.Console, Browser.dlGamesIcon);
            TimeSpan fim1 = new TimeSpan(DateTime.Now.Ticks) - ini1;

            //Load Games
            TimeSpan ini2 = new TimeSpan(DateTime.Now.Ticks);
            await LoadGames();
            await LoadGamesToPlay();
            await LoadGamesToHide();

            //UpdateConsoleLabels();
            TimeSpan fim2 = new TimeSpan(DateTime.Now.Ticks) - ini2;

            //Update ConsoleBind
            SESSION.Console.NumGames = lstGames.Count(g => g.NumAchievements > 0);
            SESSION.Console.TotalGames = lstGames.Count();

            lblOutput.Text = "[" + DateTime.Now.ToTimeLong() + "] " + SESSION.Console.Name + " Updated!" + Environment.NewLine + lblOutput.Text;

            GamesUpdated = true;
        }

        void UpdateConsoleLabels()
        {
            if (SESSION.Console == null) return;

            //Update Console
            var numGames = lstGamesSearch.Count(g => g.NumAchievements > 0);
            var totalGames = lstGamesSearch.Count();
            lblConsoleName.Text = SESSION.Console.Name;
            lblConsoleGamesTotal.Text = numGames + " of " + totalGames + " Games";
        }

        async void txtSearchGames_TextChanged(object sender, EventArgs e)
        {
            //if (txtSearchGames.Text.Count() > 0 && txtSearchGames.Text.Count() < 3) { return; }

            ListBind<Game> newSearch = new ListBind<Game>();
            foreach (Game obj in lstGames)
            {
                bool title;

                title = (obj.Title.HasValue() && obj.Title.ContainsExtend(txtSearchGames.Text));
                bool noCheevos = !chkWithoutAchievements.Checked && obj.NumAchievements == 0;

                bool official = chkOfficial.Checked && obj.Title.NotContains(RA.GameType.NotOfficial);

                bool proto = chkPrototype.Checked && obj.Title.ContainsExtend(RA.GameType.Prototype);
                bool unl = chkUnlicensed.Checked && obj.Title.ContainsExtend(RA.GameType.Unlicensed);
                bool demo = chkDemo.Checked && obj.Title.ContainsExtend(RA.GameType.Demo);
                bool hack = chkHack.Checked && obj.Title.ContainsExtend(RA.GameType.Hack);
                bool homebrew = chkHomebrew.Checked && obj.Title.ContainsExtend(RA.GameType.Homebrew);
                bool subset = chkSubset.Checked && obj.Title.ContainsExtend(RA.GameType.Subset);
                bool testkit = chkTestKit.Checked && obj.Title.ContainsExtend(RA.GameType.TestKit);
                bool demoted = chkDemoted.Checked && obj.Title.ContainsExtend(RA.GameType.Demoted);

                if (title && !noCheevos)
                {
                    if (official || proto || unl || demo || hack || homebrew || subset || testkit || demoted)
                    {
                        newSearch.Add(obj);
                    }
                }
            }

            lstGamesSearch = newSearch;
            UpdateConsoleLabels();
            dgvGames.DataSource = lstGamesSearch;

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

            await LoadGamesIcon();
            dgvGames.Refresh();
            //EnablePanelGames();
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

        async void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            GameBind = dgvGetCurrentItem<Game>(sender);
            f.GameBind = GameBind;

            pnlInfoScroll.AutoScrollPosition = new Point(pnlInfoScroll.AutoScrollPosition.X, 0);
            pnlInfoScroll.VerticalScroll.Value = 0;

            //LoadGameExtendBase();
            //await LoadGameExtend();

            //Update GameExtend
            if (GameExtendBind.IsNull() || GameExtendBind.ConsoleID == 0)
            {
                f.btnUpdateInfo_Click(null, null);
            }

            tabMain.SelectedTab = tabGameInfo;

            dgvAchievements.Focus();
        }

        async Task LoadGamesIcon()
        {
            foreach (DataGridView dgv in lstDgvGames)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        if (dgv.DataSource.IsNull() || dgv.RowCount == 0) { return; }

                        ListBind<Game> list = (ListBind<Game>)(dgv.DataSource);
                        int index = dgv.FirstDisplayedScrollingRowIndex;

                        int nItems = (int)Math.Ceiling((double)(dgv.Height - 29) / 37) + 12;

                        for (int i = index; i < index + nItems; i++)
                        {
                            if (i >= list.Count) { break; }

                            list[i].SetImageIconBitmap();
                        }
                    });
                }
                catch (Exception e)
                { var error = e.Message; }

                dgv.Refresh();
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
                lstGamesToPlay.Insert(0, game);
                await LoadGamesIcon();
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
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
                lstGamesToHide.Insert(0, game);
                await LoadGamesIcon();
                lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
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

        #region GameInfo
        void GameInfo_Init()
        {
            RALoggedChanged += GameInfo_Login;

            f.Shown += GameInfo_Shown;

            btnGamePage.Click += OnButtonGamePageClicked;
            btnHashes.Click += OnButtonHashesClicked;
        }

        void GameInfo_Shown(object sender, EventArgs e)
        {
            Browser.dlGameExtend.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
            Browser.dlGameExtendImages.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);

            btnHashes.Enabled = false;
        }

        void GameInfo_Login(object sender, EventArgs e)
        {
            btnHashes.Enabled = RALogged;
        }

        void OnButtonGamePageClicked(object sender, EventArgs e)
        {
            if (GameBind.IsNull()) { return; }
            Process.Start(RA.Game_URL(GameBind.ID));
        }

        void OnButtonHashesClicked(object sender, EventArgs e)
        {
            if (GameBind.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            HashViewer.Open(GameBind);
        }
        #endregion

        #region GamesToPlay
        void GamesToPlay_Init()
        {
            f.Shown += GamesToPlay_Shown;
            mniRemoveGameToPlay.MouseDown += mniRemoveGameToPlay_MouseDown;

            dgvGamesToPlay.AutoGenerateColumns = false;
            dgvGamesToPlay.DataSource = lstGamesToPlay;

            dgvGamesToPlay.CellPainting += dgvGames_CellPainting;
            dgvGamesToPlay.MouseDown += (sender, e) => dgvGamesToPlay.ShowContextMenu(e, mnuGamesToPlay);

            //dgvGamesToPlay.CellDoubleClick += dgvGames_CellDoubleClick;
            //dgvGamesToPlay.MouseWheel += dgvGames_MouseWheel;
            //dgvGamesToPlay.Scroll += dgvGames_Scroll;
            //dgvGamesToPlay.Sorted += dgvGames_Sorted;

            lstDgvGames.Add(dgvGamesToPlay);
        }

        async void GamesToPlay_Shown(object sender, EventArgs e)
        {
            await LoadGamesToPlay();
        }

        async Task LoadGamesToPlay()
        {
            lstGamesToPlay.Clear();
            lstGamesToPlay.AddRange(new ListBind<Game>(await Game.ListToPlay()));
            //dgvGamesToPlay.DataSource = lstGamesToPlay;
            lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
            await LoadGamesIcon();
            //dgvGamesToPlay.Refresh();
        }

        async void mniRemoveGameToPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGetCurrentItem<Game>(dgvGamesToPlay);

            if (await game.DeleteFromPlay())
            {
                if (SESSION.Console.NotNull() && SESSION.Console.ID == game.ConsoleID)
                {
                    lstGames.Insert(0, game);
                    lstGamesSearch.Insert(0, game);
                }

                lstGamesToPlay.Remove(game);
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
                await LoadGamesIcon();
            }
        }
        #endregion

        #region GamesToHide
        void GamesToHide_Init()
        {
            f.Shown += GamesToHide_Shown;
            mniRemoveGameToHide.MouseDown += mniRemoveGameToHide_MouseDown;

            dgvGamesToHide.AutoGenerateColumns = false;
            dgvGamesToHide.DataSource = lstGamesToHide;

            dgvGamesToHide.Columns.Format(CellStyle.StringCenter, 0);
            dgvGamesToHide.Columns.Format(CellStyle.Image, 1);
            dgvGamesToHide.Columns.Format(CellStyle.NumberCenter, 4, 5, 6);
            dgvGamesToHide.Columns.Format(CellStyle.DateCenter, 7);

            dgvGamesToHide.CellPainting += dgvGames_CellPainting;
            dgvGamesToHide.MouseDown += (sender, e) => dgvGamesToHide.ShowContextMenu(e, mnuGamesToHide);
            //dgvGamesToHide.CellDoubleClick += dgvGames_CellDoubleClick;
            //dgvGamesToHide.MouseWheel += dgvGames_MouseWheel;
            //dgvGamesToHide.Scroll += dgvGames_Scroll;
            //dgvGamesToHide.Sorted += dgvGames_Sorted;

            lstDgvGames.Add(dgvGamesToHide);
        }

        async void GamesToHide_Shown(object sender, EventArgs e)
        {
            await LoadGamesToHide();
        }

        async Task LoadGamesToHide()
        {
            lstGamesToHide.Clear();
            lstGamesToHide.AddRange(new ListBind<Game>(await Game.ListToHide()));

            await LoadGamesIcon();

            lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
            dgvGamesToHide.Refresh();
        }

        async void mniRemoveGameToHide_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgvGetCurrentItem<Game>(dgvGamesToHide);

            if (await game.DeleteFromHide())
            {
                if (SESSION.Console.NotNull() && SESSION.Console.ID == game.ConsoleID)
                {
                    lstGames.Insert(0, game);
                    lstGamesSearch.Insert(0, game);
                }

                lstGamesToHide.Remove(game);
                lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();

                await LoadGamesIcon();
            }
        }
        #endregion

        #region UserInfo
        void User_Init()
        {
            f.Shown += User_Shown;
            txtUsername.KeyDown += txtUsername_KeyDown;
            btnUserPage.Click += OnButtonUserPageClicked;
            lnkUserRank.LinkClicked += lnkUserRank_LinkClicked;
        }

        void User_Shown(object sender, EventArgs e)
        {
        }

        void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                f.btnGetUserInfo_Click(null, null);
            }
        }

        void OnButtonUserPageClicked(object sender, EventArgs e)
        {
            if (UserBind.ID > 0)
                Process.Start(RA.User_URL(txtUsername.Text));
        }

        void lnkUserRank_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (UserBind.RankInvalid) { return; }

            var rankOffset = (UserBind.Rank - 1) / 25 * 25;
            Process.Start(RA.HOST_URL + "globalRanking.php?s=5&t=2&o=" + rankOffset);
        }
        #endregion

        #region About
        void About_Init()
        {
            f.Shown += About_Shown;

            btnRALogin.Click += btnRALogin_Click;
            btnRAProfileAbout.Click += btnRAProfileAbout_Click;

            btnUserCheevos.Click += btnUserCheevos_Click;
        }

        void About_Shown(object sender, EventArgs e)
        {
            btnRALogin_Click(null, null);
        }

        async void btnRALogin_Click(object sender, EventArgs e)
        {
            lblRALogin.ForeColor = Color.Coral;
            lblRALogin.Text = "logging in...";

            btnRALogin.Enabled = false;
            RALogged = false;
            RALogged = await Browser.SystemLogin();
            btnRALogin.Enabled = true;

            if (Browser.RALogged)
            {
                lblRALogin.ForeColor = Color.Green;
                lblRALogin.Text = "logged in!";
            }
            else
            {
                lblRALogin.ForeColor = Color.Firebrick;
                lblRALogin.Text = "not logged in";
            }
        }

        void btnRAProfileAbout_Click(object sender, EventArgs e)
        {
            Process.Start(RA.User_URL("FBiDev"));
        }

        bool UserCheevosIsRunning;
        static UserProgress LastUser = new UserProgress();
        async void btnUserCheevos_Click(object sender, EventArgs e)
        {
            if (GameBind.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }
            if (UserCheevosIsRunning) { return; }

            UserCheevosIsRunning = true;
            btnUserCheevos.Enabled = false;
            lblUserCheevos.Text = string.Empty;

            do
            {
                UserProgress user = await RA.GetUserProgress(txtUsername.Text, GameBind.ID);
                picUserCheevos.Image = GameBind.ImageIconBitmap;
                lblUserCheevos.Text = user.NumAchieved + " / " + GameBind.NumAchievements;

                if (user.SameProgress(LastUser))
                {
                    lblCheevoLoopUpdate.BackColor = Color.Orange;
                }
                else
                {
                    lblCheevoLoopUpdate.BackColor = Color.LightGreen;
                    LastUser = user;
                }

                await Task.Run(() => { Thread.Sleep(500); });

                lblCheevoLoopUpdate.BackColor = Color.Transparent;

                await Task.Run(() => { Thread.Sleep(2500); });

            } while (chkUserCheevos.Checked);

            UserCheevosIsRunning = false;
            btnUserCheevos.Enabled = true;
        }
        #endregion

        #region Common
        T dgvGetCurrentItem<T>(object sender) where T : class
        {
            var dgv = sender as DataGridView;
            if (dgv.CurrentRow.NotNull())
                return dgv.CurrentRow.DataBoundItem as T;
            return null;
        }

        void dgv_KeyPress(object sender, KeyPressEventArgs e, string columnName)
        {
            DataGridView dgv = (DataGridView)sender;
            char typedChar = e.KeyChar;

            if (char.IsLetter(typedChar))
            {
                if (typedChar == (char)Keys.Left || typedChar == (char)Keys.Right ||
                    typedChar == (char)Keys.Up || typedChar == (char)Keys.Down)
                {
                    return;
                }

                for (int i = 0; i < (dgv.RowCount); i++)
                {
                    if (dgv.Rows[i].Cells[columnName].Value.ToString().StartsWith(typedChar.ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (dgv.Rows[dgv.CurrentRow.Index].Cells[columnName].Value.ToString().StartsWith(typedChar.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (i <= dgv.CurrentRow.Index) continue;
                        }

                        dgv.Rows[i].Cells[0].Selected = true;
                        return;
                    }
                }
            }
        }
        #endregion
    }
}