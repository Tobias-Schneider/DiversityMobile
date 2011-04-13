using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections
{
    internal abstract class OneToManyStrategy
    {
        public abstract Object LoadOneToMany(ISerializableObject owner, Type storedType, Serializer serializer);
        public abstract void UpdateOneToMany(Object collection, ISerializableObject owner, Type storedType, Serializer serializer);
    }
}
