using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.umf.initializer.attributes
{
    public class ConnectionFlags
    {
        public const byte NoCopy = 0;
        public const byte CopyOnInvisible = 1;
        public const byte CopyOnDisabled = 2;

        public static Boolean PeformCopyOnInvisible(byte flag)
        {
            return ((flag & CopyOnInvisible) == CopyOnInvisible);
        }

        public static Boolean PerformCopyOnDisabled(byte flag)
        {
            return ((flag & CopyOnDisabled) == CopyOnDisabled);
        }
    }
}
