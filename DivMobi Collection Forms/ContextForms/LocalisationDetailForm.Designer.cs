namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    partial class LocalisationDetailForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalisationDetailForm));
            this.labelCaption = new System.Windows.Forms.Label();
            this.textBoxLocation1 = new System.Windows.Forms.TextBox();
            this.labelLocation1Caption = new System.Windows.Forms.Label();
            this.textBoxLocation2 = new System.Windows.Forms.TextBox();
            this.labelLocation2Caption = new System.Windows.Forms.Label();
            this.textBoxLocAccuracy = new System.Windows.Forms.TextBox();
            this.labelLocAccuracyCaption = new System.Windows.Forms.Label();
            this.buttonNewDate = new System.Windows.Forms.Button();
            this.dateTimePickerDeterminationDate = new System.Windows.Forms.DateTimePicker();
            this.labelDateCaption = new System.Windows.Forms.Label();
            this.textBoxResponsibleName = new System.Windows.Forms.TextBox();
            this.labelResponsibleNameCaption = new System.Windows.Forms.Label();
            this.buttonShowMap = new System.Windows.Forms.Button();
            this.panelGPS = new System.Windows.Forms.Panel();
            this.labelLongitude = new System.Windows.Forms.Label();
            this.labelLongCaption = new System.Windows.Forms.Label();
            this.labelAltitude = new System.Windows.Forms.Label();
            this.labelLatitude = new System.Windows.Forms.Label();
            this.labelAltCaption = new System.Windows.Forms.Label();
            this.labelLatCaption = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panelGPS.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCaption
            // 
            this.labelCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaption.Location = new System.Drawing.Point(54, 6);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(150, 20);
            this.labelCaption.Text = "Localisation Details";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxLocation1
            // 
            this.textBoxLocation1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxLocation1.Location = new System.Drawing.Point(72, 41);
            this.textBoxLocation1.MaxLength = 255;
            this.textBoxLocation1.Name = "textBoxLocation1";
            this.textBoxLocation1.Size = new System.Drawing.Size(148, 19);
            this.textBoxLocation1.TabIndex = 1;
            this.textBoxLocation1.TextChanged += new System.EventHandler(this.textBoxLocation_TextChanged);
            // 
            // labelLocation1Caption
            // 
            this.labelLocation1Caption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelLocation1Caption.Location = new System.Drawing.Point(10, 41);
            this.labelLocation1Caption.Name = "labelLocation1Caption";
            this.labelLocation1Caption.Size = new System.Drawing.Size(58, 20);
            this.labelLocation1Caption.Text = "Location 1:";
            // 
            // textBoxLocation2
            // 
            this.textBoxLocation2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxLocation2.Location = new System.Drawing.Point(72, 66);
            this.textBoxLocation2.MaxLength = 255;
            this.textBoxLocation2.Name = "textBoxLocation2";
            this.textBoxLocation2.Size = new System.Drawing.Size(148, 19);
            this.textBoxLocation2.TabIndex = 2;
            this.textBoxLocation2.TextChanged += new System.EventHandler(this.textBoxLocation_TextChanged);
            // 
            // labelLocation2Caption
            // 
            this.labelLocation2Caption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelLocation2Caption.Location = new System.Drawing.Point(10, 66);
            this.labelLocation2Caption.Name = "labelLocation2Caption";
            this.labelLocation2Caption.Size = new System.Drawing.Size(58, 20);
            this.labelLocation2Caption.Text = "Location 2:";
            // 
            // textBoxLocAccuracy
            // 
            this.textBoxLocAccuracy.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxLocAccuracy.Location = new System.Drawing.Point(72, 91);
            this.textBoxLocAccuracy.MaxLength = 50;
            this.textBoxLocAccuracy.Name = "textBoxLocAccuracy";
            this.textBoxLocAccuracy.Size = new System.Drawing.Size(148, 19);
            this.textBoxLocAccuracy.TabIndex = 3;
            // 
            // labelLocAccuracyCaption
            // 
            this.labelLocAccuracyCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelLocAccuracyCaption.Location = new System.Drawing.Point(10, 91);
            this.labelLocAccuracyCaption.Name = "labelLocAccuracyCaption";
            this.labelLocAccuracyCaption.Size = new System.Drawing.Size(58, 20);
            this.labelLocAccuracyCaption.Text = "Accuracy:";
            // 
            // buttonNewDate
            // 
            this.buttonNewDate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonNewDate.Location = new System.Drawing.Point(178, 125);
            this.buttonNewDate.Name = "buttonNewDate";
            this.buttonNewDate.Size = new System.Drawing.Size(42, 22);
            this.buttonNewDate.TabIndex = 4;
            this.buttonNewDate.Text = "Edit";
            this.buttonNewDate.Click += new System.EventHandler(this.buttonNewDate_Click);
            // 
            // dateTimePickerDeterminationDate
            // 
            this.dateTimePickerDeterminationDate.CalendarFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dateTimePickerDeterminationDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.dateTimePickerDeterminationDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDeterminationDate.Location = new System.Drawing.Point(55, 122);
            this.dateTimePickerDeterminationDate.Name = "dateTimePickerDeterminationDate";
            this.dateTimePickerDeterminationDate.Size = new System.Drawing.Size(117, 25);
            this.dateTimePickerDeterminationDate.TabIndex = 4;
            this.dateTimePickerDeterminationDate.Visible = false;
            // 
            // labelDateCaption
            // 
            this.labelDateCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelDateCaption.Location = new System.Drawing.Point(11, 127);
            this.labelDateCaption.Name = "labelDateCaption";
            this.labelDateCaption.Size = new System.Drawing.Size(38, 20);
            this.labelDateCaption.Text = "Date:";
            // 
            // textBoxResponsibleName
            // 
            this.textBoxResponsibleName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxResponsibleName.Location = new System.Drawing.Point(72, 158);
            this.textBoxResponsibleName.MaxLength = 255;
            this.textBoxResponsibleName.Name = "textBoxResponsibleName";
            this.textBoxResponsibleName.Size = new System.Drawing.Size(148, 19);
            this.textBoxResponsibleName.TabIndex = 5;
            // 
            // labelResponsibleNameCaption
            // 
            this.labelResponsibleNameCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelResponsibleNameCaption.Location = new System.Drawing.Point(11, 157);
            this.labelResponsibleNameCaption.Name = "labelResponsibleNameCaption";
            this.labelResponsibleNameCaption.Size = new System.Drawing.Size(58, 20);
            this.labelResponsibleNameCaption.Text = "Respons.:";
            // 
            // buttonShowMap
            // 
            this.buttonShowMap.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonShowMap.Location = new System.Drawing.Point(148, 194);
            this.buttonShowMap.Name = "buttonShowMap";
            this.buttonShowMap.Size = new System.Drawing.Size(72, 40);
            this.buttonShowMap.TabIndex = 6;
            this.buttonShowMap.Text = "Show Map";
            this.buttonShowMap.Click += new System.EventHandler(this.buttonShowMap_Click);
            // 
            // panelGPS
            // 
            this.panelGPS.BackColor = System.Drawing.SystemColors.Info;
            this.panelGPS.Controls.Add(this.labelLongitude);
            this.panelGPS.Controls.Add(this.labelLongCaption);
            this.panelGPS.Controls.Add(this.labelAltitude);
            this.panelGPS.Controls.Add(this.labelLatitude);
            this.panelGPS.Controls.Add(this.labelAltCaption);
            this.panelGPS.Controls.Add(this.labelLatCaption);
            this.panelGPS.Location = new System.Drawing.Point(11, 189);
            this.panelGPS.Name = "panelGPS";
            this.panelGPS.Size = new System.Drawing.Size(131, 65);
            // 
            // labelLongitude
            // 
            this.labelLongitude.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.labelLongitude.Location = new System.Drawing.Point(43, 28);
            this.labelLongitude.Name = "labelLongitude";
            this.labelLongitude.Size = new System.Drawing.Size(84, 12);
            // 
            // labelLongCaption
            // 
            this.labelLongCaption.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.labelLongCaption.Location = new System.Drawing.Point(6, 28);
            this.labelLongCaption.Name = "labelLongCaption";
            this.labelLongCaption.Size = new System.Drawing.Size(31, 12);
            this.labelLongCaption.Text = "Long:";
            // 
            // labelAltitude
            // 
            this.labelAltitude.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.labelAltitude.Location = new System.Drawing.Point(44, 47);
            this.labelAltitude.Name = "labelAltitude";
            this.labelAltitude.Size = new System.Drawing.Size(84, 12);
            // 
            // labelLatitude
            // 
            this.labelLatitude.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.labelLatitude.Location = new System.Drawing.Point(44, 7);
            this.labelLatitude.Name = "labelLatitude";
            this.labelLatitude.Size = new System.Drawing.Size(84, 12);
            // 
            // labelAltCaption
            // 
            this.labelAltCaption.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.labelAltCaption.Location = new System.Drawing.Point(7, 47);
            this.labelAltCaption.Name = "labelAltCaption";
            this.labelAltCaption.Size = new System.Drawing.Size(31, 12);
            this.labelAltCaption.Text = "Alt:";
            // 
            // labelLatCaption
            // 
            this.labelLatCaption.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.labelLatCaption.Location = new System.Drawing.Point(7, 9);
            this.labelLatCaption.Name = "labelLatCaption";
            this.labelLatCaption.Size = new System.Drawing.Size(31, 12);
            this.labelLatCaption.Text = "Lat:";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Controls.Add(this.labelCaption);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(227, 32);
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
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(128, 260);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 20);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(10, 260);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(92, 20);
            this.buttonOk.TabIndex = 12;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // LocalisationDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelGPS);
            this.Controls.Add(this.buttonShowMap);
            this.Controls.Add(this.textBoxResponsibleName);
            this.Controls.Add(this.labelResponsibleNameCaption);
            this.Controls.Add(this.buttonNewDate);
            this.Controls.Add(this.dateTimePickerDeterminationDate);
            this.Controls.Add(this.labelDateCaption);
            this.Controls.Add(this.textBoxLocAccuracy);
            this.Controls.Add(this.labelLocAccuracyCaption);
            this.Controls.Add(this.textBoxLocation2);
            this.Controls.Add(this.labelLocation2Caption);
            this.Controls.Add(this.textBoxLocation1);
            this.Controls.Add(this.labelLocation1Caption);
            this.Menu = this.mainMenu1;
            this.Name = "LocalisationDetailForm";
            this.Text = "Diversity Mobile";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.EventLocalisationForm_Closing);
            this.panelGPS.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.TextBox textBoxLocation1;
        private System.Windows.Forms.Label labelLocation1Caption;
        private System.Windows.Forms.TextBox textBoxLocation2;
        private System.Windows.Forms.Label labelLocation2Caption;
        private System.Windows.Forms.TextBox textBoxLocAccuracy;
        private System.Windows.Forms.Label labelLocAccuracyCaption;
        private System.Windows.Forms.Button buttonNewDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerDeterminationDate;
        private System.Windows.Forms.Label labelDateCaption;
        private System.Windows.Forms.TextBox textBoxResponsibleName;
        private System.Windows.Forms.Label labelResponsibleNameCaption;
        private System.Windows.Forms.Button buttonShowMap;
        private System.Windows.Forms.Panel panelGPS;
        private System.Windows.Forms.Label labelAltCaption;
        private System.Windows.Forms.Label labelLatCaption;
        private System.Windows.Forms.Label labelAltitude;
        private System.Windows.Forms.Label labelLatitude;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelLongitude;
        private System.Windows.Forms.Label labelLongCaption;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}