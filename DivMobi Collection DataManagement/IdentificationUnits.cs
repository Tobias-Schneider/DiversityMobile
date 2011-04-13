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

using UBT.AI4.Bio.DivMobi.DataLayer;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class IdentificationUnits : ConnectedType
    {
        #region Member data
        private IdentificationUnit _currentIdentificationUnit = null;
        private CollectionSpecimen _collectionSpecimen = null;
        private Connector con = SERIALIZER.Connector;
        private IDirectAccessIterator<IdentificationUnit> _identificationUnitIterator;//Dieser iteriert im Gegensatz zu den anderen KLassen in Datamanagement nur auf dem TopLevel
 
        
        #endregion

        #region Constructor

        internal IdentificationUnits(CollectionSpecimen cs)
        {
            if (cs != null)
            {
                this._collectionSpecimen = cs;
                _identificationUnitIterator = cs.IdentificationUnits;
                _identificationUnitIterator.Reset();
                this._currentIdentificationUnit = _identificationUnitIterator.First();
                if (this._currentIdentificationUnit != null)
                {
                    while (this._currentIdentificationUnit.RelatedUnitID != null)
                    {
                        try
                        {
                            this._currentIdentificationUnit = this.Next;
                        }
                        catch (DataFunctionsException)
                        {
                            this._currentIdentificationUnit = null;
                        }
                    }
                }
                //this._identificationUnitBrotherIterator = this._identificationUnitIterator;
            }
        }

      

        #endregion

        #region Properties
        public IdentificationUnit Current
        {
            get
            {
                return this._currentIdentificationUnit;
            }
            set
            {
                this._currentIdentificationUnit = value;
            }
        }

        //public IdentificationUnit NextSameLevel
        //{
        //    get
        //    {
        //        if(this._currentIdentificationUnit.RelatedUnit==null)
        //        {
        //            this._currentIdentificationUnit=this.NextTopLevel;
        //        }
        //        else
        //        {
        //            this._currentIdentificationUnit = this._currentIdentificationUnit.RelatedUnit.ChildUnits.Next();
        //        }
        //        return this._currentIdentificationUnit;
        //    }
        //}

        public IdentificationUnit Next
        {
            //InvalidOperationException
            get
            {
                try
                {
                    if (this._identificationUnitIterator != null)
                        this._currentIdentificationUnit = this._identificationUnitIterator.Next();

                    return this._currentIdentificationUnit;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading next Identification Unit.");
                }
            }
        }

        public IdentificationUnit Previous
        {
            //InvalidOperationException
            get
            {
                try
                {
                    if (this._identificationUnitIterator != null)
                        this._currentIdentificationUnit = this._identificationUnitIterator.Prev();

                    return this._currentIdentificationUnit;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading previous Identification Unit.");
                }
            }
        }

        public IdentificationUnit First
        {
            get
            {
                try
                {
                    return this._identificationUnitIterator.First();
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading first Identification Unit.");
                }
            }
        }

        public IdentificationUnit Last
        {
            get
            {
                try
                {
                    return this._identificationUnitIterator.Last();
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading last Identification Unit.");
                }
            }
        }

        public IdentificationUnit NextTopLevel
        {
            //InvalidOperationException
            get
            {
                try
                {
                    do
                    {
                        this._currentIdentificationUnit = this._identificationUnitIterator.Next();
                    }
                    while (_currentIdentificationUnit.RelatedUnitID != null);
                    //this._identificationUnitBrotherIterator = this._currentIdentificationUnit.ChildUnits;
                    return this._currentIdentificationUnit;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading next TopLevel Identification Unit.");
                }
            }
        }

        public int Count
        {
            get
            {
                try
                {
                    return this._identificationUnitIterator.CountItems();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public IdentificationUnit PreviousTopLevel
        {
            //InvalidOperationException
            get
            {
                try
                {
                    do
                    {
                        this._currentIdentificationUnit = this._identificationUnitIterator.Prev();
                    }
                    while (_currentIdentificationUnit.RelatedUnitID != null);
                    //this._identificationUnitBrotherIterator = this._currentIdentificationUnit.ChildUnits;
                    return this._currentIdentificationUnit;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading previous TopLevel Identification Unit.");
                }
            }
        }

        public IdentificationUnit FirstTopLevel
        {
            //InvalidOperationException
            get
            {
                try
                {
                    this._currentIdentificationUnit = this._identificationUnitIterator.First();
                    do
                    {
                        this._currentIdentificationUnit = this._identificationUnitIterator.Next();
                    }
                    while (_currentIdentificationUnit.RelatedUnitID != null);
                    //this._identificationUnitBrotherIterator = this._currentIdentificationUnit.ChildUnits;
                    return this._currentIdentificationUnit;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading first TopLevel Identification Unit.");
                }
            }
        }

        public IdentificationUnit LastTopLevel
        {
            //InvalidOperationException
            get
            {
                try
                {
                    this._currentIdentificationUnit = this._identificationUnitIterator.Last();
                    do
                    {
                        this._currentIdentificationUnit = this._identificationUnitIterator.Prev();
                    }
                    while (_currentIdentificationUnit.RelatedUnitID != null);
                    //this._identificationUnitBrotherIterator = this._currentIdentificationUnit.ChildUnits;
                    return this._currentIdentificationUnit;
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Error while loading last TopLevel Identification Unit.");
                }
            }
        }

        public bool HasNext
        {
            get
            {
                try
                {
                    return this._identificationUnitIterator.HasNext();
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool HasNextTopLevel
        {
            get
            {
                try
                {
                    IdentificationUnit current = this._currentIdentificationUnit;
                    if (this._identificationUnitIterator.HasNext())
                    {
                        IdentificationUnit iu = this.Next;
                        if (iu.RelatedUnitID == null)
                        {
                            this._identificationUnitIterator.SetIteratorTo(current);
                            return true;
                        }
                        while (this.HasNext)
                        {
                            iu = this.Next;
                            if (iu.RelatedUnitID == null)
                            {
                                this._identificationUnitIterator.SetIteratorTo(current);
                                return true;
                            }
                        }
                    }
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
                    return this._identificationUnitIterator.HasPrev();
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool HasPreviousTopLevel
        {
            get
            {
                try
                {
                    IdentificationUnit current = this._currentIdentificationUnit;
                    if (this._identificationUnitIterator.HasPrev())
                    {
                        IdentificationUnit iu = this.Previous;
                        if (iu.RelatedUnitID == null)
                        {
                            this._identificationUnitIterator.SetIteratorTo(current);
                            return true;
                        }
                        while (this.HasPrevious)
                        {
                            iu = this.Previous;
                            if (iu.RelatedUnitID == null)
                            {
                                this._identificationUnitIterator.SetIteratorTo(current);
                                return true;
                            }
                        }
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
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
                    text.Append(this._identificationUnitIterator.CurrentPosition());
                    text.Append("/");
                    text.Append(this._identificationUnitIterator.CountItems());
                }
                catch (Exception)
                {
                    text = new StringBuilder();
                }
                
                return text.ToString();
            }
        }
        
        #endregion

        #region Create and Remove

        public IdentificationUnit CreateIdentificationUnit(String lastIdentificationCache)
        {
            try
            {
                IdentificationUnit iu = SERIALIZER.CreateISerializableObject<IdentificationUnit>();
                iu.CollectionSpecimenID = this._collectionSpecimen.CollectionSpecimenID;
                iu.LastIdentificationCache = lastIdentificationCache;
                SERIALIZER.ConnectOneToMany(this._collectionSpecimen, iu);
                SERIALIZER.ConnectOneToMany(this._currentIdentificationUnit, iu);
                SERIALIZER.Connector.Save(iu);
                return iu;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New Identification Unit couldn't be created");
            }
        }

        public IdentificationUnit CreateTopLevelIdentificationUnit(String lastIdentificationCache)
        {
            try
            {
                IdentificationUnit iu = SERIALIZER.CreateISerializableObject<IdentificationUnit>();
                iu.CollectionSpecimenID = this._collectionSpecimen.CollectionSpecimenID;
                iu.LastIdentificationCache = lastIdentificationCache;
                SERIALIZER.ConnectOneToMany(this._collectionSpecimen, iu);
                SERIALIZER.Connector.Save(iu);
                this._currentIdentificationUnit = this._identificationUnitIterator.Last();
                return iu;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New TopLevel Identification Unit couldn't be created");
            }
        }

        public void Remove(IdentificationUnit identificationUnit)
        {
            if (identificationUnit != null)
            {
                //IU-Children und Analysen werden durch Cascade gelöscht.

                //// Remove all IdentificationUnitAnalysis assigned to IdentificationUnit
                //IList<IdentificationUnitAnalysis> iuaList;
                //IRestriction restrict = RestrictionFactory.Eq(typeof(IdentificationUnitAnalysis), "_IdentificationUnitID", identificationUnit.IdentificationUnitID);
                //iuaList = con.LoadList<IdentificationUnitAnalysis>(restrict);
                //foreach (IdentificationUnitAnalysis iuaTemp in iuaList)
                //{
                //    DataFunctions.Instance.Remove(iuaTemp);
                //}
                //// Remove all IdentificationUnitGeoAnalysis assigned to IdentificationUnit
                //IList<IdentificationUnitGeoAnalysis> iuGeoAnalysisList;
                //restrict = RestrictionFactory.Eq(typeof(IdentificationUnitGeoAnalysis), "_IdentificationUnitID", identificationUnit.IdentificationUnitID);
                //iuGeoAnalysisList = con.LoadList<IdentificationUnitGeoAnalysis>(restrict);
                //foreach (IdentificationUnitGeoAnalysis iuGeoAnalysisTemp in iuGeoAnalysisList)
                //{
                //    DataFunctions.Instance.Remove(iuGeoAnalysisTemp);
                //}
                try
                {
                    con.Delete(identificationUnit);
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Identification Unit ("+identificationUnit.IdentificationUnitID+") couldn't be removed.");
                }
            }
        }

        public void RemoveTopLevelIU()
        {
            try
            {
                Remove(this._currentIdentificationUnit);

                if (!this.HasNextTopLevel)
                {
                    if (this.HasPreviousTopLevel)
                    {
                        this.Current = this.PreviousTopLevel;
                    }
                    else
                    {
                        this.Current = this.FirstTopLevel;
                    }
                }
                else
                {
                    this.Current = this.NextTopLevel;
                }
            }
            catch (DataFunctionsException ex)
            {
                throw ex;
            }
        }

        public bool FindTopLevelIU(int id)
        {
            try
            {
                IRestriction r1 = RestrictionFactory.Eq(typeof(IdentificationUnit), "_IdentificationUnitID", id);
                IRestriction r2 = RestrictionFactory.Eq(typeof(IdentificationUnit), "_RelatedUnitID", null);
                IRestriction restrict = RestrictionFactory.And().Add(r1).Add(r2);
                this._currentIdentificationUnit = con.Load<IdentificationUnit>(restrict);

                if (this._currentIdentificationUnit == null)
                {
                    return false;
                }
                else
                {
                    this._identificationUnitIterator.SetIteratorTo(this._currentIdentificationUnit);
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
