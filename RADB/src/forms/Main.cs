namespace RADB
{
    public partial class Main : BaseForm
    {
        public Main()
        {
            InitializeComponent();
            Init(this);

            MainCommon.Main_Init(this);
        }
    }
}