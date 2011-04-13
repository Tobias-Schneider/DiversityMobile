using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Actions
{
    public class SetVisibleAction : AbstractAction<Control, Control, Boolean>
    {
        public SetVisibleAction() : base ("Visible")
        {

        }

        protected override void PerformImpl(Control fieldOwner, FieldInfo fieldAccess, Control obj, Boolean para)
        {
            obj.Visible = para;
        }
    }
}
