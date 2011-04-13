using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.ListSynchronization
{
    public class TransferException : Exception
    {
        public TransferException():base()
        {
            
        }

        public TransferException(String message):base(message)
        {
        
        }
    }
}
