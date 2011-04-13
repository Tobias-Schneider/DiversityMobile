using System;
using System.Collections.Generic;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable
{
    public interface ISerializableObject
    {
        Guid Rowguid
        {
            get;
            set;
        }

        DateTime LogTime
        {
            get;
            set;
        }

        //String toSql
        //{
        //    get;
        //    set;
        //}
       
    }
}
