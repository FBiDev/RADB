using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using App.Core.Desktop;

namespace RADB
{
    public static partial class ImageViewerCommon
    {
        static Size FormInitialSize;
        static Size MinimumClientSize;
        static Size MaximumClientSize;
        static Size UnitImageSize;
        static Picture PictureInitial;
        static Picture PictureSmall;
        static double zoomFactor;
        static double zoomPercent;

        #region MAIN
        public static void ImageViewer_Init(ImageViewer formDesign)
        {
            form = formDesign;
            form.Init();

            form.FormClosing += frmImageViewer_FormClosing;
            form.KeyDown += frmImageViewer_KeyDown;
            form.MouseWheel += frmImageViewer_MouseWheel;

            form.VerticalScroll.SmallChange = 16;
            form.HorizontalScroll.SmallChange = 16;

            MinimumClientSize = new Size(192, 192);//96*2 x 96*2
            MaximumClientSize = new Size(1056, 576);//96*11 x 96*6

            zoomFactor = 0.25;
            zoomPercent = 1.0;
        }

        static void frmImageViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            PictureInitial.Bitmap.Dispose();
        }

        static void frmImageViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                form.Close();
            }
        }

        static void frmImageViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            //Up = 1 Down = -1
            var mousedelta = Math.Sign(e.Delta);
            var zoomTimes = (int)(((form.ClientSize.Height / UnitImageSize.Height) - 1) / zoomFactor);
            double maxZoom = (1.0 + (zoomTimes * zoomFactor));

            if (mousedelta == 1 && zoomPercent >= maxZoom || mousedelta == -1 && zoomPercent <= zoomFactor) { return; }

            zoomPercent += mousedelta * zoomFactor;

            var newSize = new Size((int)(PictureInitial.Width * zoomPercent), (int)(PictureInitial.Height * zoomPercent));

            if (mousedelta == -1 && zoomPercent <= zoomFactor)
            {
                //Decrease Size with other interpolation
                picImage.Image = PictureSmall.Bitmap;
            }
            else if (mousedelta == 1 && zoomPercent > zoomFactor)
            {
                //Increase Size
                picImage.Image = PictureInitial.Bitmap;
            }

            picImage.Size = newSize;

            SetScrollSize();
        }

        static void SetScrollSize()
        {
            //Add ScrollH space
            int ScrollH = form.HorizontalScroll.Visible && form.Height == FormInitialSize.Height ? SystemInformation.HorizontalScrollBarHeight :
                          !form.HorizontalScroll.Visible && form.Height > FormInitialSize.Height ? -SystemInformation.HorizontalScrollBarHeight : 0;

            form.Height += ScrollH;
        }

        public static void SetImage(Picture pic, Size perImageSize = default(Size))
        {
            var newForm = new ImageViewer();

            Session.MainFormRA.BeginInvoke((Action)(() =>
            {
                newForm.Hide();
                newForm.ShowDialog();
            }));

            PictureInitial = new Picture(pic.Path);

            //SmallPicture
            var sizeSmall = new Size((int)(pic.Width * zoomFactor), (int)(pic.Height * zoomFactor));
            PictureSmall = new Picture(new List<string> { pic.Path }, true, 1, sizeSmall, true);

            form.Text += " - " + PictureInitial.Name;

            picImage.Image = PictureInitial.Bitmap;
            picImage.Size = PictureInitial.Size;

            UnitImageSize = perImageSize == default(Size) ? new Size(64, 64) : perImageSize;

            int cliW = picImage.Width <= MinimumClientSize.Width ? MinimumClientSize.Width :
                                        (picImage.Width >= MaximumClientSize.Width) ? MaximumClientSize.Width : picImage.Width;

            int cliH = picImage.Height <= MinimumClientSize.Height ? MinimumClientSize.Height :
                                        (picImage.Height >= MaximumClientSize.Height) ? MaximumClientSize.Height : picImage.Height;

            form.ClientSize = new Size(cliW, cliH);

            //Add ScrollW space
            if (picImage.Height > MaximumClientSize.Height)
            {
                form.Width += SystemInformation.VerticalScrollBarWidth;
            }

            FormInitialSize = form.Size;

            SetScrollSize();
        }
        #endregion
    }
}