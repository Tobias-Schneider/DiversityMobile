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
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.ListSynchronization;
using Diversity_Synchronization;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;

namespace Diversity_Synchronization
{
    public class FieldDataSynchronizer
    {
        private AsynchronousActor fieldDataActor = new AsynchronousActor();
        private FieldDataSelectionBuilder selectionBuilder = new FieldDataSelectionBuilder();
        public FieldDataSelectionBuilder SelectionBuilder
        {
            get
            {
                return selectionBuilder;
            }
        }        

        public ProgressReporter Progress
        {
            get
            {
                return fieldDataActor.Progress;
            }
        }

        #region Upload

        public void uploadFieldData()
        {
            fieldDataActor.beginAction(new Action<IReportDetailedProgress>(uploadFieldDataWorker));
        }

        private void uploadFieldDataWorker(IReportDetailedProgress actor)
        {
            if (ConnectionsAccess.Instance.State.CheckForFlags(Diversity_Synchronization.ConnectionsAccess.ConnectionState.DatabasesConnected))
            {
                actor.IsProgressIndeterminate = true;
                actor.ProgressDescriptionID = 1141;

                ObjectSyncList syncList = new ObjectSyncList();
                List<Type> uploadTypes = new List<Type>();
                uploadTypes.Add(typeof(CollectionAgent));
                uploadTypes.Add(typeof(CollectionEvent));
                uploadTypes.Add(typeof(CollectionEventImage));
                uploadTypes.Add(typeof(CollectionEventLocalisation));
                uploadTypes.Add(typeof(CollectionEventProperty));
                uploadTypes.Add(typeof(CollectionSpecimen));
                uploadTypes.Add(typeof(CollectionSpecimenImage));
                uploadTypes.Add(typeof(Identification));
                uploadTypes.Add(typeof(IdentificationUnit));
                uploadTypes.Add(typeof(IdentificationUnitAnalysis));
                uploadTypes.Add(typeof(IdentificationUnitGeoAnalysis));
                uploadTypes.Add(typeof(CollectionEventSeries));
                uploadTypes.Add(typeof(CollectionProject));
                //Bis hier: Korrepondiert zu DBVersion 31
                foreach (Type t in uploadTypes)
                {
                    syncList.Load(t, ConnectionsAccess.MobileDB);
                }
                
                syncList.initialize(LookupSynchronizationInformation.getFieldDataList(), LookupSynchronizationInformation.getReflexiveReferences(), LookupSynchronizationInformation.getReflexiveIDFields());
                String userNumber =(ConnectionsAccess.Profile.AgentURI==null)?"":ConnectionsAccess.Profile.AgentURI.Replace(@"http://id.snsb.info/Agents/", "");
                if (userNumber == "")
                {
                    //TODO Empty User No
                }

               
                SNSBPictureTransfer snsb = new SNSBPictureTransfer(userNumber,(int) ConnectionsAccess.Profile.ProjectID, ConnectionsAccess.MobileDB,OptionsAccess.getFolderPath(ApplicationFolder.Pictures));

                AnalyzeSyncObjectList ansl = new AnalyzeSyncObjectList(syncList, ConnectionsAccess.MobileDB, ConnectionsAccess.RepositoryDB, ConnectionsAccess.Synchronization,snsb);
                ansl.analyzeAll();
                

                //Alles außer InsertState auf ignore setzen
                List<ListContainer> conflicted;
                List<ListContainer> conflictResolved;
                List<ListContainer> synchronized;
                List<ListContainer> insert;
                List<ListContainer> update;
                List<ListContainer> ignore;
                List<ListContainer> delete;
                List<ListContainer> premature;
                conflicted = ansl.getObjectsOfState(SyncStates_Enum.ConflictState);
                conflictResolved = ansl.getObjectsOfState(SyncStates_Enum.ConflictResolvedState);
                synchronized = ansl.getObjectsOfState(SyncStates_Enum.SynchronizedState);
                insert = ansl.getObjectsOfState(SyncStates_Enum.InsertState);
                update = ansl.getObjectsOfState(SyncStates_Enum.UpdateState);
                ignore = ansl.getObjectsOfState(SyncStates_Enum.IgnoreState);
                delete = ansl.getObjectsOfState(SyncStates_Enum.DeletedState);
                premature = ansl.getObjectsOfState(SyncStates_Enum.PrematureState);
                //ProgressDescription = "Warning: Only InsertState is allowed at the moment. All other states will be set to IgnoreState";
                System.Threading.Thread.Sleep(1000);
                
                foreach (ListContainer lc in conflicted)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                foreach (ListContainer lc in conflictResolved)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                foreach (ListContainer lc in synchronized)
                {
                    lc.State = SyncStates_Enum.IgnoreState;//Exception throwen?
                }
                foreach (ListContainer lc in update)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                foreach (ListContainer lc in delete)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                foreach (ListContainer lc in premature)
                {
                    lc.State = SyncStates_Enum.IgnoreState;//Exception throwen?
                }
                ansl.synchronizeAll();

                SyncStatus.Instance.Sync |= SyncStatus.SyncState.FieldDataUpload;
            }
        }
        #endregion

