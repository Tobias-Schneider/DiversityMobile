using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.UMF.Layout;


namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class FixedLayoutGroupAttribute : Attribute, IFixedLayoutGroup
    {
        private String _groupId;
        private Type _customLayoutControl;

        public FixedLayoutGroupAttribute(String groupId)
        {
            _groupId = groupId;
        }

        public String GroupId
        {
            get { return _groupId; }
        }

        public Type CustomLayoutControl
        {
            get { return _customLayoutControl; }
            set { _customLayoutControl = value; }
        }

    }
}
