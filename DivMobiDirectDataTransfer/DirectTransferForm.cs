using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.SyncBase;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;
using UBT.AI4.Bio.DivMobi.ListSynchronization;

namespace DivMobiDirectDataTransfer
{
    public partial class DirectTransferForm : Form
    {
        Serializer mobileDBSerializer;
        Serializer repositorySerializer;
        Serializer syncDBSerializer;
        IList<Type> divMobiTypes = new List<Type>();
        IList<Type> syncTypes = new List<Type>(); 

        public DirectTransferForm()
        {
            InitializeComponent();
            divMobiTypes.Add(typeof(Property));
            divMobiTypes.Add(typeof(Analysis));
            divMobiTypes.Add(typeof(AnalysisTaxonomicGroup));
            divMobiTypes.Add(typeof(CollectionAgent));
            divMobiTypes.Add(typeof(CollectionEvent));
            divMobiTypes.Add(typeof(CollectionEventImage));
            divMobiTypes.Add(typeof(CollectionEventLocalisation));
            divMobiTypes.Add(typeof(CollectionEventProperty));
            divMobiTypes.Add(typeof(CollectionSpecimen));
            divMobiTypes.Add(typeof(CollectionSpecimenImage));
            divMobiTypes.Add(typeof(CollEventImageType_Enum));
            divMobiTypes.Add(typeof(CollSpecimenImageType_Enum));
            divMobiTypes.Add(typeof(CollTaxonomicGroup_Enum));
            divMobiTypes.Add(typeof(Identification));
            divMobiTypes.Add(typeof(IdentificationUnit));
            divMobiTypes.Add(typeof(IdentificationUnitAnalysis));
            divMobiTypes.Add(typeof(LocalisationSystem));
            divMobiTypes.Add(typeof(CollCircumstances_Enum));
            divMobiTypes.Add(typeof(CollUnitRelationType_Enum));
            divMobiTypes.Add(typeof(CollectionEventSeries));
            //Bis hier: Korrepondiert zu DBVersion 20
            //divMobiTypes.Add(typeof(CollectionEventSeriesImage));
            //divMobiTypes.Add(typeof(CollEventSeriesImageType_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 22
            divMobiTypes.Add(typeof(CollIdentificationCategory_Enum));
            //divMobiTypes.Add(typeof(CollTypeStatus_Enum));
            //divMobiTypes.Add(typeof(CollIdentificationQualifier_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 25
            //divMobiTypes.Add(typeof(CollLabelTranscriptionState_Enum));
            //divMobiTypes.Add(typeof(CollLabelType_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 27
            //divMobiTypes.Add(typeof(Collection));
            divMobiTypes.Add(typeof(CollectionProject));
            //divMobiTypes.Add(typeof(CollectionSpecimenPart));
            //divMobiTypes.Add(typeof(CollMaterialCategory_Enum));
            //Bis hier: Korrepondiert zu DBVersion 31
            divMobiTypes.Add(typeof(IdentificationUnitGeoAnalysis));
            divMobiTypes.Add(typeof(AnalysisResult));
            divMobiTypes.Add(typeof(UserTaxonomicGroupTable));
            //Bis hier: Korrepondiert zu DBVersion 34
            syncTypes.Add(typeof(SyncItem));
        }

        private void connectMobileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dbConnectionDialog = new OpenFileDialog();
            dbConnectionDialog.Filter = "sdf files (*.sdf)|*.sdf"; //Im Moment wird nur das DB-Format von DiversityMobile unterstützt
            if (dbConnectionDialog.ShowDialog() == DialogResult.OK)
            {
                String mobileDBPath = @dbConnectionDialog.FileName;
                mobileDBSerializer = new MS_SqlCeSerializer(mobileDBPath);
                mobileDBSerializer.RegisterTypes(divMobiTypes);
                mobileDBSerializer.Activate();
                this.connectMobileButton.Text = "Connected to SDF";
            }
            else
                return;
        }

