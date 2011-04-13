using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserSyncGui
{
    public class ImageOptions
    {
        private String name;
        private String description;

        private float swLat;
        private float swLong;

        private float seLat;
        private float seLong;

        private float neLat;
        private float neLong;

        private int zoomLevel;

        public String Name
        {
            set
            {
                this.name = value;
            }
            get
            {
                return this.name;
            }
        }
        public String Description
        {
            set
            {
                this.description = value;
            }
            get
            {
                return this.description;
            }
        }
        public float SWLat
        {
            set
            {
                this.swLat = value;
            }
            get
            {
                return this.swLat;
            }
        }
        public float SWLong
        {
            set
            {
                this.swLong = value;
            }
            get
            {
                return this.swLong;
            }
        }
        public float SELat
        {
            set
            {
                this.seLat = value;
            }
            get
            {
                return this.seLat;
            }
        }
        public float SELong
        {
            set
            {
                this.seLong = value;
            }
            get
            {
                return this.seLong;
            }
        }
        public float NELat
        {
            set
            {
                this.neLat = value;
            }
            get
            {
                return this.neLat;
            }
        }
        public float NELong
        {
            set
            {
                this.neLong = value;
            }
            get
            {
                return this.neLong;
            }
        }

        public int ZoomLevel
        {
            set
            {
                this.zoomLevel = value;
            }
            get
            {
                return this.zoomLevel;
            }
        }
        
    }
}
