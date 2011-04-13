namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    partial class SearchForm
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
            this.panelSearchEvent = new System.Windows.Forms.Panel();
            this.radioButtonEventOR = new System.Windows.Forms.RadioButton();
            this.radioButtonEventAND = new System.Windows.Forms.RadioButton();
            this.textBoxSearchEventHabitat = new System.Windows.Forms.TextBox();
            this.labelEventHabitatCaption = new System.Windows.Forms.Label();
            this.textBoxSearchEventLocality = new System.Windows.Forms.TextBox();
            this.labelEventLocalityCaption = new System.Windows.Forms.Label();
            this.textBoxSearchEventNotes = new System.Windows.Forms.TextBox();
            this.labelEventNotesCaption = new System.Windows.Forms.Label();
            this.textBoxSearchEventNumber = new System.Windows.Forms.TextBox();
            this.labelCollectorsEventNumberCaption = new System.Windows.Forms.Label();
            this.buttonSearchEventCancel = new System.Windows.Forms.Button();
            this.buttonSearchEvent = new System.Windows.Forms.Button();
            this.labelSearchEventCaption = new System.Windows.Forms.Label();
            this.panelSearchChoice = new System.Windows.Forms.Panel();
            this.buttonSearchForIU = new System.Windows.Forms.Button();
            this.buttonSearchForSpecimen = new System.Windows.Forms.Button();
            this.buttonSearchForEvent = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelSearchCaption = new System.Windows.Forms.Label();
            this.panelSearchSpecimen = new System.Windows.Forms.Panel();
            this.buttonSearchSpecimen = new System.Windows.Forms.Button();
            this.radioButtonSpecimenOR = new System.Windows.Forms.RadioButton();
            this.radioButtonSpecimenAND = new System.Windows.Forms.RadioButton();
            this.textBoxSearchSpecimenNotes = new System.Windows.Forms.TextBox();
            this.labelCollectionSpecimenNotesCaption = new System.Windows.Forms.Label();
            this.textBoxSearchSpecimenDepositor = new System.Windows.Forms.TextBox();
            this.labelCollectionSpecimenDepositorCaption = new System.Windows.Forms.Label();
            this.textBoxSearchSpecimenAccessionNumber = new System.Windows.Forms.TextBox();
            this.labelCollectionSpecimenAccessionNumberCaption = new System.Windows.Forms.Label();
            this.buttonSearchSpecimenCancel = new System.Windows.Forms.Button();
            this.labelSearchSpecimenCaption = new System.Windows.Forms.Label();
            this.panelSearchResults = new System.Windows.Forms.Panel();
            this.buttonShowDetails = new System.Windows.Forms.Button();
            this.buttonLoadInTree = new System.Windows.Forms.Button();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.columnID = new System.Windows.Forms.ColumnHeader();
            this.columnNumber = new System.Windows.Forms.ColumnHeader();
            this.columnDate = new System.Windows.Forms.ColumnHeader();
            this.columnNotes = new System.Windows.Forms.ColumnHeader();
            this.labelSearchResultsCaption = new System.Windows.Forms.Label();
            this.panelSearchIU = new System.Windows.Forms.Panel();
            this.comboBoxIUUnitDescription = new UBT.AI4.Toolbox.Controls.ClickComboBox();
            this.radioButtonIUOR = new System.Windows.Forms.RadioButton();
            this.radioButtonIUAND = new System.Windows.Forms.RadioButton();
            this.textBoxSearchIUNotes = new System.Windows.Forms.TextBox();
            this.labelIUNotesCaption = new System.Windows.Forms.Label();
            this.textBoxSearchIUUnitIdentifier = new System.Windows.Forms.TextBox();
            this.textBoxSearchIULastIdentification = new System.Windows.Forms.TextBox();
            this.textBoxSearchIUTaxonomy = new System.Windows.Forms.TextBox();
            this.labelUnitIdentifier = new System.Windows.Forms.Label();
            this.labelUUnitDescriptionCaption = new System.Windows.Forms.Label();
            this.labelLastIdentificationCacheCaption = new System.Windows.Forms.Label();
            this.labelTaxonomicGroupCaption = new System.Windows.Forms.Label();
            this.buttonSearchIUCancel = new System.Windows.Forms.Button();
            this.buttonSearchIU = new System.Windows.Forms.Button();
            this.labelSearchIUCaption = new System.Windows.Forms.Label();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.panelSearchEvent.SuspendLayout();
            this.panelSearchChoice.SuspendLayout();
            this.panelSearchSpecimen.SuspendLayout();
            this.panelSearchResults.SuspendLayout();
            this.panelSearchIU.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSearchEvent
            // 
            this.panelSearchEvent.AutoScroll = true;
            this.panelSearchEvent.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.panelSearchEvent.BackColor = System.Drawing.SystemColors.Info;
            this.panelSearchEvent.Controls.Add(this.radioButtonEventOR);
            this.panelSearchEvent.Controls.Add(this.radioButtonEventAND);
            this.panelSearchEvent.Controls.Add(this.textBoxSearchEventHabitat);
            this.panelSearchEvent.Controls.Add(this.labelEventHabitatCaption);
            this.panelSearchEvent.Controls.Add(this.textBoxSearchEventLocality);
            this.panelSearchEvent.Controls.Add(this.labelEventLocalityCaption);
            this.panelSearchEvent.Controls.Add(this.textBoxSearchEventNotes);
            this.panelSearchEvent.Controls.Add(this.labelEventNotesCaption);
            this.panelSearchEvent.Controls.Add(this.textBoxSearchEventNumber);
            this.panelSearchEvent.Controls.Add(this.labelCollectorsEventNumberCaption);
            this.panelSearchEvent.Controls.Add(this.buttonSearchEventCancel);
            this.panelSearchEvent.Controls.Add(this.buttonSearchEvent);
            this.panelSearchEvent.Controls.Add(this.labelSearchEventCaption);
            this.panelSearchEvent.Location = new System.Drawing.Point(15, 13);
            this.panelSearchEvent.Name = "panelSearchEvent";
            this.panelSearchEvent.Size = new System.Drawing.Size(61, 69);
            this.panelSearchEvent.Visible = false;
            this.panelSearchEvent.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // radioButtonEventOR
            // 
            this.radioButtonEventOR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.radioButtonEventOR.Location = new System.Drawing.Point(79, 127);
            this.radioButtonEventOR.Name = "radioButtonEventOR";
            this.radioButtonEventOR.Size = new System.Drawing.Size(72, 21);
            this.radioButtonEventOR.TabIndex = 90;
            this.radioButtonEventOR.TabStop = false;
            this.radioButtonEventOR.Text = "  OR";
            // 
            // radioButtonEventAND
            // 
            this.radioButtonEventAND.Checked = true;
            this.radioButtonEventAND.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.radioButtonEventAND.Location = new System.Drawing.Point(11, 128);
            this.radioButtonEventAND.Name = "radioButtonEventAND";
            this.radioButtonEventAND.Size = new System.Drawing.Size(72, 21);
            this.radioButtonEventAND.TabIndex = 89;
            this.radioButtonEventAND.Text = "  AND";
            // 
            // textBoxSearchEventHabitat
            // 
            this.textBoxSearchEventHabitat.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchEventHabitat.Location = new System.Drawing.Point(105, 101);
            this.textBoxSearchEventHabitat.Name = "textBoxSearchEventHabitat";
            this.textBoxSearchEventHabitat.Size = new System.Drawing.Size(122, 19);
            this.textBoxSearchEventHabitat.TabIndex = 41;
            // 
            // labelEventHabitatCaption
            // 
            this.labelEventHabitatCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelEventHabitatCaption.Location = new System.Drawing.Point(7, 104);
            this.labelEventHabitatCaption.Name = "labelEventHabitatCaption";
            this.labelEventHabitatCaption.Size = new System.Drawing.Size(90, 20);
            this.labelEventHabitatCaption.Text = "Event Habitat:";
            // 
            // textBoxSearchEventLocality
            // 
            this.textBoxSearchEventLocality.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchEventLocality.Location = new System.Drawing.Point(105, 79);
            this.textBoxSearchEventLocality.Name = "textBoxSearchEventLocality";
            this.textBoxSearchEventLocality.Size = new System.Drawing.Size(122, 19);
            this.textBoxSearchEventLocality.TabIndex = 38;
            // 
            // labelEventLocalityCaption
            // 
            this.labelEventLocalityCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelEventLocalityCaption.Location = new System.Drawing.Point(7, 78);
            this.labelEventLocalityCaption.Name = "labelEventLocalityCaption";
            this.labelEventLocalityCaption.Size = new System.Drawing.Size(92, 20);
            this.labelEventLocalityCaption.Text = "Event Locality:";
            // 
            // textBoxSearchEventNotes
            // 
            this.textBoxSearchEventNotes.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchEventNotes.Location = new System.Drawing.Point(105, 56);
            this.textBoxSearchEventNotes.Name = "textBoxSearchEventNotes";
            this.textBoxSearchEventNotes.Size = new System.Drawing.Size(122, 19);
            this.textBoxSearchEventNotes.TabIndex = 31;
            // 
            // labelEventNotesCaption
            // 
            this.labelEventNotesCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelEventNotesCaption.Location = new System.Drawing.Point(9, 55);
            this.labelEventNotesCaption.Name = "labelEventNotesCaption";
            this.labelEventNotesCaption.Size = new System.Drawing.Size(92, 20);
            this.labelEventNotesCaption.Text = "Event Notes:";
            // 
            // textBoxSearchEventNumber
            // 
            this.textBoxSearchEventNumber.AcceptsReturn = true;
            this.textBoxSearchEventNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchEventNumber.Location = new System.Drawing.Point(105, 31);
            this.textBoxSearchEventNumber.Name = "textBoxSearchEventNumber";
            this.textBoxSearchEventNumber.Size = new System.Drawing.Size(122, 19);
            this.textBoxSearchEventNumber.TabIndex = 24;
            // 
            // labelCollectorsEventNumberCaption
            // 
            this.labelCollectorsEventNumberCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCollectorsEventNumberCaption.Location = new System.Drawing.Point(7, 30);
            this.labelCollectorsEventNumberCaption.Name = "labelCollectorsEventNumberCaption";
            this.labelCollectorsEventNumberCaption.Size = new System.Drawing.Size(92, 20);
            this.labelCollectorsEventNumberCaption.Text = "Event No.:";
            // 
            // buttonSearchEventCancel
            // 
            this.buttonSearchEventCancel.Location = new System.Drawing.Point(131, 160);
            this.buttonSearchEventCancel.Name = "buttonSearchEventCancel";
            this.buttonSearchEventCancel.Size = new System.Drawing.Size(96, 20);
            this.buttonSearchEventCancel.TabIndex = 19;
            this.buttonSearchEventCancel.Text = "Cancel";
            this.buttonSearchEventCancel.Click += new System.EventHandler(this.buttonSearchEventCancel_Click);
            // 
            // buttonSearchEvent
            // 
            this.buttonSearchEvent.Location = new System.Drawing.Point(9, 160);
            this.buttonSearchEvent.Name = "buttonSearchEvent";
            this.buttonSearchEvent.Size = new System.Drawing.Size(96, 20);
            this.buttonSearchEvent.TabIndex = 18;
            this.buttonSearchEvent.Text = "Search";
            this.buttonSearchEvent.Click += new System.EventHandler(this.buttonSearchEvent_Click);
            // 
            // labelSearchEventCaption
            // 
            this.labelSearchEventCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearchEventCaption.Location = new System.Drawing.Point(4, 7);
            this.labelSearchEventCaption.Name = "labelSearchEventCaption";
            this.labelSearchEventCaption.Size = new System.Drawing.Size(224, 20);
            this.labelSearchEventCaption.Text = "Search for Events";
            this.labelSearchEventCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelSearchChoice
            // 
            this.panelSearchChoice.AutoScroll = true;
            this.panelSearchChoice.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.panelSearchChoice.BackColor = System.Drawing.SystemColors.Info;
            this.panelSearchChoice.Controls.Add(this.buttonSearchForIU);
            this.panelSearchChoice.Controls.Add(this.buttonSearchForSpecimen);
            this.panelSearchChoice.Controls.Add(this.buttonSearchForEvent);
            this.panelSearchChoice.Controls.Add(this.buttonCancel);
            this.panelSearchChoice.Controls.Add(this.labelSearchCaption);
            this.panelSearchChoice.Location = new System.Drawing.Point(162, 13);
            this.panelSearchChoice.Name = "panelSearchChoice";
            this.panelSearchChoice.Size = new System.Drawing.Size(60, 69);
            this.panelSearchChoice.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // buttonSearchForIU
            // 
            this.buttonSearchForIU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSearchForIU.Location = new System.Drawing.Point(16, 108);
            this.buttonSearchForIU.Name = "buttonSearchForIU";
            this.buttonSearchForIU.Size = new System.Drawing.Size(160, 25);
            this.buttonSearchForIU.TabIndex = 3;
            this.buttonSearchForIU.Text = "Search Identification Unit";
            this.buttonSearchForIU.Click += new System.EventHandler(this.buttonSearchForIU_Click);
            // 
            // buttonSearchForSpecimen
            // 
            this.buttonSearchForSpecimen.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSearchForSpecimen.Location = new System.Drawing.Point(16, 73);
            this.buttonSearchForSpecimen.Name = "buttonSearchForSpecimen";
            this.buttonSearchForSpecimen.Size = new System.Drawing.Size(160, 25);
            this.buttonSearchForSpecimen.TabIndex = 2;
            this.buttonSearchForSpecimen.Text = "Search Specimen";
            this.buttonSearchForSpecimen.Click += new System.EventHandler(this.buttonSearchForSpecimen_Click);
            // 
            // buttonSearchForEvent
            // 
            this.buttonSearchForEvent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSearchForEvent.Location = new System.Drawing.Point(16, 39);
            this.buttonSearchForEvent.Name = "buttonSearchForEvent";
            this.buttonSearchForEvent.Size = new System.Drawing.Size(160, 25);
            this.buttonSearchForEvent.TabIndex = 1;
            this.buttonSearchForEvent.Text = "Search Events";
            this.buttonSearchForEvent.Click += new System.EventHandler(this.buttonSearchForEvent_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(119, 148);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 32);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            // 
            // labelSearchCaption
            // 
            this.labelSearchCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearchCaption.Location = new System.Drawing.Point(4, 6);
            this.labelSearchCaption.Name = "labelSearchCaption";
            this.labelSearchCaption.Size = new System.Drawing.Size(224, 20);
            this.labelSearchCaption.Text = "Search";
            this.labelSearchCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelSearchSpecimen
            // 
            this.panelSearchSpecimen.AutoScroll = true;
            this.panelSearchSpecimen.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.panelSearchSpecimen.BackColor = System.Drawing.SystemColors.Info;
            this.panelSearchSpecimen.Controls.Add(this.buttonSearchSpecimen);
            this.panelSearchSpecimen.Controls.Add(this.radioButtonSpecimenOR);
            this.panelSearchSpecimen.Controls.Add(this.radioButtonSpecimenAND);
            this.panelSearchSpecimen.Controls.Add(this.textBoxSearchSpecimenNotes);
            this.panelSearchSpecimen.Controls.Add(this.labelCollectionSpecimenNotesCaption);
            this.panelSearchSpecimen.Controls.Add(this.textBoxSearchSpecimenDepositor);
            this.panelSearchSpecimen.Controls.Add(this.labelCollectionSpecimenDepositorCaption);
            this.panelSearchSpecimen.Controls.Add(this.textBoxSearchSpecimenAccessionNumber);
            this.panelSearchSpecimen.Controls.Add(this.labelCollectionSpecimenAccessionNumberCaption);
            this.panelSearchSpecimen.Controls.Add(this.buttonSearchSpecimenCancel);
            this.panelSearchSpecimen.Controls.Add(this.labelSearchSpecimenCaption);
            this.panelSearchSpecimen.Location = new System.Drawing.Point(162, 98);
            this.panelSearchSpecimen.Name = "panelSearchSpecimen";
            this.panelSearchSpecimen.Size = new System.Drawing.Size(60, 67);
            this.panelSearchSpecimen.Visible = false;
            // 
            // buttonSearchSpecimen
            // 
            this.buttonSearchSpecimen.Location = new System.Drawing.Point(6, 159);
            this.buttonSearchSpecimen.Name = "buttonSearchSpecimen";
            this.buttonSearchSpecimen.Size = new System.Drawing.Size(96, 20);
            this.buttonSearchSpecimen.TabIndex = 97;
            this.buttonSearchSpecimen.Text = "Search";
            this.buttonSearchSpecimen.Click += new System.EventHandler(this.buttonSearchSpecimen_Click);
            // 
            // radioButtonSpecimenOR
            // 
            this.radioButtonSpecimenOR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.radioButtonSpecimenOR.Location = new System.Drawing.Point(78, 133);
            this.radioButtonSpecimenOR.Name = "radioButtonSpecimenOR";
            this.radioButtonSpecimenOR.Size = new System.Drawing.Size(75, 21);
            this.radioButtonSpecimenOR.TabIndex = 92;
            this.radioButtonSpecimenOR.TabStop = false;
            this.radioButtonSpecimenOR.Text = "  OR";
            // 
            // radioButtonSpecimenAND
            // 
            this.radioButtonSpecimenAND.Checked = true;
            this.radioButtonSpecimenAND.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.radioButtonSpecimenAND.Location = new System.Drawing.Point(10, 133);
            this.radioButtonSpecimenAND.Name = "radioButtonSpecimenAND";
            this.radioButtonSpecimenAND.Size = new System.Drawing.Size(75, 21);
            this.radioButtonSpecimenAND.TabIndex = 91;
            this.radioButtonSpecimenAND.Text = "  AND";
            // 
            // textBoxSearchSpecimenNotes
            // 
            this.textBoxSearchSpecimenNotes.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchSpecimenNotes.Location = new System.Drawing.Point(93, 102);
            this.textBoxSearchSpecimenNotes.Name = "textBoxSearchSpecimenNotes";
            this.textBoxSearchSpecimenNotes.Size = new System.Drawing.Size(135, 19);
            this.textBoxSearchSpecimenNotes.TabIndex = 61;
            // 
            // labelCollectionSpecimenNotesCaption
            // 
            this.labelCollectionSpecimenNotesCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCollectionSpecimenNotesCaption.Location = new System.Drawing.Point(6, 102);
            this.labelCollectionSpecimenNotesCaption.Name = "labelCollectionSpecimenNotesCaption";
            this.labelCollectionSpecimenNotesCaption.Size = new System.Drawing.Size(83, 20);
            this.labelCollectionSpecimenNotesCaption.Text = "Notes:";
            // 
            // textBoxSearchSpecimenDepositor
            // 
            this.textBoxSearchSpecimenDepositor.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchSpecimenDepositor.Location = new System.Drawing.Point(93, 71);
            this.textBoxSearchSpecimenDepositor.Name = "textBoxSearchSpecimenDepositor";
            this.textBoxSearchSpecimenDepositor.Size = new System.Drawing.Size(135, 19);
            this.textBoxSearchSpecimenDepositor.TabIndex = 46;
            // 
            // labelCollectionSpecimenDepositorCaption
            // 
            this.labelCollectionSpecimenDepositorCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCollectionSpecimenDepositorCaption.Location = new System.Drawing.Point(6, 71);
            this.labelCollectionSpecimenDepositorCaption.Name = "labelCollectionSpecimenDepositorCaption";
            this.labelCollectionSpecimenDepositorCaption.Size = new System.Drawing.Size(83, 20);
            this.labelCollectionSpecimenDepositorCaption.Text = "Depositor:";
            // 
            // textBoxSearchSpecimenAccessionNumber
            // 
            this.textBoxSearchSpecimenAccessionNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchSpecimenAccessionNumber.Location = new System.Drawing.Point(92, 39);
            this.textBoxSearchSpecimenAccessionNumber.Name = "textBoxSearchSpecimenAccessionNumber";
            this.textBoxSearchSpecimenAccessionNumber.Size = new System.Drawing.Size(136, 19);
            this.textBoxSearchSpecimenAccessionNumber.TabIndex = 43;
            // 
            // labelCollectionSpecimenAccessionNumberCaption
            // 
            this.labelCollectionSpecimenAccessionNumberCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelCollectionSpecimenAccessionNumberCaption.Location = new System.Drawing.Point(6, 39);
            this.labelCollectionSpecimenAccessionNumberCaption.Name = "labelCollectionSpecimenAccessionNumberCaption";
            this.labelCollectionSpecimenAccessionNumberCaption.Size = new System.Drawing.Size(83, 20);
            this.labelCollectionSpecimenAccessionNumberCaption.Text = "Accession No.:";
            // 
            // buttonSearchSpecimenCancel
            // 
            this.buttonSearchSpecimenCancel.Location = new System.Drawing.Point(132, 159);
            this.buttonSearchSpecimenCancel.Name = "buttonSearchSpecimenCancel";
            this.buttonSearchSpecimenCancel.Size = new System.Drawing.Size(96, 20);
            this.buttonSearchSpecimenCancel.TabIndex = 19;
            this.buttonSearchSpecimenCancel.Text = "Cancel";
            this.buttonSearchSpecimenCancel.Click += new System.EventHandler(this.buttonSearchSpecimenCancel_Click);
            // 
            // labelSearchSpecimenCaption
            // 
            this.labelSearchSpecimenCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearchSpecimenCaption.Location = new System.Drawing.Point(4, 7);
            this.labelSearchSpecimenCaption.Name = "labelSearchSpecimenCaption";
            this.labelSearchSpecimenCaption.Size = new System.Drawing.Size(224, 20);
            this.labelSearchSpecimenCaption.Text = "Search for Specimen";
            this.labelSearchSpecimenCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelSearchResults
            // 
            this.panelSearchResults.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.panelSearchResults.BackColor = System.Drawing.SystemColors.Info;
            this.panelSearchResults.Controls.Add(this.buttonShowDetails);
            this.panelSearchResults.Controls.Add(this.buttonLoadInTree);
            this.panelSearchResults.Controls.Add(this.listViewResults);
            this.panelSearchResults.Controls.Add(this.labelSearchResultsCaption);
            this.panelSearchResults.Location = new System.Drawing.Point(82, 88);
            this.panelSearchResults.Name = "panelSearchResults";
            this.panelSearchResults.Size = new System.Drawing.Size(66, 70);
            this.panelSearchResults.Visible = false;
            // 
            // buttonShowDetails
            // 
            this.buttonShowDetails.Location = new System.Drawing.Point(129, 163);
            this.buttonShowDetails.Name = "buttonShowDetails";
            this.buttonShowDetails.Size = new System.Drawing.Size(96, 20);
            this.buttonShowDetails.TabIndex = 4;
            this.buttonShowDetails.Text = "Details";
            this.buttonShowDetails.Click += new System.EventHandler(this.buttonShowDetails_Click);
            // 
            // buttonLoadInTree
            // 
            this.buttonLoadInTree.Location = new System.Drawing.Point(14, 163);
            this.buttonLoadInTree.Name = "buttonLoadInTree";
            this.buttonLoadInTree.Size = new System.Drawing.Size(96, 20);
            this.buttonLoadInTree.TabIndex = 3;
            this.buttonLoadInTree.Text = "Load";
            this.buttonLoadInTree.Click += new System.EventHandler(this.buttonLoadInTree_Click);
            // 
            // listViewResults
            // 
            this.listViewResults.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listViewResults.Columns.Add(this.columnID);
            this.listViewResults.Columns.Add(this.columnNumber);
            this.listViewResults.Columns.Add(this.columnDate);
            this.listViewResults.Columns.Add(this.columnNotes);
            this.listViewResults.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewResults.Location = new System.Drawing.Point(4, 27);
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(232, 129);
            this.listViewResults.TabIndex = 1;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            // 
            // columnID
            // 
            this.columnID.Text = "ID";
            this.columnID.Width = 60;
            // 
            // columnNumber
            // 
            this.columnNumber.Text = "Number";
            this.columnNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnNumber.Width = 60;
            // 
            // columnDate
            // 
            this.columnDate.Text = "Date";
            this.columnDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnDate.Width = 60;
            // 
            // columnNotes
            // 
            this.columnNotes.Text = "Notes";
            this.columnNotes.Width = 300;
            // 
            // labelSearchResultsCaption
            // 
            this.labelSearchResultsCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearchResultsCaption.Location = new System.Drawing.Point(8, 5);
            this.labelSearchResultsCaption.Name = "labelSearchResultsCaption";
            this.labelSearchResultsCaption.Size = new System.Drawing.Size(224, 20);
            this.labelSearchResultsCaption.Text = "Search Results";
            this.labelSearchResultsCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelSearchIU
            // 
            this.panelSearchIU.AutoScroll = true;
            this.panelSearchIU.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.panelSearchIU.BackColor = System.Drawing.SystemColors.Info;
            this.panelSearchIU.Controls.Add(this.comboBoxIUUnitDescription);
            this.panelSearchIU.Controls.Add(this.radioButtonIUOR);
            this.panelSearchIU.Controls.Add(this.radioButtonIUAND);
            this.panelSearchIU.Controls.Add(this.textBoxSearchIUNotes);
            this.panelSearchIU.Controls.Add(this.labelIUNotesCaption);
            this.panelSearchIU.Controls.Add(this.textBoxSearchIUUnitIdentifier);
            this.panelSearchIU.Controls.Add(this.textBoxSearchIULastIdentification);
            this.panelSearchIU.Controls.Add(this.textBoxSearchIUTaxonomy);
            this.panelSearchIU.Controls.Add(this.labelUnitIdentifier);
            this.panelSearchIU.Controls.Add(this.labelUUnitDescriptionCaption);
            this.panelSearchIU.Controls.Add(this.labelLastIdentificationCacheCaption);
            this.panelSearchIU.Controls.Add(this.labelTaxonomicGroupCaption);
            this.panelSearchIU.Controls.Add(this.buttonSearchIUCancel);
            this.panelSearchIU.Controls.Add(this.buttonSearchIU);
            this.panelSearchIU.Controls.Add(this.labelSearchIUCaption);
            this.panelSearchIU.Location = new System.Drawing.Point(82, 13);
            this.panelSearchIU.Name = "panelSearchIU";
            this.panelSearchIU.Size = new System.Drawing.Size(64, 69);
            this.panelSearchIU.Visible = false;
            // 
            // comboBoxIUUnitDescription
            // 
            this.comboBoxIUUnitDescription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxIUUnitDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.comboBoxIUUnitDescription.Location = new System.Drawing.Point(103, 107);
            this.comboBoxIUUnitDescription.Name = "comboBoxIUUnitDescription";
            this.comboBoxIUUnitDescription.Size = new System.Drawing.Size(118, 20);
            this.comboBoxIUUnitDescription.TabIndex = 4;
            // 
            // radioButtonIUOR
            // 
            this.radioButtonIUOR.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.radioButtonIUOR.Location = new System.Drawing.Point(72, 155);
            this.radioButtonIUOR.Name = "radioButtonIUOR";
            this.radioButtonIUOR.Size = new System.Drawing.Size(75, 20);
            this.radioButtonIUOR.TabIndex = 90;
            this.radioButtonIUOR.TabStop = false;
            this.radioButtonIUOR.Text = "  OR";
            // 
            // radioButtonIUAND
            // 
            this.radioButtonIUAND.Checked = true;
            this.radioButtonIUAND.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.radioButtonIUAND.Location = new System.Drawing.Point(6, 155);
            this.radioButtonIUAND.Name = "radioButtonIUAND";
            this.radioButtonIUAND.Size = new System.Drawing.Size(75, 20);
            this.radioButtonIUAND.TabIndex = 89;
            this.radioButtonIUAND.Text = "  AND";
            // 
            // textBoxSearchIUNotes
            // 
            this.textBoxSearchIUNotes.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchIUNotes.Location = new System.Drawing.Point(103, 133);
            this.textBoxSearchIUNotes.Name = "textBoxSearchIUNotes";
            this.textBoxSearchIUNotes.Size = new System.Drawing.Size(118, 19);
            this.textBoxSearchIUNotes.TabIndex = 5;
            // 
            // labelIUNotesCaption
            // 
            this.labelIUNotesCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelIUNotesCaption.Location = new System.Drawing.Point(5, 133);
            this.labelIUNotesCaption.Name = "labelIUNotesCaption";
            this.labelIUNotesCaption.Size = new System.Drawing.Size(99, 20);
            this.labelIUNotesCaption.Text = "Notes:";
            // 
            // textBoxSearchIUUnitIdentifier
            // 
            this.textBoxSearchIUUnitIdentifier.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchIUUnitIdentifier.Location = new System.Drawing.Point(103, 32);
            this.textBoxSearchIUUnitIdentifier.Name = "textBoxSearchIUUnitIdentifier";
            this.textBoxSearchIUUnitIdentifier.Size = new System.Drawing.Size(118, 19);
            this.textBoxSearchIUUnitIdentifier.TabIndex = 1;
            // 
            // textBoxSearchIULastIdentification
            // 
            this.textBoxSearchIULastIdentification.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchIULastIdentification.Location = new System.Drawing.Point(103, 57);
            this.textBoxSearchIULastIdentification.Name = "textBoxSearchIULastIdentification";
            this.textBoxSearchIULastIdentification.Size = new System.Drawing.Size(118, 19);
            this.textBoxSearchIULastIdentification.TabIndex = 2;
            // 
            // textBoxSearchIUTaxonomy
            // 
            this.textBoxSearchIUTaxonomy.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.textBoxSearchIUTaxonomy.Location = new System.Drawing.Point(103, 82);
            this.textBoxSearchIUTaxonomy.Name = "textBoxSearchIUTaxonomy";
            this.textBoxSearchIUTaxonomy.Size = new System.Drawing.Size(118, 19);
            this.textBoxSearchIUTaxonomy.TabIndex = 3;
            // 
            // labelUnitIdentifier
            // 
            this.labelUnitIdentifier.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelUnitIdentifier.Location = new System.Drawing.Point(5, 32);
            this.labelUnitIdentifier.Name = "labelUnitIdentifier";
            this.labelUnitIdentifier.Size = new System.Drawing.Size(99, 20);
            this.labelUnitIdentifier.Text = "Unit Identifier:";
            // 
            // labelUUnitDescriptionCaption
            // 
            this.labelUUnitDescriptionCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelUUnitDescriptionCaption.Location = new System.Drawing.Point(5, 107);
            this.labelUUnitDescriptionCaption.Name = "labelUUnitDescriptionCaption";
            this.labelUUnitDescriptionCaption.Size = new System.Drawing.Size(99, 20);
            this.labelUUnitDescriptionCaption.Text = "Unit Description:";
            // 
            // labelLastIdentificationCacheCaption
            // 
            this.labelLastIdentificationCacheCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelLastIdentificationCacheCaption.Location = new System.Drawing.Point(5, 57);
            this.labelLastIdentificationCacheCaption.Name = "labelLastIdentificationCacheCaption";
            this.labelLastIdentificationCacheCaption.Size = new System.Drawing.Size(99, 20);
            this.labelLastIdentificationCacheCaption.Text = "Last Identification:";
            // 
            // labelTaxonomicGroupCaption
            // 
            this.labelTaxonomicGroupCaption.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.labelTaxonomicGroupCaption.Location = new System.Drawing.Point(5, 82);
            this.labelTaxonomicGroupCaption.Name = "labelTaxonomicGroupCaption";
            this.labelTaxonomicGroupCaption.Size = new System.Drawing.Size(99, 20);
            this.labelTaxonomicGroupCaption.Text = "Taxonomic Group:";
            // 
            // buttonSearchIUCancel
            // 
            this.buttonSearchIUCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSearchIUCancel.Location = new System.Drawing.Point(125, 181);
            this.buttonSearchIUCancel.Name = "buttonSearchIUCancel";
            this.buttonSearchIUCancel.Size = new System.Drawing.Size(96, 20);
            this.buttonSearchIUCancel.TabIndex = 19;
            this.buttonSearchIUCancel.Text = "Cancel";
            this.buttonSearchIUCancel.Click += new System.EventHandler(this.buttonSearchIUCancel_Click);
            // 
            // buttonSearchIU
            // 
            this.buttonSearchIU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSearchIU.Location = new System.Drawing.Point(8, 181);
            this.buttonSearchIU.Name = "buttonSearchIU";
            this.buttonSearchIU.Size = new System.Drawing.Size(96, 20);
            this.buttonSearchIU.TabIndex = 18;
            this.buttonSearchIU.Text = "Search";
            this.buttonSearchIU.Click += new System.EventHandler(this.buttonSearchIU_Click);
            // 
            // labelSearchIUCaption
            // 
            this.labelSearchIUCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearchIUCaption.Location = new System.Drawing.Point(5, 7);
            this.labelSearchIUCaption.Name = "labelSearchIUCaption";
            this.labelSearchIUCaption.Size = new System.Drawing.Size(135, 20);
            this.labelSearchIUCaption.Text = "Search for Identification Unit";
            this.labelSearchIUCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(0, 5);
            this.ClientSize = new System.Drawing.Size(240, 188);
            this.Controls.Add(this.panelSearchIU);
            this.Controls.Add(this.panelSearchResults);
            this.Controls.Add(this.panelSearchSpecimen);
            this.Controls.Add(this.panelSearchChoice);
            this.Controls.Add(this.panelSearchEvent);
            this.Menu = this.mainMenu1;
            this.Name = "SearchForm";
            this.Text = "Diversity Mobile";
            this.panelSearchEvent.ResumeLayout(false);
            this.panelSearchChoice.ResumeLayout(false);
            this.panelSearchSpecimen.ResumeLayout(false);
            this.panelSearchResults.ResumeLayout(false);
            this.panelSearchIU.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSearchEvent;
        private System.Windows.Forms.Label labelSearchEventCaption;
        private System.Windows.Forms.Button buttonSearchEventCancel;
        private System.Windows.Forms.Button buttonSearchEvent;
        private System.Windows.Forms.TextBox textBoxSearchEventNotes;
        private System.Windows.Forms.Label labelEventNotesCaption;
        private System.Windows.Forms.TextBox textBoxSearchEventNumber;
        private System.Windows.Forms.Label labelCollectorsEventNumberCaption;
        private System.Windows.Forms.Panel panelSearchChoice;
        private System.Windows.Forms.Label labelSearchCaption;
        private System.Windows.Forms.TextBox textBoxSearchEventHabitat;
        private System.Windows.Forms.Label labelEventHabitatCaption;
        private System.Windows.Forms.TextBox textBoxSearchEventLocality;
        private System.Windows.Forms.Label labelEventLocalityCaption;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonEventOR;
        private System.Windows.Forms.RadioButton radioButtonEventAND;
        private System.Windows.Forms.Panel panelSearchSpecimen;
        private System.Windows.Forms.TextBox textBoxSearchSpecimenNotes;
        private System.Windows.Forms.Label labelCollectionSpecimenNotesCaption;
        private System.Windows.Forms.TextBox textBoxSearchSpecimenDepositor;
        private System.Windows.Forms.Label labelCollectionSpecimenDepositorCaption;
        private System.Windows.Forms.TextBox textBoxSearchSpecimenAccessionNumber;
        private System.Windows.Forms.Label labelCollectionSpecimenAccessionNumberCaption;
        private System.Windows.Forms.Button buttonSearchSpecimenCancel;
        private System.Windows.Forms.Label labelSearchSpecimenCaption;
        private System.Windows.Forms.RadioButton radioButtonSpecimenOR;
        private System.Windows.Forms.RadioButton radioButtonSpecimenAND;
        private System.Windows.Forms.Button buttonSearchForEvent;
        private System.Windows.Forms.Button buttonSearchForSpecimen;
        private System.Windows.Forms.Button buttonSearchForIU;
        private System.Windows.Forms.Panel panelSearchResults;
        private System.Windows.Forms.Button buttonShowDetails;
        private System.Windows.Forms.Button buttonLoadInTree;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnNumber;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnNotes;
        private System.Windows.Forms.Label labelSearchResultsCaption;
        private System.Windows.Forms.Panel panelSearchIU;
        private System.Windows.Forms.RadioButton radioButtonIUOR;
        private System.Windows.Forms.RadioButton radioButtonIUAND;
        private System.Windows.Forms.TextBox textBoxSearchIUNotes;
        private System.Windows.Forms.Label labelIUNotesCaption;
        private System.Windows.Forms.TextBox textBoxSearchIUUnitIdentifier;
        private System.Windows.Forms.TextBox textBoxSearchIULastIdentification;
        private System.Windows.Forms.TextBox textBoxSearchIUTaxonomy;
        private System.Windows.Forms.Label labelUnitIdentifier;
        private System.Windows.Forms.Label labelUUnitDescriptionCaption;
        private System.Windows.Forms.Label labelLastIdentificationCacheCaption;
        private System.Windows.Forms.Label labelTaxonomicGroupCaption;
        private System.Windows.Forms.Button buttonSearchIUCancel;
        private System.Windows.Forms.Button buttonSearchIU;
        private System.Windows.Forms.Label labelSearchIUCaption;
        private System.Windows.Forms.Button buttonSearchSpecimen;
        private System.Windows.Forms.MainMenu mainMenu1;
        private UBT.AI4.Toolbox.Controls.ClickComboBox comboBoxIUUnitDescription;
    }
}
