using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using UBT.AI4.Bio.DivMobi.umf.initializer.attributes;

namespace UBT.AI4.Bio.DivMobi.umf.initializer
{
    public class DataItemFactory
    {
        public T CreateDataItem<T>(Control control) where T : new()
        {
            FieldInfo[] fis = control.GetType().GetFields(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);

            T t = new T();

            foreach (FieldInfo fi in fis)
            {
                ConnectionAttribute a =  (ConnectionAttribute)Attribute.GetCustomAttribute(fi, typeof(ConnectionAttribute));

                if (a != null && a.ConnectedType.Equals(t.GetType()))
                {
                    Control c = (Control)fi.GetValue(control);

                    if ((c == null) ||
                        (!c.Visible && !a.CopyOnInvisible()) ||
                        (!c.Enabled && !a.CopyOnDisabled()))
                    {
                        if (a.DefaultValueProvider != null)
                        {
                            CopyDefaultValue<T>(t, a);
                        }
                    }
                    else
                    {
                        CopyProperty<T>(t, a, c);
                    }
                }
            }

            return t;
        }

        private static void CopyDefaultValue<T>(T t, ConnectionAttribute a) where T : new()
        {


            Type dvpt = a.DefaultValueProvider;
            IDefaultValueProvider dvp = null;
            
            MethodInfo mi = dvpt.GetMethod("GetInstance", BindingFlags.Static | BindingFlags.Public);
            if (mi != null)
            {
                dvp = (IDefaultValueProvider)mi.Invoke(null, new Object[] { });
            }
            else
            {
                ConstructorInfo ci = dvpt.GetConstructor(new Type[] { });
                dvp = (IDefaultValueProvider)ci.Invoke(new Object[] { });
            }
            
            Object val = dvp.GetDefaultValue();
            PropertyInfo targetProperty = RetrieveTargetProperty<T>(t, a);
            targetProperty.GetSetMethod().Invoke(t, new Object[] { val });
        }

        private static void CopyProperty<T>(T t, ConnectionAttribute a, Control c) where T : new()
        {
            PropertyInfo piSource = RetrieveSourceProperty(a, c);
            PropertyInfo piTarget = RetrieveTargetProperty<T>(t, a);

            Object val = piSource.GetGetMethod().Invoke(c, new Object[] { });
            Type vct = a.ValueConverter;
            ConstructorInfo ci = vct.GetConstructor(new Type[] { });
            IValueConverter vc = (IValueConverter)ci.Invoke(new Object[] { });
            val = vc.ConvertForDataItem(val);

            piTarget.GetSetMethod().Invoke(t, new Object[] { val });
        }

        private static PropertyInfo RetrieveSourceProperty(ConnectionAttribute a, Control c)
        {
            PropertyInfo piSource = null;
            String prop = a.ControlProperty;
            try
            {
                piSource = c.GetType().GetProperty(prop);
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
            return piSource;
        }

        private static PropertyInfo RetrieveTargetProperty<T>(T t, ConnectionAttribute a) where T : new()
        {
            PropertyInfo piTarget = null;
            String prop = a.DataItemProperty;
            try
            {
                piTarget = t.GetType().GetProperty(prop);
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
            return piTarget;
        }
    }
}
