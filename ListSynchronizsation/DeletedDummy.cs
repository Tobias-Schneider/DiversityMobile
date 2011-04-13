using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;

namespace UBT.AI4.Bio.DivMobi.ListSynchronization
{
    public class DeletedDummy : ISerializableObject
    {
        private Guid _guid;
        private String _info;

        internal DeletedDummy()
        {
            _guid = Guid.Empty;
            _info = "Dies ist ein Plazhalter für ein gelöschtes Objekt.";
        }

        public String Info{get{return _info;}}
        public Guid Rowguid { get { return _guid; } }
    }
}
