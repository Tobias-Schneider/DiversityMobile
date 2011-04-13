namespace UserSyncGui
{
    partial class SelectFieldDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFieldDataForm));
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.listBoxActualSelection = new System.Windows.Forms.ListBox();
            this.textBoxLastIdentification = new System.Windows.Forms.TextBox();
            this.textBoxTaxonomicGroup = new System.Windows.Forms.TextBox();
            this.buttonSelection = new System.Windows.Forms.Button();
            this.buttonDeselection = new System.Windows.Forms.Button();
            this.treeViewResult = new System.Windows.Forms.TreeView();
            this.imageListDiversityCollection = new System.Windows.Forms.ImageList(this.components);
            this.treeActualViewSelection = new System.Windows.Forms.TreeView();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.textBoxUnitdescription = new System.Windows.Forms.TextBox();
            this.panelIdentificationUnits = new System.Windows.Forms.Panel();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelTaxonomicGroup = new System.Windows.Forms.Label();
            this.labelLastIdentification = new System.Windows.Forms.Label();
            this.labelIdentificationUnits = new System.Windows.Forms.Label();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.checkBoxTruncate = new System.Windows.Forms.CheckBox();
            this.comboBoxSelectType = new System.Windows.Forms.ComboBox();
            this.treeViewSelection = new System.Windows.Forms.TreeView();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.buttonSynchronize = new System.Windows.Forms.Button();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.labelSearchTypeCaption = new System.Windows.Forms.Label();
            this.panelEventSeries = new System.Windows.Forms.Panel();
            this.dateTimePickerEndStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDateStartEnd = new System.Windows.Forms.DateTimePicker();
            this.checkBoxESEnd = new System.Windows.Forms.CheckBox();
            this.checkBoxESStart = new System.Windows.Forms.CheckBox();
            this.dateTimePickerDateEndEnd = new System.Windows.Forms.DateTimePicker();
            this.labelDateEnd = new System.Windows.Forms.Label();
            this.dateTimePickerDateStartStart = new System.Windows.Forms.DateTimePicker();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.labelSeriesDescription = new System.Windows.Forms.Label();
            this.labelSeriesCode = new System.Windows.Forms.Label();
            this.labelEventSeriesCaption = new System.Windows.Forms.Label();
            this.textBoxSeriesCode = new System.Windows.Forms.TextBox();
            this.textBoxSeriesDescription = new System.Windows.Forms.TextBox();
            this.panelSamplingPlot = new System.Windows.Forms.Panel();
            this.dateTimePickerDeterminationDateEnd = new System.Windows.Forms.DateTimePicker();
            this.checkBoxDDate = new System.Windows.Forms.CheckBox();
            this.dateTimePickerDeterminationDateStart = new System.Windows.Forms.DateTimePicker();
            this.labelDeterminationDate = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.Label();
            this.labelSamplingPlotCaption = new System.Windows.Forms.Label();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelLogDate = new System.Windows.Forms.Label();
            this.dateTimePickerLogStart = new System.Windows.Forms.DateTimePicker();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.labelGeneralCaption = new System.Windows.Forms.Label();
            this.dateTimePickerLogEnd = new System.Windows.Forms.DateTimePicker();
            this.panelIdentificationUnits.SuspendLayout();
            this.panelEventSeries.SuspendLayout();
            this.panelSamplingPlot.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxResult
            // 
            this.listBoxResult.FormattingEnabled = true;
            this.listBoxResult.Location = new System.Drawing.Point(191, 15);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxResult.Size = new System.Drawing.Size(256, 381);
            this.listBoxResult.TabIndex = 0;
            this.listBoxResult.SelectedIndexChanged += new System.EventHandler(this.listBoxResult_SelectedIndexChanged);
            // 
            // listBoxActualSelection
            // 
            this.listBoxActualSelection.FormattingEnabled = true;
            this.listBoxActualSelection.Location = new System.Drawing.Point(599, 15);
            this.listBoxActualSelection.Name = "listBoxActualSelection";
            this.listBoxActualSelection.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxActualSelection.Size = new System.Drawing.Size(244, 381);
            this.listBoxActualSelection.TabIndex = 1;
            this.listBoxActualSelection.SelectedIndexChanged += new System.EventHandler(this.listBoxSelection_SelectedIndexChanged);
            // 
            // textBoxLastIdentification
            // 
            this.textBoxLastIdentification.Location = new System.Drawing.Point(12, 56);
            this.textBoxLastIdentification.Name = "textBoxLastIdentification";
            this.textBoxLastIdentification.Size = new System.Drawing.Size(135, 20);
            this.textBoxLastIdentification.TabIndex = 2;
            // 
            // textBoxTaxonomicGroup
            // 
            this.textBoxTaxonomicGroup.Location = new System.Drawing.Point(12, 101);
            this.textBoxTaxonomicGroup.Name = "textBoxTaxonomicGroup";
            this.textBoxTaxonomicGroup.Size = new System.Drawing.Size(135, 20);
            this.textBoxTaxonomicGroup.TabIndex = 3;
            // 
            // buttonSelection
            // 
            this.buttonSelection.Location = new System.Drawing.Point(481, 172);
            this.buttonSelection.Name = "buttonSelection";
            this.buttonSelection.Size = new System.Drawing.Size(86, 36);
            this.buttonSelection.TabIndex = 4;
            this.buttonSelection.Text = "-->";
            this.buttonSelection.UseVisualStyleBackColor = true;
            this.buttonSelection.Click += new System.EventHandler(this.buttonSelection_Click);
            // 
            // buttonDeselection
            // 
            this.buttonDeselection.Location = new System.Drawing.Point(481, 232);
            this.buttonDeselection.Name = "buttonDeselection";
            this.buttonDeselection.Size = new System.Drawing.Size(86, 37);
            this.buttonDeselection.TabIndex = 5;
            this.buttonDeselection.Text = "<--";
            this.buttonDeselection.UseVisualStyleBackColor = true;
            this.buttonDeselection.Click += new System.EventHandler(this.buttonDeselection_Click);
            // 
            // treeViewResult
            // 
            this.treeViewResult.ImageIndex = 0;
            this.treeViewResult.ImageList = this.imageListDiversityCollection;
            this.treeViewResult.Location = new System.Drawing.Point(191, 433);
            this.treeViewResult.Name = "treeViewResult";
            this.treeViewResult.SelectedImageIndex = 0;
            this.treeViewResult.Size = new System.Drawing.Size(256, 206);
            this.treeViewResult.TabIndex = 6;
            // 
            // imageListDiversityCollection
            // 
            this.imageListDiversityCollection.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDiversityCollection.ImageStream")));
            this.imageListDiversityCollection.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDiversityCollection.Images.SetKeyName(0, "FlipHorizontal.ICO");
            this.imageListDiversityCollection.Images.SetKeyName(1, "Event.ico");
            this.imageListDiversityCollection.Images.SetKeyName(2, "EventGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(3, "EventProperty.ico");
            this.imageListDiversityCollection.Images.SetKeyName(4, "EventPropertyGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(5, "EventSeries.ico");
            this.imageListDiversityCollection.Images.SetKeyName(6, "EventSeriesGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(7, "Localisation.ico");
            this.imageListDiversityCollection.Images.SetKeyName(8, "LocalisationGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(9, "Agent.ico");
            this.imageListDiversityCollection.Images.SetKeyName(10, "AgentGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(11, "CollectionSpecimen.ico");
            this.imageListDiversityCollection.Images.SetKeyName(12, "CollectionSpecimenGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(13, "Tree.ico");
            this.imageListDiversityCollection.Images.SetKeyName(14, "TreeGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(15, "Branch.ico");
            this.imageListDiversityCollection.Images.SetKeyName(16, "Branch.ico");
            this.imageListDiversityCollection.Images.SetKeyName(17, "Plant.ico");
            this.imageListDiversityCollection.Images.SetKeyName(18, "PlantGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(19, "Other.ICO");
            this.imageListDiversityCollection.Images.SetKeyName(20, "OtherGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(21, "Identification.ico");
            this.imageListDiversityCollection.Images.SetKeyName(22, "IdentificationGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(23, "Analysis.ico");
            this.imageListDiversityCollection.Images.SetKeyName(24, "AnalysisGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(25, "Photography.ico");
            this.imageListDiversityCollection.Images.SetKeyName(26, "PhotographyGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(27, "Photography.ico");
            this.imageListDiversityCollection.Images.SetKeyName(28, "PhotographyGrey.ico");
            this.imageListDiversityCollection.Images.SetKeyName(29, "Alga.ico");
            this.imageListDiversityCollection.Images.SetKeyName(30, "Assel.ico");
            this.imageListDiversityCollection.Images.SetKeyName(31, "Bacterium.ico");
            this.imageListDiversityCollection.Images.SetKeyName(32, "Bird.ico");
            this.imageListDiversityCollection.Images.SetKeyName(33, "Bryophyt.ico");
            this.imageListDiversityCollection.Images.SetKeyName(34, "Fish.ico");
            this.imageListDiversityCollection.Images.SetKeyName(35, "Fungus.ico");
            this.imageListDiversityCollection.Images.SetKeyName(36, "Insect.ico");
            this.imageListDiversityCollection.Images.SetKeyName(37, "Lichen.ico");
            this.imageListDiversityCollection.Images.SetKeyName(38, "Mammal.ico");
            this.imageListDiversityCollection.Images.SetKeyName(39, "Mollusc.ico");
            this.imageListDiversityCollection.Images.SetKeyName(40, "Myxomycete.ico");
            this.imageListDiversityCollection.Images.SetKeyName(41, "Virus.ico");
            this.imageListDiversityCollection.Images.SetKeyName(42, "CollectionSpecimenRed.ico");
            // 
            // treeActualViewSelection
            // 
            this.treeActualViewSelection.ImageIndex = 0;
            this.treeActualViewSelection.ImageList = this.imageListDiversityCollection;
            this.treeActualViewSelection.Location = new System.Drawing.Point(599, 446);
            this.treeActualViewSelection.Name = "treeActualViewSelection";
            this.treeActualViewSelection.SelectedImageIndex = 0;
            this.treeActualViewSelection.Size = new System.Drawing.Size(244, 193);
            this.treeActualViewSelection.TabIndex = 7;
            // 
            // buttonFinish
            // 
            this.buttonFinish.Location = new System.Drawing.Point(877, 50);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(235, 31);
            this.buttonFinish.TabIndex = 8;
            this.buttonFinish.Text = "Finish Selection";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // textBoxUnitdescription
            // 
            this.textBoxUnitdescription.Location = new System.Drawing.Point(12, 150);
            this.textBoxUnitdescription.Name = "textBoxUnitdescription";
            this.textBoxUnitdescription.Size = new System.Drawing.Size(135, 20);
            this.textBoxUnitdescription.TabIndex = 9;
            // 
            // panelIdentificationUnits
            // 
            this.panelIdentificationUnits.Controls.Add(this.labelDescription);
            this.panelIdentificationUnits.Controls.Add(this.labelTaxonomicGroup);
            this.panelIdentificationUnits.Controls.Add(this.labelLastIdentification);
            this.panelIdentificationUnits.Controls.Add(this.labelIdentificationUnits);
            this.panelIdentificationUnits.Controls.Add(this.textBoxLastIdentification);
            this.panelIdentificationUnits.Controls.Add(this.textBoxUnitdescription);
            this.panelIdentificationUnits.Controls.Add(this.textBoxTaxonomicGroup);
            this.panelIdentificationUnits.Location = new System.Drawing.Point(13, 179);
            this.panelIdentificationUnits.Name = "panelIdentificationUnits";
            this.panelIdentificationUnits.Size = new System.Drawing.Size(164, 185);
            this.panelIdentificationUnits.TabIndex = 10;
            this.panelIdentificationUnits.Visible = false;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(15, 128);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(82, 13);
            this.labelDescription.TabIndex = 13;
            this.labelDescription.Text = "Unit Description";
            // 
            // labelTaxonomicGroup
            // 
            this.labelTaxonomicGroup.AutoSize = true;
            this.labelTaxonomicGroup.Location = new System.Drawing.Point(12, 82);
            this.labelTaxonomicGroup.Name = "labelTaxonomicGroup";
            this.labelTaxonomicGroup.Size = new System.Drawing.Size(91, 13);
            this.labelTaxonomicGroup.TabIndex = 12;
            this.labelTaxonomicGroup.Text = "Taxonomic Group";
            // 
            // labelLastIdentification
            // 
            this.labelLastIdentification.AutoSize = true;
            this.labelLastIdentification.Location = new System.Drawing.Point(12, 37);
            this.labelLastIdentification.Name = "labelLastIdentification";
            this.labelLastIdentification.Size = new System.Drawing.Size(90, 13);
            this.labelLastIdentification.TabIndex = 11;
            this.labelLastIdentification.Text = "Last Identification";
            // 
            // labelIdentificationUnits
            // 
            this.labelIdentificationUnits.AutoSize = true;
            this.labelIdentificationUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIdentificationUnits.Location = new System.Drawing.Point(23, 9);
            this.labelIdentificationUnits.Name = "labelIdentificationUnits";
            this.labelIdentificationUnits.Size = new System.Drawing.Size(114, 13);
            this.labelIdentificationUnits.TabIndex = 10;
            this.labelIdentificationUnits.Text = "Identification Units";
            this.labelIdentificationUnits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(12, 12);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(164, 23);
            this.buttonSelect.TabIndex = 11;
            this.buttonSelect.Text = "Query Database";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // checkBoxTruncate
            // 
            this.checkBoxTruncate.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.checkBoxTruncate.AutoSize = true;
            this.checkBoxTruncate.Checked = true;
            this.checkBoxTruncate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTruncate.Location = new System.Drawing.Point(462, 309);
            this.checkBoxTruncate.Name = "checkBoxTruncate";
            this.checkBoxTruncate.Size = new System.Drawing.Size(120, 17);
            this.checkBoxTruncate.TabIndex = 12;
            this.checkBoxTruncate.Text = "Truncate DataItems";
            this.checkBoxTruncate.UseVisualStyleBackColor = true;
            // 
            // comboBoxSelectType
            // 
            this.comboBoxSelectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectType.FormattingEnabled = true;
            this.comboBoxSelectType.Items.AddRange(new object[] {
            "Collection Event Series",
            "Identification Unit",
            "Sampling Plot"});
            this.comboBoxSelectType.Location = new System.Drawing.Point(12, 50);
            this.comboBoxSelectType.Name = "comboBoxSelectType";
            this.comboBoxSelectType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBoxSelectType.Size = new System.Drawing.Size(164, 21);
            this.comboBoxSelectType.TabIndex = 13;
            this.comboBoxSelectType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectType_SelectedIndexChanged);
            // 
            // treeViewSelection
            // 
            this.treeViewSelection.ImageIndex = 0;
            this.treeViewSelection.ImageList = this.imageListDiversityCollection;
            this.treeViewSelection.Location = new System.Drawing.Point(877, 116);
            this.treeViewSelection.Name = "treeViewSelection";
            this.treeViewSelection.SelectedImageIndex = 0;
            this.treeViewSelection.Size = new System.Drawing.Size(235, 379);
            this.treeViewSelection.TabIndex = 14;
            this.treeViewSelection.Visible = false;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(877, 526);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(199, 32);
            this.buttonReturn.TabIndex = 15;
            this.buttonReturn.Text = "Return";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Visible = false;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // buttonSynchronize
            // 
            this.buttonSynchronize.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSynchronize.Location = new System.Drawing.Point(877, 576);
            this.buttonSynchronize.Name = "buttonSynchronize";
            this.buttonSynchronize.Size = new System.Drawing.Size(199, 63);
            this.buttonSynchronize.TabIndex = 16;
            this.buttonSynchronize.Text = "Synchronize";
            this.buttonSynchronize.UseVisualStyleBackColor = true;
            this.buttonSynchronize.Visible = false;
            this.buttonSynchronize.Click += new System.EventHandler(this.buttonSynchronize_Click);
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(481, 373);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(86, 23);
            this.buttonSelectAll.TabIndex = 17;
            this.buttonSelectAll.Text = "Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // labelSearchTypeCaption
            // 
            this.labelSearchTypeCaption.AutoSize = true;
            this.labelSearchTypeCaption.Location = new System.Drawing.Point(10, 50);
            this.labelSearchTypeCaption.Name = "labelSearchTypeCaption";
            this.labelSearchTypeCaption.Size = new System.Drawing.Size(0, 13);
            this.labelSearchTypeCaption.TabIndex = 18;
            // 
            // panelEventSeries
            // 
            this.panelEventSeries.Controls.Add(this.dateTimePickerEndStart);
            this.panelEventSeries.Controls.Add(this.dateTimePickerDateStartEnd);
            this.panelEventSeries.Controls.Add(this.checkBoxESEnd);
            this.panelEventSeries.Controls.Add(this.checkBoxESStart);
            this.panelEventSeries.Controls.Add(this.dateTimePickerDateEndEnd);
            this.panelEventSeries.Controls.Add(this.labelDateEnd);
            this.panelEventSeries.Controls.Add(this.dateTimePickerDateStartStart);
            this.panelEventSeries.Controls.Add(this.labelStartDate);
            this.panelEventSeries.Controls.Add(this.labelSeriesDescription);
            this.panelEventSeries.Controls.Add(this.labelSeriesCode);
            this.panelEventSeries.Controls.Add(this.labelEventSeriesCaption);
            this.panelEventSeries.Controls.Add(this.textBoxSeriesCode);
            this.panelEventSeries.Controls.Add(this.textBoxSeriesDescription);
            this.panelEventSeries.Location = new System.Drawing.Point(13, 179);
            this.panelEventSeries.Name = "panelEventSeries";
            this.panelEventSeries.Size = new System.Drawing.Size(164, 271);
            this.panelEventSeries.TabIndex = 19;
            this.panelEventSeries.Visible = false;
            // 
            // dateTimePickerEndStart
            // 
            this.dateTimePickerEndStart.Location = new System.Drawing.Point(11, 220);
            this.dateTimePickerEndStart.Name = "dateTimePickerEndStart";
            this.dateTimePickerEndStart.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerEndStart.TabIndex = 20;
            // 
            // dateTimePickerDateStartEnd
            // 
            this.dateTimePickerDateStartEnd.Location = new System.Drawing.Point(12, 171);
            this.dateTimePickerDateStartEnd.Name = "dateTimePickerDateStartEnd";
            this.dateTimePickerDateStartEnd.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerDateStartEnd.TabIndex = 19;
            // 
            // checkBoxESEnd
            // 
            this.checkBoxESEnd.AutoSize = true;
            this.checkBoxESEnd.Location = new System.Drawing.Point(79, 202);
            this.checkBoxESEnd.Name = "checkBoxESEnd";
            this.checkBoxESEnd.Size = new System.Drawing.Size(15, 14);
            this.checkBoxESEnd.TabIndex = 18;
            this.checkBoxESEnd.UseVisualStyleBackColor = true;
            // 
            // checkBoxESStart
            // 
            this.checkBoxESStart.AutoSize = true;
            this.checkBoxESStart.Location = new System.Drawing.Point(85, 125);
            this.checkBoxESStart.Name = "checkBoxESStart";
            this.checkBoxESStart.Size = new System.Drawing.Size(15, 14);
            this.checkBoxESStart.TabIndex = 17;
            this.checkBoxESStart.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerDateEndEnd
            // 
            this.dateTimePickerDateEndEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateEndEnd.Location = new System.Drawing.Point(11, 248);
            this.dateTimePickerDateEndEnd.Name = "dateTimePickerDateEndEnd";
            this.dateTimePickerDateEndEnd.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerDateEndEnd.TabIndex = 16;
            // 
            // labelDateEnd
            // 
            this.labelDateEnd.AutoSize = true;
            this.labelDateEnd.Location = new System.Drawing.Point(12, 203);
            this.labelDateEnd.Name = "labelDateEnd";
            this.labelDateEnd.Size = new System.Drawing.Size(61, 13);
            this.labelDateEnd.TabIndex = 15;
            this.labelDateEnd.Text = "Date (End):";
            // 
            // dateTimePickerDateStartStart
            // 
            this.dateTimePickerDateStartStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDateStartStart.Location = new System.Drawing.Point(12, 144);
            this.dateTimePickerDateStartStart.Name = "dateTimePickerDateStartStart";
            this.dateTimePickerDateStartStart.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerDateStartStart.TabIndex = 14;
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(15, 126);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(64, 13);
            this.labelStartDate.TabIndex = 13;
            this.labelStartDate.Text = "Date (Start):";
            // 
            // labelSeriesDescription
            // 
            this.labelSeriesDescription.AutoSize = true;
            this.labelSeriesDescription.Location = new System.Drawing.Point(12, 80);
            this.labelSeriesDescription.Name = "labelSeriesDescription";
            this.labelSeriesDescription.Size = new System.Drawing.Size(63, 13);
            this.labelSeriesDescription.TabIndex = 12;
            this.labelSeriesDescription.Text = "Description:";
            this.labelSeriesDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelSeriesCode
            // 
            this.labelSeriesCode.AutoSize = true;
            this.labelSeriesCode.Location = new System.Drawing.Point(12, 34);
            this.labelSeriesCode.Name = "labelSeriesCode";
            this.labelSeriesCode.Size = new System.Drawing.Size(67, 13);
            this.labelSeriesCode.TabIndex = 11;
            this.labelSeriesCode.Text = "Series Code:";
            // 
            // labelEventSeriesCaption
            // 
            this.labelEventSeriesCaption.AutoSize = true;
            this.labelEventSeriesCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEventSeriesCaption.Location = new System.Drawing.Point(15, 10);
            this.labelEventSeriesCaption.Name = "labelEventSeriesCaption";
            this.labelEventSeriesCaption.Size = new System.Drawing.Size(139, 13);
            this.labelEventSeriesCaption.TabIndex = 10;
            this.labelEventSeriesCaption.Text = "Collection Event Series";
            this.labelEventSeriesCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSeriesCode
            // 
            this.textBoxSeriesCode.AcceptsReturn = true;
            this.textBoxSeriesCode.Location = new System.Drawing.Point(12, 53);
            this.textBoxSeriesCode.Name = "textBoxSeriesCode";
            this.textBoxSeriesCode.Size = new System.Drawing.Size(135, 20);
            this.textBoxSeriesCode.TabIndex = 2;
            // 
            // textBoxSeriesDescription
            // 
            this.textBoxSeriesDescription.Location = new System.Drawing.Point(12, 99);
            this.textBoxSeriesDescription.Name = "textBoxSeriesDescription";
            this.textBoxSeriesDescription.Size = new System.Drawing.Size(135, 20);
            this.textBoxSeriesDescription.TabIndex = 3;
            // 
            // panelSamplingPlot
            // 
            this.panelSamplingPlot.Controls.Add(this.dateTimePickerDeterminationDateEnd);
            this.panelSamplingPlot.Controls.Add(this.checkBoxDDate);
            this.panelSamplingPlot.Controls.Add(this.dateTimePickerDeterminationDateStart);
            this.panelSamplingPlot.Controls.Add(this.labelDeterminationDate);
            this.panelSamplingPlot.Controls.Add(this.labelLocation);
            this.panelSamplingPlot.Controls.Add(this.labelSamplingPlotCaption);
            this.panelSamplingPlot.Controls.Add(this.textBoxLocation);
            this.panelSamplingPlot.Location = new System.Drawing.Point(13, 179);
            this.panelSamplingPlot.Name = "panelSamplingPlot";
            this.panelSamplingPlot.Size = new System.Drawing.Size(164, 166);
            this.panelSamplingPlot.TabIndex = 20;
            this.panelSamplingPlot.Visible = false;
            // 
            // dateTimePickerDeterminationDateEnd
            // 
            this.dateTimePickerDeterminationDateEnd.Location = new System.Drawing.Point(11, 134);
            this.dateTimePickerDeterminationDateEnd.Name = "dateTimePickerDeterminationDateEnd";
            this.dateTimePickerDeterminationDateEnd.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerDeterminationDateEnd.TabIndex = 20;
            // 
            // checkBoxDDate
            // 
            this.checkBoxDDate.AutoSize = true;
            this.checkBoxDDate.Location = new System.Drawing.Point(132, 88);
            this.checkBoxDDate.Name = "checkBoxDDate";
            this.checkBoxDDate.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDDate.TabIndex = 19;
            this.checkBoxDDate.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerDeterminationDateStart
            // 
            this.dateTimePickerDeterminationDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDeterminationDateStart.Location = new System.Drawing.Point(12, 107);
            this.dateTimePickerDeterminationDateStart.Name = "dateTimePickerDeterminationDateStart";
            this.dateTimePickerDeterminationDateStart.Size = new System.Drawing.Size(135, 20);
            this.dateTimePickerDeterminationDateStart.TabIndex = 13;
            // 
            // labelDeterminationDate
            // 
            this.labelDeterminationDate.AutoSize = true;
            this.labelDeterminationDate.Location = new System.Drawing.Point(12, 88);
            this.labelDeterminationDate.Name = "labelDeterminationDate";
            this.labelDeterminationDate.Size = new System.Drawing.Size(101, 13);
            this.labelDeterminationDate.TabIndex = 12;
            this.labelDeterminationDate.Text = "Determination Date:";
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Location = new System.Drawing.Point(12, 37);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(51, 13);
            this.labelLocation.TabIndex = 11;
            this.labelLocation.Text = "Location:";
            // 
            // labelSamplingPlotCaption
            // 
            this.labelSamplingPlotCaption.AutoSize = true;
            this.labelSamplingPlotCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSamplingPlotCaption.Location = new System.Drawing.Point(29, 9);
            this.labelSamplingPlotCaption.Name = "labelSamplingPlotCaption";
            this.labelSamplingPlotCaption.Size = new System.Drawing.Size(84, 13);
            this.labelSamplingPlotCaption.TabIndex = 10;
            this.labelSamplingPlotCaption.Text = "Sampling Plot";
            this.labelSamplingPlotCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.AcceptsReturn = true;
            this.textBoxLocation.Location = new System.Drawing.Point(12, 56);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(135, 20);
            this.textBoxLocation.TabIndex = 2;
            // 
            // labelLogDate
            // 
            this.labelLogDate.AutoSize = true;
            this.labelLogDate.Location = new System.Drawing.Point(14, 107);
            this.labelLogDate.Name = "labelLogDate";
            this.labelLogDate.Size = new System.Drawing.Size(106, 13);
            this.labelLogDate.TabIndex = 14;
            this.labelLogDate.Text = "LastUpdate between";
            // 
            // dateTimePickerLogStart
            // 
            this.dateTimePickerLogStart.Location = new System.Drawing.Point(17, 126);
            this.dateTimePickerLogStart.Name = "dateTimePickerLogStart";
            this.dateTimePickerLogStart.Size = new System.Drawing.Size(156, 20);
            this.dateTimePickerLogStart.TabIndex = 21;
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.AutoSize = true;
            this.checkBoxLog.Location = new System.Drawing.Point(158, 106);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(15, 14);
            this.checkBoxLog.TabIndex = 22;
            this.checkBoxLog.UseVisualStyleBackColor = true;
            // 
            // labelGeneralCaption
            // 
            this.labelGeneralCaption.AutoSize = true;
            this.labelGeneralCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGeneralCaption.Location = new System.Drawing.Point(55, 85);
            this.labelGeneralCaption.Name = "labelGeneralCaption";
            this.labelGeneralCaption.Size = new System.Drawing.Size(51, 13);
            this.labelGeneralCaption.TabIndex = 23;
            this.labelGeneralCaption.Text = "General";
            this.labelGeneralCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePickerLogEnd
            // 
            this.dateTimePickerLogEnd.Location = new System.Drawing.Point(17, 153);
            this.dateTimePickerLogEnd.Name = "dateTimePickerLogEnd";
            this.dateTimePickerLogEnd.Size = new System.Drawing.Size(156, 20);
            this.dateTimePickerLogEnd.TabIndex = 24;
            // 
            // SelectFieldDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 730);
            this.Controls.Add(this.dateTimePickerLogEnd);
            this.Controls.Add(this.labelGeneralCaption);
            this.Controls.Add(this.dateTimePickerLogStart);
            this.Controls.Add(this.checkBoxLog);
            this.Controls.Add(this.panelSamplingPlot);
            this.Controls.Add(this.panelEventSeries);
            this.Controls.Add(this.labelLogDate);
            this.Controls.Add(this.labelSearchTypeCaption);
            this.Controls.Add(this.buttonSelectAll);
            this.Controls.Add(this.buttonSynchronize);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.treeViewSelection);
            this.Controls.Add(this.panelIdentificationUnits);
            this.Controls.Add(this.comboBoxSelectType);
            this.Controls.Add(this.checkBoxTruncate);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.buttonFinish);
            this.Controls.Add(this.treeActualViewSelection);
            this.Controls.Add(this.treeViewResult);
            this.Controls.Add(this.buttonDeselection);
            this.Controls.Add(this.buttonSelection);
            this.Controls.Add(this.listBoxActualSelection);
            this.Controls.Add(this.listBoxResult);
            this.Name = "SelectFieldDataForm";
            this.Text = "SelectFieldData";
            this.panelIdentificationUnits.ResumeLayout(false);
            this.panelIdentificationUnits.PerformLayout();
            this.panelEventSeries.ResumeLayout(false);
            this.panelEventSeries.PerformLayout();
            this.panelSamplingPlot.ResumeLayout(false);
            this.panelSamplingPlot.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxResult;
        private System.Windows.Forms.ListBox listBoxActualSelection;
        private System.Windows.Forms.TextBox textBoxLastIdentification;
        private System.Windows.Forms.TextBox textBoxTaxonomicGroup;
        private System.Windows.Forms.Button buttonSelection;
        private System.Windows.Forms.Button buttonDeselection;
        private System.Windows.Forms.TreeView treeViewResult;
        private System.Windows.Forms.TreeView treeActualViewSelection;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.TextBox textBoxUnitdescription;
        private System.Windows.Forms.Panel panelIdentificationUnits;
        private System.Windows.Forms.Label labelLastIdentification;
        private System.Windows.Forms.Label labelIdentificationUnits;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelTaxonomicGroup;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.ImageList imageListDiversityCollection;
        private System.Windows.Forms.CheckBox checkBoxTruncate;
        private System.Windows.Forms.ComboBox comboBoxSelectType;
        private System.Windows.Forms.TreeView treeViewSelection;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Button buttonSynchronize;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Label labelSearchTypeCaption;
        private System.Windows.Forms.Panel panelEventSeries;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelSeriesDescription;
        private System.Windows.Forms.Label labelSeriesCode;
        private System.Windows.Forms.Label labelEventSeriesCaption;
        private System.Windows.Forms.TextBox textBoxSeriesCode;
        private System.Windows.Forms.TextBox textBoxSeriesDescription;
        private System.Windows.Forms.Panel panelSamplingPlot;
        private System.Windows.Forms.Label labelDeterminationDate;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label labelSamplingPlotCaption;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateEndEnd;
        private System.Windows.Forms.Label labelDateEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateStartStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerDeterminationDateStart;
        private System.Windows.Forms.CheckBox checkBoxESEnd;
        private System.Windows.Forms.CheckBox checkBoxESStart;
        private System.Windows.Forms.CheckBox checkBoxDDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerDeterminationDateEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateStartEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndStart;
        private System.Windows.Forms.Label labelLogDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerLogStart;
        private System.Windows.Forms.CheckBox checkBoxLog;
        private System.Windows.Forms.Label labelGeneralCaption;
        private System.Windows.Forms.DateTimePicker dateTimePickerLogEnd;
    }
}