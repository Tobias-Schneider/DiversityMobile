namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class NewGPSLocalisationForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGPSLocalisationForm));
            this.labelLongitude = new System.Windows.Forms.Label();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.labelAltitude = new System.Windows.Forms.Label();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonGPS = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.labelCaption = new System.Windows.Forms.Label();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLongitude
            // 
            this.labelLongitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelLongitude.Location = new System.Drawing.Point(10, 68);
            this.labelLongitude.Name = "labelLongitude";
            this.labelLongitude.Size = new System.Drawing.Size(62, 20);
            this.labelLongitude.Text = "Longitude:";
            // 
            // labelLatitude
            // 
            this.labelLatitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelLatitude.Location = new System.Drawing.Point(10, 43);
            this.labelLatitude.Name = "labelLatitude";
            this.labelLatitude.Size = new System.Drawing.Size(62, 20);
            this.labelLatitude.Text = "Latitude:";
            // 
            // labelAltitude
            // 
            this.labelAltitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelAltitude.Location = new System.Drawing.Point(10, 95);
            this.labelAltitude.Name = "labelAltitude";
            this.labelAltitude.Size = new System.Drawing.Size(62, 17);
            this.labelAltitude.Text = "Altitude:";
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxLongitude.Location = new System.Drawing.Point(78, 68);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(81, 19);
            this.textBoxLongitude.TabIndex = 1;
            this.textBoxLongitude.TextChanged += new System.EventHandler(this.textBoxGPSData_TextChanged);
            this.textBoxLongitude.LostFocus += new System.EventHandler(this.textBoxLongitude_LostFocus);
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxLatitude.Location = new System.Drawing.Point(78, 43);
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(81, 19);
            this.textBoxLatitude.TabIndex = 2;
            this.textBoxLatitude.TextChanged += new System.EventHandler(this.textBoxGPSData_TextChanged);
            this.textBoxLatitude.LostFocus += new System.EventHandler(this.textBoxLatitude_LostFocus);
            // 
            // textBoxAltitude
            // 
            this.textBoxAltitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxAltitude.Location = new System.Drawing.Point(78, 93);
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.Size = new System.Drawing.Size(81, 19);
            this.textBoxAltitude.TabIndex = 3;
            this.textBoxAltitude.TextChanged += new System.EventHandler(this.textBoxAltitude_TextChanged);
            this.textBoxAltitude.LostFocus += new System.EventHandler(this.textBoxAltitude_LostFocus);
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(10, 118);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(88, 20);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(131, 118);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 20);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonGPS
            // 
            this.buttonGPS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonGPS.Location = new System.Drawing.Point(165, 43);
            this.buttonGPS.Name = "buttonGPS";
            this.buttonGPS.Size = new System.Drawing.Size(54, 69);
            this.buttonGPS.TabIndex = 12;
            this.buttonGPS.Text = "GPS";
            this.buttonGPS.Click += new System.EventHandler(this.buttonGPS_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Info;
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Controls.Add(this.labelCaption);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(230, 32);
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
            // labelCaption
            // 
            this.labelCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaption.Location = new System.Drawing.Point(46, 7);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(152, 20);
            this.labelCaption.Text = "New Localisation";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // NewGPSLocalisationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.ClientSize = new System.Drawing.Size(230, 150);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.buttonGPS);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxAltitude);
            this.Controls.Add(this.textBoxLatitude);
            this.Controls.Add(this.textBoxLongitude);
            this.Controls.Add(this.labelAltitude);
            this.Controls.Add(this.labelLatitude);
            this.Controls.Add(this.labelLongitude);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu1;
            this.Name = "NewGPSLocalisationForm";
            this.Text = "Diversity Mobile";
            this.Load += new System.EventHandler(this.NewGPSLocalisationForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.NewGPSLocalisationForm_Closing);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelLongitude;
        private System.Windows.Forms.Label labelLatitude;
        private System.Windows.Forms.Label labelAltitude;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.TextBox textBoxAltitude;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonGPS;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}