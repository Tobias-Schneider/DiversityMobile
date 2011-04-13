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
