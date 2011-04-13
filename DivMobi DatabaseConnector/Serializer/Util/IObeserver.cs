using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util
{
    public interface IObeserver
    {
        void Update(IObeservable obeservable, object message);
    }
}
