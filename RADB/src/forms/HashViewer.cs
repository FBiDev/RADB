using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public partial class HashViewer : Form
    {
        public HashViewer()
        {
            InitializeComponent();
        }

        public async Task GetHashCode(int GameID)
        {
            var html = await Browser.ClientLogin.DownloadString(RA.HOST + "linkedhashes.php?g=" + GameID);

            var ul = html.GetBetween("unique hashes registered for it:<br><br><ul>", "</ul>");

            string pattern = @"" + Regex.Escape("<li>") + "(.*?)" + Regex.Escape("</li>");
            Regex rgx = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            var ff = new List<Tuple<string, string>>();
            var entries = new List<Tuple<string, string>>().Select(t => new { Game = t.Item1, Hash = t.Item2 }).ToList();
            foreach (Match match in rgx.Matches(ul))
            {
                var game = match.Value.GetBetween("<p class='embedded'><b>", "</b>").Trim();
                var hash = match.Value.GetBetween("<code>", "</code>").Trim();
                ff.Add(new Tuple<string, string>(game, hash));

                entries.Add(new { Game = game, Hash = hash });

                txtHashes.Text += game + Environment.NewLine + hash + Environment.NewLine + Environment.NewLine;
            }

            //var a = entries[0].Game;
            //var b = ff[0].Item1;
        }
    }
}
