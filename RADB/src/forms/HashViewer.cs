using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
//
using GNX;

namespace RADB
{
    public partial class HashViewer : BaseForm
    {
        public HashViewer()
        {
            InitializeComponent();
            base.Init(this);

            txtHashes.KeyDown += HashViewer_KeyDown;
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
            if (Browser.RALogged)
            {
                HashViewer f = new HashViewer();
                ActiveForm.BeginInvoke((Action)(() => { f.Hide(); f.ShowDialog(); }));
                await f.GetHashCode(game);
            }
            else
            {
                MessageBox.Show("You not logged in!");
            }
        }

        private async Task GetHashCode(Game game)
        {
            this.Text = "RA HashViewer - " + game.Title + " (" + game.ConsoleName + ")";
            txtHashes.Text = string.Empty;

            var html = await Browser.RALogin.DownloadString(RA.HOST + "linkedhashes.php?g=" + game.ID);
            var ul = html.GetBetween("unique hashes registered for it:<br><br><ul>", "</ul>").HtmlDecode();

            var listLi = ul.GetBetweenList("<li>", "</li>");

            var itemObj = new { Title = default(string), Hash = default(string), Labels = default(string), User = default(string) };
            var listItems = new List<object>().Select(t => itemObj).ToList();

            foreach (string item in listLi)
            {
                var title = item.GetBetween("<b>", "</b>").Trim();
                var hash = item.GetBetween("<code>", "</code>").Trim().ToUpper();

                var labels = string.Empty;
                var imgs = item.GetBetweenList("labels/", ".");
                imgs.ForEach(x => labels += "-(" + x + ")");

                if (labels == string.Empty)
                {
                    labels = item.GetBetween("</b>", "<br/>").Trim();
                }

                var userLabel = item.GetBetween("user/", "'");
                var user = userLabel != "" ? " - linked by " + userLabel : "";

                listItems.Add(new { Title = title, Hash = hash, Labels = labels, User = user });
            }

            listItems = listItems.OrderBy(x => (x.Title.Length + x.Labels.Length)).ToList();

            var listItemsMove = listItems.Where(x => (x.Title == "Unlabeled")).ToList();
            listItemsMove.ForEach(x => listItems.MoveToLast(x));

            var lastItem = listItems.LastOrDefault();
            foreach (var item in listItems)
            {
                txtHashes.AppendText(item.Title + Environment.NewLine, Theme.CheevoTitle);
                txtHashes.AppendText(item.Hash, Theme.CheevoDescription, new Font(new FontFamily("Courier New"), txtHashes.Font.Size, txtHashes.Font.Style));
                txtHashes.AppendText(item.Labels, txtHashes.ForeColor);
                txtHashes.AppendText(item.User, txtHashes.ForeColor);

                if (item != lastItem)
                {
                    txtHashes.AppendText(Environment.NewLine + Environment.NewLine, txtHashes.ForeColor);
                }
            }

            if (listItems.Empty()) 
            {
                txtHashes.Text = "No Hashes Available for this Game";
            }

            txtHashes.SelectionStart = 0;
            picLoaderHash.Visible = false;
        }
    }
}
