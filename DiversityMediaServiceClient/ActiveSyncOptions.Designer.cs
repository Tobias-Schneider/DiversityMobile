namespace UserSyncGui
{
    partial class ActiveSyncOptions
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
            this.groupBoxActiveSync = new System.Windows.Forms.GroupBox();
            this.checkBoxUseDevice = new System.Windows.Forms.CheckBox();
            this.textBoxTaxonNamesPath = new System.Windows.Forms.TextBox();
            this.textBoxDivMobiPath = new System.Windows.Forms.TextBox();
            this.textBoxTimeOut = new System.Windows.Forms.TextBox();
            this.labelTimeOutCaption = new System.Windows.Forms.Label();
            this.labelTaxonNamesCaption = new System.Windows.Forms.Label();
            this.labelPathDivMobiCaption = new System.Windows.Forms.Label();
            this.panelActiveSyncOptions = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.groupBoxActiveSync.SuspendLayout();
            this.panelActiveSyncOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxActiveSync
            // 
            this.groupBoxActiveSync.Controls.Add(this.buttonTest);
            this.groupBoxActiveSync.Controls.Add(this.checkBoxUseDevice);
            this.groupBoxActiveSync.Controls.Add(this.textBoxTaxonNamesPath);
            this.groupBoxActiveSync.Controls.Add(this.textBoxDivMobiPath);
            this.groupBoxActiveSync.Controls.Add(this.textBoxTimeOut);
            this.groupBoxActiveSync.Controls.Add(this.labelTimeOutCaption);
            this.groupBoxActiveSync.Controls.Add(this.labelTaxonNamesCaption);
            this.groupBoxActiveSync.Controls.Add(this.labelPathDivMobiCaption);
            this.groupBoxActiveSync.Location = new System.Drawing.Point(15, 49);
            this.groupBoxActiveSync.Name = "groupBoxActiveSync";
            this.groupBoxActiveSync.Size = new System.Drawing.Size(383, 162);
            this.groupBoxActiveSync.TabIndex = 14;
            this.groupBoxActiveSync.TabStop = false;
            this.groupBoxActiveSync.Text = "Active Sync";
            // 
            // checkBoxUseDevice
            // 
            this.checkBoxUseDevice.AutoSize = true;
            this.checkBoxUseDevice.Location = new System.Drawing.Point(23, 31);
            this.checkBoxUseDevice.Name = "checkBoxUseDevice";
            this.checkBoxUseDevice.Size = new System.Drawing.Size(143, 17);
            this.checkBoxUseDevice.TabIndex = 16;
            this.checkBoxUseDevice.Text = "Connect to Mobil Device";
            this.checkBoxUseDevice.UseVisualStyleBackColor = true;
            this.checkBoxUseDevice.CheckedChanged += new System.EventHandler(this.checkBoxUseDevice_CheckedChanged);
            // 
            // textBoxTaxonNamesPath
            // 
            this.textBoxTaxonNamesPath.Location = new System.Drawing.Point(121, 129);
            this.textBoxTaxonNamesPath.Name = "textBoxTaxonNamesPath";
            this.textBoxTaxonNamesPath.Size = new System.Drawing.Size(194, 20);
            this.textBoxTaxonNamesPath.TabIndex = 14;
            // 
            // textBoxDivMobiPath
            // 
            this.textBoxDivMobiPath.Location = new System.Drawing.Point(121, 96);
            this.textBoxDivMobiPath.Name = "textBoxDivMobiPath";
            this.textBoxDivMobiPath.Size = new System.Drawing.Size(194, 20);
            this.textBoxDivMobiPath.TabIndex = 11;
            // 
            // textBoxTimeOut
            // 
            this.textBoxTimeOut.Location = new System.Drawing.Point(121, 59);
            this.textBoxTimeOut.Name = "textBoxTimeOut";
            this.textBoxTimeOut.Size = new System.Drawing.Size(100, 20);
            this.textBoxTimeOut.TabIndex = 10;
            // 
            // labelTimeOutCaption
            // 
            this.labelTimeOutCaption.AutoSize = true;
            this.labelTimeOutCaption.Location = new System.Drawing.Point(19, 62);
            this.labelTimeOutCaption.Name = "labelTimeOutCaption";
            this.labelTimeOutCaption.Size = new System.Drawing.Size(75, 13);
            this.labelTimeOutCaption.TabIndex = 9;
            this.labelTimeOutCaption.Text = "TimeOut (in s):";
            this.labelTimeOutCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelTaxonNamesCaption
            // 
            this.labelTaxonNamesCaption.AutoSize = true;
            this.labelTaxonNamesCaption.Location = new System.Drawing.Point(19, 132);
            this.labelTaxonNamesCaption.Name = "labelTaxonNamesCaption";
            this.labelTaxonNamesCaption.Size = new System.Drawing.Size(88, 13);
            this.labelTaxonNamesCaption.TabIndex = 8;
            this.labelTaxonNamesCaption.Text = "TaxonNamesDB:";
            // 
            // labelPathDivMobiCaption
            // 
            this.labelPathDivMobiCaption.AutoSize = true;
            this.labelPathDivMobiCaption.Location = new System.Drawing.Point(19, 99);
            this.labelPathDivMobiCaption.Name = "labelPathDivMobiCaption";
            this.labelPathDivMobiCaption.Size = new System.Drawing.Size(96, 13);
            this.labelPathDivMobiCaption.TabIndex = 6;
            this.labelPathDivMobiCaption.Text = "DiversityMobileDB:";
            // 
            // panelActiveSyncOptions
            // 
            this.panelActiveSyncOptions.BackColor = System.Drawing.SystemColors.Control;
            this.panelActiveSyncOptions.Controls.Add(this.buttonCancel);
            this.panelActiveSyncOptions.Controls.Add(this.buttonOk);
            this.panelActiveSyncOptions.Controls.Add(this.label1);
            this.panelActiveSyncOptions.Controls.Add(this.groupBoxActiveSync);
            this.panelActiveSyncOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActiveSyncOptions.Location = new System.Drawing.Point(0, 0);
            this.panelActiveSyncOptions.Name = "panelActiveSyncOptions";
            this.panelActiveSyncOptions.Size = new System.Drawing.Size(416, 308);
            this.panelActiveSyncOptions.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(132, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 24);
            this.label1.TabIndex = 15;
            this.label1.Text = "User Options";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(15, 239);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(79, 23);
            this.buttonOk.TabIndex = 16;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(121, 239);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(294, 25);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 17;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // ActiveSyncOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 308);
            this.Controls.Add(this.panelActiveSyncOptions);
            this.Name = "ActiveSyncOptions";
            this.groupBoxActiveSync.ResumeLayout(false);
            this.groupBoxActiveSync.PerformLayout();
            this.panelActiveSyncOptions.ResumeLayout(false);
            this.panelActiveSyncOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxActiveSync;
        private System.Windows.Forms.TextBox textBoxTaxonNamesPath;
        private System.Windows.Forms.TextBox textBoxDivMobiPath;
        private System.Windows.Forms.TextBox textBoxTimeOut;
        private System.Windows.Forms.Label labelTimeOutCaption;
        private System.Windows.Forms.Label labelTaxonNamesCaption;
        private System.Windows.Forms.Label labelPathDivMobiCaption;
        private System.Windows.Forms.Panel panelActiveSyncOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxUseDevice;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonTest;
    }
}