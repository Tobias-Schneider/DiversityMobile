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

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Policy
{
    public class PolicyRegistry : IPolicy
    {
        private static PolicyRegistry _instance = new PolicyRegistry();
        private List<IPolicy> _policies = new List<IPolicy>();

        private PolicyRegistry()
        {

        }

        public static PolicyRegistry Instance
        {
            get { return _instance; }
        }

        public PermissionLevel GetInvocationPermissionLevel(Type type, String methodName)
        {
            PermissionLevel ret = PermissionLevel.UNKNOWN;
            foreach (IPolicy p in _policies)
            {
                if (p.GetInvocationPermissionLevel(type, methodName) == PermissionLevel.PERMITTED)
                {
                    ret = PermissionLevel.PERMITTED;
                }
                if (p.GetInvocationPermissionLevel(type, methodName) == PermissionLevel.FORBIDDEN)
                {
                    return PermissionLevel.FORBIDDEN;
                }
            }
            return ret;
        }

        public PermissionLevel GetInvocationPermissionLevel(String typeName, String methodName)
        {
            Type t = Type.GetType(typeName);
            return GetInvocationPermissionLevel(t, methodName);
        }

        public void RegisterPolicy(IPolicy policy)
        {
            if (!_policies.Contains(policy))
            {
                _policies.Add(policy);
            }
        }

        public void UnRegisterPolicy(IPolicy policy)
        {
            _policies.Remove(policy);
        }
    }
}
