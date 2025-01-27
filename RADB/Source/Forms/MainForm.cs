namespace RADB
{
    public partial class MainForm : App.Core.Desktop.MainBaseForm
    {
        public MainForm()
        {
            InitializeComponent();
            var _ = new MainController(this);
        }
    }
}