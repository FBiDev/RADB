using System.Windows.Forms;

namespace RADB
{
    public partial class MainContentController
    {
        private MainContentForm Page
        {
            get { return Session.MainContentForm; }
            set { Session.MainContentForm = value; }
        }

        private FlatPanelA ContentLPanel { get { return Page.ContentLPanel; } }

        private FlatPanelA ContentRPanel { get { return Page.ContentRPanel; } }

        private FlatPanelA ContentRInsidePanel { get { return Page.ContentRInsidePanel; } }

        private FlatButtonA ConfigTabButton { get { return Page.ConfigTabButton; } }

        private FlatButtonA SpeedRunTabButton { get { return Page.SpeedRunTabButton; } }
    }

    public partial class SpeedRunController
    {
        private SpeedRunForm Page
        {
            get { return Session.SpeedRunForm; }
            set { Session.SpeedRunForm = value; }
        }

        private FlatTextBoxA SearchGameTextBox { get { return Page.SearchGameTextBox; } }

        private FlatButtonA SearchGameButton { get { return Page.SearchGameButton; } }

        private FlatDataGridA SearchGameGrid { get { return Page.SearchGameGrid; } }
    }

    public partial class MainCommon
    {
        static Main form { get { return Session.MainFormRA; } }
        //Main
        static FlatTabControlA tabMain { get { return form.tabMain; } }
        //static TabPage tabConsoles { get { return form.tabConsoles; } }
        //static TabPage tabGames { get { return form.tabGames; } }
        //static TabPage tabGameInfo { get { return form.tabGameInfo; } }
        //static TabPage tabGamesToPlay { get { return form.tabGamesToPlay; } }
        //static TabPage tabGamesToHide { get { return form.tabGamesToHide; } }
        //static TabPage tabUserInfo { get { return form.tabUserInfo; } }
        //static Label lblOutput { get { return form.lblOutput; } }
    }

    public partial class MainConsole
    {
        static Main form { get { return Session.MainFormRA; } }
        //Consoles
        static Panel pnlDownloadConsoles { get { return form.pnlDownloadConsoles; } }
        static FlatButtonA btnUpdateConsoles { get { return form.btnUpdateConsoles; } }
        static FlatLabelA lblUpdateConsoles { get { return form.lblUpdateConsoles; } }
        static FlatLabelA lblProgressConsoles { get { return form.lblProgressConsoles; } }
        static FlatProgressBarA pgbConsoles { get { return form.pgbConsoles; } }

        static FlatDataGridA dgvConsoles { get { return form.dgvConsoles; } }
        static ContextMenuStrip mnuConsoles { get { return form.mnuConsoles; } }
        static ToolStripMenuItem mniMergeGamesIcon { get { return form.mniMergeGamesIcon; } }
        static ToolStripMenuItem mniMergeGamesIconBadSize { get { return form.mniMergeGamesIconBadSize; } }

        static FlatLabelA lblNotFoundConsoles { get { return form.lblNotFoundConsoles; } }
        static FlatPictureBoxA picLoaderConsole { get { return form.picLoaderConsole; } }
    }

    public partial class MainGame
    {
        static Main form { get { return Session.MainFormRA; } }
        //Games
        static Panel pnlDownloadGameList { get { return form.pnlDownloadGameList; } }
        static FlatButtonA btnUpdateGameList { get { return form.btnUpdateGameList; } }
        static FlatLabelA lblUpdateGameList { get { return form.lblUpdateGameList; } }
        static FlatLabelA lblProgressGameList { get { return form.lblProgressGameList; } }
        static FlatProgressBarA pgbGameList { get { return form.pgbGameList; } }

        static FlatPanelA pnlGamesConsoleName { get { return form.pnlGamesConsoleName; } }
        static FlatLabelB lblConsoleName { get { return form.lblConsoleName; } }
        static FlatLabelB lblConsoleGamesTotal { get { return form.lblConsoleGamesTotal; } }

        static FlatTextBoxA txtSearchGames { get { return form.txtSearchGames; } }
        static FlatButtonA btnGameFilters { get { return form.btnGameFilters; } }
        static FlatPanelA pnlFilters { get { return form.pnlFilters; } }

