using System.Windows.Forms;

namespace RADB
{
    public partial class GameToPlayMain
    {
        Main f;
        //GameToPlay
        public FlatDataGridA dgvGamesToPlay { get { return f.dgvGamesToPlay; } }
        public ContextMenuStrip mnuGamesToPlay { get { return f.mnuGamesToPlay; } }
        public ToolStripMenuItem mniRemoveGameToPlay { get { return f.mniRemoveGameToPlay; } }
        public FlatLabelA lblNotFoundGamesToPlay { get { return f.lblNotFoundGamesToPlay; } }
    }
}
