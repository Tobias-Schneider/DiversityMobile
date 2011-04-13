using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes
{
    public class LayoutAttribute : Attribute
    {
        private Type _layoutManager;

        public LayoutAttribute(Type layoutManger)
        {
            _layoutManager = layoutManger;
        }

        public Type LayoutManager
        {
            get { return _layoutManager; }
        }
    }
}
