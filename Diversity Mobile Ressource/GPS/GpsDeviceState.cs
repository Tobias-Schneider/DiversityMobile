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
using System.Runtime.InteropServices;

public enum GpsServiceState : int
{
    Off = 0,
    On = 1,
    StartingUp = 2, 
    ShuttingDown = 3,
    Unloading = 4,
    Uninitialized = 5,
    Unknown = -1
}

namespace UBT.AI4.Bio.DiversityCollection.Ressource.GPS
{

    [StructLayout(LayoutKind.Sequential)]
    internal struct FileTime
    {
        int dwLowDateTime;
        int dwHighDateTime;
    }

    /// <summary>
    /// GpsDeviceState holds the state of the gps device and the friendly name if the 
    /// gps supports them.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class GpsDeviceState
    {
        public static int GpsMaxFriendlyName = 64;
        public static int GpsDeviceStructureSize = 216;

        int serviceState = 0;
        /// <summary>
        /// State of the GPS Intermediate Driver service
        /// </summary>
        public GpsServiceState ServiceState
        {
            get {return (GpsServiceState)serviceState;}
        }

        int deviceState = 0;
        /// <summary>
        /// Status of the actual GPS device driver.
        /// </summary>
        public GpsServiceState DeviceState
        {
            get {return (GpsServiceState)deviceState;}
        }

        string friendlyName = "";
        /// <summary>
        /// Friendly name of the real GPS device we are currently using.
        /// </summary>
        public string FriendlyName
        {
            get {return friendlyName;}
        }

        /// <summary>
        /// Constructor of GpsDeviceState.  It copies values from the native pointer 
        /// passed in. 
        /// </summary>
        /// <param name="pGpsDevice">Native pointer to memory that contains
        /// the GPS_DEVICE data</param>
        public GpsDeviceState(IntPtr pGpsDevice)
        {
            // make sure our pointer is valid
            if (pGpsDevice == IntPtr.Zero)
            {
                throw new ArgumentException();
            }

            // read in the service state which starts at offset 8
            serviceState = Marshal.ReadInt32(pGpsDevice, 8);
            // read in the device state which starts at offset 12
            deviceState = Marshal.ReadInt32(pGpsDevice, 12);

            // the friendly name starts at offset 88
            IntPtr pFriendlyName = (IntPtr)(pGpsDevice.ToInt32() + 88);
            // marshal the native string into our gpsFriendlyName
            friendlyName = Marshal.PtrToStringUni(pFriendlyName);
        }
    }
}
