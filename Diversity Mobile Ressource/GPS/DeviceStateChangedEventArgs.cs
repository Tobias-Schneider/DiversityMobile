using System;

namespace UBT.AI4.Bio.DiversityCollection.Ressource.GPS
{
    /// <summary>
    /// Event args used for DeviceStateChanged event.
    /// </summary>
    public class DeviceStateChangedEventArgs: EventArgs
    {
        public DeviceStateChangedEventArgs(GpsDeviceState deviceState)
        {
            this.deviceState = deviceState;
        }

        /// <summary>
        /// Gets the new device state when the GPS reports a new device state.
        /// </summary>
        public GpsDeviceState DeviceState
        {
            get 
            {
                return deviceState;
            }
        }

        private GpsDeviceState deviceState;
    }
}
