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
using System.Drawing.Imaging;
using PhotoSauce.MagicScaler;
using Newtonsoft.Json;

namespace RADB
{
    public partial class Main : Form
    {
        #region Init
        private RA RA = new RA();

        public static Console ConsoleBind = null;
        private Game GameBind = null;
        private GameExtend GameExtendBind = null;

        private Download dlConsoles;
        private Download dlGames;
        private Download dlGamesIcon;

        private Download dlGameExtend;
        private Download dlGameExtendImages;

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

            dgvAchievements.AutoGenerateColumns = false;
            dgvAchievements.DataSourceChanged += dgvAchievements_DataSourceChanged;

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

        private void BarStart(ProgressBar bar, ProgressBarStyle style = ProgressBarStyle.Continuous, int maximum = 100)
        {
            bar.Maximum = maximum;
            bar.Value = 0;
            bar.MarqueeAnimationSpeed = 50;
            bar.Style = style;
        }

        private void BarStop(ProgressBar bar)
        {
            if (bar.Style == ProgressBarStyle.Marquee)
            {
                //bar.MarqueeAnimationSpeed = 0;
                bar.Style = ProgressBarStyle.Continuous;
            }

            //Hack for Win7
            bar.Maximum++;
            bar.Value = bar.Maximum;
            bar.Value--;
            bar.Maximum--;
        }