        static FlatCheckBoxA chkWithoutAchievements { get { return form.chkWithoutAchievements; } }
        static FlatCheckBoxA chkOfficial { get { return form.chkOfficial; } }
        static FlatCheckBoxA chkPrototype { get { return form.chkPrototype; } }
        static FlatCheckBoxA chkUnlicensed { get { return form.chkUnlicensed; } }
        static FlatCheckBoxA chkDemo { get { return form.chkDemo; } }
        static FlatCheckBoxA chkHack { get { return form.chkHack; } }
        static FlatCheckBoxA chkHomebrew { get { return form.chkHomebrew; } }
        static FlatCheckBoxA chkSubset { get { return form.chkSubset; } }
        static FlatCheckBoxA chkTestKit { get { return form.chkTestKit; } }
        static FlatCheckBoxA chkDemoted { get { return form.chkDemoted; } }

        static FlatLabelA lblNotFoundGameList { get { return form.lblNotFoundGameList; } }
        static FlatPictureBoxA picLoaderGameList { get { return form.picLoaderGameList; } }

        static FlatDataGridA dgvGames { get { return form.dgvGames; } }
        static ContextMenuStrip mnuGames { get { return form.mnuGames; } }
        static ToolStripMenuItem mniPlayGame { get { return form.mniPlayGame; } }
        static ToolStripMenuItem mniHideGame { get { return form.mniHideGame; } }
        static ToolStripMenuItem mniMergeGameBadges { get { return form.mniMergeGameBadges; } }
    }

    public partial class MainGameInfo
    {
        static Main form { get { return Session.MainFormRA; } }
        //GameInfo
        static FlatButtonA btnUpdateInfo { get { return form.btnUpdateInfo; } }
        static FlatLabelA lblProgressInfo { get { return form.lblProgressInfo; } }
        static FlatProgressBarA pgbInfo { get { return form.pgbInfo; } }
        static FlatLabelA lblUpdateInfo { get { return form.lblUpdateInfo; } }
        static FlatPanelA pnlInfoScroll { get { return form.pnlInfoScroll; } }

        static FlatButtonA btnGamePage { get { return form.btnGamePage; } }
        static FlatButtonA btnHashes { get { return form.btnHashes; } }
        static FlatDataGridA dgvAchievements { get { return form.dgvAchievements; } }

        static FlatLabelA lblInfoName { get { return form.lblInfoName; } }
        static FlatPictureBoxA picInfoIcon { get { return form.picInfoIcon; } }
        static FlatLabelA lblInfoAchievements { get { return form.lblInfoAchievements; } }

        static FlatLabelA lblInfoDeveloper1 { get { return form.lblInfoDeveloper1; } }
        static FlatLabelA lblInfoPublisher1 { get { return form.lblInfoPublisher1; } }
        static FlatLabelA lblInfoGenre1 { get { return form.lblInfoGenre1; } }
        static FlatLabelA lblInfoReleased1 { get { return form.lblInfoReleased1; } }

        static FlatPictureBoxA picInfoTitle { get { return form.picInfoTitle; } }
        static FlatPictureBoxA picInfoInGame { get { return form.picInfoInGame; } }
        static FlatPictureBoxA picInfoBoxArt { get { return form.picInfoBoxArt; } }

        static FlatPanelA pnlInfoImages { get { return form.pnlInfoImages; } }
        static FlatPanelA pnlInfoTitle { get { return form.pnlInfoTitle; } }
        static FlatPanelA pnlInfoInGame { get { return form.pnlInfoInGame; } }
        static FlatPanelA pnlInfoBoxArt { get { return form.pnlInfoBoxArt; } }
        static FlatGroupBoxA gpbInfo { get { return form.gpbInfo; } }
        static FlatGroupBoxA gpbInfoAchievements { get { return form.gpbInfoAchievements; } }
        static FlatTextBoxA txtSearchAchiev { get { return form.txtSearchAchiev; } }
    }

    public partial class MainGameToPlay
    {
        static Main form { get { return Session.MainFormRA; } }
        //GameToPlay
        static FlatDataGridA dgvGamesToPlay { get { return form.dgvGamesToPlay; } }
        static ContextMenuStrip mnuGamesToPlay { get { return form.mnuGamesToPlay; } }
        static ToolStripMenuItem mniRemoveGameToPlay { get { return form.mniRemoveGameToPlay; } }
        static FlatLabelA lblNotFoundGamesToPlay { get { return form.lblNotFoundGamesToPlay; } }
    }

