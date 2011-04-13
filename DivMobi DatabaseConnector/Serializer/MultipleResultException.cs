using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public class MultipleResultException : SerializerException
    {
        private int _resultCount = 0;
        public MultipleResultException(int resultCount) : base("Multiple results ("+resultCount+") found!") {
            _resultCount = resultCount;
        }
        public MultipleResultException(String message) : base(message) {
        }
        public MultipleResultException(String message, Exception innerException) : base(message, innerException) {
        }
    }
}
