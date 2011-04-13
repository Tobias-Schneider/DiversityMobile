namespace UBT.AI4.Bio.DivMobi.SyncGui
{
    partial class TaxPanel
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelRepositoryConnection = new System.Windows.Forms.Panel();
            this.buttonConnectRepository = new System.Windows.Forms.Button();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelHeading = new System.Windows.Forms.Label();
            this.panelTaxDBConnection = new System.Windows.Forms.Panel();
            this.buttonConnectMobile = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelMobileConnection = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelDB = new System.Windows.Forms.Label();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.panelRepositoryConnection.SuspendLayout();
            this.panelTaxDBConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRepositoryConnection
            // 
            this.panelRepositoryConnection.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelRepositoryConnection.Controls.Add(this.textBoxDBName);
            this.panelRepositoryConnection.Controls.Add(this.labelDB);
            this.panelRepositoryConnection.Controls.Add(this.buttonConnectRepository);
            this.panelRepositoryConnection.Controls.Add(this.labelPassword);
            this.panelRepositoryConnection.Controls.Add(this.textBoxPassword);
            this.panelRepositoryConnection.Controls.Add(this.textBoxUser);
            this.panelRepositoryConnection.Controls.Add(this.labelUser);
            this.panelRepositoryConnection.Controls.Add(this.textBoxPort);
            this.panelRepositoryConnection.Controls.Add(this.labelPort);
            this.panelRepositoryConnection.Controls.Add(this.textBoxIP);
            this.panelRepositoryConnection.Controls.Add(this.labelIP);
            this.panelRepositoryConnection.Controls.Add(this.labelHeading);
            this.panelRepositoryConnection.Location = new System.Drawing.Point(28, 30);
            this.panelRepositoryConnection.Name = "panelRepositoryConnection";
            this.panelRepositoryConnection.Size = new System.Drawing.Size(252, 370);
            this.panelRepositoryConnection.TabIndex = 0;
            // 
            // buttonConnectRepository
            // 
            this.buttonConnectRepository.Location = new System.Drawing.Point(153, 307);
            this.buttonConnectRepository.Name = "buttonConnectRepository";
            this.buttonConnectRepository.Size = new System.Drawing.Size(75, 23);
            this.buttonConnectRepository.TabIndex = 9;
            this.buttonConnectRepository.Text = "Connect";
            this.buttonConnectRepository.UseVisualStyleBackColor = true;
            this.buttonConnectRepository.Click += new System.EventHandler(this.buttonConnectRepository_Click);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(27, 263);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 8;
            this.labelPassword.Text = "Password:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(106, 260);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(122, 20);
            this.textBoxPassword.TabIndex = 7;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(106, 223);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(122, 20);
            this.textBoxUser.TabIndex = 6;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(25, 226);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(58, 13);
            this.labelUser.TabIndex = 5;
            this.labelUser.Text = "Username:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxPort.Enabled = false;
            this.textBoxPort.Location = new System.Drawing.Point(106, 107);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(122, 20);
            this.textBoxPort.TabIndex = 4;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(27, 110);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 3;
            this.labelPort.Text = "Port";
            // 
            // textBoxIP
            // 
            this.textBoxIP.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxIP.Enabled = false;
            this.textBoxIP.Location = new System.Drawing.Point(106, 62);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(122, 20);
            this.textBoxIP.TabIndex = 2;
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(27, 65);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(61, 13);
            this.labelIP.TabIndex = 1;
            this.labelIP.Text = "IP-Address:";
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.Location = new System.Drawing.Point(70, 28);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(111, 13);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "RepositoryConnection";
            // 
            // panelTaxDBConnection
            // 
            this.panelTaxDBConnection.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelTaxDBConnection.Controls.Add(this.buttonConnectMobile);
            this.panelTaxDBConnection.Controls.Add(this.buttonSelect);
            this.panelTaxDBConnection.Controls.Add(this.textBoxPath);
            this.panelTaxDBConnection.Controls.Add(this.labelPath);
            this.panelTaxDBConnection.Controls.Add(this.labelMobileConnection);
            this.panelTaxDBConnection.Location = new System.Drawing.Point(28, 432);
            this.panelTaxDBConnection.Name = "panelTaxDBConnection";
            this.panelTaxDBConnection.Size = new System.Drawing.Size(252, 149);
            this.panelTaxDBConnection.TabIndex = 1;
            // 
            // buttonConnectMobile
            // 
            this.buttonConnectMobile.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonConnectMobile.Enabled = false;
            this.buttonConnectMobile.Location = new System.Drawing.Point(125, 101);
            this.buttonConnectMobile.Name = "buttonConnectMobile";
            this.buttonConnectMobile.Size = new System.Drawing.Size(103, 23);
            this.buttonConnectMobile.TabIndex = 4;
            this.buttonConnectMobile.Text = "<Select DB First>";
            this.buttonConnectMobile.UseVisualStyleBackColor = false;
            this.buttonConnectMobile.Click += new System.EventHandler(this.buttonConnectTaxDB_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSelect.Location = new System.Drawing.Point(33, 101);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonSelect.TabIndex = 3;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxPath.Enabled = false;
            this.textBoxPath.Location = new System.Drawing.Point(106, 56);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(122, 20);
            this.textBoxPath.TabIndex = 2;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(30, 59);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(32, 13);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Path:";
            // 
            // labelMobileConnection
            // 
            this.labelMobileConnection.AutoSize = true;
            this.labelMobileConnection.Location = new System.Drawing.Point(70, 18);
            this.labelMobileConnection.Name = "labelMobileConnection";
            this.labelMobileConnection.Size = new System.Drawing.Size(92, 13);
            this.labelMobileConnection.TabIndex = 0;
            this.labelMobileConnection.Text = "MobileConnection";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonStart.Enabled = false;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonStart.Location = new System.Drawing.Point(28, 622);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(252, 54);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelDB
            // 
            this.labelDB.AutoSize = true;
            this.labelDB.Location = new System.Drawing.Point(27, 151);
            this.labelDB.Name = "labelDB";
            this.labelDB.Size = new System.Drawing.Size(50, 13);
            this.labelDB.TabIndex = 10;
            this.labelDB.Text = "DBName";
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBoxDBName.Enabled = false;
            this.textBoxDBName.Location = new System.Drawing.Point(106, 148);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(122, 20);
            this.textBoxDBName.TabIndex = 11;
            // 
            // TaxPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;//Manuell
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.panelTaxDBConnection);
            this.Controls.Add(this.panelRepositoryConnection);
            this.Name = "TaxPanel";
            this.Size = new System.Drawing.Size(1507, 974);
            this.panelRepositoryConnection.ResumeLayout(false);
            this.panelRepositoryConnection.PerformLayout();
            this.panelTaxDBConnection.ResumeLayout(false);
            this.panelTaxDBConnection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRepositoryConnection;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Button buttonConnectRepository;
        private System.Windows.Forms.Panel panelTaxDBConnection;
        private System.Windows.Forms.Label labelMobileConnection;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonConnectMobile;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelDB;
        private System.Windows.Forms.TextBox textBoxDBName;
    }
}
