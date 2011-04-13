using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public enum UpdateStates
    {
        UPDATED, NO_ROWS_AFFECTED, PRIMARYKEY_MODIFIED
    }

    public interface ISerializableObjectPool : IObeserver
    {
        ISerializableObject RetrieveObject(Type objectType, DbDataReader reader, ref bool isLoaded);
        void InsertObject(ISerializableObject iso, DbConnection connection, ISerializerTransaction transaction);
        void InsertObject(ISerializableObject iso, DbConnection connection, ISerializerTransaction transaction,string table);
        UpdateStates UpdateObject(ISerializableObject iso, DbConnection connection, ISerializerTransaction transaction);
        void DeleteObject(ISerializableObject iso, DbConnection connection, ISerializerTransaction transaction);
        bool IsManagedObject(ISerializableObject iso);
        void Register(ISerializableObject iso);
    }
}
