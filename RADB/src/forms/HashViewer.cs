using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
//
using RADB.Properties;
using GNX;

namespace RADB
{
    public partial class HashViewer : Form
    {
        public HashViewer()
        {
            InitializeComponent();
            Icon = GNX.cConvert.ToIco(Resources.iconForm, new Size(250, 250));

            txtHashes.KeyDown += HashViewer_KeyDown;

            Theme.CheckTheme(this);
        }

        void HashViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public static async void Open(Game game)
        {
            HashViewer f = new HashViewer();
            ActiveForm.BeginInvoke((Action)(() => { f.Hide(); f.ShowDialog(); }));
            await f.GetHashCode(game);
        }

        private async Task GetHashCode(Game game)
        {
            this.Text = "RA HashViewer - " + game.Title + " (" + game.ConsoleName + ")";
            txtHashes.Text = string.Empty;

            var html = await Browser.RALogin.DownloadString(RA.HOST + "linkedhashes.php?g=" + game.ID);
            var ul = html.GetBetween("unique hashes registered for it:<br><br><ul>", "</ul>").HtmlDecode();

            var list = ul.GetBetweenList("<li>", "</li>");
            list = list.OrderBy(x => x.GetBetween("<b>", "</code>", true).Length).ToList();
            var lastItem = list.LastOrDefault();

            foreach (string item in list)
            {
                var title = item.GetBetween("<b>", "</b>").Trim();
                var hash = item.GetBetween("<code>", "</code>").Trim().ToUpper();

                var extra = string.Empty;
                var imgs = item.GetBetweenList("labels/", ".");
                imgs.ForEach(x => extra += "-(" + x + ")");

                var user = item.GetBetween("user/", "'");
                extra += user != "" ? " - linked by " + user : "";

                txtHashes.AppendText(title + Environment.NewLine, Theme.CheevoTitle);
                txtHashes.AppendText(hash, Theme.CheevoDescription);
                txtHashes.AppendText(extra, txtHashes.ForeColor);

                if (item != lastItem)
                {
                    txtHashes.AppendText(Environment.NewLine + Environment.NewLine, txtHashes.ForeColor);
                }
            }

            txtHashes.SelectionStart = 0;
            picLoaderHash.Visible = false;
        }
    }
}
