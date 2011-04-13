using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
namespace UBT.AI4.Bio.DivMobi.ListSynchronization
{
    public class ListContainer
    {
        internal ISerializableObject iso;
        internal ISerializableObject partner;
        private SyncStates_Enum state;

        public ListContainer(ISerializableObject iso)
        {
            this.iso=iso;
            this.state = SyncStates_Enum.PrematureState;
        }

        public ListContainer(ISerializableObject iso, SyncStates_Enum state):this(iso)
        {
            this.state = state;
        }

        public ListContainer(ISerializableObject iso, ISerializableObject partner, SyncStates_Enum state)
            : this(iso, state)
        {
            this.partner = partner;
        }

        public SyncStates_Enum State
        {
            get { return state; }
            set { state = value; }
        }
        public ISerializableObject Iso
        {
            get { return iso; }
        }
        public ISerializableObject Partner
        {
            get { return partner; }
        }

    }
}
