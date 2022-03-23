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

        public RADB()
        {
            InitializeComponent();
            Browser.Load();

            sts.Visible = true;

            cboConsoles.DisplayMember = "Name";
            cboConsoles.ValueMember = "ID";

            RA.CheckLocalFiles();
            lblUpdateProgress.Text = string.Empty;

            List<FileUpdate> objList = RA.FileToList<FileUpdate>(RA.Local_JsonFolder + RA.Update_JsonFile);
            FileUpdate obj = RA.FindFileName(objList, RA.JSN_Consoles);

            if (obj is object) { lblUpdateConsoles.Text = obj.Update.ToString(); }
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

        private void StartBar(ProgressBar bar, ProgressBarStyle style = ProgressBarStyle.Continuous, int maximum = 100)
        {
            stsL1.Text = string.Empty;
            bar.Style = style;
            bar.MarqueeAnimationSpeed = 1;
            bar.Maximum = maximum;
            bar.Value = 0;
        }

        private void StepBar(ProgressBar bar)
        {
            bar.Value += bar.Step;
        }

        private void StoptBar(ProgressBar bar)
        {
            if (bar.Style == ProgressBarStyle.Marquee)
            {
                bar.MarqueeAnimationSpeed = 0;
                bar.Style = ProgressBarStyle.Continuous;
            }

            //Hack for Win7
            bar.Maximum++;
            bar.Value = bar.Maximum;
            bar.Value--;
            bar.Maximum--;
        }

        private async void btnGameID_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }

            StartBar(pgb.ProgressBar);
            //var game = await getGame(ID_value);
            string c = File.ReadAllText(RA.Local_JsonFolder + RA.Update_JsonFile);
            //List<FileUpdate> f = Browser.ToObject<List<FileUpdate>>(c);
            var game = await Task.Run(() =>
            {
                return RA.GetGameInfoExtended(ID_value);
            });
            StoptBar(pgb.ProgressBar);

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

        private async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            lblUpdateProgress.Text = "Atualizando arquivo...";
            //StartBar(pgbUpdates, ProgressBarStyle.Marquee);
            //DateTime date = await RA.UpdateConsolesFile();
            //StoptBar(pgbUpdates);
            //lblUpdateConsoles.Text = date.ToString();
            lblUpdateProgress.Text = "Arquivo atualizado!";
            lblUpdateProgress.Text = "Abrindo URL...";

            
            await startDownload(lblUpdateProgress, pgbUpdates);





            return;
            var URL = RA.GetURL("API_GetConsoleIDs.php");
            txtURL.Text = URL;


            //List<FileUpdate> f = JsonConvert.DeserializeObject<List<FileUpdate>>(File.ReadAllText(RA.Local_JsonFolder + RA.Update_JsonFile));

            //read file
            using (StreamReader r = new StreamReader(RA.Local_JsonFolder + RA.JSN_Consoles))
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
            txtURL.Text = RA.GetURL("API_GetConsoleIDs.php", "&i=", ID_value.ToString());

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

            string URL = RA.GetURL("API_GetGame.php", "&i=", "");
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

        private Task startDownload(Label lbl, ProgressBar bar)
        {
            if (!Browser.web.IsBusy)
            {
                StartBar(bar, ProgressBarStyle.Marquee);
            }
            return Task.Run(() =>
            {
                if (!Browser.web.IsBusy)
                {
                    

                    Browser.web.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    Browser.web.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    //Browser.web.DownloadFileAsync(new Uri(RA.GetURL("API_GetGameList.php", "&i=", "12")), RA.Local_JsonFolder + "GameList.json", new List<object> { lbl, bar });
                    //Browser.web.DownloadFileAsync(new Uri(@"file:///C:/Users/fbirnfeld/Downloads/DOCS/Projects/GitHub/RADB/RADB/bin/Debug/src/rsc/json/Files.json"), RA.Local_JsonFolder + "GameList.json");

                    Browser.web.DownloadFileAsync(new Uri("https://drive.google.com/u/0/uc?id=1_C8I5Vt62xbpcFF6otwRtHczXm-NY3Y8&export=download"), RA.Local_JsonFolder + "GameList.json", new List<object> { lbl, bar });

                    //Browser.web.OpenRead("http://www.cohabct.com.br/userfiles/file/Concovados/2020/classificacao_julho_2020.pdf");
                    //Int64 bytes_total = Convert.ToInt64(Browser.web.ResponseHeaders["Content-Length"]);
                }
            });
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;

                Label l = (Label)((List<object>)e.UserState)[0];
                l.Text = "Downloaded " + ToFileSizeString(bytesIn, totalBytes);

                ProgressBar bar = (ProgressBar)((List<object>)e.UserState)[1];
                if (totalBytes == -1) { bar.Style = ProgressBarStyle.Marquee; }
                else
                {
                    bar.Style = ProgressBarStyle.Blocks;
                    //bar.Maximum = (int)totalBytes;

                    double barValue = int.Parse(Math.Truncate(percentage).ToString());
                    if (barValue > 0 && barValue <= bar.Maximum)
                    {
                        pgbUpdates.Value = (int)barValue;
                    }
                }
            });
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                //lblUpdateProgress.Text = "Completed";
                ProgressBar bar = (ProgressBar)((List<object>)e.UserState)[1];
                StoptBar(bar);
            });
        }

        string ToFileSizeString(double bytesIn, double bytesTotal)
        {
            if (bytesTotal == -1) { return ToFileSize(bytesIn); }
            return ToFileSize(bytesIn) + " of " + ToFileSize(bytesTotal);
        }

        string ToFileSize(double bytesValue)
        {
            string size = bytesValue < 1024 ? "bytes" :
                bytesValue < 1048576 ? "KB" : "MB";

            double bytesSize = bytesValue < 1024 ? bytesValue :
                bytesValue < 1048576 ? bytesValue / 1024 : bytesValue / 1024 / 1024;

            return Math.Round(bytesSize) + " " + size;
        }
    }
}
