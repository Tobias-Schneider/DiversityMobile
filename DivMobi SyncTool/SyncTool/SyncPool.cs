using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;

namespace UBT.AI4.Bio.DivMobi.SyncTool.SyncTool
{
    public class SynchronizeEventArgs : EventArgs
    {
        //refine
    }

    public delegate void SynchronizeEventHandler(SyncPool sender, SynchronizeEventArgs args);

    public class SyncPool
    {
        private Dictionary<Guid, SyncContainer> _pooledObjects;
        private Serializer _syncSerializer;
        private Serializer _serializer;
        private bool _initialized = false;

        public SyncPool(Serializer serializer, Serializer syncSerializer)
        {
            _serializer = serializer;
            _syncSerializer = syncSerializer;
            _pooledObjects = new Dictionary<Guid, SyncContainer>();
        }

        internal void Intialize()
        {
            foreach (SyncContainer c in _pooledObjects.Values)
            {
                c.InitSyncContainer(_pooledObjects);
            }
            _initialized = true;
        }

        //Vereinigung zweier SyncPools, diese müssen diesselben Serializer besitzen und initialisiert sein.
        internal SyncPool union(SyncPool s1, SyncPool s2)
        {
            //Kompatibilität testen
            try
            {
                if ((s1._serializer != s2._serializer) || (s1._syncSerializer != s2._syncSerializer))
                {
                    Exception ex=new Exception();
                    throw ex;
                }
                if (s1._initialized == false || s2._initialized == false)
                {

                    Exception ex = new Exception();
                    throw ex;
                }
            }
            catch (Exception ex){
                //Unterschiedliche Serializer, Vereinigung nicht möglich
                return null;
            }
            SyncPool s = new SyncPool(s1._serializer, s1._syncSerializer);
            s._pooledObjects = new Dictionary<Guid, SyncContainer>(s1._pooledObjects);//Performanter als kopieren

            foreach (KeyValuePair<Guid, SyncContainer> kv in s2._pooledObjects)
            {
                if (s._pooledObjects.Contains(kv) == false)
                    s._pooledObjects.Add(kv.Key,kv.Value);
            }
            //Initialisierung notwendig? Immerhin besteht der neue Syncpool nur aus initialisierten Synccontainern
            //Ist abhängig vom RelationResolver. Das heißt nur wenn neue Beziehungen aufgelöst werden müssen.
            s.Intialize();
            return s;
        }

        internal void AddISerializableObject(ISerializableObject iso)
        {
            if (!_pooledObjects.ContainsKey(AttributeWorker.RowGuid(iso)))
            {
                SyncContainer container = new SyncContainer(iso, _syncSerializer, _serializer);
                _pooledObjects[AttributeWorker.RowGuid(iso)] = container;
            }
        }

        internal void StartAnalyzing(SyncTool syncTool)
        {
            if (!_initialized)
            {
                throw new SyncException("Syncpool is not initialized!");
            }
            if (!CheckSyncContext(syncTool))
            {
                throw new SyncException("SyncPool is not compatible with SyncPeer! Both of them use a different SyncSerializer!");
            }
            if (!CheckTarget(syncTool))
            {
                throw new SyncException("Synchronising form source to source is not valid!");
            }

            foreach (SyncContainer c in _pooledObjects.Values)
            {
                c.TargetSerializer = syncTool.Serializer;
                c.Analyze();
            }
        }

        public event SynchronizeEventHandler SynchronizeEvent;
        protected virtual bool OnSynchronizeEvent(SynchronizeEventArgs args)
        {
            if (SynchronizeEvent != null)
            {
                SynchronizeEvent.Invoke(this, args);
                return true;
            }
            return false;
        }
        internal void StartSynchronizing(SyncTool syncPeer) 
        {
            if (!_initialized)
            {
                throw new SyncException("Syncpool is not initialized!");
            }
            if (!CheckSyncContext(syncPeer))
            {
                throw new SyncException("SyncPool is not compatible with SyncPeer!");
            }
            if (!CheckTarget(syncPeer))
            {
                throw new SyncException("Synchronising form source to source is not valid!");
            }

            _syncSerializer.Connector.BeginTransaction();
            syncPeer.Serializer.Connector.BeginTransaction();
            SyncContainer lastContainer=null;
            try
            {
                foreach (SyncContainer c in _pooledObjects.Values)
                {
                    lastContainer = c;
                    c.Synchronize();
                }

                _syncSerializer.Connector.Commit();
                syncPeer.Serializer.Connector.Commit();
            }
            catch (Exception ex)
            {
                ResetSynchronizedState();
                if (!OnSynchronizeEvent(new SynchronizeEventArgs()))
                {
                    throw new Exception("Synchronizing failed at: " + lastContainer, ex);
                }
            }
        }

        private bool CheckSyncContext(SyncTool syncPeer)
        {
            if (!ReferenceEquals(syncPeer.SyncSerializer, _syncSerializer))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckTarget(SyncTool syncPeer)
        {
            if (ReferenceEquals(syncPeer.Serializer, _serializer))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IList<SyncContainer> GetSyncContainer<T>() where T : SyncState
        {
            List<SyncContainer> ret = new List<SyncContainer>();
            foreach (SyncContainer c in _pooledObjects.Values)
            {
                if (c.SyncState.GetType() == typeof(T))
                {
                    ret.Add(c);
                }
            }

            return ret;
        }

        private void ResetSynchronizedState()
        {
            foreach(SyncContainer sc in GetSyncContainer<SynchronizedState>()) 
            {
                ((SynchronizedState)sc.SyncState).Reset();
            }
        }

    }
}
