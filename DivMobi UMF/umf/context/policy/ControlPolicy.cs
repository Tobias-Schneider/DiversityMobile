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
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Policy
{
    public class ControlPolicy : IPolicy
    {
        private static ControlPolicy _instance = new ControlPolicy();
        private List<String> _methods = new List<String>(3);

        private ControlPolicy()
        {
            _methods.Add("Show");
            _methods.Add("Hide");
            _methods.Add("Enabled");
        }

        public static ControlPolicy Instance { get { return _instance; } }

        public PermissionLevel GetInvocationPermissionLevel(Type type, String methodName)
        {
            if (type.IsSubclassOf(typeof(Control)))
            {
                if (_methods.Contains(methodName))
                {
                    return PermissionLevel.PERMITTED;
                }
                else
                {
                    return PermissionLevel.FORBIDDEN;
                }
            }
            return PermissionLevel.UNKNOWN;
        }

        public PermissionLevel GetInvocationPermissionLevel(String typeName, String methodName)
        {
            Type t = Type.GetType(typeName);
            return GetInvocationPermissionLevel(t, methodName);
        }
    }
}
