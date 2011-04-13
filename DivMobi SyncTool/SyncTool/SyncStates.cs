using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

namespace UBT.AI4.Bio.DivMobi.SyncTool.SyncTool
{

    public abstract class SyncState
    {
        private SyncContainer _StateOwner;
        private SyncState _PreviousState;


        public SyncState(SyncContainer owner)
        {
            _StateOwner = owner;
            _PreviousState = null;
        }

        internal SyncState(SyncContainer owner, SyncState previousState)
        {
            _StateOwner = owner;
            _PreviousState = previousState;
        }

        protected SyncContainer StateOwner
        {
            get { return _StateOwner; }
            set { _StateOwner = value; }
        }

        public virtual void Undo()
        {
            StateOwner.SyncState = _PreviousState;
        }
        public bool Undoable
        {
            get { return _PreviousState != null; }
        }

        public abstract void Resolve(ISerializableObject resolved);
        public abstract bool ResolveAllowed { get; }
        public abstract void Ignore();
        public abstract bool IgnoreAllowed { get; }
        public abstract void Truncate();
        public abstract bool TruncateAllowed { get; }

        public abstract void Prepare();
        public abstract void Release();
        public abstract void SynchronizeManyToOne();
        public abstract void SynchronizeOneToMany();
        public abstract void Synchronize();
        public abstract bool DoSynchronizing
        {
            get;
        }
    }

    public class PrematureState : SyncState
    {
        public PrematureState(SyncContainer owner)
            : base(owner)
        {

        }

        public override void Resolve(ISerializableObject resolved)
        {
            throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!");
        }
        public override bool ResolveAllowed
        {
            get { return false; }
        }
        public override void Ignore()
        {
            throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!");
        }
        public override bool IgnoreAllowed
        {
            get { return false; }
        }
        public override void Truncate()
        {
            throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!");
        }
        public override bool TruncateAllowed
        {
            get { return false; }
        }

        public override void Prepare()
        {
            throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!");
        }
        public override void Release()
        {
            throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!");
        }
        public override void SynchronizeManyToOne()
        {
            throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!");
        }
        public override void SynchronizeOneToMany()
        {
            throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!");
        }
        public override void Synchronize()
        {
            throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!");
        }
        public override bool DoSynchronizing
        {
            get { throw new InvalidOperationException("Objects in PrematureState cannont not perform any actions. Run SyncTool.Analyze first!"); }
        }
    }

    public class IgnoreState : SyncState
    {
        public IgnoreState(SyncContainer owner)
            : base(owner)
        {

        }

        internal IgnoreState(SyncContainer owner, SyncState previous)
            : base(owner, previous)
        {

        }

        public override void Resolve(ISerializableObject resolved)
        {
            throw new InvalidOperationException();
        }
        public override bool ResolveAllowed
        {
            get { return false; }
        }
        public override void Ignore()
        {
            //noop;
        }
        public override bool IgnoreAllowed
        {
            get { return true; }
        }
        public override void Truncate()
        {
            StateOwner.SyncState = new TruncateState(StateOwner, this);
        }
        public override bool TruncateAllowed
        {
            get { return true; }
        }

        public override void Prepare()
        {
            throw new InvalidOperationException();
        }
        public override void Release()
        {
            throw new InvalidOperationException();
        }
        public override void SynchronizeManyToOne()
        {
            throw new InvalidOperationException();
        }
        public override void SynchronizeOneToMany()
        {
            throw new InvalidOperationException();
        }
        public override void Synchronize()
        {
            throw new InvalidOperationException();
        }
        public override bool DoSynchronizing
        {
            get { return false; }
        }

    }

    public class TruncateState : IgnoreState
    {
        public TruncateState(SyncContainer owner) : base(owner) 
        {
            foreach (SyncContainer sc in StateOwner.Children)
            {
                sc.Truncate();
            }
        }

        internal TruncateState(SyncContainer owner, SyncState previous)
            : base(owner, previous)
        {
            foreach (SyncContainer sc in StateOwner.Children)
            {
                sc.Truncate();
            }
        }

        public override void Ignore()
        {
            throw new InvalidOperationException();
        }
        public override bool IgnoreAllowed
        {
            get { return false; }
        }

        public override void Undo()
        {
            base.Undo();
            foreach (SyncContainer sc in StateOwner.Children)
            {
                sc.Undo();
            }
        }
    }


    public abstract class WritingStates : SyncState
    {
        private ISerializableObject _keyCarrier;
        private bool _doSynchronizing;

        public WritingStates(SyncContainer owner)
            : base(owner)
        {
            _doSynchronizing = true;
        }

        internal WritingStates(SyncContainer owner, SyncState previous)
            : base(owner, previous)
        {
            _doSynchronizing = true;
        }

        protected ISerializableObject KeyCarrier { get { return _keyCarrier; } 
            set { _keyCarrier = value; } }

        public sealed override void Resolve(ISerializableObject resolved)
        {
            throw new InvalidOperationException();
        }
        public override bool ResolveAllowed
        {
            get { return false; }
        }

