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

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public class BoxLayout : LayoutManager
    {
        private BoxLayoutType _layoutType;

        internal BoxLayout(BoxLayoutType layoutType, VirtualContainer layouted)
            : base(layouted)
        {
            _layoutType = layoutType;
        }

        public override void Resize(int x, int y)
        {

        }

        public override void Pack()
        {
            foreach (AbstractLayoutable l in Layotables)
            {
                if (l.DoPack())
                {
                    l.Pack();
                }
            
            }

            if (_layoutType is VBoxLayoutType)
            {
                for (int i = 0; i < Layotables.Count - 1; i++)
                {
                    int y = Layotables[i].VirtualY + Layotables[i].ComputedHeight;
                    Layotables[i + 1].TranslateToY(y);
                }
            }
            else
            {
                for (int i = 0; i < Layotables.Count - 1; i++)
                {
                    int x = Layotables[i].VirtualX + Layotables[i].ComputedWidth;
                    Layotables[i + 1].TranslateToX(x);
                }
            }
        }
    }
}
