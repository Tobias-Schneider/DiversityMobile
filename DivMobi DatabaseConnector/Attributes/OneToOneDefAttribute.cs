using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class OneToOneDefAttribute : RelationalAttribute
    {
        private bool _Reflexive = false;

        public bool Reflexive { get { return _Reflexive; } set { _Reflexive = value; } }

        private int _deleteType = DeleteTypes.NOACTION;

        public int DeleteType { get { return _deleteType; } set { _deleteType = value; } }
    }
}
