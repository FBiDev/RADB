﻿namespace RADB
{
    partial class ConfigForm
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
            this.MainContentTable = new RADB.FlatTableA();
            this.InputTable = new RADB.FlatTableA();
            this.MainContentTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainContentTable
            // 
            this.MainContentTable.ColumnCount = 1;
            this.MainContentTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainContentTable.Controls.Add(this.InputTable, 0, 0);
            this.MainContentTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContentTable.FillOnFormResize = true;
            this.MainContentTable.Location = new System.Drawing.Point(0, 0);
            this.MainContentTable.Name = "MainContentTable";
            this.MainContentTable.Padding = new System.Windows.Forms.Padding(1);
            this.MainContentTable.RowCount = 2;
            this.MainContentTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainContentTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MainContentTable.Size = new System.Drawing.Size(300, 368);
            this.MainContentTable.SizeOriginal = new System.Drawing.Size(200, 100);
            this.MainContentTable.TabIndex = 0;
            // 
            // InputTable
            // 
            this.InputTable.ColumnCount = 2;
            this.InputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.InputTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.InputTable.Location = new System.Drawing.Point(1, 1);
            this.InputTable.Name = "InputTable";
            this.InputTable.RowCount = 2;
            this.InputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InputTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InputTable.Size = new System.Drawing.Size(298, 34);
            this.InputTable.SizeOriginal = new System.Drawing.Size(200, 100);
            this.InputTable.TabIndex = 0;
            // 
            // ConfigForm
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(300, 368);
            this.Controls.Add(this.MainContentTable);
            this.Name = "ConfigForm";
            this.SizeOriginal = new System.Drawing.Size(430, 262);
            this.Text = "ConfigForm";
            this.MainContentTable.ResumeLayout(false);
            this.MainContentTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlatTableA MainContentTable;
        internal FlatTableA InputTable;
    }
}