namespace UserSyncGui
{
    partial class RepositoryOptions
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
            this.panelRepositoryOptions = new System.Windows.Forms.Panel();
            this.groupBoxLogin = new System.Windows.Forms.GroupBox();
            this.checkBoxHidePassword = new System.Windows.Forms.CheckBox();
            this.labelHidePassword = new System.Windows.Forms.Label();
            this.labelAuthCaption = new System.Windows.Forms.Label();
            this.radioButtonWindowsAuth = new System.Windows.Forms.RadioButton();
            this.radioButtonSQLAuth = new System.Windows.Forms.RadioButton();
            this.groupBoxRepositoryConnection = new System.Windows.Forms.GroupBox();
            this.comboBoxTaxonNamesInitialCatalog = new System.Windows.Forms.ComboBox();
            this.labelTaxonNamesInitialCatalogCaption = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIPAddress = new System.Windows.Forms.TextBox();
            this.comboBoxInitialCatalog = new System.Windows.Forms.ComboBox();
            this.labelInitialCatalogCaption = new System.Windows.Forms.Label();
            this.labelPortCaption = new System.Windows.Forms.Label();
            this.labelIpAddressCaption = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelRepositoryOptions.SuspendLayout();
            this.groupBoxLogin.SuspendLayout();
            this.groupBoxRepositoryConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRepositoryOptions
            // 
            this.panelRepositoryOptions.BackColor = System.Drawing.SystemColors.Control;
            this.panelRepositoryOptions.Controls.Add(this.buttonCancel);
            this.panelRepositoryOptions.Controls.Add(this.buttonOK);
            this.panelRepositoryOptions.Controls.Add(this.groupBoxLogin);
            this.panelRepositoryOptions.Controls.Add(this.groupBoxRepositoryConnection);
            this.panelRepositoryOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRepositoryOptions.Location = new System.Drawing.Point(0, 0);
            this.panelRepositoryOptions.Name = "panelRepositoryOptions";
            this.panelRepositoryOptions.Size = new System.Drawing.Size(416, 271);
            this.panelRepositoryOptions.TabIndex = 17;
            // 
            // groupBoxLogin
            // 
            this.groupBoxLogin.Controls.Add(this.checkBoxHidePassword);
            this.groupBoxLogin.Controls.Add(this.labelHidePassword);
            this.groupBoxLogin.Controls.Add(this.labelAuthCaption);
            this.groupBoxLogin.Controls.Add(this.radioButtonWindowsAuth);
            this.groupBoxLogin.Controls.Add(this.radioButtonSQLAuth);
            this.groupBoxLogin.Location = new System.Drawing.Point(12, 12);
            this.groupBoxLogin.Name = "groupBoxLogin";
            this.groupBoxLogin.Size = new System.Drawing.Size(383, 65);
            this.groupBoxLogin.TabIndex = 16;
            this.groupBoxLogin.TabStop = false;
            this.groupBoxLogin.Text = "Login";
            // 
            // checkBoxHidePassword
            // 
            this.checkBoxHidePassword.AutoSize = true;
            this.checkBoxHidePassword.Location = new System.Drawing.Point(312, 19);
            this.checkBoxHidePassword.Name = "checkBoxHidePassword";
            this.checkBoxHidePassword.Size = new System.Drawing.Size(15, 14);
            this.checkBoxHidePassword.TabIndex = 14;
            this.checkBoxHidePassword.UseVisualStyleBackColor = true;
            // 
            // labelHidePassword
            // 
            this.labelHidePassword.AutoSize = true;
            this.labelHidePassword.Location = new System.Drawing.Point(225, 19);
            this.labelHidePassword.Name = "labelHidePassword";
            this.labelHidePassword.Size = new System.Drawing.Size(81, 13);
            this.labelHidePassword.TabIndex = 13;
            this.labelHidePassword.Text = "Hide Password:";
            // 
            // labelAuthCaption
            // 
            this.labelAuthCaption.AutoSize = true;
            this.labelAuthCaption.Location = new System.Drawing.Point(19, 19);
            this.labelAuthCaption.Name = "labelAuthCaption";
            this.labelAuthCaption.Size = new System.Drawing.Size(83, 13);
            this.labelAuthCaption.TabIndex = 12;
            this.labelAuthCaption.Text = "Authentification:";
            // 
            // radioButtonWindowsAuth
            // 
            this.radioButtonWindowsAuth.AutoSize = true;
            this.radioButtonWindowsAuth.Location = new System.Drawing.Point(119, 42);
            this.radioButtonWindowsAuth.Name = "radioButtonWindowsAuth";
            this.radioButtonWindowsAuth.Size = new System.Drawing.Size(69, 17);
            this.radioButtonWindowsAuth.TabIndex = 11;
            this.radioButtonWindowsAuth.Text = "Windows";
            this.radioButtonWindowsAuth.UseVisualStyleBackColor = true;
            // 
            // radioButtonSQLAuth
            // 
            this.radioButtonSQLAuth.AutoSize = true;
            this.radioButtonSQLAuth.Checked = true;
            this.radioButtonSQLAuth.Location = new System.Drawing.Point(119, 17);
            this.radioButtonSQLAuth.Name = "radioButtonSQLAuth";
            this.radioButtonSQLAuth.Size = new System.Drawing.Size(46, 17);
            this.radioButtonSQLAuth.TabIndex = 10;
            this.radioButtonSQLAuth.TabStop = true;
            this.radioButtonSQLAuth.Text = "SQL";
            this.radioButtonSQLAuth.UseVisualStyleBackColor = true;
            // 
            // groupBoxRepositoryConnection
            // 
            this.groupBoxRepositoryConnection.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxRepositoryConnection.Controls.Add(this.comboBoxTaxonNamesInitialCatalog);
            this.groupBoxRepositoryConnection.Controls.Add(this.labelTaxonNamesInitialCatalogCaption);
            this.groupBoxRepositoryConnection.Controls.Add(this.textBoxPort);
            this.groupBoxRepositoryConnection.Controls.Add(this.textBoxIPAddress);
            this.groupBoxRepositoryConnection.Controls.Add(this.comboBoxInitialCatalog);
            this.groupBoxRepositoryConnection.Controls.Add(this.labelInitialCatalogCaption);
            this.groupBoxRepositoryConnection.Controls.Add(this.labelPortCaption);
            this.groupBoxRepositoryConnection.Controls.Add(this.labelIpAddressCaption);
            this.groupBoxRepositoryConnection.Location = new System.Drawing.Point(12, 83);
            this.groupBoxRepositoryConnection.Name = "groupBoxRepositoryConnection";
            this.groupBoxRepositoryConnection.Size = new System.Drawing.Size(383, 131);
            this.groupBoxRepositoryConnection.TabIndex = 13;
            this.groupBoxRepositoryConnection.TabStop = false;
            this.groupBoxRepositoryConnection.Text = "Repository Connection";
            // 
            // comboBoxTaxonNamesInitialCatalog
            // 
            this.comboBoxTaxonNamesInitialCatalog.FormattingEnabled = true;
            this.comboBoxTaxonNamesInitialCatalog.Items.AddRange(new object[] {
            "DiversityMobile"});
            this.comboBoxTaxonNamesInitialCatalog.Location = new System.Drawing.Point(121, 100);
            this.comboBoxTaxonNamesInitialCatalog.Name = "comboBoxTaxonNamesInitialCatalog";
            this.comboBoxTaxonNamesInitialCatalog.Size = new System.Drawing.Size(194, 21);
            this.comboBoxTaxonNamesInitialCatalog.TabIndex = 11;
            // 
            // labelTaxonNamesInitialCatalogCaption
            // 
            this.labelTaxonNamesInitialCatalogCaption.AutoSize = true;
            this.labelTaxonNamesInitialCatalogCaption.Location = new System.Drawing.Point(19, 103);
            this.labelTaxonNamesInitialCatalogCaption.Name = "labelTaxonNamesInitialCatalogCaption";
            this.labelTaxonNamesInitialCatalogCaption.Size = new System.Drawing.Size(79, 13);
            this.labelTaxonNamesInitialCatalogCaption.TabIndex = 10;
            this.labelTaxonNamesInitialCatalogCaption.Text = "Taxon Catalog:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(121, 47);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 20);
            this.textBoxPort.TabIndex = 4;
            // 
            // textBoxIPAddress
            // 
            this.textBoxIPAddress.Location = new System.Drawing.Point(121, 21);
            this.textBoxIPAddress.Name = "textBoxIPAddress";
            this.textBoxIPAddress.Size = new System.Drawing.Size(194, 20);
            this.textBoxIPAddress.TabIndex = 3;
            // 
            // comboBoxInitialCatalog
            // 
            this.comboBoxInitialCatalog.FormattingEnabled = true;
            this.comboBoxInitialCatalog.Items.AddRange(new object[] {
            "DiversityCollection_BaseTest",
            "DiversityCollection"});
            this.comboBoxInitialCatalog.Location = new System.Drawing.Point(121, 73);
            this.comboBoxInitialCatalog.Name = "comboBoxInitialCatalog";
            this.comboBoxInitialCatalog.Size = new System.Drawing.Size(194, 21);
            this.comboBoxInitialCatalog.TabIndex = 5;
            // 
            // labelInitialCatalogCaption
            // 
            this.labelInitialCatalogCaption.AutoSize = true;
            this.labelInitialCatalogCaption.Location = new System.Drawing.Point(19, 76);
            this.labelInitialCatalogCaption.Name = "labelInitialCatalogCaption";
            this.labelInitialCatalogCaption.Size = new System.Drawing.Size(46, 13);
            this.labelInitialCatalogCaption.TabIndex = 4;
            this.labelInitialCatalogCaption.Text = "Catalog:";
            // 
            // labelPortCaption
            // 
            this.labelPortCaption.AutoSize = true;
            this.labelPortCaption.Location = new System.Drawing.Point(19, 50);
            this.labelPortCaption.Name = "labelPortCaption";
            this.labelPortCaption.Size = new System.Drawing.Size(29, 13);
            this.labelPortCaption.TabIndex = 2;
            this.labelPortCaption.Text = "Port:";
            // 
            // labelIpAddressCaption
            // 
            this.labelIpAddressCaption.AutoSize = true;
            this.labelIpAddressCaption.Location = new System.Drawing.Point(19, 24);
            this.labelIpAddressCaption.Name = "labelIpAddressCaption";
            this.labelIpAddressCaption.Size = new System.Drawing.Size(61, 13);
            this.labelIpAddressCaption.TabIndex = 0;
            this.labelIpAddressCaption.Text = "IP Address:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(17, 231);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(118, 230);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // RepositoryOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 271);
            this.Controls.Add(this.panelRepositoryOptions);
            this.Name = "RepositoryOptions";
            this.Text = "RepositoryOptions";
            this.panelRepositoryOptions.ResumeLayout(false);
            this.groupBoxLogin.ResumeLayout(false);
            this.groupBoxLogin.PerformLayout();
            this.groupBoxRepositoryConnection.ResumeLayout(false);
            this.groupBoxRepositoryConnection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRepositoryOptions;
        private System.Windows.Forms.GroupBox groupBoxLogin;
        private System.Windows.Forms.CheckBox checkBoxHidePassword;
        private System.Windows.Forms.Label labelHidePassword;
        private System.Windows.Forms.Label labelAuthCaption;
        private System.Windows.Forms.RadioButton radioButtonWindowsAuth;
        private System.Windows.Forms.RadioButton radioButtonSQLAuth;
        private System.Windows.Forms.GroupBox groupBoxRepositoryConnection;
        private System.Windows.Forms.ComboBox comboBoxTaxonNamesInitialCatalog;
        private System.Windows.Forms.Label labelTaxonNamesInitialCatalogCaption;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIPAddress;
        private System.Windows.Forms.ComboBox comboBoxInitialCatalog;
        private System.Windows.Forms.Label labelInitialCatalogCaption;
        private System.Windows.Forms.Label labelPortCaption;
        private System.Windows.Forms.Label labelIpAddressCaption;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}