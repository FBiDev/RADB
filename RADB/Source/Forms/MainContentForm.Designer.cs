﻿namespace RADB
{
    partial class MainContentForm
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
            this.ContentRPanel = new RADB.FlatPanelA();
            this.ContentRInsidePanel = new RADB.FlatPanelA();
            this.ContentLPanel = new RADB.FlatPanelA();
            this.MenuTable = new RADB.FlatTableA();
            this.SpeedRunTabButton = new RADB.FlatButtonA();
            this.ConfigTabButton = new RADB.FlatButtonA();
            this.ContentRPanel.SuspendLayout();
            this.ContentLPanel.SuspendLayout();
            this.MenuTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentRPanel
            // 
            this.ContentRPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.ContentRPanel.BackColorType = App.Core.Desktop.PanelType.white;
            this.ContentRPanel.Controls.Add(this.ContentRInsidePanel);
            this.ContentRPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentRPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ContentRPanel.Location = new System.Drawing.Point(124, 0);
            this.ContentRPanel.Name = "ContentRPanel";
            this.ContentRPanel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.ContentRPanel.Size = new System.Drawing.Size(306, 360);
            // 
            // ContentRInsidePanel
            // 
            this.ContentRInsidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentRInsidePanel.Location = new System.Drawing.Point(4, 0);
            this.ContentRInsidePanel.Name = "ContentRInsidePanel";
            this.ContentRInsidePanel.Size = new System.Drawing.Size(302, 360);
            // 
            // ContentLPanel
            // 
            this.ContentLPanel.Controls.Add(this.MenuTable);
            this.ContentLPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ContentLPanel.Location = new System.Drawing.Point(0, 0);
            this.ContentLPanel.Name = "ContentLPanel";
            this.ContentLPanel.Size = new System.Drawing.Size(124, 360);
            this.ContentLPanel.TabIndex = 1;
            // 
            // MenuTable
            // 
            this.MenuTable.ColumnCount = 1;
            this.MenuTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MenuTable.Controls.Add(this.SpeedRunTabButton, 0, 0);
            this.MenuTable.Controls.Add(this.ConfigTabButton, 0, 1);
            this.MenuTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuTable.Location = new System.Drawing.Point(0, 0);
            this.MenuTable.Name = "MenuTable";
            this.MenuTable.Padding = new System.Windows.Forms.Padding(1);
            this.MenuTable.RowCount = 2;
            this.MenuTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MenuTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MenuTable.Size = new System.Drawing.Size(124, 360);
            this.MenuTable.SizeOriginal = new System.Drawing.Size(124, 360);
            this.MenuTable.TabIndex = 0;
            // 
            // SpeedRunTabButton
            // 
            this.SpeedRunTabButton.BorderColorDefault = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.SpeedRunTabButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SpeedRunTabButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.SpeedRunTabButton.FlatAppearance.BorderSize = 0;
            this.SpeedRunTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.SpeedRunTabButton.Location = new System.Drawing.Point(1, 1);
            this.SpeedRunTabButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.SpeedRunTabButton.Name = "SpeedRunTabButton";
            this.SpeedRunTabButton.Size = new System.Drawing.Size(122, 34);
            this.SpeedRunTabButton.TabIndex = 0;
            this.SpeedRunTabButton.Text = "SpeedRun";
            // 
            // ConfigTabButton
            // 
            this.ConfigTabButton.BorderColorDefault = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.ConfigTabButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ConfigTabButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(223)))), ((int)(((byte)(229)))));
            this.ConfigTabButton.FlatAppearance.BorderSize = 0;
            this.ConfigTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(213)))), ((int)(((byte)(213)))));
            this.ConfigTabButton.Location = new System.Drawing.Point(1, 325);
            this.ConfigTabButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ConfigTabButton.Name = "ConfigTabButton";
            this.ConfigTabButton.Size = new System.Drawing.Size(122, 34);
            this.ConfigTabButton.TabIndex = 1;
            this.ConfigTabButton.Text = "Config";
            // 
            // MainContentForm
            // 
            this.ClientSize = new System.Drawing.Size(430, 360);
            this.Controls.Add(this.ContentRPanel);
            this.Controls.Add(this.ContentLPanel);
            this.Name = "MainContentForm";
            this.SizeOriginal = new System.Drawing.Size(430, 262);
            this.Text = "MainContentForm";
            this.ContentRPanel.ResumeLayout(false);
            this.ContentLPanel.ResumeLayout(false);
            this.ContentLPanel.PerformLayout();
            this.MenuTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal FlatPanelA ContentRPanel;
        internal FlatPanelA ContentLPanel;
        internal FlatPanelA ContentRInsidePanel;
        private FlatTableA MenuTable;
        internal FlatButtonA ConfigTabButton;
        internal FlatButtonA SpeedRunTabButton;

    }
}