        public sealed override void Truncate()
        {
            StateOwner.SyncState = new TruncateState(StateOwner, this);
        }
        public override bool TruncateAllowed
        {
            get { return true; }
        }


        public override void Prepare()
        {
            if (KeyCarrier != null)
            {
                return;
            }
            KeyCarrier = StateOwner.SinkSerializer.CreateISerializableObject(StateOwner.SynchronizedType);
        }

        public sealed override void Release()
        {
            KeyCarrier = null;
            StateOwner.SyncState = new SynchronizedState(StateOwner, this);
        }

        public sealed override void SynchronizeManyToOne()
        {
            ISerializableObject keyCarrier = _keyCarrier;
            foreach (SyncContainer c in StateOwner.Parents)
            {
                c.Synchronize();
                c.AdjustKeyDownward(keyCarrier);
            }
        }

        public sealed override void SynchronizeOneToMany()
        {
            foreach (SyncContainer c in StateOwner.Children)
            {
                c.Synchronize();
            }
        }

        public sealed override void Synchronize()
        {
            _doSynchronizing = false;
            SynchronizeImpl();
        }

        public sealed override bool DoSynchronizing
        {
            get { return _doSynchronizing; }
        }

        protected abstract void SynchronizeImpl();
    }

    public class InsertState : WritingStates
    {

        public InsertState(SyncContainer owner)
            : base(owner)
        {

        }
        
        public override void Ignore()
        {
            throw new InvalidOperationException();
        }
        public override bool IgnoreAllowed
        {
            get { return false; }
        }

        protected override void SynchronizeImpl()
        {
            ISerializableObject source = StateOwner.SourceObject;
            ISerializableObject sink = StateOwner.SinkSerializer.CreateISerializableObject(StateOwner.SynchronizedType);

            CopyUtils.PlainCopy(StateOwner.SinkSerializer, KeyCarrier, sink, new FieldLock(source.GetType()), true);
            CopyUtils.PlainCopy(StateOwner.SinkSerializer, source, sink, new FieldLock(source.GetType()), false);

            StateOwner.SinkSerializer.Connector.InsertPlain(sink);
            StateOwner.SinkObject = sink;

            SyncItem si1 = StateOwner.SyncSerializer.CreateISerializableObject<SyncItem>();
            SyncItem si2 = StateOwner.SyncSerializer.CreateISerializableObject<SyncItem>();

            si1.SyncGuid = AttributeWorker.RowGuid(source);
            si2.SyncGuid = AttributeWorker.RowGuid(sink);
            si1.ClassID = source.GetType().Name;
            si2.ClassID = source.GetType().Name;

            si1.Hashcode = SyncContainer.ComputeHashCode(StateOwner.SourceSerializer.Target, source);
            si2.Hashcode = SyncContainer.ComputeHashCode(StateOwner.SinkSerializer.Target, sink);

            si1.CounterPart = si2;

            StateOwner.SyncSerializer.Connector.Save(si1);
            StateOwner.SyncSerializer.ConnectOneToOne(si1, si2);
            StateOwner.SyncSerializer.Connector.Save(si2);
            StateOwner.SyncSerializer.ConnectOneToOne(si2, si1);
            StateOwner.SyncSerializer.Connector.Save(si1);

            StateOwner.UpdateFieldStates(StateOwner.SourceSerializer.Target, si1, source);//Deaktiviert
            StateOwner.UpdateFieldStates(StateOwner.SinkSerializer.Target, si2, sink);
        }
    }

    public class UpdateState : WritingStates
    {
        protected SyncItem _sourceSyncItem;
        protected FieldLock _fieldLock;

        public UpdateState(SyncContainer owner, SyncItem sourceSyncItem)
            : base(owner)
        {
            _sourceSyncItem = sourceSyncItem;
            _fieldLock = new FieldLock(_sourceSyncItem.GetType());
        }

        internal UpdateState(SyncContainer owner, SyncItem sourceSyncItem, SyncState previous)
            : base(owner, previous)
        {
            _sourceSyncItem = sourceSyncItem;
            _fieldLock = new FieldLock(_sourceSyncItem.GetType());
        }

        public override void Ignore()
        {
            StateOwner.SyncState = new IgnoreState(StateOwner, this);
        }
        public override bool IgnoreAllowed
        {
            get { return true; }
        }

        protected override void SynchronizeImpl()
        {
            ISerializableObject source = StateOwner.SourceObject;
            ISerializableObject sink = StateOwner.SinkObject;
            CopyUtils.PlainCopy(StateOwner.SinkSerializer, KeyCarrier, sink, _fieldLock ,true);
            CopyUtils.PlainCopy(StateOwner.SinkSerializer, source, sink, _fieldLock, false);

            StateOwner.SinkSerializer.Connector.UpdatePlain(sink);

            _sourceSyncItem.Hashcode = SyncContainer.ComputeHashCode(StateOwner.SourceSerializer.Target, source);
            StateOwner.SyncSerializer.Connector.Save(_sourceSyncItem);
            SyncItem sinkSyncItem = _sourceSyncItem.CounterPart;
            sinkSyncItem.Hashcode = SyncContainer.ComputeHashCode(StateOwner.SinkSerializer.Target, sink);
            StateOwner.SyncSerializer.Connector.Save(sinkSyncItem);

            StateOwner.UpdateFieldStates(StateOwner.SourceSerializer.Target, _sourceSyncItem, source);
            StateOwner.UpdateFieldStates(StateOwner.SinkSerializer.Target, sinkSyncItem, sink);
        }

