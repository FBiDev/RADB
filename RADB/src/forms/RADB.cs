using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RADB
{
    public partial class RADB : Form
    {
        #region Init
        private RA RA = new RA();
        private Download dlConsoles;
        private Download dlGameList;
        private Download dlGameInfoExtended;

        public RADB()
        {
            InitializeComponent();
            Shown += RADB_Shown;

            //RA.CheckLocalFiles();

            //Load Values
            dgvConsoles.AutoGenerateColumns = true;
            dgvGameList.AutoGenerateColumns = false;

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

        async void RADB_Shown(object sender, EventArgs e)
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
            LoadListConsoles();
            if (dgvConsoles.CurrentRow != null)
            {
                await LoadGameList(dgvConsoles.CurrentRow.DataBoundItem as Console);
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = (sender as TabControl);

            if (tab.SelectedTab == tabGames)
            {
                dgvGameList.Focus(); return;
            }

            if (tab.SelectedTab == tabConsoles)
            {
                dgvConsoles.Focus(); return;
            }
        }
        #endregion

        #region Consoles
        private void EnableConsoles(bool enable)
        {
            pnlDownloadConsoles.Enabled = enable;
            dgvConsoles.Visible = enable;
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
        }

        private void LoadListConsoles()
        {
            if (File.Exists(RA.FileConsoles()) == false) return;

            dgvConsoles.DataSource = RA.ListConsoles();
            EnableConsoles(true);
            dgvConsoles.Focus();
        }

        private async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            EnableConsoles(false);
            dlConsoles.File = RA.DownloadConsoles();
            await dlConsoles.Start();

            LoadListConsoles();


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
            await LoadGameList(obj);
        }
        #endregion

        #region GameList
        private async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (dgvConsoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("No Console Selected");
                return;
            }

            if (dgvConsoles.SelectedRows.Count > 0)
            {
                Console console = (dgvConsoles.CurrentRow.DataBoundItem as Console);

                pnlDownloadGameList.Enabled = false;

                //Download GameList
                string fileGameList = RA.FileGameList(console.Name);
                dlGameList.File = RA.DownloadGameList(console);
                await dlGameList.Start();

                //Read GameList
                List<Game> GameList = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(fileGameList));

                //Download Game Icons
                List<DownloadFile> gIconFiles = GameList.Select(a => new DownloadFile(RA.URL_Images + a.Icon.Name, a.Icon.Path)).ToList();
                Download dlIconFiles = new Download()
                {
                    Overwrite = false,
                    Files = gIconFiles,
                    ProgressBarName = pgbGameList.Name,
                    LabelBytesName = lblProgressGameList.Name,
                    LabelTimeName = lblUpdateGameList.Name,
                };
                await dlIconFiles.Start();

                pnlDownloadGameList.Enabled = true;

                if (GameList.Count > 0)
                {
                    await LoadGameList(dgvConsoles.CurrentRow.DataBoundItem as Console);
                }

                txtOutput.Text += console.Name + " GameList Updated!" + Environment.NewLine;
            }
        }

        private async Task LoadGameList(Console console)
        {
            if (dgvConsoles.SelectedRows.Count == 0) return;

            dgvGameList.DataSource = null;
            string FileGameList = RA.FileGameList(console.Name);

            lblGameListFound.Visible = false;
            if (File.Exists(FileGameList) == false)
            {
                lblGameListFound.Visible = true;
                return;
            }

            lblUpdateGameList.Text = Archive.LastUpdate(FileGameList).ToString();
            pnlDownloadGameList.Enabled = false;
            picLoaderGameList.Visible = true;

            //File exist
            List<Game> GameList = new List<Game>();
            await Task.Run(() =>
            {
                TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
                GameList = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(FileGameList));
                TimeSpan fim0 = new TimeSpan(DateTime.Now.Ticks) - ini0;

                List<Game> LCheevos = new List<Game>();
                List<Game> LNotOffical = new List<Game>();
                List<Game> LNoCheevos = new List<Game>();
                List<Game> LNotOfficalNoCheevos = new List<Game>();

                TimeSpan ini = new TimeSpan(DateTime.Now.Ticks);
                foreach (Game game in GameList)
                {
                    string infoFile = RA.JSN_GameInfoExtend(game.ConsoleID, game.ID);

                    if (File.Exists(infoFile) == false) continue;

                    JObject resultInfo = Browser.ToJObject(infoFile);
                    Game gameInfo = resultInfo.ToObject<Game>();

                    gameInfo.SetAchievements(resultInfo["Achievements"]);
                    game.AchievementsList = gameInfo.AchievementsList;

                    game.Developer = gameInfo.Developer;
                    game.Publisher = gameInfo.Publisher;
                    game.Genre = gameInfo.Genre;
                    game.Released = gameInfo.Released;

                    game.ImageTitle = gameInfo.ImageTitle;
                }

                List<string> prefixNotOffical = new List<string> { 
                        "~Demo~", "~Hack~", "~Homebrew~", "~Prototype~", "~Test Kit~", "~Unlicesed~", "~Z~" };

                //Get NotOffical
                LNotOffical = GameList.Where(x => prefixNotOffical.Any(y => x.Title.IndexOf(y) >= 0)).ToList();
                //Remove NotOffical from Main List
                GameList = GameList.Except(LNotOffical).ToList();
                //Get Game with no cheevos from NotOffical
                LNotOfficalNoCheevos = LNotOffical.Where(x => x.AchievementsCount == 0).ToList();
                //Get Games Has Cheevos
                LNotOffical = LNotOffical.Where(x => x.AchievementsCount > 0).ToList();
                //Get Game with no cheevos from Main List
                LNoCheevos = GameList.Where(x => x.AchievementsCount == 0).ToList();
                //Remove Games no Cheevos from Main List
                GameList = GameList.Except(LNoCheevos).ToList();

                TimeSpan fim = new TimeSpan(DateTime.Now.Ticks) - ini;
                //Join Ordered Lists
                GameList = GameList.OrderBy(x => x.Title).ToList();
                GameList.AddRange(LNotOffical.OrderBy(x => x.Title).ToList());
                GameList.AddRange(LNoCheevos.OrderBy(x => x.Title).ToList());
                GameList.AddRange(LNotOfficalNoCheevos.OrderBy(x => x.Title).ToList());
            });

            pnlDownloadGameList.Enabled = true;
            picLoaderGameList.Visible = false;

            TimeSpan ini2 = new TimeSpan(DateTime.Now.Ticks);
            dgvGameList.DataSource = GameList;
            dgvGameList.Focus();
            TimeSpan fim2 = new TimeSpan(DateTime.Now.Ticks) - ini2;
        }
        #endregion

        #region DataGrid
        private void dgvGameList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            Game obj = dgvGameList.CurrentRow.DataBoundItem as Game;
            lblInfoName.Text = obj.Title + " (" + obj.ConsoleName + ")";

            picInfoIcon.Image = obj.Icon.Bitmap;
            lblInfoDeveloper.Text = obj.Developer;
            lblInfoPublisher.Text = obj.Publisher;
            lblInfoGenre.Text = obj.Genre;
            lblInfoReleased.Text = obj.Released;

            picInfoTitle.Image = obj.TitleImage.Bitmap;
            picInfoTitle.Size = obj.TitleImage.Scale(picInfoTitle.MaximumSize);

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

            tabMain.SelectedTab = tabGameInfo;
        }


        #endregion

        #region GameInfo
        private async void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Download GameInfo Extend
            Game game = dgvGameList.CurrentRow.DataBoundItem as Game;
            if (game == null) { return; }

            dlGameInfoExtended.File = RA.DownloadGameInfoExtended(game);
            await dlGameInfoExtended.Start();

            string FileGameInfoExtended = dlGameInfoExtended.File.Path;
            lblUpdateInfo.Text = Archive.LastUpdate(FileGameInfoExtended).ToString();
            JObject resultInfo = Browser.ToJObject(FileGameInfoExtended);
            Game gameInfo = resultInfo.ToObject<Game>();

            game.SetAchievements(resultInfo["Achievements"]);
            game.Developer = gameInfo.Developer;
            game.ImageIcon = gameInfo.ImageIcon;
            game.ImageTitle = gameInfo.ImageTitle;

            lblInfoDeveloper.Text = game.Developer;

            List<DownloadFile> dlFiles = new List<DownloadFile>() {
                new DownloadFile(RA.URL_Images + game.Icon.Name, game.Icon.Path),
                new DownloadFile(RA.URL_Images + game.TitleImage.Name, game.TitleImage.Path),
            };

            dlGameInfoExtended.Files = dlFiles;
            await dlGameInfoExtended.Start();

            picInfoIcon.Image = game.Icon.Bitmap;
            picInfoTitle.Image = game.TitleImage.Bitmap;
            picInfoTitle.Size = game.TitleImage.Scale(picInfoTitle.MaximumSize);

            dgvGameList.Refresh();
        }
        #endregion
    }
}
