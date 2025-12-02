namespace RADB
{
    public partial class ImageViewerCommon
    {
        private static ImageViewer form;

        private static FlatPictureBoxA PicImage
        {
            get { return form.picImage; }
        }
    }
}