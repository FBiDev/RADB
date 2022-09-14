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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Ttip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlBottomOutput = new GNX.PanelBorder();
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.lblOutput = new System.Windows.Forms.Label();
            this.tabMain = new FlatTabControl.FlatTabControl();
            this.tabConsoles = new System.Windows.Forms.TabPage();
            this.lblNotFoundConsoles = new System.Windows.Forms.Label();
            this.picLoaderConsole = new System.Windows.Forms.PictureBox();
            this.pnlDownloadConsoles = new System.Windows.Forms.Panel();
            this.btnUpdateConsoles = new RADB.FlatButtonA();
            this.lblUpdateConsoles = new System.Windows.Forms.Label();
            this.pgbConsoles = new System.Windows.Forms.ProgressBar();
            this.lblProgressConsoles = new System.Windows.Forms.Label();
            this.dgvConsoles = new RADB.FlatDataGridA();
            this.cID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNumGames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTotalGames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabGames = new System.Windows.Forms.TabPage();
            this.lblNotFoundGameList = new System.Windows.Forms.Label();
            this.picLoaderGameList = new System.Windows.Forms.PictureBox();
            this.pnlGamesConsoleName = new GNX.PanelBorder();
            this.chkOfficial = new RADB.CheckBoxBlueA();
            this.chkPrototype = new RADB.CheckBoxBlueA();
            this.chkUnlicensed = new RADB.CheckBoxBlueA();
            this.chkDemo = new RADB.CheckBoxBlueA();
            this.chkHack = new RADB.CheckBoxBlueA();
            this.chkHomebrew = new RADB.CheckBoxBlueA();
            this.chkWithoutAchievements = new RADB.CheckBoxBlueA();
            this.txtSearchGames = new RADB.FlatTextBoxA();
            this.lblConsoleGamesTotal = new System.Windows.Forms.Label();
            this.lblConsoleName = new System.Windows.Forms.Label();
            this.pnlDownloadGameList = new System.Windows.Forms.Panel();
            this.btnUpdateGameList = new RADB.FlatButtonA();
            this.lblUpdateGameList = new System.Windows.Forms.Label();
            this.pgbGameList = new System.Windows.Forms.ProgressBar();
            this.lblProgressGameList = new System.Windows.Forms.Label();
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
            this.pnlInfoScroll = new RADB.PanelNoScrollOnFocus();
            this.picInfoBoxArt = new System.Windows.Forms.PictureBox();
            this.gpbInfo = new System.Windows.Forms.GroupBox();
            this.pnlInfoTop = new System.Windows.Forms.Panel();
            this.lblInfoReleased = new System.Windows.Forms.Label();
            this.lblInfoDeveloper = new System.Windows.Forms.Label();
            this.lblInfoGenre = new System.Windows.Forms.Label();
            this.lblInfoPublisher = new System.Windows.Forms.Label();
            this.picInfoIcon = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flowInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlInfoImages = new System.Windows.Forms.Panel();
            this.picInfoInGame = new System.Windows.Forms.PictureBox();
            this.picInfoTitle = new System.Windows.Forms.PictureBox();
            this.gpbInfoAchievements = new System.Windows.Forms.GroupBox();
            this.dgvAchievements = new RADB.FlatDataGridA();
            this.aOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aIcon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblInfoName = new System.Windows.Forms.Label();
            this.pnlDownloadInfo = new System.Windows.Forms.Panel();
            this.btnUpdateInfo = new RADB.FlatButtonA();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.pgbInfo = new System.Windows.Forms.ProgressBar();
            this.lblProgressInfo = new System.Windows.Forms.Label();
            this.tabUserInfo = new System.Windows.Forms.TabPage();
            this.lblCheevoLoopUpdate = new System.Windows.Forms.Label();
            this.chkUserCheevos = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCheevos = new System.Windows.Forms.Label();
            this.lblUserCheevos = new System.Windows.Forms.Label();
            this.picUserCheevos = new RADB.PictureBoxInterpolated();
            this.txtUsernameCheevos = new RADB.FlatTextBoxA();
            this.btnUserCheevos = new RADB.FlatButtonA();
            this.tabTests = new System.Windows.Forms.TabPage();
            this.lblConsoles = new System.Windows.Forms.Label();
            this.btnDownloadBadges = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblDate2 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.dtpDate2 = new System.Windows.Forms.DateTimePicker();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblOffset = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblDate1 = new System.Windows.Forms.Label();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.dtpDate1 = new System.Windows.Forms.DateTimePicker();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.btnRaProfile = new RADB.FlatButtonA();
            this.picFBiDevIcon = new System.Windows.Forms.PictureBox();
            this.lblAbTitle = new System.Windows.Forms.Label();
            this.lblAbYear = new System.Windows.Forms.Label();
            this.pnlBottomOutput.SuspendLayout();
            this.pnlOutput.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabConsoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderConsole)).BeginInit();
            this.pnlDownloadConsoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsoles)).BeginInit();
            this.tabGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderGameList)).BeginInit();
            this.pnlGamesConsoleName.SuspendLayout();
            this.pnlDownloadGameList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGames)).BeginInit();
            this.tabGameInfo.SuspendLayout();
            this.pnlInfoScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoBoxArt)).BeginInit();
            this.gpbInfo.SuspendLayout();
            this.pnlInfoTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoIcon)).BeginInit();
            this.flowInfo.SuspendLayout();
            this.pnlInfoImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoInGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoTitle)).BeginInit();
            this.gpbInfoAchievements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAchievements)).BeginInit();
            this.pnlDownloadInfo.SuspendLayout();
            this.tabUserInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserCheevos)).BeginInit();
            this.tabTests.SuspendLayout();
            this.tabAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFBiDevIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottomOutput
            // 
            this.pnlBottomOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomOutput.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottomOutput.Controls.Add(this.pnlOutput);
            this.pnlBottomOutput.Location = new System.Drawing.Point(12, 570);
            this.pnlBottomOutput.Name = "pnlBottomOutput";
            this.pnlBottomOutput.Padding = new System.Windows.Forms.Padding(2);
            this.pnlBottomOutput.Size = new System.Drawing.Size(898, 66);
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
            this.pnlOutput.Size = new System.Drawing.Size(888, 62);
            this.pnlOutput.TabIndex = 30;
            // 
            // lblOutput
            // 
            this.lblOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutput.AutoSize = true;
            this.lblOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.Location = new System.Drawing.Point(-2, -2);
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
            this.tabMain.Controls.Add(this.tabUserInfo);
            this.tabMain.Controls.Add(this.tabTests);
            this.tabMain.Controls.Add(this.tabAbout);
            this.tabMain.Location = new System.Drawing.Point(10, 12);
            this.tabMain.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.tabMain.myBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(901, 552);
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
            this.tabConsoles.Size = new System.Drawing.Size(893, 523);
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
            this.lblNotFoundConsoles.Size = new System.Drawing.Size(875, 23);
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
            this.picLoaderConsole.Size = new System.Drawing.Size(881, 52);
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
            this.pnlDownloadConsoles.Size = new System.Drawing.Size(887, 31);
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
            this.btnUpdateConsoles.Name = "btnUpdateConsoles";
            this.btnUpdateConsoles.Size = new System.Drawing.Size(144, 23);
            this.btnUpdateConsoles.TabIndex = 36;
            this.btnUpdateConsoles.Text = "Update Consoles";
            this.btnUpdateConsoles.Click += new System.EventHandler(this.btnUpdateConsoles_Click);
            // 
            // lblUpdateConsoles
            // 
            this.lblUpdateConsoles.Location = new System.Drawing.Point(155, 8);
            this.lblUpdateConsoles.Name = "lblUpdateConsoles";
            this.lblUpdateConsoles.Size = new System.Drawing.Size(110, 13);
            this.lblUpdateConsoles.TabIndex = 32;
            this.lblUpdateConsoles.Text = "00/00/0000 00:00:00";
            this.lblUpdateConsoles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbConsoles
            // 
            this.pgbConsoles.Location = new System.Drawing.Point(271, 3);
            this.pgbConsoles.MarqueeAnimationSpeed = 0;
            this.pgbConsoles.Name = "pgbConsoles";
            this.pgbConsoles.Size = new System.Drawing.Size(144, 21);
            this.pgbConsoles.Step = 1;
            this.pgbConsoles.TabIndex = 30;
            // 
            // lblProgressConsoles
            // 
            this.lblProgressConsoles.AutoSize = true;
            this.lblProgressConsoles.Location = new System.Drawing.Point(421, 8);
            this.lblProgressConsoles.MinimumSize = new System.Drawing.Size(110, 0);
            this.lblProgressConsoles.Name = "lblProgressConsoles";
            this.lblProgressConsoles.Size = new System.Drawing.Size(110, 13);
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConsoles.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvConsoles.Location = new System.Drawing.Point(6, 83);
            this.dgvConsoles.Name = "dgvConsoles";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConsoles.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvConsoles.RowTemplate.Height = 37;
            this.dgvConsoles.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConsoles.Size = new System.Drawing.Size(881, 437);
            this.dgvConsoles.TabIndex = 0;
            // 
            // cID
            // 
            this.cID.DataPropertyName = "ID";
            this.cID.HeaderText = "ID";
            this.cID.Name = "cID";
            this.cID.ReadOnly = true;
            this.cID.Width = 43;
            // 
            // cCompany
            // 
            this.cCompany.DataPropertyName = "Company";
            this.cCompany.HeaderText = "Company";
            this.cCompany.Name = "cCompany";
            this.cCompany.ReadOnly = true;
            this.cCompany.Width = 82;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.cNumGames.DefaultCellStyle = dataGridViewCellStyle3;
            this.cNumGames.HeaderText = "Games";
            this.cNumGames.Name = "cNumGames";
            this.cNumGames.ReadOnly = true;
            this.cNumGames.Width = 67;
            // 
            // cTotalGames
            // 
            this.cTotalGames.DataPropertyName = "TotalGames";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.cTotalGames.DefaultCellStyle = dataGridViewCellStyle4;
            this.cTotalGames.HeaderText = "TotalGames";
            this.cTotalGames.Name = "cTotalGames";
            this.cTotalGames.ReadOnly = true;
            this.cTotalGames.Width = 94;
            // 
            // tabGames
            // 
            this.tabGames.BackColor = System.Drawing.Color.Transparent;
            this.tabGames.Controls.Add(this.lblNotFoundGameList);
            this.tabGames.Controls.Add(this.picLoaderGameList);
            this.tabGames.Controls.Add(this.pnlGamesConsoleName);
            this.tabGames.Controls.Add(this.pnlDownloadGameList);
            this.tabGames.Controls.Add(this.dgvGames);
            this.tabGames.Location = new System.Drawing.Point(4, 25);
            this.tabGames.Name = "tabGames";
            this.tabGames.Padding = new System.Windows.Forms.Padding(3);
            this.tabGames.Size = new System.Drawing.Size(893, 523);
            this.tabGames.TabIndex = 2;
            this.tabGames.Text = "Games";
            // 
            // lblNotFoundGameList
            // 
            this.lblNotFoundGameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotFoundGameList.Location = new System.Drawing.Point(9, 162);
            this.lblNotFoundGameList.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNotFoundGameList.Name = "lblNotFoundGameList";
            this.lblNotFoundGameList.Size = new System.Drawing.Size(875, 23);
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
            this.picLoaderGameList.Size = new System.Drawing.Size(881, 52);
            this.picLoaderGameList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoaderGameList.TabIndex = 5;
            this.picLoaderGameList.TabStop = false;
            this.picLoaderGameList.Visible = false;
            // 
            // pnlGamesConsoleName
            // 
            this.pnlGamesConsoleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGamesConsoleName.Controls.Add(this.chkOfficial);
            this.pnlGamesConsoleName.Controls.Add(this.chkPrototype);
            this.pnlGamesConsoleName.Controls.Add(this.chkUnlicensed);
            this.pnlGamesConsoleName.Controls.Add(this.chkDemo);
            this.pnlGamesConsoleName.Controls.Add(this.chkHack);
            this.pnlGamesConsoleName.Controls.Add(this.chkHomebrew);
            this.pnlGamesConsoleName.Controls.Add(this.chkWithoutAchievements);
            this.pnlGamesConsoleName.Controls.Add(this.txtSearchGames);
            this.pnlGamesConsoleName.Controls.Add(this.lblConsoleGamesTotal);
            this.pnlGamesConsoleName.Controls.Add(this.lblConsoleName);
            this.pnlGamesConsoleName.Location = new System.Drawing.Point(6, 32);
            this.pnlGamesConsoleName.Name = "pnlGamesConsoleName";
            this.pnlGamesConsoleName.Padding = new System.Windows.Forms.Padding(2);
            this.pnlGamesConsoleName.Size = new System.Drawing.Size(881, 44);
            this.pnlGamesConsoleName.TabIndex = 6;
            // 
            // chkOfficial
            // 
            this.chkOfficial._Legend = "Official";
            this.chkOfficial.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkOfficial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkOfficial.Checked = true;
            this.chkOfficial.Location = new System.Drawing.Point(247, 5);
            this.chkOfficial.Name = "chkOfficial";
            this.chkOfficial.Size = new System.Drawing.Size(64, 34);
            this.chkOfficial.TabIndex = 13;
            // 
            // chkPrototype
            // 
            this.chkPrototype._Legend = "Prototype";
            this.chkPrototype.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkPrototype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkPrototype.Checked = true;
            this.chkPrototype.Location = new System.Drawing.Point(317, 5);
            this.chkPrototype.Name = "chkPrototype";
            this.chkPrototype.Size = new System.Drawing.Size(64, 34);
            this.chkPrototype.TabIndex = 12;
            // 
            // chkUnlicensed
            // 
            this.chkUnlicensed._Legend = "Unlicensed";
            this.chkUnlicensed.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkUnlicensed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkUnlicensed.Checked = true;
            this.chkUnlicensed.Location = new System.Drawing.Point(387, 5);
            this.chkUnlicensed.Name = "chkUnlicensed";
            this.chkUnlicensed.Size = new System.Drawing.Size(64, 34);
            this.chkUnlicensed.TabIndex = 11;
            // 
            // chkDemo
            // 
            this.chkDemo._Legend = "Demo";
            this.chkDemo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkDemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkDemo.Checked = true;
            this.chkDemo.Location = new System.Drawing.Point(457, 5);
            this.chkDemo.Name = "chkDemo";
            this.chkDemo.Size = new System.Drawing.Size(64, 34);
            this.chkDemo.TabIndex = 10;
            // 
            // chkHack
            // 
            this.chkHack._Legend = "Hack";
            this.chkHack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkHack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkHack.Checked = true;
            this.chkHack.Location = new System.Drawing.Point(527, 5);
            this.chkHack.Name = "chkHack";
            this.chkHack.Size = new System.Drawing.Size(64, 34);
            this.chkHack.TabIndex = 9;
            // 
            // chkHomebrew
            // 
            this.chkHomebrew._Legend = "Homebrew";
            this.chkHomebrew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkHomebrew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkHomebrew.Checked = true;
            this.chkHomebrew.Location = new System.Drawing.Point(597, 5);
            this.chkHomebrew.Name = "chkHomebrew";
            this.chkHomebrew.Size = new System.Drawing.Size(64, 34);
            this.chkHomebrew.TabIndex = 8;
            // 
            // chkWithoutAchievements
            // 
            this.chkWithoutAchievements._Legend = "No Achievement";
            this.chkWithoutAchievements.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chkWithoutAchievements.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.chkWithoutAchievements.Checked = true;
            this.chkWithoutAchievements.Location = new System.Drawing.Point(667, 5);
            this.chkWithoutAchievements.Name = "chkWithoutAchievements";
            this.chkWithoutAchievements.Size = new System.Drawing.Size(91, 34);
            this.chkWithoutAchievements.TabIndex = 3;
            // 
            // txtSearchGames
            // 
            this.txtSearchGames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchGames.LabelText = "Search Game";
            this.txtSearchGames.Location = new System.Drawing.Point(764, 5);
            this.txtSearchGames.Name = "txtSearchGames";
            this.txtSearchGames.Size = new System.Drawing.Size(112, 34);
            this.txtSearchGames.TabIndex = 2;
            // 
            // lblConsoleGamesTotal
            // 
            this.lblConsoleGamesTotal.AutoSize = true;
            this.lblConsoleGamesTotal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsoleGamesTotal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblConsoleGamesTotal.Location = new System.Drawing.Point(7, 24);
            this.lblConsoleGamesTotal.Name = "lblConsoleGamesTotal";
            this.lblConsoleGamesTotal.Size = new System.Drawing.Size(62, 13);
            this.lblConsoleGamesTotal.TabIndex = 1;
            this.lblConsoleGamesTotal.Text = "000 Games";
            // 
            // lblConsoleName
            // 
            this.lblConsoleName.AutoSize = true;
            this.lblConsoleName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsoleName.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblConsoleName.Location = new System.Drawing.Point(3, 3);
            this.lblConsoleName.Name = "lblConsoleName";
            this.lblConsoleName.Size = new System.Drawing.Size(66, 21);
            this.lblConsoleName.TabIndex = 0;
            this.lblConsoleName.Text = "Console";
            this.lblConsoleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.pnlDownloadGameList.Size = new System.Drawing.Size(887, 31);
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
            this.btnUpdateGameList.Name = "btnUpdateGameList";
            this.btnUpdateGameList.Size = new System.Drawing.Size(144, 23);
            this.btnUpdateGameList.TabIndex = 35;
            this.btnUpdateGameList.Text = "Update Games";
            this.btnUpdateGameList.Click += new System.EventHandler(this.btnUpdateGameList_Click);
            // 
            // lblUpdateGameList
            // 
            this.lblUpdateGameList.Location = new System.Drawing.Point(155, 8);
            this.lblUpdateGameList.Name = "lblUpdateGameList";
            this.lblUpdateGameList.Size = new System.Drawing.Size(110, 13);
            this.lblUpdateGameList.TabIndex = 34;
            this.lblUpdateGameList.Text = "00/00/0000 00:00:00";
            this.lblUpdateGameList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbGameList
            // 
            this.pgbGameList.Location = new System.Drawing.Point(271, 3);
            this.pgbGameList.MarqueeAnimationSpeed = 0;
            this.pgbGameList.Name = "pgbGameList";
            this.pgbGameList.Size = new System.Drawing.Size(144, 21);
            this.pgbGameList.Step = 1;
            this.pgbGameList.TabIndex = 31;
            // 
            // lblProgressGameList
            // 
            this.lblProgressGameList.AutoSize = true;
            this.lblProgressGameList.Location = new System.Drawing.Point(421, 8);
            this.lblProgressGameList.Name = "lblProgressGameList";
            this.lblProgressGameList.Size = new System.Drawing.Size(58, 13);
            this.lblProgressGameList.TabIndex = 33;
            this.lblProgressGameList.Text = "lblProgress";
            // 
            // dgvGames
            // 
            this.dgvGames.AllowUserToResizeColumns = false;
            this.dgvGames.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.dgvGames.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvGames.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvGames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gID,
            this.gIconBitmap,
            this.gTitle,
            this.gConsole,
            this.gNumAchievements,
            this.gPoints,
            this.cNumLeaderboards,
            this.gLastUpdated});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGames.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvGames.Location = new System.Drawing.Point(6, 83);
            this.dgvGames.Name = "dgvGames";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGames.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvGames.RowTemplate.Height = 37;
            this.dgvGames.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGames.Size = new System.Drawing.Size(881, 437);
            this.dgvGames.TabIndex = 1;
            // 
            // gID
            // 
            this.gID.DataPropertyName = "ID";
            this.gID.HeaderText = "ID";
            this.gID.Name = "gID";
            this.gID.ReadOnly = true;
            this.gID.Width = 43;
            // 
            // gIconBitmap
            // 
            this.gIconBitmap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.gIconBitmap.DataPropertyName = "ImageIconBitmap";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle9.NullValue")));
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(2);
            this.gIconBitmap.DefaultCellStyle = dataGridViewCellStyle9;
            this.gIconBitmap.HeaderText = "Icon";
            this.gIconBitmap.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
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
            this.gNumAchievements.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.gNumAchievements.DataPropertyName = "NumAchievements";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            this.gNumAchievements.DefaultCellStyle = dataGridViewCellStyle10;
            this.gNumAchievements.HeaderText = "Achievements";
            this.gNumAchievements.Name = "gNumAchievements";
            this.gNumAchievements.ReadOnly = true;
            this.gNumAchievements.Width = 86;
            // 
            // gPoints
            // 
            this.gPoints.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.gPoints.DataPropertyName = "Points";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Format = "N0";
            dataGridViewCellStyle11.NullValue = null;
            this.gPoints.DefaultCellStyle = dataGridViewCellStyle11;
            this.gPoints.HeaderText = "Points";
            this.gPoints.Name = "gPoints";
            this.gPoints.ReadOnly = true;
            this.gPoints.Width = 45;
            // 
            // cNumLeaderboards
            // 
            this.cNumLeaderboards.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNumLeaderboards.DataPropertyName = "NumLeaderboards";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Format = "N0";
            dataGridViewCellStyle12.NullValue = null;
            this.cNumLeaderboards.DefaultCellStyle = dataGridViewCellStyle12;
            this.cNumLeaderboards.HeaderText = "Leaderboards";
            this.cNumLeaderboards.Name = "cNumLeaderboards";
            this.cNumLeaderboards.ReadOnly = true;
            this.cNumLeaderboards.Width = 83;
            // 
            // gLastUpdated
            // 
            this.gLastUpdated.DataPropertyName = "DateModified";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.Format = "dd MMM, yyyy";
            dataGridViewCellStyle13.NullValue = null;
            this.gLastUpdated.DefaultCellStyle = dataGridViewCellStyle13;
            this.gLastUpdated.HeaderText = "Last Updated";
            this.gLastUpdated.Name = "gLastUpdated";
            this.gLastUpdated.ReadOnly = true;
            this.gLastUpdated.Width = 95;
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
            this.tabGameInfo.Size = new System.Drawing.Size(893, 523);
            this.tabGameInfo.TabIndex = 1;
            this.tabGameInfo.Text = "GameInfo";
            // 
            // pnlInfoScroll
            // 
            this.pnlInfoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfoScroll.AutoScroll = true;
            this.pnlInfoScroll.Controls.Add(this.picInfoBoxArt);
            this.pnlInfoScroll.Controls.Add(this.gpbInfo);
            this.pnlInfoScroll.Controls.Add(this.flowInfo);
            this.pnlInfoScroll.Location = new System.Drawing.Point(0, 62);
            this.pnlInfoScroll.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInfoScroll.Name = "pnlInfoScroll";
            this.pnlInfoScroll.Size = new System.Drawing.Size(887, 458);
            this.pnlInfoScroll.TabIndex = 16;
            // 
            // picInfoBoxArt
            // 
            this.picInfoBoxArt.BackColor = System.Drawing.Color.PaleTurquoise;
            this.picInfoBoxArt.Location = new System.Drawing.Point(569, 9);
            this.picInfoBoxArt.MaximumSize = new System.Drawing.Size(300, 283);
            this.picInfoBoxArt.Name = "picInfoBoxArt";
            this.picInfoBoxArt.Size = new System.Drawing.Size(300, 283);
            this.picInfoBoxArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfoBoxArt.TabIndex = 20;
            this.picInfoBoxArt.TabStop = false;
            // 
            // gpbInfo
            // 
            this.gpbInfo.Controls.Add(this.pnlInfoTop);
            this.gpbInfo.Location = new System.Drawing.Point(6, 3);
            this.gpbInfo.Name = "gpbInfo";
            this.gpbInfo.Size = new System.Drawing.Size(557, 124);
            this.gpbInfo.TabIndex = 6;
            this.gpbInfo.TabStop = false;
            this.gpbInfo.Text = "Info";
            // 
            // pnlInfoTop
            // 
            this.pnlInfoTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInfoTop.Controls.Add(this.lblInfoReleased);
            this.pnlInfoTop.Controls.Add(this.lblInfoDeveloper);
            this.pnlInfoTop.Controls.Add(this.lblInfoGenre);
            this.pnlInfoTop.Controls.Add(this.lblInfoPublisher);
            this.pnlInfoTop.Controls.Add(this.picInfoIcon);
            this.pnlInfoTop.Controls.Add(this.label5);
            this.pnlInfoTop.Controls.Add(this.label1);
            this.pnlInfoTop.Controls.Add(this.label4);
            this.pnlInfoTop.Controls.Add(this.label3);
            this.pnlInfoTop.Location = new System.Drawing.Point(6, 19);
            this.pnlInfoTop.Name = "pnlInfoTop";
            this.pnlInfoTop.Size = new System.Drawing.Size(545, 96);
            this.pnlInfoTop.TabIndex = 9;
            // 
            // lblInfoReleased
            // 
            this.lblInfoReleased.AutoSize = true;
            this.lblInfoReleased.Location = new System.Drawing.Point(190, 77);
            this.lblInfoReleased.Margin = new System.Windows.Forms.Padding(6);
            this.lblInfoReleased.Name = "lblInfoReleased";
            this.lblInfoReleased.Size = new System.Drawing.Size(52, 13);
            this.lblInfoReleased.TabIndex = 12;
            this.lblInfoReleased.Text = "Released";
            // 
            // lblInfoDeveloper
            // 
            this.lblInfoDeveloper.AutoSize = true;
            this.lblInfoDeveloper.Location = new System.Drawing.Point(190, 2);
            this.lblInfoDeveloper.Margin = new System.Windows.Forms.Padding(6);
            this.lblInfoDeveloper.Name = "lblInfoDeveloper";
            this.lblInfoDeveloper.Size = new System.Drawing.Size(56, 13);
            this.lblInfoDeveloper.TabIndex = 9;
            this.lblInfoDeveloper.Text = "Developer";
            // 
            // lblInfoGenre
            // 
            this.lblInfoGenre.AutoSize = true;
            this.lblInfoGenre.Location = new System.Drawing.Point(190, 52);
            this.lblInfoGenre.Margin = new System.Windows.Forms.Padding(6);
            this.lblInfoGenre.Name = "lblInfoGenre";
            this.lblInfoGenre.Size = new System.Drawing.Size(36, 13);
            this.lblInfoGenre.TabIndex = 11;
            this.lblInfoGenre.Text = "Genre";
            // 
            // lblInfoPublisher
            // 
            this.lblInfoPublisher.AutoSize = true;
            this.lblInfoPublisher.Location = new System.Drawing.Point(190, 27);
            this.lblInfoPublisher.Margin = new System.Windows.Forms.Padding(6);
            this.lblInfoPublisher.Name = "lblInfoPublisher";
            this.lblInfoPublisher.Size = new System.Drawing.Size(50, 13);
            this.lblInfoPublisher.TabIndex = 10;
            this.lblInfoPublisher.Text = "Publisher";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(105, 78);
            this.label5.Margin = new System.Windows.Forms.Padding(6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Released:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Developer:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 53);
            this.label4.Margin = new System.Windows.Forms.Padding(6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Genre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Publisher:";
            // 
            // flowInfo
            // 
            this.flowInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowInfo.AutoSize = true;
            this.flowInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowInfo.Controls.Add(this.pnlInfoImages);
            this.flowInfo.Controls.Add(this.gpbInfoAchievements);
            this.flowInfo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowInfo.Location = new System.Drawing.Point(3, 133);
            this.flowInfo.Margin = new System.Windows.Forms.Padding(0);
            this.flowInfo.MinimumSize = new System.Drawing.Size(563, 323);
            this.flowInfo.Name = "flowInfo";
            this.flowInfo.Size = new System.Drawing.Size(563, 329);
            this.flowInfo.TabIndex = 19;
            // 
            // pnlInfoImages
            // 
            this.pnlInfoImages.AutoSize = true;
            this.pnlInfoImages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlInfoImages.Controls.Add(this.picInfoInGame);
            this.pnlInfoImages.Controls.Add(this.picInfoTitle);
            this.pnlInfoImages.Location = new System.Drawing.Point(3, 3);
            this.pnlInfoImages.MinimumSize = new System.Drawing.Size(554, 156);
            this.pnlInfoImages.Name = "pnlInfoImages";
            this.pnlInfoImages.Size = new System.Drawing.Size(554, 162);
            this.pnlInfoImages.TabIndex = 16;
            // 
            // picInfoInGame
            // 
            this.picInfoInGame.BackColor = System.Drawing.Color.PaleTurquoise;
            this.picInfoInGame.Location = new System.Drawing.Point(285, 6);
            this.picInfoInGame.MaximumSize = new System.Drawing.Size(200, 240);
            this.picInfoInGame.Name = "picInfoInGame";
            this.picInfoInGame.Size = new System.Drawing.Size(200, 150);
            this.picInfoInGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfoInGame.TabIndex = 14;
            this.picInfoInGame.TabStop = false;
            // 
            // picInfoTitle
            // 
            this.picInfoTitle.BackColor = System.Drawing.Color.LightGray;
            this.picInfoTitle.Location = new System.Drawing.Point(76, 6);
            this.picInfoTitle.Margin = new System.Windows.Forms.Padding(6);
            this.picInfoTitle.MaximumSize = new System.Drawing.Size(200, 240);
            this.picInfoTitle.Name = "picInfoTitle";
            this.picInfoTitle.Size = new System.Drawing.Size(200, 150);
            this.picInfoTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfoTitle.TabIndex = 13;
            this.picInfoTitle.TabStop = false;
            // 
            // gpbInfoAchievements
            // 
            this.gpbInfoAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbInfoAchievements.Controls.Add(this.dgvAchievements);
            this.gpbInfoAchievements.Location = new System.Drawing.Point(3, 171);
            this.gpbInfoAchievements.MinimumSize = new System.Drawing.Size(557, 54);
            this.gpbInfoAchievements.Name = "gpbInfoAchievements";
            this.gpbInfoAchievements.Size = new System.Drawing.Size(557, 155);
            this.gpbInfoAchievements.TabIndex = 5;
            this.gpbInfoAchievements.TabStop = false;
            this.gpbInfoAchievements.Text = "Achievements";
            // 
            // dgvAchievements
            // 
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            this.dgvAchievements.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvAchievements.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAchievements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvAchievements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aOrder,
            this.aIcon,
            this.aDescription,
            this.aPoints});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAchievements.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvAchievements.Location = new System.Drawing.Point(6, 19);
            this.dgvAchievements.Name = "dgvAchievements";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAchievements.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.dgvAchievements.RowTemplate.Height = 72;
            this.dgvAchievements.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAchievements.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvAchievements.Size = new System.Drawing.Size(545, 130);
            this.dgvAchievements.TabIndex = 0;
            // 
            // aOrder
            // 
            this.aOrder.DataPropertyName = "DisplayOrder";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.aOrder.DefaultCellStyle = dataGridViewCellStyle18;
            this.aOrder.HeaderText = "Order";
            this.aOrder.Name = "aOrder";
            this.aOrder.ReadOnly = true;
            this.aOrder.Width = 62;
            // 
            // aIcon
            // 
            this.aIcon.DataPropertyName = "DisplayOrder";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.aIcon.DefaultCellStyle = dataGridViewCellStyle19;
            this.aIcon.HeaderText = "Icon";
            this.aIcon.Name = "aIcon";
            this.aIcon.ReadOnly = true;
            this.aIcon.Visible = false;
            this.aIcon.Width = 72;
            // 
            // aDescription
            // 
            this.aDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.aDescription.DataPropertyName = "DescriptionComplete";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.aDescription.DefaultCellStyle = dataGridViewCellStyle20;
            this.aDescription.HeaderText = "Description";
            this.aDescription.Name = "aDescription";
            this.aDescription.ReadOnly = true;
            // 
            // aPoints
            // 
            this.aPoints.DataPropertyName = "Points";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.aPoints.DefaultCellStyle = dataGridViewCellStyle21;
            this.aPoints.HeaderText = "Points";
            this.aPoints.Name = "aPoints";
            this.aPoints.ReadOnly = true;
            this.aPoints.Width = 64;
            // 
            // lblInfoName
            // 
            this.lblInfoName.AutoSize = true;
            this.lblInfoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoName.Location = new System.Drawing.Point(6, 34);
            this.lblInfoName.Name = "lblInfoName";
            this.lblInfoName.Size = new System.Drawing.Size(61, 24);
            this.lblInfoName.TabIndex = 3;
            this.lblInfoName.Text = "Name";
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
            this.pnlDownloadInfo.Size = new System.Drawing.Size(887, 31);
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
            this.btnUpdateInfo.Name = "btnUpdateInfo";
            this.btnUpdateInfo.Size = new System.Drawing.Size(144, 23);
            this.btnUpdateInfo.TabIndex = 35;
            this.btnUpdateInfo.Text = "Update Info";
            this.btnUpdateInfo.Click += new System.EventHandler(this.btnUpdateInfo_Click);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.Location = new System.Drawing.Point(155, 8);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(110, 13);
            this.lblUpdateInfo.TabIndex = 34;
            this.lblUpdateInfo.Text = "00/00/0000 00:00:00";
            this.lblUpdateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbInfo
            // 
            this.pgbInfo.Location = new System.Drawing.Point(271, 3);
            this.pgbInfo.MarqueeAnimationSpeed = 0;
            this.pgbInfo.Name = "pgbInfo";
            this.pgbInfo.Size = new System.Drawing.Size(144, 21);
            this.pgbInfo.Step = 1;
            this.pgbInfo.TabIndex = 31;
            // 
            // lblProgressInfo
            // 
            this.lblProgressInfo.AutoSize = true;
            this.lblProgressInfo.Location = new System.Drawing.Point(421, 8);
            this.lblProgressInfo.Name = "lblProgressInfo";
            this.lblProgressInfo.Size = new System.Drawing.Size(58, 13);
            this.lblProgressInfo.TabIndex = 33;
            this.lblProgressInfo.Text = "lblProgress";
            // 
            // tabUserInfo
            // 
            this.tabUserInfo.BackColor = System.Drawing.Color.Transparent;
            this.tabUserInfo.Controls.Add(this.lblCheevoLoopUpdate);
            this.tabUserInfo.Controls.Add(this.chkUserCheevos);
            this.tabUserInfo.Controls.Add(this.panel1);
            this.tabUserInfo.Controls.Add(this.txtUsernameCheevos);
            this.tabUserInfo.Controls.Add(this.btnUserCheevos);
            this.tabUserInfo.Location = new System.Drawing.Point(4, 25);
            this.tabUserInfo.Name = "tabUserInfo";
            this.tabUserInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserInfo.Size = new System.Drawing.Size(893, 523);
            this.tabUserInfo.TabIndex = 3;
            this.tabUserInfo.Text = "UserInfo";
            // 
            // lblCheevoLoopUpdate
            // 
            this.lblCheevoLoopUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblCheevoLoopUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCheevoLoopUpdate.Location = new System.Drawing.Point(552, 228);
            this.lblCheevoLoopUpdate.Name = "lblCheevoLoopUpdate";
            this.lblCheevoLoopUpdate.Size = new System.Drawing.Size(13, 13);
            this.lblCheevoLoopUpdate.TabIndex = 31;
            // 
            // chkUserCheevos
            // 
            this.chkUserCheevos.AutoSize = true;
            this.chkUserCheevos.Location = new System.Drawing.Point(571, 227);
            this.chkUserCheevos.Name = "chkUserCheevos";
            this.chkUserCheevos.Size = new System.Drawing.Size(50, 17);
            this.chkUserCheevos.TabIndex = 29;
            this.chkUserCheevos.Text = "Loop";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblCheevos);
            this.panel1.Controls.Add(this.lblUserCheevos);
            this.panel1.Controls.Add(this.picUserCheevos);
            this.panel1.Location = new System.Drawing.Point(400, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(221, 72);
            this.panel1.TabIndex = 27;
            // 
            // lblCheevos
            // 
            this.lblCheevos.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheevos.ForeColor = System.Drawing.Color.White;
            this.lblCheevos.Location = new System.Drawing.Point(60, 45);
            this.lblCheevos.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblCheevos.Name = "lblCheevos";
            this.lblCheevos.Size = new System.Drawing.Size(156, 20);
            this.lblCheevos.TabIndex = 2;
            this.lblCheevos.Text = "Achievements";
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
            this.lblUserCheevos.Text = "11/24";
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
            // txtUsernameCheevos
            // 
            this.txtUsernameCheevos.LabelText = "Username";
            this.txtUsernameCheevos.Location = new System.Drawing.Point(400, 183);
            this.txtUsernameCheevos.Name = "txtUsernameCheevos";
            this.txtUsernameCheevos.Size = new System.Drawing.Size(222, 34);
            this.txtUsernameCheevos.TabIndex = 30;
            // 
            // btnUserCheevos
            // 
            this.btnUserCheevos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUserCheevos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnUserCheevos.FlatAppearance.BorderSize = 0;
            this.btnUserCheevos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnUserCheevos.Location = new System.Drawing.Point(400, 223);
            this.btnUserCheevos.Name = "btnUserCheevos";
            this.btnUserCheevos.Size = new System.Drawing.Size(146, 23);
            this.btnUserCheevos.TabIndex = 28;
            this.btnUserCheevos.Text = "Get User Achievements";
            this.btnUserCheevos.Click += new System.EventHandler(this.btnUserCheevos_Click);
            // 
            // tabTests
            // 
            this.tabTests.BackColor = System.Drawing.Color.Transparent;
            this.tabTests.Controls.Add(this.lblConsoles);
            this.tabTests.Controls.Add(this.btnDownloadBadges);
            this.tabTests.Controls.Add(this.label2);
            this.tabTests.Controls.Add(this.txtURL);
            this.tabTests.Controls.Add(this.lblUser);
            this.tabTests.Controls.Add(this.txtUser);
            this.tabTests.Controls.Add(this.lblDate2);
            this.tabTests.Controls.Add(this.lblID);
            this.tabTests.Controls.Add(this.dtpDate2);
            this.tabTests.Controls.Add(this.txtID);
            this.tabTests.Controls.Add(this.lblCount);
            this.tabTests.Controls.Add(this.lblOffset);
            this.tabTests.Controls.Add(this.txtCount);
            this.tabTests.Controls.Add(this.lblDate1);
            this.tabTests.Controls.Add(this.txtOffset);
            this.tabTests.Controls.Add(this.dtpDate1);
            this.tabTests.Location = new System.Drawing.Point(4, 25);
            this.tabTests.Name = "tabTests";
            this.tabTests.Size = new System.Drawing.Size(893, 523);
            this.tabTests.TabIndex = 5;
            this.tabTests.Text = "Tests";
            // 
            // lblConsoles
            // 
            this.lblConsoles.AutoSize = true;
            this.lblConsoles.Location = new System.Drawing.Point(229, 29);
            this.lblConsoles.Name = "lblConsoles";
            this.lblConsoles.Size = new System.Drawing.Size(50, 13);
            this.lblConsoles.TabIndex = 42;
            this.lblConsoles.Text = "Consoles";
            // 
            // btnDownloadBadges
            // 
            this.btnDownloadBadges.Location = new System.Drawing.Point(33, 45);
            this.btnDownloadBadges.Name = "btnDownloadBadges";
            this.btnDownloadBadges.Size = new System.Drawing.Size(190, 23);
            this.btnDownloadBadges.TabIndex = 41;
            this.btnDownloadBadges.Text = "Download Badges";
            this.btnDownloadBadges.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "URL";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(3, 184);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(190, 20);
            this.txtURL.TabIndex = 29;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(128, 3);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(29, 13);
            this.lblUser.TabIndex = 32;
            this.lblUser.Text = "User";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(131, 19);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(92, 20);
            this.txtUser.TabIndex = 31;
            // 
            // lblDate2
            // 
            this.lblDate2.AutoSize = true;
            this.lblDate2.Location = new System.Drawing.Point(523, 3);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(52, 13);
            this.lblDate2.TabIndex = 40;
            this.lblDate2.Text = "Date End";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(30, 3);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 28;
            this.lblID.Text = "ID";
            // 
            // dtpDate2
            // 
            this.dtpDate2.CustomFormat = "dd/MM/yyyy";
            this.dtpDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate2.Location = new System.Drawing.Point(526, 19);
            this.dtpDate2.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dtpDate2.Name = "dtpDate2";
            this.dtpDate2.Size = new System.Drawing.Size(92, 20);
            this.dtpDate2.TabIndex = 39;
            this.dtpDate2.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(33, 19);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(92, 20);
            this.txtID.TabIndex = 27;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(229, 3);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(35, 13);
            this.lblCount.TabIndex = 34;
            this.lblCount.Text = "Count";
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(327, 3);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(35, 13);
            this.lblOffset.TabIndex = 36;
            this.lblOffset.Text = "Offset";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(232, 19);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(92, 20);
            this.txtCount.TabIndex = 33;
            // 
            // lblDate1
            // 
            this.lblDate1.AutoSize = true;
            this.lblDate1.Location = new System.Drawing.Point(425, 3);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(55, 13);
            this.lblDate1.TabIndex = 38;
            this.lblDate1.Text = "Date Start";
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(330, 19);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(92, 20);
            this.txtOffset.TabIndex = 35;
            // 
            // dtpDate1
            // 
            this.dtpDate1.CustomFormat = "dd/MM/yyyy";
            this.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate1.Location = new System.Drawing.Point(428, 19);
            this.dtpDate1.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dtpDate1.Name = "dtpDate1";
            this.dtpDate1.Size = new System.Drawing.Size(92, 20);
            this.dtpDate1.TabIndex = 37;
            this.dtpDate1.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // tabAbout
            // 
            this.tabAbout.BackColor = System.Drawing.Color.Transparent;
            this.tabAbout.Controls.Add(this.btnRaProfile);
            this.tabAbout.Controls.Add(this.picFBiDevIcon);
            this.tabAbout.Controls.Add(this.lblAbTitle);
            this.tabAbout.Controls.Add(this.lblAbYear);
            this.tabAbout.Location = new System.Drawing.Point(4, 25);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(893, 523);
            this.tabAbout.TabIndex = 4;
            this.tabAbout.Text = "About";
            // 
            // btnRaProfile
            // 
            this.btnRaProfile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.btnRaProfile.FlatAppearance.BorderSize = 0;
            this.btnRaProfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.btnRaProfile.Location = new System.Drawing.Point(395, 220);
            this.btnRaProfile.Name = "btnRaProfile";
            this.btnRaProfile.Size = new System.Drawing.Size(96, 32);
            this.btnRaProfile.TabIndex = 5;
            this.btnRaProfile.Text = "RA Profile";
            this.btnRaProfile.Click += new System.EventHandler(this.btnRaProfile_Click);
            // 
            // picFBiDevIcon
            // 
            this.picFBiDevIcon.Image = global::RADB.Properties.Resources.fbidev;
            this.picFBiDevIcon.Location = new System.Drawing.Point(395, 118);
            this.picFBiDevIcon.Name = "picFBiDevIcon";
            this.picFBiDevIcon.Size = new System.Drawing.Size(96, 96);
            this.picFBiDevIcon.TabIndex = 4;
            this.picFBiDevIcon.TabStop = false;
            // 
            // lblAbTitle
            // 
            this.lblAbTitle.AutoSize = true;
            this.lblAbTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(137)))), ((int)(((byte)(207)))));
            this.lblAbTitle.Location = new System.Drawing.Point(356, 64);
            this.lblAbTitle.Name = "lblAbTitle";
            this.lblAbTitle.Size = new System.Drawing.Size(173, 30);
            this.lblAbTitle.TabIndex = 2;
            this.lblAbTitle.Text = "RA Database 1.0";
            // 
            // lblAbYear
            // 
            this.lblAbYear.AutoSize = true;
            this.lblAbYear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(137)))), ((int)(((byte)(207)))));
            this.lblAbYear.Location = new System.Drawing.Point(422, 94);
            this.lblAbYear.Name = "lblAbYear";
            this.lblAbYear.Size = new System.Drawing.Size(46, 21);
            this.lblAbYear.TabIndex = 1;
            this.lblAbYear.Text = "2022";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 662);
            this.Controls.Add(this.pnlBottomOutput);
            this.Controls.Add(this.tabMain);
            this.DoubleBuffered = true;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RA Database";
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
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderGameList)).EndInit();
            this.pnlGamesConsoleName.ResumeLayout(false);
            this.pnlGamesConsoleName.PerformLayout();
            this.pnlDownloadGameList.ResumeLayout(false);
            this.pnlDownloadGameList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGames)).EndInit();
            this.tabGameInfo.ResumeLayout(false);
            this.tabGameInfo.PerformLayout();
            this.pnlInfoScroll.ResumeLayout(false);
            this.pnlInfoScroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoBoxArt)).EndInit();
            this.gpbInfo.ResumeLayout(false);
            this.pnlInfoTop.ResumeLayout(false);
            this.pnlInfoTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoIcon)).EndInit();
            this.flowInfo.ResumeLayout(false);
            this.flowInfo.PerformLayout();
            this.pnlInfoImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInfoInGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoTitle)).EndInit();
            this.gpbInfoAchievements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAchievements)).EndInit();
            this.pnlDownloadInfo.ResumeLayout(false);
            this.pnlDownloadInfo.PerformLayout();
            this.tabUserInfo.ResumeLayout(false);
            this.tabUserInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picUserCheevos)).EndInit();
            this.tabTests.ResumeLayout(false);
            this.tabTests.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFBiDevIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FlatTabControl.FlatTabControl tabMain;
        private System.Windows.Forms.TabPage tabConsoles;
        private System.Windows.Forms.TabPage tabGameInfo;
        private System.Windows.Forms.TabPage tabGames;
        public System.Windows.Forms.ToolTip Ttip;
        private System.Windows.Forms.Panel pnlDownloadConsoles;
        private System.Windows.Forms.Label lblUpdateConsoles;
        private System.Windows.Forms.Label lblProgressConsoles;
        private System.Windows.Forms.ProgressBar pgbConsoles;
        private System.Windows.Forms.Panel pnlDownloadGameList;
        private System.Windows.Forms.ProgressBar pgbGameList;
        private System.Windows.Forms.Label lblUpdateGameList;
        private System.Windows.Forms.Label lblProgressGameList;
        private System.Windows.Forms.TabPage tabUserInfo;
        private System.Windows.Forms.Label lblInfoName;
        private System.Windows.Forms.PictureBox picInfoIcon;
        private System.Windows.Forms.GroupBox gpbInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlInfoTop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInfoReleased;
        private System.Windows.Forms.Label lblInfoDeveloper;
        private System.Windows.Forms.Label lblInfoGenre;
        private System.Windows.Forms.Label lblInfoPublisher;
        private System.Windows.Forms.Label lblNotFoundGameList;
        private System.Windows.Forms.Label lblNotFoundConsoles;
        private System.Windows.Forms.Panel pnlDownloadInfo;
        private System.Windows.Forms.Label lblUpdateInfo;
        private System.Windows.Forms.Label lblProgressInfo;
        private System.Windows.Forms.ProgressBar pgbInfo;
        private System.Windows.Forms.PictureBox picInfoTitle;
        private System.Windows.Forms.PictureBox picLoaderConsole;
        private System.Windows.Forms.PictureBox picLoaderGameList;
        private System.Windows.Forms.GroupBox gpbInfoAchievements;
        private PanelNoScrollOnFocus pnlInfoScroll;
        private System.Windows.Forms.Panel pnlInfoImages;
        private System.Windows.Forms.FlowLayoutPanel flowInfo;
        private System.Windows.Forms.PictureBox picInfoInGame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUserCheevos;
        private System.Windows.Forms.CheckBox chkUserCheevos;
        private System.Windows.Forms.Label lblCheevos;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Panel pnlOutput;
        private FlatDataGridA dgvConsoles;
        private FlatButtonA btnUpdateConsoles;
        private FlatButtonA btnUpdateGameList;
        private FlatDataGridA dgvGames;
        private FlatButtonA btnUpdateInfo;
        private FlatButtonA btnUserCheevos;
        private GNX.PanelBorder pnlGamesConsoleName;
        private System.Windows.Forms.Label lblConsoleName;
        private System.Windows.Forms.Label lblConsoleGamesTotal;
        private FlatTextBoxA txtSearchGames;
        private System.Windows.Forms.DataGridViewTextBoxColumn cID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumGames;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTotalGames;
        private System.Windows.Forms.PictureBox picInfoBoxArt;
        private FlatDataGridA dgvAchievements;
        private System.Windows.Forms.DataGridViewTextBoxColumn aOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn aIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn aDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn aPoints;
        private FlatTextBoxA txtUsernameCheevos;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.Label lblAbTitle;
        private System.Windows.Forms.Label lblAbYear;
        private System.Windows.Forms.Label lblCheevoLoopUpdate;
        private System.Windows.Forms.TabPage tabTests;
        private System.Windows.Forms.Label lblConsoles;
        private System.Windows.Forms.Button btnDownloadBadges;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblDate2;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.DateTimePicker dtpDate2;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblDate1;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.DateTimePicker dtpDate1;
        private System.Windows.Forms.PictureBox picFBiDevIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn gID;
        private System.Windows.Forms.DataGridViewImageColumn gIconBitmap;
        private System.Windows.Forms.DataGridViewTextBoxColumn gTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn gConsole;
        private System.Windows.Forms.DataGridViewTextBoxColumn gNumAchievements;
        private System.Windows.Forms.DataGridViewTextBoxColumn gPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumLeaderboards;
        private System.Windows.Forms.DataGridViewTextBoxColumn gLastUpdated;
        private PictureBoxInterpolated picUserCheevos;
        private GNX.PanelBorder pnlBottomOutput;
        private CheckBoxBlueA chkWithoutAchievements;
        private CheckBoxBlueA chkUnlicensed;
        private CheckBoxBlueA chkDemo;
        private CheckBoxBlueA chkHack;
        private CheckBoxBlueA chkHomebrew;
        private CheckBoxBlueA chkPrototype;
        private CheckBoxBlueA chkOfficial;
        private FlatButtonA btnRaProfile;
    }
}

