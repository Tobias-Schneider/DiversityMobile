using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Toolbox;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DataManagement;
using System.Text.RegularExpressions;
using UBT.AI4.Bio.DivMobi.Forms;

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class IdentificationUnitForm : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        private String _oldIdentificationCache;
        private IdentificationUnit _iu = null;

        // Default Constructor for creating instance without initialization of components
        public IdentificationUnitForm() :base()
        {
            this.formName = "Identification Unit Form";
            this.formDescription = "Edit existing Identification Unit";
            
            
        }

        public IdentificationUnitForm(bool loadContext)
            : this()
        {
            // initialization related to windows forms
            InitializeComponent();
#if DEBUG
            this.MinimizeBox = false;
            this.ControlBox = true;
#else
            this.MinimizeBox = true;
            this.ControlBox = true;            
#endif
            base.adjustControlSizes();
            if (loadContext)
            {
                // Fill Relation Type & Circumstances comboBox
                try
                {
                    this.fillRelationTypeList();
                    this.comboBoxRelationType.Enabled = false;
                    this.fillCircumstancesList();
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
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
        }

        public IdentificationUnitForm(IdentificationUnit currentIU)
            : this(true)
        {
            if (currentIU != null)
            {
                this._iu = currentIU;

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.fillIUData();
                    this._oldIdentificationCache = currentIU.LastIdentificationCache;
                    Cursor.Current = Cursors.Default;
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
            }
        }

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        private void fillRelationTypeList()
        {
            try
            {
                IList<CollUnitRelationType_Enum> list = DataFunctions.Instance.RetrieveUnitRelationTypes();

                this.comboBoxRelationType.Items.Clear();
                foreach (CollUnitRelationType_Enum item in list)
                {
                    if (item != null)
                        this.comboBoxRelationType.Items.Add(item.Code);
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                throw ex;
            }
        }

        private void fillCircumstancesList()
        {
            try
            {
                IList<CollCircumstances_Enum> list = DataFunctions.Instance.RetrieveCircumstancesTypes();
                this.comboBoxCircumstances.Items.Clear();
                foreach (CollCircumstances_Enum item in list)
                {
                    if (item != null)
                        this.comboBoxCircumstances.Items.Add(item.Code);
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                throw ex;
            }
        }

        private void fillIUData()
        {
            if (this._iu != null)
            {
                if (this._iu.LastIdentificationCache != null)
                {
                    this.comboBoxIdentification.Items.Clear();
                    this.comboBoxIdentification.Items.Add(this._iu.LastIdentificationCache);
                    this.comboBoxIdentification.SelectedIndex = 0;
                }

                if (this._iu.TaxonomicGroup != null)
                {
                    this.labelTaxonomicGroup.Text = this._iu.TaxonomicGroup;
                }

                if (this._iu.RelatedUnitID != null)
                {
                    try
                    {
                        IdentificationUnit iu = DataFunctions.Instance.RetrieveIdentificationUnit((int)this._iu.RelatedUnitID);
                        if (iu != null)
                        {
                            this.labelRelatedUnit.Text = iu.UnitIdentifier + " - " + iu.LastIdentificationCache;

                            this.comboBoxRelationType.Enabled = true;
                            this.comboBoxRelationType.SelectedItem = this._iu.RelationType;
                        }
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        throw ex;
                    }
                }

                if (_iu.UnitIdentifier != null && !_iu.UnitIdentifier.Equals(String.Empty))
                    this.textBoxUnitIdentifier.Text = _iu.UnitIdentifier;

                if (this._iu.TaxonomicGroup != null)
                {
                    this.comboBoxIUUnitDescription.Items.Clear();
                    if (this._iu.TaxonomicGroup.Equals("plant"))
                    {
                        this.comboBoxIUUnitDescription.Items.Add("tree");
                        this.comboBoxIUUnitDescription.Items.Add("branch");
                        this.comboBoxIUUnitDescription.Items.Add("leaf");
                        this.comboBoxIUUnitDescription.Items.Add("gall");
                    }
                    else
                    {
                        if (this._iu.UnitDescription != null)
                            this.comboBoxIUUnitDescription.Items.Add(this._iu.UnitDescription);
                    }

                    if (this._iu.UnitDescription != null && !this._iu.UnitDescription.Equals(String.Empty))
                        this.comboBoxIUUnitDescription.SelectedItem = this._iu.UnitDescription;
                }

                if (this._iu.OnlyObserved != null)
                    this.checkBoxIUOnlyObservedDetails.Checked = (bool)this._iu.OnlyObserved;

                if (this._iu.ColonisedSubstratePart != null)
                    this.textBoxColonisedSubstratePart.Text = this._iu.ColonisedSubstratePart;

                if (this._iu.LifeStage != null)
                    this.textBoxLifeStage.Text = this._iu.LifeStage;

                if (this._iu.Gender != null)
                    this.textBoxGender.Text = this._iu.Gender;

                if (this._iu.NumberOfUnits != null)
                    this.textBoxNumberOfUnits.Text = this._iu.NumberOfUnits.ToString();

                if (this._iu.Circumstances != null)
                {
                    this.comboBoxCircumstances.Text = this._iu.Circumstances;
                }

                if (this._iu.DisplayOrder != null)
                    this.textBoxDisplayOrder.Text = this._iu.DisplayOrder.ToString();
            }
        }

        private bool saveIUData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this._iu != null)
            {
                if (this.comboBoxIdentification.Text.Equals(String.Empty))
                {
                    MessageBox.Show("LastIdentificationCache Text Box is empty and therefore won't be changed");
                    Cursor.Current = Cursors.Default;
                    return false;
                }
                else
                {
                    this._iu.LastIdentificationCache = this.comboBoxIdentification.Text;

                    // Create new Identification if necessary
                    if (!this._iu.LastIdentificationCache.Equals(this._oldIdentificationCache))
                    {
                        try
                        {
                            DataFunctions.Instance.CreateIdentificationWithCheck(this._iu);
                        }
                        catch (Exception ex)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(ex.Message + " Identification Unit and Identification won't be changed.");
                            return false;
                        }
                    }
                }

                if (!this.textBoxUnitIdentifier.Text.Equals(String.Empty))
                    this._iu.UnitIdentifier = this.textBoxUnitIdentifier.Text;
                else
                    this._iu.UnitIdentifier = null;

                if (!this.comboBoxIUUnitDescription.Text.Equals(String.Empty))
                    this._iu.UnitDescription = this.comboBoxIUUnitDescription.Text;
                else
                    this._iu.UnitDescription = null;

                this._iu.OnlyObserved = this.checkBoxIUOnlyObservedDetails.Checked;

                if (!this.textBoxColonisedSubstratePart.Text.Equals(String.Empty))
                    this._iu.ColonisedSubstratePart = this.textBoxColonisedSubstratePart.Text;
                else
                    this._iu.ColonisedSubstratePart = null;

                if (!this.textBoxLifeStage.Text.Equals(String.Empty))
                    this._iu.LifeStage = this.textBoxLifeStage.Text;
                else
                    this._iu.LifeStage = null;

                if (!this.textBoxGender.Text.Equals(String.Empty))
                    this._iu.Gender = this.textBoxGender.Text;
                else
                    this._iu.Gender = null;

                if (!this.textBoxNumberOfUnits.Text.Equals(String.Empty))
                {
                    try
                    {
                        this._iu.NumberOfUnits = short.Parse(this.textBoxNumberOfUnits.Text);
                    }
                    catch (Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        this._iu.NumberOfUnits = null;
                    }
                }
                else
                    this._iu.NumberOfUnits = null;

                if (!this.comboBoxCircumstances.Text.Equals(String.Empty))
                    this._iu.Circumstances = this.comboBoxCircumstances.Text;
                else
                    this._iu.Circumstances = null;

                if (!this.textBoxDisplayOrder.Text.Equals(String.Empty))
                {
                    try
                    {
                        this._iu.DisplayOrder = short.Parse(this.textBoxDisplayOrder.Text);
                    }
                    catch (Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        this._iu.DisplayOrder = null;
                    }
                }

                try
                {
                    DataFunctions.Instance.Update(_iu);
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Identification Unit Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            Cursor.Current = Cursors.Default;
            return true;
        }
       
        private void comboBoxRelationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._iu != null)
            {
                if (this._iu.RelatedUnitID != null)
                {
                    this._iu.RelationType = this.comboBoxRelationType.Text;
                }
            }
        }

        private void comboBoxIUUnitDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.comboBoxIUUnitDescription.Text.Length > 49 && !e.KeyChar.Equals('\r') && !e.KeyChar.Equals('\b'))
            {
                e.Handled = true;
            }
        }

        private void buttonNotes_Click(object sender, EventArgs e)
        {
            TextInputDialog tid = new TextInputDialog("Notes", this._iu.Notes);
            // Dialog zentrieren
            tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

            if (tid.ShowDialog() == DialogResult.OK)
            {
                if(this._iu != null)
                    this._iu.Notes = tid.Value;
            }
        }

        private void IdentificationUnitForm_Closing(object sender, CancelEventArgs e)
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

        public void Dispose()
        {
            ContextManager.Instance.UnRegisterAll(this);
        }

        private void textBoxNumberOfUnits_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNumberOfUnits.Text.Length > 0 && !textBoxNumberOfUnits.Text.Equals(String.Empty))
            {
                try
                {
                    int.Parse(textBoxNumberOfUnits.Text);
                }
                catch (Exception)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Number of Units has to be a numeric value");
                    this.textBoxNumberOfUnits.Text = "";
                }
            }
        }

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
                    IList<string> Ilist = DataFunctions.Instance.RetrieveTaxonCache(input, this.labelTaxonomicGroup.Text);//Auswahl der TaxonomischenGruppe gewährleisten
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
                }
                catch (ConnectionCorruptedException ex)
                {
                    this.comboBoxIdentification.Items.Clear();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.saveIUData())
                this.DialogResult = DialogResult.Abort;

            ContextManager.Instance.UnRegisterAll(this);
        }
    }
}