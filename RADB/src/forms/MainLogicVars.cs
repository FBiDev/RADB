using System.Windows.Forms;

namespace RADB
{
    public partial class MainLogic
    {
        //Main
        public FlatTabControlA tabMain { get { return f.tabMain; } set { f.tabMain = value; } }
        public TabPage tabConsoles { get { return f.tabConsoles; } set { f.tabConsoles = value; } }
        public TabPage tabGames { get { return f.tabGames; } set { f.tabGames = value; } }
        public TabPage tabGameInfo { get { return f.tabGameInfo; } set { f.tabGameInfo = value; } }
        public TabPage tabGamesToPlay { get { return f.tabGamesToPlay; } set { f.tabGamesToPlay = value; } }
        public TabPage tabGamesToHide { get { return f.tabGamesToHide; } set { f.tabGamesToHide = value; } }
        public TabPage tabUserInfo { get { return f.tabUserInfo; } set { f.tabUserInfo = value; } }

        public Label lblOutput { get { return f.lblOutput; } set { f.lblOutput = value; } }

        //Consoles
        public Panel pnlDownloadConsoles { get { return f.pnlDownloadConsoles; } set { f.pnlDownloadConsoles = value; } }
        public FlatButtonA btnUpdateConsoles { get { return f.btnUpdateConsoles; } set { f.btnUpdateConsoles = value; } }
        public FlatLabelA lblUpdateConsoles { get { return f.lblUpdateConsoles; } set { f.lblUpdateConsoles = value; } }
        public FlatLabelA lblProgressConsoles { get { return f.lblProgressConsoles; } set { f.lblProgressConsoles = value; } }
        public FlatProgressBarA pgbConsoles { get { return f.pgbConsoles; } set { f.pgbConsoles = value; } }

        public FlatDataGridA dgvConsoles { get { return f.dgvConsoles; } set { f.dgvConsoles = value; } }
        public ContextMenuStrip mnuConsoles { get { return f.mnuConsoles; } set { f.mnuConsoles = value; } }
        public ToolStripMenuItem mniMergeGamesIcon { get { return f.mniMergeGamesIcon; } set { f.mniMergeGamesIcon = value; } }
        public ToolStripMenuItem mniMergeGamesIconBadSize { get { return f.mniMergeGamesIconBadSize; } set { f.mniMergeGamesIconBadSize = value; } }

        public FlatLabelA lblNotFoundConsoles { get { return f.lblNotFoundConsoles; } set { f.lblNotFoundConsoles = value; } }
        public FlatPictureBoxA picLoaderConsole { get { return f.picLoaderConsole; } set { f.picLoaderConsole = value; } }

        //Games
        public Panel pnlDownloadGameList { get { return f.pnlDownloadGameList; } set { f.pnlDownloadGameList = value; } }
        public FlatButtonA btnUpdateGameList { get { return f.btnUpdateGameList; } set { f.btnUpdateGameList = value; } }
        public FlatLabelA lblUpdateGameList { get { return f.lblUpdateGameList; } set { f.lblUpdateGameList = value; } }
        public FlatLabelA lblProgressGameList { get { return f.lblProgressGameList; } set { f.lblProgressGameList = value; } }
        public FlatProgressBarA pgbGameList { get { return f.pgbGameList; } set { f.pgbGameList = value; } }

        public FlatPanelA pnlGamesConsoleName { get { return f.pnlGamesConsoleName; } set { f.pnlGamesConsoleName = value; } }
        public FlatLabelB lblConsoleName { get { return f.lblConsoleName; } set { f.lblConsoleName = value; } }
        public FlatLabelB lblConsoleGamesTotal { get { return f.lblConsoleGamesTotal; } set { f.lblConsoleGamesTotal = value; } }

        public FlatTextBoxA txtSearchGames { get { return f.txtSearchGames; } set { f.txtSearchGames = value; } }
        public FlatButtonA btnGameFilters { get { return f.btnGameFilters; } set { f.btnGameFilters = value; } }
        public FlatPanelA pnlFilters { get { return f.pnlFilters; } set { f.pnlFilters = value; } }

        public FlatCheckBoxA chkWithoutAchievements { get { return f.chkWithoutAchievements; } set { f.chkWithoutAchievements = value; } }
        public FlatCheckBoxA chkOfficial { get { return f.chkOfficial; } set { f.chkOfficial = value; } }
        public FlatCheckBoxA chkPrototype { get { return f.chkPrototype; } set { f.chkPrototype = value; } }
        public FlatCheckBoxA chkUnlicensed { get { return f.chkUnlicensed; } set { f.chkUnlicensed = value; } }
        public FlatCheckBoxA chkDemo { get { return f.chkDemo; } set { f.chkDemo = value; } }
        public FlatCheckBoxA chkHack { get { return f.chkHack; } set { f.chkHack = value; } }
        public FlatCheckBoxA chkHomebrew { get { return f.chkHomebrew; } set { f.chkHomebrew = value; } }
        public FlatCheckBoxA chkSubset { get { return f.chkSubset; } set { f.chkSubset = value; } }
        public FlatCheckBoxA chkTestKit { get { return f.chkTestKit; } set { f.chkTestKit = value; } }
        public FlatCheckBoxA chkDemoted { get { return f.chkDemoted; } set { f.chkDemoted = value; } }

