namespace DivMobiDirectDataTransfer
{
    partial class DirectTransferForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectMobileButton = new System.Windows.Forms.Button();
            this.connectRepoitoryButton = new System.Windows.Forms.Button();
            this.InsertButton = new System.Windows.Forms.Button();
            this.buttonIdentifications = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // connectMobileButton
            // 
            this.connectMobileButton.Location = new System.Drawing.Point(300, 40);
            this.connectMobileButton.Name = "connectMobileButton";
            this.connectMobileButton.Size = new System.Drawing.Size(161, 23);
            this.connectMobileButton.TabIndex = 0;
            this.connectMobileButton.Text = "ConnectMobileDB";
            this.connectMobileButton.UseVisualStyleBackColor = true;
            this.connectMobileButton.Click += new System.EventHandler(this.connectMobileButton_Click);
            // 
            // connectRepoitoryButton
            // 
            this.connectRepoitoryButton.Location = new System.Drawing.Point(62, 40);
            this.connectRepoitoryButton.Name = "connectRepoitoryButton";
            this.connectRepoitoryButton.Size = new System.Drawing.Size(189, 23);
            this.connectRepoitoryButton.TabIndex = 1;
            this.connectRepoitoryButton.Text = "Connect to Repository";
            this.connectRepoitoryButton.UseVisualStyleBackColor = true;
            this.connectRepoitoryButton.Click += new System.EventHandler(this.connectRepoitoryButton_Click);
            // 
            // InsertButton
            // 
            this.InsertButton.Location = new System.Drawing.Point(167, 85);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(187, 23);
            this.InsertButton.TabIndex = 2;
            this.InsertButton.Text = "Insert CE->Server";
            this.InsertButton.UseVisualStyleBackColor = true;
            this.InsertButton.Click += new System.EventHandler(this.InsertButton_Click);
            // 
            // buttonIdentifications
            // 
            this.buttonIdentifications.Location = new System.Drawing.Point(31, 117);
            this.buttonIdentifications.Name = "buttonIdentifications";
            this.buttonIdentifications.Size = new System.Drawing.Size(148, 23);
            this.buttonIdentifications.TabIndex = 3;
            this.buttonIdentifications.Text = "CopyTaxonChache->Taxon";
            this.buttonIdentifications.UseVisualStyleBackColor = true;
            this.buttonIdentifications.Click += new System.EventHandler(this.buttonIdentifications_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(300, 116);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(161, 23);
            this.buttonUpdate.TabIndex = 4;
            this.buttonUpdate.Text = "Update Identifications and IU´s";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(52, 259);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // DirectTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 336);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonIdentifications);
            this.Controls.Add(this.InsertButton);
            this.Controls.Add(this.connectRepoitoryButton);
            this.Controls.Add(this.connectMobileButton);
            this.Name = "DirectTransferForm";
            this.Text = "DivMobi Direct Insert";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button connectMobileButton;
        private System.Windows.Forms.Button connectRepoitoryButton;
        private System.Windows.Forms.Button InsertButton;
        private System.Windows.Forms.Button buttonIdentifications;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

