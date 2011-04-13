using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class ConnectionCorruptedException : Exception
    {
        public ConnectionCorruptedException(String msg)
            : base(msg)
        {
        }
    }
}
