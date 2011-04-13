using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DiversityCollection.Ressource.GPS
{
    public class GPSData
    {
        private double? longitude;
        private double? latitude;
        //private double? altitude;

        public GPSData(double? lon, double? lat)
        {
            this.longitude=lon;
            this.latitude = lat;
            //this.altitude = alt;
        }
        public double? Longitude { get { return this.longitude; } }
        public double? Latitude {get{return this.latitude;}}
        //public double? Altitude{get { return this.altitude; }}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("(");
            if(this.longitude!=null)
            {
                sb.Append(this.latitude).Append(" ");
            }
            else
            {
                return null;
            }
            if (this.latitude != null)
                sb.Append(this.longitude);
            else
                return null;
            //if (this.altitude != null)
            //{
            //    sb.Append(",").Append(this.altitude);
            //}
            sb.Append(")");
            return sb.ToString();
        }
    }
}
