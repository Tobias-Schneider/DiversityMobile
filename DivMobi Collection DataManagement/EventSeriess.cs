using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DataLayer;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class EventSeriess:ConnectedType
    {
        private CollectionEventSeries _currentSeries = null;
        private Connector con = SERIALIZER.Connector;
        private DirectAccesIteratorImpl<CollectionEventSeries> _ceIterator;

        #region Constructor

        //Beim Iterieren über die EventSeries muss beachtet werden, dass 
        //die EmptyEventSeries manuell zur Auswahl zugefügt werden muss.(noch nicht erledigt)
        private EventSeriess()
        {
            IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollectionEventSeries));           
            _ceIterator = new DirectAccesIteratorImpl<CollectionEventSeries>();
            _ceIterator.Serializer = SERIALIZER;
            _ceIterator.Restriction = restrict;
            this.Current = _ceIterator.First();
        }

        #endregion

        #region Singleton

        private static EventSeriess _instance = null;

        public static EventSeriess Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new EventSeriess();
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        throw new ConnectionCorruptedException("Instance of EventSeriess couldn't be retrieved. ("+ex.Message+")");
                    }
                }
                return _instance;
            }
            set
            {
                if (value == null)
                    _instance = null;
            }
        }

        #endregion

        #region Properties

        public CollectionEventSeries Current
        {
            get
            {
                return this._currentSeries;
            }
            set
            {
                this._currentSeries = value;
            }
        }

        public CollectionEventSeries Next
        {
            get
            {
                try
                {
                    if (this._currentSeries != null && this._ceIterator != null)
                    {
                        CollectionEventSeries ret = this._ceIterator.Next();
                        if (ret != null)
                        {
                            this.Current = ret;
                        }
                        return this.Current;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading next Collection Event Series.");
                }
            }
        }

        public CollectionEventSeries Previous
        {
            get
            {
                try
                {
                    if (this.Current != null)
                    {
                        CollectionEventSeries ret = this._ceIterator.Prev();
                        if (ret != null)
                        {
                            this.Current = ret;
                        }
                        return this.Current;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading previous Collection Event Series.");
                }
            }
        }

        public CollectionEventSeries First //beachten, dass davor die NoEventSeries eingefügt werden muss.
        {
            get
            {
                try
                {
                    if (this._ceIterator != null)//
                    {
                        CollectionEventSeries ret = this._ceIterator.First();
                        if (ret != null)
                        {
                            this.Current = ret;
                        }
                        return this.Current;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading first Collection Event Series.");
                }
            }
        }

        public CollectionEventSeries Last
        {
            get
            {
                try
                {
                    if (this._ceIterator != null)
                    {
                        CollectionEventSeries ret = this._ceIterator.Last();
                        if (ret != null)
                        {
                            this.Current = ret;
                        }
                        return this.Current;
                    }
                    return null;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading last Collection Event Series.");
                }
            }
        }

        public Connector Connector
        {
            get
            {
                return con;
            }
        }

        public bool HasNext
        {
            get
            {
                try
                {
                    return this._ceIterator.HasNext();
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool HasPrevious
        {
            get
            {
                try
                {
                    return this._ceIterator.HasPrev();
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public CollectionEvents CollectionEvents
        {
            get
            {
                try
                {
                    if (this._currentSeries != null)
                    {
                        return new CollectionEvents(this._currentSeries.SeriesID);
                    }
                    return new CollectionEvents(null);
                }
                catch (ConnectionCorruptedException ex)
                {
                    throw ex;
                }
            }
        }

        public String Position
        {
            get
            {
                StringBuilder text = new StringBuilder();
                try
                {
                    if (this.Current == null)
                    {
                        text.Append("0");
                        text.Append("/");
                        text.Append(this._ceIterator.CountItems());
                    }
                    else
                    {
                        text.Append(this._ceIterator.CurrentPosition());
                        text.Append("/");
                        text.Append(this._ceIterator.CountItems());
                    }
                }
                catch (Exception)
                {
                    text = new StringBuilder();
                }

                return text.ToString();
            }
        }

        public int Count
        {
            get
            {
                try
                {
                    return this._ceIterator.CountItems();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        
        #endregion

        #region Create and Remove

        public CollectionEventSeries CreateNewEventSeries()
        {
            try
            {
                CollectionEventSeries ceSeries = SERIALIZER.CreateISerializableObject<CollectionEventSeries>();
                con.Save(ceSeries);
                this._currentSeries = this._ceIterator.Last();
                return ceSeries;
            }
            catch (Exception ex)
            {
                throw new DataFunctionsException("New CollectionEventSeries couldn't be created. (" + ex.Message + ")");
            }
        }

        public void Remove(CollectionEventSeries cs)
        {
            try
            {
                CollectionEventSeries newSeries;
                if (cs.CollectionEvents.First() != null)
                {
                    CollectionEvent ev = cs.CollectionEvents.First();
                    ev.SeriesID = null;
                    try
                    {
                        DataFunctions.Instance.Update(ev);

                        while (cs.CollectionEvents.HasNext())
                        {
                            ev = cs.CollectionEvents.Next();
                            ev.SeriesID = null;
                            DataFunctions.Instance.Update(ev);
                        }
                    }
                    catch (DataFunctionsException ex)
                    {
                        throw ex;
                    }
                }
                if (!this.HasNext)
                {
                    if (this.HasPrevious)
                    {
                        newSeries = this.Previous;
                        this.Current = newSeries;
                    }
                    else
                    {

                        newSeries = this.First;
                        this.Current = newSeries;

                    }
                }
                else
                {

                    newSeries = this.Next;
                    this.Current = newSeries;
                }

                con.Delete(cs);
            }
            catch (ConnectionCorruptedException ex)
            {
                throw ex;
            }
            catch (DataFunctionsException ex)
            {
                throw ex;
            }
        }


        public bool Find(int id)
        {
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionEventSeries), "_SeriesID", id);
                this._currentSeries = con.Load<CollectionEventSeries>(restrict);

                if (this._currentSeries == null)
                {
                    return false;
                }
                else
                {
                    this._ceIterator.SetIteratorTo(this._currentSeries);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
