using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes
{

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ColumnAttribute : Attribute, ITarget
    {
        private string _mapping;
        private string _target;
        private bool _nullable;

        public ColumnAttribute() {
            _target = AttributeConstants.DEFAULT_TARGET;
            _mapping = null;
            _nullable = true;
        }

        public virtual bool Nullable { get { return _nullable; } set { _nullable = value; }}

        public string Target { get { return _target; } set { _target = value; } }

        public string Mapping { get { return this._mapping; } set { this._mapping = value; } }
    }

}
