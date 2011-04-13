using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Diversity_Synchronization.Options
{
    public class MapSaveOptions : IComparable
    {
        private bool useDeviceDimensions;
        private bool saveToDevice;

        public MapSaveOptions()
        {
            //Default-Werte
            this.useDeviceDimensions = true;
            this.saveToDevice = false;
        }

        #region Properties

        public bool UseDeviceDimensions
        {
            get
            {
                return useDeviceDimensions;
            }
            set
            {
                useDeviceDimensions = value;
            }
        }

        public bool SaveToDevice
        {
            get
            {
                return saveToDevice;
            }
            set
            {
                saveToDevice = value;
            }
        }

        #endregion

        public int CompareTo(object obj)
        {
            MapSaveOptions sOp = null;
            if (obj is MapSaveOptions)
            {
                sOp = obj as MapSaveOptions;
            }
            else
            {
                return 1;
            }

            if (sOp != null)
            {
                if (this.useDeviceDimensions.CompareTo(sOp.useDeviceDimensions) != 0)
                {
                    return 1;
                }
                else
                {
                    if (this.saveToDevice.CompareTo(sOp.saveToDevice) != 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            return 1;
        }
    }
}
