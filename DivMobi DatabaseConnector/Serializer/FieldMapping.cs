using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public static class FieldMapping
    {
        public static Dictionary<String, Type> Mapping;
        static FieldMapping()
        {
            Mapping = new Dictionary<string, Type>();
            Mapping.Add("Microsoft.SqlServer.Types.SqlGeography", typeof(String));
        }

        public static String Convert(Object input)
        {
            return input.ToString();
        }
    }
}
