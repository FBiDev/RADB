namespace RADB
{
    public partial class MainContentForm : App.Core.Desktop.ContentBaseForm
    {
        public MainContentForm()
        {
            InitializeComponent();
            var _ = new MainContentController(this);
        }
    }
}