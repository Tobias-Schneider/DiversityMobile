using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

namespace UBT.AI4.Bio.DivMobi.DataLayer.SyncAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
    public class CheckForeingKeyConstraintAttribute : Attribute, ITarget
    {
        private String _Target = AttributeConstants.DEFAULT_TARGET;
        private String _UnmappedTable;
        private String _ExternColumn = null;

        public CheckForeingKeyConstraintAttribute(String unmappedTable)
        {
            _UnmappedTable = unmappedTable;
        }

        public String Target { get { return _Target; } set { _Target = value; } }

        public String ExternColumn { 
            get 
            {
                return _ExternColumn;
            } 
            set { _ExternColumn = value; } }

        public String UnmappedTable
        {
            get { return _UnmappedTable; }
        }
    }
}
