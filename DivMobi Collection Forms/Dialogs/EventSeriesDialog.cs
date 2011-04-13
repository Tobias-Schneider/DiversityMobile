using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DataManagement;

using UBT.AI4.Bio.DiversityCollection.Ressource.GPS;

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class EventSeriesDialog : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        private CollectionEventSeries _eventSeries;
        private bool _isNewSeries = true;

        // Default Constructor for creating instance without initialization of components
        public EventSeriesDialog():base() 
        {
            this.formName = "Event Series Dialog";
            this.formDescription = "Create new or edit existing Event Series";
        }

        public EventSeriesDialog(bool loadContext)
            : this()
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
            Cursor.Current = Cursors.WaitCursor;
            
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
            
            Cursor.Current = Cursors.Default;
        }

        public EventSeriesDialog(CollectionEventSeries ceSeries): this(true)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.buttonOk.Enabled = false;

            if (ceSeries == null)
            {
                try
                {
                    this._eventSeries = EventSeriess.Instance.CreateNewEventSeries();
                    this._eventSeries.DateStart = DateTime.Now;
                }
                catch (ConnectionCorruptedException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("New Collection Event Series couldn't be created. (" + ex.Message + ")", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (DataFunctionsException ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
            }
            else
            {
                this._isNewSeries = false;
                this._eventSeries = ceSeries;
                this.buttonFinish.Enabled = true;
            }
            this.fillData();
            Cursor.Current = Cursors.Default;
        }

        //Implementierung von ILayouted
        public ILayout Layout { get { return this.layout; } }

        public String FormName { get { return this.formName != null ? this.formName : ""; } }

        public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


        private void fillData()
        {
            if (this._eventSeries != null)
            {
                this.textBoxDescription.Text = this._eventSeries.Description;
                
                if (this._eventSeries.DateStart != null)
                {
                    this.labelDate.Text = this._eventSeries.DateStart.ToString();
                }

                if (this._eventSeries.DateEnd != null)
                {
                    this.labelDateEndValue.Text = this._eventSeries.DateEnd.ToString();
                }

                if (this._eventSeries.SeriesCode != null && !this._eventSeries.SeriesCode.Equals(String.Empty))
                {
                    this.textBoxSeriesCode.Text = this._eventSeries.SeriesCode;
                }
            } 
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.saveEventSeriesData())
                this.DialogResult = DialogResult.Abort;

            ContextManager.Instance.UnRegisterAll(this);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (this._isNewSeries)
            {
                try
                {
                    EventSeriess.Instance.Remove(this._eventSeries);
                }
                catch (DataFunctionsException ex)
                {
                    MessageBox.Show("Creation of new Collection Event Series couldn't be cancelled. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.DialogResult = DialogResult.Abort;
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show("Creation of new Collection Event Series couldn't be cancelled. (" + ex.Message + ")", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.DialogResult = DialogResult.Abort;
                }
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxDescription.Text.Equals(String.Empty))
                this.buttonOk.Enabled = false;
            else
                this.buttonOk.Enabled = true;
        }

        public int? SeriesID
        {
            get
            {
                if (this._eventSeries != null)
                    return this._eventSeries.SeriesID;
                else
                    return -1;
            }
        }

        public CollectionEventSeries EventSeries
        {
            get
            {
                return this._eventSeries;
            }
        }

        private void textBoxDescription_GotFocus(object sender, EventArgs e)
        {
            if (textBoxDescription.Text.Equals("<TBD>"))
            {
                textBoxDescription.Text = "";
            }
        }

        public void Dispose()
        {
            ContextManager.Instance.UnRegisterAll(this);
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            if(this._eventSeries != null)
                this._eventSeries.DateEnd = DateTime.Now;
        }

        private void EventSeriesDialog_Closing(object sender, CancelEventArgs e)
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

        private bool saveEventSeriesData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_eventSeries != null)
            {
                // Save new EventSeries
                if (!this.textBoxDescription.Text.Equals(String.Empty))
                    this._eventSeries.Description = this.textBoxDescription.Text;
                else
                    this._eventSeries.Description = null;

                if (!this.textBoxSeriesCode.Text.Equals(String.Empty))
                    this._eventSeries.SeriesCode = this.textBoxSeriesCode.Text;
                else
                    this._eventSeries.SeriesCode = null;

                try
                {
                    DataFunctions.Instance.Update(this._eventSeries);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Event Series Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.DialogResult = DialogResult.Abort;
                }
            }
            Cursor.Current = Cursors.Default;
            return true;
        }
    }
}

