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
using UBT.AI4.Bio.DiversityCollection.Ressource.GPS;
using OpenNETCF.Drawing.Imaging;


namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    public partial class ShowMapForm : Form
    {
        private bool _editMode;
        private bool _showOnly;
        private bool _setPoint;
        private double _actualLat;
        private double _actualLon;
        private int _actualLatPos;
        private int _actualLonPos;
        private double _savedLat;
        private double _savedLong;
        private double _LatTop;
        private double _LatBottom;
        private double _LongLeft;
        private double _LongRight;
        private String _fileName;


        #region Constructors

        public ShowMapForm()
        {
            InitializeComponent();
        }

        public ShowMapForm(double lat,double lon,double actuallat,double actuallon,double latop,double labot,double loleft,double loright,String fileName, bool showOnly)
            : this()
        {
            this._actualLat=actuallat;           
            this._actualLon = actuallon;           
            this._savedLat = lat;
            this._savedLong = lon;
            this._LatTop = latop;
            this._LatBottom = labot;
            this._LongLeft = loleft;
            this._LongRight = loright;
            this._fileName = fileName;
            this._editMode = false;
            this._setPoint = true;
            this.loadImage(_fileName, 1200);
            this.pictureBoxMap.Visible = true;
            this._actualLatPos = this.calculateTop(this._actualLat);
            this._actualLonPos = this.calculateLeft(this._actualLon);
            this._showOnly = showOnly;
            if (this._showOnly == false)
            {
                try
                {
                    if (UserProfiles.Instance.Current.ToolbarIcons.Equals("medium") || UserProfiles.Instance.Current.ToolbarIcons.Equals("large"))
                        this.toolBar1.ImageList = this.imageListMedium;
                    else
                        this.toolBar1.ImageList = this.imageListSmall;
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show("User settings couldn't be loaded. Default images will be used. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.toolBar1.ImageList = this.imageListSmall;
                }
            }
            else
                this.toolBar1.Buttons.Clear();
            
            //StaticGPS.updatePositionDataHandler = new EventHandler(UpdatePositionData);
            //StaticGPS.updateDeviceDataHandler = new EventHandler(UpdateDeviceData);
        }

        #endregion


        private void loadImage(String path, int maxRes)
        {
         try
            {
                if (pictureBoxMap.Image != null)
                {
                    pictureBoxMap.Image.Dispose();
                    pictureBoxMap.Image = null;
                }
                ImagingFactory factory = new ImagingFactoryClass();
                IImage img;
                ImageInfo inf = new ImageInfo();
                factory.CreateImageFromFile(path, out img);

                img.GetImageInfo(out inf);
                double ratio = (double)inf.Width / (double)inf.Height;
                uint x=0;
                uint y=0;
                if (inf.Width > inf.Height)
                {
                    x = Math.Min(inf.Width, (uint)maxRes);
                    y = (uint)Math.Floor(x / ratio);
                }
                else
                {
                    y = Math.Min(inf.Height, (uint)maxRes);
                    x = (uint)Math.Floor(y * ratio);
                }

                IBitmapImage imgB;
                factory.CreateBitmapFromImage(img,
                    x,
                    y,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                    InterpolationHint.InterpolationHintDefault,
                    out imgB);

                Size s = new Size((int)x, (int)y);
                pictureBoxMap.Size = s;
                pictureBoxMap.Image = ImageUtils.IBitmapImageToBitmap(imgB);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Out of memory");
            }
        }


        private void setPoint(int Left, int Top, bool create)
        {
            this._setPoint = false;
            pictureBoxMap.Refresh();
            if (Left > 0 && Top > 0)
            {
                String _progPath1;
                try
                {
                    if (UserProfiles.Instance.Current.ToolbarIcons.Equals("medium") || UserProfiles.Instance.Current.ToolbarIcons.Equals("large"))
                    {
                        _progPath1 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\CoordinateCrossTransparent32.ico";
                    }
                    else
                    {
                        _progPath1 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\CoordinateCrossTransparent16.ico";
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show("User settings couldn't be loaded. Default file will be used. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    _progPath1 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\CoordinateCrossTransparent16.ico";
                }

                try
                {
                    FileStream fs1 = new FileStream(_progPath1, FileMode.Open);
                    Icon img1 = new Icon(fs1);
                    int middleX = (img1.Width + 1) / 2;
                    int middleY = (img1.Height + 1) / 2;
                    this.pictureBoxMap.CreateGraphics().DrawIcon(img1, Left - middleX, Top - middleY);
                    if (create)
                    {
                        this.SavedLat = this.calculateLat(Top);
                        this.SavedLong = this.calculateLong(Left);
                    }
                    fs1.Close();
                }
                catch (Exception ex)
                {
                    this._setPoint = false;
                    throw new GoogleMapsCorruptedException("Point couldn't be set. ("+ex.Message+")");
                }
            }
            if(this._actualLonPos>0&&this._actualLatPos>0)
            {
                String _progPath2;
                try
                {
                    if (UserProfiles.Instance.Current.ToolbarIcons.Equals("medium") || UserProfiles.Instance.Current.ToolbarIcons.Equals("large"))
                    {
                        _progPath2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\GpsPoint32.ico";
                    }
                    else
                    {
                        _progPath2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\GpsPoint16.ico";
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    MessageBox.Show("User settings couldn't be loaded. Default file will be used. (" + ex.Message + ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    _progPath2 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\GpsPoint16.ico";
                }

                try
                {
                    FileStream fs2 = new FileStream(_progPath2, FileMode.Open);
                    Icon img2 = new Icon(fs2);
                    int middleX = (img2.Width + 1) / 2;
                    int middleY = (img2.Height + 1) / 2;
                    this.pictureBoxMap.CreateGraphics().DrawIcon(img2, this._actualLonPos - middleX, this._actualLatPos - middleY);
                    fs2.Close();
                }
                catch (Exception ex)
                {
                    this._setPoint = false;
                    throw new GoogleMapsCorruptedException("Point couldn't be set. (" + ex.Message + ")");
                }
            }
            this._setPoint = true;
        }

        private delegate void actualizePositionDelegate();

        private void actualizePosition()
        {
            if (StaticGPS.isOpened())
            {
                if (StaticGPS.position != null)
                {
                    try
                    {
                        float latitude = float.Parse(StaticGPS.position.Latitude.ToString());
                        float longitude = float.Parse(StaticGPS.position.Longitude.ToString());
                        float altitude = float.Parse(StaticGPS.position.SeaLevelAltitude.ToString());
                        this._actualLat = latitude;
                        this._actualLon = longitude;
                        this._actualLatPos = this.calculateTop(this._actualLat);
                        this._actualLonPos = this.calculateLeft(this._actualLon);
                    }
                    catch (Exception)
                    {
                        Cursor.Current = Cursors.Default;
                        throw new DataFunctionsException("GPS-Data couldn`t be read. Data will be set to default values.");
                    }
                }
            }
        }

        private void showActualizedPosition()
        {

            if (this.InvokeRequired)
            {
                actualizePositionDelegate geoDelegate = new actualizePositionDelegate(actualizePosition);
                this.Invoke(geoDelegate);
            }
            else
            {
                this.actualizePosition();
            }
            this.setPoint(this._actualLonPos, this._actualLatPos, false);
            if (this._actualLatPos >= 0 && this._actualLonPos >= 0)
                MessageBox.Show("You are on the map");

        }

        #region Calculations

        private int calculateLeft(double lon)
        {
            double longDiffScreen = this._LongRight - this._LongLeft;
            double longDiffPoint = lon - this._LongLeft;
            double ratio = this.pictureBoxMap.Size.Width / longDiffScreen;
            return (int) Math.Round(longDiffPoint * ratio);
        }

        private int calculateTop(double lat)
        {
            double latDiffScreen = this._LatTop - this._LatBottom;
            double latDiffPoint = this._LatTop - lat;
            double ratio = this.pictureBoxMap.Size.Height / latDiffScreen;
            return (int) Math.Round(latDiffPoint * ratio);
        }

        private double calculateLong(int left)
        {
            double longDiffScreen = this._LongRight - this._LongLeft;
            double ratio = longDiffScreen / this.pictureBoxMap.Size.Width;
            return (this._LongLeft + (left * ratio));
        }

        private double calculateLat(int top)
        {
            double latDiffScreen = this._LatTop - this._LatBottom;
            double ratio = latDiffScreen / this.pictureBoxMap.Size.Height;
            return (this._LatTop - (top * ratio));
        }
        private bool StoredLocalisationIsOnMap()
        {
            if (this._savedLat > this._LatTop || this._savedLat < this._LatBottom)
                return false;

            if (this._savedLong < this._LongLeft || this._savedLong > this._LongRight)
                return false;

            return true;
        }

        private bool ActualLocalisationIsOnMap()
        {
            if (this._actualLat > this._LatTop || this._actualLat < this._LatBottom)
                return false;

            if (this._actualLon < this._LongLeft || this._actualLon > this._LongRight)
                return false;
            return true;
        }
        #endregion

        #region Properties
        public double SavedLat
        {
            set
            {
                this._savedLat = value;
            }
            get
            {
                return this._savedLat;
            }
        }

        public double SavedLong
        {
            set
            {
                this._savedLong = value;
            }
            get
            {
                return this._savedLong;
            }
        }

        //private void calculateClick(ref int x, ref int y)
        //{
        //    int adjustX = this.pictureBoxMap.Location.X;
        //    int adjusty = this.pictureBoxMap.Location.Y;
        //    Point p=new Point(x,y);
        //    Point q=this.PointToClient(p);
        //    Rectangle rec= this.Bounds;
        //    Rectangle rec2= this.ClientRectangle;
        //    Size s = this.ClientSize;
        //    ControlCollection c = this.Controls;
        //    SizeF f = this.CurrentAutoScaleDimensions;
        //    int h=this.Height;
        //    Point loc = this.Location;
        //    Form fo= this.Owner;
        //    Control fol = this.pictureBoxMap.Parent;
        //    Control cl = this.Parent;
        //    int r = this.Right;
        //    int l = this.Left;
        //    int w = this.Width;
        //    ISite ist=this.Site;
        //    Size siz = this.Size;
        //    Control hj = this.TopLevelControl;
        //    FormWindowState wd = this.WindowState;

        //    x = x - adjustX;
        //    y = y - adjusty;
        //}

        #endregion

        private void pictureBoxMap_Click(object sender, EventArgs e)
        {
            if (this._showOnly == true)
                return;
            if (!this._editMode)
                return;
            int x = Control.MousePosition.X;
            int y = Control.MousePosition.Y;
            Point p = new Point(x, y);
            p = this.PointToClient(p);
            try
            {
                this.setPoint(p.X, p.Y, true);
            }
            catch (GoogleMapsCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Google Maps Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        //void UpdatePositionData(object sender, System.EventArgs args)
        //{
        //    if (StaticGPS.device == null || StaticGPS.device.DeviceState == GpsServiceState.Off)
        //    {
        //        this.setPoint(0, 0, false);
        //        return;
        //    }

        //    if (StaticGPS.position == null)
        //    {
        //        this.setPoint(0, 0, false);
        //        return;
        //    }


        //    if (StaticGPS.position.SatelliteCount <= 2)
        //    {
        //        this.setPoint(0, 0, false);
        //    }
        //    else
        //    {
        //        this.setPoint(0, 0, false); ;
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        //void UpdateDeviceData(object sender, System.EventArgs args)
        //{

        //}

        private void ShowMapForm_Closing(object sender, CancelEventArgs e)
        {
            if (this._editMode)
            {
                MessageBox.Show("Leave EditMode before closing");
                e.Cancel=true;
                return;
            }
            if (this._showOnly == false)
            {
                if (MessageBox.Show("Do You want to save the location?", "Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                    this.DialogResult = DialogResult.OK;
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (this._showOnly == true)
                return;
            this._editMode = !this._editMode;
            if (this._editMode)
            {
                this.Text = "EditMode";
            }
            else
            {
                this.Text = "ShowMode";
            }
        }

        private void ShowMapForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.setPoint(calculateLeft(this._savedLong), calculateTop(this._savedLat), false);
            }
            catch (GoogleMapsCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Google Maps Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void ShowMapForm_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                this.setPoint(calculateLeft(this._savedLong), calculateTop(this._savedLat), false);
            }
            catch (GoogleMapsCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Google Maps Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void pictureBoxMap_Paint(object sender, PaintEventArgs e)
        {
            if (this._setPoint == false)
                return;

            try
            {
                this.setPoint(calculateLeft(this._savedLong), calculateTop(this._savedLat), false);
            }
            catch (GoogleMapsCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Google Maps Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

    }
}