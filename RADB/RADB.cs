using System;
using System.Collections.Generic;
using System.Windows.Forms;
//
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace RADB
{
    public partial class RADB : Form
    {
        private List<int> IDs = new List<int>();

        //txts
        private int ID_value;
        private int ConsoleID_value;
        private string User_value;
        private int Count_value;
        private int Offset_value;
        private DateTime Date1_value;
        private DateTime Date2_value;

        public RADB()
        {
            InitializeComponent();
            Load += RADB_Load;
            Shown += RADB_Shown;
            Paint += RADB_Paint;
            Activated += RADB_Activated;

            //RA.CheckLocalFiles();

            //List<FileUpdate> objList = RA.FileToList<FileUpdate>(RA.Local_JsonFolder + RA.JSN_Consoles);
            //FileUpdate obj = RA.FindFileName(objList, RA.JSN_Consoles);

            //if (obj is object) { lblUpdateConsoles.Text = obj.Update.ToString(); }
            //Load Values
            //Reset placeholders
            lblProgressConsoles.Text = string.Empty;
            lblUpdateConsoles.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            lblUpdateGameList.Text = string.Empty;

            sts.Visible = true;
            //Internet
            Browser.Load();
            //Folders
            Folder.CreateFolders();
            //cbos
            cboConsoles.DisplayMember = "Name";
            cboConsoles.ValueMember = "ID";
            //dgvs
            LoadConsoles();
        }

        void RADB_Activated(object sender, EventArgs e)
        {

        }

        void RADB_Paint(object sender, PaintEventArgs e)
        {


        }

        void RADB_Load(object sender, EventArgs e)
        {

        }

        void RADB_Shown(object sender, EventArgs e)
        {
            dgvConsoles.Focus();
        }

        private void ParseValues()
        {
            int.TryParse(cboConsoles.SelectedValue.ToString(), out ConsoleID_value);

            int.TryParse(txtID.Text.Trim(), out ID_value);
            User_value = txtUser.Text.Trim();
            int.TryParse(txtCount.Text.Trim(), out Count_value);
            int.TryParse(txtOffset.Text.Trim(), out Offset_value);
            DateTime.TryParse(dtpDate1.Text.Trim(), out Date1_value);
            DateTime.TryParse(dtpDate2.Text.Trim(), out Date2_value);
        }

        private bool ValidID() { return ID_value > 0; }
        private bool ValidConsoleID() { return ConsoleID_value > 0; }



        private async void btnGameID_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }

            //StartBar(pgb.ProgressBar);
            //var game = await getGame(ID_value);
            //string c = File.ReadAllText(RA.FolderJson + RA.Update_JsonFile);
            //List<FileUpdate> f = Browser.ToObject<List<FileUpdate>>(c);
            var game = await Task.Run(() =>
            {
                return RA.GetGameInfoExtended(ID_value);
            });
            //StoptBar(pgb.ProgressBar);

            stsL1.Text = "Loaded " + game.AchievementsList.Count + " Achievements ";
        }

        private static Task<Game> getGame(int gameID)
        {
            return Task.Run(() => { return RA.GetGameInfoExtended(gameID); });
        }

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

        private void btnUpdateConsoles_Click_old(object sender, EventArgs e)
        {
            Download download = new Download
            {
                LabelTime = lblUpdateConsoles,
                LabelBytes = lblProgressConsoles,
                ProgressBar = pgbConsoles,
            };
            RA.UpdateConsolesFile(download);


            return;
            var URL = RA.GetRAURL("API_GetConsoleIDs.php");
            txtURL.Text = URL;


            //List<FileUpdate> f = JsonConvert.DeserializeObject<List<FileUpdate>>(File.ReadAllText(RA.Local_JsonFolder + RA.Update_JsonFile));

            //read file
            using (StreamReader r = new StreamReader(RA.FolderJson + RA.JSN_ConsoleIDs))
            {
                string json = r.ReadToEnd();
                List<Console> consoles = JsonConvert.DeserializeObject<List<Console>>(json);
                consoles = consoles.OrderBy(x => x.Name).ToList();

                txtOutput.Text = string.Empty;
                string textTotal = string.Empty;
                foreach (var console in consoles)
                {
                    cboConsoles.Items.Add(console);
                    textTotal += console.ID.ToString().PadLeft(2, ' ') + " : " + console.Name + Environment.NewLine;
                }
                textTotal = textTotal.ToString().TrimEnd('\r', '\n');
                txtOutput.Text = textTotal;
            }
        }

        private void btnGameList_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }
            txtURL.Text = RA.GetRAURL("API_GetConsoleIDs.php", "i=" + ID_value.ToString());

            using (StreamReader r = new StreamReader("src/rsc/API_GetGameList.json"))
            {
                string json = r.ReadToEnd();
                List<Game> games = JsonConvert.DeserializeObject<List<Game>>(json);

                games = games.OrderBy(x => x.Title).ToList();

                txtOutput.Text = string.Empty;
                string textTotal = string.Empty;
                IDs.Clear();

                foreach (var game in games)
                {
                    textTotal += game.ID.ToString().PadLeft(5, ' ') + " : " + game.Title + Environment.NewLine;
                    //textTotal += game.Title + Environment.NewLine;
                    IDs.Add(game.ID);
                }
                textTotal = textTotal.ToString().TrimEnd('\r', '\n');
                txtOutput.Text = textTotal;
            }
        }

        private void btnGamesGenre_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }

            string URL = RA.GetRAURL("API_GetGame.php", "i=");
            txtURL.Text = URL;

            string textTotal = string.Empty;

            foreach (var id in IDs)
            {
                var content = Browser.DownloadString(URL + id);

                Game game = JsonConvert.DeserializeObject<Game>(content);
                textTotal += game.ID.ToString().PadLeft(5, ' ') + " : " + game.Genre + Environment.NewLine;
            }
            txtOutput.Text = textTotal.TrimEnd('\r', '\n');
        }

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

        private async void btnUpdateGameList_Click(object sender, EventArgs e)
        {
            if (cboConsoles.Items.Count > 0)
            {
                ParseValues();
                if (!ValidConsoleID()) { return; }

                Console console = (cboConsoles.SelectedItem as Console);

                pnlDownloadGameList.Enabled = false;
                //Download GameList
                string fileGameList = RA.JSN_GameList(console.Name);
                Download dl = new Download(RA.GetRAURL(RA.API_GameList, "i=" + ConsoleID_value), fileGameList)
                {
                    Overwrite = true,
                    ProgressBarName = pgbGameList.Name,
                    LabelBytesName = lblProgressGameList.Name,
                    LabelTimeName = lblUpdateGameList.Name,
                };

                await dl.Start();

                //Read GameList
                List<Game> GameList = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(fileGameList));

                List<DownloadFile> gFiles = GameList.Select(a => new DownloadFile(RA.GetRAURL(RA.API_GameExtended, "i=" + a.ID), RA.JSN_GameInfoExtend(a.ConsoleID, a.ID))).ToList();
                foreach (var file in gFiles)
                {
                    Download dlInfoExtend = new Download(file.URL, file.Path)
                    {
                        Overwrite = false,
                        ProgressBarName = pgbGameList.Name,
                        LabelBytesName = lblProgressGameList.Name,
                        LabelTimeName = lblUpdateGameList.Name,
                    };
                    await dlInfoExtend.Start();
                }

                

                pnlDownloadGameList.Enabled = true;

                LoadGameList();

                txtOutput.Text += console.Name + " GameList Updated!" + Environment.NewLine;
            }
        }

        private void cboConsoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblUpdateGameList.Text = string.Empty;
            lblProgressGameList.Text = string.Empty;
            pgbGameList.Value = 0;

            LoadGameList();
        }

        private void LoadConsoles()
        {
            List<Console> Consoles = new List<Console>();
            string file = RA.JSN_ConsoleIDs;

            if (File.Exists(file))
            {
                lblUpdateConsoles.Text = Archive.LastUpdate(file).ToString();

                Consoles = JsonConvert.DeserializeObject<List<Console>>(File.ReadAllText(file));

                dgvConsoles.AutoGenerateColumns = true;
                dgvConsoles.DataSource = Consoles;
                dgvConsoles.Focus();

                cboConsoles.DataSource = Consoles;
            }
            dgvConsoles.Visible = File.Exists(file) && Consoles.Count > 0;
        }

        private void LoadGameList()
        {
            if (cboConsoles.Items.Count > 0 && cboConsoles.SelectedIndex >= 0)
            {
                List<Game> GameList = new List<Game>();
                string file = RA.JSN_GameList((cboConsoles.SelectedItem as Console).Name);

                if (File.Exists(file))
                {
                    lblUpdateGameList.Text = Archive.LastUpdate(file).ToString();

                    GameList = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(file));

                    dgvGameList.AutoGenerateColumns = false;
                    dgvGameList.DataSource = GameList;
                    dgvGameList.Focus();
                }
                dgvGameList.Visible = File.Exists(file) && GameList.Count > 0;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = (sender as TabControl);

            if (tab.SelectedTab == tabGameList)
            {
                dgvGameList.Focus(); return;
            }

            if (tab.SelectedTab == tabConsoles)
            {
                dgvConsoles.Focus(); return;
            }
        }
    }
}
