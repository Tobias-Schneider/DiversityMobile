using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout
{
    public interface ILayoutFactory
    {
        ILayout CreateLayout(Control control);
    }
}
