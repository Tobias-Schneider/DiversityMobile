using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UBT.AI4.Bio.DivMobi.ListSynchronization
{
    public class Conflict
    {
        private Type ownerType;
        private FieldInfo field;
        private Object value1;
        private Object value2;

        internal Conflict(Type t, FieldInfo f, Object v1, Object v2)
        {
            this.ownerType = t;
            this.field = f;
            this.value1 = v1;
            this.value2 = v2;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(ownerType.Name);
            sb.Append(",").Append(field.Name).Append(",");
            if (value1 != null)
            {
                sb.Append((Object)value1.ToString());
            }
            else
            {
                sb.Append("NULL");
            }
            sb.Append(",");
            if (value2 != null)
            {
                sb.Append(value2.ToString());
            }
            else
            {
                sb.Append("NULL");
            }
            return sb.ToString();
        }
    }
}
