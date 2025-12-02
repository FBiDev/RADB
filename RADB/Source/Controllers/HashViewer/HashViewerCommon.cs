using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;
using App.Core.Web;

namespace RADB
{
    public static partial class HashViewerCommon
    {
        #region MAIN
        public static void HashViewer_Init(HashViewer formDesign)
        {
            form = formDesign;
            form.Init();

            TxtHashes.KeyDown += HashViewer_KeyDown;
        }

        private static void HashViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                form.Close();
            }
        }

        public static async Task Open(Game game)
        {
            if (Session.RALogged)
            {
                var newForm = new HashViewer();
                Session.MainFormRA.BeginInvoke((Action)(() =>
                {
                    newForm.Hide();
                    newForm.ShowDialog();
                }));

                await GetHashCode(game);
            }
            else
            {
                MessageBox.Show("You not logged in!");
            }
        }

        private static async Task GetHashCode(Game game)
        {
            form.Text = "RA HashViewer - " + game.Title + " (" + game.ConsoleName + ")";
            TxtHashes.Text = string.Empty;

            var html = await RASite.Client.DownloadString(RA.SiteURL + "linkedhashes.php?g=" + game.ID);
            var ul = html.GetBetween("registered for this game.</p></div><ul>", "</ul>").HtmlDecode();

            var listLi = ul.GetBetweenList("<li>", "</li>");

            var itemObj = new { Title = default(string), Warn = default(string), Hash = default(string), Labels = default(string), User = default(string) };
            var listItems = new List<object>().Select(t => itemObj).ToList();

            foreach (string item in listLi)
            {
                var title = item.GetBetween("<b>", "</b>").Trim();
                var hash = item.GetBetween("<code>", "</code>").Trim().ToUpper();
                var warn = item.GetBetween("> [", "]").Trim();
                if (warn != string.Empty)
                {
                    warn = " - [" + warn + "]";
                }

                var labels = string.Empty;
                var imgs = item.GetBetweenList("labels/", ".");
                imgs.ForEach(x => labels += "-(" + x + ")");

                if (labels == string.Empty)
                {
                    labels = "-( unknown )";
                }

                var userLabel = item.GetBetween("user/", "'");
                var user = userLabel != string.Empty ? " - linked by " + userLabel : string.Empty;

                listItems.Add(new { Title = title, Warn = warn, Hash = hash, Labels = labels, User = user });
            }

            listItems = listItems.OrderBy(x => x.Labels.Length).ThenBy(x => x.Title.Contains(".") ?
                           x.Title.Substring(0, x.Title.LastIndexOf(".", StringComparison.OrdinalIgnoreCase)) : x.Title).ToList();

            var mainItems = listItems.Where(x => x.Labels.Contains(")-") == false);

            // mainItems.Reverse();
            mainItems = mainItems.OrderByDescending(x => x.Title.Length + x.Warn.Length).ThenByDescending(x => x.Title);

            listItems.MoveToFirst(mainItems.Where(x => x.Title.Contains(" (Europe)")));
            listItems.MoveToFirst(mainItems.Where(x => x.Title.Contains(" (Japan)")));
            listItems.MoveToFirst(mainItems.Where(x => x.Title.Contains(" (Japan, USA)")));
            listItems.MoveToFirst(mainItems.Where(x => x.Title.Contains(" (USA, Europe)")));
            listItems.MoveToFirst(mainItems.Where(x => x.Title.Contains(" (USA)")));
            listItems.MoveToFirst(mainItems.Where(x => x.Title.Contains(" (World)")));

            listItems.MoveToLast(listItems.Where(x => x.Labels.Contains("msu1")));
            listItems.MoveToLast(listItems.Where(x => x.Title.Contains("Unlabeled")));

            var lastItem = listItems.LastOrDefault();
            foreach (var item in listItems)
            {
                TxtHashes.AppendText(item.Title, Theme.CheevoTitle);
                TxtHashes.AppendText(item.Warn + Environment.NewLine);
                TxtHashes.AppendText(item.Hash, Theme.CheevoDescription, new Font(new FontFamily("Courier New"), TxtHashes.Font.Size, TxtHashes.Font.Style));
                TxtHashes.AppendText(item.Labels, TxtHashes.ForeColor);
                TxtHashes.AppendText(item.User, TxtHashes.ForeColor);

                if (item != lastItem)
                {
                    TxtHashes.AppendText(Environment.NewLine + Environment.NewLine, TxtHashes.ForeColor);
                }
            }

            if (listItems.IsEmpty())
            {
                TxtHashes.Text = "No Hashes Available for this Game";
            }

            TxtHashes.SelectionStart = 0;
            PicLoaderHash.Visible = false;
        }
        #endregion
    }
}