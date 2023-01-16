using System.Drawing;
namespace RADB
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Ttip = new System.Windows.Forms.ToolTip(this.components);
            this.mnuGames = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniPlayGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHideGame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniMergeGameBadges = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGamesToHide = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniRemoveGameToHide = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConsoles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniMergeGamesIcon = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMergeGamesIconBadSize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGamesToPlay = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniRemoveGameToPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlBottomOutput = new RADB.FlatPanelA();
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.lblOutput = new System.Windows.Forms.Label();
            this.tabMain = new RADB.FlatTabControlA();
            this.tabConsoles = new System.Windows.Forms.TabPage();
            this.lblNotFoundConsoles = new RADB.FlatLabelA();
            this.picLoaderConsole = new RADB.FlatPictureBoxA();
            this.pnlDownloadConsoles = new System.Windows.Forms.Panel();
            this.btnUpdateConsoles = new RADB.FlatButtonA();
            this.lblUpdateConsoles = new RADB.FlatLabelA();
            this.pgbConsoles = new RADB.FlatProgressBarA();
            this.lblProgressConsoles = new RADB.FlatLabelA();
            this.dgvConsoles = new RADB.FlatDataGridA();
            this.cID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNumGames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTotalGames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabGames = new System.Windows.Forms.TabPage();
            this.pnlFilters = new RADB.FlatPanelA();
            this.chkOfficial = new RADB.FlatCheckBoxA();
            this.chkPrototype = new RADB.FlatCheckBoxA();
            this.chkUnlicensed = new RADB.FlatCheckBoxA();
            this.chkDemo = new RADB.FlatCheckBoxA();
            this.chkWithoutAchievements = new RADB.FlatCheckBoxA();
            this.chkHack = new RADB.FlatCheckBoxA();
            this.chkHomebrew = new RADB.FlatCheckBoxA();
            this.chkSubset = new RADB.FlatCheckBoxA();
            this.chkTestKit = new RADB.FlatCheckBoxA();
            this.chkDemoted = new RADB.FlatCheckBoxA();
            this.lblNotFoundGameList = new RADB.FlatLabelA();
            this.picLoaderGameList = new RADB.FlatPictureBoxA();
            this.pnlGamesConsoleName = new RADB.FlatPanelA();
            this.lblConsoleGamesTotal = new RADB.FlatLabelB();
            this.lblConsoleName = new RADB.FlatLabelB();
            this.txtSearchGames = new RADB.FlatTextBoxA();
            this.btnGameFilters = new RADB.FlatButtonA();
            this.pnlDownloadGameList = new System.Windows.Forms.Panel();
            this.btnUpdateGameList = new RADB.FlatButtonA();
            this.lblUpdateGameList = new RADB.FlatLabelA();
            this.pgbGameList = new RADB.FlatProgressBarA();
            this.lblProgressGameList = new RADB.FlatLabelA();
            this.dgvGames = new RADB.FlatDataGridA();
            this.gID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gIconBitmap = new System.Windows.Forms.DataGridViewImageColumn();
            this.gTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gConsole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gNumAchievements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNumLeaderboards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gLastUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabGameInfo = new System.Windows.Forms.TabPage();
            this.pnlInfoScroll = new RADB.FlatPanelA();
            this.gpbInfo = new RADB.FlatGroupBoxA();
            this.pnlInfoBoxArt = new RADB.FlatPanelA();
            this.picInfoBoxArt = new RADB.FlatPictureBoxA();
            this.pnlInfoImages = new RADB.FlatPanelA();
            this.pnlInfoInGame = new RADB.FlatPanelA();
            this.picInfoInGame = new RADB.FlatPictureBoxA();
            this.pnlInfoTitle = new RADB.FlatPanelA();
            this.picInfoTitle = new RADB.FlatPictureBoxA();
            this.pnlInfoTop = new System.Windows.Forms.Panel();
            this.lblInfoReleased1 = new RADB.FlatLabelA();
            this.lblInfoDeveloper1 = new RADB.FlatLabelA();
            this.lblInfoGenre1 = new RADB.FlatLabelA();
            this.lblInfoPublisher1 = new RADB.FlatLabelA();
            this.picInfoIcon = new RADB.FlatPictureBoxA();
            this.lblInfoReleased0 = new RADB.FlatLabelA();
            this.lblInfoDeveloper0 = new RADB.FlatLabelA();
            this.lblInfoGenre0 = new RADB.FlatLabelA();
            this.lblInfoPublisher0 = new RADB.FlatLabelA();
            this.gpbInfoAchievements = new RADB.FlatGroupBoxA();
            this.txtSearchAchiev = new RADB.FlatTextBoxA();
            this.btnHashes = new RADB.FlatButtonA();
            this.dgvAchievements = new RADB.FlatDataGridA();
            this.aOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aIconBitmap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGamePage = new RADB.FlatButtonA();
            this.lblInfoAchievements = new RADB.FlatLabelA();
            this.lblInfoName = new RADB.FlatLabelA();
            this.pnlDownloadInfo = new System.Windows.Forms.Panel();
            this.btnUpdateInfo = new RADB.FlatButtonA();
            this.lblUpdateInfo = new RADB.FlatLabelA();
            this.pgbInfo = new RADB.FlatProgressBarA();
            this.lblProgressInfo = new RADB.FlatLabelA();
            this.tabGamesToPlay = new System.Windows.Forms.TabPage();
            this.lblNotFoundGamesToPlay = new RADB.FlatLabelA();
            this.dgvGamesToPlay = new RADB.FlatDataGridA();
            this.gpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpIconBitmap = new System.Windows.Forms.DataGridViewImageColumn();
            this.gpTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpConsole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpNumAchievements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpNumLeaderboards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpLastUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabGamesToHide = new System.Windows.Forms.TabPage();
            this.lblNotFoundGamesToHide = new RADB.FlatLabelA();
            this.dgvGamesToHide = new RADB.FlatDataGridA();
            this.ghID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghIconBitmap = new System.Windows.Forms.DataGridViewImageColumn();
            this.ghTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghConsole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghNumAchievements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghNumLeaderboards = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghLastUpdated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabUserInfo = new System.Windows.Forms.TabPage();
            this.lblUserStatus = new RADB.FlatLabelA();
            this.lblUserMotto = new RADB.FlatLabelA();
            this.lblUserName = new RADB.FlatLabelA();
            this.btnGetUserInfo = new RADB.FlatButtonA();
            this.picUserName = new RADB.FlatPictureBoxA();
            this.gpbUserInfo = new RADB.FlatGroupBoxA();
            this.lblUserCompletion = new RADB.FlatLabelA();
            this.lblUserSoftRank = new RADB.FlatLabelA();
            this.lblUserSoftPoints = new RADB.FlatLabelA();
            this.lblUserRetroRatio = new RADB.FlatLabelA();
            this.lblUserRank = new RADB.FlatLabelA();
            this.lblUserHCPoints = new RADB.FlatLabelA();
            this.lblUserAccountType = new RADB.FlatLabelA();
            this.lblUserLastActivity = new RADB.FlatLabelA();
            this.flatLabelA10 = new RADB.FlatLabelA();
            this.flatLabelA9 = new RADB.FlatLabelA();
            this.flatLabelA8 = new RADB.FlatLabelA();
            this.flatLabelA7 = new RADB.FlatLabelA();
            this.flatLabelA6 = new RADB.FlatLabelA();
            this.flatLabelA5 = new RADB.FlatLabelA();
            this.flatLabelA4 = new RADB.FlatLabelA();
            this.flatLabelA3 = new RADB.FlatLabelA();
            this.lblUserMemberSince = new RADB.FlatLabelA();
            this.flatLabelA1 = new RADB.FlatLabelA();
            this.gpbOverlay = new RADB.FlatGroupBoxA();
            this.pnlUserCheevos = new System.Windows.Forms.Panel();
            this.lblCheevos = new RADB.FlatLabelA();
            this.lblUserCheevos = new RADB.FlatLabelA();
            this.picUserCheevos = new RADB.FlatPictureBoxA();
            this.lblCheevoLoopUpdate = new System.Windows.Forms.Label();
            this.chkUserCheevos = new System.Windows.Forms.CheckBox();
            this.btnUserCheevos = new RADB.FlatButtonA();
            this.txtUsername = new RADB.FlatTextBoxA();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.lblSystemReLogin = new RADB.FlatLabelA();
            this.btnSystemReLogin = new RADB.FlatButtonA();
            this.btnRaProfile = new RADB.FlatButtonA();
            this.picFBiDevIcon = new RADB.FlatPictureBoxA();
            this.lblAbTitle = new RADB.FlatLabelB();
            this.lblAbYear = new RADB.FlatLabelB();
            this.mnuGames.SuspendLayout();
            this.mnuGamesToHide.SuspendLayout();
            this.mnuConsoles.SuspendLayout();
            this.mnuGamesToPlay.SuspendLayout();
            this.pnlBottomOutput.SuspendLayout();
            this.pnlOutput.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabConsoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderConsole)).BeginInit();
            this.pnlDownloadConsoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsoles)).BeginInit();
            this.tabGames.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderGameList)).BeginInit();
            this.pnlGamesConsoleName.SuspendLayout();
            this.pnlDownloadGameList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGames)).BeginInit();
            this.tabGameInfo.SuspendLayout();
            this.pnlInfoScroll.SuspendLayout();
            this.gpbInfo.SuspendLayout();
            this.pnlInfoBoxArt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoBoxArt)).BeginInit();
            this.pnlInfoImages.SuspendLayout();
            this.pnlInfoInGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoInGame)).BeginInit();
            this.pnlInfoTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoTitle)).BeginInit();
            this.pnlInfoTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoIcon)).BeginInit();
            this.gpbInfoAchievements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAchievements)).BeginInit();
            this.pnlDownloadInfo.SuspendLayout();
            this.tabGamesToPlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGamesToPlay)).BeginInit();
            this.tabGamesToHide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGamesToHide)).BeginInit();
            this.tabUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserName)).BeginInit();
            this.gpbUserInfo.SuspendLayout();
            this.gpbOverlay.SuspendLayout();
            this.pnlUserCheevos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserCheevos)).BeginInit();
            this.tabAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFBiDevIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuGames
            // 
            this.mnuGames.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniPlayGame,
            this.mniHideGame,
            this.toolStripSeparator1,
            this.mniMergeGameBadges});
            this.mnuGames.Name = "ctmGames";
            this.mnuGames.ShowImageMargin = false;
            this.mnuGames.Size = new System.Drawing.Size(159, 76);
            // 
            // mniPlayGame
            // 
            this.mniPlayGame.Name = "mniPlayGame";
            this.mniPlayGame.Size = new System.Drawing.Size(158, 22);
            this.mniPlayGame.Text = "Move To Play";
            this.mniPlayGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mniPlayGame_MouseDown);
            // 
            // mniHideGame
            // 
            this.mniHideGame.Name = "mniHideGame";
            this.mniHideGame.Size = new System.Drawing.Size(158, 22);
            this.mniHideGame.Text = "Move To Hide";
            this.mniHideGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mniHideGame_MouseDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // mniMergeGameBadges
            // 
            this.mniMergeGameBadges.Name = "mniMergeGameBadges";
            this.mniMergeGameBadges.Size = new System.Drawing.Size(158, 22);
            this.mniMergeGameBadges.Text = "Merge Game Badges";
            this.mniMergeGameBadges.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mniMergeGameBadges_MouseDown);
            // 
            // mnuGamesToHide
            // 
            this.mnuGamesToHide.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniRemoveGameToHide});
            this.mnuGamesToHide.Name = "ctmHiddenGames";
            this.mnuGamesToHide.Size = new System.Drawing.Size(197, 26);
            // 
            // mniRemoveGameToHide
            // 
            this.mniRemoveGameToHide.Name = "mniRemoveGameToHide";
            this.mniRemoveGameToHide.Size = new System.Drawing.Size(196, 22);
            this.mniRemoveGameToHide.Text = "Remove Game To Hide";
            this.mniRemoveGameToHide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mniRemoveGameToHide_MouseDown);
            // 
            // mnuConsoles
            // 
            this.mnuConsoles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniMergeGamesIcon,
            this.mniMergeGamesIconBadSize});
            this.mnuConsoles.Name = "mnuConsoles";
            this.mnuConsoles.Size = new System.Drawing.Size(228, 48);
            // 
            // mniMergeGamesIcon
            // 
            this.mniMergeGamesIcon.Name = "mniMergeGamesIcon";
            this.mniMergeGamesIcon.Size = new System.Drawing.Size(227, 22);
            this.mniMergeGamesIcon.Text = "Merge Games Icon";
            this.mniMergeGamesIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mniMergeGamesIcon_MouseDown);
            // 
            // mniMergeGamesIconBadSize
            // 
            this.mniMergeGamesIconBadSize.Name = "mniMergeGamesIconBadSize";
            this.mniMergeGamesIconBadSize.Size = new System.Drawing.Size(227, 22);
            this.mniMergeGamesIconBadSize.Text = "Merge Games Icon (Bad Size)";
            this.mniMergeGamesIconBadSize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mniMergeGamesIconBadSize_MouseDown);
            // 
            // mnuGamesToPlay
            // 
            this.mnuGamesToPlay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniRemoveGameToPlay});
            this.mnuGamesToPlay.Name = "mnuGamesToPlay";
            this.mnuGamesToPlay.Size = new System.Drawing.Size(194, 26);
            // 
            // mniRemoveGameToPlay
            // 
            this.mniRemoveGameToPlay.Name = "mniRemoveGameToPlay";
            this.mniRemoveGameToPlay.Size = new System.Drawing.Size(193, 22);
            this.mniRemoveGameToPlay.Text = "Remove Game To Play";
            this.mniRemoveGameToPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mniRemoveGameToPlay_MouseDown);
            // 
            // pnlBottomOutput
            // 
            this.pnlBottomOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomOutput.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottomOutput.Controls.Add(this.pnlOutput);
            this.pnlBottomOutput.Location = new System.Drawing.Point(12, 592);
            this.pnlBottomOutput.Name = "pnlBottomOutput";
            this.pnlBottomOutput.Size = new System.Drawing.Size(913, 66);
            this.pnlBottomOutput.TabIndex = 31;
            // 
            // pnlOutput
            // 
            this.pnlOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOutput.AutoScroll = true;
            this.pnlOutput.Controls.Add(this.lblOutput);
            this.pnlOutput.Location = new System.Drawing.Point(2, 2);
            this.pnlOutput.Name = "pnlOutput";
            this.pnlOutput.Size = new System.Drawing.Size(902, 62);
            this.pnlOutput.TabIndex = 30;
            // 
            // lblOutput
            // 
            this.lblOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutput.AutoSize = true;
            this.lblOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.Location = new System.Drawing.Point(0, 0);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(16, 15);
            this.lblOutput.TabIndex = 29;
            this.lblOutput.Text = "   ";
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabConsoles);
            this.tabMain.Controls.Add(this.tabGames);
            this.tabMain.Controls.Add(this.tabGameInfo);
            this.tabMain.Controls.Add(this.tabGamesToPlay);
            this.tabMain.Controls.Add(this.tabGamesToHide);
            this.tabMain.Controls.Add(this.tabUserInfo);
            this.tabMain.Controls.Add(this.tabAbout);
            this.tabMain.Location = new System.Drawing.Point(10, 12);
            this.tabMain.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.tabMain.myBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.tabMain.myBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(916, 574);
            this.tabMain.TabIndex = 28;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabConsoles
            // 
            this.tabConsoles.BackColor = System.Drawing.Color.Transparent;
            this.tabConsoles.Controls.Add(this.lblNotFoundConsoles);
            this.tabConsoles.Controls.Add(this.picLoaderConsole);
            this.tabConsoles.Controls.Add(this.pnlDownloadConsoles);
            this.tabConsoles.Controls.Add(this.dgvConsoles);
            this.tabConsoles.Location = new System.Drawing.Point(4, 25);
            this.tabConsoles.Name = "tabConsoles";
            this.tabConsoles.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsoles.Size = new System.Drawing.Size(908, 545);
            this.tabConsoles.TabIndex = 0;
            this.tabConsoles.Text = "Consoles";
            // 
            // lblNotFoundConsoles
            // 
            this.lblNotFoundConsoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotFoundConsoles.Location = new System.Drawing.Point(9, 162);
            this.lblNotFoundConsoles.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNotFoundConsoles.Name = "lblNotFoundConsoles";
            this.lblNotFoundConsoles.Size = new System.Drawing.Size(890, 24);
            this.lblNotFoundConsoles.TabIndex = 3;
            this.lblNotFoundConsoles.Text = "No Consoles Found";
            this.lblNotFoundConsoles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLoaderConsole
            // 
            this.picLoaderConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoaderConsole.Image = global::RADB.Properties.Resources.loader;
            this.picLoaderConsole.Location = new System.Drawing.Point(6, 146);
            this.picLoaderConsole.Name = "picLoaderConsole";
            this.picLoaderConsole.Size = new System.Drawing.Size(896, 52);
            this.picLoaderConsole.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoaderConsole.TabIndex = 4;
            this.picLoaderConsole.TabStop = false;
            this.picLoaderConsole.Visible = false;
            // 
            // pnlDownloadConsoles
            // 
            this.pnlDownloadConsoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDownloadConsoles.Controls.Add(this.btnUpdateConsoles);
            this.pnlDownloadConsoles.Controls.Add(this.lblUpdateConsoles);
            this.pnlDownloadConsoles.Controls.Add(this.pgbConsoles);
            this.pnlDownloadConsoles.Controls.Add(this.lblProgressConsoles);
            this.pnlDownloadConsoles.Location = new System.Drawing.Point(0, 0);
            this.pnlDownloadConsoles.Name = "pnlDownloadConsoles";
            this.pnlDownloadConsoles.Size = new System.Drawing.Size(902, 31);
            this.pnlDownloadConsoles.TabIndex = 0;
            // 
            // btnUpdateConsoles
            // 
            this.btnUpdateConsoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateConsoles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnUpdateConsoles.FlatAppearance.BorderSize = 0;
            this.btnUpdateConsoles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnUpdateConsoles.Location = new System.Drawing.Point(5, 3);
            this.btnUpdateConsoles.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnUpdateConsoles.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnUpdateConsoles.Name = "btnUpdateConsoles";
            this.btnUpdateConsoles.Size = new System.Drawing.Size(144, 24);
            this.btnUpdateConsoles.TabIndex = 36;
            this.btnUpdateConsoles.Text = "Update Consoles";
            this.btnUpdateConsoles.Click += new System.EventHandler(this.btnUpdateConsoles_Click);
            // 
            // lblUpdateConsoles
            // 
            this.lblUpdateConsoles.Location = new System.Drawing.Point(155, 3);
            this.lblUpdateConsoles.Name = "lblUpdateConsoles";
            this.lblUpdateConsoles.Size = new System.Drawing.Size(110, 24);
            this.lblUpdateConsoles.TabIndex = 32;
            this.lblUpdateConsoles.Text = "00/00/0000 00:00:00";
            // 
            // pgbConsoles
            // 
            this.pgbConsoles.Location = new System.Drawing.Point(271, 3);
            this.pgbConsoles.MarqueeAnimationSpeed = 0;
            this.pgbConsoles.Name = "pgbConsoles";
            this.pgbConsoles.Size = new System.Drawing.Size(144, 24);
            this.pgbConsoles.Step = 1;
            this.pgbConsoles.TabIndex = 30;
            // 
            // lblProgressConsoles
            // 
            this.lblProgressConsoles.AutoSize = true;
            this.lblProgressConsoles.Location = new System.Drawing.Point(421, 7);
            this.lblProgressConsoles.MinimumSize = new System.Drawing.Size(110, 0);
            this.lblProgressConsoles.Name = "lblProgressConsoles";
            this.lblProgressConsoles.Size = new System.Drawing.Size(110, 16);
            this.lblProgressConsoles.TabIndex = 31;
            this.lblProgressConsoles.Text = "lblProgress";
            // 
            // dgvConsoles
            // 
            this.dgvConsoles.AllowUserToResizeColumns = false;
            this.dgvConsoles.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvConsoles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvConsoles.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConsoles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvConsoles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cID,
            this.cCompany,
            this.cName,
            this.cNumGames,
            this.cTotalGames});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConsoles.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvConsoles.Location = new System.Drawing.Point(6, 83);
            this.dgvConsoles.Name = "dgvConsoles";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConsoles.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvConsoles.RowTemplate.Height = 37;
            this.dgvConsoles.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConsoles.Size = new System.Drawing.Size(896, 459);
            this.dgvConsoles.TabIndex = 0;
            this.dgvConsoles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvConsoles_MouseDown);
            // 
            // cID
            // 
            this.cID.DataPropertyName = "ID";
            this.cID.HeaderText = "ID";
            this.cID.Name = "cID";
            this.cID.ReadOnly = true;
            this.cID.Width = 45;
            // 
            // cCompany
            // 
            this.cCompany.DataPropertyName = "Company";
            this.cCompany.HeaderText = "Company";
            this.cCompany.Name = "cCompany";
            this.cCompany.ReadOnly = true;
            this.cCompany.Width = 85;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cName.DataPropertyName = "Name";
            this.cName.HeaderText = "Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cNumGames
            // 
            this.cNumGames.DataPropertyName = "NumGames";
            this.cNumGames.HeaderText = "Games";
            this.cNumGames.Name = "cNumGames";
            this.cNumGames.ReadOnly = true;
            this.cNumGames.Width = 70;
            // 
            // cTotalGames
            // 
            this.cTotalGames.DataPropertyName = "TotalGames";
            this.cTotalGames.HeaderText = "TotalGames";
            this.cTotalGames.Name = "cTotalGames";
            this.cTotalGames.ReadOnly = true;
            // 
            // tabGames
            // 
            this.tabGames.BackColor = System.Drawing.Color.Transparent;
            this.tabGames.Controls.Add(this.pnlFilters);
            this.tabGames.Controls.Add(this.lblNotFoundGameList);
            this.tabGames.Controls.Add(this.picLoaderGameList);
            this.tabGames.Controls.Add(this.pnlGamesConsoleName);
            this.tabGames.Controls.Add(this.pnlDownloadGameList);
            this.tabGames.Controls.Add(this.dgvGames);
            this.tabGames.Location = new System.Drawing.Point(4, 25);
            this.tabGames.Name = "tabGames";
            this.tabGames.Padding = new System.Windows.Forms.Padding(3);
            this.tabGames.Size = new System.Drawing.Size(908, 545);
            this.tabGames.TabIndex = 2;
            this.tabGames.Text = "Games";
            // 
            // pnlFilters
            // 
            this.pnlFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilters.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.pnlFilters.Controls.Add(this.chkOfficial);
            this.pnlFilters.Controls.Add(this.chkPrototype);
            this.pnlFilters.Controls.Add(this.chkUnlicensed);
            this.pnlFilters.Controls.Add(this.chkDemo);
            this.pnlFilters.Controls.Add(this.chkWithoutAchievements);
            this.pnlFilters.Controls.Add(this.chkHack);
            this.pnlFilters.Controls.Add(this.chkHomebrew);
            this.pnlFilters.Controls.Add(this.chkSubset);
            this.pnlFilters.Controls.Add(this.chkTestKit);
            this.pnlFilters.Controls.Add(this.chkDemoted);
            this.pnlFilters.Location = new System.Drawing.Point(780, 83);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(100, 406);
            this.pnlFilters.TabIndex = 10;
            this.pnlFilters.Visible = false;
            // 
            // chkOfficial
            // 
            this.chkOfficial._Legend = "Official";
            this.chkOfficial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOfficial.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkOfficial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkOfficial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkOfficial.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkOfficial.Checked = true;
            this.chkOfficial.Location = new System.Drawing.Point(18, 47);
            this.chkOfficial.Name = "chkOfficial";
            this.chkOfficial.Size = new System.Drawing.Size(64, 34);
            this.chkOfficial.TabIndex = 13;
            // 
            // chkPrototype
            // 
            this.chkPrototype._Legend = "Prototype";
            this.chkPrototype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPrototype.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkPrototype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkPrototype.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkPrototype.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkPrototype.Checked = true;
            this.chkPrototype.Location = new System.Drawing.Point(18, 87);
            this.chkPrototype.Name = "chkPrototype";
            this.chkPrototype.Size = new System.Drawing.Size(64, 34);
            this.chkPrototype.TabIndex = 12;
            // 
            // chkUnlicensed
            // 
            this.chkUnlicensed._Legend = "Unlicensed";
            this.chkUnlicensed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUnlicensed.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkUnlicensed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkUnlicensed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkUnlicensed.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkUnlicensed.Checked = true;
            this.chkUnlicensed.Location = new System.Drawing.Point(18, 127);
            this.chkUnlicensed.Name = "chkUnlicensed";
            this.chkUnlicensed.Size = new System.Drawing.Size(64, 34);
            this.chkUnlicensed.TabIndex = 11;
            // 
            // chkDemo
            // 
            this.chkDemo._Legend = "Demo";
            this.chkDemo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDemo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkDemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkDemo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkDemo.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkDemo.Checked = true;
            this.chkDemo.Location = new System.Drawing.Point(18, 167);
            this.chkDemo.Name = "chkDemo";
            this.chkDemo.Size = new System.Drawing.Size(64, 34);
            this.chkDemo.TabIndex = 10;
            // 
            // chkWithoutAchievements
            // 
            this.chkWithoutAchievements._Legend = "No Trophy";
            this.chkWithoutAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWithoutAchievements.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkWithoutAchievements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkWithoutAchievements.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkWithoutAchievements.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkWithoutAchievements.Checked = true;
            this.chkWithoutAchievements.Location = new System.Drawing.Point(18, 7);
            this.chkWithoutAchievements.Name = "chkWithoutAchievements";
            this.chkWithoutAchievements.Size = new System.Drawing.Size(64, 34);
            this.chkWithoutAchievements.TabIndex = 3;
            // 
            // chkHack
            // 
            this.chkHack._Legend = "Hack";
            this.chkHack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkHack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkHack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkHack.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkHack.Checked = true;
            this.chkHack.Location = new System.Drawing.Point(18, 207);
            this.chkHack.Name = "chkHack";
            this.chkHack.Size = new System.Drawing.Size(64, 34);
            this.chkHack.TabIndex = 9;
            // 
            // chkHomebrew
            // 
            this.chkHomebrew._Legend = "Homebrew";
            this.chkHomebrew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHomebrew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkHomebrew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkHomebrew.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkHomebrew.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkHomebrew.Checked = true;
            this.chkHomebrew.Location = new System.Drawing.Point(18, 247);
            this.chkHomebrew.Name = "chkHomebrew";
            this.chkHomebrew.Size = new System.Drawing.Size(64, 34);
            this.chkHomebrew.TabIndex = 8;
            // 
            // chkSubset
            // 
            this.chkSubset._Legend = "Subset";
            this.chkSubset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSubset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkSubset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkSubset.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkSubset.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkSubset.Checked = true;
            this.chkSubset.Location = new System.Drawing.Point(18, 287);
            this.chkSubset.Name = "chkSubset";
            this.chkSubset.Size = new System.Drawing.Size(64, 34);
            this.chkSubset.TabIndex = 14;
            // 
            // chkTestKit
            // 
            this.chkTestKit._Legend = "TestKit";
            this.chkTestKit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTestKit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkTestKit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkTestKit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkTestKit.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkTestKit.Checked = true;
            this.chkTestKit.Location = new System.Drawing.Point(18, 327);
            this.chkTestKit.Name = "chkTestKit";
            this.chkTestKit.Size = new System.Drawing.Size(64, 34);
            this.chkTestKit.TabIndex = 15;
            // 
            // chkDemoted
            // 
            this.chkDemoted._Legend = "gRAveyard";
            this.chkDemoted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDemoted.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkDemoted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkDemoted.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkDemoted.BorderColorFocus = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.chkDemoted.Checked = true;
            this.chkDemoted.Location = new System.Drawing.Point(18, 367);
            this.chkDemoted.Name = "chkDemoted";
            this.chkDemoted.Size = new System.Drawing.Size(64, 34);
            this.chkDemoted.TabIndex = 16;
            // 
            // lblNotFoundGameList
            // 
            this.lblNotFoundGameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotFoundGameList.Location = new System.Drawing.Point(9, 162);
            this.lblNotFoundGameList.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNotFoundGameList.Name = "lblNotFoundGameList";
            this.lblNotFoundGameList.Size = new System.Drawing.Size(890, 24);
            this.lblNotFoundGameList.TabIndex = 2;
            this.lblNotFoundGameList.Text = "No Games Found";
            this.lblNotFoundGameList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLoaderGameList
            // 
            this.picLoaderGameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoaderGameList.Image = global::RADB.Properties.Resources.loader;
            this.picLoaderGameList.Location = new System.Drawing.Point(6, 146);
            this.picLoaderGameList.Name = "picLoaderGameList";
            this.picLoaderGameList.Size = new System.Drawing.Size(896, 52);
            this.picLoaderGameList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoaderGameList.TabIndex = 5;
            this.picLoaderGameList.TabStop = false;
            this.picLoaderGameList.Visible = false;
            // 
            // pnlGamesConsoleName
            // 
            this.pnlGamesConsoleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGamesConsoleName.Controls.Add(this.lblConsoleGamesTotal);
            this.pnlGamesConsoleName.Controls.Add(this.lblConsoleName);
            this.pnlGamesConsoleName.Controls.Add(this.txtSearchGames);
            this.pnlGamesConsoleName.Controls.Add(this.btnGameFilters);
            this.pnlGamesConsoleName.Location = new System.Drawing.Point(6, 32);
            this.pnlGamesConsoleName.Name = "pnlGamesConsoleName";
            this.pnlGamesConsoleName.Size = new System.Drawing.Size(896, 44);
            this.pnlGamesConsoleName.TabIndex = 6;
            // 
            // lblConsoleGamesTotal
            // 
            this.lblConsoleGamesTotal.AutoSize = true;
            this.lblConsoleGamesTotal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsoleGamesTotal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblConsoleGamesTotal.Location = new System.Drawing.Point(7, 24);
            this.lblConsoleGamesTotal.MinimumSize = new System.Drawing.Size(48, 14);
            this.lblConsoleGamesTotal.Name = "lblConsoleGamesTotal";
            this.lblConsoleGamesTotal.Size = new System.Drawing.Size(62, 14);
            this.lblConsoleGamesTotal.TabIndex = 1;
            this.lblConsoleGamesTotal.Text = "000 Games";
            // 
            // lblConsoleName
            // 
            this.lblConsoleName.AutoSize = true;
            this.lblConsoleName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsoleName.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblConsoleName.Location = new System.Drawing.Point(3, 3);
            this.lblConsoleName.MinimumSize = new System.Drawing.Size(48, 22);
            this.lblConsoleName.Name = "lblConsoleName";
            this.lblConsoleName.Size = new System.Drawing.Size(66, 22);
            this.lblConsoleName.TabIndex = 0;
            this.lblConsoleName.Text = "Console";
            this.lblConsoleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearchGames
            // 
            this.txtSearchGames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchGames.LabelText = "Search Game";
            this.txtSearchGames.Location = new System.Drawing.Point(586, 5);
            this.txtSearchGames.Name = "txtSearchGames";
            this.txtSearchGames.previousText = "";
            this.txtSearchGames.Size = new System.Drawing.Size(182, 34);
            this.txtSearchGames.TabIndex = 2;
            // 
            // btnGameFilters
            // 
            this.btnGameFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnGameFilters.FlatAppearance.BorderSize = 0;
            this.btnGameFilters.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnGameFilters.Location = new System.Drawing.Point(774, 5);
            this.btnGameFilters.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnGameFilters.Name = "btnGameFilters";
            this.btnGameFilters.Size = new System.Drawing.Size(100, 34);
            this.btnGameFilters.TabIndex = 11;
            this.btnGameFilters.Text = "Filters";
            this.btnGameFilters.Click += new System.EventHandler(this.btnGameFilters_Click);
            // 
            // pnlDownloadGameList
            // 
            this.pnlDownloadGameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDownloadGameList.Controls.Add(this.btnUpdateGameList);
            this.pnlDownloadGameList.Controls.Add(this.lblUpdateGameList);
            this.pnlDownloadGameList.Controls.Add(this.pgbGameList);
            this.pnlDownloadGameList.Controls.Add(this.lblProgressGameList);
            this.pnlDownloadGameList.Location = new System.Drawing.Point(0, 0);
            this.pnlDownloadGameList.Name = "pnlDownloadGameList";
            this.pnlDownloadGameList.Size = new System.Drawing.Size(902, 31);
            this.pnlDownloadGameList.TabIndex = 0;
            // 
            // btnUpdateGameList
            // 
            this.btnUpdateGameList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateGameList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnUpdateGameList.FlatAppearance.BorderSize = 0;
            this.btnUpdateGameList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnUpdateGameList.Location = new System.Drawing.Point(5, 3);
            this.btnUpdateGameList.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnUpdateGameList.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnUpdateGameList.Name = "btnUpdateGameList";
            this.btnUpdateGameList.Size = new System.Drawing.Size(144, 24);
            this.btnUpdateGameList.TabIndex = 35;
            this.btnUpdateGameList.Text = "Update Games";
            this.btnUpdateGameList.Click += new System.EventHandler(this.btnUpdateGameList_Click);
            // 
            // lblUpdateGameList
            // 
            this.lblUpdateGameList.Location = new System.Drawing.Point(155, 3);
            this.lblUpdateGameList.Name = "lblUpdateGameList";
            this.lblUpdateGameList.Size = new System.Drawing.Size(110, 24);
            this.lblUpdateGameList.TabIndex = 34;
            this.lblUpdateGameList.Text = "00/00/0000 00:00:00";
            this.lblUpdateGameList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbGameList
            // 
            this.pgbGameList.Location = new System.Drawing.Point(271, 3);
            this.pgbGameList.MarqueeAnimationSpeed = 0;
            this.pgbGameList.Name = "pgbGameList";
            this.pgbGameList.Size = new System.Drawing.Size(144, 24);
            this.pgbGameList.Step = 1;
            this.pgbGameList.TabIndex = 31;
            // 
            // lblProgressGameList
            // 
            this.lblProgressGameList.AutoSize = true;
            this.lblProgressGameList.Location = new System.Drawing.Point(421, 7);
            this.lblProgressGameList.MinimumSize = new System.Drawing.Size(110, 0);
            this.lblProgressGameList.Name = "lblProgressGameList";
            this.lblProgressGameList.Size = new System.Drawing.Size(110, 16);
            this.lblProgressGameList.TabIndex = 33;
            this.lblProgressGameList.Text = "lblProgress";
            // 
            // dgvGames
            // 
            this.dgvGames.AllowUserToResizeColumns = false;
            this.dgvGames.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.dgvGames.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvGames.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gID,
            this.gIconBitmap,
            this.gTitle,
            this.gConsole,
            this.gNumAchievements,
            this.gPoints,
            this.cNumLeaderboards,
            this.gLastUpdated});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGames.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvGames.Location = new System.Drawing.Point(6, 83);
            this.dgvGames.Name = "dgvGames";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGames.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvGames.RowTemplate.Height = 37;
            this.dgvGames.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGames.Size = new System.Drawing.Size(896, 459);
            this.dgvGames.TabIndex = 1;
            this.dgvGames.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvGames_MouseDown);
            // 
            // gID
            // 
            this.gID.DataPropertyName = "ID";
            this.gID.HeaderText = "ID";
            this.gID.Name = "gID";
            this.gID.ReadOnly = true;
            this.gID.Width = 45;
            // 
            // gIconBitmap
            // 
            this.gIconBitmap.DataPropertyName = "ImageIconBitmap";
            this.gIconBitmap.HeaderText = "Icon";
            this.gIconBitmap.Name = "gIconBitmap";
            this.gIconBitmap.ReadOnly = true;
            this.gIconBitmap.Width = 36;
            // 
            // gTitle
            // 
            this.gTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gTitle.DataPropertyName = "Title";
            this.gTitle.HeaderText = "Title";
            this.gTitle.Name = "gTitle";
            this.gTitle.ReadOnly = true;
            // 
            // gConsole
            // 
            this.gConsole.DataPropertyName = "ConsoleName";
            this.gConsole.HeaderText = "Console";
            this.gConsole.Name = "gConsole";
            this.gConsole.ReadOnly = true;
            // 
            // gNumAchievements
            // 
            this.gNumAchievements.DataPropertyName = "NumAchievements";
            this.gNumAchievements.HeaderText = "Trophies";
            this.gNumAchievements.Name = "gNumAchievements";
            this.gNumAchievements.ReadOnly = true;
            this.gNumAchievements.Width = 80;
            // 
            // gPoints
            // 
            this.gPoints.DataPropertyName = "Points";
            this.gPoints.HeaderText = "Points";
            this.gPoints.Name = "gPoints";
            this.gPoints.ReadOnly = true;
            this.gPoints.Width = 65;
            // 
            // cNumLeaderboards
            // 
            this.cNumLeaderboards.DataPropertyName = "NumLeaderboards";
            this.cNumLeaderboards.HeaderText = "Scores";
            this.cNumLeaderboards.Name = "cNumLeaderboards";
            this.cNumLeaderboards.ReadOnly = true;
            this.cNumLeaderboards.Width = 70;
            // 
            // gLastUpdated
            // 
            this.gLastUpdated.DataPropertyName = "DateModified";
            this.gLastUpdated.HeaderText = "Last Updated";
            this.gLastUpdated.Name = "gLastUpdated";
            this.gLastUpdated.ReadOnly = true;
            this.gLastUpdated.Width = 105;
            // 
            // tabGameInfo
            // 
            this.tabGameInfo.BackColor = System.Drawing.Color.Transparent;
            this.tabGameInfo.Controls.Add(this.pnlInfoScroll);
            this.tabGameInfo.Controls.Add(this.lblInfoName);
            this.tabGameInfo.Controls.Add(this.pnlDownloadInfo);
            this.tabGameInfo.Location = new System.Drawing.Point(4, 25);
            this.tabGameInfo.Name = "tabGameInfo";
            this.tabGameInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabGameInfo.Size = new System.Drawing.Size(908, 545);
            this.tabGameInfo.TabIndex = 1;
            this.tabGameInfo.Text = "Game Info";
            // 
            // pnlInfoScroll
            // 
            this.pnlInfoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfoScroll.AutoScroll = true;
            this.pnlInfoScroll.BorderSize = 0;
            this.pnlInfoScroll.Controls.Add(this.gpbInfo);
            this.pnlInfoScroll.Controls.Add(this.gpbInfoAchievements);
            this.pnlInfoScroll.Location = new System.Drawing.Point(0, 62);
            this.pnlInfoScroll.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInfoScroll.Name = "pnlInfoScroll";
            this.pnlInfoScroll.NoScrollOnFocus = true;
            this.pnlInfoScroll.Size = new System.Drawing.Size(902, 480);
            this.pnlInfoScroll.TabIndex = 16;
            // 
            // gpbInfo
            // 
            this.gpbInfo.Controls.Add(this.pnlInfoBoxArt);
            this.gpbInfo.Controls.Add(this.pnlInfoImages);
            this.gpbInfo.Controls.Add(this.pnlInfoTop);
            this.gpbInfo.Location = new System.Drawing.Point(6, 3);
            this.gpbInfo.Name = "gpbInfo";
            this.gpbInfo.Size = new System.Drawing.Size(876, 320);
            this.gpbInfo.TabIndex = 6;
            this.gpbInfo.TabStop = false;
            this.gpbInfo.Text = "Info";
            // 
            // pnlInfoBoxArt
            // 
            this.pnlInfoBoxArt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.pnlInfoBoxArt.BorderSize = 0;
            this.pnlInfoBoxArt.Controls.Add(this.picInfoBoxArt);
            this.pnlInfoBoxArt.Location = new System.Drawing.Point(558, 19);
            this.pnlInfoBoxArt.Name = "pnlInfoBoxArt";
            this.pnlInfoBoxArt.Size = new System.Drawing.Size(312, 295);
            this.pnlInfoBoxArt.TabIndex = 21;
            // 
            // picInfoBoxArt
            // 
            this.picInfoBoxArt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picInfoBoxArt.BackColor = System.Drawing.Color.PaleTurquoise;
            this.picInfoBoxArt.Location = new System.Drawing.Point(6, 6);
            this.picInfoBoxArt.Margin = new System.Windows.Forms.Padding(6);
            this.picInfoBoxArt.MaximumSize = new System.Drawing.Size(300, 283);
            this.picInfoBoxArt.Name = "picInfoBoxArt";
            this.picInfoBoxArt.Size = new System.Drawing.Size(300, 283);
            this.picInfoBoxArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfoBoxArt.TabIndex = 20;
            this.picInfoBoxArt.TabStop = false;
            // 
            // pnlInfoImages
            // 
            this.pnlInfoImages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.pnlInfoImages.BorderSize = 0;
            this.pnlInfoImages.Controls.Add(this.pnlInfoInGame);
            this.pnlInfoImages.Controls.Add(this.pnlInfoTitle);
            this.pnlInfoImages.Location = new System.Drawing.Point(6, 152);
            this.pnlInfoImages.MinimumSize = new System.Drawing.Size(538, 156);
            this.pnlInfoImages.Name = "pnlInfoImages";
            this.pnlInfoImages.Size = new System.Drawing.Size(546, 162);
            this.pnlInfoImages.TabIndex = 16;
            // 
            // pnlInfoInGame
            // 
            this.pnlInfoInGame.BorderSize = 0;
            this.pnlInfoInGame.Controls.Add(this.picInfoInGame);
            this.pnlInfoInGame.Location = new System.Drawing.Point(280, 6);
            this.pnlInfoInGame.Name = "pnlInfoInGame";
            this.pnlInfoInGame.Size = new System.Drawing.Size(260, 150);
            this.pnlInfoInGame.TabIndex = 16;
            // 
            // picInfoInGame
            // 
            this.picInfoInGame.BackColor = System.Drawing.Color.PaleTurquoise;
            this.picInfoInGame.Location = new System.Drawing.Point(0, 0);
            this.picInfoInGame.Margin = new System.Windows.Forms.Padding(6);
            this.picInfoInGame.MaximumSize = new System.Drawing.Size(260, 240);
            this.picInfoInGame.Name = "picInfoInGame";
            this.picInfoInGame.Size = new System.Drawing.Size(260, 150);
            this.picInfoInGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfoInGame.TabIndex = 14;
            this.picInfoInGame.TabStop = false;
            // 
            // pnlInfoTitle
            // 
            this.pnlInfoTitle.BorderSize = 0;
            this.pnlInfoTitle.Controls.Add(this.picInfoTitle);
            this.pnlInfoTitle.Location = new System.Drawing.Point(6, 6);
            this.pnlInfoTitle.Name = "pnlInfoTitle";
            this.pnlInfoTitle.Size = new System.Drawing.Size(260, 150);
            this.pnlInfoTitle.TabIndex = 15;
            // 
            // picInfoTitle
            // 
            this.picInfoTitle.BackColor = System.Drawing.Color.LightGray;
            this.picInfoTitle.Interpolation = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.picInfoTitle.Location = new System.Drawing.Point(0, 0);
            this.picInfoTitle.Margin = new System.Windows.Forms.Padding(6);
            this.picInfoTitle.MaximumSize = new System.Drawing.Size(260, 240);
            this.picInfoTitle.Name = "picInfoTitle";
            this.picInfoTitle.Size = new System.Drawing.Size(260, 150);
            this.picInfoTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfoTitle.TabIndex = 13;
            this.picInfoTitle.TabStop = false;
            // 
            // pnlInfoTop
            // 
            this.pnlInfoTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfoTop.Controls.Add(this.lblInfoReleased1);
            this.pnlInfoTop.Controls.Add(this.lblInfoDeveloper1);
            this.pnlInfoTop.Controls.Add(this.lblInfoGenre1);
            this.pnlInfoTop.Controls.Add(this.lblInfoPublisher1);
            this.pnlInfoTop.Controls.Add(this.picInfoIcon);
            this.pnlInfoTop.Controls.Add(this.lblInfoReleased0);
            this.pnlInfoTop.Controls.Add(this.lblInfoDeveloper0);
            this.pnlInfoTop.Controls.Add(this.lblInfoGenre0);
            this.pnlInfoTop.Controls.Add(this.lblInfoPublisher0);
            this.pnlInfoTop.Location = new System.Drawing.Point(12, 19);
            this.pnlInfoTop.Name = "pnlInfoTop";
            this.pnlInfoTop.Size = new System.Drawing.Size(540, 96);
            this.pnlInfoTop.TabIndex = 9;
            // 
            // lblInfoReleased1
            // 
            this.lblInfoReleased1.AutoSize = true;
            this.lblInfoReleased1.Location = new System.Drawing.Point(190, 76);
            this.lblInfoReleased1.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfoReleased1.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblInfoReleased1.Name = "lblInfoReleased1";
            this.lblInfoReleased1.Size = new System.Drawing.Size(53, 16);
            this.lblInfoReleased1.TabIndex = 12;
            this.lblInfoReleased1.Text = "Released";
            // 
            // lblInfoDeveloper1
            // 
            this.lblInfoDeveloper1.AutoSize = true;
            this.lblInfoDeveloper1.Location = new System.Drawing.Point(190, 4);
            this.lblInfoDeveloper1.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfoDeveloper1.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblInfoDeveloper1.Name = "lblInfoDeveloper1";
            this.lblInfoDeveloper1.Size = new System.Drawing.Size(60, 16);
            this.lblInfoDeveloper1.TabIndex = 9;
            this.lblInfoDeveloper1.Text = "Developer";
            // 
            // lblInfoGenre1
            // 
            this.lblInfoGenre1.AutoSize = true;
            this.lblInfoGenre1.Location = new System.Drawing.Point(190, 52);
            this.lblInfoGenre1.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfoGenre1.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblInfoGenre1.Name = "lblInfoGenre1";
            this.lblInfoGenre1.Size = new System.Drawing.Size(48, 16);
            this.lblInfoGenre1.TabIndex = 11;
            this.lblInfoGenre1.Text = "Genre";
            // 
            // lblInfoPublisher1
            // 
            this.lblInfoPublisher1.AutoSize = true;
            this.lblInfoPublisher1.Location = new System.Drawing.Point(190, 28);
            this.lblInfoPublisher1.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfoPublisher1.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblInfoPublisher1.Name = "lblInfoPublisher1";
            this.lblInfoPublisher1.Size = new System.Drawing.Size(56, 16);
            this.lblInfoPublisher1.TabIndex = 10;
            this.lblInfoPublisher1.Text = "Publisher";
            // 
            // picInfoIcon
            // 
            this.picInfoIcon.Location = new System.Drawing.Point(0, 0);
            this.picInfoIcon.Margin = new System.Windows.Forms.Padding(0);
            this.picInfoIcon.Name = "picInfoIcon";
            this.picInfoIcon.Size = new System.Drawing.Size(96, 96);
            this.picInfoIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picInfoIcon.TabIndex = 4;
            this.picInfoIcon.TabStop = false;
            // 
            // lblInfoReleased0
            // 
            this.lblInfoReleased0.AutoSize = true;
            this.lblInfoReleased0.Location = new System.Drawing.Point(112, 76);
            this.lblInfoReleased0.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfoReleased0.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblInfoReleased0.Name = "lblInfoReleased0";
            this.lblInfoReleased0.Size = new System.Drawing.Size(56, 16);
            this.lblInfoReleased0.TabIndex = 8;
            this.lblInfoReleased0.Text = "Released:";
            // 
            // lblInfoDeveloper0
            // 
            this.lblInfoDeveloper0.AutoSize = true;
            this.lblInfoDeveloper0.Location = new System.Drawing.Point(105, 4);
            this.lblInfoDeveloper0.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfoDeveloper0.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblInfoDeveloper0.Name = "lblInfoDeveloper0";
            this.lblInfoDeveloper0.Size = new System.Drawing.Size(63, 16);
            this.lblInfoDeveloper0.TabIndex = 5;
            this.lblInfoDeveloper0.Text = "Developer:";
            // 
            // lblInfoGenre0
            // 
            this.lblInfoGenre0.AutoSize = true;
            this.lblInfoGenre0.Location = new System.Drawing.Point(127, 52);
            this.lblInfoGenre0.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfoGenre0.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblInfoGenre0.Name = "lblInfoGenre0";
            this.lblInfoGenre0.Size = new System.Drawing.Size(48, 16);
            this.lblInfoGenre0.TabIndex = 7;
            this.lblInfoGenre0.Text = "Genre:";
            // 
            // lblInfoPublisher0
            // 
            this.lblInfoPublisher0.AutoSize = true;
            this.lblInfoPublisher0.Location = new System.Drawing.Point(109, 28);
            this.lblInfoPublisher0.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfoPublisher0.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblInfoPublisher0.Name = "lblInfoPublisher0";
            this.lblInfoPublisher0.Size = new System.Drawing.Size(59, 16);
            this.lblInfoPublisher0.TabIndex = 6;
            this.lblInfoPublisher0.Text = "Publisher:";
            // 
            // gpbInfoAchievements
            // 
            this.gpbInfoAchievements.Controls.Add(this.txtSearchAchiev);
            this.gpbInfoAchievements.Controls.Add(this.btnHashes);
            this.gpbInfoAchievements.Controls.Add(this.dgvAchievements);
            this.gpbInfoAchievements.Controls.Add(this.btnGamePage);
            this.gpbInfoAchievements.Controls.Add(this.lblInfoAchievements);
            this.gpbInfoAchievements.Location = new System.Drawing.Point(6, 329);
            this.gpbInfoAchievements.MinimumSize = new System.Drawing.Size(557, 54);
            this.gpbInfoAchievements.Name = "gpbInfoAchievements";
            this.gpbInfoAchievements.Size = new System.Drawing.Size(876, 151);
            this.gpbInfoAchievements.TabIndex = 5;
            this.gpbInfoAchievements.TabStop = false;
            this.gpbInfoAchievements.Text = "Trophies";
            // 
            // txtSearchAchiev
            // 
            this.txtSearchAchiev.LabelText = "Search Trophy";
            this.txtSearchAchiev.Location = new System.Drawing.Point(688, 16);
            this.txtSearchAchiev.Name = "txtSearchAchiev";
            this.txtSearchAchiev.previousText = "";
            this.txtSearchAchiev.Size = new System.Drawing.Size(182, 34);
            this.txtSearchAchiev.TabIndex = 37;
            // 
            // btnHashes
            // 
            this.btnHashes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHashes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnHashes.FlatAppearance.BorderSize = 0;
            this.btnHashes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnHashes.Location = new System.Drawing.Point(112, 16);
            this.btnHashes.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnHashes.Name = "btnHashes";
            this.btnHashes.Size = new System.Drawing.Size(100, 34);
            this.btnHashes.TabIndex = 36;
            this.btnHashes.Text = "Hashes";
            this.btnHashes.Click += new System.EventHandler(this.btnHashes_Click);
            // 
            // dgvAchievements
            // 
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            this.dgvAchievements.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAchievements.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAchievements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvAchievements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aOrder,
            this.aIconBitmap,
            this.aDescription,
            this.aPoints});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAchievements.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvAchievements.Location = new System.Drawing.Point(6, 54);
            this.dgvAchievements.Name = "dgvAchievements";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAchievements.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvAchievements.RowTemplate.Height = 72;
            this.dgvAchievements.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAchievements.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvAchievements.Size = new System.Drawing.Size(864, 30);
            this.dgvAchievements.TabIndex = 0;
            // 
            // aOrder
            // 
            this.aOrder.DataPropertyName = "DisplayOrder";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.aOrder.DefaultCellStyle = dataGridViewCellStyle11;
            this.aOrder.HeaderText = "Order";
            this.aOrder.Name = "aOrder";
            this.aOrder.ReadOnly = true;
            this.aOrder.Width = 65;
            // 
            // aIconBitmap
            // 
            this.aIconBitmap.DataPropertyName = "DisplayOrder";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.aIconBitmap.DefaultCellStyle = dataGridViewCellStyle12;
            this.aIconBitmap.HeaderText = "Icon";
            this.aIconBitmap.Name = "aIconBitmap";
            this.aIconBitmap.ReadOnly = true;
            this.aIconBitmap.Visible = false;
            this.aIconBitmap.Width = 72;
            // 
            // aDescription
            // 
            this.aDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.aDescription.DataPropertyName = "DescriptionComplete";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.aDescription.DefaultCellStyle = dataGridViewCellStyle13;
            this.aDescription.HeaderText = "Description";
            this.aDescription.Name = "aDescription";
            this.aDescription.ReadOnly = true;
            // 
            // aPoints
            // 
            this.aPoints.DataPropertyName = "Points";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.aPoints.DefaultCellStyle = dataGridViewCellStyle14;
            this.aPoints.HeaderText = "Points";
            this.aPoints.Name = "aPoints";
            this.aPoints.ReadOnly = true;
            this.aPoints.Width = 65;
            // 
            // btnGamePage
            // 
            this.btnGamePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGamePage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnGamePage.FlatAppearance.BorderSize = 0;
            this.btnGamePage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnGamePage.Location = new System.Drawing.Point(6, 16);
            this.btnGamePage.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnGamePage.Name = "btnGamePage";
            this.btnGamePage.Size = new System.Drawing.Size(100, 34);
            this.btnGamePage.TabIndex = 6;
            this.btnGamePage.Text = "Game Page";
            this.btnGamePage.Click += new System.EventHandler(this.btnGamePage_Click);
            // 
            // lblInfoAchievements
            // 
            this.lblInfoAchievements.AutoSize = true;
            this.lblInfoAchievements.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoAchievements.Location = new System.Drawing.Point(218, 23);
            this.lblInfoAchievements.Name = "lblInfoAchievements";
            this.lblInfoAchievements.Size = new System.Drawing.Size(146, 24);
            this.lblInfoAchievements.TabIndex = 35;
            this.lblInfoAchievements.Text = "0 Trophies: 0 points";
            this.lblInfoAchievements.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInfoName
            // 
            this.lblInfoName.AutoSize = true;
            this.lblInfoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoName.Location = new System.Drawing.Point(6, 34);
            this.lblInfoName.Name = "lblInfoName";
            this.lblInfoName.Size = new System.Drawing.Size(101, 25);
            this.lblInfoName.TabIndex = 3;
            this.lblInfoName.Text = "Game Title";
            // 
            // pnlDownloadInfo
            // 
            this.pnlDownloadInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDownloadInfo.Controls.Add(this.btnUpdateInfo);
            this.pnlDownloadInfo.Controls.Add(this.lblUpdateInfo);
            this.pnlDownloadInfo.Controls.Add(this.pgbInfo);
            this.pnlDownloadInfo.Controls.Add(this.lblProgressInfo);
            this.pnlDownloadInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlDownloadInfo.Name = "pnlDownloadInfo";
            this.pnlDownloadInfo.Size = new System.Drawing.Size(902, 31);
            this.pnlDownloadInfo.TabIndex = 7;
            // 
            // btnUpdateInfo
            // 
            this.btnUpdateInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnUpdateInfo.FlatAppearance.BorderSize = 0;
            this.btnUpdateInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnUpdateInfo.Location = new System.Drawing.Point(5, 3);
            this.btnUpdateInfo.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnUpdateInfo.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnUpdateInfo.Name = "btnUpdateInfo";
            this.btnUpdateInfo.Size = new System.Drawing.Size(144, 24);
            this.btnUpdateInfo.TabIndex = 35;
            this.btnUpdateInfo.Text = "Update Info";
            this.btnUpdateInfo.Click += new System.EventHandler(this.btnUpdateInfo_Click);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.Location = new System.Drawing.Point(155, 3);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(110, 24);
            this.lblUpdateInfo.TabIndex = 34;
            this.lblUpdateInfo.Text = "00/00/0000 00:00:00";
            this.lblUpdateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbInfo
            // 
            this.pgbInfo.Location = new System.Drawing.Point(271, 3);
            this.pgbInfo.MarqueeAnimationSpeed = 0;
            this.pgbInfo.Name = "pgbInfo";
            this.pgbInfo.Size = new System.Drawing.Size(144, 24);
            this.pgbInfo.Step = 1;
            this.pgbInfo.TabIndex = 31;
            // 
            // lblProgressInfo
            // 
            this.lblProgressInfo.AutoSize = true;
            this.lblProgressInfo.Location = new System.Drawing.Point(421, 7);
            this.lblProgressInfo.MinimumSize = new System.Drawing.Size(110, 0);
            this.lblProgressInfo.Name = "lblProgressInfo";
            this.lblProgressInfo.Size = new System.Drawing.Size(110, 16);
            this.lblProgressInfo.TabIndex = 33;
            this.lblProgressInfo.Text = "lblProgress";
            // 
            // tabGamesToPlay
            // 
            this.tabGamesToPlay.BackColor = System.Drawing.Color.Transparent;
            this.tabGamesToPlay.Controls.Add(this.lblNotFoundGamesToPlay);
            this.tabGamesToPlay.Controls.Add(this.dgvGamesToPlay);
            this.tabGamesToPlay.Location = new System.Drawing.Point(4, 25);
            this.tabGamesToPlay.Name = "tabGamesToPlay";
            this.tabGamesToPlay.Size = new System.Drawing.Size(908, 545);
            this.tabGamesToPlay.TabIndex = 5;
            this.tabGamesToPlay.Text = "Games to Play";
            // 
            // lblNotFoundGamesToPlay
            // 
            this.lblNotFoundGamesToPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotFoundGamesToPlay.Location = new System.Drawing.Point(9, 162);
            this.lblNotFoundGamesToPlay.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNotFoundGamesToPlay.Name = "lblNotFoundGamesToPlay";
            this.lblNotFoundGamesToPlay.Size = new System.Drawing.Size(890, 24);
            this.lblNotFoundGamesToPlay.TabIndex = 5;
            this.lblNotFoundGamesToPlay.Text = "No Games Found: Add a game with right click in Games Tab Grid";
            this.lblNotFoundGamesToPlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotFoundGamesToPlay.Visible = false;
            // 
            // dgvGamesToPlay
            // 
            this.dgvGamesToPlay.AllowUserToResizeColumns = false;
            this.dgvGamesToPlay.AllowUserToResizeRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            this.dgvGamesToPlay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvGamesToPlay.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGamesToPlay.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvGamesToPlay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gpID,
            this.gpIconBitmap,
            this.gpTitle,
            this.gpConsole,
            this.gpNumAchievements,
            this.gpPoints,
            this.gpNumLeaderboards,
            this.gpLastUpdated});
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGamesToPlay.DefaultCellStyle = dataGridViewCellStyle25;
            this.dgvGamesToPlay.Location = new System.Drawing.Point(6, 83);
            this.dgvGamesToPlay.Name = "dgvGamesToPlay";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle26.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGamesToPlay.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.dgvGamesToPlay.RowTemplate.Height = 37;
            this.dgvGamesToPlay.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGamesToPlay.Size = new System.Drawing.Size(896, 459);
            this.dgvGamesToPlay.TabIndex = 3;
            this.dgvGamesToPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvGamesToPlay_MouseDown);
            // 
            // gpID
            // 
            this.gpID.DataPropertyName = "ID";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gpID.DefaultCellStyle = dataGridViewCellStyle19;
            this.gpID.HeaderText = "ID";
            this.gpID.Name = "gpID";
            this.gpID.ReadOnly = true;
            this.gpID.Width = 45;
            // 
            // gpIconBitmap
            // 
            this.gpIconBitmap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.gpIconBitmap.DataPropertyName = "ImageIconBitmap";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.NullValue = null;
            dataGridViewCellStyle20.Padding = new System.Windows.Forms.Padding(2);
            this.gpIconBitmap.DefaultCellStyle = dataGridViewCellStyle20;
            this.gpIconBitmap.HeaderText = "Icon";
            this.gpIconBitmap.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.gpIconBitmap.Name = "gpIconBitmap";
            this.gpIconBitmap.ReadOnly = true;
            this.gpIconBitmap.Width = 36;
            // 
            // gpTitle
            // 
            this.gpTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.gpTitle.DataPropertyName = "Title";
            this.gpTitle.HeaderText = "Title";
            this.gpTitle.Name = "gpTitle";
            this.gpTitle.ReadOnly = true;
            // 
            // gpConsole
            // 
            this.gpConsole.DataPropertyName = "ConsoleName";
            this.gpConsole.HeaderText = "Console";
            this.gpConsole.Name = "gpConsole";
            this.gpConsole.ReadOnly = true;
            // 
            // gpNumAchievements
            // 
            this.gpNumAchievements.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.gpNumAchievements.DataPropertyName = "NumAchievements";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.Format = "N0";
            dataGridViewCellStyle21.NullValue = null;
            this.gpNumAchievements.DefaultCellStyle = dataGridViewCellStyle21;
            this.gpNumAchievements.HeaderText = "Trophies";
            this.gpNumAchievements.Name = "gpNumAchievements";
            this.gpNumAchievements.ReadOnly = true;
            this.gpNumAchievements.Width = 80;
            // 
            // gpPoints
            // 
            this.gpPoints.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.gpPoints.DataPropertyName = "Points";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.Format = "N0";
            dataGridViewCellStyle22.NullValue = null;
            this.gpPoints.DefaultCellStyle = dataGridViewCellStyle22;
            this.gpPoints.HeaderText = "Points";
            this.gpPoints.Name = "gpPoints";
            this.gpPoints.ReadOnly = true;
            this.gpPoints.Width = 65;
            // 
            // gpNumLeaderboards
            // 
            this.gpNumLeaderboards.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.gpNumLeaderboards.DataPropertyName = "NumLeaderboards";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.Format = "N0";
            dataGridViewCellStyle23.NullValue = null;
            this.gpNumLeaderboards.DefaultCellStyle = dataGridViewCellStyle23;
            this.gpNumLeaderboards.HeaderText = "Scores";
            this.gpNumLeaderboards.Name = "gpNumLeaderboards";
            this.gpNumLeaderboards.ReadOnly = true;
            this.gpNumLeaderboards.Width = 70;
            // 
            // gpLastUpdated
            // 
            this.gpLastUpdated.DataPropertyName = "DateModified";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.Format = "dd MMM, yyyy";
            dataGridViewCellStyle24.NullValue = null;
            this.gpLastUpdated.DefaultCellStyle = dataGridViewCellStyle24;
            this.gpLastUpdated.HeaderText = "Last Updated";
            this.gpLastUpdated.Name = "gpLastUpdated";
            this.gpLastUpdated.ReadOnly = true;
            this.gpLastUpdated.Width = 105;
            // 
            // tabGamesToHide
            // 
            this.tabGamesToHide.BackColor = System.Drawing.Color.Transparent;
            this.tabGamesToHide.Controls.Add(this.lblNotFoundGamesToHide);
            this.tabGamesToHide.Controls.Add(this.dgvGamesToHide);
            this.tabGamesToHide.Location = new System.Drawing.Point(4, 25);
            this.tabGamesToHide.Name = "tabGamesToHide";
            this.tabGamesToHide.Size = new System.Drawing.Size(908, 545);
            this.tabGamesToHide.TabIndex = 6;
            this.tabGamesToHide.Text = "Games to Hide";
            // 
            // lblNotFoundGamesToHide
            // 
            this.lblNotFoundGamesToHide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotFoundGamesToHide.Location = new System.Drawing.Point(9, 162);
            this.lblNotFoundGamesToHide.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNotFoundGamesToHide.Name = "lblNotFoundGamesToHide";
            this.lblNotFoundGamesToHide.Size = new System.Drawing.Size(890, 24);
            this.lblNotFoundGamesToHide.TabIndex = 4;
            this.lblNotFoundGamesToHide.Text = "No Games Found: Add a game with right click in Games Tab Grid";
            this.lblNotFoundGamesToHide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotFoundGamesToHide.Visible = false;
            // 
            // dgvGamesToHide
            // 
            this.dgvGamesToHide.AllowUserToResizeColumns = false;
            this.dgvGamesToHide.AllowUserToResizeRows = false;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.White;
            this.dgvGamesToHide.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle27;
            this.dgvGamesToHide.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGamesToHide.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.dgvGamesToHide.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ghID,
            this.ghIconBitmap,
            this.ghTitle,
            this.ghConsole,
            this.ghNumAchievements,
            this.ghPoints,
            this.ghNumLeaderboards,
            this.ghLastUpdated});
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGamesToHide.DefaultCellStyle = dataGridViewCellStyle29;
            this.dgvGamesToHide.Location = new System.Drawing.Point(6, 83);
            this.dgvGamesToHide.Name = "dgvGamesToHide";
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle30.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGamesToHide.RowHeadersDefaultCellStyle = dataGridViewCellStyle30;
            this.dgvGamesToHide.RowTemplate.Height = 37;
            this.dgvGamesToHide.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGamesToHide.Size = new System.Drawing.Size(896, 459);
            this.dgvGamesToHide.TabIndex = 2;
            this.dgvGamesToHide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvGamesToHide_MouseDown);
            // 
            // ghID
            // 
            this.ghID.DataPropertyName = "ID";
            this.ghID.HeaderText = "ID";
            this.ghID.Name = "ghID";
            this.ghID.ReadOnly = true;
            this.ghID.Width = 45;
            // 
            // ghIconBitmap
            // 
            this.ghIconBitmap.DataPropertyName = "ImageIconBitmap";
            this.ghIconBitmap.HeaderText = "Icon";
            this.ghIconBitmap.Name = "ghIconBitmap";
            this.ghIconBitmap.ReadOnly = true;
            this.ghIconBitmap.Width = 36;
            // 
            // ghTitle
            // 
            this.ghTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ghTitle.DataPropertyName = "Title";
            this.ghTitle.HeaderText = "Title";
            this.ghTitle.Name = "ghTitle";
            this.ghTitle.ReadOnly = true;
            // 
            // ghConsole
            // 
            this.ghConsole.DataPropertyName = "ConsoleName";
            this.ghConsole.HeaderText = "Console";
            this.ghConsole.Name = "ghConsole";
            this.ghConsole.ReadOnly = true;
            // 
            // ghNumAchievements
            // 
            this.ghNumAchievements.DataPropertyName = "NumAchievements";
            this.ghNumAchievements.HeaderText = "Trophies";
            this.ghNumAchievements.Name = "ghNumAchievements";
            this.ghNumAchievements.ReadOnly = true;
            this.ghNumAchievements.Width = 80;
            // 
            // ghPoints
            // 
            this.ghPoints.DataPropertyName = "Points";
            this.ghPoints.HeaderText = "Points";
            this.ghPoints.Name = "ghPoints";
            this.ghPoints.ReadOnly = true;
            this.ghPoints.Width = 65;
            // 
            // ghNumLeaderboards
            // 
            this.ghNumLeaderboards.DataPropertyName = "NumLeaderboards";
            this.ghNumLeaderboards.HeaderText = "Scores";
            this.ghNumLeaderboards.Name = "ghNumLeaderboards";
            this.ghNumLeaderboards.ReadOnly = true;
            this.ghNumLeaderboards.Width = 70;
            // 
            // ghLastUpdated
            // 
            this.ghLastUpdated.DataPropertyName = "DateModified";
            this.ghLastUpdated.HeaderText = "Last Updated";
            this.ghLastUpdated.Name = "ghLastUpdated";
            this.ghLastUpdated.ReadOnly = true;
            this.ghLastUpdated.Width = 105;
            // 
            // tabUserInfo
            // 
            this.tabUserInfo.BackColor = System.Drawing.Color.Transparent;
            this.tabUserInfo.Controls.Add(this.lblUserStatus);
            this.tabUserInfo.Controls.Add(this.lblUserMotto);
            this.tabUserInfo.Controls.Add(this.lblUserName);
            this.tabUserInfo.Controls.Add(this.btnGetUserInfo);
            this.tabUserInfo.Controls.Add(this.picUserName);
            this.tabUserInfo.Controls.Add(this.gpbUserInfo);
            this.tabUserInfo.Controls.Add(this.gpbOverlay);
            this.tabUserInfo.Controls.Add(this.txtUsername);
            this.tabUserInfo.Location = new System.Drawing.Point(4, 25);
            this.tabUserInfo.Name = "tabUserInfo";
            this.tabUserInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserInfo.Size = new System.Drawing.Size(908, 545);
            this.tabUserInfo.TabIndex = 3;
            this.tabUserInfo.Text = "User Info";
            // 
            // lblUserStatus
            // 
            this.lblUserStatus.AutoSize = true;
            this.lblUserStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserStatus.Location = new System.Drawing.Point(235, 7);
            this.lblUserStatus.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserStatus.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserStatus.Name = "lblUserStatus";
            this.lblUserStatus.Size = new System.Drawing.Size(48, 18);
            this.lblUserStatus.TabIndex = 36;
            this.lblUserStatus.Text = "Status";
            // 
            // lblUserMotto
            // 
            this.lblUserMotto.AutoSize = true;
            this.lblUserMotto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserMotto.Location = new System.Drawing.Point(235, 58);
            this.lblUserMotto.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserMotto.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserMotto.Name = "lblUserMotto";
            this.lblUserMotto.Size = new System.Drawing.Size(48, 18);
            this.lblUserMotto.TabIndex = 35;
            this.lblUserMotto.Text = "Motto";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(234, 29);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(100, 25);
            this.lblUserName.TabIndex = 34;
            this.lblUserName.Text = "UserName";
            // 
            // btnGetUserInfo
            // 
            this.btnGetUserInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnGetUserInfo.FlatAppearance.BorderSize = 0;
            this.btnGetUserInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnGetUserInfo.Location = new System.Drawing.Point(6, 46);
            this.btnGetUserInfo.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnGetUserInfo.Name = "btnGetUserInfo";
            this.btnGetUserInfo.Size = new System.Drawing.Size(146, 24);
            this.btnGetUserInfo.TabIndex = 32;
            this.btnGetUserInfo.Text = "Get User Info";
            this.btnGetUserInfo.Click += new System.EventHandler(this.btnGetUserInfo_Click);
            // 
            // picUserName
            // 
            this.picUserName.Interpolation = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.picUserName.Location = new System.Drawing.Point(161, 6);
            this.picUserName.Margin = new System.Windows.Forms.Padding(6);
            this.picUserName.Name = "picUserName";
            this.picUserName.Size = new System.Drawing.Size(64, 64);
            this.picUserName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUserName.TabIndex = 14;
            this.picUserName.TabStop = false;
            // 
            // gpbUserInfo
            // 
            this.gpbUserInfo.Controls.Add(this.lblUserCompletion);
            this.gpbUserInfo.Controls.Add(this.lblUserSoftRank);
            this.gpbUserInfo.Controls.Add(this.lblUserSoftPoints);
            this.gpbUserInfo.Controls.Add(this.lblUserRetroRatio);
            this.gpbUserInfo.Controls.Add(this.lblUserRank);
            this.gpbUserInfo.Controls.Add(this.lblUserHCPoints);
            this.gpbUserInfo.Controls.Add(this.lblUserAccountType);
            this.gpbUserInfo.Controls.Add(this.lblUserLastActivity);
            this.gpbUserInfo.Controls.Add(this.flatLabelA10);
            this.gpbUserInfo.Controls.Add(this.flatLabelA9);
            this.gpbUserInfo.Controls.Add(this.flatLabelA8);
            this.gpbUserInfo.Controls.Add(this.flatLabelA7);
            this.gpbUserInfo.Controls.Add(this.flatLabelA6);
            this.gpbUserInfo.Controls.Add(this.flatLabelA5);
            this.gpbUserInfo.Controls.Add(this.flatLabelA4);
            this.gpbUserInfo.Controls.Add(this.flatLabelA3);
            this.gpbUserInfo.Controls.Add(this.lblUserMemberSince);
            this.gpbUserInfo.Controls.Add(this.flatLabelA1);
            this.gpbUserInfo.Location = new System.Drawing.Point(6, 76);
            this.gpbUserInfo.Name = "gpbUserInfo";
            this.gpbUserInfo.Size = new System.Drawing.Size(896, 100);
            this.gpbUserInfo.TabIndex = 33;
            this.gpbUserInfo.TabStop = false;
            this.gpbUserInfo.Text = "Info";
            // 
            // lblUserCompletion
            // 
            this.lblUserCompletion.AutoSize = true;
            this.lblUserCompletion.Location = new System.Drawing.Point(702, 68);
            this.lblUserCompletion.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserCompletion.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserCompletion.Name = "lblUserCompletion";
            this.lblUserCompletion.Size = new System.Drawing.Size(48, 16);
            this.lblUserCompletion.TabIndex = 28;
            this.lblUserCompletion.Text = "90.00%";
            // 
            // lblUserSoftRank
            // 
            this.lblUserSoftRank.AutoSize = true;
            this.lblUserSoftRank.Location = new System.Drawing.Point(702, 44);
            this.lblUserSoftRank.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserSoftRank.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserSoftRank.Name = "lblUserSoftRank";
            this.lblUserSoftRank.Size = new System.Drawing.Size(129, 16);
            this.lblUserSoftRank.TabIndex = 27;
            this.lblUserSoftRank.Text = "34000/34000 (Top 99%)";
            // 
            // lblUserSoftPoints
            // 
            this.lblUserSoftPoints.AutoSize = true;
            this.lblUserSoftPoints.Location = new System.Drawing.Point(702, 20);
            this.lblUserSoftPoints.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserSoftPoints.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserSoftPoints.Name = "lblUserSoftPoints";
            this.lblUserSoftPoints.Size = new System.Drawing.Size(49, 16);
            this.lblUserSoftPoints.TabIndex = 26;
            this.lblUserSoftPoints.Text = "0000000";
            // 
            // lblUserRetroRatio
            // 
            this.lblUserRetroRatio.AutoSize = true;
            this.lblUserRetroRatio.Location = new System.Drawing.Point(382, 68);
            this.lblUserRetroRatio.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserRetroRatio.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserRetroRatio.Name = "lblUserRetroRatio";
            this.lblUserRetroRatio.Size = new System.Drawing.Size(48, 16);
            this.lblUserRetroRatio.TabIndex = 25;
            this.lblUserRetroRatio.Text = "3.50";
            // 
            // lblUserRank
            // 
            this.lblUserRank.AutoSize = true;
            this.lblUserRank.Location = new System.Drawing.Point(382, 44);
            this.lblUserRank.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserRank.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserRank.Name = "lblUserRank";
            this.lblUserRank.Size = new System.Drawing.Size(129, 16);
            this.lblUserRank.TabIndex = 24;
            this.lblUserRank.Text = "34000/34000 (Top 99%)";
            // 
            // lblUserHCPoints
            // 
            this.lblUserHCPoints.AutoSize = true;
            this.lblUserHCPoints.Location = new System.Drawing.Point(382, 20);
            this.lblUserHCPoints.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserHCPoints.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserHCPoints.Name = "lblUserHCPoints";
            this.lblUserHCPoints.Size = new System.Drawing.Size(49, 16);
            this.lblUserHCPoints.TabIndex = 23;
            this.lblUserHCPoints.Text = "0000000";
            // 
            // lblUserAccountType
            // 
            this.lblUserAccountType.AutoSize = true;
            this.lblUserAccountType.Location = new System.Drawing.Point(99, 68);
            this.lblUserAccountType.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserAccountType.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserAccountType.Name = "lblUserAccountType";
            this.lblUserAccountType.Size = new System.Drawing.Size(48, 16);
            this.lblUserAccountType.TabIndex = 22;
            this.lblUserAccountType.Text = "R";
            // 
            // lblUserLastActivity
            // 
            this.lblUserLastActivity.AutoSize = true;
            this.lblUserLastActivity.Location = new System.Drawing.Point(99, 44);
            this.lblUserLastActivity.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserLastActivity.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserLastActivity.Name = "lblUserLastActivity";
            this.lblUserLastActivity.Size = new System.Drawing.Size(103, 16);
            this.lblUserLastActivity.TabIndex = 21;
            this.lblUserLastActivity.Text = "23 Mar 2021, 19:46";
            // 
            // flatLabelA10
            // 
            this.flatLabelA10.AutoSize = true;
            this.flatLabelA10.Location = new System.Drawing.Point(575, 68);
            this.flatLabelA10.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA10.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA10.Name = "flatLabelA10";
            this.flatLabelA10.Size = new System.Drawing.Size(119, 16);
            this.flatLabelA10.TabIndex = 20;
            this.flatLabelA10.Text = "Average Completion:";
            // 
            // flatLabelA9
            // 
            this.flatLabelA9.AutoSize = true;
            this.flatLabelA9.Location = new System.Drawing.Point(306, 68);
            this.flatLabelA9.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA9.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA9.Name = "flatLabelA9";
            this.flatLabelA9.Size = new System.Drawing.Size(68, 16);
            this.flatLabelA9.TabIndex = 19;
            this.flatLabelA9.Text = "Retro Ratio:";
            // 
            // flatLabelA8
            // 
            this.flatLabelA8.AutoSize = true;
            this.flatLabelA8.Location = new System.Drawing.Point(316, 44);
            this.flatLabelA8.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA8.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA8.Name = "flatLabelA8";
            this.flatLabelA8.Size = new System.Drawing.Size(58, 16);
            this.flatLabelA8.TabIndex = 18;
            this.flatLabelA8.Text = "Site Rank:";
            // 
            // flatLabelA7
            // 
            this.flatLabelA7.AutoSize = true;
            this.flatLabelA7.Location = new System.Drawing.Point(279, 20);
            this.flatLabelA7.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA7.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA7.Name = "flatLabelA7";
            this.flatLabelA7.Size = new System.Drawing.Size(95, 16);
            this.flatLabelA7.TabIndex = 17;
            this.flatLabelA7.Text = "Hardcore Points:";
            // 
            // flatLabelA6
            // 
            this.flatLabelA6.AutoSize = true;
            this.flatLabelA6.Location = new System.Drawing.Point(611, 44);
            this.flatLabelA6.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA6.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA6.Name = "flatLabelA6";
            this.flatLabelA6.Size = new System.Drawing.Size(83, 16);
            this.flatLabelA6.TabIndex = 16;
            this.flatLabelA6.Text = "Softcore Rank:";
            // 
            // flatLabelA5
            // 
            this.flatLabelA5.AutoSize = true;
            this.flatLabelA5.Location = new System.Drawing.Point(604, 20);
            this.flatLabelA5.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA5.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA5.Name = "flatLabelA5";
            this.flatLabelA5.Size = new System.Drawing.Size(90, 16);
            this.flatLabelA5.TabIndex = 15;
            this.flatLabelA5.Text = "Softcore Points:";
            // 
            // flatLabelA4
            // 
            this.flatLabelA4.AutoSize = true;
            this.flatLabelA4.Location = new System.Drawing.Point(7, 68);
            this.flatLabelA4.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA4.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA4.Name = "flatLabelA4";
            this.flatLabelA4.Size = new System.Drawing.Size(84, 16);
            this.flatLabelA4.TabIndex = 13;
            this.flatLabelA4.Text = "Account Type:";
            // 
            // flatLabelA3
            // 
            this.flatLabelA3.AutoSize = true;
            this.flatLabelA3.Location = new System.Drawing.Point(17, 44);
            this.flatLabelA3.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA3.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA3.Name = "flatLabelA3";
            this.flatLabelA3.Size = new System.Drawing.Size(74, 16);
            this.flatLabelA3.TabIndex = 12;
            this.flatLabelA3.Text = "Last Activity:";
            // 
            // lblUserMemberSince
            // 
            this.lblUserMemberSince.AutoSize = true;
            this.lblUserMemberSince.Location = new System.Drawing.Point(99, 20);
            this.lblUserMemberSince.Margin = new System.Windows.Forms.Padding(4);
            this.lblUserMemberSince.MinimumSize = new System.Drawing.Size(48, 16);
            this.lblUserMemberSince.Name = "lblUserMemberSince";
            this.lblUserMemberSince.Size = new System.Drawing.Size(103, 16);
            this.lblUserMemberSince.TabIndex = 11;
            this.lblUserMemberSince.Text = "23 Mar 2021, 19:46";
            // 
            // flatLabelA1
            // 
            this.flatLabelA1.AutoSize = true;
            this.flatLabelA1.Location = new System.Drawing.Point(5, 20);
            this.flatLabelA1.Margin = new System.Windows.Forms.Padding(4);
            this.flatLabelA1.MinimumSize = new System.Drawing.Size(48, 16);
            this.flatLabelA1.Name = "flatLabelA1";
            this.flatLabelA1.Size = new System.Drawing.Size(86, 16);
            this.flatLabelA1.TabIndex = 10;
            this.flatLabelA1.Text = "Member Since:";
            // 
            // gpbOverlay
            // 
            this.gpbOverlay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gpbOverlay.Controls.Add(this.pnlUserCheevos);
            this.gpbOverlay.Controls.Add(this.lblCheevoLoopUpdate);
            this.gpbOverlay.Controls.Add(this.chkUserCheevos);
            this.gpbOverlay.Controls.Add(this.btnUserCheevos);
            this.gpbOverlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gpbOverlay.Location = new System.Drawing.Point(354, 230);
            this.gpbOverlay.Name = "gpbOverlay";
            this.gpbOverlay.Size = new System.Drawing.Size(233, 166);
            this.gpbOverlay.TabIndex = 32;
            this.gpbOverlay.TabStop = false;
            this.gpbOverlay.Text = "Overlay";
            // 
            // pnlUserCheevos
            // 
            this.pnlUserCheevos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlUserCheevos.BackColor = System.Drawing.Color.Black;
            this.pnlUserCheevos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUserCheevos.Controls.Add(this.lblCheevos);
            this.pnlUserCheevos.Controls.Add(this.lblUserCheevos);
            this.pnlUserCheevos.Controls.Add(this.picUserCheevos);
            this.pnlUserCheevos.Location = new System.Drawing.Point(6, 19);
            this.pnlUserCheevos.Name = "pnlUserCheevos";
            this.pnlUserCheevos.Size = new System.Drawing.Size(221, 72);
            this.pnlUserCheevos.TabIndex = 27;
            // 
            // lblCheevos
            // 
            this.lblCheevos.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheevos.ForeColor = System.Drawing.Color.White;
            this.lblCheevos.Location = new System.Drawing.Point(60, 45);
            this.lblCheevos.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblCheevos.Name = "lblCheevos";
            this.lblCheevos.Size = new System.Drawing.Size(156, 24);
            this.lblCheevos.TabIndex = 2;
            this.lblCheevos.Text = "Trophies";
            this.lblCheevos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserCheevos
            // 
            this.lblUserCheevos.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserCheevos.ForeColor = System.Drawing.Color.White;
            this.lblUserCheevos.Location = new System.Drawing.Point(60, 6);
            this.lblUserCheevos.Name = "lblUserCheevos";
            this.lblUserCheevos.Size = new System.Drawing.Size(156, 48);
            this.lblUserCheevos.TabIndex = 1;
            this.lblUserCheevos.Text = "0 / 0";
            this.lblUserCheevos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picUserCheevos
            // 
            this.picUserCheevos.Interpolation = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.picUserCheevos.Location = new System.Drawing.Point(11, 11);
            this.picUserCheevos.Margin = new System.Windows.Forms.Padding(6);
            this.picUserCheevos.Name = "picUserCheevos";
            this.picUserCheevos.Size = new System.Drawing.Size(48, 48);
            this.picUserCheevos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUserCheevos.TabIndex = 0;
            this.picUserCheevos.TabStop = false;
            // 
            // lblCheevoLoopUpdate
            // 
            this.lblCheevoLoopUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCheevoLoopUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblCheevoLoopUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCheevoLoopUpdate.Location = new System.Drawing.Point(158, 142);
            this.lblCheevoLoopUpdate.Name = "lblCheevoLoopUpdate";
            this.lblCheevoLoopUpdate.Size = new System.Drawing.Size(13, 13);
            this.lblCheevoLoopUpdate.TabIndex = 31;
            // 
            // chkUserCheevos
            // 
            this.chkUserCheevos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkUserCheevos.AutoSize = true;
            this.chkUserCheevos.Location = new System.Drawing.Point(177, 141);
            this.chkUserCheevos.Name = "chkUserCheevos";
            this.chkUserCheevos.Size = new System.Drawing.Size(50, 17);
            this.chkUserCheevos.TabIndex = 29;
            this.chkUserCheevos.Text = "Loop";
            // 
            // btnUserCheevos
            // 
            this.btnUserCheevos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnUserCheevos.FlatAppearance.BorderSize = 0;
            this.btnUserCheevos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnUserCheevos.Location = new System.Drawing.Point(6, 137);
            this.btnUserCheevos.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnUserCheevos.Name = "btnUserCheevos";
            this.btnUserCheevos.Size = new System.Drawing.Size(146, 24);
            this.btnUserCheevos.TabIndex = 28;
            this.btnUserCheevos.Text = "Get User Trophies";
            this.btnUserCheevos.Click += new System.EventHandler(this.btnUserCheevos_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtUsername.LabelText = "Username";
            this.txtUsername.Location = new System.Drawing.Point(6, 6);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.previousText = "";
            this.txtUsername.Size = new System.Drawing.Size(146, 34);
            this.txtUsername.TabIndex = 30;
            // 
            // tabAbout
            // 
            this.tabAbout.BackColor = System.Drawing.Color.Transparent;
            this.tabAbout.Controls.Add(this.lblSystemReLogin);
            this.tabAbout.Controls.Add(this.btnSystemReLogin);
            this.tabAbout.Controls.Add(this.btnRaProfile);
            this.tabAbout.Controls.Add(this.picFBiDevIcon);
            this.tabAbout.Controls.Add(this.lblAbTitle);
            this.tabAbout.Controls.Add(this.lblAbYear);
            this.tabAbout.Location = new System.Drawing.Point(4, 25);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(908, 545);
            this.tabAbout.TabIndex = 4;
            this.tabAbout.Text = "About";
            // 
            // lblSystemReLogin
            // 
            this.lblSystemReLogin.Location = new System.Drawing.Point(3, 30);
            this.lblSystemReLogin.Name = "lblSystemReLogin";
            this.lblSystemReLogin.Size = new System.Drawing.Size(124, 24);
            this.lblSystemReLogin.TabIndex = 35;
            this.lblSystemReLogin.Text = "not logged in";
            this.lblSystemReLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSystemReLogin
            // 
            this.btnSystemReLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSystemReLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnSystemReLogin.FlatAppearance.BorderSize = 0;
            this.btnSystemReLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnSystemReLogin.Location = new System.Drawing.Point(3, 3);
            this.btnSystemReLogin.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnSystemReLogin.Name = "btnSystemReLogin";
            this.btnSystemReLogin.Size = new System.Drawing.Size(124, 24);
            this.btnSystemReLogin.TabIndex = 34;
            this.btnSystemReLogin.Text = "System ReLogin";
            // 
            // btnRaProfile
            // 
            this.btnRaProfile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnRaProfile.FlatAppearance.BorderSize = 0;
            this.btnRaProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnRaProfile.Location = new System.Drawing.Point(407, 220);
            this.btnRaProfile.MinimumSize = new System.Drawing.Size(24, 24);
            this.btnRaProfile.Name = "btnRaProfile";
            this.btnRaProfile.Size = new System.Drawing.Size(96, 32);
            this.btnRaProfile.TabIndex = 5;
            this.btnRaProfile.Text = "RA Profile";
            this.btnRaProfile.Click += new System.EventHandler(this.btnRaProfile_Click);
            // 
            // picFBiDevIcon
            // 
            this.picFBiDevIcon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picFBiDevIcon.Image = global::RADB.Properties.Resources.fbidev;
            this.picFBiDevIcon.Location = new System.Drawing.Point(407, 118);
            this.picFBiDevIcon.Name = "picFBiDevIcon";
            this.picFBiDevIcon.Size = new System.Drawing.Size(96, 96);
            this.picFBiDevIcon.TabIndex = 4;
            this.picFBiDevIcon.TabStop = false;
            // 
            // lblAbTitle
            // 
            this.lblAbTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAbTitle.AutoSize = true;
            this.lblAbTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(137)))), ((int)(((byte)(207)))));
            this.lblAbTitle.Location = new System.Drawing.Point(369, 64);
            this.lblAbTitle.Name = "lblAbTitle";
            this.lblAbTitle.Size = new System.Drawing.Size(173, 31);
            this.lblAbTitle.TabIndex = 2;
            this.lblAbTitle.Text = "RA Database 1.1";
            // 
            // lblAbYear
            // 
            this.lblAbYear.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAbYear.AutoSize = true;
            this.lblAbYear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(137)))), ((int)(((byte)(207)))));
            this.lblAbYear.Location = new System.Drawing.Point(433, 94);
            this.lblAbYear.Name = "lblAbYear";
            this.lblAbYear.Size = new System.Drawing.Size(48, 24);
            this.lblAbYear.TabIndex = 1;
            this.lblAbYear.Text = "2023";
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(939, 684);
            this.Controls.Add(this.pnlBottomOutput);
            this.Controls.Add(this.tabMain);
            this.DoubleBuffered = true;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RA Database";
            this.mnuGames.ResumeLayout(false);
            this.mnuGamesToHide.ResumeLayout(false);
            this.mnuConsoles.ResumeLayout(false);
            this.mnuGamesToPlay.ResumeLayout(false);
            this.pnlBottomOutput.ResumeLayout(false);
            this.pnlOutput.ResumeLayout(false);
            this.pnlOutput.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabConsoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderConsole)).EndInit();
            this.pnlDownloadConsoles.ResumeLayout(false);
            this.pnlDownloadConsoles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsoles)).EndInit();
            this.tabGames.ResumeLayout(false);
            this.pnlFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderGameList)).EndInit();
            this.pnlGamesConsoleName.ResumeLayout(false);
            this.pnlGamesConsoleName.PerformLayout();
            this.pnlDownloadGameList.ResumeLayout(false);
            this.pnlDownloadGameList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGames)).EndInit();
            this.tabGameInfo.ResumeLayout(false);
            this.tabGameInfo.PerformLayout();
            this.pnlInfoScroll.ResumeLayout(false);
            this.gpbInfo.ResumeLayout(false);
            this.pnlInfoBoxArt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInfoBoxArt)).EndInit();
            this.pnlInfoImages.ResumeLayout(false);
            this.pnlInfoInGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInfoInGame)).EndInit();
            this.pnlInfoTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInfoTitle)).EndInit();
            this.pnlInfoTop.ResumeLayout(false);
            this.pnlInfoTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoIcon)).EndInit();
            this.gpbInfoAchievements.ResumeLayout(false);
            this.gpbInfoAchievements.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAchievements)).EndInit();
            this.pnlDownloadInfo.ResumeLayout(false);
            this.pnlDownloadInfo.PerformLayout();
            this.tabGamesToPlay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGamesToPlay)).EndInit();
            this.tabGamesToHide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGamesToHide)).EndInit();
            this.tabUserInfo.ResumeLayout(false);
            this.tabUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserName)).EndInit();
            this.gpbUserInfo.ResumeLayout(false);
            this.gpbUserInfo.PerformLayout();
            this.gpbOverlay.ResumeLayout(false);
            this.gpbOverlay.PerformLayout();
            this.pnlUserCheevos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picUserCheevos)).EndInit();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFBiDevIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FlatTabControlA tabMain;
        private System.Windows.Forms.TabPage tabConsoles;
        private System.Windows.Forms.TabPage tabGameInfo;
        private System.Windows.Forms.TabPage tabGames;
        public System.Windows.Forms.ToolTip Ttip;
        private System.Windows.Forms.Panel pnlDownloadConsoles;
        private FlatLabelA lblUpdateConsoles;
        private FlatLabelA lblProgressConsoles;
        private FlatProgressBarA pgbConsoles;
        private System.Windows.Forms.Panel pnlDownloadGameList;
        private FlatProgressBarA pgbGameList;
        private FlatLabelA lblUpdateGameList;
        private FlatLabelA lblProgressGameList;
        private System.Windows.Forms.TabPage tabUserInfo;
        private FlatLabelA lblInfoName;
        private FlatPictureBoxA picInfoIcon;
        private FlatGroupBoxA gpbInfo;
        private FlatLabelA lblInfoDeveloper0;
        private System.Windows.Forms.Panel pnlInfoTop;
        private FlatLabelA lblInfoReleased0;
        private FlatLabelA lblInfoGenre0;
        private FlatLabelA lblInfoPublisher0;
        private FlatLabelA lblInfoReleased1;
        private FlatLabelA lblInfoDeveloper1;
        private FlatLabelA lblInfoGenre1;
        private FlatLabelA lblInfoPublisher1;
        private FlatLabelA lblNotFoundGameList;
        private FlatLabelA lblNotFoundConsoles;
        private System.Windows.Forms.Panel pnlDownloadInfo;
        private FlatLabelA lblUpdateInfo;
        private FlatLabelA lblProgressInfo;
        private FlatProgressBarA pgbInfo;
        private FlatPictureBoxA picInfoTitle;
        private FlatPictureBoxA picLoaderConsole;
        private FlatPictureBoxA picLoaderGameList;
        private FlatGroupBoxA gpbInfoAchievements;
        private FlatPanelA pnlInfoScroll;
        private FlatPanelA pnlInfoImages;
        private FlatPictureBoxA picInfoInGame;
        private System.Windows.Forms.Panel pnlUserCheevos;
        private FlatLabelA lblUserCheevos;
        private System.Windows.Forms.CheckBox chkUserCheevos;
        private FlatLabelA lblCheevos;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Panel pnlOutput;
        private FlatDataGridA dgvConsoles;
        private FlatButtonA btnUpdateConsoles;
        private FlatButtonA btnUpdateGameList;
        private FlatDataGridA dgvGames;
        private FlatButtonA btnUpdateInfo;
        private FlatButtonA btnUserCheevos;
        private FlatPanelA pnlGamesConsoleName;
        private FlatLabelB lblConsoleName;
        private FlatLabelB lblConsoleGamesTotal;
        private FlatTextBoxA txtSearchGames;
        private FlatDataGridA dgvAchievements;
        private FlatTextBoxA txtUsername;
        private System.Windows.Forms.TabPage tabAbout;
        private FlatLabelB lblAbTitle;
        private FlatLabelB lblAbYear;
        private System.Windows.Forms.Label lblCheevoLoopUpdate;
        private System.Windows.Forms.TabPage tabGamesToPlay;
        private FlatPictureBoxA picFBiDevIcon;
        private FlatPictureBoxA picUserCheevos;
        private FlatPanelA pnlBottomOutput;
        private FlatCheckBoxA chkWithoutAchievements;
        private FlatCheckBoxA chkUnlicensed;
        private FlatCheckBoxA chkDemo;
        private FlatCheckBoxA chkHack;
        private FlatCheckBoxA chkHomebrew;
        private FlatCheckBoxA chkPrototype;
        private FlatCheckBoxA chkOfficial;
        private FlatButtonA btnRaProfile;
        private System.Windows.Forms.ContextMenuStrip mnuGames;
        private System.Windows.Forms.ToolStripMenuItem mniHideGame;
        private System.Windows.Forms.TabPage tabGamesToHide;
        private FlatDataGridA dgvGamesToHide;
        private System.Windows.Forms.ContextMenuStrip mnuGamesToHide;
        private System.Windows.Forms.ToolStripMenuItem mniRemoveGameToHide;
        private FlatLabelA lblNotFoundGamesToHide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mniMergeGameBadges;
        private System.Windows.Forms.ContextMenuStrip mnuConsoles;
        private System.Windows.Forms.ToolStripMenuItem mniMergeGamesIcon;
        private System.Windows.Forms.ToolStripMenuItem mniMergeGamesIconBadSize;
        private FlatDataGridA dgvGamesToPlay;
        private FlatLabelA lblNotFoundGamesToPlay;
        private System.Windows.Forms.ContextMenuStrip mnuGamesToPlay;
        private System.Windows.Forms.ToolStripMenuItem mniRemoveGameToPlay;
        private System.Windows.Forms.ToolStripMenuItem mniPlayGame;
        private FlatButtonA btnGamePage;
        private FlatLabelA lblInfoAchievements;
        private FlatGroupBoxA gpbOverlay;
        private FlatPanelA pnlFilters;
        private FlatButtonA btnGameFilters;
        private FlatCheckBoxA chkTestKit;
        private FlatCheckBoxA chkSubset;
        private FlatCheckBoxA chkDemoted;
        private FlatPanelA pnlInfoTitle;
        private FlatPanelA pnlInfoInGame;
        private FlatButtonA btnHashes;
        private FlatPanelA pnlInfoBoxArt;
        private FlatPictureBoxA picInfoBoxArt;

        private FlatTextBoxA txtSearchAchiev;
        private System.Windows.Forms.DataGridViewTextBoxColumn cID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumGames;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTotalGames;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpID;
        private System.Windows.Forms.DataGridViewImageColumn gpIconBitmap;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpConsole;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpNumAchievements;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpNumLeaderboards;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpLastUpdated;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghID;
        private System.Windows.Forms.DataGridViewImageColumn ghIconBitmap;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghConsole;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghNumAchievements;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghNumLeaderboards;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghLastUpdated;
        private System.Windows.Forms.DataGridViewTextBoxColumn aOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn aIconBitmap;
        private System.Windows.Forms.DataGridViewTextBoxColumn aDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn aPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn gID;
        private System.Windows.Forms.DataGridViewImageColumn gIconBitmap;
        private System.Windows.Forms.DataGridViewTextBoxColumn gTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn gConsole;
        private System.Windows.Forms.DataGridViewTextBoxColumn gNumAchievements;
        private System.Windows.Forms.DataGridViewTextBoxColumn gPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumLeaderboards;
        private System.Windows.Forms.DataGridViewTextBoxColumn gLastUpdated;
        private FlatLabelA lblSystemReLogin;
        private FlatButtonA btnSystemReLogin;
        private FlatGroupBoxA gpbUserInfo;
        private FlatLabelA lblUserName;
        private FlatButtonA btnGetUserInfo;
        private FlatPictureBoxA picUserName;
        private FlatLabelA flatLabelA10;
        private FlatLabelA flatLabelA9;
        private FlatLabelA flatLabelA8;
        private FlatLabelA flatLabelA7;
        private FlatLabelA flatLabelA6;
        private FlatLabelA flatLabelA5;
        private FlatLabelA flatLabelA4;
        private FlatLabelA flatLabelA3;
        private FlatLabelA lblUserMemberSince;
        private FlatLabelA flatLabelA1;
        private FlatLabelA lblUserMotto;
        private FlatLabelA lblUserLastActivity;
        private FlatLabelA lblUserAccountType;
        private FlatLabelA lblUserCompletion;
        private FlatLabelA lblUserSoftRank;
        private FlatLabelA lblUserSoftPoints;
        private FlatLabelA lblUserRetroRatio;
        private FlatLabelA lblUserRank;
        private FlatLabelA lblUserHCPoints;
        private FlatLabelA lblUserStatus;
    }
}

