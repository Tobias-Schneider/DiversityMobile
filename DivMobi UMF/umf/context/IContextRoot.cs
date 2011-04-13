using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace UBT.AI4.Bio.DivMobi.UMF.Context
{
    public interface IContextRoot : IConfigurationElement
    {
        IList<IContext> Contexts { get; }
    }

    public interface IContext : IConfigurationElement
    {
        String ContextId { get; }

        IList<IClassConfiguration> ClassConfiguration { get; }

        void Configure(Object o);
    }

    public interface IClassConfiguration : IConfigurationElement
    {

        String ClassName { get; }

        IList<IFieldDescriptor> FieldDesctiptors { get; }

        void Configure(Object o);
    }

    public interface IFieldDescriptor : IConfigurationElement
    {
        String FieldName { get; }

        IClassConfiguration ParentClassConfiguration { get; }

        IList<ICustomAction> CustomActions { get; }

        IList<IInvokedMethod> InvokedMethods { get; }

        IList<IModifiedProperty> ModifiedProperties { get; }

        bool Evict { get; }
    }

    public interface ICustomAction : IConfigurationElement
    {
        IFieldDescriptor ParentFieldDescriptor { get; }
        String ActionId { get; }
        String Parameter { get; }
    }

    public interface IModifiedProperty : IConfigurationElement
    {
        IFieldDescriptor ParentFieldDescriptor { get; }
        String PropertyName { get; }
        String Value { get; }
    }

    public interface IInvokedMethod : IConfigurationElement
    {
        IFieldDescriptor ParentFieldDescriptor { get; }
        String MethodName { get; }
    }

    public interface IConfigurationElementVisitor
    {
        void Visit(IContextRoot contextRoot);
        void Visit(IContext context);
        void Visit(IClassConfiguration classConfiguration);
        void Visit(IFieldDescriptor fieldDescriptor);
        void Visit(ICustomAction customAction);
        void Visit(IModifiedProperty modifiedProperty);
        void Visit(IInvokedMethod invokedMethod);
    }

    public interface IConfigurationElement
    {
        void Accept(IConfigurationElementVisitor visitor);
    }

    public class ConfigurationCheckVisitor : IConfigurationElementVisitor 
    {
        public void Visit(IContextRoot contextRoot)
        {

        }

        public void Visit(IContext context)
        {
            if (context.ContextId == null)
            {
                throw new ContextConfigurationException("IContext without definition of 'ContextId' found");
            }
        }

        public void Visit(IClassConfiguration classConfiguration)
        {
            if(classConfiguration.ClassName == null) 
            {
                throw new ContextConfigurationException("IClassConfiguration without definition of 'ClassName' found");
            }
            Type t = Type.GetType(classConfiguration.ClassName);
            if (t == null)
            {
                throw new ContextConfigurationException("Type '"+classConfiguration.ClassName+"' not found");
            }
        }

        public void Visit(IFieldDescriptor fieldDescriptor)
        {
            if(fieldDescriptor.FieldName == null) 
            {
                throw new ContextConfigurationException("IFieldDescriptor without definition of 'FieldName' found");
            }

            IClassConfiguration parent = fieldDescriptor.ParentClassConfiguration;
            FieldInfo f = GetFieldInfo(fieldDescriptor, parent);
            if (f == null)
            {
                throw new ContextConfigurationException("Field '" + fieldDescriptor.FieldName + "' not found in class '" + parent.ClassName+"'");
            }
        }

        public void Visit(ICustomAction customAction)
        {
            if (customAction.ActionId == null)
            {
                throw new ContextConfigurationException("ICustomAction without definition of 'ActionId' found");
            }   
        }

        public void Visit(IModifiedProperty modifiedProperty)
        {
            if (modifiedProperty.PropertyName == null)
            {
                throw new ContextConfigurationException("IModifiedProperty without definition of 'PropertyName' found");
            }
            IFieldDescriptor p = modifiedProperty.ParentFieldDescriptor;
            FieldInfo f = GetFieldInfo(p, p.ParentClassConfiguration);
            Type t = f.FieldType;
            PropertyInfo pi = t.GetProperty(modifiedProperty.PropertyName,
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);

            if (pi == null)
            {
                throw new ContextConfigurationException("Fieldtype of field '" + p.FieldName + "' in class '" + p.ParentClassConfiguration.ClassName + "' does not define property '" + modifiedProperty.PropertyName + "'");
            }
        }

        public void Visit(IInvokedMethod invokedMethod)
        {
            if (invokedMethod.MethodName == null)
            {
                throw new ContextConfigurationException("InvokedMethod without definition of 'MethodName' found");
            }
            IFieldDescriptor p = invokedMethod.ParentFieldDescriptor;
            FieldInfo f = GetFieldInfo(p, p.ParentClassConfiguration);
            Type t = f.FieldType;
            MethodInfo m = t.GetMethod(invokedMethod.MethodName,
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);

            if (m == null)
            {
                throw new ContextConfigurationException("Fieldtype of field '"+ p.FieldName + "' in class '"+p.ParentClassConfiguration.ClassName+"' does not define method '" + invokedMethod.MethodName + "'");
            }
        }

        private static FieldInfo GetFieldInfo(IFieldDescriptor fieldDescriptor, IClassConfiguration parent)
        {
            Type t = Type.GetType(parent.ClassName);
            FieldInfo f = t.GetField(fieldDescriptor.FieldName,
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);
            return f;
        }
    }


}
