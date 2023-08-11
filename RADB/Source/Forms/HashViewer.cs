namespace RADB
{
    public partial class HashViewer : BaseForm
    {
        public HashViewer()
        {
            InitializeComponent();
            HashViewerCommon.HashViewer_Init(this);
        }
    }
}