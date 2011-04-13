using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public class UpdateException : SerializerException
    {
        public UpdateException() : base() { }
        public UpdateException(String message) : base(message) { }
        public UpdateException(String message, Exception innerException) : base(message, innerException) {}
    }
}
