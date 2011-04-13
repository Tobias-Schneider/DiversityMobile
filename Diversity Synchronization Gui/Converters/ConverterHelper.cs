using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui.Converters
{
    class ConverterHelper
    {
        internal static bool CheckFlags(ConnectionsAccess.ConnectionState value, object parameter, bool CheckForAll)
        {           
            if (parameter == null || !(parameter is string))
                throw new InvalidOperationException("The parameter specifying wich Flags to test for may not be empty and must be of Type ConnectionState!");

            
            ConnectionsAccess.ConnectionState f = (ConnectionsAccess.ConnectionState)Enum.Parse(typeof(ConnectionsAccess.ConnectionState), (string)parameter);
            return value.CheckForFlags(f,CheckForAll);
        }
    }
}
