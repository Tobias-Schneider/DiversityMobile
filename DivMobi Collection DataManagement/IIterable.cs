using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public interface IIterable
    {
        ISerializableObject Next
        {
            get;
        }
        ISerializableObject Current
        {
            get;
        }
        ISerializableObject Previous
        {
            get;
        }
        ISerializableObject First
        {
            get;
        }
        ISerializableObject Last
        {
            get;
        }
        bool HasNext();
        bool Find();
    }
}
