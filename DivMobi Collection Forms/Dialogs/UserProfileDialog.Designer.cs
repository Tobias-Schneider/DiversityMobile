namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class UserProfileDialog
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
            this.tabControlGeneral = new System.Windows.Forms.TabControl();
            this.tabPageUserData = new System.Windows.Forms.TabPage();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelProjectName = new System.Windows.Forms.Label();
            this.labelProjectCaption = new System.Windows.Forms.Label();
            this.textBoxNameCache = new System.Windows.Forms.TextBox();
            this.labelNameCache = new System.Windows.Forms.Label();
            this.textBoxUserLoginName = new System.Windows.Forms.TextBox();
            this.labelNameCaption = new System.Windows.Forms.Label();
            this.labelCaptionUserData = new System.Windows.Forms.Label();
            this.tabPageProperties = new System.Windows.Forms.TabPage();
            this.comboBoxContext = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.comboBoxLanguageContext = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.labelContext = new System.Windows.Forms.Label();
            this.labelLanguageContext = new System.Windows.Forms.Label();
            this.checkBoxDefaultIUGeoAnalysis = new System.Windows.Forms.CheckBox();
            this.checkBoxStopGPS = new System.Windows.Forms.CheckBox();
            this.labelStopGps = new System.Windows.Forms.Label();
            this.tabPageTreeViewProperties = new System.Windows.Forms.TabPage();
            this.comboBoxDisplayLevel = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.comboBoxToolbarIcons = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.labelExpandLevel = new System.Windows.Forms.Label();
            this.labelToolbarIcons = new System.Windows.Forms.Label();
            this.labelCaptionTreeView = new System.Windows.Forms.Label();
            this.tabPageTaxonList = new System.Windows.Forms.TabPage();
            this.labelSelectedTaxonomy = new System.Windows.Forms.Label();
            this.listBoxTaxonTables = new System.Windows.Forms.ListBox();
            this.labelTaxonomyGroup = new System.Windows.Forms.Label();
            this.comboBoxTaxonomyGroup = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.tabControlGeneral.SuspendLayout();
            this.tabPageUserData.SuspendLayout();
            this.tabPageProperties.SuspendLayout();
            this.tabPageTreeViewProperties.SuspendLayout();
            this.tabPageTaxonList.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlGeneral
            // 
            this.tabControlGeneral.Controls.Add(this.tabPageUserData);
            this.tabControlGeneral.Controls.Add(this.tabPageProperties);
            this.tabControlGeneral.Controls.Add(this.tabPageTreeViewProperties);
            this.tabControlGeneral.Controls.Add(this.tabPageTaxonList);
            this.tabControlGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGeneral.Location = new System.Drawing.Point(0, 0);
            this.tabControlGeneral.Name = "tabControlGeneral";
            this.tabControlGeneral.SelectedIndex = 0;
            this.tabControlGeneral.Size = new System.Drawing.Size(238, 150);
            this.tabControlGeneral.TabIndex = 0;
            // 
            // tabPageUserData
            // 
            this.tabPageUserData.Controls.Add(this.labelVersion);
            this.tabPageUserData.Controls.Add(this.labelProjectName);
            this.tabPageUserData.Controls.Add(this.labelProjectCaption);
            this.tabPageUserData.Controls.Add(this.textBoxNameCache);
            this.tabPageUserData.Controls.Add(this.labelNameCache);
            this.tabPageUserData.Controls.Add(this.textBoxUserLoginName);
            this.tabPageUserData.Controls.Add(this.labelNameCaption);
            this.tabPageUserData.Controls.Add(this.labelCaptionUserData);
            this.tabPageUserData.Location = new System.Drawing.Point(0, 0);
            this.tabPageUserData.Name = "tabPageUserData";
            this.tabPageUserData.Size = new System.Drawing.Size(238, 151);
            this.tabPageUserData.Text = "General";
            // 
            // labelVersion
            // 
            this.labelVersion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelVersion.Location = new System.Drawing.Point(7, 118);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(216, 20);
            this.labelVersion.Text = "Diversity Mobile Version: 1.18 S";
            // 
            // labelProjectName
            // 
            this.labelProjectName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelProjectName.Location = new System.Drawing.Point(77, 84);
            this.labelProjectName.Name = "labelProjectName";
            this.labelProjectName.Size = new System.Drawing.Size(146, 20);
            // 
            // labelProjectCaption
            // 
            this.labelProjectCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelProjectCaption.Location = new System.Drawing.Point(7, 84);
            this.labelProjectCaption.Name = "labelProjectCaption";
            this.labelProjectCaption.Size = new System.Drawing.Size(65, 20);
            this.labelProjectCaption.Text = "Project:";
            // 
            // textBoxNameCache
            // 
            this.textBoxNameCache.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxNameCache.Location = new System.Drawing.Point(77, 52);
            this.textBoxNameCache.Name = "textBoxNameCache";
            this.textBoxNameCache.ReadOnly = true;
            this.textBoxNameCache.Size = new System.Drawing.Size(146, 19);
            this.textBoxNameCache.TabIndex = 2;
            // 
            // labelNameCache
            // 
            this.labelNameCache.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelNameCache.Location = new System.Drawing.Point(7, 52);
            this.labelNameCache.Name = "labelNameCache";
            this.labelNameCache.Size = new System.Drawing.Size(65, 19);
            this.labelNameCache.Text = "Name:";
            // 
            // textBoxUserLoginName
            // 
            this.textBoxUserLoginName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxUserLoginName.Location = new System.Drawing.Point(77, 27);
            this.textBoxUserLoginName.MaxLength = 255;
            this.textBoxUserLoginName.Name = "textBoxUserLoginName";
            this.textBoxUserLoginName.ReadOnly = true;
            this.textBoxUserLoginName.Size = new System.Drawing.Size(146, 19);
            this.textBoxUserLoginName.TabIndex = 1;
            // 
            // labelNameCaption
            // 
            this.labelNameCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelNameCaption.Location = new System.Drawing.Point(7, 27);
            this.labelNameCaption.Name = "labelNameCaption";
            this.labelNameCaption.Size = new System.Drawing.Size(67, 19);
            this.labelNameCaption.Text = "Loginname:";
            // 
            // labelCaptionUserData
            // 
            this.labelCaptionUserData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaptionUserData.Location = new System.Drawing.Point(7, 4);
            this.labelCaptionUserData.Name = "labelCaptionUserData";
            this.labelCaptionUserData.Size = new System.Drawing.Size(216, 20);
            this.labelCaptionUserData.Text = "General User Data";
            this.labelCaptionUserData.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPageProperties
            // 
            this.tabPageProperties.Controls.Add(this.comboBoxContext);
            this.tabPageProperties.Controls.Add(this.comboBoxLanguageContext);
            this.tabPageProperties.Controls.Add(this.labelContext);
            this.tabPageProperties.Controls.Add(this.labelLanguageContext);
            this.tabPageProperties.Controls.Add(this.checkBoxDefaultIUGeoAnalysis);
            this.tabPageProperties.Controls.Add(this.checkBoxStopGPS);
            this.tabPageProperties.Controls.Add(this.labelStopGps);
            this.tabPageProperties.Location = new System.Drawing.Point(0, 0);
            this.tabPageProperties.Name = "tabPageProperties";
            this.tabPageProperties.Size = new System.Drawing.Size(238, 151);
            this.tabPageProperties.Text = "Properties";
            // 
            // comboBoxContext
            // 
            this.comboBoxContext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxContext.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.comboBoxContext.Location = new System.Drawing.Point(77, 55);
            this.comboBoxContext.Name = "comboBoxContext";
            this.comboBoxContext.Size = new System.Drawing.Size(131, 29);
            this.comboBoxContext.TabIndex = 2;
            // 
            // comboBoxLanguageContext
            // 
            this.comboBoxLanguageContext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxLanguageContext.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.comboBoxLanguageContext.Location = new System.Drawing.Point(77, 7);
            this.comboBoxLanguageContext.Name = "comboBoxLanguageContext";
            this.comboBoxLanguageContext.Size = new System.Drawing.Size(131, 33);
            this.comboBoxLanguageContext.TabIndex = 1;
            // 
            // labelContext
            // 
            this.labelContext.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelContext.Location = new System.Drawing.Point(10, 64);
            this.labelContext.Name = "labelContext";
            this.labelContext.Size = new System.Drawing.Size(65, 19);
            this.labelContext.Text = "Context:";
            // 
            // labelLanguageContext
            // 
            this.labelLanguageContext.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelLanguageContext.Location = new System.Drawing.Point(6, 14);
            this.labelLanguageContext.Name = "labelLanguageContext";
            this.labelLanguageContext.Size = new System.Drawing.Size(65, 19);
            this.labelLanguageContext.Text = "Language:";
            // 
            // checkBoxDefaultIUGeoAnalysis
            // 
            this.checkBoxDefaultIUGeoAnalysis.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxDefaultIUGeoAnalysis.Location = new System.Drawing.Point(10, 136);
            this.checkBoxDefaultIUGeoAnalysis.Name = "checkBoxDefaultIUGeoAnalysis";
            this.checkBoxDefaultIUGeoAnalysis.Size = new System.Drawing.Size(198, 25);
            this.checkBoxDefaultIUGeoAnalysis.TabIndex = 4;
            this.checkBoxDefaultIUGeoAnalysis.Text = "Insert geography for organism";
            // 
            // checkBoxStopGPS
            // 
            this.checkBoxStopGPS.Location = new System.Drawing.Point(10, 110);
            this.checkBoxStopGPS.Name = "checkBoxStopGPS";
            this.checkBoxStopGPS.Size = new System.Drawing.Size(27, 20);
            this.checkBoxStopGPS.TabIndex = 3;
            // 
            // labelStopGps
            // 
            this.labelStopGps.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelStopGps.Location = new System.Drawing.Point(40, 104);
            this.labelStopGps.Name = "labelStopGps";
            this.labelStopGps.Size = new System.Drawing.Size(145, 32);
            this.labelStopGps.Text = "Stop GPS when taking pictures";
            // 
            // tabPageTreeViewProperties
            // 
            this.tabPageTreeViewProperties.Controls.Add(this.comboBoxDisplayLevel);
            this.tabPageTreeViewProperties.Controls.Add(this.comboBoxToolbarIcons);
            this.tabPageTreeViewProperties.Controls.Add(this.labelExpandLevel);
            this.tabPageTreeViewProperties.Controls.Add(this.labelToolbarIcons);
            this.tabPageTreeViewProperties.Controls.Add(this.labelCaptionTreeView);
            this.tabPageTreeViewProperties.Location = new System.Drawing.Point(0, 0);
            this.tabPageTreeViewProperties.Name = "tabPageTreeViewProperties";
            this.tabPageTreeViewProperties.Size = new System.Drawing.Size(238, 151);
            this.tabPageTreeViewProperties.Text = "TreeView";
            // 
            // comboBoxDisplayLevel
            // 
            this.comboBoxDisplayLevel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.comboBoxDisplayLevel.Location = new System.Drawing.Point(91, 97);
            this.comboBoxDisplayLevel.Name = "comboBoxDisplayLevel";
            this.comboBoxDisplayLevel.Size = new System.Drawing.Size(123, 40);
            this.comboBoxDisplayLevel.TabIndex = 5;
            // 
            // comboBoxToolbarIcons
            // 
            this.comboBoxToolbarIcons.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.comboBoxToolbarIcons.Location = new System.Drawing.Point(91, 35);
            this.comboBoxToolbarIcons.Name = "comboBoxToolbarIcons";
            this.comboBoxToolbarIcons.Size = new System.Drawing.Size(123, 39);
            this.comboBoxToolbarIcons.TabIndex = 4;
            // 
            // labelExpandLevel
            // 
            this.labelExpandLevel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelExpandLevel.Location = new System.Drawing.Point(10, 109);
            this.labelExpandLevel.Name = "labelExpandLevel";
            this.labelExpandLevel.Size = new System.Drawing.Size(75, 20);
            this.labelExpandLevel.Text = "Expand Level";
            // 
            // labelToolbarIcons
            // 
            this.labelToolbarIcons.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelToolbarIcons.Location = new System.Drawing.Point(9, 45);
            this.labelToolbarIcons.Name = "labelToolbarIcons";
            this.labelToolbarIcons.Size = new System.Drawing.Size(76, 20);
            this.labelToolbarIcons.Text = "Toolbar Icons:";
            // 
            // labelCaptionTreeView
            // 
            this.labelCaptionTreeView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaptionTreeView.Location = new System.Drawing.Point(7, 4);
            this.labelCaptionTreeView.Name = "labelCaptionTreeView";
            this.labelCaptionTreeView.Size = new System.Drawing.Size(207, 20);
            this.labelCaptionTreeView.Text = "TreeView Settings";
            this.labelCaptionTreeView.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPageTaxonList
            // 
            this.tabPageTaxonList.Controls.Add(this.labelSelectedTaxonomy);
            this.tabPageTaxonList.Controls.Add(this.listBoxTaxonTables);
            this.tabPageTaxonList.Controls.Add(this.labelTaxonomyGroup);
            this.tabPageTaxonList.Controls.Add(this.comboBoxTaxonomyGroup);
            this.tabPageTaxonList.Location = new System.Drawing.Point(0, 0);
            this.tabPageTaxonList.Name = "tabPageTaxonList";
            this.tabPageTaxonList.Size = new System.Drawing.Size(238, 127);
            this.tabPageTaxonList.Text = "Taxonomy";
            this.tabPageTaxonList.GotFocus += new System.EventHandler(this.tabPageTaxonList_GotFocus);
            // 
            // labelSelectedTaxonomy
            // 
            this.labelSelectedTaxonomy.Location = new System.Drawing.Point(7, 78);
            this.labelSelectedTaxonomy.Name = "labelSelectedTaxonomy";
            this.labelSelectedTaxonomy.Size = new System.Drawing.Size(214, 20);
            this.labelSelectedTaxonomy.Text = "Selected Taxonomy";
            // 
            // listBoxTaxonTables
            // 
            this.listBoxTaxonTables.Enabled = false;
            this.listBoxTaxonTables.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listBoxTaxonTables.Location = new System.Drawing.Point(7, 112);
            this.listBoxTaxonTables.Name = "listBoxTaxonTables";
            this.listBoxTaxonTables.Size = new System.Drawing.Size(215, 28);
            this.listBoxTaxonTables.TabIndex = 2;
            this.listBoxTaxonTables.SelectedIndexChanged += new System.EventHandler(this.listBoxTaxonTables_SelectedIndexChanged);
            // 
            // labelTaxonomyGroup
            // 
            this.labelTaxonomyGroup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelTaxonomyGroup.Location = new System.Drawing.Point(7, 4);
            this.labelTaxonomyGroup.Name = "labelTaxonomyGroup";
            this.labelTaxonomyGroup.Size = new System.Drawing.Size(215, 18);
            this.labelTaxonomyGroup.Text = "Taxonomic Group:";
            // 
            // comboBoxTaxonomyGroup
            // 
            this.comboBoxTaxonomyGroup.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.comboBoxTaxonomyGroup.Location = new System.Drawing.Point(8, 25);
            this.comboBoxTaxonomyGroup.Name = "comboBoxTaxonomyGroup";
            this.comboBoxTaxonomyGroup.Size = new System.Drawing.Size(215, 46);
            this.comboBoxTaxonomyGroup.TabIndex = 1;
            this.comboBoxTaxonomyGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxTaxonomyGroup_SelectedIndexChanged);
            // 
            // UserProfileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 150);
            this.Controls.Add(this.tabControlGeneral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "UserProfileDialog";
            this.Text = "Diversity Mobile";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.UserProfileDialog_Closing);
            this.tabControlGeneral.ResumeLayout(false);
            this.tabPageUserData.ResumeLayout(false);
            this.tabPageProperties.ResumeLayout(false);
            this.tabPageTreeViewProperties.ResumeLayout(false);
            this.tabPageTaxonList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlGeneral;
        private System.Windows.Forms.TabPage tabPageUserData;
        private System.Windows.Forms.TextBox textBoxUserLoginName;
        private System.Windows.Forms.Label labelNameCaption;
        private System.Windows.Forms.TabPage tabPageTreeViewProperties;
        private System.Windows.Forms.Label labelCaptionUserData;
        private System.Windows.Forms.Label labelCaptionTreeView;
        private System.Windows.Forms.TabPage tabPageProperties;
        private System.Windows.Forms.Label labelNameCache;
        private System.Windows.Forms.TextBox textBoxNameCache;
        private System.Windows.Forms.Label labelToolbarIcons;
        private System.Windows.Forms.CheckBox checkBoxStopGPS;
        private System.Windows.Forms.Label labelStopGps;
        private System.Windows.Forms.CheckBox checkBoxDefaultIUGeoAnalysis;
        private System.Windows.Forms.Label labelExpandLevel;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.Label labelContext;
        private System.Windows.Forms.Label labelLanguageContext;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxContext;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxLanguageContext;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxDisplayLevel;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxToolbarIcons;
        private System.Windows.Forms.TabPage tabPageTaxonList;
        private System.Windows.Forms.Label labelTaxonomyGroup;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxTaxonomyGroup;
        private System.Windows.Forms.ListBox listBoxTaxonTables;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelProjectCaption;
        private System.Windows.Forms.Label labelSelectedTaxonomy;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;

    }
}
