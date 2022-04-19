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
            await LoadListConsoles();
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

        private async Task LoadListConsoles()
        {
            if (File.Exists(RA.FileConsoles()) == false) return;
            EnableConsoles(false);

            dgvConsoles.DataSource = await RA.ListConsoles();
            dgvConsoles.Focus();

            EnableConsoles(true);
        }

        private async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            EnableConsoles(false);
            dlConsoles.File = RA.DownloadConsoles();
            await dlConsoles.Start();

            await LoadListConsoles();

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
        private void EnableGameList(bool enable)
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
                dgvGameList.DataSource = new List<Game>();
            }
        }

        private async Task LoadGameList(Console console)
        {
            if (console == null) return;
            if (File.Exists(RA.FileGameList(console.Name)) == false) return;

            EnableGameList(false);

            dgvGameList.DataSource = await RA.ListGameList(console);
            dgvGameList.Focus();

            EnableGameList(true);
        }

        private async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (dgvConsoles.RowCount == 0)
            {
                MessageBox.Show("No Console Selected");
                return;
            }

            EnableGameList(false);
            Console console = (dgvConsoles.CurrentRow.DataBoundItem as Console);

            //Download GameList
            //string fileGameList = RA.FileGameList(console.Name);
            dlGameList.File = RA.DownloadGameList(console);
            await dlGameList.Start();

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

            await LoadGameList(console);
            txtOutput.Text += console.Name + " GameList Updated!" + Environment.NewLine;
        }

        private void dgvGameList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            Game obj = dgvGameList.CurrentRow.DataBoundItem as Game;
            lblInfoName.Text = obj.Title + " (" + obj.ConsoleName + ")";

            picInfoIcon.Image = obj.ImageIconBitmap;
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
                new DownloadFile(RA.URL_Images + game.ImageIcon, game.ImageIconPath),
                new DownloadFile(RA.URL_Images + game.TitleImage.Name, game.TitleImage.Path),
            };

            dlGameInfoExtended.Files = dlFiles;
            await dlGameInfoExtended.Start();

            picInfoIcon.Image = game.ImageIconBitmap;
            picInfoTitle.Image = game.TitleImage.Bitmap;
            picInfoTitle.Size = game.TitleImage.Scale(picInfoTitle.MaximumSize);

            dgvGameList.Refresh();
        }
        #endregion
    }
}
