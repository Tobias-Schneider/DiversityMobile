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
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using OpenNETCF.Desktop.Communication;
using Diversity_Synchronization.Options;
using System.Data;
using System.Linq;
using System.Data.Linq;


using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.SyncBase;
using UBT.AI4.Bio.DivMobi.ListSynchronization;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;
using System.ComponentModel;
using System.Data.SqlServerCe;
using System.Data.Common;
using log4net;

namespace Diversity_Synchronization
{
    /**
     * Ermöglicht den global verwalteten Zugriff auf die Datenbankverbindungen
     */
    public class ConnectionsAccess : INotifyPropertyChanged
    {
        [Flags]
        public enum ConnectionState
        {
            None = 0x00,
            ConnectedToRepository = 0x01,
            ConnectedToRepTax = 0x02,
            ConnectedToMobile = 0x04,
            ConnectedToMobileTax = 0x08,
            ProfilePresent = 0x10,
            ConnectedToSynchronization = 0x20,



            FullyConnected = RepositoryConnected | MobileConnected,
            RepositoryConnected = ConnectedToRepository | ConnectedToRepTax | ConnectedToSynchronization,
            MobileConnected = ConnectedToMobile | ConnectedToMobileTax,
            SyncConnected = ConnectedToMobile | ConnectedToRepository | ConnectedToSynchronization,
            TaxonConnected = ConnectedToMobileTax | ConnectedToRepTax,
            DatabasesConnected = ConnectedToMobile | ConnectedToRepository       
        }
        
            
        #region Singleton
        private static ConnectionsAccess instance = new ConnectionsAccess();
        public static ConnectionsAccess Instance
        {
            get
            {
                return instance;
            }
        }
        public static ConnectionsAccess GetInstance() { return instance; }
        #endregion

        #region INotifyPropertyChanged Member

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields
        private const string syncDBCatalog = "Synchronisation_Test";
        
        private IList<Type> divMobiTypes;        
        private IList<Type> syncTypes;
        
        string localDivDBPath;
        string localTaxDBPath;

        ILog _Log = log4net.LogManager.GetLogger(typeof(ConnectionsAccess));

        private RAPI _rapi;
        private RAPI Rapi
        {
            get
            {
                if (_rapi == null)
                {
                    try
                    {
                        _rapi = new RAPI();
                    }
                    catch (Exception ex)
                    {
                        _Log.ErrorFormat("Cannot create Rapi!\nException:\n{0}", ex.Message);
                    }
                }
                return _rapi;
            }
        }

        
        #endregion

        ConnectionsAccess()
        {
            fillDivMobiTypes();
            fillSyncTypes();   
        }        

       

        #region Properties
        private static ConnectionState currentState = ConnectionState.None;
        public static ConnectionState CurrentState
        {
            get
            {
                return currentState;
            }
        }
        public ConnectionState State
        {
            get
            {
                return currentState;
            }
            private set
            {
                currentState = value;
                this.RaisePropertyChanged(() => State, PropertyChanged);
            }
        }

        internal static Serializer MobileTaxa { get; private set; }
        internal static Serializer RepositoryDefinitions { get; private set; }
        internal static Serializer MobileDB { get; private set; }
        internal static Serializer RepositoryDB { get; private set; }
        internal static Serializer Synchronization { get; private set; }


        public static UserProfile Profile
        {
            get;
            private set;
        }      

        #endregion

        #region Public Methods

