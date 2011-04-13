using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util
{
    public class GenericWeakReference<T>
    {
        private WeakReference _r;
        private SortedList<String, Object> _properties;
        
        public GenericWeakReference(T target)
        {
            _properties = new SortedList<String, Object>();
            _r = new WeakReference(target, false);
        }

        public T Target
        {
            get { return (T)_r.Target; }
        }

        public bool IsAlive
        {
            get { return _r.IsAlive; }
        }

        public SortedList<String, Object> Properties { get { return _properties; } }
    }
}
