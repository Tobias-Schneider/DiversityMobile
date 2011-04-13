using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace UBT.AI4.Bio.DivMobi.UMF.Context
{
    [XmlRoot(ElementName = "ContextRoot")]
    public class XMLContextRoot : IContextRoot
    {
        private List<XMLContext> _contexts = new List<XMLContext>();

        [XmlElement(ElementName = "Context")]
        public List<XMLContext> Contexts
        {
            get { return _contexts; }
            set { _contexts = value; }
        }

        IList<IContext> IContextRoot.Contexts
        {
            get { return _contexts.Cast<IContext>().ToList(); }
        }

        public void Accept(IConfigurationElementVisitor visitor)
        {
            visitor.Visit(this);
            foreach (XMLContext c in _contexts)
            {
                c.Accept(visitor);
            }
        }
    }

    public class XMLContext : IContext
    {
        private String _contextId;
        private List<XMLClassConfiguration> _classConfigurations = new List<XMLClassConfiguration>();

        [XmlAttribute]
        public String ContextId
        {
            get { return _contextId; }
            set { _contextId = value; }
        }

        [XmlElement("ClassConfiguration")]
        public List<XMLClassConfiguration> ClassConfiguration
        {
            get { return _classConfigurations; }
            set { _classConfigurations = value; }
        }

        IList<IClassConfiguration> IContext.ClassConfiguration
        {
            get { return _classConfigurations.Cast<IClassConfiguration>().ToList(); }
        }

        public void Configure(Object o)
        {
            foreach (IClassConfiguration cf in _classConfigurations)
            {
                Type tmp = Type.GetType(cf.ClassName);
                if (o.GetType().Equals(tmp))
                {
                    cf.Configure(o);
                }
            }
        }

        public void Accept(IConfigurationElementVisitor visitor)
        {
            visitor.Visit(this);
            foreach (XMLClassConfiguration c in _classConfigurations)
            {
                c.Accept(visitor);
            }
        }
    }

    public class XMLClassConfiguration : IClassConfiguration
    {
        private XMLContext _parentContext;
        private String _className;
        private List<XMLFieldDescriptor> _fieldDescriptors = new List<XMLFieldDescriptor>();

        [XmlIgnore]
        internal XMLContext ParentContext
        {
            get { return _parentContext; }
            set { _parentContext = value; }
        }

        [XmlAttribute]
        public String ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        [XmlElement("FieldDescriptor")]
        public List<XMLFieldDescriptor> FieldDesctiptors
        {
            get { return _fieldDescriptors; }
            set { _fieldDescriptors = value; }
        }

        IList<IFieldDescriptor> IClassConfiguration.FieldDesctiptors
        {
            get { return _fieldDescriptors.Cast<IFieldDescriptor>().ToList(); }
        }

        public void Configure(Object o)
        {
            Configurator.Configure(this, o);
        }

        public void Accept(IConfigurationElementVisitor visitor)
        {
            visitor.Visit(this);
            foreach (XMLFieldDescriptor fd in _fieldDescriptors)
            {
                fd.Accept(visitor);
            }
        }
    }

    public class XMLFieldDescriptor : IFieldDescriptor
    {
        private XMLClassConfiguration _parentClassConfiguration;
        private String _fieldName;
        private bool _evict = false;
        private List<XMLCustomAction> _customActions = new List<XMLCustomAction>();
        private List<XMLInvokedMethod> _invokedMethods = new List<XMLInvokedMethod>();
        private List<XMLModifiedProperty> _modifiedProperties = new List<XMLModifiedProperty>();


        [XmlIgnore]
        internal XMLClassConfiguration ParentClassConfiguration
        {
            get { return _parentClassConfiguration; }
            set { _parentClassConfiguration = value; }
        }


        [XmlAttribute]
        public String FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        [XmlAttribute]
        public bool Evict
        {
            get { return _evict; }
            set { _evict = value; }
        }

        [XmlElement(ElementName = "Action")]
        public List<XMLCustomAction> CustomActions
        {
            get { return _customActions; }
            set { _customActions = value; }
        }

        [XmlElement(ElementName = "ModifiedProperty")]
        public List<XMLModifiedProperty> ModifiedProperties
        {
            get { return _modifiedProperties; }
            set { _modifiedProperties = value; }
        }

        [XmlElement(ElementName = "InvokedMethod")]
        public List<XMLInvokedMethod> InvokedMethods
        {
            get { return _invokedMethods; }
            set { _invokedMethods = value; }
        }

        IClassConfiguration IFieldDescriptor.ParentClassConfiguration
        {
            get { return _parentClassConfiguration; }
        }

        IList<ICustomAction> IFieldDescriptor.CustomActions
        {
            get { return _customActions.Cast<ICustomAction>().ToList(); }
        }

        IList<IModifiedProperty> IFieldDescriptor.ModifiedProperties
        {
            get { return _modifiedProperties.Cast<IModifiedProperty>().ToList(); }
        }

        IList<IInvokedMethod> IFieldDescriptor.InvokedMethods
        {
            get { return _invokedMethods.Cast<IInvokedMethod>().ToList(); }
        }

        public void Accept(IConfigurationElementVisitor visitor)
        {
            visitor.Visit(this);
            foreach (XMLCustomAction ca in _customActions)
            {
                ca.Accept(visitor);
            }
            foreach (XMLModifiedProperty mp in _modifiedProperties)
            {
                mp.Accept(visitor);
            }
            foreach (XMLInvokedMethod im in _invokedMethods)
            {
                im.Accept(visitor);
            }
        }
    }

    public class XMLCustomAction : ICustomAction
    {
        private XMLFieldDescriptor _parentFieldDescriptor;
        private String _actionId;
        private String _parameter;

        [XmlIgnore]
        internal XMLFieldDescriptor ParentFieldDescriptor
        {
            get { return _parentFieldDescriptor; }
            set { _parentFieldDescriptor = value; }
        }

        [XmlAttribute]
        public String ActionId
        {
            get { return _actionId; }
            set { _actionId = value; }
        }

        [XmlAttribute]
        public String Parameter
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

        public void Accept(IConfigurationElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        IFieldDescriptor ICustomAction.ParentFieldDescriptor
        {
            get { return _parentFieldDescriptor; }
        }
    }

    public class XMLModifiedProperty : IModifiedProperty
    {
        private XMLFieldDescriptor _parentFieldDescriptor;
        private String _propertyName;
        private String _value;


        [XmlIgnore]
        internal XMLFieldDescriptor ParentFieldDescriptor
        {
            get { return _parentFieldDescriptor; }
            set { _parentFieldDescriptor = value; }
        }

        public String PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        public String Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void Accept(IConfigurationElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        IFieldDescriptor IModifiedProperty.ParentFieldDescriptor
        {
            get { return _parentFieldDescriptor; }
        }
    }

    public class XMLInvokedMethod : IInvokedMethod
    {
        private XMLFieldDescriptor _parentFieldDescriptor;
        private String _methodName;

        [XmlIgnore]
        internal XMLFieldDescriptor ParentFieldDescriptor
        {
            get { return _parentFieldDescriptor; }
            set { _parentFieldDescriptor = value; }
        }

        public String MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        public void Accept(IConfigurationElementVisitor visitor)
        {
            visitor.Visit(this);
        }

        IFieldDescriptor IInvokedMethod.ParentFieldDescriptor
        {
            get { return _parentFieldDescriptor; }
        }
    }



    internal class XMLParentInitializationVisitor : IConfigurationElementVisitor
    {
        public void Visit(IContextRoot contextRoot)
        {

        }

        public void Visit(IContext context)
        {
            if (context is XMLContext)
            {
                foreach (IClassConfiguration c in context.ClassConfiguration)
                {
                    if (c is XMLClassConfiguration)
                    {
                        ((XMLClassConfiguration)c).ParentContext = (XMLContext)context;
                    }
                }
            }
        }

        public void Visit(IClassConfiguration classConfiguration)
        {
            if (classConfiguration is XMLClassConfiguration)
            {
                foreach (IFieldDescriptor f in classConfiguration.FieldDesctiptors)
                {
                    if (f is XMLFieldDescriptor)
                    {
                        ((XMLFieldDescriptor)f).ParentClassConfiguration = (XMLClassConfiguration)classConfiguration;
                    }
                }
            }
        }

        public void Visit(IFieldDescriptor fieldDescriptor)
        {
            if (fieldDescriptor is XMLFieldDescriptor)
            {
                foreach (ICustomAction x in fieldDescriptor.CustomActions)
                {
                    if (x is XMLCustomAction)
                    {
                        ((XMLCustomAction)x).ParentFieldDescriptor = (XMLFieldDescriptor)fieldDescriptor;
                    }
                }
                foreach (IInvokedMethod x in fieldDescriptor.InvokedMethods)
                {
                    if (x is XMLInvokedMethod)
                    {
                        ((XMLInvokedMethod)x).ParentFieldDescriptor = (XMLFieldDescriptor)fieldDescriptor;
                    }
                }
                foreach (IModifiedProperty x in fieldDescriptor.ModifiedProperties)
                {
                    if (x is XMLModifiedProperty)
                    {
                        ((XMLModifiedProperty)x).ParentFieldDescriptor = (XMLFieldDescriptor)fieldDescriptor;
                    }
                }

            }
        }

        public void Visit(ICustomAction customAction)
        {

        }

        public void Visit(IModifiedProperty modifiedProperty)
        {

        }

        public void Visit(IInvokedMethod invokedMethod)
        {

        }
    }

    public class XMLConfigurator
    {
        public static void Configure(String fileName)
        {
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(XMLContextRoot));
                FileStream fs = File.Open(fileName, FileMode.Open);
                IContextRoot r = (IContextRoot)s.Deserialize(fs);
                fs.Close();

                IConfigurationElementVisitor v = new XMLParentInitializationVisitor();
                r.Accept(v);

                v = new ConfigurationCheckVisitor();
                r.Accept(v);

                foreach (IContext context in r.Contexts)
                {
                    ContextManager.Instance.RegisterContext(context);
                }
            }
            catch (Exception ex)
            {
                throw new ContextCorruptedException("Context couldn't be configured. (" + ex.Message + ")");
            }
        }
    }
}