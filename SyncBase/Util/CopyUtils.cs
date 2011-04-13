using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DataLayer.SyncAttributes;


namespace UBT.AI4.Bio.DivMobi.SyncBase.Util
{
    public class CopyUtils
    {
        internal static void PlainCopy(Serializer context, ISerializableObject from, ISerializableObject to, FieldLock fieldLock, bool forceForeignKeyCopy)
        {
            AttributeWorker w = AttributeWorker.GetInstance(context.Target);

            FieldInfo[] fis = AttributeWorker.RetrieveAllFields(from.GetType());

            foreach (FieldInfo fi in fis)
            {
                if (fieldLock.IsLocked(fi)) continue;
                if (AttributeWorker.IsRelationField(fi)) continue;
                if (w.IsAutoincID(fi)) continue;
                if (!w.IsPersistentField(fi)) continue;
                if (AttributeWorker.IsRowGuid(fi)) continue;
                if (w.IsForeignKey(context, fi) && !forceForeignKeyCopy)
                {
                    DirectSyncAttribute a = (DirectSyncAttribute)Attribute.GetCustomAttribute(fi, typeof(DirectSyncAttribute));

                    if (a == null) continue;
                }


                Object val = fi.GetValue(from);
                fi.SetValue(to, val);

            }
        }

        internal static void ForeignKeyCopy(Serializer context, ISerializableObject from, ISerializableObject to, FieldLock fieldLock)
        {
            AttributeWorker w = AttributeWorker.GetInstance(context.Target);

            FieldInfo[] fis = AttributeWorker.RetrieveAllFields(from.GetType());

            foreach (FieldInfo fi in fis)
            {
                if (fieldLock.IsLocked(fi)) continue;
                if (!w.IsForeignKey(context, fi)) continue;

                Object val = fi.GetValue(from);
                fi.SetValue(to, val);

            }
        }
    }
}
