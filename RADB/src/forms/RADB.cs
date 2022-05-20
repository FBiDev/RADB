using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//
using GNX;
using System.Net;
using System.Threading;

namespace RADB
{
    public partial class RADB : Form
    {
        #region Init
        private RA RA = new RA();
        private Download dlConsoles;
        private Download dlGameList;
        private Download dlGameInfoExtended;

        private Task UserCheevos;

        public RADB()
        {
            InitializeComponent();
            Shown += RADB_Shown;

            dgvConsoles.KeyPress += dgvConsoles_KeyPress;
            dgvConsoles.CellDoubleClick += dgvConsoles_CellDoubleClick;

            dgvGames.KeyPress += dgvGames_KeyPress;
            dgvGames.CellDoubleClick += dgvGames_CellDoubleClick;
            dgvGames.Scroll += dgvGames_Scroll;
            dgvGames.Sorted += dgvGames_Sorted;
            dgvGames.DataSourceChanged += dgvGames_Sorted;

            btnDownloadBadges.Click += btnDownloadBadges_Click;

            //RA.CheckLocalFiles();

            //Load Values
            dgvConsoles.AutoGenerateColumns = true;
            dgvGames.AutoGenerateColumns = false;

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

        private async void btnDownloadBadges_Click(object sender, EventArgs e)
        {
            await RA.DownloadBadges(1);
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

            dlGameList = new Download
            {
                Overwrite = true,
                ProgressBarName = pgbGameList.Name,
                LabelBytesName = lblProgressGameList.Name,
                LabelTimeName = lblUpdateGameList.Name,
            };

            dlGameInfoExtended = new Download
            {
                Overwrite = false,
                ProgressBarName = pgbInfo.Name,
                LabelBytesName = lblProgressInfo.Name,
                LabelTimeName = lblUpdateInfo.Name,
            };

            //dgvs
            await LoadConsoles();
            if (dgvConsoles.CurrentRow != null)
            {
                await LoadGames(dgvConsoles.CurrentRow.DataBoundItem as Console);
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = (sender as TabControl);

            if (tab.SelectedTab == tabGames)
            {
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
        #endregion

        #region Consoles
        private void EnablePanelConsoles(bool enable)
        {
            pnlDownloadConsoles.Enabled = enable;

            lblConsolesFound.Visible = enable;
            picLoaderConsole.Visible = !enable;
            if (enable)
            {
                string FileConsoles = RA.FileConsoles();
                if (File.Exists(FileConsoles))
                {
                    lblConsolesFound.Visible = !enable;
                    lblUpdateConsoles.Text = Archive.LastUpdate(FileConsoles).ToString();
                }
            }
            else
            {
                dgvConsoles.DataSource = new List<Console>();
            }
        }

        private async Task LoadConsoles()
        {
            if (File.Exists(RA.FileConsoles()) == false) return;
            EnablePanelConsoles(false);

            dgvConsoles.DataSource = await RA.ListConsoles();
            dgvConsoles.Focus();

            EnablePanelConsoles(true);
        }

        private async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            EnablePanelConsoles(false);
            dlConsoles.File = RA.DownloadConsoles();
            await dlConsoles.Start();

            await LoadConsoles();

            txtOutput.Text += "Consoles Updated!" + Environment.NewLine;
        }

        private async void dgvConsoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            lblUpdateGameList.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            pgbGameList.Value = 0;

            tabMain.SelectedTab = tabGames;

            Console obj = dgvConsoles.CurrentRow.DataBoundItem as Console;
            await LoadGames(obj);
        }
        #endregion

        #region GameList
        private void EnablePanelGames(bool enable)
        {
            pnlDownloadGameList.Enabled = enable;

            lblGameListFound.Visible = enable;
            picLoaderGameList.Visible = !enable;
            if (enable)
            {
                Console console = dgvConsoles.CurrentRow.DataBoundItem as Console;
                string FileGameList = RA.FileGameList(console.Name);
                if (File.Exists(FileGameList))
                {
                    lblGameListFound.Visible = !enable;
                    lblUpdateGameList.Text = Archive.LastUpdate(FileGameList).ToString();
                }
            }
            else
            {
                dgvGames.DataSource = new ListBind<Game>();
            }
        }

        private async Task LoadGames(Console console)
        {
            EnablePanelGames(false);

            if (console == null || File.Exists(RA.FileGameList(console.Name)) == false)
            {
                EnablePanelGames(true); return;
            }

            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            //ListBind<Game> list = await RA.ListGameList(console);
            ListBind<Game> list = await GameDao.Listar(new Game() { ConsoleID = console.ID });

            dgvGames.DataSource = list;
            dgvGames.Focus();

            EnablePanelGames(true);
            TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;
        }

        private async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (dgvConsoles.RowCount == 0)
            {
                MessageBox.Show("No Console Selected");
                return;
            }

            EnablePanelGames(false);
            Console console = (dgvConsoles.CurrentRow.DataBoundItem as Console);

            //Download GameList
            //string fileGameList = RA.FileGameList(console.Name);
            dlGameList.File = RA.DownloadGameList(console);
            await dlGameList.Start();

            bool excluidos = new Game() { ConsoleID = console.ID }.Excluir();
            ListBind<Game> games = (await RA.ListGameList(console));
            await GameDao.IncluirLista(games);
            //games.ToList().ForEach(g => g.Incluir());

            ////Read GameList
            //List<Game> GameList = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(fileGameList));

            ////dgvGameList.Visible = false;
            ////Download Game Icons
            //List<DownloadFile> gIconFiles = GameList.Select(g => new DownloadFile(RA.URL_Images + g.ImageIcon, g.ImageIconPath)).ToList();
            //Download dlIconFiles = new Download()
            //{
            //    Overwrite = false,
            //    Files = gIconFiles,
            //    ProgressBarName = pgbGameList.Name,
            //    LabelBytesName = lblProgressGameList.Name,
            //    LabelTimeName = lblUpdateGameList.Name,
            //};
            //await (dlIconFiles.Start());

            await LoadGames(console);
            txtOutput.Text = console.Name + " GameList Updated!" + Environment.NewLine + txtOutput.Text;
        }

        private void dgvGames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            Game obj = dgvGames.CurrentRow.DataBoundItem as Game;
            lblInfoName.Text = obj.Title + " (" + obj.ConsoleName + ")";

            picInfoIcon.Image = obj.ImageIconBitmap;
            lblInfoDeveloper.Text = obj.Developer;
            lblInfoPublisher.Text = obj.Publisher;
            lblInfoGenre.Text = obj.Genre;
            lblInfoReleased.Text = obj.Released;

            picInfoTitle.Image = obj.ImageTitleBitmap;
            picInfoTitle.Size = obj.ImageTitlePicture.Scale(picInfoTitle.MaximumSize);
            picInfoInGame.Image = obj.ImageIngameBitmap;
            picInfoInGame.Size = obj.ImageIngamePicture.Scale(picInfoInGame.MaximumSize);

            FillAchievements(obj);

            tabMain.SelectedTab = tabGameInfo;
        }

