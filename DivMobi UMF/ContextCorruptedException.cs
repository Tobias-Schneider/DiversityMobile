using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.umf.context
{
    public class ContextCorruptedException: Exception
    {
        public ContextCorruptedException(String msg)
            : base(msg)
        {
        }
    }
}
