using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

using UBT.AI4.Bio.DivMobi.DataLayer.SyncAttributes;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Relations;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;

namespace UBT.AI4.Bio.DivMobi.SyncTool.SyncTool
{


    public class SyncContainer
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SyncContainer));
        private Serializer _syncSerializer;
        private Serializer _sourceSerializer;
        private Serializer _sinkSerializer;

        private List<SyncContainer> _parents;
        private List<SyncContainer> _children;

        private Type _synchronizedType;
        private Guid _sourceGuid;
        private Guid _sinkGuid;

        
        private SyncState _SyncState;

        public SyncState SyncState { set { _SyncState = value; } get { return _SyncState; } }

       


        //Konstruktor 
        internal SyncContainer(ISerializableObject iso, Serializer syncSerializer, Serializer sourceSerializer)
        {
            SyncState = new PrematureState(this);

            _synchronizedType = iso.GetType();
            _sourceGuid = AttributeWorker.RowGuid(iso);

            _syncSerializer = syncSerializer;
            _sourceSerializer = sourceSerializer;

            _parents = new List<SyncContainer>();
            _children = new List<SyncContainer>();

        }

        internal void InitSyncContainer(IDictionary<Guid, SyncContainer> pooledObjects)
        {
            ResolverData<ISerializableObject> data = new ResolverData<ISerializableObject>();

            ISerializableObject iso = FetchObject(_sourceSerializer, _sourceGuid);

            data.HandledItem = iso;
            data.FieldsToResolve = AttributeWorker.RetrieveAllFields(iso.GetType());//Sind alle nötig?

            DependencyRelationHandler h = new DependencyRelationHandler(this, pooledObjects);
            RelationResolver<ISerializableObject> r = new RelationResolver<ISerializableObject>();
            r.Handler = h;
            r.StartRelationResolving(data);
        }

        internal void Analyze()
        {
            SyncItem sourceSyncItem = FetchSyncItem(_sourceGuid);
            SyncItem sinkSyncItem;
            ISerializableObject source = FetchObject(_sourceSerializer, _sourceGuid);


            if (sourceSyncItem == null)//In der Synchronisationstabelle gibt es keinen korrepondierenden Eintrag
            {
                #if DEBUG
                logger.Debug(source.GetType().Name + ", guid="+AttributeWorker.RowGuid(source) + " is not synchronized and will be inserted");
                #endif

                SyncState = new InsertState(this);
            } 
            else //in der Synchronisationstabelle ist ein Eintrag vorhanden
            {
                sinkSyncItem = sourceSyncItem.CounterPart;
                _sinkGuid = sinkSyncItem.SyncGuid;

                ISerializableObject sink = FetchObject(_sinkSerializer, _sinkGuid);//Das korrepondierende Objekt wird aus der Zieldatenbankgeholt

                if (sink == null)//Wenn es nicht vorhanden ist wurde es gelöscht
                {
                    #if DEBUG
                    logger.Debug(sourceSyncItem.ClassID + ", guid=" + AttributeWorker.RowGuid(source) + " does not exist, the conflict has to be resolved");
                    #endif

                    SyncState = new DeletedState(this, sourceSyncItem);
                }
                else
                {
                    String h1 = sinkSyncItem.Hashcode;
                    String sinkHashCode = ComputeHashCode(_sinkSerializer.Target, sink);
                    
                    
                    if (h1 != sinkHashCode)//Änderung der Daten in der Zieldatenbank--warum ist das schon ein Konflikt?
                    {
                        #if DEBUG
                        logger.Debug(sourceSyncItem.ClassID + ", guid=" + AttributeWorker.RowGuid(source) + " is involved in a conflict, which has to be resolved");
                        #endif
                        SyncState = new ConflictState(this, sourceSyncItem);
                    }
                    else //zieldatenbank unverändert
                    {
                        String sourceHashCode = ComputeHashCode(SourceSerializer.Target, source);

                        if (sourceHashCode == sourceSyncItem.Hashcode)//Quelldatenbank unverändert
                        {
                            #if DEBUG
                            logger.Debug(sourceSyncItem.ClassID + ", guid=" + AttributeWorker.RowGuid(source) + " has not changed and will not be synchronized");
                            #endif
                            SyncState = new IgnoreState(this);
                        }
                        else//Quelldatenbank verändert-Zieldatenbank unverändert ->update
                        {
                            #if DEBUG
                            logger.Debug(sourceSyncItem.ClassID + ", guid=" + AttributeWorker.RowGuid(source) + " has changed on the sourceside and will be updated");
                            #endif
                            SyncState = new UpdateState(this, sourceSyncItem);
                        }
                    }
                }
            }

            AttributeWorker w = AttributeWorker.GetInstance(_sinkSerializer.Target);
            IList<FieldInfo> fis = w.GetFieldByAttribute<CheckForeingKeyConstraintAttribute>(_synchronizedType);


            foreach (FieldInfo fi in fis)
            {
                Object val = fi.GetValue(source);
                if(val == null) continue;

                CheckForeingKeyConstraintAttribute attr = w.GetAttribute<CheckForeingKeyConstraintAttribute>(fi);
                String table = attr.UnmappedTable;
                String column = attr.ExternColumn;
                if (column == null)
                {
                    column = w.GetColumnMapping(fi);
                }

                if (!_sinkSerializer.Connector.Exists(column, val, table))
                {
                    //Hier kann man einen neuen Status einführen, der dieses Problem abfängt
                    throw new InvalidOperationException("Insertion of "+_synchronizedType.Name+" will fail. Unmapped foreign key "+column+"="+val+" does not exist in target database!");
                }
            }
        }


        #region statemodifiers

        public void Resolve(ISerializableObject resolved)
        {
            _SyncState.Resolve(resolved);
        }
        public bool ResolveAllowed { get { return _SyncState.ResolveAllowed; } }

        public void Ignore()
        {
            _SyncState.Ignore();
        }
        public bool IgnoreAllowed { get { return _SyncState.IgnoreAllowed; } }

        public void Truncate()
        {
            _SyncState.Truncate();
        }
        public bool TruncateAllowed { get { return _SyncState.TruncateAllowed; } }

        public void Undo() { _SyncState.Undo(); }
        public bool Undoable { get { return _SyncState.Undoable; } }

        #endregion


        internal Serializer SinkSerializer 
        {
            set { _sinkSerializer = value; }
            get { return _sinkSerializer; }
        }

        internal Serializer SourceSerializer
        {
            set { _sourceSerializer = value; }
            get { return _sourceSerializer; }
        }

        internal Serializer SyncSerializer
        {
            set { _syncSerializer = value; }
            get { return _syncSerializer; }
        }

        internal Type SynchronizedType
        {
            get { return _synchronizedType; }
        }

        internal IList<SyncContainer> Parents { get { return _parents; } }

        internal IList<SyncContainer> Children { get { return _children; } }

        public Serializer TargetSerializer { set { _sinkSerializer = value; } }

        public ISerializableObject SourceObject { get { return FetchObject(_sourceSerializer, _sourceGuid); } }

        public ISerializableObject SinkObject { 
            get { return FetchObject(_sinkSerializer, _sinkGuid); }
            set { _sinkGuid = AttributeWorker.RowGuid(value); }
        }



        public void Synchronize()
        {
            if (!SyncState.DoSynchronizing) return;

            SyncState.Prepare();

            SyncState.SynchronizeManyToOne();

            //Wenn das Objekt schon durch das Synchronisieren
            //der Hierarchie fertigestellt wurde, wird hier 
            //wird hier beendet;
            if (!SyncState.DoSynchronizing) return;

            SyncState.Synchronize();

            SyncState.SynchronizeOneToMany();

            SyncState.Release();
        }

        internal void AdjustKeyDownward(ISerializableObject iso) {
            AttributeWorker w = AttributeWorker.GetInstance(_sinkSerializer.Target);
            IVirtualKey vKey = w.CreateVirtualKey(_sinkSerializer, _synchronizedType, iso.GetType());
            vKey.InitVirtualKeyBySource(FetchObject(_sinkSerializer, _sinkGuid));
            vKey.ApplyVirtualKeyToTarget(iso);
        }

        internal void UpdateFieldStates(String target, SyncItem owner, ISerializableObject synchronized)
        {
            //AttributeWorker w = AttributeWorker.GetInstance(target);
            //List<FieldInfo> fis = new List<FieldInfo>(AttributeWorker.RetrieveAllFields(synchronized.GetType()));
            //FieldState[] fieldStates = owner.FieldStates.ToArray();
            
            //foreach (FieldState fs in fieldStates)
            //{
            //    FieldInfo f = w.RetrieveField(synchronized.GetType(), fs.FieldName, false);
            //    fis.Remove(f);
            //    fs.ComputeFieldState(f, synchronized);
            //    _syncSerializer.Connector.UpdatePlain(fs);
                
            //}

            //foreach (FieldInfo fi in fis)
            //{
            //    if (w.IsPersistentField(fi))
            //    {
            //        FieldState s = _syncSerializer.CreateISerializableObject<FieldState>();
            //        s.ComputeFieldState(fi, synchronized);
            //        _syncSerializer.ConnectOneToMany(owner, s);
            //        _syncSerializer.Connector.Save(s);
            //    }
            //}
        }

        internal static String ComputeHashCode(String target, ISerializableObject iso)
        {
            AttributeWorker w = AttributeWorker.GetInstance(target);

            FieldInfo[] fis = AttributeWorker.RetrieveAllFields(iso.GetType());

            StringBuilder b = new StringBuilder();
            foreach (FieldInfo fi in fis)
            {
                if (w.IsPersistentField(fi) && !w.IsID(fi))
                {
                    Object val = fi.GetValue(iso);
                    b.Append(val);
                }
            }

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            Byte[] tmp = encoding.GetBytes(b.ToString());


            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(tmp)).Replace("-", "").ToLower();

        }

        

        private ISerializableObject FetchObject(Serializer s, Guid guid)
        {
            FieldInfo fi = AttributeWorker.RowGuid(_synchronizedType);
            String col = AttributeWorker.GetInstance(s.Target).GetColumnMapping(fi);
            StringBuilder tmp = new StringBuilder();
            tmp.Append(col).Append("='").Append(guid).Append("'"); ;
            IRestriction res = RestrictionFactory.SqlRestriction(_synchronizedType, tmp.ToString());
            ISerializableObject iso = s.Connector.Load(_synchronizedType, res);
            return iso;
        }

        private SyncItem FetchSyncItem(Guid guid)
        {
            IRestriction r = RestrictionFactory.Eq(typeof(SyncItem), "_SyncGuid", guid);
            SyncItem siSource = (SyncItem)_syncSerializer.Connector.Load(typeof(SyncItem), r);
            return siSource;
        }

        public static bool operator ==(SyncContainer a, SyncContainer b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a._sinkGuid == b._sinkGuid && a._sourceGuid == b._sourceGuid;
        }

        public static bool operator !=(SyncContainer a, SyncContainer b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is SyncContainer)
            {
                return (this == (SyncContainer)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _sourceGuid.GetHashCode() ^ _sinkGuid.GetHashCode();
        }


        //Stringrepresentation erstellen
        public override string ToString()
        {
            string representation;
            representation = SourceObject.ToString();
            return representation;
        }

        private class DependencyRelationHandler : IRelationHandler<ISerializableObject>
        {
            private SyncContainer _owner;
            private IDictionary<Guid, SyncContainer> _pooledObjects;

            public DependencyRelationHandler(SyncContainer owner, IDictionary<Guid, SyncContainer> pooledObjects)
            {
                _pooledObjects = pooledObjects;
                _owner = owner;
            }

            public void HandleOneToMany(ISerializableObject handledItem, OneToManyAttribute attr, FieldInfo field)
            {
                IEnumerable set = (IEnumerable)field.GetValue(handledItem);

                foreach (ISerializableObject iso in set)
                {
                    try
                    {
                        SyncContainer s = _pooledObjects[AttributeWorker.RowGuid(iso)];
                        if(!_owner._children.Contains(s))
                        _owner._children.Add(s);
                        if (!s._parents.Contains(_owner))
                        s._parents.Add(_owner);
                    }
                    catch (KeyNotFoundException)
                    {

                    }
                }
            }

            public void HandleManyToOne(ISerializableObject handledItem, ManyToOneAttribute attr, FieldInfo field)
            {
                ISerializableObject iso = (ISerializableObject)field.GetValue(handledItem);

                if (iso != null)
                {
                    try
                    {
                        SyncContainer s = _pooledObjects[AttributeWorker.RowGuid(iso)];
                        if (!_owner._parents.Contains(s))
                        _owner._parents.Add(s);
                        if (!s._children.Contains(_owner))
                        s._children.Add(_owner);
                    }
                    catch (KeyNotFoundException)
                    {

                    }
                }
            }

            public void HandleOneToOneReflexive(ISerializableObject handledItem, OneToOneDefAttribute attr, FieldInfo field)
            {
                throw new NotImplementedException();
            }            
        }
    }
}
