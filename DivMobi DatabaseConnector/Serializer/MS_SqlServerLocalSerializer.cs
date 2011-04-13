
using System.Data.Common;
using System;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public class MS_SqlServerLocalSerializer : Serializer
    {
        private String _catalog;
        private String _IP;
        private String _Port;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlCeSerializer"/> class.
        /// </summary>
        public MS_SqlServerLocalSerializer(String ip, String port, String catalog)
            : base()
        {
            this._catalog = catalog;
            this._IP = ip;
            this._Port = port;
        }

        public override DbConnection CreateConnection()
        {

            string connection_string = "Data Source="+ this._IP+","+this._Port+";Initial Catalog="+this._catalog+";Integrated Security=True";
            Object conn = new System.Data.SqlClient.SqlConnection(connection_string);
            return (DbConnection)conn;

        }
    }
}
