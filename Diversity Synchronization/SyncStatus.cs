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
