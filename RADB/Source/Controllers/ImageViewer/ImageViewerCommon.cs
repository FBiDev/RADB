using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using App.Core.Desktop;

namespace RADB
{
    public static partial class ImageViewerCommon
    {
        private static Size formInitialSize;
        private static Size minimumClientSize;
        private static Size maximumClientSize;
        private static Size unitImageSize;
        private static Picture pictureInitial;
        private static Picture pictureSmall;
        private static double zoomFactor;
        private static double zoomPercent;

        #region MAIN
        public static void ImageViewer_Init(ImageViewer formDesign)
        {
            form = formDesign;
            form.Init();

            form.FormClosing += Form_Closing;
            form.KeyDown += Form_KeyDown;
            form.MouseWheel += Form_MouseWheel;

            form.VerticalScroll.SmallChange = 16;
            form.HorizontalScroll.SmallChange = 16;

            minimumClientSize = new Size(192, 192); // 96*2 x 96*2
            maximumClientSize = new Size(1056, 576); // 96*11 x 96*6

            zoomFactor = 0.25;
            zoomPercent = 1.0;
        }

        private static void Form_Closing(object sender, FormClosingEventArgs e)
        {
            pictureInitial.Bitmap.Dispose();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                form.Close();
            }
        }

        private static void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            // Up = 1 Down = -1
            var mousedelta = Math.Sign(e.Delta);
            var zoomTimes = (int)(((form.ClientSize.Height / unitImageSize.Height) - 1) / zoomFactor);
            double maxZoom = 1.0 + (zoomTimes * zoomFactor);

            if ((mousedelta == 1 && zoomPercent >= maxZoom) || (mousedelta == -1 && zoomPercent <= zoomFactor))
            {
                return;
            }

            zoomPercent += mousedelta * zoomFactor;

            var newSize = new Size((int)(pictureInitial.Width * zoomPercent), (int)(pictureInitial.Height * zoomPercent));

            if (mousedelta == -1 && zoomPercent <= zoomFactor)
            {
                // Decrease Size with other interpolation
                PicImage.Image = pictureSmall.Bitmap;
            }
            else if (mousedelta == 1 && zoomPercent > zoomFactor)
            {
                // Increase Size
                PicImage.Image = pictureInitial.Bitmap;
            }

            PicImage.Size = newSize;

            SetScrollSize();
        }

        private static void SetScrollSize()
        {
            // Add ScrollH space
            int scrollH = form.HorizontalScroll.Visible && form.Height == formInitialSize.Height ? SystemInformation.HorizontalScrollBarHeight :
                          !form.HorizontalScroll.Visible && form.Height > formInitialSize.Height ? -SystemInformation.HorizontalScrollBarHeight : 0;

            form.Height += scrollH;
        }

        public static void SetImage(Picture pic, Size perImageSize = default(Size))
        {
            var newForm = new ImageViewer();

            Session.MainFormRA.BeginInvoke((Action)(() =>
            {
                newForm.Hide();
                newForm.ShowDialog();
            }));

            pictureInitial = new Picture(pic.Path);

            // SmallPicture
            var sizeSmall = new Size((int)(pic.Width * zoomFactor), (int)(pic.Height * zoomFactor));
            pictureSmall = new Picture(new List<string> { pic.Path }, true, 1, sizeSmall, true);

            form.Text += " - " + pictureInitial.Name;

            PicImage.Image = pictureInitial.Bitmap;
            PicImage.Size = pictureInitial.Size;

            unitImageSize = perImageSize == default(Size) ? new Size(64, 64) : perImageSize;

            int cliW = PicImage.Width <= minimumClientSize.Width ? minimumClientSize.Width :
                                        (PicImage.Width >= maximumClientSize.Width) ? maximumClientSize.Width : PicImage.Width;

            int cliH = PicImage.Height <= minimumClientSize.Height ? minimumClientSize.Height :
                                        (PicImage.Height >= maximumClientSize.Height) ? maximumClientSize.Height : PicImage.Height;

            form.ClientSize = new Size(cliW, cliH);

            // Add ScrollW space
            if (PicImage.Height > maximumClientSize.Height)
            {
                form.Width += SystemInformation.VerticalScrollBarWidth;
            }

            formInitialSize = form.Size;

            SetScrollSize();
        }
        #endregion
    }
}