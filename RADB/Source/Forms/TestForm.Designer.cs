﻿namespace RADB
{
    partial class TestForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flatLabelA3 = new RADB.FlatLabelA();
            this.flatLabelA4 = new RADB.FlatLabelA();
            this.flatArticle2 = new RADB.FlatArticle();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flatLabelA3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flatLabelA4, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 110);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(150, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 92);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // flatLabelA3
            // 
            this.flatLabelA3.AutoSize = true;
            this.flatLabelA3.BackColor = System.Drawing.Color.Gainsboro;
            this.flatLabelA3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flatLabelA3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flatLabelA3.Location = new System.Drawing.Point(1, 1);
            this.flatLabelA3.Margin = new System.Windows.Forms.Padding(0);
            this.flatLabelA3.Name = "flatLabelA3";
            this.flatLabelA3.Padding = new System.Windows.Forms.Padding(0);
            this.flatLabelA3.Size = new System.Drawing.Size(148, 30);
            this.flatLabelA3.TabIndex = 0;
            this.flatLabelA3.Text = "Asa bbb bbb bbb bbb bbb aaa bbb ";
            // 
            // flatLabelA4
            // 
            this.flatLabelA4.AutoSize = true;
            this.flatLabelA4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flatLabelA4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flatLabelA4.Location = new System.Drawing.Point(1, 31);
            this.flatLabelA4.Margin = new System.Windows.Forms.Padding(0);
            this.flatLabelA4.Name = "flatLabelA4";
            this.flatLabelA4.Padding = new System.Windows.Forms.Padding(0);
            this.flatLabelA4.Size = new System.Drawing.Size(148, 60);
            this.flatLabelA4.TabIndex = 1;
            this.flatLabelA4.Text = "aaa Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast ";
            // 
            // flatArticle2
            // 
            this.flatArticle2.BackColor = System.Drawing.SystemColors.Control;
            this.flatArticle2.BackColor2 = System.Drawing.SystemColors.Control;
            this.flatArticle2.BorderColor = System.Drawing.Color.Black;
            this.flatArticle2.Location = new System.Drawing.Point(12, 12);
            this.flatArticle2.MaximumSize = new System.Drawing.Size(150, 0);
            this.flatArticle2.Name = "flatArticle2";
            this.flatArticle2.Size = new System.Drawing.Size(148, 92);
            this.flatArticle2.TabIndex = 15;
            this.flatArticle2.TextDesc = "aaa Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast Ps1 Mast ";
            this.flatArticle2.TextTitle = "Asa bbb bbb bbb bbb bbb aaa bbb ";
            // 
            // TestForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(674, 450);
            this.Controls.Add(this.flatArticle2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private FlatLabelA flatLabelA3;
        private FlatLabelA flatLabelA4;
        private FlatArticle flatArticle2;
    }
}