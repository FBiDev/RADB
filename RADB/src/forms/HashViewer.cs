using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Windows.Forms;
using System.Text.RegularExpressions;
using GNX;

namespace RADB
{
    public partial class HashViewer : Form
    {
        public HashViewer()
        {
            InitializeComponent();
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
            var ul = html.GetBetween("unique hashes registered for it:<br><br><ul>", "</ul>");

            string pattern = @"" + Regex.Escape("<li>") + "(.*?)" + Regex.Escape("</li>");
            Regex rgx = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection matchList = rgx.Matches(ul);

            var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
            var lastItem = list.LastOrDefault();

            foreach (string item in list)
            {
                var title = item.GetBetween("<b>", "</b>").Trim().HtmlDecode();
                var hash = item.GetBetween("<code>", "</code>").Trim();

                txtHashes.Text += title + Environment.NewLine + hash;
                if (item != lastItem)
                {
                    txtHashes.Text += Environment.NewLine + Environment.NewLine;
                }
            }

            picLoaderHash.Visible = false;
        }
    }
}
