using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.Forms.Dialogs;


//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class EventPropertyForm : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        public CollectionEventProperty _ceProp = null;

        #region Constructors

        // Default Constructor for creating instance without initialization of components
        public EventPropertyForm() : base()
        {
            this.formName = "Event Property Form";
            this.formDescription = "Edit existing Site Property for Collection Event";
        }

        public EventPropertyForm(bool loadContext)
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

        public EventPropertyForm(CollectionEventProperty ceProp)
            : this(true)
        {
            if (ceProp != null)
            {
                this._ceProp = ceProp;
                Cursor.Current = Cursors.WaitCursor;
                
                Property prop = null;

                try
                {
                    prop = DataFunctions.Instance.RetrieveProperty((int)_ceProp.PropertyID);
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }

                // fill Form
                if (prop != null)
                {
                    if (prop.DisplayText != null)
                        this.labelCaption.Text = prop.DisplayText;
                }
                
                if (_ceProp.AverageValueCache != null)
                    this.labelAverageValue.Text = _ceProp.AverageValueCache.ToString();

                if (_ceProp.DisplayText != null)
                    this.textBoxDisplayText.Text = _ceProp.DisplayText;

                if (_ceProp.PropertyValue != null)
                    this.textBoxValue.Text = _ceProp.PropertyValue;

                if (_ceProp.ResponsibleName != null)
                    this.labelResponsible.Text = _ceProp.ResponsibleName;

                Cursor.Current = Cursors.Default;
            }
            else
                this.Close();
        }

        #endregion

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        private bool savePropertyData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_ceProp != null)
            {
                if (!this.textBoxDisplayText.Text.Equals(String.Empty))
                    _ceProp.DisplayText = this.textBoxDisplayText.Text;
                else
                    _ceProp.DisplayText = null;

                if (!this.textBoxValue.Text.Equals(String.Empty))
                    _ceProp.PropertyValue = this.textBoxValue.Text;
                else
                    _ceProp.PropertyValue = null;
                try
                {
                    DataFunctions.Instance.Update(_ceProp);
                }
                catch(Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Property Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            Cursor.Current = Cursors.Default;
            return true;
        }

        private void EventPropertyForm_Closing(object sender, CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                if (MessageBox.Show("Data won't be saved. Do You really want to leave the Dialog?", "Data not saved", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if(this.DialogResult == DialogResult.Abort)
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
            if (!this.savePropertyData())
                this.DialogResult = DialogResult.Abort;

            ContextManager.Instance.UnRegisterAll(this);
        }
    }
}