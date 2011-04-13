using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public interface ICleanable
    {
        void Cleanup();
        Cleaner Cleaner { set; }
    }
}
