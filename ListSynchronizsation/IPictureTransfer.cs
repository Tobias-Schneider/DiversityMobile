using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;

namespace UBT.AI4.Bio.DivMobi.ListSynchronization
{
    public interface IPictureTransfer
    {
        void transferPicture(ISerializableObject iso);
    }
}
