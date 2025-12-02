using System.Drawing;
using System.Windows.Forms;
using App.Core.Desktop;
using RADB.Properties;

namespace RADB
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            IsDesignMode = true;

            Shown += (sender, e) =>
            {
                IsDesignMode = DesignMode;
                if (IsDesignMode)
                {
                    return;
                }

                ThemeBase.CheckTheme(this);
            };
        }

        public bool IsDesignMode { get; set; }

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