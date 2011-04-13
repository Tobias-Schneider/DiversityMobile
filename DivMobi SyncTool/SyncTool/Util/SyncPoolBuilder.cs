using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;

namespace UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.Util
{
    public class FreeSyncPoolBuilder
    {
        private SyncPool _pool;
        private Serializer _syncSerializer;
        private Serializer _serializer;

        public FreeSyncPoolBuilder(Serializer serializer, Serializer syncSerializer)
        {
            this._pool = new SyncPool(serializer, syncSerializer);
            this._serializer = serializer;//Übergebene Serializer für Ladevorgänge merken
            this._syncSerializer = syncSerializer;
        }

        public void Load(Type t, IRestriction r)
        {
            
            IList<ISerializableObject> list = this._serializer.Connector.LoadList(t, r);
            foreach (ISerializableObject iso in list)
            {
                this._pool.AddISerializableObject(iso);
            }
        }

        public void Load(Type t)
        {
            IRestriction r = RestrictionFactory.TypeRestriction(t);

            IList<ISerializableObject> list = this._serializer.Connector.LoadList(t, r);
            foreach (ISerializableObject iso in list)
            {
                this._pool.AddISerializableObject(iso);
            }
        }

        //So kann nicht geprüft werden, ob die Objekte über denselben Serializer geladen wurden.
        public void Add(IList<ISerializableObject> list)
        {
            foreach (ISerializableObject iso in list)
            {
                this._pool.AddISerializableObject(iso);
            }
        }

        public void Add(ISerializableObject iso)
        {
            this._pool.AddISerializableObject(iso);
        }

        public SyncPool buildPoolFree()
        {
            this._pool.Intialize();
            return this._pool;
        }

        
    }
}
