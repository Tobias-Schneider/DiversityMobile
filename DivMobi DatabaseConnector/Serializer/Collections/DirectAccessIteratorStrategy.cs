using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections
{
    internal class DirectAccessIteratorStrategy : OneToManyStrategy
    {
        public override Object LoadOneToMany(ISerializableObject owner, Type storedType, Serializer serializer)
        {
            Type genType = typeof(DirectAccesIteratorImpl<>).MakeGenericType(new Type[] {storedType});
            IDirectAccessIteratorConfiguration conf = (IDirectAccessIteratorConfiguration)Activator.CreateInstance(genType);

            IVirtualKey vKey = AttributeWorker.GetInstance(serializer.Target).CreateVirtualKey(serializer, owner.GetType(), storedType);
            vKey.InitVirtualKeyBySource(owner);

            conf.Restriction = Restrictions.RestrictionFactory.SqlRestriction(storedType, vKey.ToSqlStringForward(serializer.Target));
            conf.Serializer = serializer;

            return conf;
        }

        public override void UpdateOneToMany(Object iterator, ISerializableObject owner, Type storedType, Serializer serializer)
        {
            IVirtualKey vKey = AttributeWorker.GetInstance(serializer.Target).CreateVirtualKey(serializer, owner.GetType(), storedType);
            vKey.InitVirtualKeyBySource(owner);

            ((IDirectAccessIteratorConfiguration)iterator).Restriction = Restrictions.RestrictionFactory.SqlRestriction(storedType, vKey.ToSqlStringForward(serializer.Target));
        }
    }
}
