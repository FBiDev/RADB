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

        public async Task GetHashCode(Game game)
        {
            this.Text = "RA HashViewer - " + game.Title + " (" + game.ConsoleName + ")";
            txtHashes.Text = string.Empty;

            var html = await Browser.ClientLogin.DownloadString(RA.HOST + "linkedhashes.php?g=" + game.ID);
            var ul = html.GetBetween("unique hashes registered for it:<br><br><ul>", "</ul>");

            string pattern = @"" + Regex.Escape("<li>") + "(.*?)" + Regex.Escape("</li>");
            Regex rgx = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //var entries = new List<Tuple<string, string>>().Select(t => new { Title = t.Item1, Hash = t.Item2 }).ToList();
            foreach (Match match in rgx.Matches(ul))
            {
                var title = match.Value.GetBetween("<b>", "</b>").Trim();
                title = title.HtmlDecode();

                var hash = match.Value.GetBetween("<code>", "</code>").Trim();

                //entries.Add(new { Title = title, Hash = hash });

                txtHashes.Text += title + Environment.NewLine + hash + Environment.NewLine + Environment.NewLine;
            }
        }
    }
}
