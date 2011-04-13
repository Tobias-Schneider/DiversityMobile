//#######################################################################
//Diversity Mobile Synchronization
//Project Homepage:  http://www.diversitymobile.net
//Copyright (C) 2011  Tobias Schneider, Lehrstuhl Angewndte Informatik IV, Universität Bayreuth
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//#######################################################################
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
