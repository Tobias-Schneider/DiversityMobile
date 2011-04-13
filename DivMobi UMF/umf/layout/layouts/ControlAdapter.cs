using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace UBT.AI4.Bio.DivMobi.UMF.Layout.Layouts
{
    public class ControlAdapter : AbstractLayoutable
    {
        private Control _control;
        private bool _horizontalFixed = false;
        private bool _verticalFixed = false;

        public ControlAdapter(AbstractLayoutable parent, Control c)
            : base(parent)
        {
            this._control = c;
        }

        public override int ComputedHeight
        {
            get
            {
                if (_control == null)
                {
                    return 0;
                }
                if (_control.Visible)
                {
                    return _control.Height;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override int ComputedWidth
        {
            get
            {
                if (_control == null)
                {
                    return 0;
                }
                if (_control.Visible)
                {
                    int xOff = 0;
                    if (_horizontalFixed)
                    {
                        xOff = _control.Location.X - AbsoluteX;
                    }

                    return _control.Width+xOff;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override void TranslateToY(int y)
        {
            if (_control != null && !_verticalFixed)
            {
                Point p = _control.Location;
                p.Y = AbsoluteY;
                _control.Location = p;
            }
        }

        public override void TranslateToX(int x)
        {
            if (_control != null && !_horizontalFixed)
            {
                Point p = _control.Location;
                p.X = AbsoluteX;
                _control.Location = p;

                //int tmp = p.X;
                //p.X = AbsoluteX;
                //tmp = tmp - p.X;
                //_control.Width = _control.Width + tmp;
            }
        }

        public override bool Visible
        {
            get {
                if (_control == null) return false;
                return _control.Visible; 
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            if (this._control == null)
            {
                return this._control == obj;
            }

            return this._control.Equals(((ControlAdapter)obj)._control);
        }

        public override int GetHashCode()
        {
            if (this._control == null)
            {
                return 0;
            }

            return this._control.GetHashCode();
        }

        internal Control Control
        {
            get { return _control; }
        }

        public override void Pack()
        {

        }

        public override void Remove(Control control)
        {
            if (this._control == control)
            {
                _control = null;
            }
        }

        public bool HorizontalFix
        {
            get { return _horizontalFixed; }
            set { _horizontalFixed = value; }
        }

        public bool VerticalFix
        {
            get { return _verticalFixed; }
            set { _verticalFixed = value; }
        }
    }

    public class PanelAdapter : ControlAdapter
    {
        private ILayout _layout;

        public PanelAdapter(AbstractLayoutable parent, Type layoutType, Panel panel) : base(parent, panel)
        {
            if (layoutType != null)
            {
                _layout = LayoutFactory.Instance.CreateLayout(layoutType, panel);
            }
        }

        public override void Pack()
        {
            if (_layout != null)
            {
                _layout.Pack();
                this.Control.Width = _layout.ComputedWidth;
                this.Control.Height = _layout.ComputedHeight;
            }
            base.Pack();
        }

        public override void Remove(Control control)
        {
            if (_layout != null)
            {
                _layout.Remove(control);
            }
            if (this.Control == control)
            {
                _layout = null;
            }
            base.Remove(control);
        }
    }
}
