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
