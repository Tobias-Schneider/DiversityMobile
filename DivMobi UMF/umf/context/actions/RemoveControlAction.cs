using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.UMF.Layout;

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Actions
{
    public class RemoveControlAction : AbstractAction<Control, Control, Object>
    {
        public RemoveControlAction() : base("RemoveControlAction")
        {

        }

        protected override void PerformImpl(Control fieldOwner, FieldInfo fieldAccess, Control fieldValue, Object para)
        {
            fieldAccess.SetValue(fieldOwner, null);
            RecursiveControlRemove(fieldOwner, fieldValue);
            if (fieldOwner is ILayout)
            {
                ((ILayout)fieldOwner).Remove(fieldValue);
            }
        }

        private void RecursiveControlRemove(Control root, Control removed) 
        {
            foreach(Control c in root.Controls) 
            {
                RecursiveControlRemove(c, removed);
            }

            if (root.Controls.Contains(removed))
            {
                root.Controls.Remove(removed);
            }
        }
    }
}
