using System;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public class SerializerException : Exception
    {
        public SerializerException() : base() 
        {

        }

        public SerializerException(String message) : base(message) 
        {

        }

        public SerializerException(String message, Exception innerException) : base(message, innerException) {

        }
    }
}
