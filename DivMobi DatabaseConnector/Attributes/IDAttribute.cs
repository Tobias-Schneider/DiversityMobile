using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class IDAttribute : Attribute
    {
        private bool _autoinc = false;

        public Boolean Autoinc
        { 
            get { return _autoinc; }
            set { _autoinc = value; }
        }
    }
}
