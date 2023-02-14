using System.Windows.Forms;

namespace RADB
{
    public partial class GameMain
    {
        Main f;
        //Games
        public Panel pnlDownloadGameList { get { return f.pnlDownloadGameList; } }
        public FlatButtonA btnUpdateGameList { get { return f.btnUpdateGameList; } }
        public FlatLabelA lblUpdateGameList { get { return f.lblUpdateGameList; } }
        public FlatLabelA lblProgressGameList { get { return f.lblProgressGameList; } }
        public FlatProgressBarA pgbGameList { get { return f.pgbGameList; } }

        public FlatPanelA pnlGamesConsoleName { get { return f.pnlGamesConsoleName; } }
        public FlatLabelB lblConsoleName { get { return f.lblConsoleName; } }
        public FlatLabelB lblConsoleGamesTotal { get { return f.lblConsoleGamesTotal; } }

        public FlatTextBoxA txtSearchGames { get { return f.txtSearchGames; } }
        public FlatButtonA btnGameFilters { get { return f.btnGameFilters; } }
        public FlatPanelA pnlFilters { get { return f.pnlFilters; } }

        public FlatCheckBoxA chkWithoutAchievements { get { return f.chkWithoutAchievements; } }
        public FlatCheckBoxA chkOfficial { get { return f.chkOfficial; } }
        public FlatCheckBoxA chkPrototype { get { return f.chkPrototype; } }
        public FlatCheckBoxA chkUnlicensed { get { return f.chkUnlicensed; } }
        public FlatCheckBoxA chkDemo { get { return f.chkDemo; } }
        public FlatCheckBoxA chkHack { get { return f.chkHack; } }
        public FlatCheckBoxA chkHomebrew { get { return f.chkHomebrew; } }
        public FlatCheckBoxA chkSubset { get { return f.chkSubset; } }
        public FlatCheckBoxA chkTestKit { get { return f.chkTestKit; } }
        public FlatCheckBoxA chkDemoted { get { return f.chkDemoted; } }

        public FlatLabelA lblNotFoundGameList { get { return f.lblNotFoundGameList; } }
        public FlatPictureBoxA picLoaderGameList { get { return f.picLoaderGameList; } }

        public FlatDataGridA dgvGames { get { return f.dgvGames; } }
        public ContextMenuStrip mnuGames { get { return f.mnuGames; } }
        public ToolStripMenuItem mniPlayGame { get { return f.mniPlayGame; } }
        public ToolStripMenuItem mniHideGame { get { return f.mniHideGame; } }
        public ToolStripMenuItem mniMergeGameBadges { get { return f.mniMergeGameBadges; } }
    }
}