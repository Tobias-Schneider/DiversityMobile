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
            this.buttonTaxonNamesSearch = new System.Windows.Forms.Button();
            this.textBoxTaxonNamesPath = new System.Windows.Forms.TextBox();
            this.buttonDivMobiSearch = new System.Windows.Forms.Button();
            this.textBoxDivMobiPath = new System.Windows.Forms.TextBox();
            this.textBoxTimeOut = new System.Windows.Forms.TextBox();
            this.labelTimeOutCaption = new System.Windows.Forms.Label();
            this.labelTaxonNamesCaption = new System.Windows.Forms.Label();
            this.labelPathDivMobiCaption = new System.Windows.Forms.Label();
            this.panelActiveSyncOptions = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStripOptionsFormMenu = new System.Windows.Forms.MenuStrip();
            this.formularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxActiveSync.SuspendLayout();
            this.panelActiveSyncOptions.SuspendLayout();
            this.menuStripOptionsFormMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxActiveSync
            // 
            this.groupBoxActiveSync.Controls.Add(this.checkBoxUseDevice);
            this.groupBoxActiveSync.Controls.Add(this.buttonTaxonNamesSearch);
            this.groupBoxActiveSync.Controls.Add(this.textBoxTaxonNamesPath);
            this.groupBoxActiveSync.Controls.Add(this.buttonDivMobiSearch);
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
            // buttonTaxonNamesSearch
            // 
            this.buttonTaxonNamesSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTaxonNamesSearch.Location = new System.Drawing.Point(321, 126);
            this.buttonTaxonNamesSearch.Name = "buttonTaxonNamesSearch";
            this.buttonTaxonNamesSearch.Size = new System.Drawing.Size(48, 23);
            this.buttonTaxonNamesSearch.TabIndex = 15;
            this.buttonTaxonNamesSearch.Text = "...";
            this.buttonTaxonNamesSearch.UseVisualStyleBackColor = true;
            this.buttonTaxonNamesSearch.Click += new System.EventHandler(this.buttonTaxonNamesSearch_Click);
            // 
            // textBoxTaxonNamesPath
            // 
            this.textBoxTaxonNamesPath.Location = new System.Drawing.Point(121, 129);
            this.textBoxTaxonNamesPath.Name = "textBoxTaxonNamesPath";
            this.textBoxTaxonNamesPath.Size = new System.Drawing.Size(194, 20);
            this.textBoxTaxonNamesPath.TabIndex = 14;
            // 
            // buttonDivMobiSearch
            // 
            this.buttonDivMobiSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDivMobiSearch.Location = new System.Drawing.Point(321, 94);
            this.buttonDivMobiSearch.Name = "buttonDivMobiSearch";
            this.buttonDivMobiSearch.Size = new System.Drawing.Size(48, 23);
            this.buttonDivMobiSearch.TabIndex = 13;
            this.buttonDivMobiSearch.Text = "...";
            this.buttonDivMobiSearch.UseVisualStyleBackColor = true;
            this.buttonDivMobiSearch.Click += new System.EventHandler(this.buttonDivMobiSearch_Click);
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
            this.panelActiveSyncOptions.Controls.Add(this.label1);
            this.panelActiveSyncOptions.Controls.Add(this.groupBoxActiveSync);
            this.panelActiveSyncOptions.Location = new System.Drawing.Point(89, 62);
            this.panelActiveSyncOptions.Name = "panelActiveSyncOptions";
            this.panelActiveSyncOptions.Size = new System.Drawing.Size(53, 44);
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
            // menuStripOptionsFormMenu
            // 
            this.menuStripOptionsFormMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formularToolStripMenuItem});
            this.menuStripOptionsFormMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStripOptionsFormMenu.Name = "menuStripOptionsFormMenu";
            this.menuStripOptionsFormMenu.Size = new System.Drawing.Size(416, 24);
            this.menuStripOptionsFormMenu.TabIndex = 15;
            this.menuStripOptionsFormMenu.Text = "menuStrip1";
            // 
            // formularToolStripMenuItem
            // 
            this.formularToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.formularToolStripMenuItem.Name = "formularToolStripMenuItem";
            this.formularToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.formularToolStripMenuItem.Text = "Form";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Save + Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 247);
            this.Controls.Add(this.panelActiveSyncOptions);
            this.Controls.Add(this.menuStripOptionsFormMenu);
            this.MainMenuStrip = this.menuStripOptionsFormMenu;
            this.Name = "Options";
            this.groupBoxActiveSync.ResumeLayout(false);
            this.groupBoxActiveSync.PerformLayout();
            this.panelActiveSyncOptions.ResumeLayout(false);
            this.panelActiveSyncOptions.PerformLayout();
            this.menuStripOptionsFormMenu.ResumeLayout(false);
            this.menuStripOptionsFormMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxActiveSync;
        private System.Windows.Forms.Button buttonTaxonNamesSearch;
        private System.Windows.Forms.TextBox textBoxTaxonNamesPath;
        private System.Windows.Forms.Button buttonDivMobiSearch;
        private System.Windows.Forms.TextBox textBoxDivMobiPath;
        private System.Windows.Forms.TextBox textBoxTimeOut;
        private System.Windows.Forms.Label labelTimeOutCaption;
        private System.Windows.Forms.Label labelTaxonNamesCaption;
        private System.Windows.Forms.Label labelPathDivMobiCaption;
        private System.Windows.Forms.Panel panelActiveSyncOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStripOptionsFormMenu;
        private System.Windows.Forms.ToolStripMenuItem formularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxUseDevice;
    }
}