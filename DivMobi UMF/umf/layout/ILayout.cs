using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout
{
    public interface ILayout
    {
        void Pack();
        void Remove(Control control);
        int ComputedWidth { get; }
        int ComputedHeight { get; }
    }
}
