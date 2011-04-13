using System;
using System.Collections.Generic;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class IdentSave: ISerializableObject
    {
        [ID]
        [Column]
        private String _LastIdentificationCache;

        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        private DateTime _LogUpdatedWhen;

        public Guid Rowguid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public String LastIdentificationCache
        {
            get { return _LastIdentificationCache; }
            set { _LastIdentificationCache = value; }
        }

        public DateTime LogTime { get { return new DateTime(1900, 1, 1); } set { } }
    }
}