        private void dgvGames_Scroll(object sender, ScrollEventArgs e)
        {
            ListBind<Game> list = (ListBind<Game>)((DataGridView)sender).DataSource;
            int index = dgvGames.FirstDisplayedScrollingRowIndex;
            if (index < 0) { index = 0; }
            for (int i = index; i < index + 20; i++)
            {
                if (i == dgvGames.RowCount) { break; }
                if (list[i].ImageIconBitmap == Game.DefaultIconImage.Bitmap)
                {
                    if (File.Exists(list[i].ImageIconPath) && new FileInfo(list[i].ImageIconPath).Length > 0)
                    {
                        list[i].ImageIconBitmap = new Picture(list[i].ImageIconPath).Bitmap;
                    }
                }
            }
        }

        private void dgvGames_Sorted(object sender, EventArgs e)
        {
            dgvGames_Scroll(dgvGames, null);
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
            Game game = dgvGames.CurrentRow.DataBoundItem as Game;
            if (game == null) { return; }

            dlGameInfoExtended.File = RA.DownloadGameInfoExtended(game);
            await dlGameInfoExtended.Start();

            string FileGameInfoExtended = dlGameInfoExtended.File.Path;
            lblUpdateInfo.Text = Archive.LastUpdate(FileGameInfoExtended).ToString();
            JObject resultInfo = Browser.ToJObject(FileGameInfoExtended);
            Game gameInfo = resultInfo.ToObject<Game>();

            game.Developer = gameInfo.Developer;
            game.Publisher = gameInfo.Publisher;
            game.Genre = gameInfo.Genre;
            game.Released = gameInfo.Released;

            lblInfoName.Text = game.Title + " (" + game.ConsoleName + ")";

            lblInfoDeveloper.Text = game.Developer;
            lblInfoPublisher.Text = game.Publisher;
            lblInfoGenre.Text = game.Genre;
            lblInfoReleased.Text = game.Released;

            List<DownloadFile> dlFiles = new List<DownloadFile>() {
                new DownloadFile(RA.URL_Images + gameInfo.ImageIcon, gameInfo.ImageIconPath),
                new DownloadFile(RA.URL_Images + gameInfo.ImageTitle, gameInfo.ImageTitlePath),
                new DownloadFile(RA.URL_Images + gameInfo.ImageIngame, gameInfo.ImageIngamePath),
            };

            dlGameInfoExtended.Files = dlFiles;
            await dlGameInfoExtended.Start();

            game.ImageIcon = gameInfo.ImageIcon;
            game.ImageTitle = gameInfo.ImageTitle;
            game.ImageIngame = gameInfo.ImageIngame;

            picInfoIcon.Image = game.ImageIconBitmap;
            picInfoTitle.Image = game.ImageTitleBitmap;
            picInfoTitle.Size = game.ImageTitlePicture.Scale(picInfoTitle.MaximumSize);
            picInfoInGame.Image = game.ImageIngameBitmap;
            picInfoInGame.Size = game.ImageIngamePicture.Scale(picInfoInGame.MaximumSize);

            game.SetAchievements(resultInfo["Achievements"]);
            FillAchievements(game);

            dgvGames.Refresh();
            pnlInfoScroll.Focus();
        }
        #endregion

