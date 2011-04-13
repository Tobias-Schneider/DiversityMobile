using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using UBT.AI4.Bio.DivMobi.UMF.Context.Actions;
using UBT.AI4.Bio.DivMobi.UMF.Context.Policy;
using UBT.AI4.Bio.DivMobi.UMF.Layout;

namespace UBT.AI4.Bio.DivMobi.UMF.Context
{
    public interface StringParser
    {
        Object ParseString(String valueString, Type valueType, out bool parsed);
    }

    public class StringParserRegistry : StringParser
    {
        private static StringParserRegistry _instance = new StringParserRegistry();
        private List<StringParser> _parsers = new List<StringParser>();

        private StringParserRegistry()
        {
            _parsers.Add(new BasicStringParser());
        }

        public static StringParserRegistry Instance 
        {
            get { return _instance; }
        }

        public Object ParseString(String valueString, Type valueType, out bool parsed)
        {
            parsed = false;
            foreach (StringParser parser in _parsers)
            {
                Object tmp = parser.ParseString(valueString, valueType, out parsed);
                if (parsed)
                {
                    return tmp;
                }
            }
            return null;
        }

        public void Register(StringParser parser)
        {
            if (!_parsers.Contains(parser))
            {
                _parsers.Add(parser);
            }
        }

        public void UnRegister(StringParser parser)
        {
            _parsers.Remove(parser);
        }

        private class BasicStringParser : StringParser
        {
            public Object ParseString(String valueString, Type valueType, out bool parsed)
            {
                parsed = true;

                if (valueString == null)
                {
                    return null;
                }

                if (valueType == typeof(String))
                {
                    return valueString;
                }
                if (valueType == typeof(Int16))
                {
                    return Int16.Parse(valueString);
                }
                if (valueType == typeof(Int32))
                {
                    return Int32.Parse(valueString);
                }
                if (valueType == typeof(Int64))
                {
                    return Int64.Parse(valueString);
                }
                if (valueType == typeof(UInt16))
                {
                    return UInt16.Parse(valueString);
                }
                if (valueType == typeof(UInt32))
                {
                    return UInt32.Parse(valueString);
                }
                if (valueType == typeof(UInt64))
                {
                    return UInt64.Parse(valueString);
                }
                if (valueType == typeof(Single))
                {
                    return Single.Parse(valueString);
                }
                if (valueType == typeof(Double))
                {
                    return Double.Parse(valueString);
                }
                if (valueType == typeof(Boolean))
                {
                    return Boolean.Parse(valueString);
                }

                parsed = false;

                return null;
            }
        }
    }

    public class Configurator
    {
        public static void Configure(IClassConfiguration conf, Object o) {
            Type type = Type.GetType(conf.ClassName);
            if (o.GetType() != type)
            {
                //dürfte nicht passieren ...
                throw new Exception();
            }

            

            foreach (IFieldDescriptor fd in conf.FieldDesctiptors)
            {
                FieldInfo fi = type.GetField(fd.FieldName, 
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Instance);

                if (fi == null)
                {
                    throw new ContextRuntimeException("Field '"+fd.FieldName+"' not found in type '"+type.AssemblyQualifiedName+"'");
                }

                Object val = fi.GetValue(o);

                if (val == null)
                {
                    return;
                }

                foreach (ICustomAction ca in fd.CustomActions)
                {
                    String actionId = ca.ActionId;

                    IAction action = ActionRegistry.Instance.GetAction(actionId);

                    if (action == null)
                    {
                        throw new ContextRuntimeException("Action '"+actionId+"' not registered: type '"+type.AssemblyQualifiedName+"' -> field +'"+fd.FieldName+"'");
                    }

                    action.Perform(o, fi, val, ca.Parameter);
                }

                foreach (IInvokedMethod im in fd.InvokedMethods)
                {
                    PermissionLevel p = PolicyRegistry.Instance.GetInvocationPermissionLevel(val.GetType(), im.MethodName);
                    if (p != PermissionLevel.PERMITTED)
                    {
                        throw new Exception();
                    }

                    MethodInfo mi = val.GetType().GetMethod(im.MethodName);
                    if (mi == null)
                    {
                        throw new ContextRuntimeException("Method '" + im.MethodName + "' not found: type '" + type.AssemblyQualifiedName + "' -> field +'" + fd.FieldName + "'");
                    }

                    mi.Invoke(val, null);
                }

                foreach (IModifiedProperty mp in fd.ModifiedProperties)
                {
                    PermissionLevel p = PolicyRegistry.Instance.GetInvocationPermissionLevel(val.GetType(), mp.PropertyName);
                    if (p != PermissionLevel.PERMITTED)
                    {
                        throw new Exception();
                    }

                    PropertyInfo pi = val.GetType().GetProperty(mp.PropertyName);
                    if (pi == null)
                    {
                        throw new ContextRuntimeException("Property '" + mp.PropertyName + "' not found: type '" + type.AssemblyQualifiedName + "' -> field +'" + fd.FieldName + "'");
                    }

                    bool parsed;
                    Object tmp = StringParserRegistry.Instance.ParseString(mp.Value, pi.PropertyType, out parsed);

                    if (!parsed)
                    {
                        throw new ContextRuntimeException("Could not convert parameter '"+mp.Value+"' to '"+pi.PropertyType.AssemblyQualifiedName+"': property '" + mp.PropertyName + "' of type '" + type.AssemblyQualifiedName + "' -> field +'" + fd.FieldName + "'");
                    }

                    pi.SetValue(val, tmp, null);
                }
            }


            if (o is ILayouted)
            {
                ((ILayouted)o).Layout.Pack();
            }
            //List<ILayout> ls = ContextManager.Instance.GetLayouts(o);
            //if (ls != null)
            //{
            //    foreach (ILayout l in ls)
            //    {
            //        l.Pack();
            //    }
            //}
        }
    }
}
