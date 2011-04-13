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
using UBT.AI4.Bio.DivMobi.UMF.Layout.Attributes;
using UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts;
using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout
{
    public class LayoutFactory
    {
        private static LayoutFactory _instance = new LayoutFactory();
        private List<ILayoutFactory> _factories = new List<ILayoutFactory>();


        private LayoutFactory()
        {
            RegisterLayoutFactory(new BoxLayoutFactory());
        }

        public static LayoutFactory Instance { get { return _instance; } }

        public ILayout CreateLayout(Control control, bool inherit)
        {
            try
            {
                Object[] tmp = control.GetType().GetCustomAttributes(typeof(LayoutAttribute), inherit);
                if (tmp.Count() > 0)
                {
                    LayoutAttribute l = (LayoutAttribute)tmp[0];

                    foreach (ILayoutFactory lf in _factories)
                    {
                        if (lf.GetType() == l.LayoutManager)
                        {
                            return lf.CreateLayout(control);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ContextCorruptedException("Layout couldn't be created. ("+ex.Message+")");
            }
            throw new ContextCorruptedException("Layout couldn't be created. (No LayoutFactory found.)");
        }

        public ILayout CreateLayout(Type layoutType, Control control)
        {            

            foreach (ILayoutFactory lf in _factories)
            {
                if (lf.GetType() == layoutType)
                {
                    return lf.CreateLayout(control);
                }
            }
        
            throw new Exception();
        }

        public void RegisterLayoutFactory(ILayoutFactory lf)
        {
            if (!_factories.Contains(lf))
            {
                _factories.Add(lf);
            }
        }
    }
}
