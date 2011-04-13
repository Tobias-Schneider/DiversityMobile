using System.Windows.Forms;

namespace UBT.AI4.Bio.DivMobi.SyncGui
{
    partial class SyncPanel : UserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncPanel));
            this.labelSyncGuiHeading = new System.Windows.Forms.Label();
            this.labelPanelLocationOfRepository = new System.Windows.Forms.Label();
            this.buttonConnectRepository = new System.Windows.Forms.Button();
            this.panelDBConnection = new System.Windows.Forms.Panel();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.labelDBName = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxRepository = new System.Windows.Forms.TextBox();
            this.labelRepository = new System.Windows.Forms.Label();
            this.comboBoxRepositoryType = new System.Windows.Forms.ComboBox();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.buttonConnectMobileDB = new System.Windows.Forms.Button();
            this.buttonSelectMobileDB = new System.Windows.Forms.Button();
            this.textBoxMobileDBConnection = new System.Windows.Forms.TextBox();
            this.labelMobileDB = new System.Windows.Forms.Label();
            this.buttonConnectSyncDB = new System.Windows.Forms.Button();
            this.labelSyncDBConnection = new System.Windows.Forms.Label();
            this.tabControlAnalysis = new System.Windows.Forms.TabControl();
            this.tabPageConflicts = new System.Windows.Forms.TabPage();
            this.buttonAnalyzeConflict = new System.Windows.Forms.Button();
            this.listBoxConflicts = new System.Windows.Forms.ListBox();
            this.tabPageConfilctResolved = new System.Windows.Forms.TabPage();
            this.listBoxConflictResolved = new System.Windows.Forms.ListBox();
            this.tabPageSynchronized = new System.Windows.Forms.TabPage();
            this.listBoxSynchronized = new System.Windows.Forms.ListBox();
            this.tabPageInsert = new System.Windows.Forms.TabPage();
            this.listBoxInsert = new System.Windows.Forms.ListBox();
            this.tabPageUdate = new System.Windows.Forms.TabPage();
            this.listBoxUpdate = new System.Windows.Forms.ListBox();
            this.tabPageIgnore = new System.Windows.Forms.TabPage();
            this.listBoxIgnore = new System.Windows.Forms.ListBox();
            this.tabPageTruncate = new System.Windows.Forms.TabPage();
            this.listBoxTruncate = new System.Windows.Forms.ListBox();
            this.tabPageDeleted = new System.Windows.Forms.TabPage();
            this.listBoxDelete = new System.Windows.Forms.ListBox();
            this.tabPagePremature = new System.Windows.Forms.TabPage();
            this.listBoxPremature = new System.Windows.Forms.ListBox();
            this.buttonSynchronize = new System.Windows.Forms.Button();
            this.panelMobileDB = new System.Windows.Forms.Panel();
            this.radioButtonDownload = new System.Windows.Forms.RadioButton();
            this.radioButtonUpload = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSyncDBName = new System.Windows.Forms.Label();
            this.textBoxSyncName = new System.Windows.Forms.TextBox();
            this.comboBoxSyncLocation = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelDBConnection.SuspendLayout();
            this.tabControlAnalysis.SuspendLayout();
            this.tabPageConflicts.SuspendLayout();
            this.tabPageConfilctResolved.SuspendLayout();
            this.tabPageSynchronized.SuspendLayout();
            this.tabPageInsert.SuspendLayout();
            this.tabPageUdate.SuspendLayout();
            this.tabPageIgnore.SuspendLayout();
            this.tabPageTruncate.SuspendLayout();
            this.tabPageDeleted.SuspendLayout();
            this.tabPagePremature.SuspendLayout();
            this.panelMobileDB.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSyncGuiHeading
            // 
            this.labelSyncGuiHeading.AutoSize = true;
            this.labelSyncGuiHeading.BackColor = System.Drawing.SystemColors.Control;
            this.labelSyncGuiHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSyncGuiHeading.Location = new System.Drawing.Point(689, 12);
            this.labelSyncGuiHeading.Name = "labelSyncGuiHeading";
            this.labelSyncGuiHeading.Size = new System.Drawing.Size(183, 20);
            this.labelSyncGuiHeading.TabIndex = 0;
            this.labelSyncGuiHeading.Text = "Synchronization Form";
            this.labelSyncGuiHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPanelLocationOfRepository
            // 
            this.labelPanelLocationOfRepository.AutoSize = true;
            this.labelPanelLocationOfRepository.Location = new System.Drawing.Point(40, 15);
            this.labelPanelLocationOfRepository.Name = "labelPanelLocationOfRepository";
            this.labelPanelLocationOfRepository.Size = new System.Drawing.Size(113, 13);
            this.labelPanelLocationOfRepository.TabIndex = 0;
            this.labelPanelLocationOfRepository.Text = "Location of Repository";
            // 
            // buttonConnectRepository
            // 
            this.buttonConnectRepository.Location = new System.Drawing.Point(96, 293);
            this.buttonConnectRepository.Name = "buttonConnectRepository";
            this.buttonConnectRepository.Size = new System.Drawing.Size(75, 23);
            this.buttonConnectRepository.TabIndex = 4;
            this.buttonConnectRepository.Text = "Connect";
            this.buttonConnectRepository.UseVisualStyleBackColor = true;
            this.buttonConnectRepository.Visible = false;
            this.buttonConnectRepository.Click += new System.EventHandler(this.buttonConnectRepository_Click);
            // 
            // panelDBConnection
            // 
            this.panelDBConnection.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelDBConnection.Controls.Add(this.textBoxPassword);
            this.panelDBConnection.Controls.Add(this.labelPassword);
            this.panelDBConnection.Controls.Add(this.textBoxUserName);
            this.panelDBConnection.Controls.Add(this.labelUserName);
            this.panelDBConnection.Controls.Add(this.textBoxDBName);
            this.panelDBConnection.Controls.Add(this.labelDBName);
            this.panelDBConnection.Controls.Add(this.textBoxPort);
            this.panelDBConnection.Controls.Add(this.labelPort);
            this.panelDBConnection.Controls.Add(this.textBoxRepository);
            this.panelDBConnection.Controls.Add(this.labelRepository);
            this.panelDBConnection.Controls.Add(this.comboBoxRepositoryType);
            this.panelDBConnection.Controls.Add(this.buttonConnectRepository);
            this.panelDBConnection.Controls.Add(this.labelPanelLocationOfRepository);
            this.panelDBConnection.Location = new System.Drawing.Point(16, 53);
            this.panelDBConnection.Name = "panelDBConnection";
            this.panelDBConnection.Size = new System.Drawing.Size(187, 331);
            this.panelDBConnection.TabIndex = 1;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(13, 267);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(158, 20);
            this.textBoxPassword.TabIndex = 15;
            this.textBoxPassword.Visible = false;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(17, 250);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 14;
            this.labelPassword.Text = "Password";
            this.labelPassword.Visible = false;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(13, 223);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(158, 20);
            this.textBoxUserName.TabIndex = 13;
            this.textBoxUserName.Visible = false;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(12, 207);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(57, 13);
            this.labelUserName.TabIndex = 12;
            this.labelUserName.Text = "UserName";
            this.labelUserName.Visible = false;
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.Location = new System.Drawing.Point(13, 175);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(158, 20);
            this.textBoxDBName.TabIndex = 11;
            this.textBoxDBName.Visible = false;
            // 
            // labelDBName
            // 
            this.labelDBName.AutoSize = true;
            this.labelDBName.Location = new System.Drawing.Point(14, 155);
            this.labelDBName.Name = "labelDBName";
            this.labelDBName.Size = new System.Drawing.Size(50, 13);
            this.labelDBName.TabIndex = 10;
            this.labelDBName.Text = "DBName";
            this.labelDBName.Visible = false;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(13, 128);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(158, 20);
            this.textBoxPort.TabIndex = 9;
            this.textBoxPort.Visible = false;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(12, 113);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 8;
            this.labelPort.Text = "Port";
            this.labelPort.Visible = false;
            // 
            // textBoxRepository
            // 
            this.textBoxRepository.Location = new System.Drawing.Point(13, 82);
            this.textBoxRepository.Name = "textBoxRepository";
            this.textBoxRepository.Size = new System.Drawing.Size(158, 20);
            this.textBoxRepository.TabIndex = 7;
            this.textBoxRepository.Visible = false;
            // 
            // labelRepository
            // 
            this.labelRepository.AutoSize = true;
            this.labelRepository.Location = new System.Drawing.Point(13, 66);
            this.labelRepository.Name = "labelRepository";
            this.labelRepository.Size = new System.Drawing.Size(20, 13);
            this.labelRepository.TabIndex = 6;
            this.labelRepository.Text = "IP:";
            this.labelRepository.Visible = false;
            // 
            // comboBoxRepositoryType
            // 
            this.comboBoxRepositoryType.FormattingEnabled = true;
            this.comboBoxRepositoryType.Items.AddRange(new object[] {
            "Remote",
            "Local WA",
            "Local SQL"});
            this.comboBoxRepositoryType.Location = new System.Drawing.Point(14, 38);
            this.comboBoxRepositoryType.Name = "comboBoxRepositoryType";
            this.comboBoxRepositoryType.Size = new System.Drawing.Size(156, 21);
            this.comboBoxRepositoryType.TabIndex = 5;
            this.comboBoxRepositoryType.SelectedIndexChanged += new System.EventHandler(this.comboBoxRepositoryType_SelectedIndexChanged);
            // 
            // analyzeButton
            // 
            this.analyzeButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.analyzeButton.Enabled = false;
            this.analyzeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.analyzeButton.Location = new System.Drawing.Point(16, 684);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(187, 41);
            this.analyzeButton.TabIndex = 15;
            this.analyzeButton.Text = "Analyze";
            this.analyzeButton.UseVisualStyleBackColor = false;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // buttonConnectMobileDB
            // 
            this.buttonConnectMobileDB.Location = new System.Drawing.Point(95, 64);
            this.buttonConnectMobileDB.Name = "buttonConnectMobileDB";
            this.buttonConnectMobileDB.Size = new System.Drawing.Size(75, 23);
            this.buttonConnectMobileDB.TabIndex = 14;
            this.buttonConnectMobileDB.Text = "Connect";
            this.buttonConnectMobileDB.UseVisualStyleBackColor = true;
            this.buttonConnectMobileDB.Click += new System.EventHandler(this.buttonConnectMobileDB_Click);
            // 
            // buttonSelectMobileDB
            // 
            this.buttonSelectMobileDB.Location = new System.Drawing.Point(14, 64);
            this.buttonSelectMobileDB.Name = "buttonSelectMobileDB";
            this.buttonSelectMobileDB.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectMobileDB.TabIndex = 13;
            this.buttonSelectMobileDB.Text = "Select";
            this.buttonSelectMobileDB.UseVisualStyleBackColor = true;
            this.buttonSelectMobileDB.Click += new System.EventHandler(this.buttonSelectMobileDB_Click);
            // 
            // textBoxMobileDBConnection
            // 
            this.textBoxMobileDBConnection.Location = new System.Drawing.Point(14, 33);
            this.textBoxMobileDBConnection.Name = "textBoxMobileDBConnection";
            this.textBoxMobileDBConnection.ReadOnly = true;
            this.textBoxMobileDBConnection.Size = new System.Drawing.Size(156, 20);
            this.textBoxMobileDBConnection.TabIndex = 12;
            this.textBoxMobileDBConnection.Text = "Location of MobileDB";
            // 
            // labelMobileDB
            // 
            this.labelMobileDB.AutoSize = true;
            this.labelMobileDB.Location = new System.Drawing.Point(40, 12);
            this.labelMobileDB.Name = "labelMobileDB";
            this.labelMobileDB.Size = new System.Drawing.Size(108, 13);
            this.labelMobileDB.TabIndex = 11;
            this.labelMobileDB.Text = "Location of mobileDB";
            // 
            // buttonConnectSyncDB
            // 
            this.buttonConnectSyncDB.Location = new System.Drawing.Point(95, 112);
            this.buttonConnectSyncDB.Name = "buttonConnectSyncDB";
            this.buttonConnectSyncDB.Size = new System.Drawing.Size(75, 23);
            this.buttonConnectSyncDB.TabIndex = 10;
            this.buttonConnectSyncDB.Text = "Connect";
            this.buttonConnectSyncDB.UseVisualStyleBackColor = true;
            this.buttonConnectSyncDB.Visible = false;
            this.buttonConnectSyncDB.Click += new System.EventHandler(this.buttonConnectSyncDB_Click);
            // 
            // labelSyncDBConnection
            // 
            this.labelSyncDBConnection.AutoSize = true;
            this.labelSyncDBConnection.Location = new System.Drawing.Point(40, 12);
            this.labelSyncDBConnection.Name = "labelSyncDBConnection";
            this.labelSyncDBConnection.Size = new System.Drawing.Size(102, 13);
            this.labelSyncDBConnection.TabIndex = 8;
            this.labelSyncDBConnection.Text = "Location of SyncDB";
            // 
            // tabControlAnalysis
            // 
            this.tabControlAnalysis.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlAnalysis.Controls.Add(this.tabPageConflicts);
            this.tabControlAnalysis.Controls.Add(this.tabPageConfilctResolved);
            this.tabControlAnalysis.Controls.Add(this.tabPageSynchronized);
            this.tabControlAnalysis.Controls.Add(this.tabPageInsert);
            this.tabControlAnalysis.Controls.Add(this.tabPageUdate);
            this.tabControlAnalysis.Controls.Add(this.tabPageIgnore);
            this.tabControlAnalysis.Controls.Add(this.tabPageTruncate);
            this.tabControlAnalysis.Controls.Add(this.tabPageDeleted);
            this.tabControlAnalysis.Controls.Add(this.tabPagePremature);
            this.tabControlAnalysis.Enabled = false;
            this.tabControlAnalysis.Location = new System.Drawing.Point(227, 53);
            this.tabControlAnalysis.Name = "tabControlAnalysis";
            this.tabControlAnalysis.SelectedIndex = 0;
            this.tabControlAnalysis.Size = new System.Drawing.Size(936, 737);
            this.tabControlAnalysis.TabIndex = 2;
            // 
            // tabPageConflicts
            // 
            this.tabPageConflicts.BackColor = System.Drawing.SystemColors.Info;
            this.tabPageConflicts.Controls.Add(this.buttonAnalyzeConflict);
            this.tabPageConflicts.Controls.Add(this.listBoxConflicts);
            this.tabPageConflicts.Location = new System.Drawing.Point(4, 4);
            this.tabPageConflicts.Name = "tabPageConflicts";
            this.tabPageConflicts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConflicts.Size = new System.Drawing.Size(928, 711);
            this.tabPageConflicts.TabIndex = 0;
            this.tabPageConflicts.Text = "Conflict State !";
            this.tabPageConflicts.UseVisualStyleBackColor = true;
            // 
            // buttonAnalyzeConflict
            // 
            this.buttonAnalyzeConflict.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonAnalyzeConflict.Location = new System.Drawing.Point(790, 659);
            this.buttonAnalyzeConflict.Name = "buttonAnalyzeConflict";
            this.buttonAnalyzeConflict.Size = new System.Drawing.Size(123, 29);
            this.buttonAnalyzeConflict.TabIndex = 1;
            this.buttonAnalyzeConflict.Text = "AnalyzeConflict";
            this.buttonAnalyzeConflict.UseVisualStyleBackColor = false;
            this.buttonAnalyzeConflict.Click += new System.EventHandler(this.buttonAnalyzeConflict_Click);
            // 
            // listBoxConflicts
            // 
            this.listBoxConflicts.FormattingEnabled = true;
            this.listBoxConflicts.Location = new System.Drawing.Point(17, 21);
            this.listBoxConflicts.Name = "listBoxConflicts";
            this.listBoxConflicts.Size = new System.Drawing.Size(755, 667);
            this.listBoxConflicts.TabIndex = 0;
            // 
            // tabPageConfilctResolved
            // 
            this.tabPageConfilctResolved.Controls.Add(this.listBoxConflictResolved);
            this.tabPageConfilctResolved.Location = new System.Drawing.Point(4, 4);
            this.tabPageConfilctResolved.Name = "tabPageConfilctResolved";
            this.tabPageConfilctResolved.Size = new System.Drawing.Size(928, 711);
            this.tabPageConfilctResolved.TabIndex = 7;
            this.tabPageConfilctResolved.Text = "Conflict Resolved State";
            this.tabPageConfilctResolved.UseVisualStyleBackColor = true;
            // 
            // listBoxConflictResolved
            // 
            this.listBoxConflictResolved.FormattingEnabled = true;
            this.listBoxConflictResolved.Location = new System.Drawing.Point(169, 62);
            this.listBoxConflictResolved.Name = "listBoxConflictResolved";
            this.listBoxConflictResolved.Size = new System.Drawing.Size(472, 550);
            this.listBoxConflictResolved.TabIndex = 0;
            // 
            // tabPageSynchronized
            // 
            this.tabPageSynchronized.Controls.Add(this.listBoxSynchronized);
            this.tabPageSynchronized.Location = new System.Drawing.Point(4, 4);
            this.tabPageSynchronized.Name = "tabPageSynchronized";
            this.tabPageSynchronized.Size = new System.Drawing.Size(928, 711);
            this.tabPageSynchronized.TabIndex = 3;
            this.tabPageSynchronized.Text = "Synchronized State";
            this.tabPageSynchronized.UseVisualStyleBackColor = true;
            // 
            // listBoxSynchronized
            // 
            this.listBoxSynchronized.FormattingEnabled = true;
            this.listBoxSynchronized.Location = new System.Drawing.Point(127, 47);
            this.listBoxSynchronized.Name = "listBoxSynchronized";
            this.listBoxSynchronized.Size = new System.Drawing.Size(426, 524);
            this.listBoxSynchronized.TabIndex = 0;
            // 
            // tabPageInsert
            // 
            this.tabPageInsert.Controls.Add(this.listBoxInsert);
            this.tabPageInsert.Location = new System.Drawing.Point(4, 4);
            this.tabPageInsert.Name = "tabPageInsert";
            this.tabPageInsert.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInsert.Size = new System.Drawing.Size(928, 711);
            this.tabPageInsert.TabIndex = 1;
            this.tabPageInsert.Text = "InsertState";
            this.tabPageInsert.UseVisualStyleBackColor = true;
            // 
            // listBoxInsert
            // 
            this.listBoxInsert.FormattingEnabled = true;
            this.listBoxInsert.Location = new System.Drawing.Point(20, 34);
            this.listBoxInsert.Name = "listBoxInsert";
            this.listBoxInsert.Size = new System.Drawing.Size(781, 563);
            this.listBoxInsert.TabIndex = 0;
            // 
            // tabPageUdate
            // 
            this.tabPageUdate.Controls.Add(this.listBoxUpdate);
            this.tabPageUdate.Location = new System.Drawing.Point(4, 4);
            this.tabPageUdate.Name = "tabPageUdate";
            this.tabPageUdate.Size = new System.Drawing.Size(928, 711);
            this.tabPageUdate.TabIndex = 2;
            this.tabPageUdate.Text = "Update State";
            this.tabPageUdate.UseVisualStyleBackColor = true;
            // 
            // listBoxUpdate
            // 
            this.listBoxUpdate.FormattingEnabled = true;
            this.listBoxUpdate.Location = new System.Drawing.Point(131, 47);
            this.listBoxUpdate.Name = "listBoxUpdate";
            this.listBoxUpdate.Size = new System.Drawing.Size(553, 563);
            this.listBoxUpdate.TabIndex = 0;
            // 
            // tabPageIgnore
            // 
            this.tabPageIgnore.Controls.Add(this.listBoxIgnore);
            this.tabPageIgnore.Location = new System.Drawing.Point(4, 4);
            this.tabPageIgnore.Name = "tabPageIgnore";
            this.tabPageIgnore.Size = new System.Drawing.Size(928, 711);
            this.tabPageIgnore.TabIndex = 5;
            this.tabPageIgnore.Text = "Ignore State";
            this.tabPageIgnore.UseVisualStyleBackColor = true;
            // 
            // listBoxIgnore
            // 
            this.listBoxIgnore.FormattingEnabled = true;
            this.listBoxIgnore.Location = new System.Drawing.Point(174, 47);
            this.listBoxIgnore.Name = "listBoxIgnore";
            this.listBoxIgnore.Size = new System.Drawing.Size(509, 602);
            this.listBoxIgnore.TabIndex = 0;
            // 
            // tabPageTruncate
            // 
            this.tabPageTruncate.Controls.Add(this.listBoxTruncate);
            this.tabPageTruncate.Location = new System.Drawing.Point(4, 4);
            this.tabPageTruncate.Name = "tabPageTruncate";
            this.tabPageTruncate.Size = new System.Drawing.Size(928, 711);
            this.tabPageTruncate.TabIndex = 6;
            this.tabPageTruncate.Text = "Truncate State";
            this.tabPageTruncate.UseVisualStyleBackColor = true;
            // 
            // listBoxTruncate
            // 
            this.listBoxTruncate.FormattingEnabled = true;
            this.listBoxTruncate.Location = new System.Drawing.Point(230, 47);
            this.listBoxTruncate.Name = "listBoxTruncate";
            this.listBoxTruncate.Size = new System.Drawing.Size(463, 628);
            this.listBoxTruncate.TabIndex = 0;
            // 
            // tabPageDeleted
            // 
            this.tabPageDeleted.Controls.Add(this.listBoxDelete);
            this.tabPageDeleted.Location = new System.Drawing.Point(4, 4);
            this.tabPageDeleted.Name = "tabPageDeleted";
            this.tabPageDeleted.Size = new System.Drawing.Size(928, 711);
            this.tabPageDeleted.TabIndex = 8;
            this.tabPageDeleted.Text = "Deleted State";
            this.tabPageDeleted.UseVisualStyleBackColor = true;
            // 
            // listBoxDelete
            // 
            this.listBoxDelete.FormattingEnabled = true;
            this.listBoxDelete.Location = new System.Drawing.Point(242, 37);
            this.listBoxDelete.Name = "listBoxDelete";
            this.listBoxDelete.Size = new System.Drawing.Size(641, 628);
            this.listBoxDelete.TabIndex = 0;
            // 
            // tabPagePremature
            // 
            this.tabPagePremature.Controls.Add(this.listBoxPremature);
            this.tabPagePremature.Location = new System.Drawing.Point(4, 4);
            this.tabPagePremature.Name = "tabPagePremature";
            this.tabPagePremature.Size = new System.Drawing.Size(928, 711);
            this.tabPagePremature.TabIndex = 4;
            this.tabPagePremature.Text = "Premature State";
            this.tabPagePremature.UseVisualStyleBackColor = true;
            // 
            // listBoxPremature
            // 
            this.listBoxPremature.FormattingEnabled = true;
            this.listBoxPremature.Location = new System.Drawing.Point(177, 47);
            this.listBoxPremature.Name = "listBoxPremature";
            this.listBoxPremature.Size = new System.Drawing.Size(464, 563);
            this.listBoxPremature.TabIndex = 0;
            // 
            // buttonSynchronize
            // 
            this.buttonSynchronize.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonSynchronize.Enabled = false;
            this.buttonSynchronize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.buttonSynchronize.Location = new System.Drawing.Point(16, 731);
            this.buttonSynchronize.Name = "buttonSynchronize";
            this.buttonSynchronize.Size = new System.Drawing.Size(187, 45);
            this.buttonSynchronize.TabIndex = 3;
            this.buttonSynchronize.Text = "Synchronize";
            this.buttonSynchronize.UseVisualStyleBackColor = false;
            this.buttonSynchronize.Click += new System.EventHandler(this.buttonSynchronize_Click);
            // 
            // panelMobileDB
            // 
            this.panelMobileDB.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelMobileDB.Controls.Add(this.labelMobileDB);
            this.panelMobileDB.Controls.Add(this.buttonConnectMobileDB);
            this.panelMobileDB.Controls.Add(this.textBoxMobileDBConnection);
            this.panelMobileDB.Controls.Add(this.buttonSelectMobileDB);
            this.panelMobileDB.Location = new System.Drawing.Point(16, 390);
            this.panelMobileDB.Name = "panelMobileDB";
            this.panelMobileDB.Size = new System.Drawing.Size(187, 93);
            this.panelMobileDB.TabIndex = 4;
            // 
            // radioButtonDownload
            // 
            this.radioButtonDownload.AutoSize = true;
            this.radioButtonDownload.Location = new System.Drawing.Point(30, 652);
            this.radioButtonDownload.Name = "radioButtonDownload";
            this.radioButtonDownload.Size = new System.Drawing.Size(73, 17);
            this.radioButtonDownload.TabIndex = 16;
            this.radioButtonDownload.Text = "Download";
            this.radioButtonDownload.UseVisualStyleBackColor = true;
            this.radioButtonDownload.Click += new System.EventHandler(this.radioButtonDownload_Click);
            // 
            // radioButtonUpload
            // 
            this.radioButtonUpload.AutoSize = true;
            this.radioButtonUpload.Checked = true;
            this.radioButtonUpload.Location = new System.Drawing.Point(111, 652);
            this.radioButtonUpload.Name = "radioButtonUpload";
            this.radioButtonUpload.Size = new System.Drawing.Size(59, 17);
            this.radioButtonUpload.TabIndex = 17;
            this.radioButtonUpload.TabStop = true;
            this.radioButtonUpload.Text = "Upload";
            this.radioButtonUpload.UseVisualStyleBackColor = true;
            this.radioButtonUpload.Click += new System.EventHandler(this.radioButtonUpload_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.labelSyncDBName);
            this.panel1.Controls.Add(this.textBoxSyncName);
            this.panel1.Controls.Add(this.comboBoxSyncLocation);
            this.panel1.Controls.Add(this.buttonConnectSyncDB);
            this.panel1.Controls.Add(this.labelSyncDBConnection);
            this.panel1.Location = new System.Drawing.Point(16, 495);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(187, 148);
            this.panel1.TabIndex = 19;
            // 
            // labelSyncDBName
            // 
            this.labelSyncDBName.AutoSize = true;
            this.labelSyncDBName.Location = new System.Drawing.Point(53, 63);
            this.labelSyncDBName.Name = "labelSyncDBName";
            this.labelSyncDBName.Size = new System.Drawing.Size(74, 13);
            this.labelSyncDBName.TabIndex = 13;
            this.labelSyncDBName.Text = "SyncDBName";
            this.labelSyncDBName.Visible = false;
            // 
            // textBoxSyncName
            // 
            this.textBoxSyncName.Location = new System.Drawing.Point(13, 82);
            this.textBoxSyncName.Name = "textBoxSyncName";
            this.textBoxSyncName.Size = new System.Drawing.Size(157, 20);
            this.textBoxSyncName.TabIndex = 12;
            this.textBoxSyncName.Visible = false;
            // 
            // comboBoxSyncLocation
            // 
            this.comboBoxSyncLocation.FormattingEnabled = true;
            this.comboBoxSyncLocation.Items.AddRange(new object[] {
            "Repository",
            "Mobile"});
            this.comboBoxSyncLocation.Location = new System.Drawing.Point(13, 33);
            this.comboBoxSyncLocation.Name = "comboBoxSyncLocation";
            this.comboBoxSyncLocation.Size = new System.Drawing.Size(158, 21);
            this.comboBoxSyncLocation.TabIndex = 11;
            this.comboBoxSyncLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxSyncLocation_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1180, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 164);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SyncPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioButtonUpload);
            this.Controls.Add(this.radioButtonDownload);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.panelMobileDB);
            this.Controls.Add(this.buttonSynchronize);
            this.Controls.Add(this.tabControlAnalysis);
            this.Controls.Add(this.panelDBConnection);
            this.Controls.Add(this.labelSyncGuiHeading);
            this.Name = "SyncPanel";
            this.Size = new System.Drawing.Size(1507, 974);
            this.panelDBConnection.ResumeLayout(false);
            this.panelDBConnection.PerformLayout();
            this.tabControlAnalysis.ResumeLayout(false);
            this.tabPageConflicts.ResumeLayout(false);
            this.tabPageConfilctResolved.ResumeLayout(false);
            this.tabPageSynchronized.ResumeLayout(false);
            this.tabPageInsert.ResumeLayout(false);
            this.tabPageUdate.ResumeLayout(false);
            this.tabPageIgnore.ResumeLayout(false);
            this.tabPageTruncate.ResumeLayout(false);
            this.tabPageDeleted.ResumeLayout(false);
            this.tabPagePremature.ResumeLayout(false);
            this.panelMobileDB.ResumeLayout(false);
            this.panelMobileDB.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelSyncGuiHeading;
        private Label labelPanelLocationOfRepository;
        private Button buttonConnectRepository;
        private Panel panelDBConnection;
        private Button buttonConnectSyncDB;
        private Label labelSyncDBConnection;
        private Button buttonConnectMobileDB;
        private Button buttonSelectMobileDB;
        private TextBox textBoxMobileDBConnection;
        private Label labelMobileDB;
        private Button analyzeButton;
        private TabControl tabControlAnalysis;
        private TabPage tabPageConflicts;
        private TabPage tabPageInsert;
        private ListBox listBoxConflicts;
        private TabPage tabPageUdate;
        private TabPage tabPageSynchronized;
        private TabPage tabPagePremature;
        private TabPage tabPageIgnore;
        private TabPage tabPageTruncate;
        private TabPage tabPageConfilctResolved;
        private TabPage tabPageDeleted;
        private ListBox listBoxConflictResolved;
        private ListBox listBoxSynchronized;
        private ListBox listBoxInsert;
        private ListBox listBoxUpdate;
        private ListBox listBoxIgnore;
        private ListBox listBoxTruncate;
        private ListBox listBoxDelete;
        private ListBox listBoxPremature;
        private Button buttonSynchronize;
        private Button buttonAnalyzeConflict;
        private Panel panelMobileDB;
        private RadioButton radioButtonDownload;
        private RadioButton radioButtonUpload;
        private Panel panel1;
        private Label labelSyncDBName;
        private TextBox textBoxSyncName;
        private ComboBox comboBoxSyncLocation;
        private ComboBox comboBoxRepositoryType;
        private Label labelPort;
        private TextBox textBoxRepository;
        private Label labelRepository;
        private TextBox textBoxPort;
        private Label labelDBName;
        private TextBox textBoxDBName;
        private Label labelPassword;
        private TextBox textBoxUserName;
        private Label labelUserName;
        private TextBox textBoxPassword;
        private PictureBox pictureBox1;

    }
}
