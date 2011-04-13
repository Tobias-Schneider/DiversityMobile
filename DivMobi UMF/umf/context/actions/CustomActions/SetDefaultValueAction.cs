using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using UBT.AI4.Bio.DivMobi.UMF.Context.Actions;
using UBT.AI4.Toolbox.Controls;

namespace UBT.AI4.Bio.DivMobi.UMF.UMF.Context.Actions.CustomActions
{
    public class SetDefaultValueAction : AbstractAction<Control, Control, String>
    {
        public SetDefaultValueAction()
            : base("SetDefaultValueAction")
        {

        }

        protected override void PerformImpl(Control fieldOwner, FieldInfo fieldAccess, Control fieldValue, String parameter)
        {
            if (fieldValue.GetType() == typeof(ContextPanel))
            {
                ((ContextPanel)fieldValue).DefaultValue = parameter;
            }
        }
    }
}