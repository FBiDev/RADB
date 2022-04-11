using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RADB
{
    public partial class RADB : Form
    {
        //txts
        private int ID_value;
        private int ConsoleID_value = 0;
        private string User_value;
        private int Count_value;
        private int Offset_value;
        private DateTime Date1_value;
        private DateTime Date2_value;

        public RADB()
        {
            InitializeComponent();
            Shown += RADB_Shown;

            //RA.CheckLocalFiles();

            //Load Values
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

        void RADB_Shown(object sender, EventArgs e)
        {
            //dgvs
            LoadConsoles();
            LoadGameList(dgvConsoles.CurrentRow.DataBoundItem as Console);
        }

        private void ParseValues()
        {
            int.TryParse(txtID.Text.Trim(), out ID_value);
            User_value = txtUser.Text.Trim();
            int.TryParse(txtCount.Text.Trim(), out Count_value);
            int.TryParse(txtOffset.Text.Trim(), out Offset_value);
            DateTime.TryParse(dtpDate1.Text.Trim(), out Date1_value);
            DateTime.TryParse(dtpDate2.Text.Trim(), out Date2_value);
        }

        private bool ValidID() { return ID_value > 0; }
        private bool ValidConsoleID() { return ConsoleID_value > 0; }

        private async void btnDownloadBadges_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }

            //await Task.Run(async () =>
            //{
            await RA.DownloadBadges(ID_value);
            //});
            txtOutput.Text = "Badges Downloaded!";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
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

        #region Consoles
        private async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            pnlDownloadConsoles.Enabled = false;
            Download dl = new Download(RA.GetRAURL(RA.API_ConsoleIDs), RA.JSN_ConsoleIDs)
            {
                Overwrite = true,
                ProgressBarName = pgbConsoles.Name,
                LabelBytesName = lblProgressConsoles.Name,
                LabelTimeName = lblUpdateConsoles.Name,
            };

            await dl.Start();
            pnlDownloadConsoles.Enabled = true;

            LoadConsoles();

            txtOutput.Text += "Consoles Updated!" + Environment.NewLine;
        }

        private void LoadConsoles()
        {
            List<Console> Consoles = new List<Console>();
            string file = RA.JSN_ConsoleIDs;

            lblConsolesFound.Visible = false;
            if (File.Exists(file) == false)
            {
                lblConsolesFound.Visible = true;
                return;
            }

            lblUpdateConsoles.Text = Archive.LastUpdate(file).ToString();

            Consoles = JsonConvert.DeserializeObject<List<Console>>(File.ReadAllText(file));
            Consoles = Consoles.OrderBy(x => x.Name).ToList();

            dgvConsoles.AutoGenerateColumns = true;
            dgvConsoles.DataSource = Consoles;
            dgvConsoles.Focus();
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
                string fileGameList = RA.JSN_GameList(console.Name);
                Download dlGameList = new Download(RA.GetRAURL(RA.API_GameList, "i=" + console.ID), fileGameList)
                {
                    Overwrite = true,
                    ProgressBarName = pgbGameList.Name,
                    LabelBytesName = lblProgressGameList.Name,
                    LabelTimeName = lblUpdateGameList.Name,
                };

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
                    LoadGameList(dgvConsoles.CurrentRow.DataBoundItem as Console);
                }

                txtOutput.Text += console.Name + " GameList Updated!" + Environment.NewLine;
            }
        }

        private void LoadGameList(Console console)
        {
            if (dgvConsoles.SelectedRows.Count == 0) return;

            dgvGameList.DataSource = null;
            string file = RA.JSN_GameList(console.Name);

            lblGameListFound.Visible = false;
            if (File.Exists(file) == false)
            {
                lblGameListFound.Visible = true;
                return;
            }

            //File exist
            List<Game> GameList = new List<Game>();
            lblUpdateGameList.Text = Archive.LastUpdate(file).ToString();

            TimeSpan ini0 = new TimeSpan(DateTime.Now.Ticks);
            GameList = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(file));
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

            TimeSpan ini2 = new TimeSpan(DateTime.Now.Ticks);

            //Join Ordered Lists
            GameList = GameList.OrderBy(x => x.Title).ToList();
            GameList.AddRange(LNotOffical.OrderBy(x => x.Title).ToList());
            GameList.AddRange(LNoCheevos.OrderBy(x => x.Title).ToList());
            GameList.AddRange(LNotOfficalNoCheevos.OrderBy(x => x.Title).ToList());

            dgvGameList.AutoGenerateColumns = false;
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

            tabControl1.SelectedTab = tabGameInfo;
        }

        private void dgvConsoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            Console obj = dgvConsoles.CurrentRow.DataBoundItem as Console;

            lblUpdateGameList.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            pgbGameList.Value = 0;

            LoadGameList(obj);

            tabControl1.SelectedTab = tabGames;
        }
        #endregion

        private async void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            //Download GameInfo Extend
            Game obj = dgvGameList.CurrentRow.DataBoundItem as Game;
            string fileJson = RA.JSN_GameInfoExtend(obj.ConsoleID, obj.ID);

            List<DownloadFile> dlFiles = new List<DownloadFile>() {
                new DownloadFile(RA.GetRAURL(RA.API_GameExtended, "i=" + obj.ID), fileJson),
                new DownloadFile(RA.URL_Images + obj.Icon.Name, obj.Icon.Path),
                new DownloadFile(RA.URL_Images + obj.TitleImage.Name, obj.TitleImage.Path),
            };

            Download dlInfoExtend = new Download()
            {
                Files = dlFiles,
                Overwrite = false,
                ProgressBarName = pgbInfo.Name,
                LabelBytesName = lblProgressInfo.Name,
                LabelTimeName = lblUpdateInfo.Name,
            };
            await dlInfoExtend.Start();

            Game objExtend = JsonConvert.DeserializeObject<Game>(File.ReadAllText(fileJson));
            obj = objExtend;
            dgvGameList.Refresh();
            lblUpdateInfo.Text = Archive.LastUpdate(fileJson).ToString();
            picInfoIcon.Image = obj.IconBitmap;
            picInfoTitle.Image = obj.TitleImage.Bitmap;
        }
    }
}
