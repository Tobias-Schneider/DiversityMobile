//#######################################################################
//Diversity Mobile Synchronization
//Project Homepage:  http://www.diversitymobile.net
//Copyright (C) 2011  Tobias Schneider, Lehrstuhl Angewndte Informatik IV, Universität Bayreuth
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//#######################################################################
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DataLayer;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class CollectionEvents : ConnectedType
    {
        private CollectionEvent _currentEvent = null;
        private Connector con = SERIALIZER.Connector;
        private DirectAccesIteratorImpl<CollectionEvent> _ceIterator;


        #region Constructor

        //internal CollectionEvents()
        //{
        //    IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollectionEvent));           
        //    _ceIterator = new DirectAccesIteratorImpl<CollectionEvent>();
        //    _ceIterator.Serializer = SERIALIZER;
        //    _ceIterator.Restriction = restrict;
        //    this.Current = _ceIterator.First();
        //}

        public CollectionEvents(int? seriesID)
        {
            IRestriction restrict = null;
            restrict = RestrictionFactory.Eq(typeof(CollectionEvent), "_SeriesID", seriesID);
            _ceIterator = new DirectAccesIteratorImpl<CollectionEvent>();
            _ceIterator.Serializer = SERIALIZER;
            _ceIterator.Restriction = restrict;
            this.Current = _ceIterator.First();
        }

        #endregion

        #region Properties

        public CollectionEvent Current
        {
            get
            {
                return this._currentEvent;
            }
            set
            {
                this._currentEvent = value;
            }
        }

        public CollectionEvent Next
        {
            get
            {
                try
                {
                    if (this._currentEvent != null)
                    {
                        CollectionEvent ret = this._ceIterator.Next();
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
                    throw new DataFunctionsException("Error while loading next Collection Event.");
                }
            }
        }

        public CollectionEvent Previous
        {
            get
            {
                try
                {
                    if (this.Current != null)
                    {
                        CollectionEvent ret = this._ceIterator.Prev();
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
                    throw new DataFunctionsException("Error while loading previous Collection Event.");
                }
            }
        }

        public CollectionEvent First
        {
            get
            {
                try
                {
                    if (this.Current != null)
                    {
                        CollectionEvent ret = this._ceIterator.First();
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
                    throw new DataFunctionsException("Error while loading first Collection Event.");
                }
            }
        }

        public CollectionEvent Last
        {
            get
            {
                try
                {
                    if (this.Current != null)
                    {
                        CollectionEvent ret = this._ceIterator.Last();
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
                    throw new DataFunctionsException("Error while loading last Collection Event.");
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
                    if (this._ceIterator != null)
                        return this._ceIterator.HasNext();
                    else
                        return false;
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
                    if (this._ceIterator != null)
                        return this._ceIterator.HasPrev();
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public int Count
        {
            get
            {
                try
                {
                    if (this._ceIterator != null)
                        return this._ceIterator.CountItems();
                    else
                        return 0;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public Specimen Specimen
        {
            get
            {
                try
                {
                    return new Specimen(this._currentEvent);
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
                    text.Append(this._ceIterator.CurrentPosition());
                    text.Append("/");
                    text.Append(this._ceIterator.CountItems());
                }
                catch (Exception)
                {
                    text = new StringBuilder();
                }

                return text.ToString();
            }
        }

        public int LastCollectorsEventNumber
        {
            get
            {
                try
                {
                    IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollectionEvent));

                    IList<CollectionEvent> _ceList = con.LoadList<CollectionEvent>(restrict);
                    int number = 0;
                    foreach (CollectionEvent ce in _ceList)
                    {
                        if (ce != null)
                        {
                            try
                            {
                                int tmp = int.Parse(ce.CollectorsEventNumber);
                                if (tmp > number)
                                    number = tmp;
                            }
                            catch (Exception)
                            {
                                return -1;
                            }
                        }
                    }

                    return number;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }
        
        #endregion

        #region Create

        public void CreateNewEvent(int? seriesID)
        {
            CollectionEvent ce = SERIALIZER.CreateISerializableObject<CollectionEvent>();
            // EventSeries
            ce.SeriesID = seriesID;
            if (seriesID != null)
            {
                try
                {
                    SERIALIZER.ConnectOneToMany(EventSeriess.Instance.Current, ce);
                }
                catch (ConnectionCorruptedException ex)
                {
                    throw ex;
                }
            }
            if (this.LastCollectorsEventNumber != -1)
            {
                ce.CollectorsEventNumber = (this.LastCollectorsEventNumber + 1).ToString();
            }
            try
            {
                con.Save(ce);
                this._currentEvent = this._ceIterator.Last();
            }
            catch (Exception ex)
            {
                throw new DataFunctionsException("New CollectionEvent couldn't be saved. (" + ex.Message + ")");
            }
        }

        public void CreateNewEvent(int? seriesID, double altitude, double longitude, double latitude, int countSat, float dilution)
        {
            CollectionEvent ce = SERIALIZER.CreateISerializableObject<CollectionEvent>();
            ce.SeriesID = seriesID;
            if (this.LastCollectorsEventNumber != -1)
            {
                ce.CollectorsEventNumber = (this.LastCollectorsEventNumber + 1).ToString();
            }
            if (seriesID != null)
            {
                try
                {
                    SERIALIZER.ConnectOneToMany(EventSeriess.Instance.Current, ce);
                }
                catch (ConnectionCorruptedException ex)
                {
                    throw ex;
                }
            }

            try
            {
                con.Save(ce);
                this._currentEvent = this._ceIterator.Last();
            }
            catch (Exception ex)
            {
                throw new DataFunctionsException("New CollectionEvent couldn't be saved. (" + ex.Message + ")");
            }

            // Default Localisation: altitude
            CollectionEventLocalisation ceLoc1 = SERIALIZER.CreateISerializableObject<CollectionEventLocalisation>();
            SERIALIZER.ConnectOneToMany(ce, ceLoc1);
            ceLoc1.LocalisationSystemID = 4;
            ceLoc1.AverageAltitudeCache = altitude;
            ceLoc1.Location1 = altitude.ToString("00.00");

            // Notes: automatically changed per GPS
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("GPS Coordinates automatically changed");
            builder.AppendLine("Number of Satellites: " + countSat.ToString());
            builder.AppendLine("Position Dilution: " + dilution.ToString());

            ceLoc1.LocationNotes = builder.ToString();

            try
            {
                if (UserProfiles.Instance.Current != null)
                {
                    ceLoc1.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;

                    if (UserProfiles.Instance.Current.AgentURI != null)
                        ceLoc1.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                }
            }
            catch (ConnectionCorruptedException)
            { }

            try
            {
                con.Save(ceLoc1);
            }
            catch (Exception ex)
            {
                throw new DataFunctionsException("Associated Localisation (Type Altitude) couldn't be created. (" + ex.Message + ")");
            }

            // Default Localisation: wgs84
            CollectionEventLocalisation ceLoc2 = SERIALIZER.CreateISerializableObject<CollectionEventLocalisation>();
            SERIALIZER.ConnectOneToMany(ce, ceLoc2);
            ceLoc2.LocalisationSystemID = 8;
            ceLoc2.AverageLatitudeCache = latitude;
            ceLoc2.AverageLongitudeCache = longitude;
            ceLoc2.AverageAltitudeCache = altitude;
            ceLoc2.Location1 = longitude.ToString("00.00000000");
            ceLoc2.Location2 = latitude.ToString("00.00000000");

            // Notes: automatically changed per GPS
            ceLoc2.LocationNotes = builder.ToString();

            try
            {
                if (UserProfiles.Instance.Current != null)
                {
                    ceLoc2.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;

                    if (UserProfiles.Instance.Current.AgentURI != null)
                        ceLoc2.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                }
            }
            catch (ConnectionCorruptedException)
            { }

            try
            {
                con.Save(ceLoc2);
            }
            catch (Exception ex)
            {
                throw new DataFunctionsException("Associated Localisation (Type GPS) couldn't be created. (" + ex.Message + ")");
            } 
        }

       
        #endregion

        #region Remove

        public void Remove()
        {
            if (this._currentEvent != null)
            {
                try
                {
                    //// Remove Collection Event Attributs, schon im Cascaden enthalten.
                    //foreach (CollectionEventLocalisation ceLoc in DataFunctions.Instance.RetrieveLocalisationSystemForCollectionEvent(this._currentEvent))
                    //{
                    //    if (ceLoc != null)
                    //    {
                    //        try
                    //        {
                    //            DataFunctions.Instance.Remove(ceLoc);
                    //        }
                    //        catch (DataFunctionsException ex)
                    //        {
                    //            throw ex;
                    //        }
                    //    }
                    //}

                    //foreach (CollectionEventProperty ceProp in DataFunctions.Instance.RetrievePropertyForCollectionEvent(this._currentEvent))
                    //{
                    //    if (ceProp != null)
                    //    {
                    //        try
                    //        {
                    //            DataFunctions.Instance.Remove(ceProp);
                    //        }
                    //        catch (DataFunctionsException ex)
                    //        {
                    //            throw ex;
                    //        }
                    //    }
                    //}

                    // Remove all CollectionEventImages assigned to CollectionEvent
                    IList<CollectionEventImage> ceImgList;
                    IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionEventImage), "_CollectionEventID", this._currentEvent.CollectionEventID);
                    ceImgList = con.LoadList<CollectionEventImage>(restrict);

                    foreach (CollectionEventImage ceImgTemp in ceImgList)
                    {
                        if (ceImgTemp != null)
                        {
                            try
                            {
                                DataFunctions.Instance.Remove(ceImgTemp);
                            }
                            catch (DataFunctionsException ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    // Remove all Collection Specimen assigned to Collection Event
                    //IList<CollectionSpecimen> csList;
                    //restrict = RestrictionFactory.Eq(typeof(CollectionSpecimen), "_CollectionEventID", this._currentEvent.CollectionEventID);
                    //csList = con.LoadList<CollectionSpecimen>(restrict);

                    //foreach (CollectionSpecimen csTemp in csList)
                    //{
                    //    if (csTemp != null)
                    //    {
                    //        try
                    //        {
                    //            DataFunctions.Instance.Remove(csTemp);
                    //        }
                    //        catch (DataFunctionsException ex)
                    //        {
                    //            throw ex;
                    //        }
                    //    }
                    //}

                    try
                    {
                        con.Delete(this._currentEvent);
                    }
                    catch (Exception)
                    {
                        throw new DataFunctionsException("Object (CollectionEvent) couldn't be removed.");
                    }

                    // Change Current Event

                    try
                    {
                        if (!this.HasNext)
                        {
                            if (this.HasPrevious)
                            {
                                this.Current = this.Previous;
                            }
                            else
                            {
                                this.Current = this.First;
                            }
                        }
                        else
                        {
                            this.Current = this.Next;
                        }
                    }
                    catch (DataFunctionsException ex)
                    {
                        throw ex;
                    }
                }
                catch (ConnectionCorruptedException ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        public bool Find(int id)
        {
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionEvent), "_CollectionEventID", id);
                this._currentEvent = con.Load<CollectionEvent>(restrict);

                if (this._currentEvent == null)
                {
                    return false;
                }
                else
                {
                    this._ceIterator.SetIteratorTo(this._currentEvent);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