        private void connectRepoitoryButton_Click(object sender, EventArgs e)
        {
            repositorySerializer = new MS_SqlServerIPSerializer("Schneider", "TS@AI4-UB#7768","141.84.65.107","5432", "DiversityCollection_Monitoring", "[DiversityCollection_Monitoring].[dbo].");
            repositorySerializer.RegisterTypes(divMobiTypes);
            repositorySerializer.Activate();
            syncDBSerializer = new MS_SqlServerIPSerializer("Schneider", "TS@AI4-UB#7768", "141.84.65.107", "5432", "Synchronisation_Test", String.Empty);
            String syncIt = "[AGRambold2].SyncItem";
            MappingDictionary.Mapping.Add(typeof(SyncItem), syncIt);
            syncDBSerializer.RegisterType(typeof(SyncItem));
            syncDBSerializer.Activate();
            this.connectRepoitoryButton.Text="Connected to repository";
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
                ObjectSyncList syncList = new ObjectSyncList();
                List<Type> uploadTypes = new List<Type>();
                //uploadTypes.Add(typeof(Analysis));//ANalysen müssen aufgrund der referenzen gesondert behandelt werden.
                uploadTypes.Add(typeof(CollectionEventSeries));
                uploadTypes.Add(typeof(CollectionAgent));
                uploadTypes.Add(typeof(CollectionEvent));
                uploadTypes.Add(typeof(CollectionEventLocalisation));
                uploadTypes.Add(typeof(CollectionEventProperty));
                uploadTypes.Add(typeof(CollectionProject));
                uploadTypes.Add(typeof(CollectionSpecimen));
                uploadTypes.Add(typeof(Identification));
                uploadTypes.Add(typeof(IdentificationUnit));
                //uploadTypes.Add(typeof(IdentificationUnitAnalysis));
                //uploadTypes.Add(typeof(IdentificationUnitGeoAnalysis));               
                //Bis hier: Korrepondiert zu DBVersion 31
                foreach (Type t in uploadTypes)
                {
                    syncList.Load(t, mobileDBSerializer);
                }
                syncList.initialize(LookupSynchronizationInformation.getFieldDataList(), LookupSynchronizationInformation.getReflexiveReferences(), LookupSynchronizationInformation.getReflexiveIDFields());
                AnalyzeSyncObjectList ansl = new AnalyzeSyncObjectList(syncList, mobileDBSerializer, repositorySerializer, syncDBSerializer);
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
                MessageBox.Show("Warning: Only InsertState is allowed at the moment. All other states will be set to IgnoreState");
                System.Threading.Thread.Sleep(1000);
                MessageBox.Show("Inserting");

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
                MessageBox.Show("Finished");
            }

        private void buttonIdentifications_Click(object sender, EventArgs e)
        {
            IRestriction r=RestrictionFactory.TypeRestriction(typeof(IdentificationUnit));
            IList<IdentificationUnit> units = mobileDBSerializer.Connector.LoadList<IdentificationUnit>(r);
            foreach (IdentificationUnit iu in units)
            {
                Identification id = iu.Identifications.First();
                id.TaxonomicName = iu.LastIdentificationCache;
                mobileDBSerializer.Connector.Save(id);
            }
            MessageBox.Show("Success");
            
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            IRestriction rIds = RestrictionFactory.TypeRestriction(typeof(Identification));
            IRestriction rUnits = RestrictionFactory.TypeRestriction(typeof(IdentificationUnit));
            IList<Identification> idents_mob = mobileDBSerializer.Connector.LoadList<Identification>(rIds);
            IList<Identification> idents_rep = new List<Identification>();
            IList<IdentificationUnit> units_mob = mobileDBSerializer.Connector.LoadList<IdentificationUnit>(rUnits);
            IList<IdentificationUnit> units_rep = new List<IdentificationUnit>();

            foreach (Identification mobi in idents_mob)
            {
                IRestriction r = RestrictionFactory.Eq(typeof(Identification), "_guid", mobi.Rowguid);
                Identification partner = repositorySerializer.Connector.Load<Identification>(r);
                if (partner == null)
                    MessageBox.Show(mobi.Rowguid.ToString());
                else
                {
                    if(mobi.IdentificationDate!=partner.IdentificationDate)
                        idents_rep.Add(partner);
                }
            }
            //foreach (IdentificationUnit mobi in units_mob)
            //{
               
            //    IRestriction r = RestrictionFactory.Eq(typeof(IdentificationUnit), "_guid", mobi.Rowguid);
            //    IdentificationUnit partner = repositorySerializer.Connector.Load<IdentificationUnit>(r);
            //    if (partner == null)
            //        MessageBox.Show(mobi.Rowguid.ToString());
            //    else
            //    {
            //        units_rep.Add(partner);
            //    }
            //}
            MessageBox.Show("Success");
        }
     }      
}
