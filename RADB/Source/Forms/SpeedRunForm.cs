namespace RADB
{
    public partial class SpeedRunForm : App.Core.Desktop.ContentBaseForm
    {
        public SpeedRunForm()
        {
            InitializeComponent();
            var _ = new SpeedRunController(this);
        }
    }
}