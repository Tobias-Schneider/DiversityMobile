using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.SyncTool.SyncTool
{
    public class SyncException : Exception
    {
        public SyncException(String message) : base(message)
        {

        }

        public SyncException(String message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
