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
