using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Actions
{
    public class SetEnabledAction : AbstractAction<Control, Control, Boolean>
    {
        public SetEnabledAction() : base("Enabled")
        {

        }

        protected override void PerformImpl(Control fieldOwner, FieldInfo fieldAccess, Control fieldValue, Boolean parameter)
        {
            fieldValue.Enabled = parameter;
        }
    }
}
