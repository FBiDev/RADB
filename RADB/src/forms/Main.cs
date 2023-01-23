using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
//
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GNX;

namespace RADB
{
    public partial class Main : BaseForm
    {
        #region Init
        private RA RA = new RA();

        public static Console ConsoleBind = null;
        private Game GameBind = null;
        private GameExtend GameExtendBind = null;
        private User UserBind = new User();

        public ListBind<Game> lstGames = new ListBind<Game>();
        public ListBind<Game> lstGamesSearch = new ListBind<Game>();

        public ListBind<Game> lstGamesToHide = new ListBind<Game>();
        public ListBind<Game> lstGamesToPlay = new ListBind<Game>();

        public List<DataGridView> lstDgvGames = new List<DataGridView>();

        public ListBind<Achievement> lstAchievs = new ListBind<Achievement>();
        public ListBind<Achievement> lstAchievsSearch = new ListBind<Achievement>();

        public Main()
        {
            InitializeComponent();
            base.Init(this);

            //styles
            dgvConsoles.Columns.Format(CellStyle.StringCenter, 0);
            dgvConsoles.Columns.Format(CellStyle.NumberCenter, 3, 4);

            dgvGames.Columns.Format(CellStyle.StringCenter, 0);
            dgvGames.Columns.Format(CellStyle.Image, 1);
            dgvGames.Columns.Format(CellStyle.NumberCenter, 4, 5, 6);
            dgvGames.Columns.Format(CellStyle.DateCenter, 7);

            dgvGamesToHide.Columns.Format(CellStyle.StringCenter, 0);
            dgvGamesToHide.Columns.Format(CellStyle.Image, 1);
            dgvGamesToHide.Columns.Format(CellStyle.NumberCenter, 4, 5, 6);
            dgvGamesToHide.Columns.Format(CellStyle.DateCenter, 7);

            Load += Main_Load;
            Shown += Main_Shown;
            Resize += Main_Resize;

            //KeyPreview = true;
            //KeyDown += Main_KeyDown;
            tabMain.KeyDown += tabMain_KeyDown;

            dgvConsoles.AutoGenerateColumns = true;
            dgvConsoles.CellDoubleClick += dgvConsoles_CellDoubleClick;
            dgvConsoles.KeyPress += dgvConsoles_KeyPress;
            dgvConsoles.KeyDown += dgvConsoles_KeyDown;

            dgvGames.AutoGenerateColumns = false;
            dgvGames.DataSourceChanged += dgvGames_DataSourceChanged;
            dgvGames.CellDoubleClick += dgvGames_CellDoubleClick;
            dgvGames.KeyPress += dgvGames_KeyPress;
            dgvGames.KeyDown += dgvGames_KeyDown;

            dgvGames.MouseWheel += dgvGames_MouseWheel;
            dgvGames.Scroll += dgvGames_Scroll;
            dgvGames.Sorted += dgvGames_Sorted;

            dgvGames.CellPainting += dgvGames_CellPainting;
            dgvGamesToPlay.CellPainting += dgvGames_CellPainting;
            dgvGamesToHide.CellPainting += dgvGames_CellPainting;

            dgvGamesToPlay.AutoGenerateColumns = false;
            dgvGamesToPlay.CellDoubleClick += dgvGames_CellDoubleClick;
            dgvGamesToPlay.MouseWheel += dgvGames_MouseWheel;
            dgvGamesToPlay.Scroll += dgvGames_Scroll;
            dgvGamesToPlay.Sorted += dgvGames_Sorted;

            dgvGamesToHide.AutoGenerateColumns = false;
            dgvGamesToHide.CellDoubleClick += dgvGames_CellDoubleClick;
            dgvGamesToHide.MouseWheel += dgvGames_MouseWheel;
            dgvGamesToHide.Scroll += dgvGames_Scroll;
            dgvGamesToHide.Sorted += dgvGames_Sorted;

            dgvAchievements.AutoGenerateColumns = false;
            dgvAchievements.DataSourceChanged += dgvAchievements_DataSourceChanged;
            dgvAchievements.CellPainting += dgvAchievements_CellPainting;

            txtSearchGames.TextChanged += txtSearchGames_TextChanged;
            txtSearchGames.KeyDown += txtSearchGames_KeyDown;

            txtSearchAchiev.TextChanged += txtSearchAchiev_TextChanged;
            txtSearchAchiev.KeyDown += txtSearchAchiev_KeyDown;

            chkOfficial.CheckedChanged += chkUpdateDataGrid;
            chkPrototype.CheckedChanged += chkUpdateDataGrid;
            chkUnlicensed.CheckedChanged += chkUpdateDataGrid;
            chkDemo.CheckedChanged += chkUpdateDataGrid;
            chkHack.CheckedChanged += chkUpdateDataGrid;
            chkHomebrew.CheckedChanged += chkUpdateDataGrid;
            chkWithoutAchievements.CheckedChanged += chkUpdateDataGrid;
            chkSubset.CheckedChanged += chkUpdateDataGrid;
            chkTestKit.CheckedChanged += chkUpdateDataGrid;
            chkDemoted.CheckedChanged += chkUpdateDataGrid;

            txtUsername.KeyDown += txtUsername_KeyDown;

            //Reset placeholders
            lblProgressConsoles.Text = string.Empty;
            lblUpdateConsoles.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            lblUpdateGameList.Text = string.Empty;
            lblProgressInfo.Text = string.Empty;
            lblUpdateInfo.Text = string.Empty;

            //Internet
            Browser.Load();
            //Folders
            Folder.CreateFolders();
        }

