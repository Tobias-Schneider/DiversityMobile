using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util
{
    public interface IObeservable
    {
        void AddObserver(IObeserver observer);
        void RemoveObserver(IObeserver observer);
        void RemoveAll();
        void NotifyObservers(Object message);
    }

    public class ObservableDelegate : IObeservable
    {
        private IObeservable _owner;
        private List<IObeserver> _observers = new List<IObeserver>();


        public ObservableDelegate(IObeservable observable)
        {
            _owner = observable;
        }

        public void AddObserver(IObeserver o)
        {
            _observers.Add(o);
        }

        public void RemoveObserver(IObeserver o)
        {
            _observers.Remove(o);
        }

        public void RemoveAll()
        {
            _observers.Clear();
        }

        public void NotifyObservers(Object message) {
            foreach (IObeserver o in _observers)
            {
                o.Update(_owner, message);
            }
        }
    }
}