        public void LockField(String fieldName) 
        {
            _fieldLock.LockField(fieldName);
        }

        public void UnlockField(String fieldName)
        {
            _fieldLock.UnLockField(fieldName);
        }

        public bool IsLocked(String fieldName)
        {
            return _fieldLock.IsLocked(fieldName);
        }
    }

    public class ConflictResolvedState : UpdateState
    {
        private ISerializableObject _resolved;

        public ConflictResolvedState(SyncContainer owner, ISerializableObject resolved, SyncItem sourceSyncItem)
            : base(owner, sourceSyncItem)
        {
            _resolved = resolved;
        }

        internal ConflictResolvedState(SyncContainer owner, ISerializableObject resolved, SyncItem sourceSyncItem, SyncState previous)
            : base(owner, sourceSyncItem, previous)
        {
            _resolved = resolved;
        }

        protected override void SynchronizeImpl()
        {
            ISerializableObject source = StateOwner.SourceObject;
            ISerializableObject sink = StateOwner.SinkObject;


            CopyUtils.ForeignKeyCopy(StateOwner.SinkSerializer, KeyCarrier, sink, base._fieldLock);
            
            //Kopiere die angepassten Daten...
            CopyUtils.PlainCopy(StateOwner.SinkSerializer, _resolved, sink, base._fieldLock, false);
            CopyUtils.PlainCopy(StateOwner.SourceSerializer, _resolved, source, base._fieldLock, false);

            StateOwner.SinkSerializer.Connector.UpdatePlain(sink);
            StateOwner.SourceSerializer.Connector.UpdatePlain(source);


            _sourceSyncItem.Hashcode = SyncContainer.ComputeHashCode(StateOwner.SourceSerializer.Target, source);
            StateOwner.SyncSerializer.Connector.Save(_sourceSyncItem);
            SyncItem tmp = _sourceSyncItem.CounterPart;
            tmp.Hashcode = SyncContainer.ComputeHashCode(StateOwner.SinkSerializer.Target, sink);
            StateOwner.SyncSerializer.Connector.Save(tmp);

            StateOwner.UpdateFieldStates(StateOwner.SourceSerializer.Target, _sourceSyncItem, source);
            StateOwner.UpdateFieldStates(StateOwner.SinkSerializer.Target, tmp, sink);
        }
    }

        
    public class ConflictState : SyncState
    {
        private SyncItem _sourceSyncItem;        

        public ConflictState(SyncContainer owner, SyncItem sourceSyncItem) : base(owner)
        {
            _sourceSyncItem = sourceSyncItem;
        }

        public override void Resolve(ISerializableObject resolved)
        {
            StateOwner.SyncState = new ConflictResolvedState(StateOwner, resolved, _sourceSyncItem, this);
        }
        public override bool ResolveAllowed
        {
            get { return true; }
        }

        public override void Ignore()
        {
            StateOwner.SyncState = new IgnoreState(StateOwner, this);
        }
        public override bool IgnoreAllowed
        {
            get { return true; }
        }
        public override void Truncate()
        {
            StateOwner.SyncState = new TruncateState(StateOwner, this);
        }
        public override bool TruncateAllowed
        {
            get { return true; }
        }

        public override void Prepare()
        {
            throw new InvalidOperationException();
        }
        public override void Release()
        {
            throw new InvalidOperationException();
        }
        public override void SynchronizeManyToOne()
        {
            throw new InvalidOperationException();
        }
        public override void SynchronizeOneToMany()
        {
            throw new InvalidOperationException();
        }
        public override void Synchronize() 
        {
            throw new InvalidOperationException();
        }
        public override bool DoSynchronizing
        {
            get { throw new InvalidOperationException(); }
        }
    }

    public class DeletedState : ConflictState
    {
        public DeletedState(SyncContainer owner, SyncItem sourceSyncItem)
            : base(owner, sourceSyncItem)
        {

        }

        public override void Ignore()
        {
            throw new InvalidOperationException();
        }
        public override bool IgnoreAllowed
        {
            get { return false; }
        }
    }


    public class SynchronizedState : IgnoreState
    {
        internal SynchronizedState(SyncContainer owner, SyncState previous)
            : base(owner, previous)
        {

        }

        public sealed override void Ignore()
        {
            throw new InvalidOperationException();
        }

        public override void Undo()
        {
            throw new InvalidOperationException("Synchronized state cannot be undone!");
        }

        internal void Reset()
        {
            base.Undo();
        }
    }
}
