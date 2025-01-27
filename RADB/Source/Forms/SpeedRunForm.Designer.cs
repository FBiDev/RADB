namespace RADB
{
    partial class SpeedRunForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainContentTable = new RADB.FlatTableA();
            this.TitleLabel = new RADB.FlatLabelA();
            this.MainTabControl = new RADB.FlatTabControlA();
            this.SearchTab = new System.Windows.Forms.TabPage();
            this.InputTable = new RADB.FlatTableA();
            this.SearchGameTextBox = new RADB.FlatTextBoxA();
            this.SearchGameButton = new RADB.FlatButtonA();
            this.SearchGameGrid = new RADB.FlatDataGridA();
            this.TimesTab = new System.Windows.Forms.TabPage();
            this.MainContentTable.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.SearchTab.SuspendLayout();
            this.InputTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchGameGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // MainContentTable
            // 
            this.MainContentTable.ColumnCount = 1;
            this.MainContentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainContentTable.Controls.Add(this.TitleLabel, 0, 0);
            this.MainContentTable.Controls.Add(this.MainTabControl, 0, 1);
            this.MainContentTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContentTable.FillOnFormResize = true;
            this.MainContentTable.Location = new System.Drawing.Point(0, 0);
            this.MainContentTable.Name = "MainContentTable";
            this.MainContentTable.Padding = new System.Windows.Forms.Padding(1);
            this.MainContentTable.RowCount = 2;
            this.MainContentTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.MainContentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainContentTable.Size = new System.Drawing.Size(745, 364);
            this.MainContentTable.SizeOriginal = new System.Drawing.Size(745, 364);
            this.MainContentTable.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.TitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(149)))), ((int)(((byte)(237)))));
            this.TitleLabel.ForeColorType = App.Core.Desktop.LabelType.primary;
            this.TitleLabel.Location = new System.Drawing.Point(1, 1);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(743, 34);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Speed";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.SearchTab);
            this.MainTabControl.Controls.Add(this.TimesTab);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(4, 40);
            this.MainTabControl.MyBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.MainTabControl.MyBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.MainTabControl.MyBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(737, 320);
            this.MainTabControl.TabIndex = 1;
            // 
            // SearchTab
            // 
            this.SearchTab.Controls.Add(this.InputTable);
            this.SearchTab.Location = new System.Drawing.Point(4, 25);
            this.SearchTab.Name = "SearchTab";
            this.SearchTab.Padding = new System.Windows.Forms.Padding(3);
            this.SearchTab.Size = new System.Drawing.Size(729, 291);
            this.SearchTab.TabIndex = 0;
            this.SearchTab.Text = "Game Search";
            this.SearchTab.UseVisualStyleBackColor = true;
            // 
            // InputTable
            // 
            this.InputTable.ColumnCount = 4;
            this.InputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.InputTable.Controls.Add(this.SearchGameTextBox, 0, 0);
            this.InputTable.Controls.Add(this.SearchGameButton, 3, 0);
            this.InputTable.Controls.Add(this.SearchGameGrid, 0, 1);
            this.InputTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputTable.Location = new System.Drawing.Point(3, 3);
            this.InputTable.Name = "InputTable";
            this.InputTable.RowCount = 2;
            this.InputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InputTable.Size = new System.Drawing.Size(723, 285);
            this.InputTable.SizeOriginal = new System.Drawing.Size(723, 285);
            this.InputTable.TabIndex = 13;
            // 
            // SearchGameTextBox
            // 
            this.InputTable.SetColumnSpan(this.SearchGameTextBox, 3);
            this.SearchGameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchGameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchGameTextBox.LabelText = "Name";
            this.SearchGameTextBox.Location = new System.Drawing.Point(2, 2);
            this.SearchGameTextBox.Name = "SearchGameTextBox";
            this.SearchGameTextBox.PreviousText = "";
            this.SearchGameTextBox.Size = new System.Drawing.Size(536, 34);
            this.SearchGameTextBox.TabIndex = 0;
            // 
            // SearchGameButton
            // 
            this.SearchGameButton.BorderColorDefault = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.SearchGameButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchGameButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.SearchGameButton.FlatAppearance.BorderSize = 0;
            this.SearchGameButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.SearchGameButton.Location = new System.Drawing.Point(542, 2);
            this.SearchGameButton.Name = "SearchGameButton";
            this.SearchGameButton.Size = new System.Drawing.Size(179, 34);
            this.SearchGameButton.TabIndex = 1;
            this.SearchGameButton.Text = "Search";
            // 
            // SearchGameGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.SearchGameGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SearchGameGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.SearchGameGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.SearchGameGrid.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.SearchGameGrid.ColorColumnHeader = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.SearchGameGrid.ColorColumnHeaderMouseHover = System.Drawing.Color.Empty;
            this.SearchGameGrid.ColorColumnHeaderReorderDiv = System.Drawing.Color.Empty;
            this.SearchGameGrid.ColorColumnHeaderReorderRec = System.Drawing.Color.Empty;
            this.SearchGameGrid.ColorColumnSelection = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.SearchGameGrid.ColorFontRow = System.Drawing.Color.White;
            this.SearchGameGrid.ColorFontRowSelection = System.Drawing.SystemColors.HighlightText;
            this.SearchGameGrid.ColorGrid = System.Drawing.Color.Silver;
            this.SearchGameGrid.ColorRow = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.SearchGameGrid.ColorRowAlternate = System.Drawing.Color.White;
            this.SearchGameGrid.ColorRowMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(237)))), ((int)(((byte)(255)))));
            this.SearchGameGrid.ColorRowSelection = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SearchGameGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.InputTable.SetColumnSpan(this.SearchGameGrid, 4);
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SearchGameGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.SearchGameGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchGameGrid.Location = new System.Drawing.Point(3, 41);
            this.SearchGameGrid.Name = "SearchGameGrid";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SearchGameGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.SearchGameGrid.RowTemplate.Height = 30;
            this.SearchGameGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SearchGameGrid.Size = new System.Drawing.Size(717, 241);
            this.SearchGameGrid.StandardTab = true;
            this.SearchGameGrid.TabIndex = 2;
            this.SearchGameGrid.TabStop = true;
            // 
            // TimesTab
            // 
            this.TimesTab.Location = new System.Drawing.Point(4, 25);
            this.TimesTab.Name = "TimesTab";
            this.TimesTab.Padding = new System.Windows.Forms.Padding(3);
            this.TimesTab.Size = new System.Drawing.Size(729, 291);
            this.TimesTab.TabIndex = 1;
            this.TimesTab.Text = "Game Times";
            this.TimesTab.UseVisualStyleBackColor = true;
            // 
            // SpeedRunForm
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(745, 364);
            this.Controls.Add(this.MainContentTable);
            this.Name = "SpeedRunForm";
            this.SizeOriginal = new System.Drawing.Size(430, 262);
            this.Text = "SpeedRunForm";
            this.MainContentTable.ResumeLayout(false);
            this.MainContentTable.PerformLayout();
            this.MainTabControl.ResumeLayout(false);
            this.SearchTab.ResumeLayout(false);
            this.SearchTab.PerformLayout();
            this.InputTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SearchGameGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlatTableA MainContentTable;
        private FlatLabelA TitleLabel;
        private FlatTabControlA MainTabControl;
        private System.Windows.Forms.TabPage SearchTab;
        private System.Windows.Forms.TabPage TimesTab;
        internal FlatTextBoxA SearchGameTextBox;
        internal FlatButtonA SearchGameButton;
        private FlatTableA InputTable;
        internal FlatDataGridA SearchGameGrid;
    }
}