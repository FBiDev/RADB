using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using GNX;
using GNX.Desktop;
using GNX.Web;

namespace RADB
{
    public static partial class HashViewerCommon
    {
        #region MAIN
        public static void HashViewer_Init(HashViewer formDesign)
        {
            form = formDesign;
            form.Init(form);

            txtHashes.KeyDown += HashViewer_KeyDown;
        }

        static void HashViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                form.Close();
            }
        }

        public static async Task Open(Game game)
        {
            if (BIND.RALogged)
            {
                var newForm = new HashViewer();
                Form.ActiveForm.BeginInvoke((Action)(() =>
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

        static async Task GetHashCode(Game game)
        {
            form.Text = "RA HashViewer - " + game.Title + " (" + game.ConsoleName + ")";
            txtHashes.Text = string.Empty;

            var html = await Browser.RALogin.DownloadString(RA.HOST_URL + "linkedhashes.php?g=" + game.ID);
            var ul = html.GetBetween("registered for this game.</p></div><ul>", "</ul>").HtmlDecode();

            var listLi = ul.GetBetweenList("<li>", "</li>");

            var itemObj = new { Title = default(string), Warn = default(string), Hash = default(string), Labels = default(string), User = default(string) };
            var listItems = new List<object>().Select(t => itemObj).ToList();

            foreach (string item in listLi)
            {
                var title = item.GetBetween("<b>", "</b>").Trim();
                var hash = item.GetBetween("<code>", "</code>").Trim().ToUpper();
                var warn = item.GetBetween("> [", "]").Trim();
                if (warn != string.Empty) { warn = " - [" + warn + "]"; }

                var labels = string.Empty;
                var imgs = item.GetBetweenList("labels/", ".");
                imgs.ForEach(x => labels += "-(" + x + ")");

                if (labels == string.Empty)
                    labels = "-( unknown )";

                var userLabel = item.GetBetween("user/", "'");
                var user = userLabel != "" ? " - linked by " + userLabel : "";

                listItems.Add(new { Title = title, Warn = warn, Hash = hash, Labels = labels, User = user });
            }

            listItems = listItems.OrderBy(x => x.Labels.Length).ThenBy(x => x.Title.Contains(".") ?
                           (x.Title.Substring(0, x.Title.LastIndexOf(".", StringComparison.OrdinalIgnoreCase))) : x.Title).ToList();

            var mainItems = listItems.Where(x => x.Labels.Contains(")-") == false);
            //mainItems.Reverse();
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
                txtHashes.AppendText(item.Title, Theme.CheevoTitle);
                txtHashes.AppendText(item.Warn + Environment.NewLine);
                txtHashes.AppendText(item.Hash, Theme.CheevoDescription, new Font(new FontFamily("Courier New"), txtHashes.Font.Size, txtHashes.Font.Style));
                txtHashes.AppendText(item.Labels, txtHashes.ForeColor);
                txtHashes.AppendText(item.User, txtHashes.ForeColor);

                if (item != lastItem)
                {
                    txtHashes.AppendText(Environment.NewLine + Environment.NewLine, txtHashes.ForeColor);
                }
            }

            if (listItems.IsEmpty())
            {
                txtHashes.Text = "No Hashes Available for this Game";
            }

            txtHashes.SelectionStart = 0;
            picLoaderHash.Visible = false;
        }
        #endregion
    }
}