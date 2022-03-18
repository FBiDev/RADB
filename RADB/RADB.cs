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

namespace RADB
{
    public partial class RADB : Form
    {
        private WebClient web = new WebClient() { Proxy = Config.Proxy, };
        private string URI_API = Config.URI_API;
        private string AuthQS = Config.AuthQS;
        private string URL = "";

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
            Config.WebStart();
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

        private bool ValidID()
        {
            return ID_value > 0;
        }

        private void btnGameID_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }

            Game game = RA.GetGameInfoExtended(ID_value);

            txtOutput.Text = string.IsNullOrWhiteSpace(game.Title) ? "NULL" : game.Title + Environment.NewLine;

            string badges = string.Empty;
            foreach (var achievement in game.AchievementsList)
            {
                badges += achievement.BadgeName + Environment.NewLine;
            }

            txtOutput.Text += badges.TrimEnd('\r', '\n');
        }

        private void btnDownloadBadges_Click(object sender, EventArgs e)
        {
            ParseValues();
            if (!ValidID()) { return; }

            RA.DownloadBadges(ID_value);
            txtOutput.Text = "Badges Downloaded!";
        }

        private void btnConsoleIDs_Click(object sender, EventArgs e)
        {
            var file = "API_GetConsoleIDs.php";
            URL = URI_API + file + AuthQS;
            txtURL.Text = URL;

            using (StreamReader r = new StreamReader("src/rsc/API_GetConsoleIDs.json"))
            {
                string json = r.ReadToEnd();
                List<Console> consoles = JsonConvert.DeserializeObject<List<Console>>(json);
                consoles = consoles.OrderBy(x => x.Name).ToList();

                txtOutput.Text = string.Empty;
                string textTotal = string.Empty;
                foreach (var console in consoles)
                {
                    textTotal += console.ID.ToString().PadLeft(2, ' ') + " : " + console.Name + Environment.NewLine;
                }
                textTotal = textTotal.ToString().TrimEnd('\r', '\n');
                txtOutput.Text = textTotal;
            }
        }

        private void btnGameList_Click(object sender, EventArgs e)
        {
            var file = "API_GetGameList.php";
            URL = URI_API + file + AuthQS + "&i=" + txtID.Text;
            txtURL.Text = URL;

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
            var file = "API_GetGame.php";
            txtURL.Text = file;

            string textTotal = string.Empty;

            foreach (var id in IDs)
            {
                URL = URI_API + file + AuthQS + "&i=" + id;

                var content = web.DownloadString(URL);

                Game game = JsonConvert.DeserializeObject<Game>(content);
                textTotal += game.ID.ToString().PadLeft(5, ' ') + " : " + game.Genre + Environment.NewLine;
            }
            txtOutput.Text = textTotal.TrimEnd('\r', '\n');
        }

        private void btnMergeImages_Click(object sender, EventArgs e)
        {
            int gameID;
            bool isNumber = int.TryParse(txtID.Text.Trim(), out gameID);
            string filePath = "src/rsc/game/" + txtID.Text + "/";
            string[] files = new string[] { "93518_lock.png", "93519_lock.png" };

            if (txtID.Text.Trim() == string.Empty || !isNumber || !Directory.Exists(filePath)) return;

            foreach (string file in files)
            {
                if (!File.Exists(filePath + file)) return;
            }

            //read all images into memory
            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;
                int index = 1;
                int imagesPerRow = 11;
                int maxWidth = 0;

                foreach (string file in files)
                {
                    //create a Bitmap from the file and add it to the list
                    Bitmap bitmap = new Bitmap(filePath + file);

                    //update the size of the final bitmap
                    if (index <= imagesPerRow && width <= maxWidth)
                    {
                        width += bitmap.Width;
                        height = bitmap.Height > height ? bitmap.Height : height;
                    }

                    if (width > maxWidth) { maxWidth = width; }

                    if (index > imagesPerRow)
                    {
                        height += bitmap.Height > height ? bitmap.Height : height;
                        width = 0;
                        index = 0;
                    }
                    index++;

                    images.Add(bitmap);
                }

                //create a bitmap to hold the combined image
                finalImage = new Bitmap(maxWidth, height);

                //get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(Color.Magenta);

                    //go through each image and draw it on the final image
                    int offsetW = 0;
                    int offsetH = 0;
                    index = 1;

                    foreach (Bitmap image in images)
                    {
                        if (index > imagesPerRow)
                        {
                            offsetH += image.Height;
                            offsetW = 0;
                            index = 0;
                        }
                        index++;

                        g.DrawImage(image,
                          new Rectangle(offsetW, offsetH, image.Width, image.Height));
                        offsetW += image.Width;
                    }
                }

                ImageCodecInfo encoder = GetEncoder(ImageFormat.Jpeg);
                EncoderParameters parameters = new EncoderParameters(1)
                {
                    Param = new EncoderParameter[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 91L) }
                };

                string fileName = "_" + gameID + ".jpg";

                if (File.Exists(filePath + fileName)) { File.Delete(filePath + fileName); }

                finalImage.Save(filePath + fileName, encoder, parameters);
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
                finalImage.Dispose();
            }
        }

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

        
    }
}
