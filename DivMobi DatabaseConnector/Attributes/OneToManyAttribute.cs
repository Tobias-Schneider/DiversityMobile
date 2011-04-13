using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=true)]
    public class OneToManyAttribute : RelationalAttribute
    {
        private int _deleteType = DeleteTypes.NOACTION;

        public int DeleteType { get { return _deleteType; } set { _deleteType = value; } }
    }

    public struct DeleteTypes
    {
        public const int NOACTION = 0;
        public const int CASCADE = 1;
        
    }
}

