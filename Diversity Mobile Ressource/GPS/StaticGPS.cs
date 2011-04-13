//#######################################################################
//Diversity Mobile Synchronization
//Project Homepage:  http://www.diversitymobile.net
//Copyright (C) 2011  Tobias Schneider, Lehrstuhl Angewndte Informatik IV, Universität Bayreuth
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//#######################################################################

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace UBT.AI4.Bio.DiversityCollection.Ressource.GPS
{
    public static class StaticGPS
    {
        public static GpsDeviceState device = null;
        public static GpsPosition position = null;
        //public static List<GPSData> dataList;
        private static DateTime lastSave;
        private static  Gps gps = new Gps();

        public static EventHandler updatePositionDataHandler = null;
        public static EventHandler updateDeviceDataHandler = null;

        static StaticGPS()
        {
            gps.DeviceStateChanged += new DeviceStateChangedEventHandler(gps_DeviceStateChanged);
            gps.LocationChanged += new LocationChangedEventHandler(gps_LocationChanged);
            //dataList = new List<GPSData>();
        }


        public static bool isOpened()
        {
            return gps.Opened;
        }

        public static void Open()
        {
            if (!gps.Opened)
            {
                gps.Open();
            }
        }

        public static void Close()
        {
            if (gps.Opened)
            {
                gps.Close();
            }
        }

        static void gps_LocationChanged(object sender, LocationChangedEventArgs args)
        {
            position = args.Position;

            // call the UpdateData method via the updateDataHandler so that we
            // update the UI on the UI thread
            if (updatePositionDataHandler != null)
            {
                updatePositionDataHandler.Invoke(sender, args);
            }
            //if (lastSave == null)
            //{
            //    GPSData point = new GPSData(position.Longitude, position.Latitude);
            //    //dataList.Add(point);
            //    lastSave = DateTime.Now;
            //}
            //if ((DateTime.Now.Millisecond - lastSave.Millisecond) > 10000)
            //{
            //    GPSData point = new GPSData(position.Longitude, position.Latitude);
            //    dataList.Add(point);
            //    lastSave = DateTime.Now;
            //}
            //System.Threading.Thread.Sleep(3000);
        }

        static void gps_DeviceStateChanged(object sender, DeviceStateChangedEventArgs args)
        {
            device = args.DeviceState;

            // call the UpdateData method via the updateDataHandler so that we
            // update the UI on the UI thread
            if (updatePositionDataHandler != null)
            {
                updateDeviceDataHandler.Invoke(sender, args);
            }
        }

        static bool UpdatePositionData()
        {
            return true;
        }

        static bool UpdateDeviceData()
        {
            return true;
        }
        
    }
}
