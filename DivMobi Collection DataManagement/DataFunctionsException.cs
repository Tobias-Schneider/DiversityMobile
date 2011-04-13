using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class DataFunctionsException : Exception
    {
        public DataFunctionsException(String msg)
            : base(msg)
        {
        }
    }
}