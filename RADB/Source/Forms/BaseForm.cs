using System.Drawing;
using System.Windows.Forms;
using RADB.Properties;
using App.Core.Desktop;

namespace RADB
{
    public partial class BaseForm : Form
    {
        public bool isDesignMode = true;

        public BaseForm()
        {
            InitializeComponent();
            Shown += (sender, e) =>
            {
                isDesignMode = DesignMode;
                if (isDesignMode) return;

                ThemeBase.CheckTheme(this);
            };
        }

        public void Init()
        {
            Icon = App.Core.Cast.ToIco(Resources.iconForm, new Size(250, 250));
        }

        public void CenterWindow()
        {
            CenterToScreen();
        }
    }
}