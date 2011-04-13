using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Globalization;
using UBT.AI4.Bio.DivMobi.DataManagement;


namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    public partial class SelectMapForm : DialogBase
    {
        private double _savedGpsLat;
        private double _savedGpsLong;
        private double _actualGpsLat;
        private double _actualGpsLong;
        private String _mapPath;
        private double _LatTop;
        private double _LatBottom;
        private double _LongLeft;
        private double _LongRight;
        private bool _showOnly;

        bool storedData = false;

        #region Constructor

        public SelectMapForm(double actualLat, double actualLong):base()
        {
            InitializeComponent();
            base.adjustControlSizes();
            try
            {
                // further initialization
                if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("large"))
                {
                    this.toolBarGoogleMapForm.ImageList = imageListLargeGoogleMaps;
                }
                else if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("medium"))
                {
                    this.toolBarGoogleMapForm.ImageList = imageListMediumGoogleMaps;
                }
                else
                {
                    this.toolBarGoogleMapForm.ImageList = imageListGoogleMaps;
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show("User settings couldn't be loaded. Default images will be used. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.toolBarGoogleMapForm.ImageList = imageListGoogleMaps;
            }

            this.ActualLat = actualLat;
            this.ActualLong = actualLong;
            _mapPath = String.Concat(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), @"\Maps");
            this.GPSLat = 0;
            this.GPSLong = 0;
            this._showOnly = true;
        }

        public SelectMapForm(double gpsLat, double gpsLong, double actualLat, double actualLong ): this(actualLat, actualLong)
        {           
            this.GPSLat = gpsLat;
            this.GPSLong = gpsLong;
            this._showOnly = false;
            storedData = true;
        }

        #endregion

        #region Public Member Data

        public double GPSLat
        {
            set
            {
                this._savedGpsLat = value;
                this.labelStoredLatitude.Text = "Lat: " + value.ToString("F");
            }
            get
            {
                return this._savedGpsLat;
            }
        }

        public double GPSLong
        {
            set
            {
                this._savedGpsLong = value;
                this.labelStoredLongitude.Text = "Long: " + value.ToString("F");
            }
            get
            {
                return this._savedGpsLong;
            }
        }

        public double ActualLat
        {
            set
            {
                this._actualGpsLat = value;
                this.labelActualLatitude.Text = "Lat: " + value.ToString("F");
            }
            get
            {
                return this._actualGpsLat;
            }
        }

        public double ActualLong
        {
            set
            {
                this._actualGpsLong = value;
                this.labelActualLongitude.Text = "Long: " + value.ToString("F");
            }
            get
            {
                return this._actualGpsLong;
            }
        }

        #endregion

        #region Map methods

        private void getSuitableMaps(bool all)
        {
            if (Directory.Exists(_mapPath))
            {
                try
                {
                    this.listViewMaps.Items.Clear();
                    DirectoryInfo dirInfo = new DirectoryInfo(_mapPath);
                    FileInfo[] fileInfo = dirInfo.GetFiles("*.xml");
                    foreach (FileInfo item in fileInfo)
                    {
                        if (item != null)
                            this.readXML(item.FullName, all);
                    }
                }
                catch (GoogleMapsCorruptedException ex)
                {
                    throw ex;
                }
                catch (Exception)
                {
                    throw new GoogleMapsCorruptedException("Map files and settings couldn't be read.");
                }
            }
        }

        private void showMap()
        {
            String name = null;
            foreach (int index in this.listViewMaps.SelectedIndices)
            {
                if (index >= 0)
                {
                    name = this.listViewMaps.Items[index].Text;
                    try
                    {
                        CultureInfo info = null;
                        if (listViewMaps.Items[index].SubItems[1].Text.Contains(","))
                            info = System.Globalization.CultureInfo.CreateSpecificCulture("de-de");//Über LanguageContext regeln
                        else if (listViewMaps.Items[index].SubItems[1].Text.Contains("."))
                            info = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");
                        this._LongLeft = float.Parse(listViewMaps.Items[index].SubItems[1].Text, info);
                        this._LongRight = float.Parse(listViewMaps.Items[index].SubItems[2].Text, info);
                        this._LatBottom = float.Parse(listViewMaps.Items[index].SubItems[4].Text, info);
                        this._LatTop = float.Parse(listViewMaps.Items[index].SubItems[3].Text, info);
                    }
                    catch (Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        throw new GoogleMapsCorruptedException("GPS Data couldn`t be read. Map can`t be displayed!");
                    }
                }
            }

            if (!this.StoredLocalisationIsOnMap()&&!this.ActualLocalisationIsOnMap())
            {
                if (MessageBox.Show("Localisation isn`t on selected map. Do you want to display the map nonetheless?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    this._LongLeft = 0;
                    this._LongRight = 0;
                    this._LatBottom = 0;
                    this._LatTop = 0;
                    return;
                }
            }
            if (name != null)
            {
                this.panelData.Visible = false;
                String fileName = this._mapPath + @"\" + name + ".png";
                ShowMapForm mapForm= new ShowMapForm(this._savedGpsLat, this._savedGpsLong,this._actualGpsLat,this._actualGpsLong, this._LatTop, this._LatBottom, this._LongLeft, this._LongRight, fileName, this._showOnly);
                if (mapForm.ShowDialog() == DialogResult.OK)
                {
                    this.GPSLat = mapForm.SavedLat;
                    this.GPSLong = mapForm.SavedLong;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    this.DialogResult = DialogResult.Cancel;
            }
        }

        private void readXML(String path, bool all)
        {
            try
            {
                XmlTextReader reader;
                reader = new XmlTextReader(path);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                bool isOnActual=true;
                bool isOnStored=true;
                ListViewItem listItem = null;

                while (reader.Read())
                {
                    if (reader.LocalName.Equals("Name"))
                    {
                        listItem = new ListViewItem(reader.ReadElementContentAsString());

                        for (int index = 0; index < 4; index++)
                        {
                            listItem.SubItems.Add("");
                        }
                    }
                    if (reader.LocalName.Equals("SWLat"))
                    {
                        listItem.SubItems[4].Text = reader.ReadElementContentAsString();
                        if (!this.StoredLocalisationIsOnMap(listItem.SubItems[4].Text, 4))
                        {
                            isOnStored = false;
                        }
                        if (!this.ActualLocalisationIsOnMap(listItem.SubItems[4].Text, 4))
                        {
                            isOnActual = false;
                        }
                    }
                    if (reader.LocalName.Equals("SWLong"))
                    { 
                        listItem.SubItems[1].Text = reader.ReadElementContentAsString();
                        if (!this.StoredLocalisationIsOnMap(listItem.SubItems[1].Text, 1))
                        {
                            isOnStored = false;
                        }
                        if (!this.ActualLocalisationIsOnMap(listItem.SubItems[1].Text, 1))
                        {
                            isOnActual = false;
                        }                      
                    }
                    if (reader.LocalName.Equals("NELat"))
                    {
                        listItem.SubItems[3].Text = reader.ReadElementContentAsString();
                        if (!this.StoredLocalisationIsOnMap(listItem.SubItems[3].Text, 3))
                        {
                            isOnStored = false;
                        }
                        if (!this.ActualLocalisationIsOnMap(listItem.SubItems[3].Text, 3))
                        {
                            isOnActual = false;
                        }
                    }
                    if (reader.LocalName.Equals("NELong"))
                    {
                        listItem.SubItems[2].Text = reader.ReadElementContentAsString();
                        if (!this.StoredLocalisationIsOnMap(listItem.SubItems[2].Text, 2))
                        {
                            isOnStored = false;
                        }
                        if (!this.ActualLocalisationIsOnMap(listItem.SubItems[2].Text, 2))
                        {
                            isOnActual = false;
                        }
                    } 
                }
                reader.Close();
                if (isOnStored == true || isOnActual == true || all==true)
                {
                    if (listItem != null)
                        this.listViewMaps.Items.Add(listItem);
                }
            }
            catch (Exception)
            {
                throw new GoogleMapsCorruptedException("Setting from XML file couldn't be read.");
            }
        }

        private bool StoredLocalisationIsOnMap()
        {
            if(this._savedGpsLat > this._LatTop || this._savedGpsLat < this._LatBottom)
                return false;

            if (this._savedGpsLong < this._LongLeft || this._savedGpsLong > this._LongRight)
                return false;

            return true;
        }

        private bool ActualLocalisationIsOnMap()
        {
            if (this._actualGpsLat > this._LatTop || this._actualGpsLat < this._LatBottom)
                return false;

            if (this._actualGpsLong < this._LongLeft || this._actualGpsLong > this._LongRight)
                return false;

            return true;
        }

        private bool StoredLocalisationIsOnMap(String text, int index)
        {
            // index means: 1 - Longitude Left, 2 - Longitude Right, 3 - Latitude Top, 4 - Latitude Bottom

            float temp;
            try
            {
                CultureInfo info = null;
                if (text.Contains(","))
                    info = System.Globalization.CultureInfo.CreateSpecificCulture("de-de");
                else if (text.Contains("."))
                    info = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");
                temp = float.Parse(text, info);
            }
            catch (Exception)
            {
                return false;
            }

            switch (index)
            {
                case 1:
                    if (this._savedGpsLong < temp)
                        return false;
                    break;
                case 2:
                    if (this._savedGpsLong > temp)
                        return false;
                    break;
                case 3:
                    if (this._savedGpsLat > temp)
                        return false;
                    break;
                case 4:
                    if (this._savedGpsLat < temp)
                        return false;
                    break;

            }
            return true;
        }

        private bool ActualLocalisationIsOnMap(String text, int index)
        {
            // index means: 1 - Longitude Left, 2 - Longitude Right, 3 - Latitude Top, 4 - Latitude Bottom

            float temp;
            try
            {
                CultureInfo info = null;
                if (text.Contains(","))
                    info = System.Globalization.CultureInfo.CreateSpecificCulture("de-de");
                else if (text.Contains("."))
                    info = System.Globalization.CultureInfo.CreateSpecificCulture("en-us");
                temp = float.Parse(text, info);
            }
            catch (Exception)
            {
                return false;
            }

            switch (index)
            {
                case 1:
                    if (this._actualGpsLong < temp)
                        return false;
                    break;
                case 2:
                    if (this._actualGpsLong > temp)
                        return false;
                    break;
                case 3:
                    if (this._actualGpsLat > temp)
                        return false;
                    break;
                case 4:
                    if (this._actualGpsLat < temp)
                        return false;
                    break;

            }

            return true;
        }

        #endregion

        #region Control Events
        
        private void listViewMaps_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                this.showMap();
            }
            catch (GoogleMapsCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Google Maps Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void checkBoxShowAll_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                this.getSuitableMaps(this.checkBoxShowAll.Checked);
            }
            catch (GoogleMapsCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Google Maps Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.listViewMaps.Items.Clear();
            }
        }

        private void listViewMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.listViewMaps.SelectedIndices.Count > 0)
            {
                toolBarButtonShowMap.Enabled = true;
                toolBarButtonShowMap.ImageIndex = 0;
            }
        }

        private void toolBarGoogleMapForm_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolBarButtonShowMap)
            {
                try
                {
                    this.showMap();
                }
                catch (GoogleMapsCorruptedException ex)
                {
                    MessageBox.Show(ex.Message, "Google Maps Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            else if (e.Button == toolBarButtonCloseMap)
            {
                this.panelData.Visible = true;
                toolBarButtonCloseMap.Enabled = false;
                toolBarButtonCloseMap.ImageIndex = 3;
                this.listViewMaps.Refresh();
            }
        }
        
        private void SelectMapForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.getSuitableMaps(this.checkBoxShowAll.Checked);
            }
            catch (GoogleMapsCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Google Maps Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.listViewMaps.Items.Clear();
            }
            this.labelStoredGPSCaption.Visible = this.storedData;
            this.labelStoredLatitude.Visible = this.storedData;
            this.labelStoredLongitude.Visible = this.storedData;
        }
        
        #endregion    
    }
}