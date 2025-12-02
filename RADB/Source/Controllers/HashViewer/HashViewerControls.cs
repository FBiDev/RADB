using System.Windows.Forms;

namespace RADB
{
    public partial class HashViewerCommon
    {
        private static HashViewer form;

        private static RichTextBox TxtHashes
        {
            get { return form.txtHashes; }
        }

        private static PictureBox PicLoaderHash
        {
            get { return form.picLoaderHash; }
        }
    }
}