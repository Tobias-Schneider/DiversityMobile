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

using UBT.AI4.Bio.DiversityCollection.Ressource.GPS;

//UMF Imports
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    [Layout(typeof(BoxLayoutFactory))]
    public partial class NewGPSLocalisationForm : DialogBase, ILayouted
    {
        private ILayout layout;
        private String formName;
        private String formDescription;

        public CollectionEventLocalisation locAlt = null;
        public CollectionEventLocalisation locGPS = null;

        private bool _gpsChanged;
        private bool _start;

        // Default Constructor for creating instance without initialization of components
        public NewGPSLocalisationForm():base() 
        {
            this.formName = "New GPS Localisation Form";
            this.formDescription = "Create new GPS Localisation";
        }

        public NewGPSLocalisationForm(bool loadContext)
            : this()
        {
            this._start = true;
            InitializeComponent();
            base.adjustControlSizes();
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

            this._start = false;
        }

         public NewGPSLocalisationForm(CollectionEventLocalisation alt, CollectionEventLocalisation gps)
            : this(true)
        {
            this._start = true;
            Cursor.Current = Cursors.WaitCursor;
            if (alt != null)
            {
                this.locAlt = alt;
            }
            if (gps != null)
            {
                this.locGPS = gps;
            }
            // fill Form
            if (locGPS != null)
            {
                this.textBoxLongitude.Text = locGPS.Location1;
                this.textBoxLatitude.Text = locGPS.Location2;
            }
            if (locAlt != null)
                this.textBoxAltitude.Text = locAlt.Location1;

            Cursor.Current = Cursors.Default;
            this._start = false;
        }

         //Implementierung von ILayouted
         public ILayout Layout { get { return this.layout; } }

         public String FormName { get { return this.formName != null ? this.formName : ""; } }

         public String FormDescription { get { return this.formDescription != null ? this.formDescription : ""; } }


         private float checkValue(string value)
         {
             if (value == null || value.Equals(String.Empty))
                return -20000;
             
             float f;
             try
             {
                 f = float.Parse(value,System.Globalization.CultureInfo.CurrentUICulture);
             }
             catch 
             {
                 Cursor.Current = Cursors.Default;
                 return -10000; //ungültiges Format
             }
             return f;
         }
        
        //Prüfung ob der String auch eine gültige Float darstellt. Genaues Eingabeformat wird nicht geprüft.
         private void textBoxLongitude_LostFocus(object sender, EventArgs e)
         {
             float f = checkValue(textBoxLongitude.Text);
             if (f == -10000 || f==-20000) 
             {
                 if (f == -10000)//Formatfehler
                 {
                     MessageBox.Show("Ungültige Eingabe");
                     textBoxLongitude.Text = "0";
                 }
             }
             else
             {
                 if (locGPS != null)
                 {
                     locGPS.Location1 = f.ToString();
                     locGPS.AverageLongitudeCache = f;
                 }
             }

         }

         private void textBoxLatitude_LostFocus(object sender, EventArgs e)
         {
             float f = checkValue(textBoxLatitude.Text);
             if (f == -10000||f==-20000) 
             {
                 if (f == -10000)//Formatfehler
                 {
                     MessageBox.Show("Ungültige Eingabe");
                     textBoxLatitude.Text = "0";
                 }
             }
             else
             {
                 if (locGPS != null)
                 {
                     locGPS.Location2 = f.ToString();
                     locGPS.AverageLatitudeCache = f;
                 }
             }

         }

         private void textBoxAltitude_LostFocus(object sender, EventArgs e)
         {
             float f = checkValue(textBoxAltitude.Text);
             if (f == -10000||f==-20000)
             {
                 if (f == -10000)//Formatfehler
                 {
                     MessageBox.Show("Ungültige Eingabe");
                     textBoxAltitude.Text = "0";
                 }
             }
             else
             {
                 if (locAlt != null)
                 {
                     locAlt.Location1 = f.ToString();
                     locAlt.AverageAltitudeCache = f;
                 }
             }
         }

         private void textBoxGPSData_TextChanged(object sender, EventArgs e)
         {
             StringBuilder builder = new StringBuilder();
             if (this._gpsChanged)
             {
                 // Notes: automatically changed per GPS
                 builder.AppendLine("GPS Coordinates automatically changed");

                 builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 builder.AppendLine("Position Dilution: " + StaticGPS.position.PositionDilutionOfPrecision.ToString());
             }
             else if (!this._start)
             {
                 // Notes: manually changed
                 builder.AppendLine("GPS Coordinates manually changed");
                 if(StaticGPS.position!=null)
                    builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 if (StaticGPS.device.DeviceState != GpsServiceState.Off && StaticGPS.position != null)
                    builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 if (StaticGPS.device.DeviceState != GpsServiceState.Off && StaticGPS.position != null)
                    builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 if (StaticGPS.device.DeviceState != GpsServiceState.Off && StaticGPS.position != null)
                    builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 if (StaticGPS.device.DeviceState != GpsServiceState.Off && StaticGPS.position != null)
                    builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 if (StaticGPS.device.DeviceState != GpsServiceState.Off && StaticGPS.position != null)
                     builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 if (StaticGPS.device.DeviceState != GpsServiceState.Off && StaticGPS.position != null)
                    builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 if (StaticGPS.device.DeviceState != GpsServiceState.Off && StaticGPS.position != null)
                    builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
             }
             if(locGPS != null)
                 this.locGPS.LocationNotes = builder.ToString();
         }

         private void textBoxAltitude_TextChanged(object sender, EventArgs e)
         {
             StringBuilder builder = new StringBuilder();
             if (this._gpsChanged)
             {
                 // Notes: automatically changed per GPS
                 builder.AppendLine("GPS Coordinates automatically changed");

                 builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
                 builder.AppendLine("Position Dilution: " + StaticGPS.position.PositionDilutionOfPrecision.ToString());
             }
             else if (!this._start)
             {
                 // Notes: manually changed
                 builder.AppendLine("GPS Coordinates manually changed");
                 if (StaticGPS.device.DeviceState != GpsServiceState.Off && StaticGPS.position != null)
                     builder.AppendLine("Number of Satellites: " + StaticGPS.position.SatelliteCount.ToString());
             }
             if(locAlt != null)
                 this.locAlt.LocationNotes = builder.ToString();
         }

         private void buttonGPS_Click(object sender, EventArgs e)
         {
             if (locAlt != null && locGPS != null)
             {
                 if (locAlt.LocalisationSystemID != 4 || locGPS.LocalisationSystemID != 8) //Wenn kein GPS Dann wird gewarnt
                 {
                     MessageBox.Show("Achtung! LocalisationSystem ist nicht WGS84.");
                 }
             }
             this._gpsChanged = true;
             this.UpdateData();
             this._gpsChanged = false;
         }

         private void UpdateData()
         {
             if (StaticGPS.isOpened())
             {
                 if (StaticGPS.position != null)
                 {
                     if (StaticGPS.position.LatitudeValid)
                     {
                         if (locGPS != null)
                         {
                             this.textBoxLatitude.Text = StaticGPS.position.Latitude.ToString();
                             this.locGPS.AverageLatitudeCache = float.Parse(this.textBoxLatitude.Text, System.Globalization.CultureInfo.CurrentUICulture);
                         }
                     }

                     if (StaticGPS.position.LongitudeValid)
                     {
                         if (locGPS != null)
                         {
                             this.textBoxLongitude.Text = StaticGPS.position.Longitude.ToString();
                             this.locGPS.AverageLongitudeCache = float.Parse(this.textBoxLongitude.Text, System.Globalization.CultureInfo.CurrentUICulture);
                         }
                     }

                     if (StaticGPS.position.SeaLevelAltitudeValid)
                     {
                         if (locAlt != null)
                         {
                             this.textBoxAltitude.Text = StaticGPS.position.SeaLevelAltitude.ToString();
                             this.locAlt.AverageAltitudeCache = float.Parse(this.textBoxAltitude.Text, System.Globalization.CultureInfo.CurrentUICulture);
                         }
                     }
                 }
             }
         }

         void UpdatePositionData(object sender, System.EventArgs args)
         {}

         void UpdateDeviceData(object sender, System.EventArgs args)
         {
             if (StaticGPS.device.DeviceState == GpsServiceState.Off)
             {
                 this.buttonGPS.Enabled = false;
             }
             else
             {
                 this.buttonGPS.Enabled = true;
             }
         }

         private void NewGPSLocalisationForm_Load(object sender, EventArgs e)
         {
             StaticGPS.updatePositionDataHandler = new EventHandler(UpdatePositionData);
             StaticGPS.updateDeviceDataHandler = new EventHandler(UpdateDeviceData);

             if (StaticGPS.device.DeviceState == GpsServiceState.Off || StaticGPS.position == null)
             {
                 this.buttonGPS.Enabled = false;
             }
             else
             {
                 this.buttonGPS.Enabled = true;
             }
         }

         private void NewGPSLocalisationForm_Closing(object sender, CancelEventArgs e)
         {
             if (this.DialogResult != DialogResult.OK)
             {
                 if (MessageBox.Show("Data won't be saved. Do You really want to leave the Dialog?", "Data not saved", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                 {
                     e.Cancel = true;
                     return;
                 }
             }
             ContextManager.Instance.UnRegisterAll(this);
         }

         public void Dispose()
         {
             ContextManager.Instance.UnRegisterAll(this);
         }

         private void buttonOk_Click(object sender, EventArgs e)
         {
             if (this.locAlt != null)
             {
                 if (!this.textBoxAltitude.Text.Equals(String.Empty))
                 {
                     this.locAlt.Location1 = this.textBoxAltitude.Text;
                 }
                 else
                 {
                     this.locAlt.Location1 = null;
                 }
             }

             if (this.locGPS != null)
             {

                 if (!this.textBoxLatitude.Text.Equals(String.Empty))
                 {
                     this.locGPS.Location1 = this.textBoxLatitude.Text;
                 }
                 else
                 {
                     this.locGPS.Location1 = null;
                 }

                 if (!this.textBoxLongitude.Text.Equals(String.Empty))
                 {
                     this.locGPS.Location2 = this.textBoxLongitude.Text;
                 }
                 else
                 {
                     this.locGPS.Location2 = null;
                 }
             }
             //StaticGPS.updatePositionDataHandler = null;
             //StaticGPS.updateDeviceDataHandler = null;
         }
    }
}