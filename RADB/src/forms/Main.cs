using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
//
using Newtonsoft.Json.Linq;
using RADB.Properties;
using GNX;

namespace RADB
{
    public partial class Main : Form
    {
        #region Init
        private RA RA = new RA();

        public static Console ConsoleBind = null;
        private Game GameBind = null;

        private Download dlConsoles;
        private Download dlGames;
        private Download dlGameInfo;
        private Download dlGamesIcon;

        public ListBind<Game> lstGames = new ListBind<Game>();
        public ListBind<Game> lstGamesSearch = new ListBind<Game>();

        public Main()
        {
            InitializeComponent();
            Icon = GNX.cConvert.ToIco(Resources.favicon, new Size(250, 250));

            Shown += RADB_Shown;
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

            txtSearchGames.TextChanged += txtSearchGames_TextChanged;
            txtSearchGames.KeyDown += txtSearchGames_KeyDown;

            btnDownloadBadges.Click += btnDownloadBadges_Click;

            //Reset placeholders
            lblProgressConsoles.Text = string.Empty;
            lblUpdateConsoles.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            lblUpdateGameList.Text = string.Empty;

            //Internet
            Browser.Load();
            //Folders
            Folder.CreateFolders();
        }

        private async void RADB_Shown(object sender, EventArgs e)
        {
            dlConsoles = new Download
            {
                Overwrite = true,
                ProgressBarName = pgbConsoles.Name,
                LabelBytesName = lblProgressConsoles.Name,
                LabelTimeName = lblUpdateConsoles.Name,
            };

            dlGames = new Download
            {
                Overwrite = true,
                ProgressBarName = pgbGameList.Name,
                LabelBytesName = lblProgressGameList.Name,
                LabelTimeName = lblUpdateGameList.Name,
            };

            dlGamesIcon = new Download()
            {
                Overwrite = false,
                ProgressBarName = pgbGameList.Name,
                LabelBytesName = lblProgressGameList.Name,
                LabelTimeName = lblUpdateGameList.Name,
            };

            dlGameInfo = new Download
            {
                Overwrite = true,
                ProgressBarName = pgbInfo.Name,
                LabelBytesName = lblProgressInfo.Name,
                LabelTimeName = lblUpdateInfo.Name,
            };

            await LoadConsoles();
            await LoadGames();
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
            // When window state changes
            if (WindowState != LastWindowState)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    // Maximized!
                    LoadGamesIcon();
                }
                if (WindowState == FormWindowState.Normal)
                {
                    // Restored!
                }
                LastWindowState = WindowState;
            }
        }
        #endregion

        #region Tab
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = (sender as TabControl);

            if (tab.SelectedTab == tabGames)
            {
                pnlGamesConsoleName.Visible = !ConsoleBind.IsNull();
                dgvGames.Focus(); return;
            }

            if (tab.SelectedTab == tabConsoles)
            {
                dgvConsoles.Focus(); return;
            }

            if (tab.SelectedTab == tabGameInfo)
            {
                pnlInfoScroll.Focus(); return;
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
        private void EnablePanelConsoles(bool enable)
        {
            pnlDownloadConsoles.Enabled = enable;

            lblNotFoundConsoles.Visible = false;
            picLoaderConsole.Visible = !enable;

            if (enable)
            {
                lblNotFoundConsoles.Visible = (dgvConsoles.RowCount == 0);
                lblUpdateConsoles.Text = Archive.LastUpdate(RA.ConsolesFile());
            }
            else
            {
                //dgvConsoles.DataSource = new List<Console>();
            }
        }

        private async Task LoadConsoles()
        {
            EnablePanelConsoles(false);
            dgvConsoles.DataSource = await Console.ListarBind();
            dgvConsoles.Focus();
            EnablePanelConsoles(true);
        }

        private void UpdateConsoleLabels()
        {
            if (ConsoleBind.NotNull())
            {
                lblConsoleName.Text = ConsoleBind.Name;
                lblConsoleGamesTotal.Text = ConsoleBind.NumGames + " of " + ConsoleBind.TotalGames + " Games";
            }
        }

        private async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            EnablePanelConsoles(false);
            await RA.DownloadConsoles(dlConsoles);
            await LoadConsoles();

            lblOutput.Text = "[" + DateTime.Now.ToLongTimeString() + "] " + "Consoles Updated!" + Environment.NewLine + lblOutput.Text;
        }

        private async void dgvConsoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            ConsoleBind = dgv_SelectionChanged<Console>(sender);
            UpdateConsoleLabels();

            lblUpdateGameList.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            pgbGameList.Value = 0;
            txtSearchGames.Text = string.Empty;

            tabMain.SelectedTab = tabGames;

            await LoadGames();

            //Update GameList
            if (lstGames.Count == 0)
            {
                btnUpdateGameList_Click(null, null);
            }
        }
        #endregion

        #region GameList
        private void EnablePanelGames(bool enable)
        {
            pnlDownloadGameList.Enabled = enable;
            txtSearchGames.Enabled = enable;

            lblNotFoundGameList.Visible = false;
            picLoaderGameList.Visible = !enable;

            dgvGames.Visible = enable;

            if (enable)
            {
                lblNotFoundGameList.Visible = (dgvGames.RowCount == 0);
                lblUpdateGameList.Text = Archive.LastUpdate(RA.GamesFile(ConsoleBind.Name));
            }
            else
            {
                
                //dgvGames.DataSource = new ListBind<Game>();
            }
        }

        private async Task LoadGames()
        {
            if (ConsoleBind.IsNull()) { return; }

            EnablePanelGames(false);

            lstGames = await Game.ListarBind(ConsoleBind.ID);
            lstGamesSearch = new ListBind<Game>();
            lstGamesSearch.AddRange(lstGames);

            dgvGames.DataSource = lstGamesSearch;
            dgvGames.Focus();
            EnablePanelGames(true);
        }

        private async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (ConsoleBind.IsNull()) { MessageBox.Show("No Console Selected"); return; }

            EnablePanelGames(false);

            //Download GameList
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGames(dlGames, ConsoleBind);
            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
            await LoadGames();

            //Update Console
            ListBind<Game> list = (ListBind<Game>)dgvGames.DataSource;
            ConsoleBind.NumGames = list.Sum(g => (g.NumAchievements > 0).ToInt());
            ConsoleBind.TotalGames = list.Count();

            UpdateConsoleLabels();

            dgvGames.Enabled = false;

            //Download game icons
            await RA.DownloadGamesIcon(dlGamesIcon, ConsoleBind);

            dgvGames.Enabled = true;
            dgvGames.Focus();
            //dgvGames_Sorted(null, null);
            dgvGames.Refresh();

            lblOutput.Text = "[" + DateTime.Now.ToLongTimeString() + "] " + ConsoleBind.Name + " GameList Updated!" + Environment.NewLine + lblOutput.Text;
        }

        private void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            GameBind = dgv_SelectionChanged<Game>(sender);

            lblInfoName.Text = GameBind.Title + " (" + GameBind.ConsoleName + ")";

            picInfoIcon.Image = GameBind.ImageIconBitmap;
            lblInfoDeveloper.Text = GameBind.Developer;
            lblInfoPublisher.Text = GameBind.Publisher;
            lblInfoGenre.Text = GameBind.Genre;
            lblInfoReleased.Text = GameBind.Released;

            picInfoTitle.Image = GameBind.ImageTitleBitmap;
            picInfoTitle.Size = GameBind.ImageTitlePicture.Scale(picInfoTitle.MaximumSize);
            picInfoInGame.Image = GameBind.ImageIngameBitmap;
            picInfoInGame.Size = GameBind.ImageIngamePicture.Scale(picInfoInGame.MaximumSize);

            FillAchievements(GameBind);

            tabMain.SelectedTab = tabGameInfo;
        }

        private void dgvGames_Scroll(object sender, ScrollEventArgs e)
        {
            if (wheel > 0 && wheel < 3) { wheel++; return; }
            wheel = 0;

            dgvGames.Focus();
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
            try
            {
                await Task.Run(() =>
                {
                    if (dgvGames.DataSource.IsNull()) { return; }

                    ListBind<Game> list = (ListBind<Game>)(dgvGames.DataSource);

                    int index = dgvGames.FirstDisplayedScrollingRowIndex;
                    if (list.Count == 0 || index < 0) { return; }

                    double nItems = Math.Ceiling((float)((float)(this.Size.Height - 293) / 37)) + 3;

                    for (int i = index; i < index + nItems; i++)
                    {
                        if (i >= list.Count) { return; }

                        Game g = list[i];
                        if (g.ImageIconBitmap == RA.DefaultIconImage.Bitmap)
                        {
                            g.ImageIconBitmap = new Picture(g.ImageIconPath, RA.ErrorIcon).Bitmap;
                        }
                    }
                });
            }
            catch (Exception) { }
            dgvGames.Refresh();
        }

        private void txtSearchGames_TextChanged(object sender, EventArgs e)
        {
            //if (txtSearchGames.Text.Count() > 0 && txtSearchGames.Text.Count() < 3) { return; }

            ListBind<Game> newSearch = new ListBind<Game>();
            foreach (Game obj in lstGames)
            {
                bool title;

                title = (obj.Title != null && (obj.Title.IndexOf(txtSearchGames.Text, StringComparison.CurrentCultureIgnoreCase) > -1));

                if (title)
                {
                    newSearch.Add(obj);
                }
            }
            dgvGames.DataSource = newSearch;

            LoadGamesIcon();
            dgvGames.Refresh();
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
        private void FillAchievements(Game obj)
        {
            int acLocation = 0;
            int size = 32;
            pnlAchievements.Controls.Clear();
            foreach (var ac in obj.AchievementsList)
            {
                Panel p = new Panel() { Height = size, Width = pnlAchievements.Width - 17, Location = new Point(0, acLocation) };
                PictureBox pic = new PictureBox() { Width = size, Height = size, Image = new Picture(size, size).Bitmap };
                p.Controls.Add(pic);
                Label title = new Label() { AutoSize = true, Text = ac.Title + " (" + ac.Points + ")" + " (" + ac.TrueRatio + ")", Location = new Point(pic.Width + 5, 0) };
                p.Controls.Add(title);
                Label description = new Label() { AutoSize = true, Text = ac.Description, Location = new Point(pic.Width + 5, 18) };
                p.Controls.Add(description);

                pnlAchievements.Controls.Add(p);

                acLocation += p.Height + 5;
            }
        }
        private async void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Download GameInfo Extend
            if (GameBind == null) { return; }

            dlGameInfo.File = RA.DownloadGameInfoExtended(GameBind);
            await dlGameInfo.Start();

            string FileGameInfoExtended = dlGameInfo.File.Path;
            lblUpdateInfo.Text = Archive.LastUpdate(FileGameInfoExtended).ToString();
            JObject resultInfo = Browser.ToJObject(FileGameInfoExtended);
            Game gameInfo = resultInfo.ToObject<Game>();

            GameBind.Developer = gameInfo.Developer;
            GameBind.Publisher = gameInfo.Publisher;
            GameBind.Genre = gameInfo.Genre;
            GameBind.Released = gameInfo.Released;

            lblInfoName.Text = GameBind.Title + " (" + GameBind.ConsoleName + ")";

            lblInfoDeveloper.Text = GameBind.Developer;
            lblInfoPublisher.Text = GameBind.Publisher;
            lblInfoGenre.Text = GameBind.Genre;
            lblInfoReleased.Text = GameBind.Released;

            List<DownloadFile> dlFiles = new List<DownloadFile>() {
                new DownloadFile(RA.URL_Images + gameInfo.ImageIcon, gameInfo.ImageIconPath),
                new DownloadFile(RA.URL_Images + gameInfo.ImageTitle, gameInfo.ImageTitlePath),
                new DownloadFile(RA.URL_Images + gameInfo.ImageIngame, gameInfo.ImageIngamePath),
            };

            dlGameInfo.Files = dlFiles;
            await dlGameInfo.Start();

            GameBind.ImageIcon = gameInfo.ImageIcon;
            GameBind.ImageTitle = gameInfo.ImageTitle;
            GameBind.ImageIngame = gameInfo.ImageIngame;

            picInfoIcon.Image = GameBind.ImageIconBitmap;
            picInfoTitle.Image = GameBind.ImageTitleBitmap;
            picInfoTitle.Size = GameBind.ImageTitlePicture.Scale(picInfoTitle.MaximumSize);
            picInfoInGame.Image = GameBind.ImageIngameBitmap;
            picInfoInGame.Size = GameBind.ImageIngamePicture.Scale(picInfoInGame.MaximumSize);

            GameBind.SetAchievements(resultInfo["Achievements"]);
            FillAchievements(GameBind);

            dgvGames.Refresh();
            pnlInfoScroll.Focus();
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

                for (int i = 0; i < (dgv.Rows.Count); i++)
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

        private async void btnUserCheevos_Click(object sender, EventArgs e)
        {
            if (GameBind.IsNull()) return;

            do
            {
                lblUserCheevos.Text = await Task<string>.Run(async () =>
                {
                    picUserCheevos.Image = GameBind.ImageIconBitmap;
                    UserProgress user = await RA.GetUserProgress(GameBind.ID);
                    return user.NumAchieved + " / " + GameBind.NumAchievements;
                });

                await Task.Run(() => { Thread.Sleep(5000); });

            } while (chkUserCheevos.Checked);
        }

        private async void btnDownloadBadges_Click(object sender, EventArgs e)
        {
            await RA.DownloadBadges(1);
        }
    }
}