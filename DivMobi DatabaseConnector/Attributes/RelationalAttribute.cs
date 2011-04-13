using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RelationalAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class JoinColumsAttribute : Attribute
    {
        private String _defColumn;
        private String _joinColumn;

        public String DefColumn
        {
            get { return _defColumn; }
            set { _defColumn = value; }
        }

        public String JoinColumn
        {
            get { return _joinColumn; }
            set { _joinColumn = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class MappedByAttribute : Attribute
    {
        private String _mappedBy;

        public MappedByAttribute(String mappedBy)
        {
            _mappedBy = mappedBy;
        }

        public String MappedBy
        {
            get { return _mappedBy; }
        }

    }
}
