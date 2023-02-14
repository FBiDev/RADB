using System.Windows.Forms;

namespace RADB
{
    public partial class AboutMain
    {
        Main f;
        //About
        public FlatButtonA btnRALogin { get { return f.btnRALogin; } }
        public FlatLabelA lblRALogin { get { return f.lblSystemReLogin; } }
        public FlatButtonA btnRAProfileAbout { get { return f.btnRAProfileAbout; } }

        public FlatPictureBoxA picUserCheevos { get { return f.picUserCheevos; } }
        public FlatLabelA lblUserCheevos { get { return f.lblUserCheevos; } }
        public FlatButtonA btnUserCheevos { get { return f.btnUserCheevos; } }
        public Label lblCheevoLoopUpdate { get { return f.lblCheevoLoopUpdate; } }
        public CheckBox chkUserCheevos { get { return f.chkUserCheevos; } }
    }
}
