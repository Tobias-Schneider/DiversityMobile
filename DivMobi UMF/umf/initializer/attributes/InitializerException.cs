using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.umf.initializer
{
    public class InitializerException : Exception
    {
        public InitializerException()
            : base()
        {

        }

        public InitializerException(String msg) 
            : base(msg)
        {
        }

        public InitializerException(String msg, Exception cause)
            : base(msg, cause)
        {

        }
    }
}
