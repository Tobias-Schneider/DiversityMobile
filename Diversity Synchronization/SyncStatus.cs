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
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace Diversity_Synchronization
{
    public class SyncStatus : INotifyPropertyChanged
    {
        [Flags]
        public enum ConnectionState
        {
            None = 0x00,
            ConnectedToRepository = 0x01,
            ConnectedToRepTax = 0x02,
            ConnectedToMobile = 0x04,
            ConnectedToMobileTax = 0x08,     



            FullyConnected = ConnectedToRepository | ConnectedToRepTax | ConnectedToMobile | ConnectedToMobileTax,
            RepositoriesConnected = ConnectedToRepository | ConnectedToRepTax,
            MobileConnected = ConnectedToMobile | ConnectedToMobileTax,
            SyncConnected = ConnectedToMobile | ConnectedToRepository,
            TaxonConnected = ConnectedToMobileTax | ConnectedToRepTax,
            DatabasesConnected = ConnectedToMobile | ConnectedToRepository
        }

        [Flags]
        public enum SyncState
        {
            None = 0x00,
            TaxonCatalogs = 0x01,
            CollectionDefinitions = 0x02,
            FieldDataUpload = 0x04,
            FieldDataDownload = 0x08,
            PropertyDownload = 0x10,
            Cleaned = 0x20,
            Maps = 0x40
        }

        private SyncStatus()
        {
            resetConnection();
            resetSync();
        }

        public void resetConnection()
        {
            Connection = ConnectionState.None;            
        }

        public void resetSync()
        {
            Sync = SyncState.None;
        }


        #region Singleton
        private static SyncStatus instance = new SyncStatus();
        public static SyncStatus Instance
        {
            get
            {
                return instance;
            }
        }
        public static SyncStatus GetInstance() { return instance; }
        #endregion

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        private ConnectionState connectionState;
        public ConnectionState Connection
        {
            get
            {
                return connectionState;
            }
            internal set
            {
                connectionState = value;
                this.RaisePropertyChanged(() => Connection, PropertyChanged);
            }
        }

        private SyncState syncState;        
        public SyncState Sync
        {
            get
            {
                return syncState;
            }
            set
            {
                if (syncState != value)
                {
                    syncState = value;
                    this.RaisePropertyChanged(() => Sync, PropertyChanged);
                    _isDirty = true;
                }
            }
        }

        private bool _isDirty = false;
        public bool isDirty()
        {
            return _isDirty;
        }
        public void setClean()
        {
            _isDirty = false;
        }

        #endregion

    }
}
