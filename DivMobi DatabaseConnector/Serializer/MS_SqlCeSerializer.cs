using System.Data.SqlServerCe;
using System.Data.Common;
using System;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public class MS_SqlCeSerializer : Serializer
    {
        private string _dbPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCeSerializer"/> class.
        /// </summary>
        public MS_SqlCeSerializer(string dbPath) : base()
        {
            _dbPath = dbPath;
        }

        public override DbConnection CreateConnection()
        {
            
            string connection_string = "Data Source=\"" + _dbPath + "\";Max Database Size=128;Default Lock Escalation=100;";
            SqlCeConnection c=null;
            try
            {
                c = new SqlCeConnection(connection_string);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return c;

        }
    }
}
