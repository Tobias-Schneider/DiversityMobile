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
