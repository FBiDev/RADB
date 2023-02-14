using System.Windows.Forms;

namespace RADB
{
    public partial class MainLogic
    {
        //Main
        public FlatTabControlA tabMain { get { return f.tabMain; } }
        public TabPage tabConsoles { get { return f.tabConsoles; } }
        public TabPage tabGames { get { return f.tabGames; } }
        public TabPage tabGameInfo { get { return f.tabGameInfo; } }
        public TabPage tabGamesToPlay { get { return f.tabGamesToPlay; } }
        public TabPage tabGamesToHide { get { return f.tabGamesToHide; } }
        public TabPage tabUserInfo { get { return f.tabUserInfo; } }
        public Label lblOutput { get { return f.lblOutput; } }

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

        //GameInfo
        public FlatButtonA btnUpdateInfo { get { return f.btnUpdateInfo; } }
        public FlatLabelA lblProgressInfo { get { return f.lblProgressInfo; } }
        public FlatProgressBarA pgbInfo { get { return f.pgbInfo; } }
        public FlatLabelA lblUpdateInfo { get { return f.lblUpdateInfo; } }
        public FlatPanelA pnlInfoScroll { get { return f.pnlInfoScroll; } }

        public FlatButtonA btnGamePage { get { return f.btnGamePage; } }
        public FlatButtonA btnHashes { get { return f.btnHashes; } }
        public FlatDataGridA dgvAchievements { get { return f.dgvAchievements; } }

        public FlatLabelA lblInfoName { get { return f.lblInfoName; } }
        public FlatPictureBoxA picInfoIcon { get { return f.picInfoIcon; } }
        public FlatLabelA lblInfoAchievements { get { return f.lblInfoAchievements; } }

        public FlatLabelA lblInfoDeveloper1 { get { return f.lblInfoDeveloper1; } }
        public FlatLabelA lblInfoPublisher1 { get { return f.lblInfoPublisher1; } }
        public FlatLabelA lblInfoGenre1 { get { return f.lblInfoGenre1; } }
        public FlatLabelA lblInfoReleased1 { get { return f.lblInfoReleased1; } }

        public FlatPictureBoxA picInfoTitle { get { return f.picInfoTitle; } }
        public FlatPictureBoxA picInfoInGame { get { return f.picInfoInGame; } }
        public FlatPictureBoxA picInfoBoxArt { get { return f.picInfoBoxArt; } }

        public FlatPanelA pnlInfoImages { get { return f.pnlInfoImages; } }
        public FlatPanelA pnlInfoTitle { get { return f.pnlInfoTitle; } }
        public FlatPanelA pnlInfoInGame { get { return f.pnlInfoInGame; } }
        public FlatPanelA pnlInfoBoxArt { get { return f.pnlInfoBoxArt; } }
        public FlatGroupBoxA gpbInfo { get { return f.gpbInfo; } }
        public FlatGroupBoxA gpbInfoAchievements { get { return f.gpbInfoAchievements; } }
        public FlatTextBoxA txtSearchAchiev { get { return f.txtSearchAchiev; } }

        //GameToHide
        public FlatDataGridA dgvGamesToHide { get { return f.dgvGamesToHide; } }
        public ContextMenuStrip mnuGamesToHide { get { return f.mnuGamesToHide; } }
        public ToolStripMenuItem mniRemoveGameToHide { get { return f.mniRemoveGameToHide; } }
        public FlatLabelA lblNotFoundGamesToHide { get { return f.lblNotFoundGamesToHide; } }

        //UserInfo
        public FlatTextBoxA txtUsername { get { return f.txtUsername; } }
        public FlatButtonA btnGetUserInfo { get { return f.btnGetUserInfo; } }
        public FlatButtonA btnUserPage { get { return f.btnUserPage; } }
        public LinkLabel lnkUserRank { get { return f.lnkUserRank; } }

        public FlatListViewA lsvGameAwards { get { return f.lsvGameAwards; } }
        public FlatLabelA lblUserCompletion { get { return f.lblUserCompletion; } }
        public FlatPictureBoxA picUserLastGame { get { return f.picUserLastGame; } }
        public FlatPictureBoxA picLoaderUserAwards { get { return f.picLoaderUserAwards; } }

        public FlatLabelA lblUserStatus { get { return f.lblUserStatus; } }
        public FlatLabelA lblUserName { get { return f.lblUserName; } }
        public FlatLabelA lblUserMotto { get { return f.lblUserMotto; } }

        public FlatLabelA lblUserMemberSince { get { return f.lblUserMemberSince; } }
        public FlatLabelA lblUserLastActivity { get { return f.lblUserLastActivity; } }
        public FlatLabelA lblUserAccountType { get { return f.lblUserAccountType; } }

        public FlatLabelA lblUserHCPoints { get { return f.lblUserHCPoints; } }
        public FlatLabelA lblUserRetroRatio { get { return f.lblUserRetroRatio; } }
        public FlatLabelA lblUserSoftPoints { get { return f.lblUserSoftPoints; } }
        public FlatLabelA lblUserSoftRank { get { return f.lblUserSoftRank; } }

        public FlatPictureBoxA picUserName { get { return f.picUserName; } }
        public FlatLabelA lblUserRichPresence { get { return f.lblUserRichPresence; } }
        public FlatLabelA lblUserLastConsole { get { return f.lblUserLastConsole; } }
        public FlatLabelA lblUserLastGame { get { return f.lblUserLastGame; } }

        public FlatPanelA pnlAwardFloating { get { return f.pnlAwardFloating; } }
        public FlatPictureBoxA picAwardFloating { get { return f.picAwardFloating; } }
        public FlatLabelA lblAwardFloatingTitle { get { return f.lblAwardFloatingTitle; } }
        public FlatLabelA lblAwardFloatingDesc { get { return f.lblAwardFloatingDesc; } }
    }
}
