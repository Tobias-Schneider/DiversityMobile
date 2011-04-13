using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.SqlServerCe;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;
using System.ComponentModel;
using log4net;

namespace Diversity_Synchronization
{
    public class TaxonDownloader
    {
        private ILog log = log4net.LogManager.GetLogger(typeof(TaxonDownloader));
        private string taxonListsForUser; 
        private readonly string taxonListsExpression;
        private ObservableCollection<TaxonList> taxonLists;
        private AsynchronousActor taxonActor = new AsynchronousActor();

        private IDbConnection source;
        private SqlCeConnection destination;
        private string destinationTable;
        private string sourceExpression;
        private IReportDetailedProgress progress;

        float progressPerTaxonList = 100f;

        public TaxonDownloader()
        {
            taxonListsForUser = "[" + OptionsAccess.RepositoryOptions.TaxonNamesInitialCatalog + "].[dbo].[TaxonListsForUser]";
            taxonListsExpression = String.Format("{0}('{1}')", taxonListsForUser, OptionsAccess.RepositoryOptions.LastUsername);      
            fetchTaxonLists();

            selectedTaxa = from taxonList in TaxonLists where taxonList.IsSelected select taxonList;
        }

        

       

        #region Taxon Selection
        public class TaxonList
        {
            public string DataSource { get; set; }
            public string DisplayText { get; set; }
            public string TaxonomicGroup { get; set; }
            public bool IsSelected { get; set; }
        }

        public ObservableCollection<TaxonList> TaxonLists
        {
            get
            {
                if (taxonLists == null)
                    taxonLists = new ObservableCollection<TaxonList>();
                return taxonLists;
            }
        }

        public ProgressReporter ProgressNotification
        {
            get
            {
                return taxonActor.Progress;
            }
        }
        #endregion 

        public void copyTaxonData()
        {
            taxonActor.beginAction(new Action<IReportDetailedProgress>(copyTaxonDataWorker));
        }

        private void fetchTaxonLists()
        {
            TaxonLists.Clear();
            string sql = String.Format("SELECT * FROM {0};", taxonListsExpression);
            var connRep = ConnectionsAccess.RepositoryDefinitions.CreateConnection();
            var cmd = connRep.CreateCommand();
            cmd.CommandText = sql;
            try
            {
                connRep.Open();
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TaxonList tl = new TaxonList();
                    tl.DataSource = rdr["Datasource"].ToString();
                    tl.DisplayText = rdr["DisplayText"].ToString();
                    tl.IsSelected = false;
                    tl.TaxonomicGroup = rdr["TaxonomicGroup"].ToString();
                    TaxonLists.Add(tl);
                }

            }
            catch (Exception e)
            {
                //TODO Error

            }
            finally
            {
                connRep.Close();
                connRep.Dispose();
            }

        }
        private const string TAXON_LISTS_TABLE = "TaxonListsForUser";
        private const string TAXONLIST_DATASOURCE_COLUMN = "DataSource";
        private const string TAXONLIST_NAME_COLUMN = "DisplayText";
        private const string TAXONLIST_GROUP_COLUMN = "TaxonomicGroup";

        private IEnumerable<TaxonList> selectedTaxa;
        private void copyTaxonDataWorker(IReportDetailedProgress progress)
        {
            this.progress = progress;

            if(ConnectionsAccess.Instance.State.CheckForFlags(ConnectionsAccess.ConnectionState.TaxonConnected))
            {
                //Downloading Taxa:
                progress.ProgressDescriptionID = 2031;                             
                
                try
                {
                    openConnections();

                    downloadTaxonLists();

                    fillTaxonListsForUser();                   

                    updateSelectedTaxonLists();

                    SyncStatus.Instance.Sync |= SyncStatus.SyncState.TaxonCatalogs;
                }
                catch (Exception e)
                {
                    log.ErrorFormat("Error while downloading Taxon Lists:\n{0}", e.Message);
                }
                finally
                {
                    closeConnections();
                }


            }        
        }

        private void closeConnections()
        {
            source.Close();
            destination.Close();
        }

        private void openConnections()
        {
            source = ConnectionsAccess.RepositoryDefinitions.CreateConnection();
            destination = (SqlCeConnection)ConnectionsAccess.MobileTaxa.CreateConnection();
            source.Open();
            destination.Open();
        }

        private void downloadTaxonLists()
        {
            System.Threading.Thread.Sleep(500);
            progressPerTaxonList = 100 / selectedTaxa.Count();
            

            foreach (var taxonList in selectedTaxa)
            {
                progress.ProgressOutput = taxonList.DisplayText;
                progress.IsProgressIndeterminate = true;

                copyTable(taxonList.DataSource);                
            }
        }

