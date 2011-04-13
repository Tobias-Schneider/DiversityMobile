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

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public class VirtualContainer : AbstractLayoutable
    {
        private IList<AbstractLayoutable> _layoutables;
        private LayoutManager _layoutManager;
        private int _bottomSpace;
        private int _rightSpace;

        public VirtualContainer(AbstractLayoutable parent)
            : base(parent)
        {
            _layoutables = new List<AbstractLayoutable>();
        }

        public void Add(AbstractLayoutable l)
        {
            _layoutables.Add(l);
        }

        public int Count
        {
            get
            {
                return _layoutables.Count;
            }
        }

        public void Resize(int x, int y)
        {
            _layoutManager.Resize(x, y);
        }

        public override void Pack()
        {
            _layoutManager.Pack();
        }

        public override void Remove(Control control)
        {
            //List<AbstractLayoutable> removed = new List<AbstractLayoutable>();
            foreach (AbstractLayoutable asl in _layoutables)
            {
                asl.Remove(control);
                //if (asl is ControlAdapter)
                //{
                //    if (((ControlAdapter)asl).Control == c)
                //    {
                //        removed.Add(asl);
                //    }
                //}
                //else if (asl is VirtualContainer)
                //{
                //    ((VirtualContainer)asl).RemoveControl(c);
                //}
            }

            //foreach (AbstractLayoutable asl in removed)
            //{
            //    _layoutables.Remove(asl);
            //}
        }

        public IList<AbstractLayoutable> Layoutables
        {
            get
            {
                return _layoutables;
            }

            set
            {
                _layoutables = value;
            }
        }

        public void RemoveControl(Control c)
        {
            List<AbstractLayoutable> removed = new List<AbstractLayoutable>();
            foreach (AbstractLayoutable asl in _layoutables)
            {
                if (asl is ControlAdapter)
                {
                    if (((ControlAdapter)asl).Control == c)
                    {
                        removed.Add(asl);
                    }
                }
                else if (asl is VirtualContainer)
                {
                    ((VirtualContainer)asl).RemoveControl(c);
                }
            }

            foreach (AbstractLayoutable asl in removed)
            {
                _layoutables.Remove(asl);
            }
        }


        internal void ComputeBottomSpace()
        {
            int h = 0;

            foreach (AbstractLayoutable l in _layoutables)
            {
                if (l.VirtualY + l.VirtualHeight > h)
                {
                    h = l.VirtualY + l.VirtualHeight;
                }
                if (l is VirtualContainer)
                {
                    ((VirtualContainer)l).ComputeBottomSpace();
                }
            }
            _bottomSpace = this.VirtualHeight - h;
        }

        internal void ComputeRightSpace()
        {
            int w = 0;

            foreach (AbstractLayoutable l in _layoutables)
            {
                if (l.VirtualX + l.VirtualWidth > w)
                {
                    w = l.VirtualX + l.VirtualWidth;
                }
                if (l is VirtualContainer)
                {
                    ((VirtualContainer)l).ComputeRightSpace();
                }
            }
            _rightSpace = this.VirtualWidth - w;
        }



        public override int ComputedHeight
        {
            get
            {
                int h = 0;
                foreach (AbstractLayoutable l in _layoutables)
                {
                    if (l.VirtualY + l.ComputedHeight > h && l.ComputedHeight > 0)
                    {
                        h = l.VirtualY + l.ComputedHeight;
                    }
                }

                if (h > 0) h = h + _bottomSpace;

                return h;
            }
        }

        public override int ComputedWidth
        {
            get
            {
                int w = 0;
                foreach (AbstractLayoutable l in _layoutables)
                {
                    if (l.VirtualX + l.ComputedWidth > w && l.ComputedWidth > 0)
                    {
                        w = l.VirtualX + l.ComputedWidth;
                    }
                }

                if (w > 0) w = w + _rightSpace;

                return w;
            }
        }

        public override void TranslateToY(int y)
        {
            VirtualY = y;
            foreach (AbstractLayoutable l in _layoutables)
            {
                l.TranslateToY(l.VirtualY);
            }
        }

        public override void TranslateToX(int x)
        {
            VirtualX = x;
            foreach (AbstractLayoutable l in _layoutables)
            {
                l.TranslateToX(l.VirtualX);
            }
        }

        public LayoutManager LayoutManager
        {
            set
            {
                this._layoutManager = value;
            }
        }

        public override bool Visible
        {
            get
            {
                foreach (AbstractLayoutable asl in _layoutables)
                {
                    if (asl.Visible) return true;
                }
                return false;
            }
        }
    }

    internal class TopLevelVirtualContainer : VirtualContainer
    {
        private List<ICustomLayoutControl> _customLayoutControls = new List<ICustomLayoutControl>();
        private bool active = false;
        private bool scheduled = false;

        public TopLevelVirtualContainer(AbstractLayoutable parent)
            : base(parent)
        {

        }



        public override void Pack()
        {
            if (!active)
            {
                scheduled = true;
            }

            foreach (ICustomLayoutControl c in _customLayoutControls)
            {
                c.PrePack();
            }

            base.Pack();

            foreach (ICustomLayoutControl c in _customLayoutControls)
            {
                c.PostPack();
            }
        }


        public IList<ICustomLayoutControl> CustomLayoutControls
        {
            get { return _customLayoutControls; }
        }

        public void Activated(object sender, EventArgs e)
        {
            active = true;
            if (scheduled)
            {
                Pack();
                scheduled = false;
            }
        }
    }
}