        #region Repository Connection
        public void connectToRepository(RepositoryOptions ro, string user, string password) 
        {
            if (string.IsNullOrEmpty(ro.InitialCatalog) ||
                string.IsNullOrEmpty(ro.IPAddress) ||
                string.IsNullOrEmpty(ro.IPPort) ||
                string.IsNullOrEmpty(user) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(ro.TaxonNamesInitialCatalog))
                return;
            //RepositoryOptions aktualisieren?
            if (ro.SqlAuthentification)
            {                
                if (ro.LastUsername != user)
                {
                    ro.LastUsername = user;
                    OptionsAccess.RepositoryOptions = ro;
                }
            }
            else
            {                
                if (ro.LastUsername != Environment.UserName)
                {
                    ro.LastUsername = Environment.UserName;
                    OptionsAccess.RepositoryOptions = ro;
                }
            }
            StringBuilder praef=new StringBuilder();
            praef.Append("[").Append(ro.InitialCatalog).Append("].[dbo].");
            String praefix = praef.ToString();
            StringBuilder taxPraef = new StringBuilder();
            taxPraef.Append("[").Append(ro.TaxonNamesInitialCatalog).Append("].[dbo].");
            String taxonPraefix = taxPraef.ToString();
            StringBuilder syncPraef = new StringBuilder();
            syncPraef.Append("[").Append("Synchronisation_Test").Append("].");
            String syncPraefix = syncPraef.ToString();
            if(ro.SqlAuthentification)
            {                
                RepositoryDB = new MS_SqlServerIPSerializer(ro.LastUsername, password ,ro.IPAddress, ro.IPPort, ro.InitialCatalog, praefix);
                RepositoryDefinitions = new MS_SqlServerIPSerializer(ro.LastUsername, password, ro.IPAddress, ro.IPPort, ro.TaxonNamesInitialCatalog,taxonPraefix);
                Synchronization = new MS_SqlServerIPSerializer(ro.LastUsername, password, ro.IPAddress, ro.IPPort, syncDBCatalog,syncPraefix);
            }
            else
            {               
                RepositoryDB = new MS_SqlServerWASerializier(ro.IPAddress+","+ro.IPPort, ro.InitialCatalog,praefix);
                RepositoryDefinitions = new MS_SqlServerWASerializier(ro.IPAddress + "," + ro.IPPort, ro.TaxonNamesInitialCatalog,taxonPraefix);
                Synchronization = new MS_SqlServerWASerializier(ro.IPAddress + "," + ro.IPPort, syncDBCatalog,syncPraefix);
            }
            //Verbindung zum Repository herstellen
            connectRepositoryDB();
            connectSynchronization(ro);
            connectRepositoryDefinitions();
                    
        }

        private void connectRepositoryDefinitions()
        {
            try
            {
                //Taxon Serializer erstellen                

                RepositoryDefinitions.RegisterType(typeof(TaxonNames));
                RepositoryDefinitions.RegisterType(typeof(PropertyNames));
                State |= ConnectionState.ConnectedToRepTax;
            }
            catch //(Exception repTaxEx)
            {
                RepositoryDefinitions = null;
                State &= ~ConnectionState.ConnectedToRepTax;
            }
        }

        private void connectSynchronization(RepositoryOptions ro)
        {
            try
            {

                String syncIt = "[" + ro.LastUsername + "].SyncItem";
                String fieldSt = "[" + ro.LastUsername + "].FieldState";
                MappingDictionary.Mapping.Add(typeof(SyncItem), syncIt);
                MappingDictionary.Mapping.Add(typeof(FieldState), fieldSt);
                Synchronization.RegisterType(typeof(SyncItem));
                Synchronization.RegisterType(typeof(FieldState));
                Synchronization.Activate();
                State |= ConnectionState.ConnectedToSynchronization;

            }
            catch// (Exception syncDBEx)
            {
                Synchronization = null;
                MappingDictionary.Mapping.Clear();
                State &= ~ConnectionState.ConnectedToSynchronization;
            }
        }

        private void connectRepositoryDB()
        {
            try
            {
                RepositoryDB.RegisterTypes(divMobiTypes);
                RepositoryDB.RegisterType(typeof(UserProxy));
                RepositoryDB.Activate();
                State |= ConnectionState.ConnectedToRepository;
            }
            catch// (Exception repositoryEx)
            {
                RepositoryDB = null;
                State &= ~ConnectionState.RepositoryConnected;
            }
        }

        public void disconnectFromRepository()
        {
            if (Synchronization != null)
            {
                Synchronization.Dispose();
                Synchronization = null;
            }
            if (RepositoryDB != null)
            {
                RepositoryDB.Dispose();
                RepositoryDB = null;
            }
            if (RepositoryDefinitions != null)
            {
                RepositoryDefinitions.Dispose();
                RepositoryDefinitions = null;
            }
            State &= ~ConnectionState.RepositoryConnected;
        }
        #endregion 

