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
using System.Collections.Generic;
using System.Text;

using UBT.AI4.Bio.DivMobi.DataLayer;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class Specimen : ConnectedType
    {
        #region Member data

        private CollectionEvent _parent = null;
        private CollectionSpecimen _currentSpecimen = null;
        private Connector con = SERIALIZER.Connector;
        private IDirectAccessIterator <CollectionSpecimen> _csIterator;

        #endregion

        #region Constructor

        internal Specimen(CollectionEvent ce)
        {

            if (ce != null)
            {
                _parent = ce;
                _csIterator = ce.CollectionSpecimen;
                _csIterator.Reset();
                this.Current = _csIterator.First();
            }
        }

        #endregion

        #region Properties

        public CollectionSpecimen Current
        {
            get
            {
                return this._currentSpecimen;
            }
            set
            {
                this._currentSpecimen = value;
                if (value == null)
                    this._csIterator.Reset();
            }
        }

        public CollectionSpecimen Next
        {
            get
            {
                try
                {
                    CollectionSpecimen ret = _csIterator.Next();
                    if (ret != null)
                    {
                        this.Current = ret;
                    }
                    return this.Current;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading next Specimen.");
                }
            }
        }

        public CollectionSpecimen Previous
        {
            get
            {
                try
                {
                    CollectionSpecimen ret = _csIterator.Prev();
                    if (ret != null)
                    {
                        this.Current = ret;
                    }
                    return this.Current;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading previous Specimen.");
                }
                
            }
        }

        public CollectionSpecimen First
        {
            get
            {
                try
                {
                    CollectionSpecimen ret = _csIterator.First();
                    if (ret != null)
                    {
                        this.Current = ret;
                    }
                    return this.Current;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading first Specimen.");
                }
            }
        }

        public CollectionSpecimen Last
        {
            get
            {
                try
                {
                    CollectionSpecimen ret = _csIterator.Last();
                    if (ret != null)
                    {
                        this.Current = ret;
                    }
                    return this.Current;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading last Specimen.");
                }
            }
        }

        public bool HasNext
        {
            get
            {
                if(_csIterator != null)
                    return _csIterator.HasNext();

                return false;
            }
        }

        public bool HasPrevious
        {
            get
            {
                if (_csIterator != null)
                    return this._csIterator.HasPrev();

                return false;
            }
        }

        public IdentificationUnits IdentificationUnits
        {
            get
            {
                try
                {
                    return new IdentificationUnits(this._currentSpecimen);
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
                if (_csIterator != null)
                {
                    text.Append(this._csIterator.CurrentPosition());
                    text.Append("/");
                    text.Append(this._csIterator.CountItems());
                }
                return text.ToString();
            }
        }
        
        public int Count
        {
            get
            {
                if(_csIterator != null)
                    return this._csIterator.CountItems();

                return 0;
            }
        }
        
        public bool Find(int id)
        {
            CollectionSpecimen cs;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionSpecimen), "_CollectionSpecimenID", id);
                cs = con.Load<CollectionSpecimen>(restrict);
            }
            catch (Exception)
            {
                return false;
            }

            if (cs != null)
            {
                this.Current = cs;
                this._csIterator.SetIteratorTo(this._currentSpecimen);
                return true;
            }
            return false;
        }


        #endregion

        #region Remove and Create

        public CollectionSpecimen CreateSpecimen()
        {
            CollectionSpecimen cs = null;

            try
            {
                cs = SERIALIZER.CreateISerializableObject<CollectionSpecimen>();
                SERIALIZER.ConnectOneToMany(_parent, cs);
                cs.CollectionEventID = _parent.CollectionEventID;
                con.Save(cs);
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New Specimen couldn't be created.");
            }

            try
            {
                if (UserProfiles.Instance.Current != null)
                {
                    try
                    {
                        CollectionAgent agent = SERIALIZER.CreateISerializableObject<CollectionAgent>();
                        SERIALIZER.ConnectOneToMany(cs, agent);
                        agent.CollectionSpecimenID = cs.CollectionSpecimenID;
                        agent.CollectorsName = UserProfiles.Instance.Current.CombinedNameCache;
                        if (UserProfiles.Instance.Current.AgentURI != null)
                        {
                            agent.CollectorsAgentURI = UserProfiles.Instance.Current.AgentURI;
                        }
                        con.Save(agent);
                    }
                    catch (Exception)
                    {
                        throw new DataFunctionsException("Associated Agent of new Specimen couldn't be created.");
                    }

                    try
                    {
                        CollectionProject csProject = SERIALIZER.CreateISerializableObject<CollectionProject>();
                        csProject.CollectionSpecimenID = cs.CollectionSpecimenID;
                        csProject.ProjectID = (int)UserProfiles.Instance.Current.ProjectID;
                        con.Save(csProject);
                    }
                    catch (Exception)
                    {
                        throw new DataFunctionsException("Associated Project of new Specimen couldn't be created.");
                    }
                }
            }
            catch (ConnectionCorruptedException)
            { 
            }
            this._currentSpecimen = this._csIterator.Last();
            return cs;
        }

        public void Remove(CollectionSpecimen collectionSpecimen)
        {
            if (collectionSpecimen != null)
            {
                try
                {
                    //// Remove Collection Specimen Attributs
                    //foreach (CollectionAgent agent in DataFunctions.Instance.RetrieveAgentForCollectionSpecimen((int)collectionSpecimen.CollectionSpecimenID))
                    //{
                    //    if (agent != null)
                    //    {
                    //        try
                    //        {
                    //            DataFunctions.Instance.Remove(agent);
                    //        }
                    //        catch (DataFunctionsException ex)
                    //        {
                    //            throw ex;
                    //        }
                    //    }
                    //}

                    // Remove all CollectionSpecimenImages assigned to CollectionSpecimen
                    IList<CollectionSpecimenImage> csImgList;
                    IRestriction restrict;
                    try
                    {
                        restrict = RestrictionFactory.Eq(typeof(CollectionSpecimenImage), "_CollectionSpecimenID", collectionSpecimen.CollectionSpecimenID);
                        csImgList = con.LoadList<CollectionSpecimenImage>(restrict);
                    }
                    catch (Exception)
                    {
                        throw new DataFunctionsException("List of assigned images couldn't be loaded.");
                    }

                    foreach (CollectionSpecimenImage csImgTemp in csImgList)
                    {
                        if (csImgTemp != null)
                        {
                            try
                            {
                                DataFunctions.Instance.Remove(csImgTemp);
                            }
                            catch (DataFunctionsException ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    // Remove all IdentificationUnit assigned to CollectionSpecimen
                    //IList<IdentificationUnit> iuList;
                    //try
                    //{
                    //    restrict = RestrictionFactory.Eq(typeof(IdentificationUnit), "_CollectionSpecimenID", collectionSpecimen.CollectionSpecimenID);
                    //    iuList = con.LoadList<IdentificationUnit>(restrict);
                    //}
                    //catch (Exception)
                    //{
                    //    throw new DataFunctionsException("List of associated Identification Units couldn't be loaded.");
                    //}

                    //foreach (IdentificationUnit iuTemp in iuList)
                    //{
                    //    if (iuTemp != null)
                    //    {
                    //        try
                    //        {
                    //            DataFunctions.Instance.Remove(iuTemp);
                    //        }
                    //        catch (DataFunctionsException ex)
                    //        {
                    //            throw ex;
                    //        }
                    //    }
                    //}

                    //Remove all CollectionProjects assigned to CollectionSpecimen
                    //IList<CollectionProject> projList;
                    //try
                    //{
                    //    restrict = RestrictionFactory.Eq(typeof(CollectionProject), "_CollectionSpecimenID", collectionSpecimen.CollectionSpecimenID);
                    //    projList = con.LoadList<CollectionProject>(restrict);
                    //}
                    //catch (Exception)
                    //{
                    //    throw new DataFunctionsException("List of associated Identification Units couldn't be loaded.");
                    //}

                    //foreach (CollectionProject projTemp in projList)
                    //{
                    //    if (projTemp != null)
                    //    {
                    //        try
                    //        {
                    //            DataFunctions.Instance.Remove(projTemp);
                    //        }
                    //        catch (DataFunctionsException ex)
                    //        {
                    //            throw ex;
                    //        }
                    //    }
                    //}
                    try
                    {
                        con.Delete(collectionSpecimen);
                    }
                    catch (Exception)
                    {
                        throw new DataFunctionsException("Object (CollectionSpecimen) couldn't be removed.");
                    }

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
    }
}