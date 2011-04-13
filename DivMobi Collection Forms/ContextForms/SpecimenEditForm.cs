using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UBT.AI4.Toolbox;
using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;


namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    //1. ILayouted implementieren ...
    //2. Mit Layout-Attribut auszeichnen
    //2.1 Typ der Layoutfactory als Parameter (im Moment nur BoxLayoutFactory)
    [Layout(typeof(BoxLayoutFactory))]
    public partial class SpecimenEditForm : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        private CollectionSpecimen _specimen = null;

        // Default Constructor for creating instance without initialization of components
        public SpecimenEditForm():base() 
        {
            this.formName = "Specimen Edit Form";
            this.formDescription = "Edit existing Collection Specimen";
        }

        public SpecimenEditForm(bool loadContext)
            : this()
        {
            // initialization related to windows forms
            InitializeComponent();
            base.adjustControlSizes();
#if DEBUG
            this.MinimizeBox = false;
            this.ControlBox = true;
#else
            this.MinimizeBox = true;
            this.ControlBox = true;            
#endif
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
        }

        public SpecimenEditForm(CollectionSpecimen currentSpecimen)
            : this(true)
        {
            if (currentSpecimen != null)
            {
                this._specimen = currentSpecimen;
                //tabPage Specimen füllen und anzeigen
                Cursor.Current = Cursors.WaitCursor;
                this.fillSpecimenData();
                Cursor.Current = Cursors.Default;
            }
            else
                this.Close();
        }

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        private void fillSpecimenData()
        {
            if (this._specimen != null)
            {
                if (this._specimen.AccessionNumber != null)
                    this.textBoxAccessionNumber.Text = this._specimen.AccessionNumber;

                if (this._specimen.DepositorsName != null)
                    this.textBoxDepositorsName.Text = this._specimen.DepositorsName;

                if (this._specimen.ExsiccataAbbreviation != null)
                    this.textBoxExsicatta.Text = this._specimen.ExsiccataAbbreviation;

                if (this._specimen.LabelTitle != null)
                    this.textBoxLabelTitle.Text = this._specimen.LabelTitle;

                if (this._specimen.LabelType != null)
                    this.comboBoxLabelType.Text = this._specimen.LabelType;
            }
        }

        private void buttonOriginalNotes_Click(object sender, EventArgs e)
        {
            if (this._specimen != null)
            {
                TextInputDialog tid = new TextInputDialog(buttonOriginalNotes.Text, this._specimen.OriginalNotes);

                // Dialog zentrieren
                tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

                if (tid.ShowDialog() == DialogResult.OK)
                {
                    this._specimen.OriginalNotes = tid.Value;
                }
            }
        }

        private void buttonAdditionalNotes_Click(object sender, EventArgs e)
        {
            if (this._specimen != null)
            {
                TextInputDialog tid = new TextInputDialog(buttonAdditionalNotes.Text, this._specimen.AdditionalNotes);

                // Dialog zentrieren
                tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

                if (tid.ShowDialog() == DialogResult.OK)
                {
                    this._specimen.AdditionalNotes = tid.Value;
                }
            }
        }

        private void buttonWithholdingReason_Click(object sender, EventArgs e)
        {
            if (this._specimen != null)
            {
                TextInputDialog tid = new TextInputDialog(buttonDataWithholdingReason.Text, this._specimen.DataWithholdingReason);

                // Dialog zentrieren
                tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

                if (tid.ShowDialog() == DialogResult.OK)
                {
                    this._specimen.DataWithholdingReason = tid.Value;
                }
            }
        }

        private bool saveSpecimenData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_specimen != null)
            {
                if (!this.textBoxAccessionNumber.Text.Equals(String.Empty))
                    _specimen.AccessionNumber = this.textBoxAccessionNumber.Text;
                else
                    _specimen.AccessionNumber = null;

                if (!this.textBoxDepositorsName.Text.Equals(String.Empty))
                    _specimen.DepositorsName = this.textBoxDepositorsName.Text;
                else
                    _specimen.DepositorsName = null;

                if (!this.textBoxExsicatta.Text.Equals(String.Empty))
                    _specimen.ExsiccataAbbreviation = this.textBoxExsicatta.Text;
                else
                    _specimen.ExsiccataAbbreviation = null;

                if (!this.textBoxLabelTitle.Text.Equals(String.Empty))
                    _specimen.LabelTitle = this.textBoxLabelTitle.Text;
                else
                    _specimen.LabelTitle = null;

                if (!this.comboBoxLabelType.Text.Equals(String.Empty))
                    _specimen.LabelType = this.comboBoxLabelType.Text;
                else
                    _specimen.LabelType = null;

                try
                {
                    DataFunctions.Instance.Update(_specimen);
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Specimen Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            Cursor.Current = Cursors.Default;
            return true;
        }

        private void CollectionSpecimenForm_Closing(object sender, CancelEventArgs e)
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

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.saveSpecimenData())
                this.DialogResult = DialogResult.Abort;

            ContextManager.Instance.UnRegisterAll(this);
        }
    }
}