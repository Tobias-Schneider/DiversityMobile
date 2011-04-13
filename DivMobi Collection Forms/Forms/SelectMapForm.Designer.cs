namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    partial class SelectMapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectMapForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.imageListGoogleMaps = new System.Windows.Forms.ImageList();
            this.imageListMediumGoogleMaps = new System.Windows.Forms.ImageList();
            this.imageListLargeGoogleMaps = new System.Windows.Forms.ImageList();
            this.toolBarGoogleMapForm = new System.Windows.Forms.ToolBar();
            this.toolBarButtonShowMap = new System.Windows.Forms.ToolBarButton();
            this.toolBarSeparator = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonCloseMap = new System.Windows.Forms.ToolBarButton();
            this.labelStoredLongitude = new System.Windows.Forms.Label();
            this.panelData = new System.Windows.Forms.Panel();
            this.labelActualLongitude = new System.Windows.Forms.Label();
            this.labelActualGPS = new System.Windows.Forms.Label();
            this.labelActualLatitude = new System.Windows.Forms.Label();
            this.labelStoredGPSCaption = new System.Windows.Forms.Label();
            this.checkBoxShowAll = new System.Windows.Forms.CheckBox();
            this.labelStoredLatitude = new System.Windows.Forms.Label();
            this.listViewMaps = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnLatTop = new System.Windows.Forms.ColumnHeader();
            this.columnLatBottom = new System.Windows.Forms.ColumnHeader();
            this.columnLongLeft = new System.Windows.Forms.ColumnHeader();
            this.columnLongRight = new System.Windows.Forms.ColumnHeader();
            this.panelData.SuspendLayout();
            this.SuspendLayout();
            this.imageListGoogleMaps.Images.Clear();
            this.imageListGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageListGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageListGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageListGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            // 
            // imageListMediumGoogleMaps
            // 
            this.imageListMediumGoogleMaps.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListMediumGoogleMaps.Images.Clear();
            this.imageListMediumGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.imageListMediumGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            this.imageListMediumGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
            this.imageListMediumGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource7"))));
            // 
            // imageListLargeGoogleMaps
            // 
            this.imageListLargeGoogleMaps.ImageSize = new System.Drawing.Size(40, 40);
            this.imageListLargeGoogleMaps.Images.Clear();
            this.imageListLargeGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource8"))));
            this.imageListLargeGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource9"))));
            this.imageListLargeGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource10"))));
            this.imageListLargeGoogleMaps.Images.Add(((System.Drawing.Image)(resources.GetObject("resource11"))));
            // 
            // toolBarGoogleMapForm
            // 
            this.toolBarGoogleMapForm.Buttons.Add(this.toolBarButtonShowMap);
            this.toolBarGoogleMapForm.Buttons.Add(this.toolBarSeparator);
            this.toolBarGoogleMapForm.Buttons.Add(this.toolBarButtonCloseMap);
            this.toolBarGoogleMapForm.ImageList = this.imageListGoogleMaps;
            this.toolBarGoogleMapForm.Name = "toolBarGoogleMapForm";
            this.toolBarGoogleMapForm.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarGoogleMapForm_ButtonClick);
            // 
            // toolBarButtonShowMap
            // 
            this.toolBarButtonShowMap.Enabled = false;
            this.toolBarButtonShowMap.ImageIndex = 1;
            // 
            // toolBarSeparator
            // 
            this.toolBarSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonCloseMap
            // 
            this.toolBarButtonCloseMap.Enabled = false;
            this.toolBarButtonCloseMap.ImageIndex = 3;
            this.toolBarButtonCloseMap.ToolTipText = "Close Map";
            // 
            // labelStoredLongitude
            // 
            this.labelStoredLongitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelStoredLongitude.Location = new System.Drawing.Point(113, 44);
            this.labelStoredLongitude.Name = "labelStoredLongitude";
            this.labelStoredLongitude.Size = new System.Drawing.Size(89, 20);
            this.labelStoredLongitude.Text = "Long:";
            // 
            // panelData
            // 
            this.panelData.Controls.Add(this.labelActualLongitude);
            this.panelData.Controls.Add(this.labelActualGPS);
            this.panelData.Controls.Add(this.labelActualLatitude);
            this.panelData.Controls.Add(this.labelStoredLongitude);
            this.panelData.Controls.Add(this.labelStoredGPSCaption);
            this.panelData.Controls.Add(this.checkBoxShowAll);
            this.panelData.Controls.Add(this.labelStoredLatitude);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(246, 91);
            // 
            // labelActualLongitude
            // 
            this.labelActualLongitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelActualLongitude.Location = new System.Drawing.Point(3, 44);
            this.labelActualLongitude.Name = "labelActualLongitude";
            this.labelActualLongitude.Size = new System.Drawing.Size(89, 20);
            this.labelActualLongitude.Text = "Long:";
            // 
            // labelActualGPS
            // 
            this.labelActualGPS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.labelActualGPS.Location = new System.Drawing.Point(5, 4);
            this.labelActualGPS.Name = "labelActualGPS";
            this.labelActualGPS.Size = new System.Drawing.Size(89, 20);
            this.labelActualGPS.Text = "Actual Data";
            // 
            // labelActualLatitude
            // 
            this.labelActualLatitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelActualLatitude.Location = new System.Drawing.Point(3, 24);
            this.labelActualLatitude.Name = "labelActualLatitude";
            this.labelActualLatitude.Size = new System.Drawing.Size(89, 20);
            this.labelActualLatitude.Text = "Lat:";
            // 
            // labelStoredGPSCaption
            // 
            this.labelStoredGPSCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.labelStoredGPSCaption.Location = new System.Drawing.Point(113, 4);
            this.labelStoredGPSCaption.Name = "labelStoredGPSCaption";
            this.labelStoredGPSCaption.Size = new System.Drawing.Size(89, 20);
            this.labelStoredGPSCaption.Text = "Stored Data";
            // 
            // checkBoxShowAll
            // 
            this.checkBoxShowAll.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.checkBoxShowAll.Location = new System.Drawing.Point(7, 67);
            this.checkBoxShowAll.Name = "checkBoxShowAll";
            this.checkBoxShowAll.Size = new System.Drawing.Size(100, 20);
            this.checkBoxShowAll.TabIndex = 6;
            this.checkBoxShowAll.Text = "List all maps";
            this.checkBoxShowAll.CheckStateChanged += new System.EventHandler(this.checkBoxShowAll_CheckStateChanged);
            // 
            // labelStoredLatitude
            // 
            this.labelStoredLatitude.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelStoredLatitude.Location = new System.Drawing.Point(113, 24);
            this.labelStoredLatitude.Name = "labelStoredLatitude";
            this.labelStoredLatitude.Size = new System.Drawing.Size(89, 20);
            this.labelStoredLatitude.Text = "Lat:";
            // 
            // listViewMaps
            // 
            this.listViewMaps.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listViewMaps.Columns.Add(this.columnName);
            this.listViewMaps.Columns.Add(this.columnLatTop);
            this.listViewMaps.Columns.Add(this.columnLatBottom);
            this.listViewMaps.Columns.Add(this.columnLongLeft);
            this.listViewMaps.Columns.Add(this.columnLongRight);
            this.listViewMaps.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewMaps.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listViewMaps.Location = new System.Drawing.Point(0, 97);
            this.listViewMaps.Name = "listViewMaps";
            this.listViewMaps.Size = new System.Drawing.Size(246, 115);
            this.listViewMaps.TabIndex = 8;
            this.listViewMaps.View = System.Windows.Forms.View.Details;
            this.listViewMaps.ItemActivate += new System.EventHandler(this.listViewMaps_ItemActivate);
            this.listViewMaps.SelectedIndexChanged += new System.EventHandler(this.listViewMaps_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 74;
            // 
            // columnLatTop
            // 
            this.columnLatTop.Text = "Lat Top";
            this.columnLatTop.Width = 60;
            // 
            // columnLatBottom
            // 
            this.columnLatBottom.Text = "Lat Bottom";
            this.columnLatBottom.Width = 60;
            // 
            // columnLongLeft
            // 
            this.columnLongLeft.Text = "Long Left";
            this.columnLongLeft.Width = 60;
            // 
            // columnLongRight
            // 
            this.columnLongRight.Text = "Long Right";
            this.columnLongRight.Width = 60;
            // 
            // SelectMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(246, 212);
            this.Controls.Add(this.listViewMaps);
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.toolBarGoogleMapForm);
            this.Menu = this.mainMenu1;
            this.Name = "SelectMapForm";
            this.Text = "Diversity Mobile";
            this.Load += new System.EventHandler(this.SelectMapForm_Load);
            this.panelData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.ImageList imageListGoogleMaps;
        private System.Windows.Forms.ImageList imageListMediumGoogleMaps;
        private System.Windows.Forms.ImageList imageListLargeGoogleMaps;
        private System.Windows.Forms.ToolBar toolBarGoogleMapForm;
        private System.Windows.Forms.ToolBarButton toolBarButtonShowMap;
        private System.Windows.Forms.ToolBarButton toolBarSeparator;
        private System.Windows.Forms.ToolBarButton toolBarButtonCloseMap;
        private System.Windows.Forms.Label labelStoredLongitude;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.Label labelActualLongitude;
        private System.Windows.Forms.Label labelActualGPS;
        private System.Windows.Forms.Label labelActualLatitude;
        private System.Windows.Forms.Label labelStoredGPSCaption;
        private System.Windows.Forms.CheckBox checkBoxShowAll;
        private System.Windows.Forms.Label labelStoredLatitude;
        private System.Windows.Forms.ListView listViewMaps;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnLongLeft;
        private System.Windows.Forms.ColumnHeader columnLongRight;
        private System.Windows.Forms.ColumnHeader columnLatTop;
        private System.Windows.Forms.ColumnHeader columnLatBottom;
    }
}