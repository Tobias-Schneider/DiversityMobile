using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using UBT.AI4.Bio.DivMobi.UMF.Context.Actions;

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Actions.CustomActions
{
    public class SetTextAction : AbstractAction<Control, Control, String>
    {
        public SetTextAction()
            : base("SetTextAction")
        {

        }

        protected override void PerformImpl(Control fieldOwner, FieldInfo fieldAccess, Control fieldValue, String parameter)
        {
            fieldValue.Text = parameter;
        }
    }
}
