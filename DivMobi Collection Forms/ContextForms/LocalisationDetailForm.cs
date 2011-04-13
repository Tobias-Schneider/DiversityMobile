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
using UBT.AI4.Bio.DivMobi.Forms.Dialogs;

using UBT.AI4.Bio.DivMobi.Forms.Forms;

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;
using UBT.AI4.Bio.DiversityCollection.Ressource.GPS;


namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class LocalisationDetailForm : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        public CollectionEventLocalisation _ceLoc = null;
        private bool LocationChanged = false;
        
        #region Constructors

        // Default Constructor for creating instance without initialization of components
        public LocalisationDetailForm() :base()
        {
            this.formName = "Localisation Detail Form";
            this.formDescription = "Show and edit Localisation Details";
        }

        public LocalisationDetailForm(bool loadContext)
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

        public LocalisationDetailForm(CollectionEventLocalisation ceLoc)
            : this(true)
        {
            if (ceLoc != null)
            {
                this._ceLoc = ceLoc;
                Cursor.Current = Cursors.WaitCursor;

                LocalisationSystem locSystem = null;

                try
                {

                    locSystem = DataFunctions.Instance.RetrieveLocalisationSystem((int)_ceLoc.LocalisationSystemID);
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }

                // fill Form
                if (locSystem != null)
                    this.labelCaption.Text = locSystem.LocalisationSystemName;

                if (_ceLoc.LocationAccuracy != null)
                    this.textBoxLocAccuracy.Text = _ceLoc.LocationAccuracy;

                if (_ceLoc.Location1 != null)
                {
                    this.textBoxLocation1.Text = _ceLoc.Location1;
                }

                if (_ceLoc.Location2 != null)
                {
                    this.textBoxLocation2.Text = _ceLoc.Location2;
                }

                if (_ceLoc.ResponsibleName != null)
                    this.textBoxResponsibleName.Text = _ceLoc.ResponsibleName;

                if (_ceLoc.DeterminationDate > DateTime.MinValue)
                {
                    if (_ceLoc.DeterminationDate.CompareTo(this.dateTimePickerDeterminationDate.MinDate) >= 0)
                    {
                        try
                        {
                            if (!UserProfiles.Instance.Current.Context.Equals("ManagementMobile"))
                            {
                                this.dateTimePickerDeterminationDate.Visible = true;
                            }
                        }
                        catch (ConnectionCorruptedException)
                        {
                            this.dateTimePickerDeterminationDate.Visible = false;
                        }

                        this.buttonNewDate.Visible = false;
                        this.dateTimePickerDeterminationDate.Value = _ceLoc.DeterminationDate;
                    }
                }
                else
                {
                    this.dateTimePickerDeterminationDate.Visible = false;
                    try
                    {
                        if (!UserProfiles.Instance.Current.Context.Equals("ManagementMobile"))
                        {
                            this.buttonNewDate.Visible = true;
                        }
                    }
                    catch (ConnectionCorruptedException)
                    {
                        this.dateTimePickerDeterminationDate.Visible = false;
                    }
                }

                // Altitude Localisation
                if (this._ceLoc.LocalisationSystemID == 4)
                {
                    this.textBoxLocation2.Enabled = false;
                    this.buttonShowMap.Enabled = false;
                }

                if (_ceLoc.AverageLongitudeCache != null)
                {
                    this.labelLongitude.Text = ((double)_ceLoc.AverageLongitudeCache).ToString("F");
                }

                if (_ceLoc.AverageLatitudeCache != null)
                {
                    this.labelLatitude.Text = ((double)_ceLoc.AverageLatitudeCache).ToString("F");
                }

                if (_ceLoc.AverageAltitudeCache != null)
                {
                    this.labelAltitude.Text = ((double)_ceLoc.AverageAltitudeCache).ToString("F");
                }

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


        private bool saveLocalisationProperties()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_ceLoc != null)
            {
                _ceLoc.LocationAccuracy = this.textBoxLocAccuracy.Text;
                _ceLoc.Location1 = this.textBoxLocation1.Text;
                _ceLoc.Location2 = this.textBoxLocation2.Text;

                // Altitude Localisation
                if (this._ceLoc.LocalisationSystemID == 4)
                {
                    try
                    {
                        this._ceLoc.AverageAltitudeCache = float.Parse(this.textBoxLocation1.Text);
                    }
                    catch (Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Parsing Exception. Please check value of Location1");
                        return false;
                    }
                }
                else
                {
                    try
                    {
                        this._ceLoc.AverageLongitudeCache = float.Parse(this.textBoxLocation1.Text);
                        this._ceLoc.AverageLatitudeCache = float.Parse(this.textBoxLocation2.Text);
                    }
                    catch (Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Parsing Exception. Please check value of Location1 or Location2");
                        return false;
                    }
                }

                if (LocationChanged)
                {
                    this._ceLoc.LocationNotes = "GPS Coordinates manually changed while editing Localisation";
                }
                
                if (this.textBoxResponsibleName.Text != String.Empty && this.textBoxResponsibleName.Text != "")
                {
                    _ceLoc.ResponsibleName = this.textBoxResponsibleName.Text;
                }

                if (this.dateTimePickerDeterminationDate.Visible)
                {
                    _ceLoc.DeterminationDate = this.dateTimePickerDeterminationDate.Value;
                }

                try
                {
                    DataFunctions.Instance.Update(_ceLoc);
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Localisation Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            Cursor.Current = Cursors.Default;
            return true;
        }

        private void buttonNewDate_Click(object sender, EventArgs e)
        {
            this.buttonNewDate.Visible = false;
            this.dateTimePickerDeterminationDate.Value = DateTime.Now;
            this.dateTimePickerDeterminationDate.Visible = true;
        }

        private void textBoxLocation_TextChanged(object sender, EventArgs e)
        {
            this.LocationChanged = true;
        }

        private void EventLocalisationForm_Closing(object sender, CancelEventArgs e)
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

        private void buttonShowMap_Click(object sender, EventArgs e)
        {
            float actualLatitude = 0;
            float actualLongitude = 0;
            try
            {
                actualLatitude = float.Parse(StaticGPS.position.Latitude.ToString());
                actualLongitude = float.Parse(StaticGPS.position.Longitude.ToString());
            }
            catch (Exception)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("GPS-Data couldn`t be read. Data will be set to default values.");
            }
            SelectMapForm form = new SelectMapForm((float)this._ceLoc.AverageLatitudeCache, (float)this._ceLoc.AverageLongitudeCache,actualLatitude,actualLongitude);
            form.ShowDialog();

            if (this._ceLoc != null)
            {
                this._ceLoc.AverageLatitudeCache = form.GPSLat;
                this.labelLatitude.Text = form.GPSLat.ToString("F");
                this._ceLoc.AverageLongitudeCache = form.GPSLong;
                this.labelLongitude.Text = form.GPSLong.ToString("F");

                this._ceLoc.Location2 = form.GPSLat.ToString();
                this.textBoxLocation2.Text = this._ceLoc.Location2;
                this._ceLoc.Location1 = form.GPSLong.ToString();
                this.textBoxLocation1.Text = this._ceLoc.Location1;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!this.saveLocalisationProperties())
                this.DialogResult = DialogResult.Abort;
 
            ContextManager.Instance.UnRegisterAll(this);
        }
    }
}