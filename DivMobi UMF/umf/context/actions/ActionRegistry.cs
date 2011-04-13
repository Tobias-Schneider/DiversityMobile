using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.UMF.Context.Actions
{
    public class ActionRegistry
    {
        private static ActionRegistry _instance = new ActionRegistry();
        private Dictionary<String, IAction> _actions = new Dictionary<String, IAction>();

        private ActionRegistry()
        {

        }

        public static ActionRegistry Instance { get { return _instance; } }

        public void RegisterAction(IAction action)
        {
            try
            {
                _actions[action.ActionId] = action;
            }
            catch (Exception ex)
            {
                throw new ContextCorruptedException("Action couldn't be registered. ("+ex.Message+")");
            }
        }

        public void UnregisterAction(IAction action)
        {
            _actions.Remove(action.ActionId);
        }

        public IAction GetAction(String actionId)
        {
            IAction val = null;
            _actions.TryGetValue(actionId, out val);
            return val;
        }
    }
}
