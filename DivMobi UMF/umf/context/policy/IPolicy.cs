using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Policy
{
    public interface IPolicy
    {
        PermissionLevel GetInvocationPermissionLevel(Type type, String methodName);
        PermissionLevel GetInvocationPermissionLevel(String typeName, String methodName);
    }
}
