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
