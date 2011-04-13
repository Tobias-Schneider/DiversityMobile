using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public interface ICustomLayoutControl
    {
        void PrePack();
        void PostPack();
        bool DoLayout();

        Control DeclaringControl { get; set; }
    }
}