        #region Mobile Connection
        public void connectToMobileDB(ActiveSyncOptions aso)
        {
            try
            {
                copyPictures(aso);
            }
            catch (Exception ex)
            {
                _Log.ErrorFormat("Picture Copy Error: {0}",ex.Message != null ? ex.Message : "");
            }

            //Lokale Kopie der Datenbanken anlegen
            localDivDBPath = OptionsAccess.getFolderPath(ApplicationFolder.CurrentTransaction) +"\\"+ Path.GetFileName(aso.DiversityMobileDBPath);
            localTaxDBPath = OptionsAccess.getFolderPath(ApplicationFolder.CurrentTransaction) +"\\"+ Path.GetFileName(aso.TaxonNamesDBPath);
            bool divCopySuccess = false;
            bool taxCopySuccess = false;
            try
            {
                divCopySuccess = copyFromDevice(localDivDBPath, aso.DiversityMobileDBPath, aso.UseDevice);
                if (divCopySuccess == false)
                {
                    throw new Exception();
                }
            }
            catch (Exception divEx)
            {
                _Log.ErrorFormat("Copy Failure MobileDB: {0}",divEx.Message!=null ? divEx.Message : "");
            }
            try
            {
                taxCopySuccess = copyFromDevice(localTaxDBPath, aso.TaxonNamesDBPath, aso.UseDevice);
                if (taxCopySuccess == false)
                {
                    throw new Exception();
                }
            }
            catch (Exception taxEx)
            {
                _Log.ErrorFormat("Copy Failure TaxonNames: {0}", taxEx.Message != null ? taxEx.Message : "");
              
            }

            if (divCopySuccess && taxCopySuccess)
            {
                _Log.Info("Database working copy created.");
                connectMobile();
            }                
        }

        private void connectMobile()
        {
            int i = 0;
            try
            {
                MobileDB = new MS_SqlCeSerializer(localDivDBPath);
                i++;//1
                MobileDB.RegisterTypes(divMobiTypes);
                i++;
                MobileDB.RegisterType(typeof(UserProfile));
                i++;
                MobileDB.Activate();
                i++;
                //Prüfen ob ein UserProfile zum LoginNamen existiert.                
                IList<UserProfile> profiles = new List<UserProfile>();
                i++;//5
                IRestriction r = RestrictionFactory.Eq(typeof(UserProfile), "_LoginName", OptionsAccess.RepositoryOptions.LastUsername);
                //IRestriction r = RestrictionFactory.Eq(typeof(UserProfile), "_LoginName", "TestEditor");
                profiles = MobileDB.Connector.LoadList<UserProfile>(r);
                i++;
                if (profiles.Count > 0)
                {
                    Profile = profiles[0];
                }
                else
                {
                    Profile = createProfile();
                }

                i++;//7


                //mobile Tax Serializer erzeugen
                try
                {
                    MobileTaxa = new MS_SqlCeSerializer(localTaxDBPath);
                    i++;
                    MobileTaxa.RegisterType(typeof(TaxonNames));
                    MobileTaxa.RegisterType(typeof(PropertyNames));
                    i++;//9
                }
                catch
                {
                    MobileTaxa = null;
                }
            }
            catch (Exception mobileDBEx)
            {
                _Log.ErrorFormat("ConnectionError {0} {1}", i, mobileDBEx.Message != null ? mobileDBEx.Message : "");
                MobileDB = null;
                Profile = null;
            }
            finally
            {
                i = 0;
                if (Profile != null)
                {
                    State |= ConnectionState.ProfilePresent;
                    State |= ConnectionState.ConnectedToMobile;
                    i = 10;
                }
                else
                {
                    State &= ~ConnectionState.ProfilePresent;
                    State &= ~ConnectionState.ConnectedToMobile;
                    i = 20;
                }
                if (MobileTaxa != null)
                {
                    State |= ConnectionState.ConnectedToMobileTax;
                    i = i + 100;
                }
                else
                {
                    State &= ~ConnectionState.ConnectedToMobileTax;
                    i = i + 200;
                }
                if (i != 110)
                    _Log.ErrorFormat("Final Result: {0}",i);
            }
        }