        #region Download

        public void downloadFieldData()
        {
            fieldDataActor.beginAction(new Action<IReportDetailedProgress>(downloadFieldDataWorker));
        }

        private void downloadFieldDataWorker(IReportDetailedProgress progess)
        {
            
            ObjectSyncList syncList = new ObjectSyncList();
            syncList.addList(SelectionBuilder.FinishedSelection);
            //this.setProgressInformation("Initializing");
            syncList.initialize(LookupSynchronizationInformation.getFieldDataList(), LookupSynchronizationInformation.getReflexiveReferences(), LookupSynchronizationInformation.getReflexiveIDFields());
            //this.setProgressValue(10);
            //this.setProgressInformation("Analyzing");
            AnalyzeSyncObjectList ansl = new AnalyzeSyncObjectList(syncList, ConnectionsAccess.RepositoryDB, ConnectionsAccess.MobileDB, ConnectionsAccess.Synchronization);
            ansl.analyzeAll();
            //this.setProgressInformation("Analysis Complete");
            //this.setProgressValue(20);
            //Alles außer InsertState auf ignore setzen
            List<ListContainer> conflicted;
            List<ListContainer> conflictResolved;
            List<ListContainer> synchronized;
            List<ListContainer> insert;
            List<ListContainer> update;
            List<ListContainer> ignore;
            List<ListContainer> delete;
            List<ListContainer> premature;
            conflicted = ansl.getObjectsOfState(SyncStates_Enum.ConflictState);
            conflictResolved = ansl.getObjectsOfState(SyncStates_Enum.ConflictResolvedState);
            synchronized = ansl.getObjectsOfState(SyncStates_Enum.SynchronizedState);
            insert = ansl.getObjectsOfState(SyncStates_Enum.InsertState);
            update = ansl.getObjectsOfState(SyncStates_Enum.UpdateState);
            ignore = ansl.getObjectsOfState(SyncStates_Enum.IgnoreState);
            delete = ansl.getObjectsOfState(SyncStates_Enum.DeletedState);
            premature = ansl.getObjectsOfState(SyncStates_Enum.PrematureState);

            //this.setProgressValue(30);
            //this.setProgressInformation("Warning: Only InsertState is allowed at the moment. All other states will be set to IgnoreState");
            System.Threading.Thread.Sleep(1000);
            //this.setProgressInformation("Synchronizing");

            foreach (ListContainer lc in conflicted)
            {
                lc.State = SyncStates_Enum.IgnoreState;
            }
            //this.setProgressValue(40);
            foreach (ListContainer lc in conflictResolved)
            {
                lc.State = SyncStates_Enum.IgnoreState;
            }
            //this.setProgressValue(50);
            foreach (ListContainer lc in synchronized)
            {
                lc.State = SyncStates_Enum.IgnoreState;//Exception throwen?
            }
            //this.setProgressValue(60);
            foreach (ListContainer lc in update)
            {
                lc.State = SyncStates_Enum.IgnoreState;
            }
            //this.setProgressValue(70);
            foreach (ListContainer lc in delete)
            {
                lc.State = SyncStates_Enum.IgnoreState;
            }
            //this.setProgressValue(80);
            foreach (ListContainer lc in premature)
            {
                lc.State = SyncStates_Enum.IgnoreState;//Exception throwen?
            }
            //this.setProgressValue(90);
            ansl.synchronizeAll();
            //this.setProgressInformation("Complete");
            //this.setProgressValue(100);
            //this.endMobileConnection(true);

            SyncStatus.Instance.Sync |= SyncStatus.SyncState.FieldDataDownload;
        }
        
        #endregion

    }
}
