using System;
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
    public partial class CollectionEventForm : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        private CollectionEvent _event = null;
        private IList<CollectionEventSeries> _series = null;

        // Default Constructor for creating instance without initialization of components
        public CollectionEventForm():base() 
        {
            this.formName = "Collection Event Form";
            this.formDescription = "Create new or edit existing Collection Event";
        }

        public CollectionEventForm(bool loadContext)
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

        public CollectionEventForm(CollectionEvent currentEvent)
            : this(true)
        {
            if (currentEvent != null)
            {
                this._event = currentEvent;
                Cursor.Current = Cursors.WaitCursor;
                this.fillEventData();
                Cursor.Current = Cursors.Default;
            }
            else
                this.Close();
        }

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        #region CollectionEvent editing

        private void fillEventData()
        {
            if (this._event == null)
            {
                return;
            }

            // Event Text
            StringBuilder sbEventTitle = new StringBuilder();
            sbEventTitle.Append(_event.CollectionDate.Year);
            sbEventTitle.Append('/');
            sbEventTitle.Append(_event.CollectionDate.Month);
            sbEventTitle.Append('/');
            sbEventTitle.Append(_event.CollectionDate.Day);

            if (_event.CollectorsEventNumber != null && !_event.CollectorsEventNumber.Equals(string.Empty))
            {
                sbEventTitle.Append(": No.");
                sbEventTitle.Append(_event.CollectorsEventNumber);
            }

            if (_event.LocalityDescription != null && !_event.LocalityDescription.Equals(string.Empty))
            {
                sbEventTitle.Append(" (");
                sbEventTitle.Append(_event.LocalityDescription);
                sbEventTitle.Append(")");
            }

            this.labelCEText.Text = sbEventTitle.ToString();

            if (_event.Version != null)
            {
                this.labelVersion.Text = _event.Version.ToString();
            }

            if (_event.CollectorsEventNumber != null)
            {
                this.textBoxCollectorsEventNumber.Text = _event.CollectorsEventNumber;
            }
            
            if (_event.CollectionDate > DateTime.MinValue)
            {
                if (_event.CollectionDate.CompareTo(this.dateTimePickerCollectionEventDate.MinDate) >= 0)
                {
                    try
                    {
                        if (!UserProfiles.Instance.Current.Context.Equals("ManagementMobile"))
                        {
                            this.dateTimePickerCollectionEventDate.Visible = true;
                        }
                    }
                    catch (ConnectionCorruptedException)
                    {
                        this.dateTimePickerCollectionEventDate.Visible = false; ;
                    }

                    this.buttonNewDate.Visible = false;
                    this.dateTimePickerCollectionEventDate.Value = _event.CollectionDate;
                }
            }
            else
            {
                try
                {
                    if (!UserProfiles.Instance.Current.Context.Equals("ManagementMobile"))
                    {
                        this.buttonNewDate.Visible = true;
                    }
                }
                catch (ConnectionCorruptedException)
                {
                    this.buttonNewDate.Visible = false; ;
                }
            }

            if (_event.CollectionDateSupplement != null)
            {
                this.textBoxDateSupplement.Text = _event.CollectionDateSupplement;
            }

            if (_event.CollectionTime != null)
            {
                this.textBoxCollectionTime.Text = _event.CollectionTime;
            }

            if (_event.CollectionTimeSpan != null)
            {
                this.textBoxTimeSpan.Text = _event.CollectionTimeSpan;
            }

            if (_event.ReferenceTitle != null)
            {
                this.textBoxRefTitle.Text = _event.ReferenceTitle;
            }

            if (_event.CountryCache != null)
            {
                this.textBoxCountryCache.Text = _event.CountryCache;
            }
        }

        private bool saveEventData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_event != null)
            {
                if (!this.textBoxCollectorsEventNumber.Text.Equals(String.Empty))
                    _event.CollectorsEventNumber = this.textBoxCollectorsEventNumber.Text;

                if (this.dateTimePickerCollectionEventDate.Visible)
                {
                    _event.CollectionDate = this.dateTimePickerCollectionEventDate.Value;

                    _event.CollectionDay = (byte?)_event.CollectionDate.Day;
                    _event.CollectionMonth = (byte?)_event.CollectionDate.Month;
                    _event.CollectionYear = (short?)_event.CollectionDate.Year;
                }

                if (!this.textBoxDateSupplement.Text.Equals(String.Empty))
                    _event.CollectionDateSupplement = this.textBoxDateSupplement.Text;

                if (!this.textBoxCollectionTime.Text.Equals(String.Empty))
                    _event.CollectionTime = this.textBoxCollectionTime.Text;

                if (!this.textBoxTimeSpan.Text.Equals(String.Empty))
                    _event.CollectionTimeSpan = this.textBoxTimeSpan.Text;

                if (!this.textBoxRefTitle.Text.Equals(String.Empty))
                    _event.ReferenceTitle = this.textBoxRefTitle.Text;

                if (!this.textBoxCountryCache.Text.Equals(String.Empty))
                    _event.CountryCache = this.textBoxCountryCache.Text;

                try
                {
                    DataFunctions.Instance.Update(_event);
                }
                catch(Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Collection Event Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            Cursor.Current = Cursors.Default;
            return true;
        }

        private void buttonLocality_Click(object sender, EventArgs e)
        {
            if (this._event != null)
            {
                TextInputDialog tid = new TextInputDialog("Description of Event Locality", this._event.LocalityDescription);
                // Dialog zentrieren
                tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

                if (tid.ShowDialog() == DialogResult.OK)
                {
                    this._event.LocalityDescription = tid.Value;
                }
            }
        }

        private void buttonHabitat_Click(object sender, EventArgs e)
        {
            if (this._event != null)
            {
                TextInputDialog tid = new TextInputDialog("Habitat Description", this._event.HabitatDescription);
                // Dialog zentrieren
                tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

                if (tid.ShowDialog() == DialogResult.OK)
                {
                    this._event.HabitatDescription = tid.Value;
                }
            }
        }

        private void buttonMethod_Click(object sender, EventArgs e)
        {
            if (this._event != null)
            {
                TextInputDialog tid = new TextInputDialog("Collecting Method", this._event.CollectingMethod);
                // Dialog zentrieren
                tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

                if (tid.ShowDialog() == DialogResult.OK)
                {
                    this._event.CollectingMethod = tid.Value;
                }
            }
        }

        private void buttonNotes_Click(object sender, EventArgs e)
        {
            if (this._event != null)
            {
                TextInputDialog tid = new TextInputDialog("Notes", this._event.Notes);
                // Dialog zentrieren
                tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

                if (tid.ShowDialog() == DialogResult.OK)
                {
                    this._event.Notes = tid.Value;
                }
            }
        }

        private void buttonNewDate_Click(object sender, EventArgs e)
        {
            this.buttonNewDate.Visible = false;
            this.dateTimePickerCollectionEventDate.Value = DateTime.Now;
            this.dateTimePickerCollectionEventDate.Visible = true;
        }

        #endregion

        private void CollectionEventForm_Closing(object sender, CancelEventArgs e)
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
            if (!this.saveEventData())
                this.DialogResult = DialogResult.Abort;

            ContextManager.Instance.UnRegisterAll(this);
        }
   }
}