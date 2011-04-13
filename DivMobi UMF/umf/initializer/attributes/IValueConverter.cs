using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.umf.initializer
{
    public interface IValueConverter
    {
        Object ConvertForDataItem(Object val);

        Object ConvertForControl(Object val);
    }
}
