using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    partial class NewIdentificationUnitDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewIdentificationUnitDialog));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelIdentification = new System.Windows.Forms.Label();
            this.labelTaxonomicGroupCaption = new System.Windows.Forms.Label();
            this.labelRelationCaption = new System.Windows.Forms.Label();
            this.pictureBoxIUImage = new System.Windows.Forms.PictureBox();
            this.labelSynonym = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ImageList();
            this.imageListMedium = new System.Windows.Forms.ImageList();
            this.imageListLarge = new System.Windows.Forms.ImageList();
            this.comboBoxRelation = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.comboBoxIdentification = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.comboBoxTaxonomicGroup = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonNext = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxFormImage = new System.Windows.Forms.PictureBox();
            this.labelCaption = new System.Windows.Forms.Label();
            this.buttonNewAnalysis = new System.Windows.Forms.Button();
            this.textBoxUnitIdentifier = new System.Windows.Forms.TextBox();
            this.labelUnitIdentifierCaption = new System.Windows.Forms.Label();
            this.comboBoxUnitDescription = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.labelUnitDescriptionCaption = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.Location = new System.Drawing.Point(110, 361);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(92, 20);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonOk.Location = new System.Drawing.Point(8, 361);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(92, 20);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // labelIdentification
            // 
            this.labelIdentification.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelIdentification.Location = new System.Drawing.Point(10, 130);
            this.labelIdentification.Name = "labelIdentification";
            this.labelIdentification.Size = new System.Drawing.Size(80, 16);
            this.labelIdentification.Text = "Identification:";
            // 
            // labelTaxonomicGroupCaption
            // 
            this.labelTaxonomicGroupCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelTaxonomicGroupCaption.Location = new System.Drawing.Point(9, 51);
            this.labelTaxonomicGroupCaption.Name = "labelTaxonomicGroupCaption";
            this.labelTaxonomicGroupCaption.Size = new System.Drawing.Size(65, 20);
            this.labelTaxonomicGroupCaption.Text = "Tax. Group:";
            // 
            // labelRelationCaption
            // 
            this.labelRelationCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelRelationCaption.Location = new System.Drawing.Point(10, 245);
            this.labelRelationCaption.Name = "labelRelationCaption";
            this.labelRelationCaption.Size = new System.Drawing.Size(65, 21);
            this.labelRelationCaption.Text = "Relation:";
            // 
            // pictureBoxIUImage
            // 
            this.pictureBoxIUImage.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxIUImage.Location = new System.Drawing.Point(84, 39);
            this.pictureBoxIUImage.Name = "pictureBoxIUImage";
            this.pictureBoxIUImage.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxIUImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // labelSynonym
            // 
            this.labelSynonym.BackColor = System.Drawing.Color.White;
            this.labelSynonym.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.labelSynonym.Location = new System.Drawing.Point(111, 159);
            this.labelSynonym.Name = "labelSynonym";
            this.labelSynonym.Size = new System.Drawing.Size(92, 17);
            this.labelSynonym.Text = "Working Name";
            this.labelSynonym.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.imageList.Images.Clear();
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource7"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource8"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource9"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource10"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource11"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource12"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource13"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource14"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource15"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource16"))));
            this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource17"))));
            // 
            // imageListMedium
            // 
            this.imageListMedium.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListMedium.Images.Clear();
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource18"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource19"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource20"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource21"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource22"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource23"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource24"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource25"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource26"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource27"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource28"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource29"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource30"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource31"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource32"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource33"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource34"))));
            this.imageListMedium.Images.Add(((System.Drawing.Image)(resources.GetObject("resource35"))));
            // 
            // imageListLarge
            // 
            this.imageListLarge.ImageSize = new System.Drawing.Size(40, 40);
            this.imageListLarge.Images.Clear();
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource36"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource37"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource38"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource39"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource40"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource41"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource42"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource43"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource44"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource45"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource46"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource47"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource48"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource49"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource50"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource51"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource52"))));
            this.imageListLarge.Images.Add(((System.Drawing.Image)(resources.GetObject("resource53"))));
            // 
            // comboBoxRelation
            // 
            this.comboBoxRelation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.comboBoxRelation.Location = new System.Drawing.Point(84, 233);
            this.comboBoxRelation.Name = "comboBoxRelation";
            this.comboBoxRelation.Size = new System.Drawing.Size(118, 34);
            this.comboBoxRelation.TabIndex = 5;
            // 
            // comboBoxIdentification
            // 
            this.comboBoxIdentification.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.comboBoxIdentification.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxIdentification.Location = new System.Drawing.Point(9, 193);
            this.comboBoxIdentification.Name = "comboBoxIdentification";
            this.comboBoxIdentification.Size = new System.Drawing.Size(194, 29);
            this.comboBoxIdentification.TabIndex = 3;
            this.comboBoxIdentification.TextChanged += new System.EventHandler(this.comboBoxIdentification_TextChanged);
            // 
            // comboBoxTaxonomicGroup
            // 
            this.comboBoxTaxonomicGroup.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.comboBoxTaxonomicGroup.Location = new System.Drawing.Point(9, 77);
            this.comboBoxTaxonomicGroup.Name = "comboBoxTaxonomicGroup";
            this.comboBoxTaxonomicGroup.Size = new System.Drawing.Size(193, 42);
            this.comboBoxTaxonomicGroup.TabIndex = 1;
            this.comboBoxTaxonomicGroup.TextChanged += new System.EventHandler(this.comboBoxTaxonomicGroup_TextChanged);
            this.comboBoxTaxonomicGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxTaxonomicGroup_SelectedIndexChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearch.Location = new System.Drawing.Point(97, 128);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(103, 19);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            this.textBoxSearch.GotFocus += new System.EventHandler(this.textBoxSearch_GotFocus);
            // 
            // buttonNext
            // 
            this.buttonNext.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonNext.Location = new System.Drawing.Point(8, 400);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(92, 20);
            this.buttonNext.TabIndex = 9;
            this.buttonNext.Text = "Next IU";
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.Info;
            this.panelTop.Controls.Add(this.pictureBoxFormImage);
            this.panelTop.Controls.Add(this.labelCaption);
            this.panelTop.Location = new System.Drawing.Point(1, 1);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(215, 32);
            // 
            // pictureBoxFormImage
            // 
            this.pictureBoxFormImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxFormImage.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxFormImage.Image")));
            this.pictureBoxFormImage.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFormImage.Name = "pictureBoxFormImage";
            this.pictureBoxFormImage.Size = new System.Drawing.Size(40, 32);
            this.pictureBoxFormImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // labelCaption
            // 
            this.labelCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelCaption.Location = new System.Drawing.Point(49, 6);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(152, 20);
            this.labelCaption.Text = "Identification Unit";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonNewAnalysis
            // 
            this.buttonNewAnalysis.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonNewAnalysis.Location = new System.Drawing.Point(110, 400);
            this.buttonNewAnalysis.Name = "buttonNewAnalysis";
            this.buttonNewAnalysis.Size = new System.Drawing.Size(92, 20);
            this.buttonNewAnalysis.TabIndex = 16;
            this.buttonNewAnalysis.Text = "New Analysis";
            this.buttonNewAnalysis.Click += new System.EventHandler(this.buttonNewAnalysis_Click);
            // 
            // textBoxUnitIdentifier
            // 
            this.textBoxUnitIdentifier.Location = new System.Drawing.Point(108, 282);
            this.textBoxUnitIdentifier.Name = "textBoxUnitIdentifier";
            this.textBoxUnitIdentifier.Size = new System.Drawing.Size(92, 21);
            this.textBoxUnitIdentifier.TabIndex = 25;
            this.textBoxUnitIdentifier.Visible = false;
            this.textBoxUnitIdentifier.GotFocus += new System.EventHandler(this.textBoxUnitIdentifier_GotFocus);
            // 
            // labelUnitIdentifierCaption
            // 
            this.labelUnitIdentifierCaption.Location = new System.Drawing.Point(10, 285);
            this.labelUnitIdentifierCaption.Name = "labelUnitIdentifierCaption";
            this.labelUnitIdentifierCaption.Size = new System.Drawing.Size(92, 20);
            this.labelUnitIdentifierCaption.Text = "Identifier:";
            this.labelUnitIdentifierCaption.Visible = false;
            // 
            // comboBoxUnitDescription
            // 
            this.comboBoxUnitDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxUnitDescription.Location = new System.Drawing.Point(107, 321);
            this.comboBoxUnitDescription.Name = "comboBoxUnitDescription";
            this.comboBoxUnitDescription.Size = new System.Drawing.Size(92, 20);
            this.comboBoxUnitDescription.TabIndex = 27;
            this.comboBoxUnitDescription.Visible = false;
            // 
            // labelUnitDescriptionCaption
            // 
            this.labelUnitDescriptionCaption.Location = new System.Drawing.Point(10, 322);
            this.labelUnitDescriptionCaption.Name = "labelUnitDescriptionCaption";
            this.labelUnitDescriptionCaption.Size = new System.Drawing.Size(92, 20);
            this.labelUnitDescriptionCaption.Text = "Description:";
            this.labelUnitDescriptionCaption.Visible = false;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(10, 162);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(72, 14);
            this.buttonSearch.TabIndex = 35;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // NewIdentificationUnitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(230, 381);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelUnitDescriptionCaption);
            this.Controls.Add(this.comboBoxUnitDescription);
            this.Controls.Add(this.labelUnitIdentifierCaption);
            this.Controls.Add(this.textBoxUnitIdentifier);
            this.Controls.Add(this.buttonNewAnalysis);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.comboBoxRelation);
            this.Controls.Add(this.comboBoxIdentification);
            this.Controls.Add(this.comboBoxTaxonomicGroup);
            this.Controls.Add(this.labelSynonym);
            this.Controls.Add(this.pictureBoxIUImage);
            this.Controls.Add(this.labelRelationCaption);
            this.Controls.Add(this.labelTaxonomicGroupCaption);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelIdentification);
            this.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "NewIdentificationUnitDialog";
            this.Text = "Diversity Mobile";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.NewIdentificationUnitDialog_Closing);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;        
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelIdentification;
        private System.Windows.Forms.Label labelTaxonomicGroupCaption;
        private System.Windows.Forms.Label labelRelationCaption;
        private System.Windows.Forms.PictureBox pictureBoxIUImage;
        private System.Windows.Forms.Label labelSynonym;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ImageList imageListMedium;
        private System.Windows.Forms.ImageList imageListLarge;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxTaxonomicGroup;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxIdentification;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxRelation;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxFormImage;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Button buttonNewAnalysis;
        private System.Windows.Forms.TextBox textBoxUnitIdentifier;
        private System.Windows.Forms.Label labelUnitIdentifierCaption;
        private System.Windows.Forms.Label labelUnitDescriptionCaption;
        private System.Windows.Forms.Button buttonSearch;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxUnitDescription;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}