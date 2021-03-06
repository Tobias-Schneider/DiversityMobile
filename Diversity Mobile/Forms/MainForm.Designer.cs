﻿namespace UBT.AI4.Bio.DiversityCollection.Mobile.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageListToolbarButtons = new System.Windows.Forms.ImageList();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.toolBarButtonMoveFirst = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPrevious = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonLevelPicture = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonNext = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonMoveLast = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSearch = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonUserProfile = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonEditContext = new System.Windows.Forms.ToolBarButton();
            this.imageListDiversityCollection = new System.Windows.Forms.ImageList();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuTreeView = new System.Windows.Forms.ContextMenu();
            this.menuItemShowMap = new System.Windows.Forms.MenuItem();
            this.menuItemTakePicture = new System.Windows.Forms.MenuItem();
            this.menuItemDisplayPicture = new System.Windows.Forms.MenuItem();
            this.menuItemTakeVideo = new System.Windows.Forms.MenuItem();
            this.menuItemShowVideo = new System.Windows.Forms.MenuItem();
            this.menuItemTakeAudio = new System.Windows.Forms.MenuItem();
            this.menuItemPlayAudio = new System.Windows.Forms.MenuItem();
            this.imageListMediumToolbarButtons = new System.Windows.Forms.ImageList();
            this.imageListMediumDiversityCollection = new System.Windows.Forms.ImageList();
            this.imageListLargeToolbarButtons = new System.Windows.Forms.ImageList();
            this.imageListLargeDiversityCollection = new System.Windows.Forms.ImageList();
            this.panelFieldDataCommands = new System.Windows.Forms.Panel();
            this.pictureBoxGPS = new System.Windows.Forms.PictureBox();
            this.pictureBoxHome = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewEvent = new System.Windows.Forms.PictureBox();
            this.pictureBoxDelete = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewEventSeries = new System.Windows.Forms.PictureBox();
            this.pictureBoxEdit = new System.Windows.Forms.PictureBox();
            this.labelPosition = new System.Windows.Forms.Label();
            this.pictureBoxNewEventProperty = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewIUGeoAnalysis = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewAnalysis = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewIdentificationUnit = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewSpecimen = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewLocalisation = new System.Windows.Forms.PictureBox();
            this.treeViewFieldData = new System.Windows.Forms.TreeView();
            this.panelFieldDataCommands.SuspendLayout();
            this.SuspendLayout();
            this.imageListToolbarButtons.Images.Clear();
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource7"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource8"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource9"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource10"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource11"))));
            this.imageListToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource12"))));
            // 
            // toolBar
            // 
            this.toolBar.Buttons.Add(this.toolBarButtonMoveFirst);
            this.toolBar.Buttons.Add(this.toolBarButtonPrevious);
            this.toolBar.Buttons.Add(this.toolBarButtonLevelPicture);
            this.toolBar.Buttons.Add(this.toolBarButtonNext);
            this.toolBar.Buttons.Add(this.toolBarButtonMoveLast);
            this.toolBar.Buttons.Add(this.toolBarButton1);
            this.toolBar.Buttons.Add(this.toolBarButtonSearch);
            this.toolBar.Buttons.Add(this.toolBarButtonUserProfile);
            this.toolBar.Buttons.Add(this.toolBarButtonEditContext);
            this.toolBar.ImageList = this.imageListToolbarButtons;
            this.toolBar.Name = "toolBar";
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // toolBarButtonMoveFirst
            // 
            this.toolBarButtonMoveFirst.ImageIndex = 0;
            this.toolBarButtonMoveFirst.ToolTipText = "Show first Collection Event";
            // 
            // toolBarButtonPrevious
            // 
            this.toolBarButtonPrevious.ImageIndex = 1;
            this.toolBarButtonPrevious.ToolTipText = "Show previous Collection Event";
            // 
            // toolBarButtonLevelPicture
            // 
            this.toolBarButtonLevelPicture.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // toolBarButtonNext
            // 
            this.toolBarButtonNext.ImageIndex = 2;
            this.toolBarButtonNext.ToolTipText = "Show next Collection Event";
            // 
            // toolBarButtonMoveLast
            // 
            this.toolBarButtonMoveLast.ImageIndex = 3;
            this.toolBarButtonMoveLast.ToolTipText = "Show last Collection Event";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonSearch
            // 
            this.toolBarButtonSearch.ImageIndex = 4;
            // 
            // toolBarButtonUserProfile
            // 
            this.toolBarButtonUserProfile.ImageIndex = 6;
            // 
            // toolBarButtonEditContext
            // 
            this.toolBarButtonEditContext.ImageIndex = 5;
            this.toolBarButtonEditContext.Visible = false;
            this.imageListDiversityCollection.Images.Clear();
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource13"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource14"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource15"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource16"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource17"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource18"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource19"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource20"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource21"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource22"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource23"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource24"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource25"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource26"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource27"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource28"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource29"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource30"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource31"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource32"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource33"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource34"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource35"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource36"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource37"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource38"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource39"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource40"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource41"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource42"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource43"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource44"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource45"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource46"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource47"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource48"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource49"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource50"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource51"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource52"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource53"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource54"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource55"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource56"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource57"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource58"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource59"))));
            this.imageListDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource60"))));
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 85);
            this.label1.Text = "What does that mean? Either there is no specimen selected in the tree or the curr" +
                "ently selected node does not have a specimen node as parent.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(20, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 110);
            this.label2.Text = "No Specimen selected";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // contextMenuTreeView
            // 
            this.contextMenuTreeView.MenuItems.Add(this.menuItemShowMap);
            this.contextMenuTreeView.MenuItems.Add(this.menuItemTakePicture);
            this.contextMenuTreeView.MenuItems.Add(this.menuItemDisplayPicture);
            this.contextMenuTreeView.MenuItems.Add(this.menuItemTakeVideo);
            this.contextMenuTreeView.MenuItems.Add(this.menuItemShowVideo);
            this.contextMenuTreeView.MenuItems.Add(this.menuItemTakeAudio);
            this.contextMenuTreeView.MenuItems.Add(this.menuItemPlayAudio);
            // 
            // menuItemShowMap
            // 
            this.menuItemShowMap.Text = "Show Map";
            this.menuItemShowMap.Click += new System.EventHandler(this.menuItemShowMap_Click);
            // 
            // menuItemTakePicture
            // 
            this.menuItemTakePicture.Text = "Take Picture";
            this.menuItemTakePicture.Click += new System.EventHandler(this.menuItemTakePicture_Click);
            // 
            // menuItemDisplayPicture
            // 
            this.menuItemDisplayPicture.Text = "Display Picture";
            this.menuItemDisplayPicture.Click += new System.EventHandler(this.menuItemDisplayPicture_Click);
            // 
            // menuItemTakeVideo
            // 
            this.menuItemTakeVideo.Text = "Take Video";
            this.menuItemTakeVideo.Click += new System.EventHandler(this.menuRecordVideo_Click);
            // 
            // menuItemShowVideo
            // 
            this.menuItemShowVideo.Text = "Show Video";
            this.menuItemShowVideo.Click += new System.EventHandler(this.menuItemShowVideo_Click);
            // 
            // menuItemTakeAudio
            // 
            this.menuItemTakeAudio.Text = "Record Audio";
            this.menuItemTakeAudio.Click += new System.EventHandler(this.menuItemTakeAudio_Click);
            // 
            // menuItemPlayAudio
            // 
            this.menuItemPlayAudio.Text = "Play Audio";
            this.menuItemPlayAudio.Click += new System.EventHandler(this.menuItemPlayAudio_Click);
            // 
            // imageListMediumToolbarButtons
            // 
            this.imageListMediumToolbarButtons.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListMediumToolbarButtons.Images.Clear();
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource61"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource62"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource63"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource64"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource65"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource66"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource67"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource68"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource69"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource70"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource71"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource72"))));
            this.imageListMediumToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource73"))));
            // 
            // imageListMediumDiversityCollection
            // 
            this.imageListMediumDiversityCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListMediumDiversityCollection.Images.Clear();
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource74"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource75"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource76"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource77"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource78"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource79"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource80"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource81"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource82"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource83"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource84"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource85"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource86"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource87"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource88"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource89"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource90"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource91"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource92"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource93"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource94"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource95"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource96"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource97"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource98"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource99"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource100"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource101"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource102"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource103"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource104"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource105"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource106"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource107"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource108"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource109"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource110"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource111"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource112"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource113"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource114"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource115"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource116"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource117"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource118"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource119"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource120"))));
            this.imageListMediumDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource121"))));
            // 
            // imageListLargeToolbarButtons
            // 
            this.imageListLargeToolbarButtons.ImageSize = new System.Drawing.Size(40, 40);
            this.imageListLargeToolbarButtons.Images.Clear();
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource122"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource123"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource124"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource125"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource126"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource127"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource128"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource129"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource130"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource131"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource132"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource133"))));
            this.imageListLargeToolbarButtons.Images.Add(((System.Drawing.Image)(resources.GetObject("resource134"))));
            // 
            // imageListLargeDiversityCollection
            // 
            this.imageListLargeDiversityCollection.ImageSize = new System.Drawing.Size(48, 48);
            this.imageListLargeDiversityCollection.Images.Clear();
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource135"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource136"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource137"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource138"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource139"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource140"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource141"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource142"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource143"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource144"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource145"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource146"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource147"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource148"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource149"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource150"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource151"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource152"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource153"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource154"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource155"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource156"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource157"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource158"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource159"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource160"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource161"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource162"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource163"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource164"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource165"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource166"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource167"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource168"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource169"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource170"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource171"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource172"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource173"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource174"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource175"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource176"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource177"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource178"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource179"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource180"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource181"))));
            this.imageListLargeDiversityCollection.Images.Add(((System.Drawing.Image)(resources.GetObject("resource182"))));
            // 
            // panelFieldDataCommands
            // 
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxGPS);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxHome);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxNewEvent);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxDelete);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxNewEventSeries);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxEdit);
            this.panelFieldDataCommands.Controls.Add(this.labelPosition);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxNewEventProperty);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxNewIUGeoAnalysis);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxNewAnalysis);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxNewIdentificationUnit);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxNewSpecimen);
            this.panelFieldDataCommands.Controls.Add(this.pictureBoxNewLocalisation);
            this.panelFieldDataCommands.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFieldDataCommands.Location = new System.Drawing.Point(0, 0);
            this.panelFieldDataCommands.Name = "panelFieldDataCommands";
            this.panelFieldDataCommands.Size = new System.Drawing.Size(240, 22);
            // 
            // pictureBoxGPS
            // 
            this.pictureBoxGPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxGPS.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxGPS.Image")));
            this.pictureBoxGPS.Location = new System.Drawing.Point(49, 3);
            this.pictureBoxGPS.Name = "pictureBoxGPS";
            this.pictureBoxGPS.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxGPS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxGPS.Click += new System.EventHandler(this.pictureBoxGPS_Click);
            // 
            // pictureBoxHome
            // 
            this.pictureBoxHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxHome.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxHome.Image")));
            this.pictureBoxHome.Location = new System.Drawing.Point(71, 3);
            this.pictureBoxHome.Name = "pictureBoxHome";
            this.pictureBoxHome.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxHome.Click += new System.EventHandler(this.pictureBoxHome_Click);
            // 
            // pictureBoxNewEvent
            // 
            this.pictureBoxNewEvent.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewEvent.Image")));
            this.pictureBoxNewEvent.Location = new System.Drawing.Point(155, 3);
            this.pictureBoxNewEvent.Name = "pictureBoxNewEvent";
            this.pictureBoxNewEvent.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxNewEvent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxNewEvent.Click += new System.EventHandler(this.pictureBoxNewEvent_Click);
            this.pictureBoxNewEvent.EnabledChanged += new System.EventHandler(this.pictureBoxNewEvent_EnabledChanged);
            // 
            // pictureBoxDelete
            // 
            this.pictureBoxDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxDelete.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxDelete.Image")));
            this.pictureBoxDelete.Location = new System.Drawing.Point(115, 3);
            this.pictureBoxDelete.Name = "pictureBoxDelete";
            this.pictureBoxDelete.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxDelete.Click += new System.EventHandler(this.pictureBoxDelete_Click);
            // 
            // pictureBoxNewEventSeries
            // 
            this.pictureBoxNewEventSeries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxNewEventSeries.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewEventSeries.Image")));
            this.pictureBoxNewEventSeries.Location = new System.Drawing.Point(155, 3);
            this.pictureBoxNewEventSeries.Name = "pictureBoxNewEventSeries";
            this.pictureBoxNewEventSeries.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxNewEventSeries.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxNewEventSeries.Click += new System.EventHandler(this.pictureBoxNewEventSeries_Click);
            this.pictureBoxNewEventSeries.EnabledChanged += new System.EventHandler(this.pictureBoxNewEventSeries_EnabledChanged);
            // 
            // pictureBoxEdit
            // 
            this.pictureBoxEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxEdit.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxEdit.Image")));
            this.pictureBoxEdit.Location = new System.Drawing.Point(93, 3);
            this.pictureBoxEdit.Name = "pictureBoxEdit";
            this.pictureBoxEdit.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxEdit.Click += new System.EventHandler(this.pictureBoxEdit_Click);
            // 
            // labelPosition
            // 
            this.labelPosition.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.labelPosition.Location = new System.Drawing.Point(4, 1);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(40, 20);
            // 
            // pictureBoxNewEventProperty
            // 
            this.pictureBoxNewEventProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxNewEventProperty.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewEventProperty.Image")));
            this.pictureBoxNewEventProperty.Location = new System.Drawing.Point(155, 3);
            this.pictureBoxNewEventProperty.Name = "pictureBoxNewEventProperty";
            this.pictureBoxNewEventProperty.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxNewEventProperty.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxNewEventProperty.Click += new System.EventHandler(this.pictureBoxNewEventProperty_Click);
            this.pictureBoxNewEventProperty.EnabledChanged += new System.EventHandler(this.pictureBoxNewEventProperty_EnabledChanged);
            // 
            // pictureBoxNewIUGeoAnalysis
            // 
            this.pictureBoxNewIUGeoAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxNewIUGeoAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewIUGeoAnalysis.Image")));
            this.pictureBoxNewIUGeoAnalysis.Location = new System.Drawing.Point(177, 3);
            this.pictureBoxNewIUGeoAnalysis.Name = "pictureBoxNewIUGeoAnalysis";
            this.pictureBoxNewIUGeoAnalysis.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxNewIUGeoAnalysis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxNewIUGeoAnalysis.Visible = false;
            this.pictureBoxNewIUGeoAnalysis.Click += new System.EventHandler(this.pictureBoxNewIUGeoAnalysis_Click);
            this.pictureBoxNewIUGeoAnalysis.EnabledChanged += new System.EventHandler(this.pictureBoxNewIUGeoAnalysis_EnabledChanged);
            // 
            // pictureBoxNewAnalysis
            // 
            this.pictureBoxNewAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxNewAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewAnalysis.Image")));
            this.pictureBoxNewAnalysis.Location = new System.Drawing.Point(221, 3);
            this.pictureBoxNewAnalysis.Name = "pictureBoxNewAnalysis";
            this.pictureBoxNewAnalysis.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxNewAnalysis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxNewAnalysis.Click += new System.EventHandler(this.pictureBoxNewAnalysis_Click);
            this.pictureBoxNewAnalysis.EnabledChanged += new System.EventHandler(this.pictureBoxNewAnalysis_EnabledChanged);
            // 
            // pictureBoxNewIdentificationUnit
            // 
            this.pictureBoxNewIdentificationUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxNewIdentificationUnit.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewIdentificationUnit.Image")));
            this.pictureBoxNewIdentificationUnit.Location = new System.Drawing.Point(199, 3);
            this.pictureBoxNewIdentificationUnit.Name = "pictureBoxNewIdentificationUnit";
            this.pictureBoxNewIdentificationUnit.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxNewIdentificationUnit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxNewIdentificationUnit.Click += new System.EventHandler(this.pictureBoxNewIdentificationUnit_Click);
            this.pictureBoxNewIdentificationUnit.EnabledChanged += new System.EventHandler(this.pictureBoxNewIdentificationUnit_EnabledChanged);
            // 
            // pictureBoxNewSpecimen
            // 
            this.pictureBoxNewSpecimen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxNewSpecimen.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewSpecimen.Image")));
            this.pictureBoxNewSpecimen.Location = new System.Drawing.Point(199, 3);
            this.pictureBoxNewSpecimen.Name = "pictureBoxNewSpecimen";
            this.pictureBoxNewSpecimen.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxNewSpecimen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxNewSpecimen.Click += new System.EventHandler(this.pictureBoxNewSpecimen_Click);
            this.pictureBoxNewSpecimen.EnabledChanged += new System.EventHandler(this.pictureBoxNewSpecimen_EnabledChanged);
            // 
            // pictureBoxNewLocalisation
            // 
            this.pictureBoxNewLocalisation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxNewLocalisation.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxNewLocalisation.Image")));
            this.pictureBoxNewLocalisation.Location = new System.Drawing.Point(177, 3);
            this.pictureBoxNewLocalisation.Name = "pictureBoxNewLocalisation";
            this.pictureBoxNewLocalisation.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxNewLocalisation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxNewLocalisation.Click += new System.EventHandler(this.pictureBoxNewLocalisation_Click);
            this.pictureBoxNewLocalisation.EnabledChanged += new System.EventHandler(this.pictureBoxNewLocalisation_EnabledChanged);
            // 
            // treeViewFieldData
            // 
            this.treeViewFieldData.ContextMenu = this.contextMenuTreeView;
            this.treeViewFieldData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFieldData.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.treeViewFieldData.ImageIndex = 0;
            this.treeViewFieldData.ImageList = this.imageListDiversityCollection;
            this.treeViewFieldData.Indent = 5;
            this.treeViewFieldData.Location = new System.Drawing.Point(0, 22);
            this.treeViewFieldData.Name = "treeViewFieldData";
            this.treeViewFieldData.SelectedImageIndex = 0;
            this.treeViewFieldData.Size = new System.Drawing.Size(240, 166);
            this.treeViewFieldData.TabIndex = 9;
            this.treeViewFieldData.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFieldData_AfterCollapse);
            this.treeViewFieldData.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewFieldData_BeforeExpand);
            this.treeViewFieldData.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewFieldData_BeforeCollapse);
            this.treeViewFieldData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFieldData_AfterSelect);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.treeViewFieldData);
            this.Controls.Add(this.panelFieldDataCommands);
            this.Controls.Add(this.toolBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Diversity Mobile";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panelFieldDataCommands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListToolbarButtons;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.ToolBarButton toolBarButtonMoveFirst;
        private System.Windows.Forms.ToolBarButton toolBarButtonPrevious;
        private System.Windows.Forms.ToolBarButton toolBarButtonNext;
        private System.Windows.Forms.ToolBarButton toolBarButtonMoveLast;
        private System.Windows.Forms.ImageList imageListDiversityCollection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenu contextMenuTreeView;
        private System.Windows.Forms.MenuItem menuItemShowMap;
        private System.Windows.Forms.MenuItem menuItemTakePicture;
        private System.Windows.Forms.MenuItem menuItemDisplayPicture;
        private System.Windows.Forms.ImageList imageListMediumToolbarButtons;
        private System.Windows.Forms.ImageList imageListMediumDiversityCollection;
        private System.Windows.Forms.ImageList imageListLargeToolbarButtons;
        private System.Windows.Forms.ImageList imageListLargeDiversityCollection;
        private System.Windows.Forms.MenuItem menuItemTakeVideo;
        private System.Windows.Forms.MenuItem menuItemShowVideo;
        private System.Windows.Forms.MenuItem menuItemTakeAudio;
        private System.Windows.Forms.MenuItem menuItemPlayAudio;
        private System.Windows.Forms.ToolBarButton toolBarButtonLevelPicture;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton toolBarButtonSearch;
        private System.Windows.Forms.ToolBarButton toolBarButtonUserProfile;
        private System.Windows.Forms.Panel panelFieldDataCommands;
        private System.Windows.Forms.PictureBox pictureBoxGPS;
        private System.Windows.Forms.PictureBox pictureBoxHome;
        private System.Windows.Forms.PictureBox pictureBoxNewEvent;
        private System.Windows.Forms.PictureBox pictureBoxDelete;
        private System.Windows.Forms.PictureBox pictureBoxNewEventSeries;
        private System.Windows.Forms.PictureBox pictureBoxEdit;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.PictureBox pictureBoxNewEventProperty;
        private System.Windows.Forms.PictureBox pictureBoxNewIUGeoAnalysis;
        private System.Windows.Forms.PictureBox pictureBoxNewAnalysis;
        private System.Windows.Forms.PictureBox pictureBoxNewIdentificationUnit;
        private System.Windows.Forms.PictureBox pictureBoxNewSpecimen;
        private System.Windows.Forms.PictureBox pictureBoxNewLocalisation;
        private System.Windows.Forms.TreeView treeViewFieldData;
        private System.Windows.Forms.ToolBarButton toolBarButtonEditContext;
    }
}

