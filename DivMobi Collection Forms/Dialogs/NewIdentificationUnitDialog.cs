using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using System.Collections;

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;

using UBT.AI4.Toolbox.Controls;

//GPS
using UBT.AI4.Bio.DiversityCollection.Ressource.GPS;


namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class NewIdentificationUnitDialog : DialogBase, ILayouted
    {
        # region Internal Types
        /// <summary>
        /// Specifies the index of icons in the icon image list.
        /// </summary>
        private enum IconIndex : int
        {
            unknown = 0,
            tree = 1,
            branch = 2,
            leaf = 3,
            other = 4,
            alga = 5,
            assel = 6,
            bacterium = 7,
            bird = 8,
            bryophyte = 9,
            fish = 10,
            fungus = 11,
            insect = 12,
            lichen = 13,
            mammal = 14,
            mollusc = 15,
            myxomycete = 16,
            virus = 17
        }

        # endregion

        private ILayout layout;
        private String formName;
        private String formDescription;

        private IdentificationUnit parent;
        private IdentificationUnit current;
        private TaxonNames tax;
        private short displayOrder;
        private CollectionSpecimen cs;
        private ImageList imgList = new ImageList();
        private IdentificationUnits _ius = null;
        private String _taxGroup = "";
        private String _lastIdentification = "";
        private bool searchTrigger;

        # region Constructors

        // Default Constructor for creating instance without initialization of components
        public NewIdentificationUnitDialog():base() 
        {
            this.formName = "New Identification Unit Dialog";
            this.formDescription = "Create new Identification Unit";
        }

        public NewIdentificationUnitDialog(bool loadContext)
            : this()
        {
            this.InitializeComponent();
            base.adjustControlSizes();
            try
            {
                if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("large"))
                {
                    imgList = this.imageListLarge;
                    this.pictureBoxIUImage.Size = new Size(48, 48);
                }
                else if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("medium"))
                {
                    imgList = this.imageListMedium;
                    this.pictureBoxIUImage.Size = new Size(32, 32);
                }
                else
                {
                    imgList = this.imageList;
                    this.pictureBoxIUImage.Size = new Size(16, 16);
                }
                
            }
            catch (ConnectionCorruptedException)
            {
                imgList = this.imageList;
                this.pictureBoxIUImage.Size = new Size(16, 16);
            }

            if (loadContext)
            {
                // Further initialization
                try
                {
                    //Layout erzeugen (Keine Überschneidungen)
                    layout = LayoutFactory.Instance.CreateLayout(this, false);
                    //Beim Kontextmanager registrieren
                    ContextManager.Instance.Register(this);
                }
                catch (ContextCorruptedException ex)
                {
                    throw new ContextCorruptedException("Form can't be shown and will be closed. (" + ex.Message + ")");
                }

                //Kontext ausführen
                //Sprachkontext
                String language = null;
                try
                {
                    language = UserProfiles.Instance.Current.LanguageContext;
                }
                catch (ConnectionCorruptedException) { }

                try
                {
                    if (language != null && !language.Equals(String.Empty))
                    {
                        if (ContextManager.Instance.GetContext(language) != null)
                            ContextManager.Instance.GetContext(language).Configure(this);
                        else
                        {
                            throw new ContextCorruptedException("Form can't be shown and will be closed. (" + language + " context doesn't exist)");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new ContextCorruptedException("Form can't be shown and will be closed. (Error while configuring language context)");
                }

                //Weiterer Kontext
                String context = null;
                try
                {
                    context = UserProfiles.Instance.Current.Context;
                }
                catch (ConnectionCorruptedException) { }

                try
                {
                    if (context != null && !context.Equals(String.Empty))
                    {

                        if (ContextManager.Instance.GetContext(context) != null)
                            ContextManager.Instance.GetContext(context).Configure(this);
                        else
                        {
                            throw new ContextCorruptedException("Form can't be shown and will be closed. (" + context + " context doesn't exist)");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new ContextCorruptedException("Form can't be shown and will be closed. (Error while configuring " + context + " context)");
                }
            }
            searchTrigger = true;
        }

        public NewIdentificationUnitDialog(CollectionSpecimen spec, IdentificationUnits ius):this(true)
        {
            if (ius != null && spec != null)
            {
                this.cs = spec;
                this._ius = ius;
                
                this.comboBoxTaxonomicGroup_TextChanged(null, null);

                this.parent = null;
                try
                {
                    tax = DataFunctions.Instance.CreateTaxonNames();
                    this.buttonOk.Enabled = false;
                    this.buttonNewAnalysis.Enabled = false;
                    this.comboBoxRelation.Enabled = false;
                    this.fillTaxonomicGroupList();
                    if (this.comboBoxTaxonomicGroup.Items.Count > 0)
                    {
                        this.comboBoxTaxonomicGroup.SelectedItem = this.comboBoxTaxonomicGroup.Items[0];
                    }

                    // Liste aller IdentificationUnits des aktuellen Specimen, um DiplayOrder zu finden
                    short order = (short)DataFunctions.Instance.RetrieveMaxDisplayOrderCollectionSpecimenIdentificationUnit(spec);
                    if (order > 0)
                        this.displayOrder = (short)(order + 1);
                    else
                        this.displayOrder = 1;
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
            }
            else
                this.Close();
        }

        public NewIdentificationUnitDialog(CollectionSpecimen spec, IdentificationUnit parentUnit, IdentificationUnits ius)
            : this(spec, ius)
        {
            try
            {
                this.parent = parentUnit;
                if (this.parent != null)
                {
                    this.comboBoxTaxonomicGroup.SelectedItem = this._taxGroup = parent.TaxonomicGroup;

                    if (parent.LastIdentificationCache != null)
                    {
                        this.comboBoxIdentification.Items.Add(parent.LastIdentificationCache);
                        this.comboBoxIdentification.SelectedItem = this._lastIdentification = parent.LastIdentificationCache;
                        TaxonNames taxparent = DataFunctions.Instance.RetrieveTaxon(parent.LastIdentificationCache, parent.TaxonomicGroup);
                        if (taxparent != null)
                            tax = taxparent;
                        else
                            tax.TaxonNameCache = parent.LastIdentificationCache;
                        setSynonym(tax);
                    }
                }

                fillRelationList();

                this.comboBoxRelation.Enabled = true;

                short order = (short)DataFunctions.Instance.RetrieveMaxDisplayOrderCollectionSpecimenIdentificationUnit(spec);
                if (order > 0)
                    this.displayOrder = (short)(order + 1);
                else
                    this.displayOrder = 1;

                this.buttonOk.Enabled = this.setOkButton();
                this.buttonNewAnalysis.Enabled = this.setOkButton();
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Close();
            }
        }

        #endregion

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        # region Properties

        public IdentificationUnit Current
        {
            get { return this.current; }
        }

        public String Identification
        {
            get {
                if (this.tax != null)
                    return this.tax.TaxonNameCache;
                else
                    return null;
            }
        }

        public String IdentificationURI
        {
            get {
                if (this.tax != null)
                    return this.tax.NameURI;
                else
                    return null; 
                }
        }

        public String Family
        {
            get {
                if (this.tax != null)
                    return this.tax.Family;
                else
                    return null;
            }
        }

        public String Order
        {
            get {
                if (this.tax != null)
                    return this.tax.Order;
                else
                    return null;
            }
        }

        public String TaxonomicGroup
        {
            get
            {
                return this.comboBoxTaxonomicGroup.Text;
            }
        }

        public String Relation
        {
            get
            {
                if (this.comboBoxRelation.Enabled)
                {
                    if(this.comboBoxRelation.Text.Equals(String.Empty))
                        return null;
                    return this.comboBoxRelation.Text;                   
                }
                else
                    return null;
            }
        }

        public String UnitDescription
        {
            get
            {
                return this.comboBoxUnitDescription.Text;
            }
        }

        public String UnitIdentifier
        {
            get
            {
                return this.textBoxUnitIdentifier.Text;
            }
        }

        public short DisplayOrder
        {
            get
            {
                return this.displayOrder;
            }
        }

        #endregion

        # region Events

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxSearch.Text.Trim().Length >= 1)
            {
                try
                {
                    // Ausgabe des Zeichens verhindern
                    TaxonNames actual = DataFunctions.Instance.CreateTaxonNames();
                    actual.TaxonNameCache = this.textBoxSearch.Text;//Unterstützung von Arbeitsnamen
                    actual.NameURI = String.Empty;
                    String input = this.textBoxSearch.Text.Trim();
                    input += "%";
                    input = input.Replace(" ", "% ");
                    input = input.Replace("'", "''");
                    this.comboBoxIdentification.Items.Clear();
                    comboBoxIdentification.Items.Insert(0, actual.TaxonNameCache);
                    this.comboBoxIdentification.SelectedItem = actual.TaxonNameCache;
                    IList<string> Ilist = DataFunctions.Instance.RetrieveTaxonCache(input, this.comboBoxTaxonomicGroup.Text);//Auswahl der TaxonomischenGruppe gewährleisten
                    foreach (string item in Ilist)
                    {
                        if (item != null)
                        {
                            comboBoxIdentification.Items.Add(item);
                            if (item.Equals(actual.TaxonNameCache))//prüfen,ob der  Arbeitsname bereits eine korrekte Bezeichnung ist. Falls ja diesen durch das vollständige Objekt ersetzen
                            {
                                comboBoxIdentification.SelectedItem = item;
                            }
                        }
                    }
                    tax = actual;
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.comboBoxIdentification.Items.Clear();
                }
            }
            setSynonym(tax);
            this.buttonOk.Enabled = this.setOkButton();
            this.buttonNewAnalysis.Enabled = this.setOkButton();
        }

        private void comboBoxIdentification_TextChanged(object sender, EventArgs e)       
        {
            if (comboBoxIdentification.Text != null)
            {
                try
                {
                    TaxonNames taxon = null;
                    taxon = DataFunctions.Instance.RetrieveTaxon(this.comboBoxIdentification.Text, this.comboBoxTaxonomicGroup.Text);
                    if (taxon != null)
                        tax = taxon;
                    else
                    {
                        tax = new TaxonNames();
                        tax.TaxonNameCache = comboBoxIdentification.Text;
                        tax.NameURI = String.Empty;
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    tax = new TaxonNames();
                }
            }
            else
            {
                tax = new TaxonNames();
            }
            setSynonym(tax);
            this.buttonOk.Enabled = this.setOkButton();
            this.buttonNewAnalysis.Enabled = this.setOkButton();
        }

        private void comboBoxTaxonomicGroup_TextChanged(object sender, EventArgs e)
        {
            this.comboBoxUnitDescription.Items.Clear();

            if (this.TaxonomicGroup != null)
            {
                if (this.TaxonomicGroup.Equals("plant"))
                {
                    this.comboBoxUnitDescription.Items.Add("tree");
                    this.comboBoxUnitDescription.Items.Add("branch");
                    this.comboBoxUnitDescription.Items.Add("leaf");
                    this.comboBoxUnitDescription.Items.Add("gall");
                    this.pictureBoxIUImage.Image = this.imgList.Images[(int)IconIndex.leaf];//Wenn keine Description angegeben ist, wird eine Pflanze durch einen Baum symbolosiert.
                }
                else
                {
                    int index;
                    try
                    {
                        index = (int)Enum.Parse(typeof(IconIndex), this.TaxonomicGroup, true);

                    }
                    catch (Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        index = (int)IconIndex.unknown;
                    }
                    this.pictureBoxIUImage.Image = this.imgList.Images[index];
                }
            }
            this.comboBoxIdentification.Items.Clear();
            this.comboBoxIdentification.Text = String.Empty;
            this.tax = new TaxonNames();
            setSynonym(tax);
            setOkButton();
            if (this.TaxonomicGroup != null)
                this.comboBoxIdentification.Enabled = true;
        }

        private void comboBoxTaxonomicGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxIdentification.Items.Clear();
            this.buttonOk.Enabled = this.setOkButton();
            this.buttonNewAnalysis.Enabled = this.setOkButton();
        }

        private void comboBoxUnitDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.UnitDescription)
            {
                case "tree":
                    this.pictureBoxIUImage.Image = this.imgList.Images[(int)IconIndex.tree];
                    break;
                case "branch":
                    this.pictureBoxIUImage.Image = this.imgList.Images[(int)IconIndex.branch];
                    break;
                case "leaf":
                    this.pictureBoxIUImage.Image = this.imgList.Images[(int)IconIndex.leaf];
                    break;
                case "gall":
                    this.pictureBoxIUImage.Image = this.imgList.Images[(int)IconIndex.other];
                    break;
            }
        }

        private void comboBoxUnitDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            // MaxLength for Unitdescription = 50
            if (this.comboBoxUnitDescription.Text.Length > 49 && !e.KeyChar.Equals('\r') && !e.KeyChar.Equals('\b'))
            {
                // no more insert possible
                e.Handled = true;
            }
        }

        # endregion

        # region FillLists 

        private void fillTaxonomicGroupList()
        {
            try
            {
                IList<CollTaxonomicGroup_Enum> list = DataFunctions.Instance.RetrieveTaxonomicGroups();

                foreach (CollTaxonomicGroup_Enum item in list)
                {
                    if (item != null)
                        this.comboBoxTaxonomicGroup.Items.Add(item.Code);
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                throw ex;
            }
        }

        private void fillRelationList()
        {
            try
            {
                IList<CollUnitRelationType_Enum> list = DataFunctions.Instance.RetrieveUnitRelationTypes();

                foreach (CollUnitRelationType_Enum item in list)
                {
                    if (item != null)
                        this.comboBoxRelation.Items.Add(item.Code);
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                throw ex;
            }
        }
        # endregion

        private bool setOkButton()
        {
            if (this.comboBoxIdentification.Text.Equals(String.Empty) || this.comboBoxTaxonomicGroup.Text.Equals(String.Empty))
            {
                return false;
            }
            return true;
        }

        private void setSynonym(TaxonNames tax)
        {
            if (tax != null)
            {
                if (tax.Synonymy != null)
                {
                    if (tax.Synonymy.Equals("Synonym"))
                    {
                        labelSynonym.Text = "Synonym";
                        labelSynonym.BackColor = Color.Red;
                    }
                    if (tax.Synonymy.Equals("Accepted"))
                    {
                        labelSynonym.Text = "Accepted";
                        labelSynonym.BackColor = Color.Green;
                    }
                }
                else
                {
                    labelSynonym.Text = "Working Name";
                    labelSynonym.BackColor = Color.White;
                }
            }
        }

        public void Dispose()
        {
            ContextManager.Instance.UnRegisterAll(this);
        }

        private void NewIdentificationUnitDialog_Closing(object sender, CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                if (MessageBox.Show("Data won't be saved. Do You really want to leave the Dialog?", "Data not saved", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if (this.DialogResult == DialogResult.Abort)
            {
                e.Cancel = true;
                return;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.saveIU())
                this.DialogResult = DialogResult.Abort;

            ContextManager.Instance.UnRegisterAll(this);
        }

        private bool saveIU()
        {
            Cursor.Current = Cursors.WaitCursor;
            IdentificationUnit iu=null;
            try
            {
                if (this.parent == null)
                    iu = this._ius.CreateTopLevelIdentificationUnit(Identification);
                else
                    iu = this._ius.CreateIdentificationUnit(Identification);
            }
            catch (DataFunctionsException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message + " Identification and Identification Unit couldn't be saved.", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }


            if (iu != null)
            {
                this.current = iu;
                this._lastIdentification = iu.LastIdentificationCache;
                iu.TaxonomicGroup = this._taxGroup = TaxonomicGroup;

                // Create Identification for new IU
                try
                {
                    DataFunctions.Instance.CreateIdentificationWithCheck(iu);
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message + " Identification and Identification Unit couldn't be saved.", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    try
                    {
                        if (this.parent == null)
                        {
                            this._ius.RemoveTopLevelIU();
                        }
                        else
                            this._ius.Remove(iu);
                    }
                    catch (DataFunctionsException) { }

                    return false;
                }

                if (UnitDescription != null && !UnitDescription.Equals(String.Empty))
                    iu.UnitDescription = UnitDescription;

                if (Family != null && !Family.Equals(String.Empty))
                    iu.FamilyCache = Family;

                if (Order != null && !Order.Equals(String.Empty))
                    iu.OrderCache = Order;

                if (UnitIdentifier != null && !UnitIdentifier.Equals(String.Empty))
                    iu.UnitIdentifier = UnitIdentifier;

                iu.CollectionSpecimenID = cs.CollectionSpecimenID;

                iu.DisplayOrder = DisplayOrder;

                if (this.parent != null)//hat einen parent
                {
                    iu.RelatedUnitID = parent.IdentificationUnitID;
                    iu.RelationType = Relation;
                }
                try
                {
                    DataFunctions.Instance.Update(iu);
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Identification Unit Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return false;
                }

                if (UserProfiles.Instance.Current.DefaultIUGeographyAnalysis == true)
                {
                    try
                    {
                        // Create new IUGeographyAnalysis automatically
                        if (this.InvokeRequired)
                        {
                            newGeoAnalysisDelegate geoDelegate = new newGeoAnalysisDelegate(newGeoAnalysis);
                            this.Invoke(geoDelegate, iu);
                        }
                        else
                        {
                            this.newGeoAnalysis(iu);
                        }
                    }
                    catch (DataFunctionsException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Associated GeoAnalysis with IU couldn't be automatically created. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
            }  
            Cursor.Current = Cursors.Default;
            return true;
        }

        private delegate void newGeoAnalysisDelegate(IdentificationUnit iu);

        private void newGeoAnalysis(IdentificationUnit iu)
        {
            if (iu != null)
            {
                IdentificationUnitGeoAnalysis iuGeoAnalysis = null;

                try
                {
                    if (UserProfiles.Instance.Current != null && (bool)UserProfiles.Instance.Current.DefaultIUGeographyAnalysis)
                    {
                        try
                        {
                            iuGeoAnalysis = DataFunctions.Instance.CreateIdentificationUnitGeoAnalysis(iu, iu.CollectionSpecimen);
                        }
                        catch (DataFunctionsException ex)
                        {
                            throw ex;
                        }

                        if (StaticGPS.isOpened())
                        {
                            if (StaticGPS.position != null)
                            {
                                try
                                {
                                    float latitude = float.Parse(StaticGPS.position.Latitude.ToString());
                                    float longitude = float.Parse(StaticGPS.position.Longitude.ToString());
                                    float altitude = float.Parse(StaticGPS.position.SeaLevelAltitude.ToString());
                                    iuGeoAnalysis.setGeography(48.2, 11.8, 230.12);
                                }
                                catch (Exception)
                                {
                                    Cursor.Current = Cursors.Default;
                                    throw new DataFunctionsException("GPS-Data couldn`t be read. Data will be set to default values.");
                                }
                            }
                        }
                        try
                        {
                            if (UserProfiles.Instance.Current != null)
                            {
                                iuGeoAnalysis.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;
                                iuGeoAnalysis.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                            }
                        }
                        catch (ConnectionCorruptedException) { }

                        try
                        {
                            DataFunctions.Instance.Update(iuGeoAnalysis);
                        }
                        catch (DataFunctionsException ex)
                        {
                            Cursor.Current = Cursors.Default;
                            DataFunctions.Instance.Remove(iuGeoAnalysis);
                            throw ex;
                        }
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    throw new DataFunctionsException(ex.Message);
                }
            }
        }

        private void clearTextBoxes()
        {
            this.buttonOk.Enabled = false;
            this.buttonNewAnalysis.Enabled = false;

            if (this.comboBoxTaxonomicGroup.Items.Count > 0)
            {
                if (this._taxGroup.Equals(String.Empty))
                    this.comboBoxTaxonomicGroup.SelectedItem = this.comboBoxTaxonomicGroup.Items[0];
                else
                    this.comboBoxTaxonomicGroup.SelectedItem = this._taxGroup;
            }

            // Liste aller IdentificationUnits des aktuellen Specimen, um DiplayOrder zu finden
            try
            {
                int order = (int)DataFunctions.Instance.RetrieveMaxDisplayOrderCollectionSpecimenIdentificationUnit(this.cs);
                if (order > 0)
                    this.displayOrder = (short)(order + 1);
                else
                    this.displayOrder = 1;
            }
            catch (ConnectionCorruptedException)
            {
                this.displayOrder = 1;
            }

            this.textBoxSearch.Text = "";

            if (!this._lastIdentification.Equals(String.Empty))
            {
                this.comboBoxIdentification.Items.Add(this._lastIdentification);
                this.comboBoxIdentification.SelectedItem = this._lastIdentification;
                try
                {
                    TaxonNames taxparent = DataFunctions.Instance.RetrieveTaxon(this._lastIdentification, this._taxGroup);
                    if (taxparent != null)
                        tax = taxparent;
                    else
                        tax.TaxonNameCache = this._lastIdentification;
                }
                catch (ConnectionCorruptedException)
                {
                    tax.TaxonNameCache = this._lastIdentification;
                }
                setSynonym(tax);
            }
            this.panelTop.Focus();
            this.Refresh();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save current Identification Unit and create a new one with same level?","Save & New",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (this.saveIU())
                {
                    // Clear TextBoxes
                    this.clearTextBoxes();
                }
                else
                {
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                }
            }
        }

        private void buttonNewAnalysis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save current Identification Unit and create new Analysis for it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            if (this.saveIU())
            {
                try
                {
                    // Erlaubte Analysen für taxonomoische Gruppe werden geladen
                    IList<Analysis> list = DataFunctions.Instance.RetrievePossibleAnalysis(TaxonomicGroup);

                    if (list.Count < 1)
                    {
                        MessageBox.Show("There is no selectable analysis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    NewAnalysisDialog nad = null;
                    try
                    {
                        if (UserProfiles.Instance.Current.LastAnalysisID != null)
                            nad = new NewAnalysisDialog((int)UserProfiles.Instance.Current.LastAnalysisID);
                        else
                            nad = new NewAnalysisDialog(true);

                        if (nad == null)
                            return;
                    }
                    catch (ContextCorruptedException ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    // Dialog zentrieren
                    nad.Location = new Point((this.Size.Width) / 2 - (nad.Size.Width) / 2, this.Location.Y);
                    nad.Analysis = list;

                    Cursor.Current = Cursors.Default;
                    if (nad.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        String analysisValue = nad.Value;
                        int analysisPerformed = nad.PerformedAnalysis;
                        DateTime analysisDate = nad.AnalysisDate;
                        IdentificationUnitAnalysis iua;

                        try
                        {
                            if (this.current != null)
                                iua = DataFunctions.Instance.CreateIdentificationUnitAnalysis(this.current, analysisPerformed, analysisValue, analysisDate);
                            else
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Associated Analysis with IU couldn't be created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Associated Analysis with IU couldn't be created. ("+ex.Message+")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        // edit last performed analysis in UserProfile
                        if (UserProfiles.Instance.Current != null)
                        {
                            int oldAnalysis = -1;
                            if (UserProfiles.Instance.Current.LastAnalysisID != null)
                                oldAnalysis = (int)UserProfiles.Instance.Current.LastAnalysisID;

                            UserProfiles.Instance.Current.LastAnalysisID = nad.PerformedAnalysis;

                            try
                            {
                                UserProfiles.Instance.Update(UserProfiles.Instance.Current);
                            }
                            catch (UserProfileCorruptedException ex)
                            {
                                Cursor.Current = Cursors.Default;
                                if (oldAnalysis > -1)
                                    UserProfiles.Instance.Current.LastAnalysisID = oldAnalysis;
                                else
                                    UserProfiles.Instance.Current.LastAnalysisID = null;

                                MessageBox.Show(ex.Message + " Last performed analysis remains: " + oldAnalysis.ToString());
                            }
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
        }

        private void textBoxSearch_GotFocus(object sender, EventArgs e)
        {
            if (this.searchTrigger == true)
            {
                this.searchTrigger = false;
                TextInputDialog2 ti = new TextInputDialog2(this.labelIdentification.Text, this.textBoxSearch.Text);
                ti.Location = new Point(0, 30);
                if (ti.ShowDialog() == DialogResult.OK)
                    this.textBoxSearch.Text = ti.Value;
                this.searchTrigger = true;
            }
        }

        private void textBoxUnitIdentifier_GotFocus(object sender, EventArgs e)
        {
            if (this.searchTrigger == true)
            {
                this.searchTrigger = false;
                TextInputDialog2 ti = new TextInputDialog2(this.labelUnitIdentifierCaption.Text, this.textBoxUnitIdentifier.Text);
                ti.Location = new Point(0, 30);
                if (ti.ShowDialog() == DialogResult.OK)
                    this.textBoxUnitIdentifier.Text = ti.Value;
                this.searchTrigger = true;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            TaxonInputDialog2 ti = new TaxonInputDialog2(this.comboBoxTaxonomicGroup.Text);
            ti.Location = new Point(0, 30);
            if (ti.ShowDialog() == DialogResult.OK)
                this.textBoxSearch.Text = ti.Value;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}