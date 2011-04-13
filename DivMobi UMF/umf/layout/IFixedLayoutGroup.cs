using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout
{
    public interface IFixedLayoutGroup
    {
        String GroupId { get; }
        Type CustomLayoutControl { get; set; }
    }
}
