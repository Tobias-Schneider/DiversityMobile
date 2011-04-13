using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector2.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        private bool _nullable;

        public ColumnAttribute()
        {
            _nullable = true;
        }

        public ColumnAttribute(bool nullable):this()
        {
            _nullable = nullable;
        }

        public virtual bool Nullable { get { return _nullable; } set { _nullable = value; } }
    }
}
