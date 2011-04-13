using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class GoogleMapsCorruptedException : Exception
    {
        public GoogleMapsCorruptedException(String msg)
            : base(msg)
        {
        }
    }
}
