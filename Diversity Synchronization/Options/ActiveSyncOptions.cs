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
    
    public class ActiveSyncOptions : IComparable
    {
        
        private bool useDevice;
        private int connectingTimeOut;
        private string diversityMobileDBPath;
        private string taxonNamesDBPath;

        public ActiveSyncOptions()
        {
            //Default-Werte
            useDevice = false;
            connectingTimeOut = 30000;
            diversityMobileDBPath = Environment.CurrentDirectory + @"\MobileDB.sdf";
            taxonNamesDBPath = Environment.CurrentDirectory + @"\TaxonNames.sdf";
        }

        #region Properties

        public bool UseDevice
        {
            get
            {
                return useDevice;
            }
            set
            {
                useDevice = value;
            }
        }

        public int ConnectingTimeOut
        {
            get
            {
                return connectingTimeOut;
            }
            set
            {
                connectingTimeOut = value;
            }
        }

        public string DiversityMobileDBPath
        {
            get
            {
                return diversityMobileDBPath;
            }
            set
            {
                diversityMobileDBPath = value;
            }
        }

        public string TaxonNamesDBPath
        {
            get
            {
                return taxonNamesDBPath;
            }
            set
            {
                taxonNamesDBPath = value;
            }
        }

        #endregion

        public int CompareTo(object obj)
        {
            ActiveSyncOptions sOp = null;
            if (obj is ActiveSyncOptions)
            {
                sOp = obj as ActiveSyncOptions;
            }
            else
            {
                return 1;
            }

            if (sOp != null)
            {
                if (this.useDevice.CompareTo(sOp.useDevice) != 0)
                {
                    return 1;
                }
                else
                {
                    if (this.connectingTimeOut.CompareTo(sOp.connectingTimeOut) != 0)
                    {
                        return 1;
                    }
                    else
                    {
                        if (this.diversityMobileDBPath.CompareTo(sOp.diversityMobileDBPath) != 0)
                        {
                            return 1;
                        }
                        else
                        {
                            if (this.taxonNamesDBPath.CompareTo(sOp.taxonNamesDBPath) != 0)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
            }

            return 1;
        }
    }
}