    public partial class MainGameToHide
    {
        static Main form { get { return Session.MainFormRA; } }
        //GameToHide
        static FlatDataGridA dgvGamesToHide { get { return form.dgvGamesToHide; } }
        static ContextMenuStrip mnuGamesToHide { get { return form.mnuGamesToHide; } }
        static ToolStripMenuItem mniRemoveGameToHide { get { return form.mniRemoveGameToHide; } }
        static FlatLabelA lblNotFoundGamesToHide { get { return form.lblNotFoundGamesToHide; } }
    }

    public partial class MainUserInfo
    {
        static Main form { get { return Session.MainFormRA; } }
        //UserInfo
        static FlatTextBoxA txtUsername { get { return form.txtUsername; } }
        static FlatButtonA btnGetUserInfo { get { return form.btnGetUserInfo; } }
        static FlatButtonA btnUserPage { get { return form.btnUserPage; } }
        static LinkLabel lnkUserRank { get { return form.lnkUserRank; } }

        static FlatListViewA lsvGameAwards { get { return form.lsvGameAwards; } }
        static FlatLabelA lblUserCompletion { get { return form.lblUserCompletion; } }
        static FlatPictureBoxA picUserLastGame { get { return form.picUserLastGame; } }
        static FlatPictureBoxA picLoaderUserAwards { get { return form.picLoaderUserAwards; } }

        static FlatLabelA lblUserStatus { get { return form.lblUserStatus; } }
        static FlatLabelA lblUserName { get { return form.lblUserName; } }
        static FlatLabelA lblUserMotto { get { return form.lblUserMotto; } }

        static FlatLabelA lblUserMemberSince { get { return form.lblUserMemberSince; } }
        static FlatLabelA lblUserLastActivity { get { return form.lblUserLastActivity; } }
        static FlatLabelA lblUserAccountType { get { return form.lblUserAccountType; } }

        static FlatLabelA lblUserHCPoints { get { return form.lblUserHCPoints; } }
        static FlatLabelA lblUserRetroRatio { get { return form.lblUserRetroRatio; } }
        static FlatLabelA lblUserSoftPoints { get { return form.lblUserSoftPoints; } }
        static FlatLabelA lblUserSoftRank { get { return form.lblUserSoftRank; } }

        static FlatPictureBoxA picUserName { get { return form.picUserName; } }
        static FlatLabelA lblUserRichPresence { get { return form.lblUserRichPresence; } }
        static FlatLabelA lblUserLastConsole { get { return form.lblUserLastConsole; } }
        static FlatLabelA lblUserLastGame { get { return form.lblUserLastGame; } }

        static FlatPanelA pnlAwardFloating { get { return form.pnlAwardFloating; } }
        static FlatPictureBoxA picAwardFloating { get { return form.picAwardFloating; } }
        static FlatLabelA lblAwardFloatingTitle { get { return form.lblAwardFloatingTitle; } }
        static FlatLabelA lblAwardFloatingDesc { get { return form.lblAwardFloatingDesc; } }
    }

    public partial class MainAbout
    {
        static Main form { get { return Session.MainFormRA; } }
        //About
        static FlatButtonA btnRALogin { get { return form.btnRALogin; } }
        static FlatLabelA lblRALogin { get { return form.lblSystemReLogin; } }
        static FlatButtonA btnRAProfileAbout { get { return form.btnRAProfileAbout; } }

        static FlatPictureBoxA picUserCheevos { get { return form.picUserCheevos; } }
        static FlatLabelA lblUserCheevos { get { return form.lblUserCheevos; } }
        static FlatButtonA btnUserCheevos { get { return form.btnUserCheevos; } }
        static Label lblCheevoLoopUpdate { get { return form.lblCheevoLoopUpdate; } }
        static CheckBox chkUserCheevos { get { return form.chkUserCheevos; } }

        static FlatCheckBoxA chkDarkMode { get { return form.chkDarkMode; } }
        static FlatCheckBoxA chkDebugMode { get { return form.chkDebugMode; } }
    }
}