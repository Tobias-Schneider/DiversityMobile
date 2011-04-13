using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.UMF.Context
{
    public class ContextCorruptedException: Exception
    {
        public ContextCorruptedException(String msg)
            : base(msg)
        {
        }
    }
}
