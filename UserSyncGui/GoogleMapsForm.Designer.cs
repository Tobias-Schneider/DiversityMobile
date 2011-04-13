namespace UserSyncGui
{
    partial class GoogleMapsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoogleMapsForm));
            this.buttonShowMap = new System.Windows.Forms.Button();
            this.labelLatitudeCaption = new System.Windows.Forms.Label();
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.labelLongitudeCaption = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GPSOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mobileOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSaveToDevice = new System.Windows.Forms.Button();
            this.webBrowserMap = new System.Windows.Forms.WebBrowser();
            this.buttonFromMap = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.checkBoxUseDeviceDimensions = new System.Windows.Forms.CheckBox();
            this.textBoxFileDescription = new System.Windows.Forms.TextBox();
            this.labelFileDescription = new System.Windows.Forms.Label();
            this.labelFileNameCaption = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.groupBoxGoogleMap = new System.Windows.Forms.GroupBox();
            this.groupBoxMap = new System.Windows.Forms.GroupBox();
            this.groupBoxPoint1 = new System.Windows.Forms.GroupBox();
            this.textBoxNELong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNELat = new System.Windows.Forms.TextBox();
            this.groupBoxPoint2 = new System.Windows.Forms.GroupBox();
            this.textBoxSWLong = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSWLat = new System.Windows.Forms.TextBox();
            this.groupBoxPoint3 = new System.Windows.Forms.GroupBox();
            this.textBoxSELong = new System.Windows.Forms.TextBox();
            this.textBoxSELat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxNotNorthbound = new System.Windows.Forms.CheckBox();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxGoogleMap.SuspendLayout();
            this.groupBoxMap.SuspendLayout();
            this.groupBoxPoint1.SuspendLayout();
            this.groupBoxPoint2.SuspendLayout();
            this.groupBoxPoint3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonShowMap
            // 
            this.buttonShowMap.Location = new System.Drawing.Point(95, 71);
            this.buttonShowMap.Name = "buttonShowMap";
            this.buttonShowMap.Size = new System.Drawing.Size(75, 23);
            this.buttonShowMap.TabIndex = 3;
            this.buttonShowMap.Text = "--> Map";
            this.buttonShowMap.UseVisualStyleBackColor = true;
            this.buttonShowMap.Click += new System.EventHandler(this.buttonShowMap_Click);
            // 
            // labelLatitudeCaption
            // 
            this.labelLatitudeCaption.AutoSize = true;
            this.labelLatitudeCaption.Location = new System.Drawing.Point(3, 48);
            this.labelLatitudeCaption.Name = "labelLatitudeCaption";
            this.labelLatitudeCaption.Size = new System.Drawing.Size(48, 13);
            this.labelLatitudeCaption.TabIndex = 4;
            this.labelLatitudeCaption.Text = "Latitude:";
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Location = new System.Drawing.Point(66, 45);
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(104, 20);
            this.textBoxLatitude.TabIndex = 2;
            this.textBoxLatitude.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Location = new System.Drawing.Point(66, 19);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(104, 20);
            this.textBoxLongitude.TabIndex = 1;
            this.textBoxLongitude.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // labelLongitudeCaption
            // 
            this.labelLongitudeCaption.AutoSize = true;
            this.labelLongitudeCaption.Location = new System.Drawing.Point(3, 22);
            this.labelLongitudeCaption.Name = "labelLongitudeCaption";
            this.labelLongitudeCaption.Size = new System.Drawing.Size(57, 13);
            this.labelLongitudeCaption.TabIndex = 6;
            this.labelLongitudeCaption.Text = "Longitude:";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsToolStripMenuItem,
            this.googleMapToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1017, 24);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip1";
            // 
            // OptionsToolStripMenuItem
            // 
            this.OptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GPSOptionsToolStripMenuItem,
            this.mobileOptionsToolStripMenuItem});
            this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            this.OptionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.OptionsToolStripMenuItem.Text = "&Options";
            // 
            // GPSOptionsToolStripMenuItem
            // 
            this.GPSOptionsToolStripMenuItem.Name = "GPSOptionsToolStripMenuItem";
            this.GPSOptionsToolStripMenuItem.ShortcutKeyDisplayString = "Strg+G";
            this.GPSOptionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.GPSOptionsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.GPSOptionsToolStripMenuItem.Text = "&GPS Options";
            // 
            // mobileOptionsToolStripMenuItem
            // 
            this.mobileOptionsToolStripMenuItem.Name = "mobileOptionsToolStripMenuItem";
            this.mobileOptionsToolStripMenuItem.ShortcutKeyDisplayString = "Strg+D";
            this.mobileOptionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mobileOptionsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.mobileOptionsToolStripMenuItem.Text = "&Device Options";
            this.mobileOptionsToolStripMenuItem.Click += new System.EventHandler(this.mobileOptionsToolStripMenuItem_Click);
            // 
            // googleMapToolStripMenuItem
            // 
            this.googleMapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveToDeviceToolStripMenuItem,
            this.showToolStripMenuItem,
            this.openMapToolStripMenuItem});
            this.googleMapToolStripMenuItem.Name = "googleMapToolStripMenuItem";
            this.googleMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.googleMapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.googleMapToolStripMenuItem.Text = "&Map";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Strg+S";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // saveToDeviceToolStripMenuItem
            // 
            this.saveToDeviceToolStripMenuItem.Name = "saveToDeviceToolStripMenuItem";
            this.saveToDeviceToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Shift+S";
            this.saveToDeviceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveToDeviceToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.saveToDeviceToolStripMenuItem.Text = "&Save to Device";
            this.saveToDeviceToolStripMenuItem.Click += new System.EventHandler(this.buttonSaveToDevice_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.ShortcutKeyDisplayString = "Strg+H";
            this.showToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.showToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.showToolStripMenuItem.Text = "S&how GoogleMap";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.buttonShowMap_Click);
            // 
            // openMapToolStripMenuItem
            // 
            this.openMapToolStripMenuItem.Name = "openMapToolStripMenuItem";
            this.openMapToolStripMenuItem.ShortcutKeyDisplayString = "Strg+O";
            this.openMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMapToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.openMapToolStripMenuItem.Text = "&Open Map";
            this.openMapToolStripMenuItem.Click += new System.EventHandler(this.openMapToolStripMenuItem_Click);
            // 
            // buttonSaveToDevice
            // 
            this.buttonSaveToDevice.Location = new System.Drawing.Point(20, 203);
            this.buttonSaveToDevice.Name = "buttonSaveToDevice";
            this.buttonSaveToDevice.Size = new System.Drawing.Size(109, 23);
            this.buttonSaveToDevice.TabIndex = 16;
            this.buttonSaveToDevice.Text = "Save To Device";
            this.buttonSaveToDevice.UseVisualStyleBackColor = true;
            this.buttonSaveToDevice.Click += new System.EventHandler(this.buttonSaveToDevice_Click);
            // 
            // webBrowserMap
            // 
            this.webBrowserMap.Location = new System.Drawing.Point(202, 27);
            this.webBrowserMap.MaximumSize = new System.Drawing.Size(800, 800);
            this.webBrowserMap.MinimumSize = new System.Drawing.Size(800, 800);
            this.webBrowserMap.Name = "webBrowserMap";
            this.webBrowserMap.ScrollBarsEnabled = false;
            this.webBrowserMap.Size = new System.Drawing.Size(800, 800);
            this.webBrowserMap.TabIndex = 10;
            this.webBrowserMap.TabStop = false;
            // 
            // buttonFromMap
            // 
            this.buttonFromMap.Location = new System.Drawing.Point(95, 102);
            this.buttonFromMap.Name = "buttonFromMap";
            this.buttonFromMap.Size = new System.Drawing.Size(75, 23);
            this.buttonFromMap.TabIndex = 4;
            this.buttonFromMap.Text = "<-- Map";
            this.buttonFromMap.UseVisualStyleBackColor = true;
            this.buttonFromMap.Click += new System.EventHandler(this.buttonFromMap_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(20, 175);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(109, 23);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Controls.Add(this.checkBoxUseDeviceDimensions);
            this.groupBoxFile.Controls.Add(this.textBoxFileDescription);
            this.groupBoxFile.Controls.Add(this.labelFileDescription);
            this.groupBoxFile.Controls.Add(this.labelFileNameCaption);
            this.groupBoxFile.Controls.Add(this.textBoxFileName);
            this.groupBoxFile.Controls.Add(this.buttonSave);
            this.groupBoxFile.Controls.Add(this.buttonSaveToDevice);
            this.groupBoxFile.Location = new System.Drawing.Point(8, 440);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(188, 239);
            this.groupBoxFile.TabIndex = 13;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "File";
            // 
            // checkBoxUseDeviceDimensions
            // 
            this.checkBoxUseDeviceDimensions.AutoSize = true;
            this.checkBoxUseDeviceDimensions.Location = new System.Drawing.Point(20, 153);
            this.checkBoxUseDeviceDimensions.Name = "checkBoxUseDeviceDimensions";
            this.checkBoxUseDeviceDimensions.Size = new System.Drawing.Size(135, 17);
            this.checkBoxUseDeviceDimensions.TabIndex = 14;
            this.checkBoxUseDeviceDimensions.Text = "Use device dimensions";
            this.checkBoxUseDeviceDimensions.UseVisualStyleBackColor = true;
            // 
            // textBoxFileDescription
            // 
            this.textBoxFileDescription.Location = new System.Drawing.Point(20, 82);
            this.textBoxFileDescription.Multiline = true;
            this.textBoxFileDescription.Name = "textBoxFileDescription";
            this.textBoxFileDescription.Size = new System.Drawing.Size(159, 53);
            this.textBoxFileDescription.TabIndex = 13;
            // 
            // labelFileDescription
            // 
            this.labelFileDescription.AutoSize = true;
            this.labelFileDescription.Location = new System.Drawing.Point(17, 66);
            this.labelFileDescription.Name = "labelFileDescription";
            this.labelFileDescription.Size = new System.Drawing.Size(63, 13);
            this.labelFileDescription.TabIndex = 15;
            this.labelFileDescription.Text = "Description:";
            // 
            // labelFileNameCaption
            // 
            this.labelFileNameCaption.AutoSize = true;
            this.labelFileNameCaption.Location = new System.Drawing.Point(17, 21);
            this.labelFileNameCaption.Name = "labelFileNameCaption";
            this.labelFileNameCaption.Size = new System.Drawing.Size(57, 13);
            this.labelFileNameCaption.TabIndex = 14;
            this.labelFileNameCaption.Text = "File Name:";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(18, 37);
            this.textBoxFileName.MaxLength = 50;
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.ShortcutsEnabled = false;
            this.textBoxFileName.Size = new System.Drawing.Size(161, 20);
            this.textBoxFileName.TabIndex = 12;
            this.textBoxFileName.WordWrap = false;
            this.textBoxFileName.TextChanged += new System.EventHandler(this.textBoxFileName_TextChanged);
            // 
            // groupBoxGoogleMap
            // 
            this.groupBoxGoogleMap.Controls.Add(this.buttonShowMap);
            this.groupBoxGoogleMap.Controls.Add(this.buttonFromMap);
            this.groupBoxGoogleMap.Controls.Add(this.textBoxLongitude);
            this.groupBoxGoogleMap.Controls.Add(this.labelLatitudeCaption);
            this.groupBoxGoogleMap.Controls.Add(this.labelLongitudeCaption);
            this.groupBoxGoogleMap.Controls.Add(this.textBoxLatitude);
            this.groupBoxGoogleMap.Location = new System.Drawing.Point(8, 38);
            this.groupBoxGoogleMap.Name = "groupBoxGoogleMap";
            this.groupBoxGoogleMap.Size = new System.Drawing.Size(188, 131);
            this.groupBoxGoogleMap.TabIndex = 14;
            this.groupBoxGoogleMap.TabStop = false;
            this.groupBoxGoogleMap.Text = "Google Map";
            // 
            // groupBoxMap
            // 
            this.groupBoxMap.Controls.Add(this.groupBoxPoint1);
            this.groupBoxMap.Controls.Add(this.groupBoxPoint2);
            this.groupBoxMap.Controls.Add(this.groupBoxPoint3);
            this.groupBoxMap.Controls.Add(this.checkBoxNotNorthbound);
            this.groupBoxMap.Location = new System.Drawing.Point(8, 175);
            this.groupBoxMap.Name = "groupBoxMap";
            this.groupBoxMap.Size = new System.Drawing.Size(188, 259);
            this.groupBoxMap.TabIndex = 15;
            this.groupBoxMap.TabStop = false;
            this.groupBoxMap.Text = "Map";
            // 
            // groupBoxPoint1
            // 
            this.groupBoxPoint1.Controls.Add(this.textBoxNELong);
            this.groupBoxPoint1.Controls.Add(this.label3);
            this.groupBoxPoint1.Controls.Add(this.label4);
            this.groupBoxPoint1.Controls.Add(this.textBoxNELat);
            this.groupBoxPoint1.Location = new System.Drawing.Point(0, 30);
            this.groupBoxPoint1.Name = "groupBoxPoint1";
            this.groupBoxPoint1.Size = new System.Drawing.Size(188, 70);
            this.groupBoxPoint1.TabIndex = 19;
            this.groupBoxPoint1.TabStop = false;
            this.groupBoxPoint1.Text = "1. Point";
            // 
            // textBoxNELong
            // 
            this.textBoxNELong.Location = new System.Drawing.Point(64, 19);
            this.textBoxNELong.Name = "textBoxNELong";
            this.textBoxNELong.Size = new System.Drawing.Size(118, 20);
            this.textBoxNELong.TabIndex = 6;
            this.textBoxNELong.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "NE Lat:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "NE Long:";
            // 
            // textBoxNELat
            // 
            this.textBoxNELat.AcceptsReturn = true;
            this.textBoxNELat.Location = new System.Drawing.Point(64, 45);
            this.textBoxNELat.Name = "textBoxNELat";
            this.textBoxNELat.Size = new System.Drawing.Size(118, 20);
            this.textBoxNELat.TabIndex = 7;
            this.textBoxNELat.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // groupBoxPoint2
            // 
            this.groupBoxPoint2.Controls.Add(this.textBoxSWLong);
            this.groupBoxPoint2.Controls.Add(this.label1);
            this.groupBoxPoint2.Controls.Add(this.label2);
            this.groupBoxPoint2.Controls.Add(this.textBoxSWLat);
            this.groupBoxPoint2.Location = new System.Drawing.Point(0, 104);
            this.groupBoxPoint2.Name = "groupBoxPoint2";
            this.groupBoxPoint2.Size = new System.Drawing.Size(188, 75);
            this.groupBoxPoint2.TabIndex = 18;
            this.groupBoxPoint2.TabStop = false;
            this.groupBoxPoint2.Text = "2. Point";
            // 
            // textBoxSWLong
            // 
            this.textBoxSWLong.Location = new System.Drawing.Point(63, 19);
            this.textBoxSWLong.Name = "textBoxSWLong";
            this.textBoxSWLong.Size = new System.Drawing.Size(119, 20);
            this.textBoxSWLong.TabIndex = 8;
            this.textBoxSWLong.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "SW Lat:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "SW Long:";
            // 
            // textBoxSWLat
            // 
            this.textBoxSWLat.Location = new System.Drawing.Point(63, 45);
            this.textBoxSWLat.Name = "textBoxSWLat";
            this.textBoxSWLat.Size = new System.Drawing.Size(119, 20);
            this.textBoxSWLat.TabIndex = 9;
            this.textBoxSWLat.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // groupBoxPoint3
            // 
            this.groupBoxPoint3.Controls.Add(this.textBoxSELong);
            this.groupBoxPoint3.Controls.Add(this.textBoxSELat);
            this.groupBoxPoint3.Controls.Add(this.label6);
            this.groupBoxPoint3.Controls.Add(this.label5);
            this.groupBoxPoint3.Enabled = false;
            this.groupBoxPoint3.Location = new System.Drawing.Point(0, 182);
            this.groupBoxPoint3.Name = "groupBoxPoint3";
            this.groupBoxPoint3.Size = new System.Drawing.Size(188, 75);
            this.groupBoxPoint3.TabIndex = 17;
            this.groupBoxPoint3.TabStop = false;
            this.groupBoxPoint3.Text = "3. Point";
            // 
            // textBoxSELong
            // 
            this.textBoxSELong.Enabled = false;
            this.textBoxSELong.Location = new System.Drawing.Point(64, 19);
            this.textBoxSELong.Name = "textBoxSELong";
            this.textBoxSELong.Size = new System.Drawing.Size(118, 20);
            this.textBoxSELong.TabIndex = 10;
            this.textBoxSELong.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxSELat
            // 
            this.textBoxSELat.Enabled = false;
            this.textBoxSELat.Location = new System.Drawing.Point(64, 45);
            this.textBoxSELat.Name = "textBoxSELat";
            this.textBoxSELat.Size = new System.Drawing.Size(118, 20);
            this.textBoxSELat.TabIndex = 11;
            this.textBoxSELat.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "SE Long:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "SE Lat:";
            // 
            // checkBoxNotNorthbound
            // 
            this.checkBoxNotNorthbound.AutoSize = true;
            this.checkBoxNotNorthbound.Location = new System.Drawing.Point(53, 10);
            this.checkBoxNotNorthbound.Name = "checkBoxNotNorthbound";
            this.checkBoxNotNorthbound.Size = new System.Drawing.Size(128, 17);
            this.checkBoxNotNorthbound.TabIndex = 5;
            this.checkBoxNotNorthbound.Text = "Map isn\'t northbound ";
            this.checkBoxNotNorthbound.UseVisualStyleBackColor = true;
            this.checkBoxNotNorthbound.CheckedChanged += new System.EventHandler(this.checkBoxNotNorthbound_CheckedChanged);
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxMap.ErrorImage = null;
            this.pictureBoxMap.InitialImage = null;
            this.pictureBoxMap.Location = new System.Drawing.Point(202, 27);
            this.pictureBoxMap.MaximumSize = new System.Drawing.Size(800, 800);
            this.pictureBoxMap.MinimumSize = new System.Drawing.Size(800, 800);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(800, 800);
            this.pictureBoxMap.TabIndex = 16;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.Visible = false;
            this.pictureBoxMap.WaitOnLoad = true;
            this.pictureBoxMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_MouseDown);
            // 
            // GoogleMapsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.ClientSize = new System.Drawing.Size(1034, 692);
            this.Controls.Add(this.pictureBoxMap);
            this.Controls.Add(this.groupBoxMap);
            this.Controls.Add(this.groupBoxGoogleMap);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.webBrowserMap);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "GoogleMapsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxFile.PerformLayout();
            this.groupBoxGoogleMap.ResumeLayout(false);
            this.groupBoxGoogleMap.PerformLayout();
            this.groupBoxMap.ResumeLayout(false);
            this.groupBoxMap.PerformLayout();
            this.groupBoxPoint1.ResumeLayout(false);
            this.groupBoxPoint1.PerformLayout();
            this.groupBoxPoint2.ResumeLayout(false);
            this.groupBoxPoint2.PerformLayout();
            this.groupBoxPoint3.ResumeLayout(false);
            this.groupBoxPoint3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonShowMap;
        private System.Windows.Forms.Label labelLatitudeCaption;
        private System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.Label labelLongitudeCaption;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GPSOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mobileOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToDeviceToolStripMenuItem;
        private System.Windows.Forms.Button buttonSaveToDevice;
        private System.Windows.Forms.WebBrowser webBrowserMap;
        private System.Windows.Forms.Button buttonFromMap;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.TextBox textBoxFileDescription;
        private System.Windows.Forms.Label labelFileDescription;
        private System.Windows.Forms.Label labelFileNameCaption;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.CheckBox checkBoxUseDeviceDimensions;
        private System.Windows.Forms.GroupBox groupBoxGoogleMap;
        private System.Windows.Forms.GroupBox groupBoxMap;
        private System.Windows.Forms.CheckBox checkBoxNotNorthbound;
        private System.Windows.Forms.TextBox textBoxSELong;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSELat;
        private System.Windows.Forms.GroupBox groupBoxPoint3;
        private System.Windows.Forms.GroupBox groupBoxPoint1;
        private System.Windows.Forms.TextBox textBoxNELong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNELat;
        private System.Windows.Forms.GroupBox groupBoxPoint2;
        private System.Windows.Forms.TextBox textBoxSWLong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSWLat;
        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.ToolStripMenuItem openMapToolStripMenuItem;
    }
}