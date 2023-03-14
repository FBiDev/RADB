using System.Windows.Forms;

namespace RADB
{
    public partial class HashViewerCommon
    {
        static HashViewer form;

        static RichTextBox txtHashes { get { return form.txtHashes; } }
        static PictureBox picLoaderHash { get { return form.picLoaderHash; } }
    }
}