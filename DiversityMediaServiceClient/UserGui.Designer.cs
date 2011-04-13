namespace UserSyncGui
{
    partial class UserGui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserGui));
            this.labelConnectionRepository = new System.Windows.Forms.Label();
            this.labelConnectionMobile = new System.Windows.Forms.Label();
            this.buttonConnectMobile = new System.Windows.Forms.Button();
            this.buttonConnectRepository = new System.Windows.Forms.Button();
            this.panelLogon = new System.Windows.Forms.Panel();
            this.labelLogon = new System.Windows.Forms.Label();
            this.buttonChooseProject = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.listBoxChooseProject = new System.Windows.Forms.ListBox();
            this.labelCurrentProject = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonDownLoad = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonGetDefinitions = new System.Windows.Forms.Button();
            this.buttonGetTaxa = new System.Windows.Forms.Button();
            this.labelActions = new System.Windows.Forms.Label();
            this.menuStripMainMenu = new System.Windows.Forms.MenuStrip();
            this.programmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repositoryConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemGoogleMaps = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemlocalMaps = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonIdentSave = new System.Windows.Forms.Button();
            this.panelLogon.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStripMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelConnectionRepository
            // 
            this.labelConnectionRepository.AutoSize = true;
            this.labelConnectionRepository.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectionRepository.Location = new System.Drawing.Point(214, 33);
            this.labelConnectionRepository.Name = "labelConnectionRepository";
            this.labelConnectionRepository.Size = new System.Drawing.Size(239, 16);
            this.labelConnectionRepository.TabIndex = 0;
            this.labelConnectionRepository.Text = "Not Connected to SNSB IT Center";
            // 
            // labelConnectionMobile
            // 
            this.labelConnectionMobile.AutoSize = true;
            this.labelConnectionMobile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectionMobile.Location = new System.Drawing.Point(214, 76);
            this.labelConnectionMobile.Name = "labelConnectionMobile";
            this.labelConnectionMobile.Size = new System.Drawing.Size(199, 16);
            this.labelConnectionMobile.TabIndex = 1;
            this.labelConnectionMobile.Text = "Not Connected to MobileDB";
            // 
            // buttonConnectMobile
            // 
            this.buttonConnectMobile.BackColor = System.Drawing.SystemColors.Control;
            this.buttonConnectMobile.Location = new System.Drawing.Point(13, 73);
            this.buttonConnectMobile.Name = "buttonConnectMobile";
            this.buttonConnectMobile.Size = new System.Drawing.Size(169, 23);
            this.buttonConnectMobile.TabIndex = 2;
            this.buttonConnectMobile.Text = "Connect to Mobile DB";
            this.buttonConnectMobile.UseVisualStyleBackColor = false;
            this.buttonConnectMobile.Click += new System.EventHandler(this.buttonConnectMobile_Click);
            // 
            // buttonConnectRepository
            // 
            this.buttonConnectRepository.BackColor = System.Drawing.SystemColors.Control;
            this.buttonConnectRepository.Location = new System.Drawing.Point(13, 30);
            this.buttonConnectRepository.Name = "buttonConnectRepository";
            this.buttonConnectRepository.Size = new System.Drawing.Size(169, 23);
            this.buttonConnectRepository.TabIndex = 1;
            this.buttonConnectRepository.Text = "Connect to Repository";
            this.buttonConnectRepository.UseVisualStyleBackColor = false;
            this.buttonConnectRepository.Click += new System.EventHandler(this.buttonConnectRepository_Click);
            // 
            // panelLogon
            // 
            this.panelLogon.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelLogon.Controls.Add(this.labelLogon);
            this.panelLogon.Controls.Add(this.buttonConnectRepository);
            this.panelLogon.Controls.Add(this.labelConnectionMobile);
            this.panelLogon.Controls.Add(this.buttonConnectMobile);
            this.panelLogon.Controls.Add(this.labelConnectionRepository);
            this.panelLogon.Location = new System.Drawing.Point(13, 45);
            this.panelLogon.Name = "panelLogon";
            this.panelLogon.Size = new System.Drawing.Size(740, 120);
            this.panelLogon.TabIndex = 4;
            // 
            // labelLogon
            // 
            this.labelLogon.AutoSize = true;
            this.labelLogon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogon.Location = new System.Drawing.Point(271, 10);
            this.labelLogon.Name = "labelLogon";
            this.labelLogon.Size = new System.Drawing.Size(138, 16);
            this.labelLogon.TabIndex = 4;
            this.labelLogon.Text = "Log In Informations";
            // 
            // buttonChooseProject
            // 
            this.buttonChooseProject.BackColor = System.Drawing.SystemColors.Control;
            this.buttonChooseProject.Enabled = false;
            this.buttonChooseProject.Location = new System.Drawing.Point(13, 8);
            this.buttonChooseProject.Name = "buttonChooseProject";
            this.buttonChooseProject.Size = new System.Drawing.Size(169, 23);
            this.buttonChooseProject.TabIndex = 3;
            this.buttonChooseProject.Text = "List Projects";
            this.buttonChooseProject.UseVisualStyleBackColor = false;
            this.buttonChooseProject.Click += new System.EventHandler(this.buttonChooseProject_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.buttonChoose);
            this.panel1.Controls.Add(this.labelProjectName);
            this.panel1.Controls.Add(this.listBoxChooseProject);
            this.panel1.Controls.Add(this.labelCurrentProject);
            this.panel1.Controls.Add(this.labelUserName);
            this.panel1.Controls.Add(this.buttonChooseProject);
            this.panel1.Location = new System.Drawing.Point(13, 182);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 96);
            this.panel1.TabIndex = 6;
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(188, 70);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(75, 23);
            this.buttonChoose.TabIndex = 13;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Visible = false;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(283, 70);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(71, 13);
            this.labelProjectName.TabIndex = 12;
            this.labelProjectName.Text = "Project Name";
            // 
            // listBoxChooseProject
            // 
            this.listBoxChooseProject.FormattingEnabled = true;
            this.listBoxChooseProject.Location = new System.Drawing.Point(13, 37);
            this.listBoxChooseProject.Name = "listBoxChooseProject";
            this.listBoxChooseProject.Size = new System.Drawing.Size(169, 56);
            this.listBoxChooseProject.TabIndex = 11;
            this.listBoxChooseProject.Visible = false;
            this.listBoxChooseProject.DoubleClick += new System.EventHandler(this.listBoxChooseProject_DoubleClick);
            // 
            // labelCurrentProject
            // 
            this.labelCurrentProject.AutoSize = true;
            this.labelCurrentProject.Location = new System.Drawing.Point(283, 47);
            this.labelCurrentProject.Name = "labelCurrentProject";
            this.labelCurrentProject.Size = new System.Drawing.Size(43, 13);
            this.labelCurrentProject.TabIndex = 10;
            this.labelCurrentProject.Text = "Project:";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(283, 13);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(60, 13);
            this.labelUserName.TabIndex = 9;
            this.labelUserName.Text = "User Name";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Info;
            this.panel2.Controls.Add(this.buttonIdentSave);
            this.panel2.Controls.Add(this.buttonDownLoad);
            this.panel2.Controls.Add(this.buttonUpload);
            this.panel2.Controls.Add(this.buttonGetDefinitions);
            this.panel2.Controls.Add(this.buttonGetTaxa);
            this.panel2.Controls.Add(this.labelActions);
            this.panel2.Location = new System.Drawing.Point(13, 295);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(740, 123);
            this.panel2.TabIndex = 7;
            // 
            // buttonDownLoad
            // 
            this.buttonDownLoad.BackColor = System.Drawing.SystemColors.Control;
            this.buttonDownLoad.Enabled = false;
            this.buttonDownLoad.Location = new System.Drawing.Point(571, 53);
            this.buttonDownLoad.Name = "buttonDownLoad";
            this.buttonDownLoad.Size = new System.Drawing.Size(136, 23);
            this.buttonDownLoad.TabIndex = 7;
            this.buttonDownLoad.Text = "Download Field Data";
            this.buttonDownLoad.UseVisualStyleBackColor = false;
            this.buttonDownLoad.Click += new System.EventHandler(this.buttonDownLoad_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.BackColor = System.Drawing.SystemColors.Control;
            this.buttonUpload.Enabled = false;
            this.buttonUpload.Location = new System.Drawing.Point(413, 54);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(130, 23);
            this.buttonUpload.TabIndex = 6;
            this.buttonUpload.Text = "Upload Field Data";
            this.buttonUpload.UseVisualStyleBackColor = false;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // buttonGetDefinitions
            // 
            this.buttonGetDefinitions.BackColor = System.Drawing.SystemColors.Control;
            this.buttonGetDefinitions.Enabled = false;
            this.buttonGetDefinitions.Location = new System.Drawing.Point(196, 55);
            this.buttonGetDefinitions.Name = "buttonGetDefinitions";
            this.buttonGetDefinitions.Size = new System.Drawing.Size(190, 23);
            this.buttonGetDefinitions.TabIndex = 5;
            this.buttonGetDefinitions.Text = "Get DiversityCollection Definitions";
            this.buttonGetDefinitions.UseVisualStyleBackColor = false;
            this.buttonGetDefinitions.Click += new System.EventHandler(this.buttonGetDefinitions_Click);
            // 
            // buttonGetTaxa
            // 
            this.buttonGetTaxa.BackColor = System.Drawing.SystemColors.Control;
            this.buttonGetTaxa.Enabled = false;
            this.buttonGetTaxa.Location = new System.Drawing.Point(13, 55);
            this.buttonGetTaxa.Name = "buttonGetTaxa";
            this.buttonGetTaxa.Size = new System.Drawing.Size(145, 23);
            this.buttonGetTaxa.TabIndex = 4;
            this.buttonGetTaxa.Text = "Get Taxon Definitions";
            this.buttonGetTaxa.UseVisualStyleBackColor = false;
            this.buttonGetTaxa.Click += new System.EventHandler(this.buttonGetTaxa_Click);
            // 
            // labelActions
            // 
            this.labelActions.AutoSize = true;
            this.labelActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelActions.Location = new System.Drawing.Point(305, 19);
            this.labelActions.Name = "labelActions";
            this.labelActions.Size = new System.Drawing.Size(59, 16);
            this.labelActions.TabIndex = 0;
            this.labelActions.Text = "Actions";
            // 
            // menuStripMainMenu
            // 
            this.menuStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programmToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.googleMapsToolStripMenuItem});
            this.menuStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainMenu.Name = "menuStripMainMenu";
            this.menuStripMainMenu.Size = new System.Drawing.Size(765, 24);
            this.menuStripMainMenu.TabIndex = 13;
            this.menuStripMainMenu.Text = "menuStrip1";
            // 
            // programmToolStripMenuItem
            // 
            this.programmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finishToolStripMenuItem});
            this.programmToolStripMenuItem.Name = "programmToolStripMenuItem";
            this.programmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.programmToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.programmToolStripMenuItem.Text = "&Program";
            // 
            // finishToolStripMenuItem
            // 
            this.finishToolStripMenuItem.Name = "finishToolStripMenuItem";
            this.finishToolStripMenuItem.ShortcutKeyDisplayString = "Strg+F";
            this.finishToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.finishToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.finishToolStripMenuItem.Text = "&Finish";
            this.finishToolStripMenuItem.Click += new System.EventHandler(this.finishToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repositoryConnectionToolStripMenuItem,
            this.activeSyncToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // repositoryConnectionToolStripMenuItem
            // 
            this.repositoryConnectionToolStripMenuItem.Name = "repositoryConnectionToolStripMenuItem";
            this.repositoryConnectionToolStripMenuItem.ShortcutKeyDisplayString = "Strg+R";
            this.repositoryConnectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.repositoryConnectionToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.repositoryConnectionToolStripMenuItem.Text = "&Repository Connection";
            this.repositoryConnectionToolStripMenuItem.Click += new System.EventHandler(this.repositoryConnectionToolStripMenuItem_Click);
            // 
            // activeSyncToolStripMenuItem
            // 
            this.activeSyncToolStripMenuItem.Name = "activeSyncToolStripMenuItem";
            this.activeSyncToolStripMenuItem.ShortcutKeyDisplayString = "Strg+A";
            this.activeSyncToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.activeSyncToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.activeSyncToolStripMenuItem.Text = "&Active Sync";
            this.activeSyncToolStripMenuItem.Click += new System.EventHandler(this.activeSyncToolStripMenuItem_Click);
            // 
            // googleMapsToolStripMenuItem
            // 
            this.googleMapsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemGoogleMaps,
            this.ToolStripMenuItemlocalMaps});
            this.googleMapsToolStripMenuItem.Name = "googleMapsToolStripMenuItem";
            this.googleMapsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.googleMapsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.googleMapsToolStripMenuItem.Text = "&Maps";
            // 
            // ToolStripMenuItemGoogleMaps
            // 
            this.ToolStripMenuItemGoogleMaps.Name = "ToolStripMenuItemGoogleMaps";
            this.ToolStripMenuItemGoogleMaps.ShortcutKeyDisplayString = "Strg+G";
            this.ToolStripMenuItemGoogleMaps.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.ToolStripMenuItemGoogleMaps.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItemGoogleMaps.Text = "&Google Maps";
            this.ToolStripMenuItemGoogleMaps.Click += new System.EventHandler(this.ToolStripMenuItemGoogleMaps_Click);
            // 
            // ToolStripMenuItemlocalMaps
            // 
            this.ToolStripMenuItemlocalMaps.Name = "ToolStripMenuItemlocalMaps";
            this.ToolStripMenuItemlocalMaps.ShortcutKeyDisplayString = "Strg+L";
            this.ToolStripMenuItemlocalMaps.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.ToolStripMenuItemlocalMaps.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItemlocalMaps.Text = "&Local Maps";
            this.ToolStripMenuItemlocalMaps.Click += new System.EventHandler(this.ToolStripMenuItemlocalMaps_Click);
            // 
            // buttonIdentSave
            // 
            this.buttonIdentSave.Location = new System.Drawing.Point(413, 11);
            this.buttonIdentSave.Name = "buttonIdentSave";
            this.buttonIdentSave.Size = new System.Drawing.Size(130, 23);
            this.buttonIdentSave.TabIndex = 8;
            this.buttonIdentSave.Text = "IdentSave";
            this.buttonIdentSave.UseVisualStyleBackColor = true;
            this.buttonIdentSave.Visible = false;
            this.buttonIdentSave.Click += new System.EventHandler(this.buttonIdentSave_Click);
            // 
            // UserGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 438);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelLogon);
            this.Controls.Add(this.menuStripMainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMainMenu;
            this.Name = "UserGui";
            this.Text = "DiversityMobile Synchronization";
            this.panelLogon.ResumeLayout(false);
            this.panelLogon.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStripMainMenu.ResumeLayout(false);
            this.menuStripMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelConnectionRepository;
        private System.Windows.Forms.Label labelConnectionMobile;
        private System.Windows.Forms.Button buttonConnectMobile;
        private System.Windows.Forms.Button buttonConnectRepository;
        private System.Windows.Forms.Panel panelLogon;
        private System.Windows.Forms.Button buttonChooseProject;
        private System.Windows.Forms.Label labelLogon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonGetDefinitions;
        private System.Windows.Forms.Button buttonGetTaxa;
        private System.Windows.Forms.Label labelActions;
        private System.Windows.Forms.Button buttonDownLoad;
        private System.Windows.Forms.Label labelCurrentProject;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.MenuStrip menuStripMainMenu;
        private System.Windows.Forms.ToolStripMenuItem programmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repositoryConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeSyncToolStripMenuItem;
        private System.Windows.Forms.ListBox listBoxChooseProject;
        private System.Windows.Forms.ToolStripMenuItem googleMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemGoogleMaps;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemlocalMaps;
        private System.Windows.Forms.Button buttonIdentSave;
    }
}

