namespace RADB
{
    partial class HashViewer
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
            this.txtHashes = new System.Windows.Forms.TextBox();
            this.picLoaderHash = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderHash)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHashes
            // 
            this.txtHashes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHashes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtHashes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHashes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHashes.Location = new System.Drawing.Point(12, 12);
            this.txtHashes.Multiline = true;
            this.txtHashes.Name = "txtHashes";
            this.txtHashes.ReadOnly = true;
            this.txtHashes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtHashes.Size = new System.Drawing.Size(719, 238);
            this.txtHashes.TabIndex = 0;
            // 
            // picLoaderHash
            // 
            this.picLoaderHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoaderHash.Image = global::RADB.Properties.Resources.loader;
            this.picLoaderHash.Location = new System.Drawing.Point(12, 98);
            this.picLoaderHash.Name = "picLoaderHash";
            this.picLoaderHash.Size = new System.Drawing.Size(702, 52);
            this.picLoaderHash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoaderHash.TabIndex = 5;
            this.picLoaderHash.TabStop = false;
            // 
            // HashViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(743, 262);
            this.Controls.Add(this.picLoaderHash);
            this.Controls.Add(this.txtHashes);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(296, 296);
            this.Name = "HashViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RA HashViewer";
            ((System.ComponentModel.ISupportInitialize)(this.picLoaderHash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHashes;
        private System.Windows.Forms.PictureBox picLoaderHash;
    }
}