        private void copyPictures(ActiveSyncOptions aso)
        {
            string mobilePictureDirectory = Path.GetDirectoryName(aso.DiversityMobileDBPath) + "\\pictures";
            if (Directory.Exists(mobilePictureDirectory))
            {
                foreach (var picture in Directory.GetFiles(mobilePictureDirectory))
                {
                    copyFromDevice(OptionsAccess.getFolderPath(ApplicationFolder.Pictures) + "\\" + Path.GetFileName(picture), picture, aso.UseDevice);
                }
            }
        }

        public void disconnectFromMobileDB()
        {
            if (MobileDB != null)
            {
                MobileDB.Dispose();
                MobileDB = null;
            }
            if (MobileTaxa != null)
            {
                MobileTaxa.Dispose();
                MobileTaxa = null;
            }
            State &= ~ConnectionState.MobileConnected;            
        }
        #endregion

        #region DB Operations
        private string defaultMobileDB = "MobileDB.sdf";
        private string defaultTaxonDB = "TaxonNames.sdf";
        public void cleanDB()
        {
            _Log.Info("Cleaning DBs");
            var profile = Profile;
            disconnectFromMobileDB();

            if (copyEmptyDBs())
            {
                SyncStatus.Instance.Sync |= SyncStatus.SyncState.Cleaned;
                connectMobile();

                Profile = profile;
                Profile.HomeDB = OptionsAccess.RepositoryOptions.InitialCatalog;
                saveProfile();
                disconnectFromMobileDB();

                writeBackChanges();
                truncateSyncTable();                
            }
            connectMobile();
            SyncStatus.Instance.Sync = SyncStatus.SyncState.None;
        }

        private bool copyEmptyDBs()
        {
            bool successSoFar = true;
            string dbDir = OptionsAccess.getFolderPath(ApplicationFolder.EmptyDatabases);
            string emptyMobile = dbDir + "\\" + defaultMobileDB;
            string emptyTaxa = dbDir + "\\" + defaultTaxonDB;

            string currentTransactionDir = OptionsAccess.getFolderPath(ApplicationFolder.CurrentTransaction);
            localDivDBPath = currentTransactionDir + "\\clean_" + defaultMobileDB;
            localTaxDBPath = currentTransactionDir + "\\clean_" + defaultTaxonDB;
             
            if (!File.Exists(emptyMobile))
            {
                _Log.Error("Empty Mobile DB missing!");
                successSoFar = false;
            }

            if (!File.Exists(emptyTaxa))
            {
                _Log.Error("Empty Taxon DB missing!");
                successSoFar = false;
            }

            if (successSoFar)
            {
                try
                {
                    File.Copy(emptyTaxa, localTaxDBPath, true);
                    File.Copy(emptyMobile, localDivDBPath, true);
                }
                catch (IOException io)
                {
                    _Log.ErrorFormat("Error while copying empty databases: {0}", io);
                    successSoFar = false;
                }
            }
            return successSoFar;
        }

        private void truncateSyncTable()
        {
            if (Synchronization != null)
            {
                using (var sync = Synchronization.CreateConnection())
                {
                    using (var truncate = sync.CreateCommand())
                    {
                        truncate.CommandText = String.Format("TRUNCATE TABLE [{0}].[{1}].[SyncItem];", syncDBCatalog, ConnectionsAccess.Profile.LoginName.nullToEmpty());
                        int rowsAffected;
                        try
                        {
                            sync.Open();
                            rowsAffected = truncate.ExecuteNonQuery();
                        }
                        finally
                        {
                            sync.Close();
                        }

                        _Log.InfoFormat("Sync table truncated. {0} rows affected", rowsAffected);
                    }
                }
            }
            else
                _Log.Error("Cannot truncate sync table. Sync Serializer is null.");
        }

        public void saveProfile() 
        {
            if (Profile != null)
            {
                if (ConnectionsAccess.MobileDB != null)
                {
                    ConnectionsAccess.MobileDB.Connector.Save(Profile);
                }
            }
            else
            {
                //TODO Should not have happened
            }
        }        
        #endregion

        public void disconnectEverything()
        {
            disconnectFromRepository();
            disconnectFromMobileDB();
        }