        private void dgv_KeyPress(DataGridView dgv, KeyPressEventArgs e, string columnName)
        {
            char typedChar;
            if (Char.IsLetter(e.KeyChar))
            {
                typedChar = e.KeyChar;

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
                        return; // stop looping
                    }
                }
            }
        }

        private void dgvConsoles_KeyPress(object sender, KeyPressEventArgs e)
        {
            dgv_KeyPress((DataGridView)sender, e, "cName");
        }

        private void dgvGames_KeyPress(object sender, KeyPressEventArgs e)
        {
            dgv_KeyPress((DataGridView)sender, e, "gTitle");
        }

        private async void btnUserCheevos_Click(object sender, EventArgs e)
        {
            if (dgvGames.CurrentRow.IsNull()) return;

            Game g = null;

            do
            {
                lblUserCheevos.Text = await Task<string>.Run(() =>
                {
                    if (g.NotNull()) Thread.Sleep(5000);

                    g = dgvGames.CurrentRow.DataBoundItem as Game;
                    picUserCheevos.Image = new Bitmap(g.ImageIconBitmap);
                    Game obj = RA.UserProgress(g.ID);
                    return obj.NumAchieved + " / " + g.NumAchievements;
                });
            } while (chkUserCheevos.Checked);
        }
    }
}
