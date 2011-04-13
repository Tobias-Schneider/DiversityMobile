using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.umf.initializer.attributes;
using System.Reflection;

namespace UBT.AI4.Bio.DivMobi.umf.initializer
{
    public class ControlConfigurator
    {
        public void ConfigureControl(Control control, Object dataItem)
        {
            FieldInfo[] fis = control.GetType().GetFields(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);

            foreach (FieldInfo fi in fis)
            {
                ConnectionAttribute a = (ConnectionAttribute)Attribute.GetCustomAttribute(fi, typeof(ConnectionAttribute));

                if (a != null && a.ConnectedType.Equals(dataItem.GetType()))
                {
                    Control c = (Control)fi.GetValue(control);

                    if ((c == null) ||
                        (!c.Visible && !a.CopyOnInvisible()) ||
                        (!c.Enabled && !a.CopyOnDisabled()))
                    {
                        continue;                     
                    }
                    else
                    {
                        CopyProperty(dataItem, a, c);
                    }
                }
            }
        }

        private static void CopyProperty(Object dataItem, ConnectionAttribute a, Control c)
        {
            PropertyInfo piSource = null;
            PropertyInfo piTarget = null;
            String prop = a.DataItemProperty;
            try
            {
                piSource = dataItem.GetType().GetProperty(prop);
            }
            catch (Exception ex)
            {
                throw new InitializerException(
                    new StringBuilder("Source property '").
                    Append(prop).
                    Append("' not found").ToString(),
                    ex);
            }

            if (piSource == null)
            {
                throw new InitializerException(
                    new StringBuilder("Source property '").
                    Append(prop).
                    Append("' not found").ToString());
            }

            prop = a.ControlProperty;
            try
            {
                piTarget = c.GetType().GetProperty(prop);
            }
            catch (Exception ex)
            {
                throw new InitializerException(
                    new StringBuilder("Target property '").
                    Append(prop).
                    Append("' not found").ToString(),
                    ex);
            }

            if (piTarget == null)
            {
                throw new InitializerException(
                    new StringBuilder("Target property '").
                    Append(prop).
                    Append("' not found").ToString());
            }

            Object val = piSource.GetGetMethod().Invoke(dataItem, new Object[] { });

            Type vct = a.ValueConverter;
            ConstructorInfo ci = vct.GetConstructor(new Type[] { });
            IValueConverter vc = (IValueConverter)ci.Invoke(new Object[] { });
            val = vc.ConvertForControl(val);

            piTarget.GetSetMethod().Invoke(c, new Object[] { val });
        }
    }
}
