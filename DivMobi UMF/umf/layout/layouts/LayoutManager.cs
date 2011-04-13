using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public abstract class LayoutManager
    {
        private VirtualContainer _layouted;

        protected LayoutManager(VirtualContainer layouted)
        {
            this._layouted = layouted;
        }

        protected IList<AbstractLayoutable> Layotables
        {
            get
            {
                return _layouted.Layoutables;
            }
        }

        public abstract void Resize(int x, int y);
        public abstract void Pack();
    }
}
