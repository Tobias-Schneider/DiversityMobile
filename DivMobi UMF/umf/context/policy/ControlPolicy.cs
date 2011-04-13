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
