﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using System.Reflection;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public class MS_SqlServerWASerializier : Serializer
    {

        //Diese Klasse ermöglicht Zugriff auf Datenbanken, die mit  der Einstellung Windows Authentication arbeiten
        private String _url;
        private String _catalog;


        public MS_SqlServerWASerializier(String url, String catalog, String praefix)
        {
            _url = url;
            _catalog = catalog;
            _praefix = praefix;
        }

        public override DbConnection CreateConnection()
        {
            StringBuilder connectionString = new StringBuilder();

            connectionString.Append("Data Source=").Append(_url).Append(";");
            connectionString.Append("Initial Catalog=").Append(_catalog).Append(";");
            connectionString.Append("Persist Security Info=True;");
            connectionString.Append("Integrated Security=SSPI;");


            //Aus unbekannten Gründen is ein direkter oder impliziter Cast
            //con SqlConnection nach DbConnection im CompactFramework nicht
            //möglich, daher der Umweg über Object
            Object conn = new System.Data.SqlClient.SqlConnection(connectionString.ToString());

            return (DbConnection)conn;
        }

        #region sqlstring generator
        private const String SELECT = "SELECT * FROM ";
        private const String COUNT = "SELECT COUNT(*) FROM ";
        private const String WHERE = " WHERE ";

        //Erzeugt eine SQL Anfrage in der der Tabellenname übergeben wird
        //ACHTUNG: Praefix_Handling Verbessern. Insbesondere die mehrfachen Wege zum Erreichen von Tabellen korrigieren.
        #endregion

    }
}
