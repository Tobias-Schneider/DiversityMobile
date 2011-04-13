using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;

namespace UBT.AI4.Bio.DivMobi.SyncTool.SyncTool
{
    public class SyncTool
    {
        private Serializer _serializer;
        private Serializer _syncSerializer;
        private IList<Type> _registeredTypes;


        public SyncTool(Serializer serializer, Serializer syncSerializer, IList<Type> registeredTypes)
        {
            _serializer = serializer;
            _registeredTypes = registeredTypes;
            _syncSerializer = syncSerializer;
        }

        //Problem: Wenn ncht alle Fremdschlüssel in registered Types liegen, werden hier Null-Werte eingatreagen.
        //Dies tritt auf, wenn nicht alle DataItems die beim Serialiszer registriert sind, synchronisiert werden sollen.
        public SyncPool CreateSyncPool()
        {
            SyncPool pool = new SyncPool(_serializer, _syncSerializer);
            foreach (Type t in _registeredTypes)
            {
                IRestriction r = RestrictionFactory.TypeRestriction(t);
                IList<ISerializableObject> list = _serializer.Connector.LoadList(t, r);
                foreach (ISerializableObject iso in list)
                {
                    pool.AddISerializableObject(iso);
                }
            }
            pool.Intialize();
            return pool;
        }

        public SyncPool CreateSyncPool(IRestriction r)
        {
            SyncPool pool = new SyncPool(_serializer, _syncSerializer);
            foreach (Type t in _registeredTypes)
            {
                IList<ISerializableObject> list = _serializer.Connector.LoadList(t, r);
                foreach (ISerializableObject iso in list)
                {
                    pool.AddISerializableObject(iso);
                }
            }
            pool.Intialize();
            return pool;
        }

        public void Analyze(SyncPool syncPool)
        {
            syncPool.StartAnalyzing(this);
        }

        public void Synchronize(SyncPool syncPool)
        {
            syncPool.StartSynchronizing(this);
        }

        public Serializer SyncSerializer
        {
            get { return _syncSerializer; }
        }

        public Serializer Serializer
        {
            get { return _serializer; }
        }
    }
}
