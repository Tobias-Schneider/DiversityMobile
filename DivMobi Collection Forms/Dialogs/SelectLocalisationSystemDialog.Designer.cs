namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class SelectLocalisationSystemDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLocalisationSystemDialog));
            this.buttonGPS = new System.Windows.Forms.Button();
            this.buttonSamplingPlot = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGPS
            // 
            this.buttonGPS.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonGPS.Location = new System.Drawing.Point(40, 48);
            this.buttonGPS.Name = "buttonGPS";
            this.buttonGPS.Size = new System.Drawing.Size(142, 20);
            this.buttonGPS.TabIndex = 0;
            this.buttonGPS.Text = "GPS";
            this.buttonGPS.Click += new System.EventHandler(this.buttonGPS_Click);
            // 
            // buttonSamplingPlot
            // 
            this.buttonSamplingPlot.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSamplingPlot.Location = new System.Drawing.Point(40, 92);
            this.buttonSamplingPlot.Name = "buttonSamplingPlot";
            this.buttonSamplingPlot.Size = new System.Drawing.Size(142, 20);
            this.buttonSamplingPlot.TabIndex = 1;
            this.buttonSamplingPlot.Text = "SamplingPlot";
            this.buttonSamplingPlot.Click += new System.EventHandler(this.buttonSamplingPlot_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(40, 136);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(142, 20);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Info;
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Location = new System.Drawing.Point(1, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(228, 32);
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
            // SelectLocalisationSystemDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(230, 170);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSamplingPlot);
            this.Controls.Add(this.buttonGPS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "SelectLocalisationSystemDialog";
            this.Text = "Diversity Mobile";
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGPS;
        private System.Windows.Forms.Button buttonSamplingPlot;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
    }
}
