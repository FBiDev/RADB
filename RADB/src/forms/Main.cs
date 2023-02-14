namespace RADB
{
    public partial class Main : BaseForm
    {
        public Main()
        {
            InitializeComponent();
            Init(this);

            var logic = new MainLogic(this);
        }
    }
}