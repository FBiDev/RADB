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
            this.btnGameID = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnConsoleIDs = new System.Windows.Forms.Button();
            this.btnGameList = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGamesGenre = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.btnMergeImages = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGameID
            // 
            this.btnGameID.Location = new System.Drawing.Point(12, 51);
            this.btnGameID.Name = "btnGameID";
            this.btnGameID.Size = new System.Drawing.Size(199, 23);
            this.btnGameID.TabIndex = 1;
            this.btnGameID.Text = "Search GameID";
            this.btnGameID.UseVisualStyleBackColor = true;
            this.btnGameID.Click += new System.EventHandler(this.btnGameID_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(635, 20);
            this.txtSearch.TabIndex = 2;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 9);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 3;
            this.lblSearch.Text = "Search";
            // 
            // btnConsoleIDs
            // 
            this.btnConsoleIDs.Location = new System.Drawing.Point(12, 80);
            this.btnConsoleIDs.Name = "btnConsoleIDs";
            this.btnConsoleIDs.Size = new System.Drawing.Size(199, 23);
            this.btnConsoleIDs.TabIndex = 4;
            this.btnConsoleIDs.Text = "Search ConsoleIDs";
            this.btnConsoleIDs.UseVisualStyleBackColor = true;
            this.btnConsoleIDs.Click += new System.EventHandler(this.btnConsoleIDs_Click);
            // 
            // btnGameList
            // 
            this.btnGameList.Location = new System.Drawing.Point(12, 109);
            this.btnGameList.Name = "btnGameList";
            this.btnGameList.Size = new System.Drawing.Size(199, 23);
            this.btnGameList.TabIndex = 5;
            this.btnGameList.Text = "Search Game List";
            this.btnGameList.UseVisualStyleBackColor = true;
            this.btnGameList.Click += new System.EventHandler(this.btnGameList_Click);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(12, 184);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(635, 20);
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
            this.btnGamesGenre.Location = new System.Drawing.Point(217, 51);
            this.btnGamesGenre.Name = "btnGamesGenre";
            this.btnGamesGenre.Size = new System.Drawing.Size(199, 23);
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
            this.txtOutput.Size = new System.Drawing.Size(635, 236);
            this.txtOutput.TabIndex = 9;
            this.txtOutput.Text = "";
            this.txtOutput.WordWrap = false;
            // 
            // btnMergeImages
            // 
            this.btnMergeImages.Location = new System.Drawing.Point(217, 80);
            this.btnMergeImages.Name = "btnMergeImages";
            this.btnMergeImages.Size = new System.Drawing.Size(199, 23);
            this.btnMergeImages.TabIndex = 10;
            this.btnMergeImages.Text = "Merge Cheevo Images";
            this.btnMergeImages.UseVisualStyleBackColor = true;
            this.btnMergeImages.Click += new System.EventHandler(this.btnMergeImages_Click);
            // 
            // RADB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 459);
            this.Controls.Add(this.btnMergeImages);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGamesGenre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.btnGameList);
            this.Controls.Add(this.btnConsoleIDs);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnGameID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RADB";
            this.Text = "RADB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGameID;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnConsoleIDs;
        private System.Windows.Forms.Button btnGameList;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGamesGenre;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Button btnMergeImages;
    }
}

