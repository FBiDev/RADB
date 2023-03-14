namespace RADB
{
    public partial class ImageViewer : BaseForm
    {
        public ImageViewer()
        {
            InitializeComponent();
            ImageViewerCommon.ImageViewer_Init(this);
        }
    }
}