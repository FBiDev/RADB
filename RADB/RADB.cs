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

namespace RADB
{
    public partial class RADB : Form
    {
        private List<int> IDs = new List<int>();

        //txts
        private int ID_value;
        private string User_value;
        private int Count_value;
        private int Offset_value;
        private DateTime Date1_value;
        private DateTime Date2_value;

        public RADB()
        {
            InitializeComponent();
            Browser.WebStart();

            sts.Visible = true;

            cboConsoles.DisplayMember = "Name";
            cboConsoles.ValueMember = "ID";

            if (File.Exists(RA.Local_JsonFolder + RA.Update_JsonFile))
            {
                string r = File.ReadAllText(RA.Local_JsonFolder + RA.Update_JsonFile);
                List<FileUpdate> f = JsonConvert.DeserializeObject<List<FileUpdate>>(r);
                lblUpdateConsoles.Text = f[0].Name + ": " + f[0].Update.ToString();
            }
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

        private void StartBar(ToolStripProgressBar bar)
        {
            stsL1.Text = string.Empty;
            bar.Style = ProgressBarStyle.Marquee;
            bar.MarqueeAnimationSpeed = 30;
            bar.Value = 0;
        }

        private void StoptBar(ToolStripProgressBar bar)
        {
            bar.Style = ProgressBarStyle.Continuous;
            bar.MarqueeAnimationSpeed = 0;
            bar.Value = bar.Maximum;
        }

        private async void btnGameID_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }

            StartBar(pgb);
            //var game = await getGame(ID_value);
            var game = await Task.Run(() =>
            {
                return RA.GetGameInfoExtended(ID_value);
            });
            StoptBar(pgb);

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

            await Task.Run(() =>
            {
                return RA.DownloadBadges(ID_value);
            });
            txtOutput.Text = "Badges Downloaded!";
        }

        private void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            return;
            var URL = RA.CreateURL("API_GetConsoleIDs.php");
            txtURL.Text = URL;

            //save file
            byte[] jsonFile = Browser.DownloadData(URL);
            File.WriteAllBytes(RA.Local_JsonFolder + RA.ConsolesJson, jsonFile);

            //read file
            using (StreamReader r = new StreamReader(RA.Local_JsonFolder + RA.ConsolesJson))
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

                //var date = .ToString();
                List<FileUpdate> f = JsonConvert.DeserializeObject<List<FileUpdate>>(File.ReadAllText(RA.Local_JsonFolder + RA.Update_JsonFile));
                var fu = f.Find(o => o.Name == RA.ConsolesJson);
                fu.Update = DateTime.Now;

                //FileUpdate consoleFile = new FileUpdate { ID = 1, Name = RA.ConsolesJson, Update = DateTime.Now };

                string jsonData = JsonConvert.SerializeObject(f);
                if (!Directory.Exists(RA.Local_JsonFolder)) Directory.CreateDirectory(RA.Local_JsonFolder);
                File.WriteAllText(RA.Local_JsonFolder + RA.Update_JsonFile, jsonData);

                lblUpdateConsoles.Text = RA.ConsolesJson + ": " + fu.Update.ToString(); ;
            }
        }

        private void btnGameList_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }
            txtURL.Text = RA.CreateURL("API_GetConsoleIDs.php", "&i=", ID_value.ToString());

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

            string URL = RA.CreateURL("API_GetGame.php", "&i=", "");
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
    }
}
