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
        private string User_value;
        private int Count_value;
        private int Offset_value;
        private DateTime Date1_value;
        private DateTime Date2_value;

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public RADB()
        {
            InitializeComponent();
            Browser.Load();
            Folder.CreateFolders();

            sts.Visible = true;

            cboConsoles.DisplayMember = "Name";
            cboConsoles.ValueMember = "ID";

            RA.CheckLocalFiles();
            lblUpdateProgress.Text = string.Empty;

            //List<FileUpdate> objList = RA.FileToList<FileUpdate>(RA.Local_JsonFolder + RA.JSN_Consoles);
            //FileUpdate obj = RA.FindFileName(objList, RA.JSN_Consoles);

            //if (obj is object) { lblUpdateConsoles.Text = obj.Update.ToString(); }
            lblUpdateConsoles.Text = Archive.LastUpdate(RA.JSN_Consoles).ToString();
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

        private void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            Download download = new Download
            {
                LabelTime = lblUpdateConsoles,
                LabelBytes = lblUpdateProgress,
                ProgressBar = pgbUpdates,
            };
            RA.UpdateConsolesFile(download);


            return;
            var URL = RA.API_URL("API_GetConsoleIDs.php");
            txtURL.Text = URL;


            //List<FileUpdate> f = JsonConvert.DeserializeObject<List<FileUpdate>>(File.ReadAllText(RA.Local_JsonFolder + RA.Update_JsonFile));

            //read file
            using (StreamReader r = new StreamReader(RA.FolderJson + RA.JSN_Consoles))
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
            txtURL.Text = RA.API_URL("API_GetConsoleIDs.php", "&i=", ID_value.ToString());

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

            string URL = RA.API_URL("API_GetGame.php", "&i=", "");
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
