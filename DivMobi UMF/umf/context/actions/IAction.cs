using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Actions
{
    public interface IAction
    {
        String ActionId { get; }
        void Perform(Object fieldOwner, FieldInfo fieldAccess, Object fieldValue, String parameter);
    }
}