        public FlatLabelA lblNotFoundGameList { get { return f.lblNotFoundGameList; } set { f.lblNotFoundGameList = value; } }
        public FlatPictureBoxA picLoaderGameList { get { return f.picLoaderGameList; } set { f.picLoaderGameList = value; } }

        public FlatDataGridA dgvGames { get { return f.dgvGames; } set { f.dgvGames = value; } }
        public ContextMenuStrip mnuGames { get { return f.mnuGames; } set { f.mnuGames = value; } }
        public ToolStripMenuItem mniPlayGame { get { return f.mniPlayGame; } set { f.mniPlayGame = value; } }
        public ToolStripMenuItem mniHideGame { get { return f.mniHideGame; } set { f.mniHideGame = value; } }
        public ToolStripMenuItem mniMergeGameBadges { get { return f.mniMergeGameBadges; } set { f.mniMergeGameBadges = value; } }

        //GameInfo
        public FlatLabelA lblProgressInfo { get { return f.lblProgressInfo; } set { f.lblProgressInfo = value; } }
        public FlatProgressBarA pgbInfo { get { return f.pgbInfo; } set { f.pgbInfo = value; } }
        public FlatLabelA lblUpdateInfo { get { return f.lblUpdateInfo; } set { f.lblUpdateInfo = value; } }
        public FlatPanelA pnlInfoScroll { get { return f.pnlInfoScroll; } set { f.pnlInfoScroll = value; } }
        public FlatButtonA btnGamePage { get { return f.btnGamePage; } set { f.btnGamePage = value; } }
        public FlatButtonA btnHashes { get { return f.btnHashes; } set { f.btnHashes = value; } }
        public FlatDataGridA dgvAchievements { get { return f.dgvAchievements; } set { f.dgvAchievements = value; } }

        //GameToPlay
        public FlatDataGridA dgvGamesToPlay { get { return f.dgvGamesToPlay; } set { f.dgvGamesToPlay = value; } }
        public ContextMenuStrip mnuGamesToPlay { get { return f.mnuGamesToPlay; } set { f.mnuGamesToPlay = value; } }
        public ToolStripMenuItem mniRemoveGameToPlay { get { return f.mniRemoveGameToPlay; } set { f.mniRemoveGameToPlay = value; } }

        public FlatLabelA lblNotFoundGamesToPlay { get { return f.lblNotFoundGamesToPlay; } set { f.lblNotFoundGamesToPlay = value; } }

        //GameToHide
        public FlatDataGridA dgvGamesToHide { get { return f.dgvGamesToHide; } set { f.dgvGamesToHide = value; } }
        public ContextMenuStrip mnuGamesToHide { get { return f.mnuGamesToHide; } set { f.mnuGamesToHide = value; } }
        public ToolStripMenuItem mniRemoveGameToHide { get { return f.mniRemoveGameToHide; } set { f.mniRemoveGameToHide = value; } }

        public FlatLabelA lblNotFoundGamesToHide { get { return f.lblNotFoundGamesToHide; } set { f.lblNotFoundGamesToHide = value; } }

        //UserInfo
        public FlatTextBoxA txtUsername { get { return f.txtUsername; } set { f.txtUsername = value; } }
        public FlatButtonA btnGetUserInfo { get { return f.btnGetUserInfo; } set { f.btnGetUserInfo = value; } }
        public FlatButtonA btnUserPage { get { return f.btnUserPage; } set { f.btnUserPage = value; } }
        public LinkLabel lnkUserRank { get { return f.lnkUserRank; } set { f.lnkUserRank = value; } }

        //About
        public FlatButtonA btnRALogin { get { return f.btnRALogin; } set { f.btnRALogin = value; } }
        public FlatLabelA lblRALogin { get { return f.lblSystemReLogin; } set { f.lblSystemReLogin = value; } }
        public FlatButtonA btnRAProfileAbout { get { return f.btnRAProfileAbout; } set { f.btnRAProfileAbout = value; } }

        public FlatPictureBoxA picUserCheevos { get { return f.picUserCheevos; } set { f.picUserCheevos = value; } }
        public FlatLabelA lblUserCheevos { get { return f.lblUserCheevos; } set { f.lblUserCheevos = value; } }
        public FlatButtonA btnUserCheevos { get { return f.btnUserCheevos; } set { f.btnUserCheevos = value; } }
        public Label lblCheevoLoopUpdate { get { return f.lblCheevoLoopUpdate; } set { f.lblCheevoLoopUpdate = value; } }
        public CheckBox chkUserCheevos { get { return f.chkUserCheevos; } set { f.chkUserCheevos = value; } }
    }
}
