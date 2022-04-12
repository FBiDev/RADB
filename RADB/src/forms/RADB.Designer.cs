namespace RADB
{
    partial class RADB
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RADB));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblOffset = new System.Windows.Forms.Label();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.dtpDate1 = new System.Windows.Forms.DateTimePicker();
            this.lblDate1 = new System.Windows.Forms.Label();
            this.lblDate2 = new System.Windows.Forms.Label();
            this.dtpDate2 = new System.Windows.Forms.DateTimePicker();
            this.btnDownloadBadges = new System.Windows.Forms.Button();
            this.lblConsoles = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabConsoles = new System.Windows.Forms.TabPage();
            this.pnlDownloadConsoles = new System.Windows.Forms.Panel();
            this.btnUpdateConsoles = new System.Windows.Forms.Button();
            this.lblUpdateConsoles = new System.Windows.Forms.Label();
            this.pgbConsoles = new System.Windows.Forms.ProgressBar();
            this.lblProgressConsoles = new System.Windows.Forms.Label();
            this.lblConsolesFound = new System.Windows.Forms.Label();
            this.dgvConsoles = new System.Windows.Forms.DataGridView();
            this.cID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabGames = new System.Windows.Forms.TabPage();
            this.pnlDownloadGameList = new System.Windows.Forms.Panel();
            this.btnUpdateGameList = new System.Windows.Forms.Button();
            this.lblUpdateGameList = new System.Windows.Forms.Label();
            this.pgbGameList = new System.Windows.Forms.ProgressBar();
            this.lblProgressGameList = new System.Windows.Forms.Label();
            this.lblGameListFound = new System.Windows.Forms.Label();
            this.dgvGameList = new System.Windows.Forms.DataGridView();
            this.gID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gIconBitmap = new System.Windows.Forms.DataGridViewImageColumn();
            this.gTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gAchievementsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gAchievementsPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gLastUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabGameInfo = new System.Windows.Forms.TabPage();
            this.pnlDownloadInfo = new System.Windows.Forms.Panel();
            this.btnUpdateInfo = new System.Windows.Forms.Button();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.pgbInfo = new System.Windows.Forms.ProgressBar();
            this.lblProgressInfo = new System.Windows.Forms.Label();
            this.gpbInfo = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInfoReleased = new System.Windows.Forms.Label();
            this.lblInfoDeveloper = new System.Windows.Forms.Label();
            this.lblInfoGenre = new System.Windows.Forms.Label();
            this.lblInfoPublisher = new System.Windows.Forms.Label();
            this.picInfoIcon = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInfoName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gpbInfoAchievements = new System.Windows.Forms.GroupBox();
            this.pnlAchievements = new System.Windows.Forms.Panel();
            this.picInfoTitle = new System.Windows.Forms.PictureBox();
            this.tabTemp = new System.Windows.Forms.TabPage();
            this.Ttip = new System.Windows.Forms.ToolTip(this.components);
            this.picLoaderConsole = new System.Windows.Forms.PictureBox();
            this.picLoaderGameList = new System.Windows.Forms.PictureBox();
            this.tabMain.SuspendLayout();
            this.tabConsoles.SuspendLayout();
            this.pnlDownloadConsoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsoles)).BeginInit();
            this.tabGames.SuspendLayout();
            this.pnlDownloadGameList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGameList)).BeginInit();
            this.tabGameInfo.SuspendLayout();
            this.pnlDownloadInfo.SuspendLayout();
            this.gpbInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoIcon)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.gpbInfoAchievements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoTitle)).BeginInit();
            this.tabTemp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderConsole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderGameList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(36, 20);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(92, 20);
            this.txtID.TabIndex = 2;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(33, 4);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "ID";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(6, 185);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(190, 20);
            this.txtURL.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "URL";
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(12, 558);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(635, 67);
            this.txtOutput.TabIndex = 9;
            this.txtOutput.Text = "";
            this.txtOutput.WordWrap = false;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(131, 4);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(29, 13);
            this.lblUser.TabIndex = 12;
            this.lblUser.Text = "User";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(134, 20);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(92, 20);
            this.txtUser.TabIndex = 11;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(232, 4);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(35, 13);
            this.lblCount.TabIndex = 14;
            this.lblCount.Text = "Count";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(235, 20);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(92, 20);
            this.txtCount.TabIndex = 13;
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(330, 4);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(35, 13);
            this.lblOffset.TabIndex = 16;
            this.lblOffset.Text = "Offset";
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(333, 20);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(92, 20);
            this.txtOffset.TabIndex = 15;
            // 
            // dtpDate1
            // 
            this.dtpDate1.CustomFormat = "dd/MM/yyyy";
            this.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate1.Location = new System.Drawing.Point(431, 20);
            this.dtpDate1.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dtpDate1.Name = "dtpDate1";
            this.dtpDate1.Size = new System.Drawing.Size(92, 20);
            this.dtpDate1.TabIndex = 17;
            this.dtpDate1.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // lblDate1
            // 
            this.lblDate1.AutoSize = true;
            this.lblDate1.Location = new System.Drawing.Point(428, 4);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(55, 13);
            this.lblDate1.TabIndex = 18;
            this.lblDate1.Text = "Date Start";
            // 
            // lblDate2
            // 
            this.lblDate2.AutoSize = true;
            this.lblDate2.Location = new System.Drawing.Point(526, 4);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(52, 13);
            this.lblDate2.TabIndex = 20;
            this.lblDate2.Text = "Date End";
            // 
            // dtpDate2
            // 
            this.dtpDate2.CustomFormat = "dd/MM/yyyy";
            this.dtpDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate2.Location = new System.Drawing.Point(529, 20);
            this.dtpDate2.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dtpDate2.Name = "dtpDate2";
            this.dtpDate2.Size = new System.Drawing.Size(92, 20);
            this.dtpDate2.TabIndex = 19;
            this.dtpDate2.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // btnDownloadBadges
            // 
            this.btnDownloadBadges.Location = new System.Drawing.Point(6, 46);
            this.btnDownloadBadges.Name = "btnDownloadBadges";
            this.btnDownloadBadges.Size = new System.Drawing.Size(190, 23);
            this.btnDownloadBadges.TabIndex = 21;
            this.btnDownloadBadges.Text = "Download Badges";
            this.btnDownloadBadges.UseVisualStyleBackColor = true;
            // 
            // lblConsoles
            // 
            this.lblConsoles.AutoSize = true;
            this.lblConsoles.Location = new System.Drawing.Point(232, 30);
            this.lblConsoles.Name = "lblConsoles";
            this.lblConsoles.Size = new System.Drawing.Size(50, 13);
            this.lblConsoles.TabIndex = 26;
            this.lblConsoles.Text = "Consoles";
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tabConsoles);
            this.tabMain.Controls.Add(this.tabGames);
            this.tabMain.Controls.Add(this.tabGameInfo);
            this.tabMain.Controls.Add(this.tabTemp);
            this.tabMain.Location = new System.Drawing.Point(12, 12);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(635, 540);
            this.tabMain.TabIndex = 28;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabConsoles
            // 
            this.tabConsoles.Controls.Add(this.pnlDownloadConsoles);
            this.tabConsoles.Controls.Add(this.lblConsolesFound);
            this.tabConsoles.Controls.Add(this.picLoaderConsole);
            this.tabConsoles.Controls.Add(this.dgvConsoles);
            this.tabConsoles.Location = new System.Drawing.Point(4, 22);
            this.tabConsoles.Name = "tabConsoles";
            this.tabConsoles.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsoles.Size = new System.Drawing.Size(627, 514);
            this.tabConsoles.TabIndex = 0;
            this.tabConsoles.Text = "Consoles";
            this.tabConsoles.UseVisualStyleBackColor = true;
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
            this.pnlDownloadConsoles.Size = new System.Drawing.Size(621, 31);
            this.pnlDownloadConsoles.TabIndex = 0;
            // 
            // btnUpdateConsoles
            // 
            this.btnUpdateConsoles.Location = new System.Drawing.Point(0, 3);
            this.btnUpdateConsoles.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnUpdateConsoles.Name = "btnUpdateConsoles";
            this.btnUpdateConsoles.Size = new System.Drawing.Size(144, 23);
            this.btnUpdateConsoles.TabIndex = 36;
            this.btnUpdateConsoles.Text = "Update Consoles";
            this.btnUpdateConsoles.UseVisualStyleBackColor = true;
            this.btnUpdateConsoles.Click += new System.EventHandler(this.btnUpdateConsoles_Click);
            // 
            // lblUpdateConsoles
            // 
            this.lblUpdateConsoles.Location = new System.Drawing.Point(150, 8);
            this.lblUpdateConsoles.Name = "lblUpdateConsoles";
            this.lblUpdateConsoles.Size = new System.Drawing.Size(110, 13);
            this.lblUpdateConsoles.TabIndex = 32;
            this.lblUpdateConsoles.Text = "00/00/0000 00:00:00";
            this.lblUpdateConsoles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbConsoles
            // 
            this.pgbConsoles.Location = new System.Drawing.Point(266, 3);
            this.pgbConsoles.MarqueeAnimationSpeed = 0;
            this.pgbConsoles.Name = "pgbConsoles";
            this.pgbConsoles.Size = new System.Drawing.Size(144, 21);
            this.pgbConsoles.Step = 1;
            this.pgbConsoles.TabIndex = 30;
            // 
            // lblProgressConsoles
            // 
            this.lblProgressConsoles.AutoSize = true;
            this.lblProgressConsoles.BackColor = System.Drawing.Color.Snow;
            this.lblProgressConsoles.Location = new System.Drawing.Point(416, 8);
            this.lblProgressConsoles.Name = "lblProgressConsoles";
            this.lblProgressConsoles.Size = new System.Drawing.Size(58, 13);
            this.lblProgressConsoles.TabIndex = 31;
            this.lblProgressConsoles.Text = "lblProgress";
            // 
            // lblConsolesFound
            // 
            this.lblConsolesFound.Location = new System.Drawing.Point(6, 162);
            this.lblConsolesFound.Name = "lblConsolesFound";
            this.lblConsolesFound.Size = new System.Drawing.Size(615, 23);
            this.lblConsolesFound.TabIndex = 3;
            this.lblConsolesFound.Text = "No Consoles Found";
            this.lblConsolesFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvConsoles
            // 
            this.dgvConsoles.AllowUserToAddRows = false;
            this.dgvConsoles.AllowUserToDeleteRows = false;
            this.dgvConsoles.AllowUserToResizeColumns = false;
            this.dgvConsoles.AllowUserToResizeRows = false;
            this.dgvConsoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConsoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvConsoles.BackgroundColor = System.Drawing.Color.White;
            this.dgvConsoles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvConsoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsoles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cID,
            this.cName});
            this.dgvConsoles.Location = new System.Drawing.Point(6, 43);
            this.dgvConsoles.MultiSelect = false;
            this.dgvConsoles.Name = "dgvConsoles";
            this.dgvConsoles.ReadOnly = true;
            this.dgvConsoles.RowHeadersVisible = false;
            this.dgvConsoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConsoles.Size = new System.Drawing.Size(615, 465);
            this.dgvConsoles.TabIndex = 0;
            this.dgvConsoles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConsoles_CellDoubleClick);
            // 
            // cID
            // 
            this.cID.DataPropertyName = "ID";
            this.cID.HeaderText = "ID";
            this.cID.Name = "cID";
            this.cID.ReadOnly = true;
            this.cID.Width = 43;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cName.DataPropertyName = "Name";
            this.cName.HeaderText = "Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // tabGames
            // 
            this.tabGames.Controls.Add(this.pnlDownloadGameList);
            this.tabGames.Controls.Add(this.lblGameListFound);
            this.tabGames.Controls.Add(this.picLoaderGameList);
            this.tabGames.Controls.Add(this.dgvGameList);
            this.tabGames.Location = new System.Drawing.Point(4, 22);
            this.tabGames.Name = "tabGames";
            this.tabGames.Padding = new System.Windows.Forms.Padding(3);
            this.tabGames.Size = new System.Drawing.Size(627, 514);
            this.tabGames.TabIndex = 2;
            this.tabGames.Text = "Games";
            this.tabGames.UseVisualStyleBackColor = true;
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
            this.pnlDownloadGameList.Size = new System.Drawing.Size(621, 31);
            this.pnlDownloadGameList.TabIndex = 0;
            // 
            // btnUpdateGameList
            // 
            this.btnUpdateGameList.Location = new System.Drawing.Point(0, 3);
            this.btnUpdateGameList.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnUpdateGameList.Name = "btnUpdateGameList";
            this.btnUpdateGameList.Size = new System.Drawing.Size(144, 23);
            this.btnUpdateGameList.TabIndex = 35;
            this.btnUpdateGameList.Text = "Update Games";
            this.btnUpdateGameList.UseVisualStyleBackColor = true;
            this.btnUpdateGameList.Click += new System.EventHandler(this.btnUpdateGameList_Click);
            // 
            // lblUpdateGameList
            // 
            this.lblUpdateGameList.Location = new System.Drawing.Point(150, 8);
            this.lblUpdateGameList.Name = "lblUpdateGameList";
            this.lblUpdateGameList.Size = new System.Drawing.Size(110, 13);
            this.lblUpdateGameList.TabIndex = 34;
            this.lblUpdateGameList.Text = "00/00/0000 00:00:00";
            this.lblUpdateGameList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbGameList
            // 
            this.pgbGameList.Location = new System.Drawing.Point(266, 3);
            this.pgbGameList.MarqueeAnimationSpeed = 0;
            this.pgbGameList.Name = "pgbGameList";
            this.pgbGameList.Size = new System.Drawing.Size(144, 21);
            this.pgbGameList.Step = 1;
            this.pgbGameList.TabIndex = 31;
            // 
            // lblProgressGameList
            // 
            this.lblProgressGameList.AutoSize = true;
            this.lblProgressGameList.BackColor = System.Drawing.Color.Snow;
            this.lblProgressGameList.Location = new System.Drawing.Point(416, 8);
            this.lblProgressGameList.Name = "lblProgressGameList";
            this.lblProgressGameList.Size = new System.Drawing.Size(58, 13);
            this.lblProgressGameList.TabIndex = 33;
            this.lblProgressGameList.Text = "lblProgress";
            // 
            // lblGameListFound
            // 
            this.lblGameListFound.Location = new System.Drawing.Point(6, 162);
            this.lblGameListFound.Name = "lblGameListFound";
            this.lblGameListFound.Size = new System.Drawing.Size(615, 23);
            this.lblGameListFound.TabIndex = 2;
            this.lblGameListFound.Text = "No Games Found";
            this.lblGameListFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvGameList
            // 
            this.dgvGameList.AllowUserToAddRows = false;
            this.dgvGameList.AllowUserToDeleteRows = false;
            this.dgvGameList.AllowUserToResizeColumns = false;
            this.dgvGameList.AllowUserToResizeRows = false;
            this.dgvGameList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGameList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvGameList.BackgroundColor = System.Drawing.Color.White;
            this.dgvGameList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGameList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGameList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gID,
            this.gIconBitmap,
            this.gTitle,
            this.gAchievementsCount,
            this.gAchievementsPoints,
            this.gLastUpdate});
            this.dgvGameList.Location = new System.Drawing.Point(6, 43);
            this.dgvGameList.MultiSelect = false;
            this.dgvGameList.Name = "dgvGameList";
            this.dgvGameList.ReadOnly = true;
            this.dgvGameList.RowHeadersVisible = false;
            this.dgvGameList.RowTemplate.Height = 36;
            this.dgvGameList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGameList.Size = new System.Drawing.Size(615, 465);
            this.dgvGameList.TabIndex = 1;
            this.dgvGameList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGameList_CellDoubleClick);
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
            this.gIconBitmap.DataPropertyName = "IconBitmap";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle13.NullValue")));
            dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(2);
            this.gIconBitmap.DefaultCellStyle = dataGridViewCellStyle13;
            this.gIconBitmap.HeaderText = "Icon";
            this.gIconBitmap.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
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
            // gAchievementsCount
            // 
            this.gAchievementsCount.DataPropertyName = "AchievementsCount";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gAchievementsCount.DefaultCellStyle = dataGridViewCellStyle14;
            this.gAchievementsCount.HeaderText = "Achievements";
            this.gAchievementsCount.Name = "gAchievementsCount";
            this.gAchievementsCount.ReadOnly = true;
            this.gAchievementsCount.Width = 99;
            // 
            // gAchievementsPoints
            // 
            this.gAchievementsPoints.DataPropertyName = "AchievementsPoints";
            this.gAchievementsPoints.HeaderText = "Points";
            this.gAchievementsPoints.Name = "gAchievementsPoints";
            this.gAchievementsPoints.ReadOnly = true;
            this.gAchievementsPoints.Width = 61;
            // 
            // gLastUpdate
            // 
            this.gLastUpdate.DataPropertyName = "LastUpdate";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gLastUpdate.DefaultCellStyle = dataGridViewCellStyle15;
            this.gLastUpdate.HeaderText = "LastUpdate";
            this.gLastUpdate.Name = "gLastUpdate";
            this.gLastUpdate.ReadOnly = true;
            this.gLastUpdate.Width = 87;
            // 
            // tabGameInfo
            // 
            this.tabGameInfo.Controls.Add(this.pnlDownloadInfo);
            this.tabGameInfo.Controls.Add(this.gpbInfo);
            this.tabGameInfo.Controls.Add(this.tableLayoutPanel1);
            this.tabGameInfo.Location = new System.Drawing.Point(4, 22);
            this.tabGameInfo.Name = "tabGameInfo";
            this.tabGameInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabGameInfo.Size = new System.Drawing.Size(627, 514);
            this.tabGameInfo.TabIndex = 1;
            this.tabGameInfo.Text = "GameInfo";
            this.tabGameInfo.UseVisualStyleBackColor = true;
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
            this.pnlDownloadInfo.Size = new System.Drawing.Size(615, 31);
            this.pnlDownloadInfo.TabIndex = 7;
            // 
            // btnUpdateInfo
            // 
            this.btnUpdateInfo.Location = new System.Drawing.Point(0, 3);
            this.btnUpdateInfo.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnUpdateInfo.Name = "btnUpdateInfo";
            this.btnUpdateInfo.Size = new System.Drawing.Size(144, 23);
            this.btnUpdateInfo.TabIndex = 35;
            this.btnUpdateInfo.Text = "Update Info";
            this.btnUpdateInfo.UseVisualStyleBackColor = true;
            this.btnUpdateInfo.Click += new System.EventHandler(this.btnUpdateInfo_Click);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.Location = new System.Drawing.Point(150, 8);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(110, 13);
            this.lblUpdateInfo.TabIndex = 34;
            this.lblUpdateInfo.Text = "00/00/0000 00:00:00";
            this.lblUpdateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgbInfo
            // 
            this.pgbInfo.Location = new System.Drawing.Point(266, 3);
            this.pgbInfo.MarqueeAnimationSpeed = 0;
            this.pgbInfo.Name = "pgbInfo";
            this.pgbInfo.Size = new System.Drawing.Size(144, 21);
            this.pgbInfo.Step = 1;
            this.pgbInfo.TabIndex = 31;
            // 
            // lblProgressInfo
            // 
            this.lblProgressInfo.AutoSize = true;
            this.lblProgressInfo.BackColor = System.Drawing.Color.Snow;
            this.lblProgressInfo.Location = new System.Drawing.Point(416, 8);
            this.lblProgressInfo.Name = "lblProgressInfo";
            this.lblProgressInfo.Size = new System.Drawing.Size(58, 13);
            this.lblProgressInfo.TabIndex = 33;
            this.lblProgressInfo.Text = "lblProgress";
            // 
            // gpbInfo
            // 
            this.gpbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbInfo.Controls.Add(this.panel1);
            this.gpbInfo.Controls.Add(this.lblInfoName);
            this.gpbInfo.Location = new System.Drawing.Point(6, 43);
            this.gpbInfo.Name = "gpbInfo";
            this.gpbInfo.Size = new System.Drawing.Size(615, 144);
            this.gpbInfo.TabIndex = 6;
            this.gpbInfo.TabStop = false;
            this.gpbInfo.Text = "Info";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblInfoReleased);
            this.panel1.Controls.Add(this.lblInfoDeveloper);
            this.panel1.Controls.Add(this.lblInfoGenre);
            this.panel1.Controls.Add(this.lblInfoPublisher);
            this.panel1.Controls.Add(this.picInfoIcon);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(6, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(603, 96);
            this.panel1.TabIndex = 9;
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
            // lblInfoName
            // 
            this.lblInfoName.AutoSize = true;
            this.lblInfoName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoName.Location = new System.Drawing.Point(6, 16);
            this.lblInfoName.Name = "lblInfoName";
            this.lblInfoName.Size = new System.Drawing.Size(61, 24);
            this.lblInfoName.TabIndex = 3;
            this.lblInfoName.Text = "Name";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gpbInfoAchievements, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.picInfoTitle, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 193);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(621, 306);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // gpbInfoAchievements
            // 
            this.gpbInfoAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbInfoAchievements.Controls.Add(this.pnlAchievements);
            this.gpbInfoAchievements.Location = new System.Drawing.Point(3, 108);
            this.gpbInfoAchievements.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.gpbInfoAchievements.Name = "gpbInfoAchievements";
            this.gpbInfoAchievements.Size = new System.Drawing.Size(615, 169);
            this.gpbInfoAchievements.TabIndex = 5;
            this.gpbInfoAchievements.TabStop = false;
            this.gpbInfoAchievements.Text = "Achievements";
            // 
            // pnlAchievements
            // 
            this.pnlAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAchievements.AutoScroll = true;
            this.pnlAchievements.Location = new System.Drawing.Point(6, 19);
            this.pnlAchievements.Name = "pnlAchievements";
            this.pnlAchievements.Size = new System.Drawing.Size(603, 144);
            this.pnlAchievements.TabIndex = 0;
            this.pnlAchievements.TabStop = true;
            // 
            // picInfoTitle
            // 
            this.picInfoTitle.Location = new System.Drawing.Point(3, 3);
            this.picInfoTitle.MaximumSize = new System.Drawing.Size(200, 240);
            this.picInfoTitle.Name = "picInfoTitle";
            this.picInfoTitle.Size = new System.Drawing.Size(96, 95);
            this.picInfoTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfoTitle.TabIndex = 13;
            this.picInfoTitle.TabStop = false;
            // 
            // tabTemp
            // 
            this.tabTemp.Controls.Add(this.lblConsoles);
            this.tabTemp.Controls.Add(this.btnDownloadBadges);
            this.tabTemp.Controls.Add(this.label2);
            this.tabTemp.Controls.Add(this.txtURL);
            this.tabTemp.Controls.Add(this.lblUser);
            this.tabTemp.Controls.Add(this.txtUser);
            this.tabTemp.Controls.Add(this.lblDate2);
            this.tabTemp.Controls.Add(this.lblID);
            this.tabTemp.Controls.Add(this.dtpDate2);
            this.tabTemp.Controls.Add(this.txtID);
            this.tabTemp.Controls.Add(this.lblCount);
            this.tabTemp.Controls.Add(this.lblOffset);
            this.tabTemp.Controls.Add(this.txtCount);
            this.tabTemp.Controls.Add(this.lblDate1);
            this.tabTemp.Controls.Add(this.txtOffset);
            this.tabTemp.Controls.Add(this.dtpDate1);
            this.tabTemp.Location = new System.Drawing.Point(4, 22);
            this.tabTemp.Name = "tabTemp";
            this.tabTemp.Padding = new System.Windows.Forms.Padding(3);
            this.tabTemp.Size = new System.Drawing.Size(627, 514);
            this.tabTemp.TabIndex = 3;
            this.tabTemp.Text = "Temp";
            this.tabTemp.UseVisualStyleBackColor = true;
            // 
            // picLoaderConsole
            // 
            this.picLoaderConsole.Image = global::RADB.Properties.Resources.loader;
            this.picLoaderConsole.Location = new System.Drawing.Point(6, 146);
            this.picLoaderConsole.Name = "picLoaderConsole";
            this.picLoaderConsole.Size = new System.Drawing.Size(615, 52);
            this.picLoaderConsole.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoaderConsole.TabIndex = 4;
            this.picLoaderConsole.TabStop = false;
            this.picLoaderConsole.Visible = false;
            // 
            // picLoaderGameList
            // 
            this.picLoaderGameList.Image = global::RADB.Properties.Resources.loader;
            this.picLoaderGameList.Location = new System.Drawing.Point(6, 146);
            this.picLoaderGameList.Name = "picLoaderGameList";
            this.picLoaderGameList.Size = new System.Drawing.Size(615, 52);
            this.picLoaderGameList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoaderGameList.TabIndex = 5;
            this.picLoaderGameList.TabStop = false;
            this.picLoaderGameList.Visible = false;
            // 
            // RADB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 650);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.txtOutput);
            this.Name = "RADB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RADB";
            this.tabMain.ResumeLayout(false);
            this.tabConsoles.ResumeLayout(false);
            this.pnlDownloadConsoles.ResumeLayout(false);
            this.pnlDownloadConsoles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsoles)).EndInit();
            this.tabGames.ResumeLayout(false);
            this.pnlDownloadGameList.ResumeLayout(false);
            this.pnlDownloadGameList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGameList)).EndInit();
            this.tabGameInfo.ResumeLayout(false);
            this.tabGameInfo.PerformLayout();
            this.pnlDownloadInfo.ResumeLayout(false);
            this.pnlDownloadInfo.PerformLayout();
            this.gpbInfo.ResumeLayout(false);
            this.gpbInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfoIcon)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gpbInfoAchievements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInfoTitle)).EndInit();
            this.tabTemp.ResumeLayout(false);
            this.tabTemp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderConsole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderGameList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.DateTimePicker dtpDate1;
        private System.Windows.Forms.Label lblDate1;
        private System.Windows.Forms.Label lblDate2;
        private System.Windows.Forms.DateTimePicker dtpDate2;
        private System.Windows.Forms.Button btnDownloadBadges;
        private System.Windows.Forms.Label lblConsoles;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabConsoles;
        private System.Windows.Forms.TabPage tabGameInfo;
        private System.Windows.Forms.TabPage tabGames;
        public System.Windows.Forms.ToolTip Ttip;
        private System.Windows.Forms.Panel pnlDownloadConsoles;
        private System.Windows.Forms.Label lblUpdateConsoles;
        private System.Windows.Forms.Label lblProgressConsoles;
        private System.Windows.Forms.ProgressBar pgbConsoles;
        private System.Windows.Forms.DataGridView dgvConsoles;
        private System.Windows.Forms.Panel pnlDownloadGameList;
        private System.Windows.Forms.ProgressBar pgbGameList;
        private System.Windows.Forms.Label lblUpdateGameList;
        private System.Windows.Forms.Label lblProgressGameList;
        private System.Windows.Forms.Button btnUpdateConsoles;
        private System.Windows.Forms.Button btnUpdateGameList;
        private System.Windows.Forms.DataGridView dgvGameList;
        private System.Windows.Forms.DataGridViewTextBoxColumn cID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.TabPage tabTemp;
        private System.Windows.Forms.Label lblInfoName;
        private System.Windows.Forms.PictureBox picInfoIcon;
        private System.Windows.Forms.GroupBox gpbInfoAchievements;
        private System.Windows.Forms.GroupBox gpbInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInfoReleased;
        private System.Windows.Forms.Label lblInfoDeveloper;
        private System.Windows.Forms.Label lblInfoGenre;
        private System.Windows.Forms.Label lblInfoPublisher;
        private System.Windows.Forms.Panel pnlAchievements;
        private System.Windows.Forms.Label lblGameListFound;
        private System.Windows.Forms.Label lblConsolesFound;
        private System.Windows.Forms.Panel pnlDownloadInfo;
        private System.Windows.Forms.Button btnUpdateInfo;
        private System.Windows.Forms.Label lblUpdateInfo;
        private System.Windows.Forms.Label lblProgressInfo;
        private System.Windows.Forms.ProgressBar pgbInfo;
        private System.Windows.Forms.PictureBox picInfoTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn gID;
        private System.Windows.Forms.DataGridViewImageColumn gIconBitmap;
        private System.Windows.Forms.DataGridViewTextBoxColumn gTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn gAchievementsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn gAchievementsPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn gLastUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox picLoaderConsole;
        private System.Windows.Forms.PictureBox picLoaderGameList;
    }
}

