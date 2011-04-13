namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    partial class AudioForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioForm));
            this.buttonPlay = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonPlay.Location = new System.Drawing.Point(43, 84);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(153, 34);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(240, 32);
            // 
            // pictureBoxFormImage
            // 
            this.pictureBoxFormImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxFormImage.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxFormImage.Image")));
            this.pictureBoxFormImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFormImage.Name = "pictureBoxFormImage";
            this.pictureBoxFormImage.Size = new System.Drawing.Size(40, 32);
            this.pictureBoxFormImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // AudioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 214);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.buttonPlay);
            this.Name = "AudioForm";
            this.Text = "AudioForm";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
    }
}