        void Main_Load(object sender, EventArgs e)
        {
            var j = JsonConvert.DeserializeObject<JObject>("{\"LoadJsonDLL\":\"...\"}");
        }

        private async void Main_Shown(object sender, EventArgs e)
        {
            Browser.dlConsoles.SetControls(lblProgressConsoles, pgbConsoles, lblUpdateConsoles);
            Browser.dlConsolesGamesIcon.SetControls(lblProgressConsoles, pgbConsoles, lblUpdateConsoles);

            Browser.dlGames.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            Browser.dlGamesIcon.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);
            Browser.dlGamesBadges.SetControls(lblProgressGameList, pgbGameList, lblUpdateGameList);

            Browser.dlGameExtend.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);
            Browser.dlGameExtendImages.SetControls(lblProgressInfo, pgbInfo, lblUpdateInfo);

            lstDgvGames.Add(dgvGames);
            lstDgvGames.Add(dgvGamesToPlay);
            lstDgvGames.Add(dgvGamesToHide);

            btnSystemReLogin_Click(null, null);

            await LoadConsoles();
            await LoadGames();
            await LoadGamesToPlay();
            await LoadGamesToHide();

            //TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            //TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Modifiers == Keys.Alt;
        }

        FormWindowState? LastWindowState = null;
        private void Main_Resize(object sender, EventArgs e)
        {
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;
                if (WindowState == FormWindowState.Maximized)
                {
                    LoadGamesIcon();
                }
                else if (WindowState == FormWindowState.Normal) { }
            }
        }
        #endregion

        #region Tab
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = (sender as TabControl);

            if (tab.SelectedTab == tabConsoles)
            {
                dgvConsoles.Focus(); return;
            }
            else if (tab.SelectedTab == tabGames)
            {
                pnlGamesConsoleName.Visible = !ConsoleBind.IsNull();
                dgvGames.Focus(); return;
            }
            else if (tab.SelectedTab == tabGamesToPlay)
            {
                dgvGamesToPlay.Focus(); return;
            }
            else if (tab.SelectedTab == tabGamesToHide)
            {
                dgvGamesToHide.Focus(); return;
            }
            else if (tab.SelectedTab == tabGameInfo)
            {
                pnlInfoScroll.Focus(); return;
            }
            else if (tab.SelectedTab == tabUserInfo)
            {
                txtUsername.Focus(); return;
            }
        }

        private void tabMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Alt) { return; }
            if (e.KeyCode == Keys.Right && tabMain.SelectedIndex < tabMain.TabPages.Count) { tabMain.SelectedIndex += 1; }
            if (e.KeyCode == Keys.Left && tabMain.SelectedIndex > 0) { tabMain.SelectedIndex -= 1; }
        }
        #endregion

        #region Consoles
        private void EnablePanelConsoles(bool enable, bool resetDatagrid = true)
        {
            pnlDownloadConsoles.Enabled = enable;

            lblNotFoundConsoles.Visible = false;
            picLoaderConsole.Visible = !enable;

            if (resetDatagrid == false) { dgvConsoles.Enabled = enable; }

            if (enable)
            {
                lblNotFoundConsoles.Visible = (dgvConsoles.RowCount == 0);
            }
            else if (resetDatagrid)
            {
                dgvConsoles.DataSource = new List<Console>();
            }
        }

        private async Task LoadConsoles()
        {
            EnablePanelConsoles(false);
            dgvConsoles.DataSource = new ListBind<Console>(await Console.List());

            EnablePanelConsoles(true);
            dgvConsoles.Focus();
        }

        private void UpdateConsoleLabels()
        {
            if (ConsoleBind.NotNull())
            {
                //Update Console
                var numGames = lstGamesSearch.Sum(g => (g.NumAchievements > 0).ToInt());
                var totalGames = lstGamesSearch.Count();
                lblConsoleName.Text = ConsoleBind.Name;
                lblConsoleGamesTotal.Text = numGames + " of " + totalGames + " Games";
            }
        }

        private async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            EnablePanelConsoles(false);
            await RA.DownloadConsoles();
            await LoadConsoles();

            lblOutput.Text = "[" + DateTime.Now.ToLongTimeString() + "] " + "Consoles Updated!" + Environment.NewLine + lblOutput.Text;
        }

        private async void dgvConsoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            Console ConsoleSelected = dgv_SelectionChanged<Console>(sender);
            if (ConsoleBind.IsNull() || ConsoleSelected.ID != ConsoleBind.ID)
            {
                ConsoleBind = ConsoleSelected;

                lblUpdateGameList.Text = string.Empty;
                lblProgressGameList.Text = string.Empty;
                pgbGameList.Value = 0;
                txtSearchGames.Text = string.Empty;

                tabMain.SelectedTab = tabGames;

                await LoadGames();

                //Update GameList
                if (lstGames.Count == 0 || ConsoleBind.ID == 0 && !File.Exists(Folder.GameData + ConsoleBind.Name + ".json"))
                {
                    btnUpdateGameList_Click(null, null);
                }

                UpdateConsoleLabels();
            }

            tabMain.SelectedTab = tabGames;
        }
        #endregion

        #region GameList
        private void EnablePanelGames(bool enable, bool resetDatagrid = true)
        {
            pnlDownloadGameList.Enabled = enable;
            pnlGamesConsoleName.Enabled = enable;

            lblNotFoundGameList.Visible = false;
            picLoaderGameList.Visible = !enable;

            //Black color in last row
            //dgvGames.Visible = enable;

            if (resetDatagrid == false) { dgvGames.Enabled = enable; }

            if (enable)
            {
                lblNotFoundGameList.Visible = (dgvGames.RowCount == 0);
            }
            else if (resetDatagrid)
            {
                dgvGames.DataSource = new ListBind<Game>();
            }
        }

        private async Task LoadGames()
        {
            if (ConsoleBind.IsNull()) { return; }
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            EnablePanelGames(false);

            lstGames = new ListBind<Game>(await Game.Search(ConsoleBind.ID));
            lstGamesSearch = new ListBind<Game>();
            //lstGamesSearch.Clear();
            lstGamesSearch.AddRange(lstGames);

            //dgvGames.DataSource = lstGamesSearch;

            //Show console Column if is in All Games
            dgvGames.Columns["gConsole"].Visible = ConsoleBind.ID == 0;

            txtSearchGames_TextChanged(null, null);

            EnablePanelGames(true);
            dgvGames.Focus();

            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
        }

        private async Task LoadGamesToPlay()
        {
            lstGamesToPlay = new ListBind<Game>(await Game.ListToPlay());
            dgvGamesToPlay.DataSource = lstGamesToPlay;
            LoadGamesIcon();
            lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
            dgvGamesToPlay.Refresh();
        }

        private async Task LoadGamesToHide()
        {
            lstGamesToHide = new ListBind<Game>(await Game.ListToHide());
            dgvGamesToHide.DataSource = lstGamesToHide;
            LoadGamesIcon();
            lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
            dgvGamesToHide.Refresh();
        }

        private async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (ConsoleBind.IsNull()) { MessageBox.Show("No Console Selected"); return; }

            EnablePanelGames(false);

            //Download GameList
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGameList(ConsoleBind);
            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;

            //Download game icons
            TimeSpan ini1 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGamesIcon(ConsoleBind, Browser.dlGamesIcon);
            TimeSpan fim1 = new TimeSpan(DateTime.Now.Ticks) - ini1;

            //Load Games
            TimeSpan ini2 = new TimeSpan(DateTime.Now.Ticks);
            await LoadGames();
            await LoadGamesToPlay();
            await LoadGamesToHide();

            UpdateConsoleLabels();
            TimeSpan fim2 = new TimeSpan(DateTime.Now.Ticks) - ini2;
            //Update ConsoleBind
            ConsoleBind.NumGames = lstGames.Sum(g => (g.NumAchievements > 0).ToInt());
            ConsoleBind.TotalGames = lstGames.Count();

            lblOutput.Text = "[" + DateTime.Now.ToLongTimeString() + "] " + ConsoleBind.Name + " Updated!" + Environment.NewLine + lblOutput.Text;
        }

        private async void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            GameBind = dgv_SelectionChanged<Game>(sender);

            pnlInfoScroll.AutoScrollPosition = new Point(pnlInfoScroll.AutoScrollPosition.X, 0);
            pnlInfoScroll.VerticalScroll.Value = 0;

            LoadGameExtendBase();
            await LoadGameExtend();

            //Update GameExtend
            if (GameExtendBind.IsNull() || GameExtendBind.ConsoleID == 0)
            {
                btnUpdateInfo_Click(null, null);
            }

            tabMain.SelectedTab = tabGameInfo;

            dgvAchievements.Focus();
        }

        private void dgvGames_Scroll(object sender, ScrollEventArgs e)
        {
            if (wheel > 0 && wheel < 3) { wheel++; return; }
            wheel = 0;

            ((DataGridView)sender).Focus();
            LoadGamesIcon();
        }

        int wheel = 0;
        private void dgvGames_MouseWheel(object sender, MouseEventArgs e)
        {
            wheel = 1;
        }

        private void dgvGames_DataSourceChanged(object sender, EventArgs e)
        {
            LoadGamesIcon();
        }

        private void dgvGames_Sorted(object sender, EventArgs e)
        {
            LoadGamesIcon();
        }

        private async void LoadGamesIcon()
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
                catch (Exception) { }
                dgv.Refresh();
            }
        }

        private void txtSearchGames_TextChanged(object sender, EventArgs e)
        {
            //if (txtSearchGames.Text.Count() > 0 && txtSearchGames.Text.Count() < 3) { return; }

            ListBind<Game> newSearch = new ListBind<Game>();
            foreach (Game obj in lstGames)
            {
                bool title;

                title = (obj.Title != null && (obj.Title.IndexOf(txtSearchGames.Text, StringComparison.CurrentCultureIgnoreCase) > -1));
                bool noCheevos = !chkWithoutAchievements.Checked && obj.NumAchievements == 0;

                bool official = chkOfficial.Checked
                    && obj.Title.IndexOf("~Prototype~") == -1
                    && obj.Title.IndexOf("~Unlicensed~") == -1
                    && obj.Title.IndexOf("~Demo~") == -1
                    && obj.Title.IndexOf("~Hack~") == -1
                    && obj.Title.IndexOf("~Homebrew~") == -1
                    && obj.Title.IndexOf("[Subset") == -1
                    && obj.Title.IndexOf("~Test") == -1
                    && obj.Title.IndexOf("~Z~") == -1;

                bool proto = chkPrototype.Checked && obj.Title.IndexOf("~Prototype~") >= 0;
                bool unl = chkUnlicensed.Checked && obj.Title.IndexOf("~Unlicensed~") >= 0;
                bool demo = chkDemo.Checked && obj.Title.IndexOf("~Demo~") >= 0;
                bool hack = chkHack.Checked && obj.Title.IndexOf("~Hack~") >= 0;
                bool homebrew = chkHomebrew.Checked && obj.Title.IndexOf("~Homebrew~") >= 0;
                bool subset = chkSubset.Checked && obj.Title.IndexOf("[Subset") >= 0;
                bool testkit = chkTestKit.Checked && obj.Title.IndexOf("~Test") >= 0;
                bool demoted = chkDemoted.Checked && obj.Title.IndexOf("~Z~") >= 0;

                if (title && !noCheevos)
                {
                    if (official || proto || unl || demo || hack || homebrew || subset || testkit || demoted)
                    {
                        newSearch.Add(obj);
                    }
                }
            }

            int scrollPosition = dgvGames.FirstDisplayedScrollingRowIndex;
            lstGamesSearch = newSearch;
            dgvGames.DataSource = lstGamesSearch;

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

            LoadGamesIcon();
            dgvGames.Refresh();
            UpdateConsoleLabels();

            EnablePanelGames(true);
        }

        private void txtSearchGames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dgvGames.Focus();
            }
        }
        #endregion

        #region GameInfo
        private void LoadGameExtendBase()
        {
            if (GameBind.IsNull()) { return; }

            lblInfoName.Text = GameBind.Title + " (" + GameBind.ConsoleName + ")";
            picInfoIcon.Image = GameBind.ImageIconBitmap;

            lblInfoAchievements.Text = GameBind.NumAchievements.ToString() + " Trophies: " + GameBind.Points + " points";
        }

        private async Task LoadGameExtend()
        {
            if (GameBind.IsNull()) { return; }

            GameExtendBind = await GameExtend.Find(GameBind.ID);

            lblInfoDeveloper1.Text = GameExtendBind.Developer;
            lblInfoPublisher1.Text = GameExtendBind.Publisher;
            lblInfoGenre1.Text = GameExtendBind.Genre;
            lblInfoReleased1.Text = GameExtendBind.Released;

            GameExtendBind.SetImagesBitmap();

            picInfoTitle.ScaleTo(GameExtendBind.ImageTitleBitmap);
            picInfoInGame.ScaleTo(GameExtendBind.ImageIngameBitmap);
            picInfoBoxArt.ScaleTo(GameExtendBind.ImageBoxArtBitmap);

            {//Scale Boxes
                pnlInfoTitle.Height = (picInfoTitle.Height > picInfoInGame.Height ? picInfoTitle.Height : picInfoInGame.Height);
                if (pnlInfoTitle.Height < pnlInfoImages.MinimumSize.Height - 12) pnlInfoTitle.Height = pnlInfoImages.MinimumSize.Height - 12;
                pnlInfoInGame.Height = pnlInfoTitle.Height;

                pnlInfoImages.Height = pnlInfoTitle.Height + 12;
                pnlInfoBoxArt.Height = pnlInfoImages.Location.Y + pnlInfoImages.Height - 19;

                picInfoBoxArt.MaximumSize = new Size(pnlInfoBoxArt.Width - 12, pnlInfoBoxArt.Height - 12);
                picInfoBoxArt.ScaleTo(GameExtendBind.ImageBoxArtBitmap);

                picInfoTitle.Location = new Point(pnlInfoTitle.Width / 2 - picInfoTitle.Width / 2, (pnlInfoTitle.Height / 2) - (picInfoTitle.Height / 2));
                picInfoInGame.Location = new Point(pnlInfoInGame.Width / 2 - picInfoInGame.Width / 2, (pnlInfoInGame.Height / 2) - (picInfoInGame.Height / 2));
                picInfoBoxArt.Location = new Point(pnlInfoBoxArt.Width / 2 - picInfoBoxArt.Width / 2, (pnlInfoBoxArt.Height / 2) - (picInfoBoxArt.Height / 2));

                gpbInfo.Height = gpbInfo.PreferredSize.Height - 13;
                gpbInfoAchievements.Location = new Point(gpbInfoAchievements.Location.X, (gpbInfo.Height - pnlInfoScroll.VerticalScroll.Value) + 9);
            }

            ListBind<Achievement> lstCheevos = new ListBind<Achievement>();
            dgvAchievements.DataSource = lstCheevos;
            if (File.Exists(RA.API_GameExtend(GameBind).Path))
            {
                //gx.SetAchievements(resultInfo["Achievements"]);
                string AllText = File.ReadAllText(RA.API_GameExtend(GameBind).Path);
                string cheevos = AllText.GetBetween("\"Achievements\":{", "}}");
                cheevos = "{" + cheevos + "}";

                JToken jcheevos = JsonConvert.DeserializeObject<JToken>(cheevos);

                GameExtendBind.SetAchievements(jcheevos);
                lstCheevos = new ListBind<Achievement>(GameExtendBind.AchievementsList);
                dgvAchievements.DataSource = lstCheevos;
            }
            lstAchievs = lstCheevos;
        }

        private async void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Download GameExtend
            if (GameBind.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            txtSearchAchiev.Enabled = false;
            //Download GameExtend
            await RA.DownloadGameExtend(GameBind, Browser.dlGameExtend);

            //Download game images
            await RA.DownloadGameExtendImages(GameBind);

            //Load Game
            await LoadGameExtend();
            txtSearchAchiev.Enabled = true;

            lblOutput.Text = "[" + DateTime.Now.ToLongTimeString() + "] Game " + GameBind.ID + " Updated!" + Environment.NewLine + lblOutput.Text;

            pnlInfoScroll.Focus();
        }

        private void txtSearchAchiev_TextChanged(object sender, EventArgs e)
        {
            ListBind<Achievement> newSearch = new ListBind<Achievement>();
            foreach (Achievement obj in lstAchievs)
            {
                bool title = (obj.Title != null && (obj.Title.IndexOf(txtSearchAchiev.Text, StringComparison.CurrentCultureIgnoreCase) > -1));
                bool desc = (obj.Description != null && (obj.Description.IndexOf(txtSearchAchiev.Text, StringComparison.CurrentCultureIgnoreCase) > -1));

                if (title || desc)
                {
                    newSearch.Add(obj);
                }
            }

            int scrollPosition = pnlInfoScroll.VerticalScroll.Value;

            lstAchievsSearch = newSearch;
            dgvAchievements.DataSource = lstAchievsSearch;

            if (scrollPosition < gpbInfo.Height + 4) scrollPosition = gpbInfo.Height + 4;
            if (scrollPosition > pnlInfoScroll.VerticalScroll.Maximum) scrollPosition = pnlInfoScroll.VerticalScroll.Maximum;

            bool maintainScroll = true;
            if (maintainScroll)
            {
                bool txtFocus = txtSearchAchiev.Focused;

                pnlInfoScroll.VerticalScroll.Value = scrollPosition;

                if (txtFocus) { txtSearchAchiev.Focus(); }
            }

            dgvAchievements.Refresh();
        }

        private void txtSearchAchiev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                dgvAchievements.Focus();
            }
        }
        #endregion

        private void dgv_KeyPress(object sender, KeyPressEventArgs e, string columnName)
        {
            DataGridView dgv = (DataGridView)sender;
            char typedChar = e.KeyChar;

            if (Char.IsLetter(typedChar))
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

        private void dgvConsoles_KeyPress(object sender, KeyPressEventArgs e)
        {
            dgv_KeyPress(sender, e, "cName");
        }

        private void dgvGames_KeyPress(object sender, KeyPressEventArgs e)
        {
            dgv_KeyPress(sender, e, "gTitle");
        }

        private void dgvGames_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                dgvGames_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
            }

            if (e.KeyData == Keys.Escape)
            {
                txtSearchGames.Focus();
            }

            if (e.KeyData == Keys.F5)
            {
                btnUpdateGameList_Click(null, null);
            }
        }

        private void dgvConsoles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                dgvConsoles_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
            }
        }

        private T dgv_SelectionChanged<T>(object sender) where T : class
        {
            if (((DataGridView)sender).CurrentRow.NotNull())
                return ((DataGridView)sender).CurrentRow.DataBoundItem as T;
            return null;
        }

        private void dgvAchievements_DataSourceChanged(object sender, EventArgs e)
        {
            dgvAchievements.Height = dgvAchievements.PreferredSize.Height - 16;
            gpbInfoAchievements.Height = gpbInfoAchievements.PreferredSize.Height - 13;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnGetUserInfo_Click(null, null);
            }
        }

        private async void btnGetUserInfo_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();

            btnGetUserInfo.Enabled = false;
            UserBind = await RA.GetUserInfo(txtUsername.Text.Trim());
            btnGetUserInfo.Enabled = true;
            if (UserBind.Name.Empty()) return;

            picUserName.Image = UserBind.UserPicBitmap;
            lblUserStatus.Text = UserBind.Status;
            lblUserName.Text = UserBind.Name;
            lblUserMotto.Text = UserBind.Motto;

            lblUserMemberSince.Text = UserBind.MemberSince.ToString("dd MMM yyyy, HH:mm");
            lblUserLastActivity.Text = UserBind.Lastupdate.ToString("dd MMM yyyy, HH:mm");
            lblUserAccountType.Text = UserBind.AccountType;

            lblUserHCPoints.Text = UserBind.TotalPoints.ToNumber(customLanguage: true) + " (" + UserBind.TotalTruePoints.ToNumber(customLanguage: true) + ")";
            lnkUserRank.Text = UserBind.GetRank();
            lnkUserRank.Size = lnkUserRank.PreferredSize;

            if (UserBind.Untracked || UserBind.Rank == null || UserBind.TotalPoints < RA.MIN_POINTS)
                lnkUserRank.LinkArea = new LinkArea(0, 0);
            else
                lnkUserRank.LinkArea = new LinkArea(0, UserBind.Rank.ToNumber(true).Count());

            lblUserRetroRatio.Text = UserBind.RetroRatio.ToNumber();
            lblUserSoftPoints.Text = UserBind.TotalSoftcorePoints.ToNumber(customLanguage: true);
            lblUserSoftRank.Text = UserBind.GetSoftRank();

            lblUserCompletion.Text = UserBind.AverageCompletion;

            if (UserBind.LastGame.ImageIcon.Empty() == false)
                picUserLastGame.Image = UserBind.LastGame.ImageIconBitmap;
            else
                picUserLastGame.Image = null;

            lblUserLastConsole.Text = UserBind.LastGame.ConsoleName;
            lblUserLastGame.Text = UserBind.LastGameTitle();
            lblUserRichPresence.Text = UserBind.RichPresence();

            //Reallocate RichPresence
            lblUserRichPresence.Location = new Point(lblUserRichPresence.Location.X, lblUserLastGame.Location.Y + lblUserLastGame.Size.Height + lblUserRichPresence.Margin.Top);

            //Awards
            pnlUserPlayedGames.Controls.Clear();

            var completedGames = UserBind.PlayedGames.Where(x => x.PctWon == 1.0);

            var dl = new Download() { Overwrite = false };
            dl.Files = completedGames.Select(x => x.ImageIconFile).ToList();
            await dl.Start();

            int i = 0, c = 0, r = 0;
            int perRow = 5;


            var images = new List<Bitmap>();
            var imagePaths = new List<string>();
            //var pics = new ImageList()
            //{
            //    ImageSize = new Size(52,52)
            //};
            foreach (var game in completedGames)
            {
                game.SetImageIconBitmap();

                var picBox = new FlatPictureBoxA()
                {
                    Margin = new Padding(3),
                    Size = new Size(48, 48),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Interpolation = InterpolationMode.HighQualityBicubic,
                    Image = game.ImageIconBitmap,
                    Location = new Point(c * 54, r * 54),
                };

                imagePaths.Add(game.ImageIconFile.Path);
                images.Add(game.ImageIconBitmap);

                //pics.Images.Add(game.ImageIconBitmap);

                var item = new ListViewItem()
                {
                    ImageIndex = i,
                    //IndentCount = 0
                    //ImageKey = game.GameID.ToString(),
                    //Text = game.Title,
                };

                //lsvGameAwards.Items.Add(item);
                pnlUserPlayedGames.Controls.Add(picBox);

                i++;
                c = (i % perRow);
                r = (i / perRow);
            }

            //lsvGameAwards.ImagePadding = 0;
            //lsvGameAwards.TileSize = new Size(56, 49);

            lsvGameAwards.TileSize = new Size(50, 50);
            lsvGameAwards.AddImageList(images, new Size(48, 48));

            
            //lsvGameAwards.View = View.Tile;
            //lsvGameAwards.TileSize = new Size(60, 60);
            //lsvGameAwards.LargeImageList = pics;
            //lsvGameAwards.SmallImageList = pics;
        }

        private bool UserCheevosIsRunning = false;
        private static UserProgress LastUser = new UserProgress();
        private async void btnUserCheevos_Click(object sender, EventArgs e)
        {
            if (GameBind.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }
            if (UserCheevosIsRunning) { return; };

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

        void dgvGames_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.Bilinear;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;

            if (e.ColumnIndex != 1 || e.RowHeader()) { return; }

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
        }

        void dgvAchievements_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowHeader() || e.Value == null || e.ColumnIndex != 2) { return; }

            DataGridView dgv = (DataGridView)sender;

            if (!e.Handled)
            {
                e.Handled = true;
                e.PaintBackground(e.CellBounds, dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected);
            }

            if ((e.PaintParts & DataGridViewPaintParts.ContentForeground) != DataGridViewPaintParts.None)
            {
                Achievement ach = (Achievement)dgv.Rows[e.RowIndex].DataBoundItem;

                Rectangle rect1 = new Rectangle(e.CellBounds.Location, e.CellBounds.Size);

                using (Font fnt = new Font(new FontFamily("Verdana"), 9.75f, FontStyle.Regular))
                {
                    using (Brush cellForeBrush = new SolidBrush(Theme.CheevoTitle))
                        e.Graphics.DrawString(ach.Title, fnt, cellForeBrush, rect1);

                    using (Brush cellForeBrush2 = new SolidBrush(Theme.CheevoDescription))
                        e.Graphics.DrawString(Environment.NewLine + ach.Description, fnt, cellForeBrush2, rect1);
                }
            }
        }

        private void chkUpdateDataGrid(object sender, EventArgs e)
        {
            txtSearchGames_TextChanged(null, null);
            dgvGames.Focus();
        }

        private void lnkUserRank_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var rank = UserBind.Rank;
            if (rank.GetValueOrDefault() == 0) { return; }

            var rankOffset = (int)((rank - 1) / 25) * 25;
            Process.Start(RA.HOST + "globalRanking.php?s=5&t=2&o=" + rankOffset);
        }

        private void btnGamePage_Click(object sender, EventArgs e)
        {
            if (GameBind.IsNull()) { return; }
            Process.Start(RA.Game_URL(GameBind.ID));
        }

        private void btnRaProfile_Click(object sender, EventArgs e)
        {
            Process.Start(RA.User_URL("FBiDev"));
        }

        private void dgvConsoles_MouseDown(object sender, MouseEventArgs e)
        {
            dgvShowContextMenu(sender, e, mnuConsoles);
        }

        private void dgvGames_MouseDown(object sender, MouseEventArgs e)
        {
            dgvShowContextMenu(sender, e, mnuGames);
        }

        private void dgvGamesToPlay_MouseDown(object sender, MouseEventArgs e)
        {
            dgvShowContextMenu(sender, e, mnuGamesToPlay);
        }

        private void dgvGamesToHide_MouseDown(object sender, MouseEventArgs e)
        {
            dgvShowContextMenu(sender, e, mnuGamesToHide);
        }

        private void dgvShowContextMenu(object sender, MouseEventArgs e, ContextMenuStrip menu)
        {
            DataGridView dgv = ((DataGridView)sender);
            int index = dgv.HitTest(e.X, e.Y).RowIndex;

            if (index > -1 && e.Button == MouseButtons.Right)
            {
                dgv.ClearSelection();
                dgv.CurrentCell = dgv.Rows[index].Cells[0];
                menu.Show(MousePosition);
            }
        }

        private async void mniPlayGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgv_SelectionChanged<Game>(dgvGames);

            if (await game.InsertToPlay())
            {
                lstGames.Remove(game);
                lstGamesSearch.Remove(game);
                lstGamesToPlay.Insert(0, game);

                LoadGamesIcon();
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
            }
        }

        private async void mniHideGame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgv_SelectionChanged<Game>(dgvGames);

            if (await game.InsertToHide())
            {
                //CurrencyManager cMnger = (CurrencyManager)BindingContext[dgvGames.DataSource];
                //cMnger.SuspendBinding();
                //dgvGames.SelectedRows[0].Visible = false;
                //cMnger.ResumeBinding();

                lstGames.Remove(game);
                lstGamesSearch.Remove(game);
                lstGamesToHide.Insert(0, game);

                LoadGamesIcon();
                lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
            }
        }

        private async void mniRemoveGameToPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgv_SelectionChanged<Game>(dgvGamesToPlay);

            if (await game.DeleteFromPlay())
            {
                lstGamesToPlay.Remove(game);
                if (ConsoleBind.NotNull() && ConsoleBind.ID == game.ConsoleID)
                {
                    lstGames.Insert(0, game);
                    lstGamesSearch.Insert(0, game);
                }

                LoadGamesIcon();
                lblNotFoundGamesToPlay.Visible = lstGamesToPlay.Empty();
            }
        }

        private async void mniRemoveGameToHide_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgv_SelectionChanged<Game>(dgvGamesToHide);

            if (await game.DeleteFromHide())
            {
                lstGamesToHide.Remove(game);
                if (ConsoleBind.NotNull() && ConsoleBind.ID == game.ConsoleID)
                {
                    lstGames.Insert(0, game);
                    lstGamesSearch.Insert(0, game);
                }

                LoadGamesIcon();
                lblNotFoundGamesToHide.Visible = lstGamesToHide.Empty();
            }
        }

        private async void mniMergeGameBadges_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var game = dgv_SelectionChanged<Game>(dgvGames);

            EnablePanelGames(false, false);
            await RA.MergeGameBadges(game);
            EnablePanelGames(true, false);
        }

        private async void mniMergeGamesIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var console = dgv_SelectionChanged<Console>(dgvConsoles);

            EnablePanelConsoles(false, false);
            await RA.MergeGamesIcon(console);
            EnablePanelConsoles(true, false);
        }

        private async void mniMergeGamesIconBadSize_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var console = dgv_SelectionChanged<Console>(dgvConsoles);

            EnablePanelConsoles(false, false);
            await RA.MergeGamesIcon(console, true);
            EnablePanelConsoles(true, false);
        }

        private void btnGameFilters_Click(object sender, EventArgs e)
        {
            pnlFilters.Visible = !pnlFilters.Visible;
            dgvGames.Focus();
        }

        private void btnHashes_Click(object sender, EventArgs e)
        {
            if (GameBind.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }

            HashViewer.Open(GameBind);
        }

        private async void btnSystemReLogin_Click(object sender, EventArgs e)
        {
            lblSystemReLogin.ForeColor = Color.Coral;
            lblSystemReLogin.Text = "logging in...";

            btnSystemReLogin.Enabled = false;
            btnHashes.Enabled = false;
            await Browser.SystemLogin();
            btnSystemReLogin.Enabled = true;
            btnHashes.Enabled = true;

            if (Browser.RALogged)
            {
                lblSystemReLogin.ForeColor = Color.Green;
                lblSystemReLogin.Text = "logged in!";
            }
            else
            {
                lblSystemReLogin.ForeColor = Color.Firebrick;
                lblSystemReLogin.Text = "not logged in";
            }
        }
    }
}