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