        private void updateSelectedTaxonLists()
        {
            var firstListOfEachGroup = from list in selectedTaxa
                                       group list by list.TaxonomicGroup into g
                                       select g.First();

            using (var mobileConn = ConnectionsAccess.MobileDB.CreateConnection())
            {
                using (var update = mobileConn.CreateCommand())
                {
                    mobileConn.Open();
                    try
                    {
                        foreach (var list in firstListOfEachGroup)
                        {
                            update.CommandText = String.Format("UPDATE [UserTaxonomicGroupTable] SET [TaxonomicTable] = '{0}' WHERE [TaxonomicCode] = '{1}';", list.DataSource, list.TaxonomicGroup);
                            int rowsAffected = update.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                update.CommandText = String.Format("INSERT INTO [UserTaxonomicGroupTable] ([TaxonomicCode], [TaxonomicTable]) VALUES ('{0}','{1}');", list.TaxonomicGroup, list.DataSource);
                                update.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.ErrorFormat("Error while updating Selected Taxon Lists:\n{0}", ex.Message);
                    }
                    finally
                    {
                        mobileConn.Close();
                    }
                }
            }
        }

        private void fillTaxonListsForUser()
        {
            try
            {
                using (var count = destination.CreateCommand())
                {
                    using (var insert = destination.CreateCommand())
                    {
                        foreach (var list in selectedTaxa)
                        {
                            count.CommandText = String.Format("SELECT COUNT(*) FROM {0} WHERE [DataSource] = '{1}'", TAXON_LISTS_TABLE, list.DataSource);
                            bool exists = ((int)count.ExecuteScalar()) != 0;
                            if (!exists)
                            {
                                insert.CommandText = String.Format("INSERT INTO {0} ([{1}],[{2}],[{3}]) VALUES ('{4}','{5}','{6}')",
                                    TAXON_LISTS_TABLE, TAXONLIST_DATASOURCE_COLUMN, TAXONLIST_NAME_COLUMN, TAXONLIST_GROUP_COLUMN,
                                    list.DataSource, list.DisplayText, list.TaxonomicGroup);
                                insert.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error while filling TaxonListsForUser:\n{0}", ex.Message);
            }
        }

        
        private void copyTable(string table)
        {
            sourceExpression = "["+OptionsAccess.RepositoryOptions.TaxonNamesInitialCatalog+"].[dbo].[" + table + "]";
            destinationTable = table;
            copyDBExpressionToTable();
        }
        

        private void copyDBExpressionToTable()
        {

            dropDestinationTable();
            
            using (var cmd = destination.CreateCommand())
            {
                cmd.CommandText = destinationTable;
                cmd.CommandType = CommandType.TableDirect;

                
                int rowCount = countRowsToCopy();
                

                IDbCommand readCmd = source.CreateCommand();
                readCmd.CommandText = "SELECT * FROM " + sourceExpression;
                using (var sourceReader = readCmd.ExecuteReader())
                {

                    createDestinationTable(sourceReader.GetSchemaTable());

                    var internalProgress = progress.startInternal(progressPerTaxonList, rowCount);

                    using (var destinationResultSet = cmd.ExecuteResultSet(ResultSetOptions.Updatable | ResultSetOptions.Scrollable))
                    {
                        while (sourceReader.Read())
                        {
                            SqlCeUpdatableRecord record = destinationResultSet.CreateRecord();
                            object[] values = new object[sourceReader.FieldCount];
                            sourceReader.GetValues(values);
                            record.SetValues(values);
                            destinationResultSet.Insert(record);
                            
                            internalProgress.advance();                                                     
                        }
                    }
                    sourceReader.Close();
                }
            }
            return;
        }

        private int countRowsToCopy()
        {
            using (IDbCommand readCmd = source.CreateCommand())
            {
                readCmd.CommandText = "SELECT COUNT(*) FROM " + sourceExpression;
                return (int)readCmd.ExecuteScalar();
            }
        }

        private void createDestinationTable(DataTable schema)
        {
            using (var create = destination.CreateCommand())
            {
                create.CommandText = GetCreateTableStatement(destinationTable, schema);
                create.ExecuteNonQuery();
            }
        }

        private void dropDestinationTable()
        {
            bool tableExists;
            using (var existenceCheckCmd = destination.CreateCommand())
            {
                existenceCheckCmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + destinationTable + "'";
                tableExists = (Int32)existenceCheckCmd.ExecuteScalar() > 0;                
            }
            if (tableExists)
            {
                using (var drop = destination.CreateCommand())
                {
                    drop.CommandText = "DROP TABLE " + destinationTable;
                    drop.ExecuteNonQuery();           
                }
            }
        }

         /// <summary>
        /// Genenerates a SQL CE compatible CREATE TABLE statement based on a schema obtained from
        /// a SqlDataReader or a SqlCeDataReader.
        /// </summary>
        /// <param name="tableName">The name of the table to be created.</param>
        /// <param name="schema">The schema returned from reader.GetSchemaTable().</param>
        /// <returns>The CREATE TABLE... Statement for the given schema.</returns>
        public static string GetCreateTableStatement(string tableName, DataTable schema)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("CREATE TABLE [{0}] (", tableName));

            foreach (DataRow row in schema.Rows)
            {
                string typeName = row["DataType"].ToString();
                Type type = Type.GetType(typeName);

                string name = (string)row["ColumnName"];
                int size = (int)row["ColumnSize"];

                SqlDbType dbType = GetSqlDBTypeFromType(type);
                string sqlceType = GetSqlServerCETypeName(dbType,size);

                builder.Append(String.Format("[{0}] {1}, ", name, sqlceType));
            }

            if (schema.Rows.Count > 0) builder.Length = builder.Length - 2;

            builder.Append(");");
            return builder.ToString();
        }
       

        /// <summary>
        /// Gets the correct SqlDBType for a given .NET type. Useful for working with SQL CE.
        /// </summary>
        /// <param name="type">The .Net Type used to find the SqlDBType.</param>
        /// <returns>The correct SqlDbType for the .Net type passed in.</returns>
        public static SqlDbType GetSqlDBTypeFromType(Type type)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(DbType));
           
                DbType dbType = (DbType)tc.ConvertFrom(type.Name);
                // A cheat, but the parameter class knows how to map between DbType and SqlDBType.
                SqlParameter param = new SqlParameter();
                param.DbType = dbType;
                return param.SqlDbType; // The parameter class did the conversion for us!!
           
        }

        /// <summary>
        /// The method gets the SQL CE type name for use in SQL Statements such as CREATE TABLE
        /// </summary>
        /// <param name="dbType">The SqlDbType to get the type name for</param>
        /// <param name="size">The size where applicable e.g. to create a nchar(n) type where n is the size passed in.</param>
        /// <returns>The SQL CE compatible type for use in SQL Statements</returns>
        public static string GetSqlServerCETypeName(SqlDbType dbType, int size)
        {
            // Conversions according to: http://msdn.microsoft.com/en-us/library/ms173018.aspx
            bool max = (size == int.MaxValue) ? true : false;
            bool over4k = (size > 4000) ? true : false;
            switch (dbType)
            {
	            case SqlDbType.BigInt:
                    return "bigint";
                 break;
                case SqlDbType.Binary:
                    return String.Format("binary({0})",size);
                 break;
                case SqlDbType.Bit:
                    return "bit";
                 break;
                case SqlDbType.Char:
                    return (over4k) ? "ntext" : String.Format("nchar({0})",size);
                 break;
                case SqlDbType.Date:
                    return "nchar(10)";
                 break;
                case SqlDbType.DateTime:
                    return "datetime";
                 break;
                case SqlDbType.DateTime2:
                    return "nvarchar(27)";
                 break;
                case SqlDbType.DateTimeOffset:
                    return "nvarchar(34)";
                 break;
                case SqlDbType.Decimal:
                 break;
                case SqlDbType.Float:
                    return "float";
                 break;
                case SqlDbType.Image:
                    return "image";
                 break;
                case SqlDbType.Int:
                    return "int";
                 break;
                case SqlDbType.Money:
                    return "money";
                 break;
                case SqlDbType.NChar:
                    return String.Format("nchar({0})",size);
                 break;
                case SqlDbType.NText:
                    return "ntext";
                 break;
                case SqlDbType.NVarChar:
                    return String.Format("nvarchar({0})",size);
                 break;
                case SqlDbType.Real:
                    return "real";
                 break;
                case SqlDbType.SmallDateTime:
                    return "datetime";
                 break;
                case SqlDbType.SmallInt:
                    return "smallint";
                 break;
                case SqlDbType.SmallMoney:
                    return "money";
                 break;
                case SqlDbType.Structured:
                 break;
                case SqlDbType.Text:
                    return "ntext";
                 break;
                case SqlDbType.Time:
                    return "nvarchar(16)";
                 break;
                case SqlDbType.Timestamp:
                 break;
                case SqlDbType.TinyInt:
                    return "tinyint";
                 break;
                case SqlDbType.Udt:
                 break;
                case SqlDbType.UniqueIdentifier:
                 break;
                case SqlDbType.VarBinary:
                 break;
                case SqlDbType.VarChar:
                 break;
                case SqlDbType.Variant:
                 break;
                case SqlDbType.Xml:
                    return "ntext";
                 break;
                default:
                 break;
            }
            throw new ArgumentException (String.Format("Could Not Convert SQL DataType {0} to SQLCE DataType",dbType.ToString()));
        }


        


    }
}
