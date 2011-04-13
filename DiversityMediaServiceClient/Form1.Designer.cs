namespace UBT.AI4.Bio.DivMobi.MediaServiceApplication
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonConnectRepository = new System.Windows.Forms.Button();
            this.buttonConnectMobile = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.labelConnectionRepository = new System.Windows.Forms.Label();
            this.menuStripMainMenu = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repositoryConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelConnectionMobile = new System.Windows.Forms.Label();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.listBoxChooseProject = new System.Windows.Forms.ListBox();
            this.labelCurrentProject = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.buttonChooseProject = new System.Windows.Forms.Button();
            this.buttonGetTaxa = new System.Windows.Forms.Button();
            this.buttonGetDefinitions = new System.Windows.Forms.Button();
            this.buttonDownLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStripMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(77, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Transfer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonTransfer_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(13, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(968, 215);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Info;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 452);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(231, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(398, 20);
            this.textBox1.TabIndex = 4;
            // 
            // buttonConnectRepository
            // 
            this.buttonConnectRepository.Location = new System.Drawing.Point(22, 292);
            this.buttonConnectRepository.Name = "buttonConnectRepository";
            this.buttonConnectRepository.Size = new System.Drawing.Size(146, 23);
            this.buttonConnectRepository.TabIndex = 5;
            this.buttonConnectRepository.Text = "buttonConnectRepository";
            this.buttonConnectRepository.UseVisualStyleBackColor = true;
            this.buttonConnectRepository.Click += new System.EventHandler(this.buttonConnectRepository_Click);
            // 
            // buttonConnectMobile
            // 
            this.buttonConnectMobile.Location = new System.Drawing.Point(22, 338);
            this.buttonConnectMobile.Name = "buttonConnectMobile";
            this.buttonConnectMobile.Size = new System.Drawing.Size(130, 23);
            this.buttonConnectMobile.TabIndex = 6;
            this.buttonConnectMobile.Text = "ConnectMobile";
            this.buttonConnectMobile.UseVisualStyleBackColor = true;
            this.buttonConnectMobile.Click += new System.EventHandler(this.buttonConnectMobile_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(503, 423);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(130, 23);
            this.buttonUpload.TabIndex = 7;
            this.buttonUpload.Text = "Upload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // labelConnectionRepository
            // 
            this.labelConnectionRepository.AutoSize = true;
            this.labelConnectionRepository.Location = new System.Drawing.Point(206, 292);
            this.labelConnectionRepository.Name = "labelConnectionRepository";
            this.labelConnectionRepository.Size = new System.Drawing.Size(57, 13);
            this.labelConnectionRepository.TabIndex = 8;
            this.labelConnectionRepository.Text = "Repository";
            // 
            // menuStripMainMenu
            // 
            this.menuStripMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.mapsToolStripMenuItem});
            this.menuStripMainMenu.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainMenu.Name = "menuStripMainMenu";
            this.menuStripMainMenu.Size = new System.Drawing.Size(993, 24);
            this.menuStripMainMenu.TabIndex = 9;
            this.menuStripMainMenu.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finishToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // finishToolStripMenuItem
            // 
            this.finishToolStripMenuItem.Name = "finishToolStripMenuItem";
            this.finishToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.finishToolStripMenuItem.Text = "Finish";
            this.finishToolStripMenuItem.Click += new System.EventHandler(this.finishToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repositoryConnectionToolStripMenuItem,
            this.activeSyncToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // repositoryConnectionToolStripMenuItem
            // 
            this.repositoryConnectionToolStripMenuItem.Name = "repositoryConnectionToolStripMenuItem";
            this.repositoryConnectionToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.repositoryConnectionToolStripMenuItem.Text = "Repository Connection";
            this.repositoryConnectionToolStripMenuItem.Click += new System.EventHandler(this.repositoryConnectionToolStripMenuItem_Click);
            // 
            // activeSyncToolStripMenuItem
            // 
            this.activeSyncToolStripMenuItem.Name = "activeSyncToolStripMenuItem";
            this.activeSyncToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.activeSyncToolStripMenuItem.Text = "Active Sync";
            this.activeSyncToolStripMenuItem.Click += new System.EventHandler(this.activeSyncToolStripMenuItem_Click);
            // 
            // mapsToolStripMenuItem
            // 
            this.mapsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleMapsToolStripMenuItem,
            this.localMapsToolStripMenuItem});
            this.mapsToolStripMenuItem.Name = "mapsToolStripMenuItem";
            this.mapsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.mapsToolStripMenuItem.Text = "Maps";
            // 
            // googleMapsToolStripMenuItem
            // 
            this.googleMapsToolStripMenuItem.Name = "googleMapsToolStripMenuItem";
            this.googleMapsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.googleMapsToolStripMenuItem.Text = "Google Maps";
            this.googleMapsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemGoogleMaps_Click);
            // 
            // localMapsToolStripMenuItem
            // 
            this.localMapsToolStripMenuItem.Name = "localMapsToolStripMenuItem";
            this.localMapsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.localMapsToolStripMenuItem.Text = "Local Maps";
            this.localMapsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemlocalMaps_Click);
            // 
            // labelConnectionMobile
            // 
            this.labelConnectionMobile.AutoSize = true;
            this.labelConnectionMobile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectionMobile.Location = new System.Drawing.Point(188, 338);
            this.labelConnectionMobile.Name = "labelConnectionMobile";
            this.labelConnectionMobile.Size = new System.Drawing.Size(199, 16);
            this.labelConnectionMobile.TabIndex = 10;
            this.labelConnectionMobile.Text = "Not Connected to MobileDB";
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(678, 354);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(75, 23);
            this.buttonChoose.TabIndex = 19;
            this.buttonChoose.Text = "Choose";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Visible = false;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChooseProject_Click);
            // 
            // labelProjectName
            // 
            this.labelProjectName.AutoSize = true;
            this.labelProjectName.Location = new System.Drawing.Point(773, 354);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(71, 13);
            this.labelProjectName.TabIndex = 18;
            this.labelProjectName.Text = "Project Name";
            // 
            // listBoxChooseProject
            // 
            this.listBoxChooseProject.FormattingEnabled = true;
            this.listBoxChooseProject.Location = new System.Drawing.Point(503, 321);
            this.listBoxChooseProject.Name = "listBoxChooseProject";
            this.listBoxChooseProject.Size = new System.Drawing.Size(169, 56);
            this.listBoxChooseProject.TabIndex = 17;
            this.listBoxChooseProject.Visible = false;
            // 
            // labelCurrentProject
            // 
            this.labelCurrentProject.AutoSize = true;
            this.labelCurrentProject.Location = new System.Drawing.Point(773, 331);
            this.labelCurrentProject.Name = "labelCurrentProject";
            this.labelCurrentProject.Size = new System.Drawing.Size(43, 13);
            this.labelCurrentProject.TabIndex = 16;
            this.labelCurrentProject.Text = "Project:";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(773, 297);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(60, 13);
            this.labelUserName.TabIndex = 15;
            this.labelUserName.Text = "User Name";
            // 
            // buttonChooseProject
            // 
            this.buttonChooseProject.BackColor = System.Drawing.SystemColors.Control;
            this.buttonChooseProject.Enabled = false;
            this.buttonChooseProject.Location = new System.Drawing.Point(503, 292);
            this.buttonChooseProject.Name = "buttonChooseProject";
            this.buttonChooseProject.Size = new System.Drawing.Size(169, 23);
            this.buttonChooseProject.TabIndex = 14;
            this.buttonChooseProject.Text = "List Projects";
            this.buttonChooseProject.UseVisualStyleBackColor = false;
            this.buttonChooseProject.Click += new System.EventHandler(this.listBoxChooseProject_DoubleClick);
            // 
            // buttonGetTaxa
            // 
            this.buttonGetTaxa.BackColor = System.Drawing.SystemColors.Control;
            this.buttonGetTaxa.Enabled = false;
            this.buttonGetTaxa.Location = new System.Drawing.Point(13, 423);
            this.buttonGetTaxa.Name = "buttonGetTaxa";
            this.buttonGetTaxa.Size = new System.Drawing.Size(145, 23);
            this.buttonGetTaxa.TabIndex = 20;
            this.buttonGetTaxa.Text = "Get Taxon Definitions";
            this.buttonGetTaxa.UseVisualStyleBackColor = false;
            this.buttonGetTaxa.Click += new System.EventHandler(this.buttonGetTaxa_Click);
            // 
            // buttonGetDefinitions
            // 
            this.buttonGetDefinitions.BackColor = System.Drawing.SystemColors.Control;
            this.buttonGetDefinitions.Enabled = false;
            this.buttonGetDefinitions.Location = new System.Drawing.Point(209, 423);
            this.buttonGetDefinitions.Name = "buttonGetDefinitions";
            this.buttonGetDefinitions.Size = new System.Drawing.Size(190, 23);
            this.buttonGetDefinitions.TabIndex = 21;
            this.buttonGetDefinitions.Text = "Get DiversityCollection Definitions";
            this.buttonGetDefinitions.UseVisualStyleBackColor = false;
            this.buttonGetDefinitions.Click += new System.EventHandler(this.buttonGetDefinitions_Click);
            // 
            // buttonDownLoad
            // 
            this.buttonDownLoad.BackColor = System.Drawing.SystemColors.Control;
            this.buttonDownLoad.Enabled = false;
            this.buttonDownLoad.Location = new System.Drawing.Point(697, 423);
            this.buttonDownLoad.Name = "buttonDownLoad";
            this.buttonDownLoad.Size = new System.Drawing.Size(136, 23);
            this.buttonDownLoad.TabIndex = 22;
            this.buttonDownLoad.Text = "Download Field Data";
            this.buttonDownLoad.UseVisualStyleBackColor = false;
            this.buttonDownLoad.Click += new System.EventHandler(this.buttonDownLoad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 492);
            this.Controls.Add(this.buttonDownLoad);
            this.Controls.Add(this.buttonGetDefinitions);
            this.Controls.Add(this.buttonGetTaxa);
            this.Controls.Add(this.buttonChoose);
            this.Controls.Add(this.labelProjectName);
            this.Controls.Add(this.listBoxChooseProject);
            this.Controls.Add(this.labelCurrentProject);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.buttonChooseProject);
            this.Controls.Add(this.labelConnectionMobile);
            this.Controls.Add(this.labelConnectionRepository);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.buttonConnectMobile);
            this.Controls.Add(this.buttonConnectRepository);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStripMainMenu);
            this.MainMenuStrip = this.menuStripMainMenu;
            this.Name = "Form1";
            this.Text = "MediaService Client";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStripMainMenu.ResumeLayout(false);
            this.menuStripMainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonConnectRepository;
        private System.Windows.Forms.Button buttonConnectMobile;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Label labelConnectionRepository;
        private System.Windows.Forms.MenuStrip menuStripMainMenu;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem repositoryConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeSyncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localMapsToolStripMenuItem;
        private System.Windows.Forms.Label labelConnectionMobile;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.ListBox listBoxChooseProject;
        private System.Windows.Forms.Label labelCurrentProject;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Button buttonChooseProject;
        private System.Windows.Forms.Button buttonGetTaxa;
        private System.Windows.Forms.Button buttonGetDefinitions;
        private System.Windows.Forms.Button buttonDownLoad;
    }
}

