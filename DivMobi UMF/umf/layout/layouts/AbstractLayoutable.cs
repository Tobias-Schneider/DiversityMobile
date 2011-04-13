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

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public abstract class AbstractLayoutable : ILayout
    {
        private AbstractLayoutable _parent;
        private int _virtualX;
        private int _virtualY;
        private int _virtualWidth;
        private int _virtualHeight;
        private bool _grouped = false;
        private ICustomLayoutControl _customLayoutControl = null;

        public AbstractLayoutable(AbstractLayoutable parent)
        {
            this._parent = parent;
            this._virtualX = 0;
            this._virtualY = 0;
        }

        public AbstractLayoutable Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public int AbsoluteX
        {
            get
            {
                if (_parent != null)
                {
                    return this._virtualX + _parent.AbsoluteX;
                }
                else
                {
                    return this._virtualX;
                }
            }
        }

        public int AbsoluteY
        {
            get
            {
                if (_parent != null)
                {
                    return this._virtualY + _parent.AbsoluteY;
                }
                else
                {
                    return this._virtualY;
                }
            }
        }

        public int VirtualX
        {
            get { return _virtualX; }
            set { _virtualX = value; }
        }

        public int VirtualY
        {
            get { return _virtualY; }
            set { _virtualY = value; }
        }

        public int VirtualWidth
        {
            get { return _virtualWidth; }
            set { _virtualWidth = value; }
        }

        public int VirtualHeight
        {
            get { return _virtualHeight; }
            set { _virtualHeight = value; }
        }

        public abstract int ComputedHeight { get; }

        public abstract int ComputedWidth { get; }

        public abstract void TranslateToY(int y);

        public abstract void TranslateToX(int x);

        public abstract bool Visible { get; }

        public bool DoPack()
        {
            if (!Visible) return false;

            if (_customLayoutControl != null)
            {
                return _customLayoutControl.DoLayout();
            }

            return !Grouped;
        }

        public abstract void Pack();

        public abstract void Remove(Control control);

        public bool Grouped
        {
            get { return _grouped; }
            set { _grouped = value; }
        }

        public ICustomLayoutControl CustomLayoutControl
        {
            get { return _customLayoutControl; }
            set { _customLayoutControl = value; }
        }
    }
}
