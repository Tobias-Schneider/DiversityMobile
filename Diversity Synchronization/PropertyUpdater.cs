using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using System.Data.Common;
using System.Data.SqlServerCe;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;
using log4net;


namespace Diversity_Synchronization
{
    public class PropertyUpdater
    {
        private ILog _Log = LogManager.GetLogger(typeof(PropertyUpdater));
        AsynchronousActor propertyActor = new AsynchronousActor();

        public IReportDetailedProgress Progress
        {
            get
            {
                return propertyActor.Progress;
            }
        }

        #region Download Properties

        IReportDetailedProgress progress;
        double progressPerProperty;
        public void updateProperties()
        {
            propertyActor.beginAction(new Action<IReportDetailedProgress>(updatePropertiesWorker));
        }
        private void updatePropertiesWorker(IReportDetailedProgress progress)
        {
            this.progress = progress;
            progressPerProperty = 50d;

            updateProperties("LebensraumTypen", "LebensraumTypenLfU");            

            updateProperties("Pflanzengesellschaften", "Pflanzengesellschaften");
            
            
        }

        //Überträgt die Namen von Properties vom Repository in die mobile Datenbank. Um doppelte Einträge zu vermeiden werden zunächst Einträge in der Mobilen Datenbank
        //gelöscht. Kann mit Reflexion verallgemeinert und mit updateTaxonNames kombiniert werden.
        public void updateProperties(string sourceTable, string targetTable)
        {
            if (MappingDictionary.Mapping.ContainsKey(typeof(PropertyNames)))
                MappingDictionary.Mapping[typeof(PropertyNames)] = sourceTable;
            else
                MappingDictionary.Mapping.Add(typeof(PropertyNames), sourceTable);
            //Neue Properties holen
            IList<PropertyNames> properties = new List<PropertyNames>();
            String praefix = ConnectionsAccess.RepositoryDefinitions.Praefix;
            //IRestriction r = RestrictionFactory.TypeRestriction(typeof(PropertyNames));
            //properties = taxonrepSerializer.Connector.LoadList<PropertyNames>(r);//geht nicht , weil auf der Sicht keine GUID definiert ist
            DbConnection connRepository = ConnectionsAccess.RepositoryDefinitions.CreateConnection();
            DbCommand select = connRepository.CreateCommand();
            DbCommand count = connRepository.CreateCommand();

            select.CommandText = String.Format("SELECT * FROM {0}", praefix+sourceTable);
            count.CommandText = String.Format("SELECT COUNT(*) FROM {0}", praefix+sourceTable);
            DbDataReader reader = null;
            IAdvanceProgress localProgress = progress.startExternal(progressPerProperty / 2);
            int propertyCount = 0;
            try
            {
                connRepository.Open();
                propertyCount  = (int)count.ExecuteScalar();
                localProgress.InternalTotal = propertyCount;

                reader = select.ExecuteReader();

                while (reader.Read())
                {
                    PropertyNames prop = new PropertyNames();
                    prop.PropertyID = reader.GetInt32(0);
                    if (!reader.IsDBNull(1))
                        prop.PropertyURI = reader.GetString(1);
                    if (!reader.IsDBNull(2))
                        prop.DisplayText = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        prop.HierarchyCache = reader.GetString(3);
                    //primary key
                    prop.TermID = reader.GetInt32(4);
                    if (!reader.IsDBNull(5))
                        prop.BroaderTermID = reader.GetInt32(5);
                    properties.Add(prop);

                    localProgress.advance();
                }
            }
            catch (Exception e)
            {
                _Log.ErrorFormat("Exception reading Properties: [{0}]",e);
                return;
            }
            finally
            {
                connRepository.Close();
            }

            DbConnection connMobile = ConnectionsAccess.MobileTaxa.CreateConnection();
            connMobile.Open();
            SqlCeTransaction trans = null;
            try
            {
                trans = (SqlCeTransaction)connMobile.BeginTransaction();
                localProgress = progress.startInternal(progressPerProperty / 2, propertyCount);
                
                    
                //Alte Taxa löschen


                DbCommand commandMobile = connMobile.CreateCommand();
                commandMobile.CommandText = String.Format("DELETE FROM {0}", targetTable);
                commandMobile.ExecuteNonQuery();

                //Taxa eintragen            



                foreach (PropertyNames prop in properties)
                {
                    var sb = new StringBuilder(); //Alternativ mobileDBSerializer.Connector.Save(taxon)
                    sb.Append("Insert INTO ").Append(targetTable).Append(" (PropertyID,PropertyURI,DisplayText,HierarchyCache,TermID,BroaderTermID) VALUES (");
                    sb.Append(SqlUtil.SqlConvert(prop.PropertyID)).Append(",");
                    sb.Append(SqlUtil.SqlConvert(prop.PropertyURI)).Append(",").Append(SqlUtil.SqlConvert(prop.DisplayText)).Append(",").Append(SqlUtil.SqlConvert(prop.HierarchyCache)).Append(",").Append(prop.TermID).Append(",").Append(prop.BroaderTermID).Append(")");
                    DbCommand insert = connMobile.CreateCommand();
                    insert.CommandText = @sb.ToString();
                    insert.ExecuteNonQuery();

                    localProgress.advance();
                }
                trans.Commit();
            }
            catch (Exception)
            {
                if (trans != null)
                    trans.Rollback();
                return;
            }
            finally
            {
                connMobile.Close();
            }
        }
        #endregion
    }        
}
