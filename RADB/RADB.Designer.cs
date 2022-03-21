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
            this.btnGameInfo = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.btnUpdateConsoles = new System.Windows.Forms.Button();
            this.btnGameList = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGamesGenre = new System.Windows.Forms.Button();
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
            this.sts = new System.Windows.Forms.StatusStrip();
            this.pgb = new System.Windows.Forms.ToolStripProgressBar();
            this.stsL1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cboConsoles = new System.Windows.Forms.ComboBox();
            this.lblConsoles = new System.Windows.Forms.Label();
            this.lblUpdateConsoles = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.sts.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGameInfo
            // 
            this.btnGameInfo.Location = new System.Drawing.Point(12, 51);
            this.btnGameInfo.Name = "btnGameInfo";
            this.btnGameInfo.Size = new System.Drawing.Size(190, 23);
            this.btnGameInfo.TabIndex = 1;
            this.btnGameInfo.Text = "GetGameInfo";
            this.btnGameInfo.UseVisualStyleBackColor = true;
            this.btnGameInfo.Click += new System.EventHandler(this.btnGameID_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(12, 25);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(92, 20);
            this.txtID.TabIndex = 2;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(9, 9);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 13);
            this.lblID.TabIndex = 3;
            this.lblID.Text = "ID";
            // 
            // btnUpdateConsoles
            // 
            this.btnUpdateConsoles.Location = new System.Drawing.Point(6, 6);
            this.btnUpdateConsoles.Name = "btnUpdateConsoles";
            this.btnUpdateConsoles.Size = new System.Drawing.Size(145, 23);
            this.btnUpdateConsoles.TabIndex = 4;
            this.btnUpdateConsoles.Text = "Consoles";
            this.btnUpdateConsoles.UseVisualStyleBackColor = true;
            this.btnUpdateConsoles.Click += new System.EventHandler(this.btnUpdateConsoles_Click);
            // 
            // btnGameList
            // 
            this.btnGameList.Location = new System.Drawing.Point(404, 63);
            this.btnGameList.Name = "btnGameList";
            this.btnGameList.Size = new System.Drawing.Size(190, 23);
            this.btnGameList.TabIndex = 5;
            this.btnGameList.Text = "Search Game List";
            this.btnGameList.UseVisualStyleBackColor = true;
            this.btnGameList.Click += new System.EventHandler(this.btnGameList_Click);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(12, 184);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(582, 20);
            this.txtURL.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "URL";
            // 
            // btnGamesGenre
            // 
            this.btnGamesGenre.Location = new System.Drawing.Point(404, 92);
            this.btnGamesGenre.Name = "btnGamesGenre";
            this.btnGamesGenre.Size = new System.Drawing.Size(190, 23);
            this.btnGamesGenre.TabIndex = 8;
            this.btnGamesGenre.Text = "Games Genre";
            this.btnGamesGenre.UseVisualStyleBackColor = true;
            this.btnGamesGenre.Click += new System.EventHandler(this.btnGamesGenre_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(12, 211);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(582, 223);
            this.txtOutput.TabIndex = 9;
            this.txtOutput.Text = "";
            this.txtOutput.WordWrap = false;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(107, 9);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(29, 13);
            this.lblUser.TabIndex = 12;
            this.lblUser.Text = "User";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(110, 25);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(92, 20);
            this.txtUser.TabIndex = 11;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(205, 9);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(35, 13);
            this.lblCount.TabIndex = 14;
            this.lblCount.Text = "Count";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(208, 25);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(92, 20);
            this.txtCount.TabIndex = 13;
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(303, 9);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(35, 13);
            this.lblOffset.TabIndex = 16;
            this.lblOffset.Text = "Offset";
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(306, 25);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(92, 20);
            this.txtOffset.TabIndex = 15;
            // 
            // dtpDate1
            // 
            this.dtpDate1.CustomFormat = "dd/MM/yyyy";
            this.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate1.Location = new System.Drawing.Point(404, 25);
            this.dtpDate1.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dtpDate1.Name = "dtpDate1";
            this.dtpDate1.Size = new System.Drawing.Size(92, 20);
            this.dtpDate1.TabIndex = 17;
            this.dtpDate1.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // lblDate1
            // 
            this.lblDate1.AutoSize = true;
            this.lblDate1.Location = new System.Drawing.Point(401, 9);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(55, 13);
            this.lblDate1.TabIndex = 18;
            this.lblDate1.Text = "Date Start";
            // 
            // lblDate2
            // 
            this.lblDate2.AutoSize = true;
            this.lblDate2.Location = new System.Drawing.Point(499, 9);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(52, 13);
            this.lblDate2.TabIndex = 20;
            this.lblDate2.Text = "Date End";
            // 
            // dtpDate2
            // 
            this.dtpDate2.CustomFormat = "dd/MM/yyyy";
            this.dtpDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate2.Location = new System.Drawing.Point(502, 25);
            this.dtpDate2.MinDate = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            this.dtpDate2.Name = "dtpDate2";
            this.dtpDate2.Size = new System.Drawing.Size(92, 20);
            this.dtpDate2.TabIndex = 19;
            this.dtpDate2.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // btnDownloadBadges
            // 
            this.btnDownloadBadges.Location = new System.Drawing.Point(404, 158);
            this.btnDownloadBadges.Name = "btnDownloadBadges";
            this.btnDownloadBadges.Size = new System.Drawing.Size(190, 23);
            this.btnDownloadBadges.TabIndex = 21;
            this.btnDownloadBadges.Text = "Download Badges";
            this.btnDownloadBadges.UseVisualStyleBackColor = true;
            this.btnDownloadBadges.Click += new System.EventHandler(this.btnDownloadBadges_Click);
            // 
            // sts
            // 
            this.sts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pgb,
            this.stsL1});
            this.sts.Location = new System.Drawing.Point(0, 437);
            this.sts.Name = "sts";
            this.sts.Size = new System.Drawing.Size(659, 22);
            this.sts.SizingGrip = false;
            this.sts.TabIndex = 24;
            this.sts.Text = "statusStrip1";
            // 
            // pgb
            // 
            this.pgb.Maximum = 10;
            this.pgb.Name = "pgb";
            this.pgb.Size = new System.Drawing.Size(100, 16);
            // 
            // stsL1
            // 
            this.stsL1.Name = "stsL1";
            this.stsL1.Size = new System.Drawing.Size(16, 17);
            this.stsL1.Text = "   ";
            // 
            // cboConsoles
            // 
            this.cboConsoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsoles.FormattingEnabled = true;
            this.cboConsoles.Location = new System.Drawing.Point(208, 51);
            this.cboConsoles.Name = "cboConsoles";
            this.cboConsoles.Size = new System.Drawing.Size(190, 21);
            this.cboConsoles.TabIndex = 25;
            // 
            // lblConsoles
            // 
            this.lblConsoles.AutoSize = true;
            this.lblConsoles.Location = new System.Drawing.Point(205, 35);
            this.lblConsoles.Name = "lblConsoles";
            this.lblConsoles.Size = new System.Drawing.Size(50, 13);
            this.lblConsoles.TabIndex = 26;
            this.lblConsoles.Text = "Consoles";
            // 
            // lblUpdateConsoles
            // 
            this.lblUpdateConsoles.AutoSize = true;
            this.lblUpdateConsoles.Location = new System.Drawing.Point(157, 11);
            this.lblUpdateConsoles.Name = "lblUpdateConsoles";
            this.lblUpdateConsoles.Size = new System.Drawing.Size(99, 13);
            this.lblUpdateConsoles.TabIndex = 27;
            this.lblUpdateConsoles.Text = "Consoles DateTime";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(47, 158);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(557, 193);
            this.tabControl1.TabIndex = 28;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(549, 167);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblUpdateConsoles);
            this.tabPage2.Controls.Add(this.btnUpdateConsoles);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(549, 167);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Updates";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // RADB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 459);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblConsoles);
            this.Controls.Add(this.cboConsoles);
            this.Controls.Add(this.sts);
            this.Controls.Add(this.btnDownloadBadges);
            this.Controls.Add(this.lblDate2);
            this.Controls.Add(this.dtpDate2);
            this.Controls.Add(this.lblDate1);
            this.Controls.Add(this.dtpDate1);
            this.Controls.Add(this.lblOffset);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGamesGenre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.btnGameList);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnGameInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RADB";
            this.Text = "RADB";
            this.sts.ResumeLayout(false);
            this.sts.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGameInfo;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnUpdateConsoles;
        private System.Windows.Forms.Button btnGameList;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGamesGenre;
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
        private System.Windows.Forms.StatusStrip sts;
        private System.Windows.Forms.ToolStripProgressBar pgb;
        private System.Windows.Forms.ToolStripStatusLabel stsL1;
        private System.Windows.Forms.ComboBox cboConsoles;
        private System.Windows.Forms.Label lblConsoles;
        private System.Windows.Forms.Label lblUpdateConsoles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

