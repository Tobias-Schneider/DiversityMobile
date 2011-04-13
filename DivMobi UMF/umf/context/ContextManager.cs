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
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.UMF.Layout;

namespace UBT.AI4.Bio.DivMobi.UMF.Context
{
    public class ContextManager
    {
        private Dictionary<Object, List<ILayout>> _virtualContainers = new Dictionary<Object, List<ILayout>>();

        private static ContextManager _instance = new ContextManager();

        private Dictionary<String, IContext> _contexts = new Dictionary<String,IContext>();

        private ContextManager()
        {

        }

        public static ContextManager Instance { get { return _instance; } }

        public void RegisterContext(IContext context) 
        {
            _contexts[context.ContextId] = context;
        }

        public void DropContexts()
        {
            _contexts.Clear();
        }

        public bool DropContext(String contextId)
        {
            return _contexts.Remove(contextId);
        }

        public IContext GetContext(String contextId)
        {
            IContext tmp;
            if (_contexts.TryGetValue(contextId, out tmp))
                return tmp;
            else
                return null;
        }

        public void Register(ILayouted layouted)
        {
            try
            {
                List<ILayout> tmp = null;

                if (_virtualContainers.ContainsKey(layouted))
                {
                    tmp = _virtualContainers[layouted];
                }

                if (tmp == null)
                {
                    tmp = new List<ILayout>();
                    _virtualContainers[layouted] = tmp;
                }

                if (!tmp.Contains(layouted.Layout))
                {
                    tmp.Add(layouted.Layout);
                }
            }
            catch (Exception ex)
            {
                throw new ContextCorruptedException("Layout couldn't be registered. (" + ex.Message + ")");
            }
        }



        public bool UnRegisterAll(Object managed)
        {
            return _virtualContainers.Remove(managed);            
        }

        internal List<ILayout> GetLayouts(Object managed)
        {
            if (_virtualContainers.ContainsKey(managed))
            {
                return _virtualContainers[managed];
            }
            else
            {
                return null;
            }
        }
    }
}
