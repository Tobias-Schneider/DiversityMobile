using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.ListSynchronization;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using System.Data;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using log4net;

namespace Diversity_Synchronization
{
    public class DefinitionLoader
    {
        private ILog _Log = LogManager.GetLogger(typeof(DefinitionLoader));
        private AsynchronousActor _defActor = new AsynchronousActor();
        private List<Type> _defTypes = new List<Type>()
        {
            typeof(Property), 
            typeof(CollEventImageType_Enum),
            typeof(CollSpecimenImageType_Enum), 
            typeof(CollTaxonomicGroup_Enum), 
            typeof(LocalisationSystem), 
            typeof(CollCircumstances_Enum), 
            typeof(CollUnitRelationType_Enum),             
            typeof(CollIdentificationCategory_Enum),
        };

        public ProgressReporter Progress
        {
            get
            {
                return _defActor.Progress;
            }
        }

        public void loadCollectionDefinitions()
        {
            _defActor.beginAction(new Action<IReportDetailedProgress>(loadCollectionDefinitionsWorker));
        }

        private void loadCollectionDefinitionsWorker(IReportDetailedProgress progress)
        {           
            ObjectSyncList transferList = new ObjectSyncList();
            progress.advanceProgress(5);
            String sql = null;
            IList<ISerializableObject> list = null;
            progress.ProgressDescriptionID = 1160;
            try
            {
                sql = @"SELECT * FROM [" + OptionsAccess.RepositoryOptions.InitialCatalog + "].[dbo].[AnalysisProjectList] (" + ConnectionsAccess.Profile.ProjectID + ")";
                list = ConnectionsAccess.RepositoryDB.Connector.LoadList(typeof(Analysis), sql);
                transferList.addList(list);
                foreach (ISerializableObject iso in list)
                {
                    Analysis ana = (Analysis)iso;
                    IRestriction rana = RestrictionFactory.Eq(typeof(AnalysisResult), "_AnalysisID", ana.AnalysisID);
                    IList<ISerializableObject> resultList = ConnectionsAccess.RepositoryDB.Connector.LoadList(typeof(AnalysisResult), rana);
                    transferList.addList(resultList);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                _Log.ErrorFormat("Exception while updating AnalysisTaxonomicGroups: {0}", e); ;
            }


            sql = @"SELECT AnalysisID,TaxonomicGroup,RowGUID FROM [" + OptionsAccess.RepositoryOptions.InitialCatalog + "].[dbo].[AnalysisTaxonomicGroupForProject] (" + ConnectionsAccess.Profile.ProjectID + ")";
            IList<AnalysisTaxonomicGroup> atgList = new List<AnalysisTaxonomicGroup>();
            IDbConnection connRepository = ConnectionsAccess.RepositoryDB.CreateConnection();
            connRepository.Open();
            IDbCommand com = connRepository.CreateCommand();
            com.CommandText = sql;
            IDataReader reader = null;
            try
            {
                reader = com.ExecuteReader();
                while (reader.Read())
                {
                    AnalysisTaxonomicGroup atg = new AnalysisTaxonomicGroup();
                    atg.AnalysisID = reader.GetInt32(0);
                    atg.TaxonomicGroup = reader.GetString(1);
                    atg.Rowguid = Guid.NewGuid();
                    atgList.Add(atg);
                }

                connRepository.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                _Log.ErrorFormat("Exception while updating AnalysisTaxonomicGroups: {0}", e); ;
                connRepository.Close();
            }


            foreach (AnalysisTaxonomicGroup atg in atgList)
            {
                foreach (ISerializableObject iso in list)
                {
                    if (iso.GetType().Equals(typeof(Analysis)))
                    {
                        Analysis ana = (Analysis)iso;
                        if (ana.AnalysisID == atg.AnalysisID)
                        {
                            transferList.addObject(atg);
                        }
                    }
                }
            }


            double progressPerType = 90d / _defTypes.Count;
            foreach (Type t in _defTypes)
            {                
                transferList.Load(t, ConnectionsAccess.RepositoryDB);
                progress.advanceProgress(progressPerType);
            }
            transferList.initialize(LookupSynchronizationInformation.downloadDefinitionsList(), LookupSynchronizationInformation.getReflexiveReferences(), LookupSynchronizationInformation.getReflexiveIDFields());
                        
            
            List<ISerializableObject> orderedObjects = transferList.orderedObjects;
            progress.ProgressDescriptionID = 1161;            
            foreach (ISerializableObject iso in orderedObjects)
            {
                try
                {
                    ConnectionsAccess.MobileDB.Connector.InsertPlain(iso);
                }
                catch (Exception)
                {
                    try
                    {
                        if (iso.GetType().Equals(typeof(AnalysisTaxonomicGroup)))
                        {
                            AnalysisTaxonomicGroup atg = (AnalysisTaxonomicGroup)iso;
                            IRestriction r1 = RestrictionFactory.Eq(iso.GetType(), "_AnalysisID", atg.AnalysisID);
                            IRestriction r2 = RestrictionFactory.Eq(iso.GetType(), "_TaxonomicGroup", atg.TaxonomicGroup);
                            IRestriction r = RestrictionFactory.And().Add(r1).Add(r2);
                            ISerializableObject isoStored = ConnectionsAccess.MobileDB.Connector.Load(iso.GetType(), r);
                            atg.Rowguid = isoStored.Rowguid;
                        }
                        else
                        {
                            IRestriction r = RestrictionFactory.Eq(iso.GetType(), "_guid", iso.Rowguid);
                            ISerializableObject isoStored = ConnectionsAccess.MobileDB.Connector.Load(iso.GetType(), r);
                        }
                        ConnectionsAccess.MobileDB.Connector.UpdatePlain(iso);
                    }
                    catch (Exception ex)
                    {
                        _Log.ErrorFormat("Exception while transferring [{0}]: [{1}]",iso,ex);
                    }
                }                
            }
        }
        
    }
}