        public void writeBackChanges()
        {
            if (SyncStatus.Instance.isDirty())
            {
                _Log.Info("Writing back changes");
                copyToDevice(localDivDBPath, OptionsAccess.ActiveSyncOptions.DiversityMobileDBPath, OptionsAccess.ActiveSyncOptions.UseDevice);
                copyToDevice(localTaxDBPath, OptionsAccess.ActiveSyncOptions.TaxonNamesDBPath, OptionsAccess.ActiveSyncOptions.UseDevice);

                string mobileMapsPath = Path.GetDirectoryName(OptionsAccess.ActiveSyncOptions.DiversityMobileDBPath) + "\\maps";
                string localMapsPath = OptionsAccess.getFolderPath(ApplicationFolder.Maps);
                if (!Directory.Exists(mobileMapsPath))
                    Directory.CreateDirectory(mobileMapsPath);
                if (Directory.Exists(localMapsPath) && Directory.Exists(mobileMapsPath))
                {
                    bool useAS = OptionsAccess.ActiveSyncOptions.UseDevice;
                    foreach (var file in Directory.GetFiles(localMapsPath, "*.png"))
                    {
                        var xmlFile = file.Replace(".png", ".xml");
                        var pngLPath = file;
                        var pngMPath = file.Replace(localMapsPath, mobileMapsPath);
                        var xmlLPath = xmlFile;
                        var xmlMPath = xmlFile.Replace(localMapsPath, mobileMapsPath);
                        if (File.Exists(xmlLPath))
                        {
                            copyToDevice(pngLPath, pngMPath, useAS);
                            copyToDevice(xmlLPath, xmlMPath, useAS);
                        }
                    }

                }

            }
            else
                _Log.Info("Not writing back changes: unchanged.");
        }

        #endregion

        #region Private Methods



        private bool copyFromDevice(string localPath, string remotePath, bool useActiveSync)
        {

            _Log.DebugFormat("Copying file from device:[{1}]->[{0}]", localPath, remotePath);


            if (useActiveSync && Rapi != null)
            {
                if (Rapi.DevicePresent && Rapi.Connected)
                {
                    Rapi.CopyFileFromDevice(localPath, remotePath, true);
                    return true;
                }
                else
                {
                    return false;//TODO Fehlermeldung/Logging
                }                    
            }
            else
            {
                if (System.IO.File.Exists(remotePath))
                {
                    try
                    {
                        System.IO.File.Copy(remotePath, localPath, true);
                        return true;
                    }
                    catch (IOException ex)
                    {
                        _Log.ErrorFormat("Copy Failure: [{0}]", ex.Message != null ? ex.Message : "");
                        return false;
                    }
                }
                else
                {
                    _Log.ErrorFormat("Copy Failure: Path Not Found [{0}]",remotePath.nullToEmpty());
                    return false;
                }
            }  
        }

        private bool copyToDevice(string localPath, string remotePath, bool useActiveSync)
        {
            _Log.DebugFormat("Copying file to device: [{0}]->[{1}]", localPath, remotePath);
            if (useActiveSync && Rapi != null)
            {
                if (Rapi.DevicePresent && Rapi.Connected)
                {
                    Rapi.CopyFileToDevice(localPath, remotePath, true);
                    return true;
                }
                else
                {
                    return false;//TODO Fehlermeldung/Logging
                }
            }
            else
            {
                if (System.IO.File.Exists(localPath))
                {
                    try
                    {
                        System.IO.File.Copy(localPath, remotePath, true);
                        return true;
                    }
                    catch (IOException ex)
                    {
                        _Log.ErrorFormat("Copying file to device failed:\n[{0}]\n[{1}]->[{2}]",ex, localPath.nullToEmpty(), remotePath.nullToEmpty());
                        return false;
                    }
                }
                else
                {
                    _Log.ErrorFormat("Copying file to device failed: Source does not exist! [{0}]",localPath.nullToEmpty());
                    return false;
                }
            }
        }

