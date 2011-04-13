using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.UMF.Initializer.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class FieldToPropertyConnectionAttribute : Attribute
    {
        private Type _connectedType;
        private String _connectedProperty;


        public Type ConnectedType
        {
            get { return _connectedType; }
        }

        public String ConnectedProperty
        {
            get { return _connectedProperty; }
        }
    }
}