        private void DownloadChanged(Download download, Label resultLabel, ProgressBar resultBar, Label resultTime)
        {
            resultLabel.InvokeIfRequired(() =>
            {
                resultLabel.Text = download.Result;
            });

            switch (download.Status)
            {
                case Download.DownloadStatus.Connecting:

                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStart(resultBar, ProgressBarStyle.Marquee);
                    });
                    break;
                case Download.DownloadStatus.ProgressChanged:
                    resultBar.InvokeIfRequired(() =>
                    {
                        resultBar.Value = download.Percentage;
                        resultBar.Style = (download.BytesToReceive == -1 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous);
                    });
                    break;
                case Download.DownloadStatus.FileDownloaded:
                    break;
                case Download.DownloadStatus.NextFiles:
                    break;
                case Download.DownloadStatus.Completed:
                    resultTime.InvokeIfRequired(() =>
                    {
                        resultTime.Text = download.TimeCompleted.ToString();
                    });
                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStop(resultBar);
                    });
                    break;
                case Download.DownloadStatus.Stopped:
                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStop(resultBar);
                    });
                    break;
                default:
                    break;
            }
        }

        private async void RADB_Shown(object sender, EventArgs e)
        {
            dlConsoles = new Download
            {
                Overwrite = true,
                //ProgressBarName = pgbConsoles.Name,
                //LabelBytesName = lblProgressConsoles.Name,
                //LabelTimeName = lblUpdateConsoles.Name,
            };

            dlGames = new Download
            {
                Overwrite = true,
            };

            dlGames.ProgressChanged += () =>
                DownloadChanged(dlGames, lblProgressGameList, pgbGameList, lblUpdateGameList);

            dlGamesIcon = new Download()
            {
                Overwrite = false,
            };

            dlGamesIcon.ProgressChanged += () =>
                DownloadChanged(dlGamesIcon, lblProgressGameList, pgbGameList, lblUpdateGameList);

            dlGameExtend = new Download
            {
                Overwrite = true,
                //ProgressBarName = pgbInfo.Name,
                //LabelBytesName = lblProgressInfo.Name,
                //LabelTimeName = lblUpdateInfo.Name,
            };

            dlGameExtendImages = new Download
            {
                Overwrite = false,
                //ProgressBarName = pgbInfo.Name,
                //LabelBytesName = lblProgressInfo.Name,
                //LabelTimeName = lblUpdateInfo.Name,
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
                lblUpdateConsoles.Text = Archive.LastUpdate(RA.ConsolesPath());
            }
            else
            {
                dgvConsoles.DataSource = new List<Console>();
            }
        }

        private async Task LoadConsoles()
        {
            EnablePanelConsoles(false);
            dgvConsoles.DataSource = await Console.ListarBind();

            EnablePanelConsoles(true);
            dgvConsoles.Focus();
        }

        private void UpdateConsoleLabels()
        {
            if (ConsoleBind.NotNull())
            {
                //Update Console
                ListBind<Game> list = (ListBind<Game>)dgvGames.DataSource;
                var NumGames = list.Sum(g => (g.NumAchievements > 0).ToInt());
                var TotalGames = list.Count();

                lblConsoleName.Text = ConsoleBind.Name;
                lblConsoleGamesTotal.Text = NumGames + " of " + TotalGames + " Games";
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
                if (lstGames.Count == 0)
                {
                    btnUpdateGameList_Click(null, null);
                }

                UpdateConsoleLabels();
            }

            tabMain.SelectedTab = tabGames;
        }
        #endregion

        #region GameList
        private void EnablePanelGames(bool enable)
        {
            pnlDownloadGameList.Enabled = enable;
            txtSearchGames.Enabled = enable;

            lblNotFoundGameList.Visible = false;
            picLoaderGameList.Visible = !enable;

            //Black color in last row
            //dgvGames.Visible = enable;

            if (enable)
            {
                lblNotFoundGameList.Visible = (dgvGames.RowCount == 0);
                //lblUpdateGameList.Text = Archive.LastUpdate(RA.GameListPath(ConsoleBind.Name));
            }
            else
            {
                dgvGames.DataSource = new ListBind<Game>();
            }
        }

        private async Task LoadGames()
        {
            if (ConsoleBind.IsNull()) { return; }
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            EnablePanelGames(false);

            lstGames = await Game.ListarBind(ConsoleBind.ID);
            lstGamesSearch = new ListBind<Game>();
            //lstGamesSearch.Clear();
            lstGamesSearch.AddRange(lstGames);

            dgvGames.DataSource = lstGamesSearch;

            EnablePanelGames(true);
            dgvGames.Focus();

            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
        }

        private async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (ConsoleBind.IsNull()) { MessageBox.Show("No Console Selected"); return; }

            EnablePanelGames(false);

            //Download GameList
            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGameList(dlGames, ConsoleBind);
            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;

            //Download game icons
            TimeSpan ini1 = new TimeSpan(DateTime.Now.Ticks);
            await RA.DownloadGamesIcon(dlGamesIcon, ConsoleBind);
            TimeSpan fim1 = new TimeSpan(DateTime.Now.Ticks) - ini1;

            //Load Games
            TimeSpan ini2 = new TimeSpan(DateTime.Now.Ticks);
            await LoadGames();
            TimeSpan fim2 = new TimeSpan(DateTime.Now.Ticks) - ini2;

            UpdateConsoleLabels();

            //Update ConsoleBind
            ListBind<Game> list = (ListBind<Game>)dgvGames.DataSource;
            ConsoleBind.NumGames = list.Sum(g => (g.NumAchievements > 0).ToInt());
            ConsoleBind.TotalGames = list.Count();

            lblOutput.Text = "[" + DateTime.Now.ToLongTimeString() + "] " + ConsoleBind.Name + " GameList Updated!" + Environment.NewLine + lblOutput.Text;
        }

        private async void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            GameBind = dgv_SelectionChanged<Game>(sender);

            LoadGameExtendBase();
            await LoadGameExtend();

            //Update GameExtend
            if (GameExtendBind.ID == 0)
            {
                btnUpdateInfo_Click(null, null);
            }

            tabMain.SelectedTab = tabGameInfo;

            dgvAchievements.Focus();

            pnlInfoScroll.AutoScrollPosition = new Point(pnlInfoScroll.AutoScrollPosition.X, 0);
            pnlInfoScroll.VerticalScroll.Value = 0;
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
                        g.SetImageIconBitmap();
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
            UpdateConsoleLabels();
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
        }

        private async Task LoadGameExtend()
        {
            if (GameBind.IsNull()) { return; }

            GameExtendBind = await GameExtend.Listar(GameBind.ID);

            lblInfoDeveloper.Text = GameExtendBind.Developer;
            lblInfoPublisher.Text = GameExtendBind.Publisher;
            lblInfoGenre.Text = GameExtendBind.Genre;
            lblInfoReleased.Text = GameExtendBind.Released;

            GameExtendBind.SetImagesBitmap();

            picInfoTitle.ScaleTo(GameExtendBind.ImageTitleBitmap);
            picInfoInGame.ScaleTo(GameExtendBind.ImageIngameBitmap);
            picInfoBoxArt.ScaleTo(GameExtendBind.ImageBoxArtBitmap);

            ListBind<Achievement> lstCheevos = new ListBind<Achievement>();
            dgvAchievements.DataSource = lstCheevos;
            if (File.Exists(RA.GameExtendPath(GameBind)))
            {
                //gx.SetAchievements(resultInfo["Achievements"]);
                string AllText = File.ReadAllText(RA.GameExtendPath(GameBind));
                string cheevos = AllText.GetBetween("\"Achievements\":{", "}}");
                cheevos = "{" + cheevos + "}";

                JToken jcheevos = JsonConvert.DeserializeObject<JToken>(cheevos);

                GameExtendBind.SetAchievements(jcheevos);
                lstCheevos = new ListBind<Achievement>(GameExtendBind.AchievementsList);
                dgvAchievements.DataSource = lstCheevos;
            }
        }

        private async void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Download GameExtend
            if (GameBind == null) { return; }

            //Download GameExtend
            await RA.DownloadGameExtend(dlGameExtend, GameBind);

            //Download game images
            await RA.DownloadGameExtendImages(dlGameExtendImages, GameBind);

            //Load Game
            await LoadGameExtend();

            lblOutput.Text = "[" + DateTime.Now.ToLongTimeString() + "] Game " + GameBind.ID + " Updated!" + Environment.NewLine + lblOutput.Text;
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
            var height = dgvAchievements.RowTemplate.Height;
            gpbInfoAchievements.Height = (height * dgvAchievements.RowCount) + 25 + 30;
            dgvAchievements.Height = (height * dgvAchievements.RowCount) + 30;
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
                    return user.NumAchievedHardcore + " / " + GameBind.NumAchievements;
                });

                await Task.Run(() => { Thread.Sleep(1000); });

            } while (chkUserCheevos.Checked);
        }

        private async void btnDownloadBadges_Click(object sender, EventArgs e)
        {
            //await RA.DownloadBadges(1);

            var file = @"Data\Temp\W2.png";
            var file2 = @"Data\Temp\W2_RS2.png";
            var file3 = @"Data\Temp\W2_RS2_NEW.png";

            var file4 = @"Data\Temp\W2_RS0.png";

            var otp = await Task.Run(() =>
            {
                //var Encoder = new JpegEncoderOptions(98, ChromaSubsampleMode.Subsample444, true);
                var Encoder = new PngEncoderOptions(PngFilter.None, false);
                CropScaleMode rs = CropScaleMode.Stretch;

                MagicImageProcessor.ProcessImage(file, file2, new ProcessImageSettings
                {
                    Width = 96,
                    Height = 96,
                    ResizeMode = rs,
                    EncoderOptions = Encoder,
                });


                var b = new Bitmap(96 * 2, 96 * 2);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.White);

                    Bitmap image1 = new Bitmap(file);
                    Bitmap image2 = new Bitmap(file2);
                    Bitmap image4 = new Bitmap(file4);
                    g.DrawImage(image1, new Rectangle(0, 0, image1.Width, image1.Height));
                    g.DrawImage(image2, new Rectangle(0, image1.Height, image2.Width, image2.Height));
                    g.DrawImage(image4, new Rectangle(image2.Width, image1.Height, image4.Width, image4.Height));
                    image1.Dispose();
                    image2.Dispose();
                    image4.Dispose();
                }
                b.Save(file3, ImageFormat.Png);

                var pic = new Picture(file2, PictureFormat.Png);
                return pic.Compress();
            });
        }
    }
}