        private UserProfile createProfile()
        {
            UserProfile newProfile;
            UserProxy proxy;
            int i = 0;
            try
            {
                newProfile = ConnectionsAccess.MobileDB.CreateISerializableObject<UserProfile>();
                i++;
                //Zuerst korrespondierenden Userproxy holen                
                IRestriction r = RestrictionFactory.Eq(typeof(UserProxy), "_LoginName", OptionsAccess.RepositoryOptions.LastUsername);
                //IRestriction r = RestrictionFactory.Eq(typeof(UserProxy), "_LoginName", @"TestEditor");
                proxy = ConnectionsAccess.RepositoryDB.Connector.Load<UserProxy>(r);
                i++;
                newProfile.LoginName = OptionsAccess.RepositoryOptions.LastUsername;
                //newProfile.LoginName = @"TestEditor";
                string agentName = null;
                i++;//3
                using (var conn = ConnectionsAccess.RepositoryDefinitions.CreateConnection())
                {
                    try
                    {
                        conn.Open();
                        i++;
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = "SELECT [AgentName] FROM ["+OptionsAccess.RepositoryOptions.TaxonNamesInitialCatalog+"].[dbo].[IBFagents] WHERE [AgentURI] = '" + proxy.AgentURI + "'";
                        _Log.DebugFormat("Select AgentName Command: [{0}]",cmd.CommandText);
                        i++;//5
                        agentName = cmd.ExecuteScalar() as string;
                        _Log.DebugFormat("AgentName: [{0}]", agentName);
                        i++;   
                    }
                    finally
                    {                            
                        conn.Close();
                        i++;//7
                    }
                }      
                
                newProfile.CombinedNameCache = (!string.IsNullOrEmpty(agentName)) ? agentName : proxy.CombinedNameCache;
                i++;
                newProfile.HomeDB = OptionsAccess.RepositoryOptions.InitialCatalog;
                i++;//9
                newProfile.AgentURI = proxy.AgentURI;
                i++;        
            }
            catch(Exception ex)
            {               
                newProfile = null;               
                
                _Log.ErrorFormat("Error Creating Profile: {0}",ex);
            }
            return newProfile;
        }

        

       

        
        

        private void fillDivMobiTypes()
        {
            divMobiTypes = new List<Type>()
            {
                typeof(Property),
                typeof(Analysis),
                typeof(AnalysisTaxonomicGroup),
                typeof(CollectionAgent),
                typeof(CollectionEvent),
                typeof(CollectionEventImage),
                typeof(CollectionEventLocalisation),
                typeof(CollectionEventProperty),
                typeof(CollectionSpecimen),
                typeof(CollectionSpecimenImage),
                typeof(CollEventImageType_Enum),
                typeof(CollSpecimenImageType_Enum),
                typeof(CollTaxonomicGroup_Enum),
                typeof(Identification),
                typeof(IdentificationUnit),
                typeof(IdentificationUnitAnalysis),
                typeof(LocalisationSystem),
                typeof(CollCircumstances_Enum),
                typeof(CollUnitRelationType_Enum),
                typeof(CollectionEventSeries),
                //Bis hier: Korrepondiert zu DBVersion 20
                //typeof(CollectionEventSeriesImage),
                //typeof(CollEventSeriesImageType_Enum),
                ////Bis hier: Korrepondiert zu DBVersion 22
                typeof(CollIdentificationCategory_Enum),
                //typeof(CollTypeStatus_Enum),
                //typeof(CollIdentificationQualifier_Enum),
                ////Bis hier: Korrepondiert zu DBVersion 25
                //typeof(CollLabelTranscriptionState_Enum),
                //typeof(CollLabelType_Enum),
                ////Bis hier: Korrepondiert zu DBVersion 27
                //typeof(Collection),
                //typeof(CollectionProject),
                //typeof(CollectionSpecimenPart),
                //typeof(CollMaterialCategory_Enum),
                //Bis hier: Korrepondiert zu DBVersion 31
                typeof(IdentificationUnitGeoAnalysis),
                typeof(AnalysisResult),

                typeof(UserTaxonomicGroupTable)
                //Bis hier: Korrepondiert zu DBVersion 34
            };
        }

        private void fillSyncTypes()
        {
            syncTypes = new List<Type>()
            {
                typeof(SyncItem),
                typeof(FieldState)
            };
        }   
        #endregion

        
    }
}
