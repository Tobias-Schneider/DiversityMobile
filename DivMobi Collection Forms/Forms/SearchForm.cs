using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.Forms.Dialogs;
using UBT.AI4.Bio.DivMobi.Forms.ContextForms;
using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    public partial class SearchForm : DialogBase
    {
        private SearchResults _results = null;
        public int resultID = 0;
        public string type = "";

        public SearchForm():base()
        {
            InitializeComponent();
            base.adjustControlSizes();
#if DEBUG
            this.MinimizeBox = false;
            this.ControlBox = true;
#else
            this.MinimizeBox = true;
            this.ControlBox = true;            
#endif

            // further initialization
            try
            {
                this._results = SearchResults.Instance;
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Close();
            }
            this.panelSearchChoice.Dock = DockStyle.Fill;
            this.panelSearchEvent.Dock = DockStyle.Fill;
            this.panelSearchSpecimen.Dock = DockStyle.Fill;
            this.panelSearchIU.Dock = DockStyle.Fill;
            this.panelSearchResults.Dock = DockStyle.Fill;
        }

        private void paintBorder(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(
                new Pen(Color.Black),
                0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
        }

        private void buttonSearchEventCancel_Click(object sender, EventArgs e)
        {
            this.textBoxSearchEventNotes.Text = "";
            this.textBoxSearchEventNumber.Text = "";
            this.textBoxSearchEventLocality.Text = "";
            this.textBoxSearchEventHabitat.Text = "";
            this.panelSearchEvent.Visible = false;
            this.panelSearchChoice.Visible = true;
        }

        private void buttonSearchEvent_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // Fill ListView on SearchResult Panel
            Dictionary<String, String> searchStrings = new Dictionary<string, string>();
          
            // @EventNumber, @EventDate, @EventNotes, @EventLocality, @EventHabitat
       
            // 1. String: Number
            if (!(this.textBoxSearchEventNumber.Text.Equals(String.Empty) || this.textBoxSearchEventNumber.Text.Equals("")))
                searchStrings.Add("_CollectorsEventNumber", String.Concat("%",this.textBoxSearchEventNumber.Text,"%"));

            // 2. String: Notes
            if (!(this.textBoxSearchEventNotes.Text.Equals(String.Empty) || this.textBoxSearchEventNotes.Text.Equals("")))
                searchStrings.Add("_Notes",String.Concat("%",this.textBoxSearchEventNotes.Text,"%"));

            // 3. String: Locality
            if (!(this.textBoxSearchEventLocality.Text.Equals(String.Empty) || this.textBoxSearchEventLocality.Text.Equals("")))
                searchStrings.Add("_LocalityDescription",String.Concat("%",this.textBoxSearchEventLocality.Text,"%"));

            // 4. String: Habitat
            if (!(this.textBoxSearchEventHabitat.Text.Equals(String.Empty) || this.textBoxSearchEventHabitat.Text.Equals("")))
                searchStrings.Add("_HabitatDescription", String.Concat("%", this.textBoxSearchEventHabitat.Text, "%"));

            foreach (ListViewItem item in _results.searchEventResults(searchStrings, this.radioButtonEventAND.Checked))
            {
                if (item != null)
                    this.listViewResults.Items.Add(item);
            }

            Cursor.Current = Cursors.Default;

            this.listViewResults.Columns[0].Text = "ID";
            this.listViewResults.Columns[1].Text = "Number";
            this.listViewResults.Columns[2].Text = "Date";
            this.listViewResults.Columns[3].Text = "Notes";

            if (this.listViewResults.Items.Count > 0)
            {
                this.panelSearchEvent.Visible = false;
                this.listViewResults.Tag = "Event";
                this.panelSearchResults.Visible = true;
            }
            else
            {
                // Mitteilung an User, dass keine Events zu den Suchkriterien gefunden wurden
                MessageBox.Show("No Events found for search criterias","Search Result",MessageBoxButtons.OK,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonSearchSpecimen_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // Fill ListView on SearchResult Panel
            Dictionary<String, String> searchStrings = new Dictionary<string, string>();

            // 1. String: AccessionNumber
            if (!(this.textBoxSearchSpecimenAccessionNumber.Text.Equals(String.Empty) || this.textBoxSearchSpecimenAccessionNumber.Text.Equals("")))
                searchStrings.Add("_AccessionNumber", String.Concat("%",this.textBoxSearchSpecimenAccessionNumber.Text, "%"));

            // 2. String: DepositorsName
            if (!(this.textBoxSearchSpecimenDepositor.Text.Equals(String.Empty) || this.textBoxSearchSpecimenDepositor.Text.Equals("")))
                searchStrings.Add("_DepositorsName",String.Concat("%",this.textBoxSearchSpecimenDepositor.Text, "%"));

            // 3. String: OriginalNotes + AdditionalNotes
            if (!(this.textBoxSearchSpecimenNotes.Text.Equals(String.Empty) || this.textBoxSearchSpecimenNotes.Text.Equals("")))
            {
                searchStrings.Add("_OriginalNotes", String.Concat("%",this.textBoxSearchSpecimenNotes.Text, "%"));
                searchStrings.Add("_AdditionalNotes", String.Concat("%", this.textBoxSearchSpecimenNotes.Text, "%"));
            }

            foreach (ListViewItem item in _results.searchSpecimenResults(searchStrings, this.radioButtonSpecimenAND.Checked))
            {
                if (item != null)
                    this.listViewResults.Items.Add(item);
            }
            Cursor.Current = Cursors.Default;
            
            this.listViewResults.Columns[0].Text = "ID";
            this.listViewResults.Columns[1].Text = "Number";
            this.listViewResults.Columns[2].Text = "Date";
            this.listViewResults.Columns[3].Text = "Depositor";


            if (this.listViewResults.Items.Count > 0)
            {
                this.panelSearchSpecimen.Visible = false;
                this.listViewResults.Tag = "Specimen";
                this.panelSearchResults.Visible = true;
            }
            else
            {
                // Mitteilung an User, dass keine Events zu den Suchkriterien gefunden wurden
                MessageBox.Show("No Specimen found for search criterias", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
        }

        private void buttonSearchSpecimenCancel_Click(object sender, EventArgs e)
        {
            this.textBoxSearchSpecimenAccessionNumber.Text = "";
            this.textBoxSearchSpecimenDepositor.Text = "";
            this.textBoxSearchSpecimenNotes.Text = "";
            this.panelSearchSpecimen.Visible = false;
            this.panelSearchChoice.Visible = true;
        }

        private void buttonSearchIU_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // Fill ListView on SearchResult Panel
            Dictionary<String,String> searchStrings = new Dictionary<string,string>();
            // 1. String: UnitIdentifier
            if (!(this.textBoxSearchIUUnitIdentifier.Text.Equals(String.Empty) || this.textBoxSearchIUUnitIdentifier.Text.Equals("")))
                searchStrings.Add("_UnitIdentifier", String.Concat("%", this.textBoxSearchIUUnitIdentifier.Text, "%"));
            
            // 2. String: Last Identification
            if (!(this.textBoxSearchIULastIdentification.Text.Equals(String.Empty) || this.textBoxSearchIULastIdentification.Text.Equals("")))
                searchStrings.Add("_LastIdentificationCache", String.Concat("%", this.textBoxSearchIULastIdentification.Text, "%"));
            
            // 3. String: Taxonomy
            if (!(this.textBoxSearchIUTaxonomy.Text.Equals(String.Empty) || this.textBoxSearchIUTaxonomy.Text.Equals("")))
                searchStrings.Add("_TaxonomicGroup", String.Concat("%", this.textBoxSearchIUTaxonomy.Text, "%"));
            
            // 4. String: Unit Description
            if (!(this.comboBoxIUUnitDescription.Text.Equals("") || this.comboBoxIUUnitDescription.Text.Equals(String.Empty)))
                searchStrings.Add("_UnitDescription", String.Concat("%", this.comboBoxIUUnitDescription.Text, "%"));

            // 5. String: Notes
            if (!(this.textBoxSearchIUNotes.Text.Equals(String.Empty) || this.textBoxSearchIUNotes.Text.Equals("")))
                searchStrings.Add("_Notes", String.Concat("%", this.textBoxSearchIUNotes.Text, "%"));

            foreach (ListViewItem item in _results.searchIUResults(searchStrings, this.radioButtonIUAND.Checked))
            {
                if (item != null)
                    this.listViewResults.Items.Add(item);
            }

            Cursor.Current = Cursors.Default;

            this.listViewResults.Columns[0].Text = "ID";
            this.listViewResults.Columns[1].Text = "Identification";
            this.listViewResults.Columns[2].Text = "Taxonomy";
            this.listViewResults.Columns[3].Text = "Identifier";

            if (this.listViewResults.Items.Count > 0)
            {
                this.panelSearchIU.Visible = false;
                this.listViewResults.Tag = "IU";
                this.panelSearchResults.Visible = true;
            }
            else
            {
                // Mitteilung an User, dass keine Events zu den Suchkriterien gefunden wurden
                MessageBox.Show("No Identification Unit found for search criterias", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }   
        }

        private void buttonSearchIUCancel_Click(object sender, EventArgs e)
        {
            this.textBoxSearchIUTaxonomy.Text = "";
            this.textBoxSearchIUUnitIdentifier.Text = "";
            this.textBoxSearchIULastIdentification.Text = "";
            this.textBoxSearchIUNotes.Text = "";
            this.comboBoxIUUnitDescription.SelectedIndex = 0;
            this.panelSearchIU.Visible = false;
            this.panelSearchChoice.Visible = true;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            this.paintBorder(e);
        }

        private void listViewResults_ItemActivate(object sender, EventArgs e)
        {
            this.loadInTree();
        }

        private void buttonLoadInTree_Click(object sender, EventArgs e)
        {
            this.loadInTree();
        }

        private void buttonShowDetails_Click(object sender, EventArgs e)
        {
            try
            {
                this.showDetails();
            }
            catch (ContextCorruptedException ex)
            {
                MessageBox.Show("Details can't be displayed. (" + ex.Message + ")", "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void showDetails()
        {
            Cursor.Current = Cursors.WaitCursor;
            ListView.SelectedIndexCollection results = listViewResults.SelectedIndices;
            foreach (int index in results)
            {
                if (index != null && index >= 0)
                {
                    int id = (int)listViewResults.Items[index].Tag;
                    this._results.setID(id);

                    try
                    {
                        // Display Selected Item in DetailWindow
                        switch ((String)listViewResults.Tag)
                        {
                            case "Event":
                                new CollectionEventForm(_results.CE).ShowDialog();
                                break;
                            case "Specimen":
                                new SpecimenEditForm(_results.CS).ShowDialog();
                                break;
                            case "IU":
                                new IdentificationUnitForm(_results.IU).ShowDialog();
                                break;
                        }
                    }
                    catch (ContextCorruptedException ex)
                    {
                        throw ex;
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void loadInTree()
        {
            // Load Selected Item in TreeView of MainForm
            this.type = (String)this.listViewResults.Tag;

            ListView.SelectedIndexCollection results = listViewResults.SelectedIndices;
            foreach (int index in results)
            {
                if (index != null && index >= 0)
                    this.resultID = (int)listViewResults.Items[index].Tag;
            }

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void buttonSearchForEvent_Click(object sender, EventArgs e)
        {
            this.panelSearchChoice.Visible = false;
            this.panelSearchEvent.Visible = true;
        }

        private void buttonSearchForSpecimen_Click(object sender, EventArgs e)
        {
            this.panelSearchChoice.Visible = false;
            this.panelSearchSpecimen.Visible = true;
        }

        private void buttonSearchForIU_Click(object sender, EventArgs e)
        {
            this.panelSearchChoice.Visible = false;
            this.panelSearchIU.Visible = true;
        }
    }
}

