using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace DiversityWorkbench
{
    public partial class UserControlGoogleMaps : UserControl
    {
        #region Parameter
        private System.Uri _URI;
        private float _Latitude = 400f;
        private float _Longitude = 400f;
        private float _LatitudeAccuracy = 0f;
        private float _LongitudeAccuracy = 0f;
        private float _UpperLatitude = 400;
        private float _LowerLatitude = 400;
        private float _LeftLongitude = 400;
        private float _RightLongitude = 400;

        #endregion

        #region Construction
        public UserControlGoogleMaps()
        {
            InitializeComponent();
        }
        
        #endregion

        public void setUrlForPolygon(string URL)
        {
            if (URL.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("No coordinates found");
                return;
            }
            if (URL.Length > 2000)
            {
                System.Windows.Forms.MessageBox.Show("Too many items, please reduce selection");
                return;
            }
            if (URL.Length > 0)
            {
                try
                {
                    URL = System.Web.HttpUtility.UrlEncode(URL);
                    if (!URL.StartsWith("http://"))
                        URL = "http://www.snsb.info/GoogleMaps/DiversityWorkbench_Polygon.cfm?" + URL;
                    URL += "&Height=" + (this.Height).ToString() + "&Width=" + (this.Width).ToString();
                    this._URI = new Uri(URL);
                    this.webBrowser.ScrollBarsEnabled = false;
                    this.webBrowser.Url = this._URI;
                }
                catch (Exception ex)
                {
                    //DiversityWorkbench.ExceptionHandling.WriteToErrorLogFile(ex);
                }
            }
            else
                System.Windows.Forms.MessageBox.Show("Could not find coordinates to generate a map");
        }

        public void setUrlForPolygon(string Coordinates, string CoordinatesParent, string CoordinatesPoints, string LabelForPoints)
        {
            if (Coordinates.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("No coordinates found");
                return;
            }
            if (Coordinates.Length + CoordinatesParent.Length > 2000)
            {
                System.Windows.Forms.MessageBox.Show("Too many items, please reduce selection");
                return;
            }
            string DataInCoordinates = Coordinates.Replace("Lng", "");
            DataInCoordinates = DataInCoordinates.Replace("Lat", "");
            DataInCoordinates = DataInCoordinates.Replace("&", "");
            DataInCoordinates = DataInCoordinates.Replace("=", "");
            if (DataInCoordinates.Length == 0) Coordinates = "";
            else Coordinates = System.Web.HttpUtility.UrlEncode(Coordinates);

            string DataInPoints = CoordinatesPoints.Replace("&LatPoint", "");
            DataInPoints = DataInPoints.Replace("&LngPoint", "");
            DataInPoints = DataInPoints.Replace("=", "");
            if (DataInPoints.Length == 0) CoordinatesPoints = "";
            else CoordinatesPoints = System.Web.HttpUtility.UrlEncode(CoordinatesPoints);

            string DataInParent = CoordinatesParent.Replace("&LatPar", "");
            DataInParent = DataInParent.Replace("%LngPar", "");
            DataInParent = DataInParent.Replace("=", "");
            if (DataInParent.Length == 0) CoordinatesParent = "";
            else CoordinatesParent = System.Web.HttpUtility.UrlEncode(CoordinatesParent);

            string DataInLabel = LabelForPoints.Replace("&Label=", "");
            if (DataInLabel.Length == 0) LabelForPoints = "";
            else LabelForPoints = System.Web.HttpUtility.UrlEncode(LabelForPoints);

            if (Coordinates.Length > 0 || (DataInCoordinates.Length > 0 || DataInPoints.Length > 0))
            {
                try
                {
                    string URL = "http://www.snsb.info/GoogleMaps/DiversityWorkbench_PolygonParent.cfm?" + Coordinates + CoordinatesParent + CoordinatesPoints + LabelForPoints;





                    //bool WithParent = false;
                    //string DataInCoordintesParent = CoordinatesParent.Replace("&LatPar", "");
                    //DataInCoordintesParent = DataInCoordintesParent.Replace("&LngPar", "");
                    //DataInCoordintesParent = DataInCoordintesParent.Replace("=", "");

                    //if (CoordinatesParent.Length > 0 && DataInCoordintesParent.Length > 0) WithParent = true;
                    //URL = System.Web.HttpUtility.UrlEncode(Coordinates);
                    //if (!CoordinatesParent.StartsWith("&")) CoordinatesParent = "&" + CoordinatesParent;
                    //if (WithParent)
                    //{
                    //    URL += System.Web.HttpUtility.UrlEncode(CoordinatesParent);
                    //    URL = "http://www.snsb.info/GoogleMaps/DiversityWorkbench_PolygonParent.cfm?" + URL;
                    //    if (CoordinatesPoints.Length > 0 && LabelForPoints.Length > 0)
                    //    {
                    //        if (!CoordinatesPoints.StartsWith("&"))
                    //        {
                    //            CoordinatesPoints = "&" + CoordinatesPoints;
                    //        }
                    //        URL += System.Web.HttpUtility.UrlEncode(CoordinatesPoints);
                    //        if (!LabelForPoints.StartsWith("&"))
                    //        {
                    //            LabelForPoints = "&" + LabelForPoints;
                    //        }
                    //        URL += System.Web.HttpUtility.UrlEncode(LabelForPoints);
                    //    }
                    //}
                    //else
                    //{
                    //    if (CoordinatesPoints.Length > 0)
                    //    {
                    //        if (!CoordinatesPoints.StartsWith("&"))
                    //        {
                    //            CoordinatesPoints = "&" + CoordinatesPoints;
                    //        }
                    //        URL += System.Web.HttpUtility.UrlEncode(CoordinatesPoints);
                    //        if (LabelForPoints.Length == 0)
                    //        {
                    //            LabelForPoints = "Label=";
                    //            char[] charSeparators = new char[] { '&' };
                    //            string[] Labels = CoordinatesPoints.Split(charSeparators);
                    //            if (Labels.Length == 3)
                    //                LabelForPoints += "0";
                    //            else
                    //            {
                    //                for (int i = 0; i < Labels.Length; i++)
                    //                {
                    //                    if (Labels[i].StartsWith("Lat"))
                    //                        LabelForPoints += i.ToString() + "|";
                    //                }
                    //                LabelForPoints = LabelForPoints.Substring(LabelForPoints.Length - 1);
                    //            }
                    //        }
                    //        if (!LabelForPoints.StartsWith("&"))
                    //        {
                    //            LabelForPoints = "&" + LabelForPoints;
                    //        }
                    //        URL += System.Web.HttpUtility.UrlEncode(LabelForPoints);
                    //    }
                    //    URL = "http://www.snsb.info/GoogleMaps/DiversityWorkbench_PolygonParent.cfm?" + URL;
                    //}
                    URL += "&Height=" + (this.Height).ToString() + "&Width=" + (this.Width).ToString();
                    this._URI = new Uri(URL);
                    this.webBrowser.ScrollBarsEnabled = false;
                    this.webBrowser.Url = this._URI;
                }
                catch (Exception ex)
                {
                    //DiversityWorkbench.ExceptionHandling.WriteToErrorLogFile(ex);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Could not find coordinates to generate a map");
                this._URI = new Uri("about:blank");
                this.webBrowser.Url = this._URI;
            }
        }

        public void setUrlForCoordinateRetrieval(float Latitude, float Longitude)
        {
            this._Latitude = Latitude;
            this._Longitude = Longitude;
            string URI = "http://www.snsb.info/GoogleMaps/DiversityCollection_GetCoordinates.cfm";
            if (this._Latitude != 0 || this._Longitude != 0)
                URI += "?Lat=" + this._Latitude.ToString().Replace(",", ".") + "&Lng=" + this._Longitude.ToString().Replace(",", ".");
            else
                URI += "?Lat=48.16385096&Lng=11.50062829";
            URI += "&SizeX=" + this.webBrowser.Width.ToString() + "&SizeY=" + this.webBrowser.Height.ToString();
            System.Uri u = new Uri(URI);
            this.webBrowser.Url = u;
        }

        public void ReadValuesFromCurrentPosition()
        {
            try
            {
                CultureInfo InvC = new CultureInfo("");

                //System.Uri Uri = this.webBrowser.Url;
                //this.webBrowser.Url = Uri;
                string s = System.Windows.Forms.Clipboard.GetText();
                if (s.Length > 0)
                {
                    string sLowLat = s.Substring(2, s.IndexOf(" ") - 3).Trim();
                    string sLeftLon = s.Substring(s.IndexOf(" "), s.IndexOf(")") - s.IndexOf(" ")).Trim();
                    s = s.Substring(s.IndexOf("), (") + 4).Trim();
                    string sUpLat = s.Substring(0, s.IndexOf(" ") - 1).Trim();
                    string sRightLon = s.Substring(s.IndexOf(" "), s.IndexOf(")") - s.IndexOf(" ")).Trim();

                    if (!float.TryParse(sLowLat, NumberStyles.Float, InvC, out this._LowerLatitude))
                    {
                        if (!float.TryParse(sLowLat.Replace(",", "."), NumberStyles.Float, InvC, out this._LowerLatitude))
                            this._LowerLatitude = 0.0f;
                    }
                    if (!float.TryParse(sLeftLon, NumberStyles.Float, InvC, out this._LeftLongitude))
                    {
                        if (!float.TryParse(sLeftLon.Replace(",", "."), NumberStyles.Float, InvC, out this._LeftLongitude))
                            this._LeftLongitude = 0.0f;
                    }
                    if (!float.TryParse(sUpLat, NumberStyles.Float, InvC, out this._UpperLatitude))
                    {
                        if (!float.TryParse(sUpLat.Replace(",", "."), NumberStyles.Float, InvC, out this._UpperLatitude))
                            this._UpperLatitude = 0.0f;
                    }
                    if (!float.TryParse(sRightLon, NumberStyles.Float, InvC, out this._RightLongitude))
                    {
                        if (!float.TryParse(sRightLon.Replace(",", "."), NumberStyles.Float, InvC, out this._RightLongitude))
                            this._RightLongitude = 0.0f;
                    }

                    this._Longitude = (this._LeftLongitude + this._RightLongitude) / 2;
                    this._Latitude = (this._LowerLatitude + this._UpperLatitude) / 2;
                    this._LongitudeAccuracy = System.Math.Abs((this._LeftLongitude - this._RightLongitude) / (float)this.webBrowser.Width);
                    this._LatitudeAccuracy = System.Math.Abs((this._LowerLatitude - this._UpperLatitude) / (float)this.webBrowser.Height);
                }
            }
            catch (Exception ex)
            {
                //DiversityWorkbench.ExceptionHandling.WriteToErrorLogFile(ex);
            }
        }

        public double Longitude
        {
            get
            {
                if (this._Longitude > 180 || this._Longitude < -180)
                    this.ReadValuesFromCurrentPosition();
                return (double)((float)this._Longitude);
            }
        }

        public double Latitude
        {
            get
            {
                if (this._Latitude > 180 || this._Latitude < -180)
                    this.ReadValuesFromCurrentPosition();
                return (double)((float)this._Latitude);
            }
        }

        public double LatitudeAccuracy
        {
            get
            {
                if (this._LatitudeAccuracy == 0)
                    this.ReadValuesFromCurrentPosition();
                return (double)((float)this._LatitudeAccuracy);
            }
        }

        public double LongitudeAccuracy
        {
            get
            {
                if (this._LongitudeAccuracy == 0)
                    this.ReadValuesFromCurrentPosition();
                return (double)((float)this._LongitudeAccuracy);
            }
        }

        public double Accuracy
        {
            get
            {
                if (this._LatitudeAccuracy == 0)
                    this.ReadValuesFromCurrentPosition();
                double A = this._LatitudeAccuracy * 111120 * 2;
                if (A < 1)
                    A = Math.Round(A, 1);
                else
                    A = Math.Round(A, 0);
                return A;
            }
        }

    }
}
