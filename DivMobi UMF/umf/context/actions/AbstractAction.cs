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

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Actions
{
    public abstract class AbstractAction<O, E, P> : IAction
    {
        private String _actionId;

        public AbstractAction(String actionId)
        {
            this._actionId = actionId;
        }
        
        public String ActionId { get { return _actionId; } }
        
        public void Perform(Object fieldOwner, FieldInfo fieldAccess, Object fieldValue, String parameter)
        {
            bool parsed;
            Object o = StringParserRegistry.Instance.ParseString(parameter, typeof(P), out parsed);
            
            if (!parsed) throw new Exception();
            PerformImpl((O)fieldOwner, fieldAccess, (E)fieldValue, (P)o);
        }

        protected abstract void PerformImpl(O fieldOwner, FieldInfo fieldAccess, E fieldValue, P para);
    }
}
