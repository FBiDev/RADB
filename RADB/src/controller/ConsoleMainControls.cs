using System.Windows.Forms;

namespace RADB
{
    public partial class ConsoleMain
    {
        Main f;
        //Consoles
        public Panel pnlDownloadConsoles { get { return f.pnlDownloadConsoles; } }
        public FlatButtonA btnUpdateConsoles { get { return f.btnUpdateConsoles; } }
        public FlatLabelA lblUpdateConsoles { get { return f.lblUpdateConsoles; } }
        public FlatLabelA lblProgressConsoles { get { return f.lblProgressConsoles; } }
        public FlatProgressBarA pgbConsoles { get { return f.pgbConsoles; } }

        public FlatDataGridA dgvConsoles { get { return f.dgvConsoles; } }
        public ContextMenuStrip mnuConsoles { get { return f.mnuConsoles; } }
        public ToolStripMenuItem mniMergeGamesIcon { get { return f.mniMergeGamesIcon; } }
        public ToolStripMenuItem mniMergeGamesIconBadSize { get { return f.mniMergeGamesIconBadSize; } }

        public FlatLabelA lblNotFoundConsoles { get { return f.lblNotFoundConsoles; } }
        public FlatPictureBoxA picLoaderConsole { get { return f.picLoaderConsole; } }
    }
}
