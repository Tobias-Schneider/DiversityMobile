using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util
{
    public class GenericWeakReferenceSet<T> : IEnumerable<T>
    {
        private List<GenericWeakReference<T>> _referencedObjects = new List<GenericWeakReference<T>>();
        private List<IEnumerator> _enumerators = new List<IEnumerator>();
        public void Add(T element)
        {
            InvalidateEnumerators();

            List<GenericWeakReference<T>> remove = new List<GenericWeakReference<T>>();

            foreach (GenericWeakReference<T> r in _referencedObjects)
            {
                T t = r.Target;

                if (t != null)
                {
                    if (Object.ReferenceEquals(t, element)) return;
                }
                else
                {
                    remove.Add(r);
                }
            }

            _referencedObjects.Add(new GenericWeakReference<T>(element));
            

            foreach (GenericWeakReference<T> r in remove)
            {
                _referencedObjects.Remove(r);
            }
        }

        public bool Remove(T element)
        {
            InvalidateEnumerators();

            foreach (GenericWeakReference<T> r in _referencedObjects)
            {
                T t = r.Target;

                if (t != null)
                {
                    if (Object.ReferenceEquals(t, element))
                    {
                        return _referencedObjects.Remove(r);
                    }
                }
            }

            return false;
        }

        public int Count { get { return _referencedObjects.Count; } }

        public bool Contains(T element) {
            foreach (GenericWeakReference<T> r in _referencedObjects)
            {
                T t = r.Target;
                if (t != null)
                {
                    if (Object.ReferenceEquals(element, t))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public T this[int index] 
        {
            get 
            {
                return _referencedObjects[index].Target;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> e = new EnumeratorImpl<T>(this);
            _enumerators.Add(e);
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void RemoveEnumerator(IEnumerator e)
        {
            _enumerators.Remove(e);
        }

        private void InvalidateEnumerators()
        {
            foreach (IEnumerator e in _enumerators)
            {
                ((IInvalidateable)e).Invalidate();
            }
        }

        private interface IInvalidateable
        {
            void Invalidate();
        }

        private class EnumeratorImpl<TI> : IEnumerator<TI>, IInvalidateable
        {
            private GenericWeakReferenceSet<TI> _backingSet;
            private int _currentIndex = -1;
            private TI _current = default(TI);
            private bool _invalid = false;

            public EnumeratorImpl(GenericWeakReferenceSet<TI> set) 
            {
                _backingSet = set;
            }

            public bool MoveNext()
            {
                if (_invalid)
                {
                    throw new InvalidOperationException();
                }

                _currentIndex++;
                for (; _currentIndex < _backingSet.Count; _currentIndex++)
                {
                    _current = _backingSet[_currentIndex];
                    if (_current != null) return true;
                }

                return false;
            }

            public void Reset()
            {
                _invalid = false;
                _currentIndex = -1;
                _current = default(TI);
            }

            public TI Current
            {
                get
                {
                    return _current;
                }
            }

            Object IEnumerator.Current
            {
                get
                {
                    return _current;
                }
            }

            public void Dispose()
            {
                _current = default(TI);
                _backingSet.RemoveEnumerator(this);
                _backingSet = null;
            }

            public void Invalidate()
            {
                _invalid = true;  
            }
        }
    }
}
