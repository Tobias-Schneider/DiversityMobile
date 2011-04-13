using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public class BoxLayout : LayoutManager
    {
        private BoxLayoutType _layoutType;

        internal BoxLayout(BoxLayoutType layoutType, VirtualContainer layouted)
            : base(layouted)
        {
            _layoutType = layoutType;
        }

        public override void Resize(int x, int y)
        {

        }

        public override void Pack()
        {
            foreach (AbstractLayoutable l in Layotables)
            {
                if (l.DoPack())
                {
                    l.Pack();
                }
            
            }

            if (_layoutType is VBoxLayoutType)
            {
                for (int i = 0; i < Layotables.Count - 1; i++)
                {
                    int y = Layotables[i].VirtualY + Layotables[i].ComputedHeight;
                    Layotables[i + 1].TranslateToY(y);
                }
            }
            else
            {
                for (int i = 0; i < Layotables.Count - 1; i++)
                {
                    int x = Layotables[i].VirtualX + Layotables[i].ComputedWidth;
                    Layotables[i + 1].TranslateToX(x);
                }
            }
        }
    }
}
