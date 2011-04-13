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
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DataLayer;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;


namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class DataFunctions : ConnectedType
    {
        #region Member data

        private Connector con = SERIALIZER.Connector;

        #endregion

        #region Singleton

        private static DataFunctions _instance = null;

        public static DataFunctions Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new DataFunctions();
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        throw new ConnectionCorruptedException("Instance of DataFunctions couldn't be retrieved. ("+ex.Message+")");
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

        public int LastCollectorsEventNumber
        {
            get
            {
                try
                {
                    IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollectionEvent));
                    IList<CollectionEvent> _ceList = con.LoadList<CollectionEvent>(restrict);
                    if (_ceList == null)
                        return -1;

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

        #region Constructor

        private DataFunctions()
        {

        }


        #endregion

        #region Data Functions

        #region Create

        public CollectionEventLocalisation CreateCollectionEventLocalisation(int LocSystemID, CollectionEvent ce)
        {
            try
            {
                CollectionEventLocalisation ceLoc = SERIALIZER.CreateISerializableObject<CollectionEventLocalisation>();
                //ceLoc.CollectionEventID = this.Current.CollectionEventID;

                SERIALIZER.ConnectOneToMany(ce, ceLoc);
                ceLoc.LocalisationSystemID = LocSystemID;

                try
                {
                    if (UserProfiles.Instance.Current != null)
                    {
                        ceLoc.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;

                        if (UserProfiles.Instance.Current.AgentURI != null)
                            ceLoc.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                    }
                }
                catch (ConnectionCorruptedException)
                { }

                con.Save(ceLoc);

                return ceLoc;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New Localisation couldn't be created");
            } 
        }

        public CollectionEventLocalisation CreateCollectionEventLocalisation(int LocSystemID, String value, CollectionEvent ce)
        {
            try
            {
                CollectionEventLocalisation ceLoc = SERIALIZER.CreateISerializableObject<CollectionEventLocalisation>();
                //ceLoc.CollectionEventID = this.Current.CollectionEventID;
                SERIALIZER.ConnectOneToMany(ce, ceLoc);
                ceLoc.LocalisationSystemID = LocSystemID;
                ceLoc.Location1 = value;

                try
                {
                    if (UserProfiles.Instance.Current != null)
                    {
                        ceLoc.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;

                        if (UserProfiles.Instance.Current.AgentURI != null)
                            ceLoc.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                    }
                }
                catch (ConnectionCorruptedException) { }

                con.Save(ceLoc);
                return ceLoc;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New Localisation couldn't be created");
            }
        }

        public CollectionEventProperty CreateCollectionEventProperty(int propertyID, String text, CollectionEvent ce)
        {
            try
            {
                CollectionEventProperty ceProp = SERIALIZER.CreateISerializableObject<CollectionEventProperty>();
                SERIALIZER.ConnectOneToMany(ce, ceProp);
                ceProp.PropertyID = propertyID;
                ceProp.DisplayText = text;

                try
                {
                    if (UserProfiles.Instance.Current != null)
                    {
                        ceProp.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;

                        if (UserProfiles.Instance.Current.AgentURI != null)
                            ceProp.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                    }
                }
                catch (ConnectionCorruptedException) { }

                con.Save(ceProp);
                return ceProp;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New Event Property couldn't be created");
            }
        }

       

        public IdentificationUnitAnalysis CreateIdentificationUnitAnalysis(IdentificationUnit iu, int analysisId, String value, DateTime analysisDate)
        {
            try
            {
                IdentificationUnitAnalysis iua = SERIALIZER.CreateISerializableObject<IdentificationUnitAnalysis>();

                SERIALIZER.ConnectOneToMany(iu, iua);

                iua.AnalysisID = analysisId;
                iua.AnalysisNumber = DateTime.Now.ToString();
                iua.AnalysisResult = value;
                iua.AnalysisDate = analysisDate.ToShortDateString();
                iua.IdentificationUnitID = iu.IdentificationUnitID;
                iua.CollectionSpecimenID = iu.CollectionSpecimenID;
                //Das Datum der Analyse wird gespeichert
                iua.AnalysisDate = System.DateTime.Now.ToShortDateString();

                // Current User as responsible agent
                try
                {
                    if (UserProfiles.Instance.Current != null)
                    {
                        iua.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;

                        if (UserProfiles.Instance.Current.AgentURI != null)
                            iua.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                    }
                }
                catch (ConnectionCorruptedException)
                { }

                con.Save(iua);
                return iua;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New IdentificationUnitAnalysis couldn't be created.");
            }
        }

        public IdentificationUnitGeoAnalysis CreateIdentificationUnitGeoAnalysis(IdentificationUnit iu, CollectionSpecimen cs)
        {
            try
            {
                IdentificationUnitGeoAnalysis iuGeoAnalysis = SERIALIZER.CreateISerializableObject<IdentificationUnitGeoAnalysis>();
                SERIALIZER.ConnectOneToMany(iu, iuGeoAnalysis);
                iuGeoAnalysis.IdentificationUnitID = iu.IdentificationUnitID;
                iuGeoAnalysis.CollectionSpecimenID = cs.CollectionSpecimenID;

                try
                {
                    if (UserProfiles.Instance.Current != null)
                    {
                        iuGeoAnalysis.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;
                        if (UserProfiles.Instance.Current.AgentURI != null)
                            iuGeoAnalysis.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                    }
                }
                catch (ConnectionCorruptedException)
                { }

                con.Save(iuGeoAnalysis);
                return iuGeoAnalysis;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New IdentificationUnit GeoAnalysis couldn't be created.");
            }
        }

        public CollectionAgent CreateCollectionAgent(int csID, int collector)
        {
            try
            {
                CollectionAgent agent = SERIALIZER.CreateISerializableObject<CollectionAgent>();
                agent.CollectionSpecimenID = csID;
                agent.CollectorsName = UserProfiles.Instance.List[collector].CombinedNameCache;

                if (UserProfiles.Instance.List[collector].AgentURI != null)
                    agent.CollectorsAgentURI = UserProfiles.Instance.List[collector].AgentURI;

                con.Save(agent);

                return agent;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New Collection Agent couldn't be created.");
            }
        }

        public void CreateCollectionEventImage(String path, String picturepath, String imgType, int ceID)
        {
            if (ceID < 0)
            {
                throw new DataFunctionsException("Collection Event not found.");
            }
            int i = 0;
            try
            {
                CollectionEventImage ceImg = SERIALIZER.CreateISerializableObject<CollectionEventImage>();
                i++;//1
                ceImg.ImageType = imgType;
                ceImg.CollectionEventID = ceID;
                i++;//2
                StringBuilder sb = new StringBuilder(picturepath);
                sb.Append(@"\");
                sb.Append(ceImg.Rowguid);
                sb.Append(Path.GetExtension(path));
                String destinationPath = sb.ToString();
                i++;//3
                File.Move(path, destinationPath);
                i++;//4
                ceImg.URI = destinationPath;
                i++;//5
                con.Save(ceImg);
                i++;//6
            }
            catch (Exception e)
            {
                throw new DataFunctionsException("New Image for Event couldn't be assigned. "+e.Message+" "+i);
            }
        }
        
        public void CreateCollectionSpecimenImage(String path, String picturepath, int csID, int iuID, String imgType)
        {
            if (csID < 0)
            {
                throw new DataFunctionsException("Specimen not found.");
            }
            int i = 0;
            try
            {
                CollectionSpecimenImage csImg = SERIALIZER.CreateISerializableObject<CollectionSpecimenImage>();
                i++;
                csImg.CollectionSpecimenID = csID;
                csImg.IdentificationUnitID = iuID;
                csImg.ImageType = imgType;
                i++;
                StringBuilder sb = new StringBuilder(picturepath);
                sb.Append(@"\");
                sb.Append(csImg.Rowguid);
                sb.Append(Path.GetExtension(path));
                string destinationPath = sb.ToString();
                i++;
                File.Move(path, destinationPath);
                i++;               
                csImg.URI = destinationPath;
                i++;
                con.Save(csImg);
                i++;
            }
            catch (Exception e)
            {
                throw new DataFunctionsException("New Image for IU couldn't be assigned."+e.Message+" "+i);
            }
        }
        
        public void CreateCollectionSpecimenImage(String path, String picturepath, int csID, String imgType)
        {
            if (csID < 0)
            {
                throw new DataFunctionsException("Specimen not found.");
            }
            int i = 0;
            try
            {
                CollectionSpecimenImage csImg = SERIALIZER.CreateISerializableObject<CollectionSpecimenImage>();
                i++;
                csImg.CollectionSpecimenID = csID;
                i++;
                csImg.ImageType = imgType;
                i++;
                StringBuilder sb = new StringBuilder(picturepath);
                sb.Append(@"\");
                sb.Append(csImg.Rowguid);
                sb.Append(Path.GetExtension(path));
                string destinationPath = sb.ToString();
                i++;
                File.Move(path, destinationPath);
                i++;
                csImg.URI = destinationPath;
                i++;
                con.Save(csImg);
                i++;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New Image for Specimen couldn't be assigned.");
            }
        }

        public void CreateIdentificationWithCheck(IdentificationUnit iu)
        {
            try
            {
                Identification ident = SERIALIZER.CreateISerializableObject<Identification>();
                SERIALIZER.ConnectOneToMany(iu, ident);
                ident.CollectionSpecimenID = iu.CollectionSpecimenID;
                ident.IdentificationUnitID = iu.IdentificationUnitID;

                if (iu.LastIdentificationCache != null)
                {
                    ident.TaxonomicName = iu.LastIdentificationCache;
                }
                ident.IdentificationCategory = "determination";

                try
                {
                    // Current User as responsible agent
                    if (UserProfiles.Instance.Current != null)
                    {
                        ident.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;

                        if (UserProfiles.Instance.Current.AgentURI != null)
                            ident.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
                    }
                }
                catch (ConnectionCorruptedException)
                { }

                short sequence = 0;
                IList<Identification> identList = RetrieveIdentificationsForIU(iu.IdentificationUnitID);

                foreach (Identification item in identList)
                {
                    if (item != null)
                    {
                        if (item.IdentificationSequence > sequence)
                            sequence = (short)item.IdentificationSequence;
                    }
                }

                ident.IdentificationSequence = ++sequence;
                con.Save(ident);
            }
            catch (Exception)
            {
                throw new DataFunctionsException("New Identification couldn't be created");
            }
        }

        public void CreateUserTaxonomicGroupTable(String taxCode, String taxTable)
        {
            try
            {
                UserTaxonomicGroupTable userTaxTable = SERIALIZER.CreateISerializableObject<UserTaxonomicGroupTable>();
                userTaxTable.TaxonomicCode = taxCode;
                userTaxTable.TaxonomicTable = taxTable;

                con.Save(userTaxTable);
            }
            catch (Exception)
            {
                throw new DataFunctionsException("User TaxonomicGroup Table couldn't be created.");
            }
        }

        public void CreateOrUpdateUserTaxonomicGroupTable(String taxCode, String taxTable)
        {
            UserTaxonomicGroupTable userTaxTable;
            userTaxTable = RetrieveUserTaxonomicGroupTable(taxCode);

            if (userTaxTable != null)
            {
                if (!userTaxTable.TaxonomicTable.Equals(taxTable))
                {
                    userTaxTable.TaxonomicTable = taxTable;
                    try
                    {
                        Update(userTaxTable);
                    }
                    catch (DataFunctionsException ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                try
                {
                    CreateUserTaxonomicGroupTable(taxCode, taxTable);
                }
                catch (DataFunctionsException ex)
                {
                    throw ex;
                }
            }
        }

        //public void CreateIdentification(Identification lastIdent, string taxonName, string nameURI)
        //{
        //    Identification ident = SERIALIZER.CreateISerializableObject<Identification>();
        //    SERIALIZER.ConnectOneToMany(RetrieveIdentificationUnit((int)lastIdent.IdentificationUnitID),ident);
        //    ident.CollectionSpecimenID = lastIdent.CollectionSpecimenID;
        //    ident.IdentificationUnitID = lastIdent.IdentificationUnitID;
        //    ident.IdentificationSequence = lastIdent.IdentificationSequence;
        //    ident.IdentificationSequence++;
        //    ident.TaxonomicName = taxonName;
        //    ident.NameURI = nameURI;

        //    // Current User as responsible agent
        //    if (UserProfiles.Instance.Current != null)
        //    {
        //        ident.ResponsibleName = UserProfiles.Instance.Current.CombinedNameCache;

        //        if (UserProfiles.Instance.Current.AgentURI != null)
        //            ident.ResponsibleAgentURI = UserProfiles.Instance.Current.AgentURI;
        //    }

        //    con.Save(ident);
        //}
        
        public TaxonNames CreateTaxonNames()
        {
            TaxonNames tax = TaxonomySERIALIZER.CreateISerializableObject<TaxonNames>();
            return tax;
        }

        public PropertyNames CreatePropertyNames()
        {
            PropertyNames pr = TaxonomySERIALIZER.CreateISerializableObject<PropertyNames>();
            return pr;
        }


        #endregion

        #region Remove

        public void Remove(ISerializableObject iso)
        {
            if (iso != null)
            {
                try
                {
                    con.Delete(iso);
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Object (" + iso.GetType().Name + ") couldn't be removed.");
                }
            }
        }

        # endregion

        #region Update

        public void Update(ISerializableObject iso)
        {
            if (iso != null)
            {
                try
                {
                    con.Save(iso);
                }
                catch (Exception)
                {
                    throw new DataFunctionsException("Object ("+iso.GetType().Name+") couldn't be updated.");
                }
            }
        }

        #endregion

        #region Retrieve

        public IEnumerable<CollectionEventLocalisation> RetrieveLocalisationSystemForCollectionEvent(CollectionEvent ce)
        {
            if (ce != null)
                return ce.CollectionEventLocalisation;
            else
                return new List<CollectionEventLocalisation>();
        }

        public IEnumerable<CollectionEventProperty> RetrievePropertyForCollectionEvent(CollectionEvent ce)
        {
            if (ce != null)
                return ce.CollectionEventProperties;
            else
                return new List<CollectionEventProperty>();
        }

        public IList<CollectionEventImage> RetrieveImagesForCollectionEvent(String imageType, CollectionEvent ce)
        {
            IList<CollectionEventImage> ceImgList;
            try
            {
                IRestriction idRes = RestrictionFactory.Eq(typeof(CollectionEventImage), "_CollectionEventID", ce.CollectionEventID);
                IRestriction typeRes = RestrictionFactory.Eq(typeof(CollectionEventImage), "_ImageType", imageType);
                IRestriction restrict = RestrictionFactory.And().Add(idRes).Add(typeRes);
                ceImgList = con.LoadList<CollectionEventImage>(restrict);
            }
            catch (Exception)
            {
                ceImgList = new List<CollectionEventImage>();
            }
            return ceImgList;
        }

        public CollectionEvent RetrieveCollectionEvent(int id)
        {
            CollectionEvent ce;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionEvent), "_CollectionEventID", id);
                ce = con.Load<CollectionEvent>(restrict);
                return ce;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("Collection Event (ID: " + id + ") can't be loaded.");
            }
        }

        public IList<CollectionEventSeries> RetrieveEventSeries()
        {
            IList<CollectionEventSeries> ceSeriesList;

            try
            {
                IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollectionEventSeries));
                ceSeriesList = con.LoadList<CollectionEventSeries>(restrict);
            }
            catch (Exception)
            {
                ceSeriesList = new List<CollectionEventSeries>();
            }

            return ceSeriesList;
        }

        public CollectionEventSeries RetrieveEventSeries(int SeriesID)
        {
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionEventSeries), "_SeriesID", SeriesID);
                CollectionEventSeries series = con.Load<CollectionEventSeries>(restrict);
                return series;
            }
            catch (Exception)
            {
                throw new DataFunctionsException("Collection Event Series (ID: " + SeriesID + ") can't be loaded.");
            }
        }

        public IList<CollectionEvent> RetrieveCollectionEventsForSeries(int? seriesId)//Sollte nir für SeriesID==null verwendet werden
        {
            IList<CollectionEvent> ceList;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionEvent), "_SeriesID", seriesId);
                ceList = con.LoadList<CollectionEvent>(restrict);
            }
            catch (Exception)
            {
                ceList = new List<CollectionEvent>();
            }
            return ceList;
        }

        public CollectionSpecimen RetrieveCollectionSpecimen(int id)
        {
            CollectionSpecimen cs;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionSpecimen), "_CollectionSpecimenID", id);
                cs = con.Load<CollectionSpecimen>(restrict);
            }
            catch (Exception)
            {
                cs = null;
            }
            return cs;
        }

        public IdentificationUnit RetrieveIdentificationUnit(int identificationUnitId)
        {
            IdentificationUnit iu;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(IdentificationUnit), "_IdentificationUnitID", identificationUnitId);
                iu = con.Load<IdentificationUnit>(restrict);
            }
            catch (Exception)
            {
                iu = null;
            }

            return iu;
        }

        public IList<Identification> RetrieveIdentificationsForIU(int iuID)
        {
            IList<Identification> identList;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(Identification), "_IdentificationUnitID", iuID);
                identList = con.LoadList<Identification>(restrict);
            }
            catch (Exception)
            {
                identList = new List<Identification>();
            }
            return identList;
        }

        public IList<Analysis> RetrievePossibleAnalysis(string taxonomicGroup)
        {
            IList<Analysis> analysisList;
            try
            {
                IRestriction r1 = RestrictionFactory.Eq(typeof(AnalysisTaxonomicGroup), "_TaxonomicGroup", taxonomicGroup);
                IRestriction r2 = RestrictionFactory.Eq(typeof(Analysis), "_OnlyHierarchy", false);
                IRestriction restrict = RestrictionFactory.And().Add(r1).Add(r2);
                analysisList = con.LoadList<Analysis>(restrict);
            }
            catch (Exception)
            {
                analysisList = new List<Analysis>();
            }
            return analysisList;
        }

        //private void RetrieveChildAnalysis(int analysisID, IList<Analysis> list)
        //{
        //    IList<Analysis> anaList = new List<Analysis>();
        //    try
        //    {
        //        IRestriction restrict = RestrictionFactory.Eq(typeof(Analysis), "_AnalysisParentID", analysisID);
        //        anaList = con.LoadList<Analysis>(restrict);
        //    }
        //    catch(Exception)
        //    {
        //        return;
        //    }

        //    foreach (Analysis analysis in anaList)
        //    {
        //        if (analysis != null)
        //        {
        //            if (!analysis.OnlyHierarchy)
        //                list.Add(analysis);

        //            RetrieveChildAnalysis((int)analysis.AnalysisID, list);
        //        }
        //    } 
        //}

        public IList<LocalisationSystem> RetrievePossibleLocalisationSystem(int eventID)
        {
            IList<LocalisationSystem> localisationSystemList;
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("LOCALISATIONSYSTEMID NOT IN (SELECT LOCALISATIONSYSTEMID FROM COLLECTIONEVENTLOCALISATION WHERE COLLECTIONEVENTID=");
                query.Append(eventID).Append(")");
                IRestriction restrict = RestrictionFactory.SqlRestriction(typeof(LocalisationSystem), query.ToString());
                localisationSystemList = con.LoadList<LocalisationSystem>(restrict);
            }
            catch (Exception)
            {
                localisationSystemList = new List<LocalisationSystem>();
            }
            return localisationSystemList;
        }

        public IList<Property> RetrievePossibleProperty(int eventID)
        {
            IList<Property> propertyList;

            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("PROPERTYID NOT IN (SELECT PROPERTYID FROM COLLECTIONEVENTPROPERTY WHERE COLLECTIONEVENTID=");
                query.Append(eventID).Append(")");
                IRestriction restrict = RestrictionFactory.SqlRestriction(typeof(Property), query.ToString());
                propertyList = con.LoadList<Property>(restrict);
            }
            catch (Exception)
            {
                propertyList = new List<Property>();
            }

            return propertyList;
        }

        public Analysis RetrieveAnalysis(int id)
        {
            Analysis analysis;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(Analysis), "_AnalysisID", id);
                analysis = (Analysis)con.Load(typeof(Analysis), restrict);
            }
            catch (Exception)
            {
                analysis = null;
            }
            return analysis;
        }

        public IList<AnalysisResult> RetrieveAnalysisResults(int AnalysisID)
        {
            IList<AnalysisResult> analysisList;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(AnalysisResult), "_AnalysisID", AnalysisID);
                analysisList = con.LoadList<AnalysisResult>(restrict);
            }
            catch (Exception)
            {
                analysisList = new List<AnalysisResult>();
            }
            return analysisList;
        }

        public LocalisationSystem RetrieveLocalisationSystem(int locID)
        {
            LocalisationSystem loc;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(LocalisationSystem), "_LocalisationSystemID", locID);
                loc = (LocalisationSystem)con.Load(typeof(LocalisationSystem), restrict);
            }
            catch (Exception)
            {
                loc = null;
            }

            return loc;
        }

        public Property RetrieveProperty(int propID)
        {
            Property prop;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(Property), "_PropertyID", propID);
                prop = (Property)con.Load(typeof(Property), restrict);
            }
            catch (Exception)
            {
                prop = null;
            }

            return prop;
        }

        public CollectionEventLocalisation RetrieveCollectionEventLocalisation(int locID, int eventID)
        {
            CollectionEventLocalisation cel;
            try
            {
                IRestriction r1 = RestrictionFactory.Eq(typeof(CollectionEventLocalisation), "_CollectionEventID", eventID);
                IRestriction r2 = RestrictionFactory.Eq(typeof(CollectionEventLocalisation), "_LocalisationSystemID", locID);
                IRestriction restrict = RestrictionFactory.And().Add(r1).Add(r2);
                IList<CollectionEventLocalisation> celist = this.con.LoadList<CollectionEventLocalisation>(restrict);
                cel = celist.First();
            }
            catch (Exception)
            {
                cel = null;
            }
            return cel;
        }

        public CollectionEventProperty RetrieveCollectionEventProperty(int propID, int eventID)
        {
            CollectionEventProperty ceProp;
            try
            {
                IRestriction restrict1 = RestrictionFactory.Eq(typeof(CollectionEventProperty), "_PropertyID", propID);
                IRestriction restrict2 = RestrictionFactory.Eq(typeof(CollectionEventProperty), "_CollectionEventID", eventID);
                IRestriction restrict = RestrictionFactory.And().Add(restrict1).Add(restrict2);

                ceProp = (CollectionEventProperty)con.Load(typeof(CollectionEventProperty), restrict);
            }
            catch (Exception)
            {
                ceProp = null;
            }
            
            return ceProp;
        }

        public CollectionAgent RetrieveCollectionAgent(string collectorsName, int csID)
        {
            CollectionAgent agent;
            try
            {
                IRestriction restrict1 = RestrictionFactory.Eq(typeof(CollectionAgent), "_CollectorsName", collectorsName);
                IRestriction restrict2 = RestrictionFactory.Eq(typeof(CollectionAgent), "_CollectionSpecimenID", csID);
                IRestriction restrict = RestrictionFactory.And().Add(restrict1).Add(restrict2);

                agent = (CollectionAgent)con.Load(typeof(CollectionAgent), restrict);
            }
            catch (Exception)
            {
                agent = null;
            }

            return agent;
        }

        public IList<CollectionAgent> RetrieveAgentForCollectionSpecimen(int csID)
        {
            IList<CollectionAgent> agentList;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionAgent), "_CollectionSpecimenID", csID);
                agentList = con.LoadList<CollectionAgent>(restrict);
            }
            catch (Exception)
            {
                agentList = new List<CollectionAgent>();
            }

            return agentList;
        }

        public IList<CollectionSpecimenImage> RetrieveImagesForCollectionSpecimen(int csID,String imageType)
        {
            IList<CollectionSpecimenImage> csImgList;
            try
            {
                IRestriction idRes = RestrictionFactory.Eq(typeof(CollectionSpecimenImage), "_CollectionSpecimenID", csID);
                IRestriction typeRes = RestrictionFactory.Eq(typeof(CollectionSpecimenImage), "_ImageType", imageType);
                IRestriction restrict = RestrictionFactory.And().Add(idRes).Add(typeRes);
                csImgList = con.LoadList<CollectionSpecimenImage>(restrict);
            }
            catch (Exception)
            {
                csImgList = new List<CollectionSpecimenImage>();
            }
            return csImgList;
        }

        public IList<CollectionSpecimenImage> RetrieveImagesForCollectionSpecimen(int csID, int iuID,String imageType)
        {
            IList<CollectionSpecimenImage> csImgList;
            try
            {
                IRestriction restrict1 = RestrictionFactory.Eq(typeof(CollectionSpecimenImage), "_IdentificationUnitID", iuID);
                IRestriction restrict2 = RestrictionFactory.Eq(typeof(CollectionSpecimenImage), "_CollectionSpecimenID", csID);
                IRestriction typeRes = RestrictionFactory.Eq(typeof(CollectionSpecimenImage), "_ImageType", imageType);
                IRestriction idRes = RestrictionFactory.And().Add(restrict1).Add(restrict2);
                IRestriction restrict = RestrictionFactory.And().Add(idRes).Add(typeRes);
                csImgList = con.LoadList<CollectionSpecimenImage>(restrict);
            }
            catch (Exception)
            {
                csImgList = new List<CollectionSpecimenImage>();
            }
            return csImgList;
        }

        public IEnumerable<IdentificationUnitAnalysis> RetrieveIdentificationUnitAnalysisForIdentificationUnit(IdentificationUnit iu)
        {
            if (iu != null)
                return iu.IdentificationUnitAnalysis;
            else
                return new List<IdentificationUnitAnalysis>();
        }

        public IList<IdentificationUnitGeoAnalysis> RetrieveIdentificationUnitGeoAnalysisForIdentificationUnit(IdentificationUnit iu)
        {
            IList<IdentificationUnitGeoAnalysis> iuGeoAnalysisList;

            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(IdentificationUnitGeoAnalysis), "_IdentificationUnitID", iu.IdentificationUnitID);
                iuGeoAnalysisList = con.LoadList<IdentificationUnitGeoAnalysis>(restrict);
            }
            catch (Exception)
            {
                iuGeoAnalysisList = new List<IdentificationUnitGeoAnalysis>();
            }

            return iuGeoAnalysisList;
        }


        //AnalaysisDate ist als primary key ungeeignet
        //public IdentificationUnitGeoAnalysis RetrieveIdentificationUnitGeoAnalysis(int iuID, int csID, DateTime date)
        //{
        //    IdentificationUnitGeoAnalysis iuGeoAnalysis;

        //    try
        //    {
        //        IRestriction restrict1 = RestrictionFactory.Eq(typeof(IdentificationUnitGeoAnalysis), "_IdentificationUnitID", iuID);
        //        IRestriction restrict2 = RestrictionFactory.Eq(typeof(IdentificationUnitGeoAnalysis), "_CollectionSpecimenID", csID);
        //        IRestriction restrict3 = RestrictionFactory.Eq(typeof(IdentificationUnitGeoAnalysis), "_AnalysisDate", date);

        //        IRestriction restrict = RestrictionFactory.And().Add(restrict1).Add(restrict2).Add(restrict3);

        //        iuGeoAnalysis = (IdentificationUnitGeoAnalysis)con.Load(typeof(IdentificationUnitGeoAnalysis), restrict);
        //    }
        //    catch(Exception)
        //    {
        //        iuGeoAnalysis = null;
        //    }

        //    return iuGeoAnalysis;
        //}

        public IdentificationUnitAnalysis RetrieveIdentificationUnitAnalysis(int CollectionSpecimenID, int IdentificationUnitID, int AnalysisID, string AnalysisNumber)
        {
            IdentificationUnitAnalysis iua;

            try
            {
                IRestriction restrict1 = RestrictionFactory.Eq(typeof(IdentificationUnitAnalysis), "_IdentificationUnitID", IdentificationUnitID);
                IRestriction restrict2 = RestrictionFactory.Eq(typeof(IdentificationUnitAnalysis), "_CollectionSpecimenID", CollectionSpecimenID);
                IRestriction restrict3 = RestrictionFactory.Eq(typeof(IdentificationUnitAnalysis), "_AnalysisID", AnalysisID);
                IRestriction restrict4 = RestrictionFactory.Eq(typeof(IdentificationUnitAnalysis), "_AnalysisNumber", AnalysisNumber);

                IRestriction restrict = RestrictionFactory.And().Add(restrict1).Add(restrict2).Add(restrict3).Add(restrict4);
                iua = (IdentificationUnitAnalysis)con.Load(typeof(IdentificationUnitAnalysis), restrict);
            }
            catch (Exception)
            {
                iua = null;
            }

            return iua;
        }

        public IList<CollEventImageType_Enum> RetrieveCollectionEventImageType()
        {
            IList<CollEventImageType_Enum> ceImgTypeList;

            try
            {
                IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollEventImageType_Enum));
                ceImgTypeList = con.LoadList<CollEventImageType_Enum>(restrict);
            }
            catch (Exception)
            {
                ceImgTypeList = new List<CollEventImageType_Enum>();
            }

            return ceImgTypeList;
        }

        public IList<CollTaxonomicGroup_Enum> RetrieveTaxonomicGroups()
        {
            IList<CollTaxonomicGroup_Enum> collTaxGroupList;

            try
            {
                String sql = "SELECT * FROM CollTaxonomicGroup_Enum ORDER BY DisplayOrder DESC";
                collTaxGroupList = con.LoadList<CollTaxonomicGroup_Enum>(sql);
            }
            catch (Exception)
            {
                collTaxGroupList = new List<CollTaxonomicGroup_Enum>();
            }
            return collTaxGroupList;
        }

        public int? RetrieveMaxDisplayOrderSpecimen(CollectionSpecimen cs)
        {
            int? max = 0;
            String where = "CollectionSpecimenID";
            String s = null;
            try
            {
                IList<String> t = con.selectWhatFromWhere<String>("MAX(DisplayOrder)", "IdentificationUnit", where, cs.CollectionSpecimenID.ToString());
                if (t.Count == 0)
                    return 0;
                s = t[0];
            
                max = Int32.Parse(s);
            }
            catch (Exception)
            {
                return -1;
            }
            return max;
        }

        public int? RetrieveMaxDisplayOrderCollectionSpecimenIdentificationUnit(CollectionSpecimen cs)
        {
            int? max = 0;
            String where= "CollectionSpecimenID=" + cs.CollectionSpecimenID + " AND RelatedUnitID";
            String s = null;
            
            try
            {
                IList<String> t = con.selectWhatFromWhere<String>("MAX(DisplayOrder)", "IdentificationUnit", where, null);
                if (t.Count == 0)
                    return 0;
                s = t[0];
            
                max = Int32.Parse(s);
            }
            catch (Exception)
            {
                return -1;
            }
            return max;
        }

        public IList<CollUnitRelationType_Enum> RetrieveUnitRelationTypes()
        {
            IList<CollUnitRelationType_Enum> unitRelationTypeList;

            try
            {
                IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollUnitRelationType_Enum));
                unitRelationTypeList = con.LoadList<CollUnitRelationType_Enum>(restrict);
            }
            catch (Exception)
            {
                unitRelationTypeList = new List<CollUnitRelationType_Enum>();
            }

            return unitRelationTypeList;
        }

        public IList<CollCircumstances_Enum> RetrieveCircumstancesTypes()
        {
            IList<CollCircumstances_Enum> unitCircumstancesList;
            try
            {
                IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollCircumstances_Enum));
                unitCircumstancesList = con.LoadList<CollCircumstances_Enum>(restrict);
            }
            catch (Exception)
            {
                unitCircumstancesList = new List<CollCircumstances_Enum>();
            }

            return unitCircumstancesList;
        }

        public IList<CollSpecimenImageType_Enum> RetrieveCollectionSpecimenImageType()
        {
            IList<CollSpecimenImageType_Enum> csImgTypeList;
            try
            {
                IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(CollSpecimenImageType_Enum));
                csImgTypeList = con.LoadList<CollSpecimenImageType_Enum>(restrict);
            }
            catch (Exception)
            {
                csImgTypeList = new List<CollSpecimenImageType_Enum>();
            }

            return csImgTypeList;
        }

        public IList<TaxonNames> RetrieveTaxonomy(String input, String taxGroup)
        {
            Connector taxonomyCon = TaxonomySERIALIZER.Connector;
            //Kann auch per Konvention mit Tabellenname gleich der Taxonomischen Gruppe gelöst werden
            String table;
            table = getTable(taxGroup);
            IList<TaxonNames> taxNameList = new List<TaxonNames>();
            if (table == null)
                return taxNameList;
            try
            {
                IRestriction restrict = RestrictionFactory.Like(typeof(TaxonNames), "_TaxonNameCache", input);
                taxNameList = taxonomyCon.LoadList<TaxonNames>(restrict, table);
            }
            catch (Exception) { 
                taxNameList = new List<TaxonNames>(); 
            }
            return taxNameList;
        }

        public IList<SamplingPlots> RetrieveAllSamplingPlots()
        {
            Connector sampCon = TaxonomySERIALIZER.Connector;
            IList<SamplingPlots> samplingPlots;
            try
            {
                IRestriction restrict = RestrictionFactory.TypeRestriction(typeof(SamplingPlots));
                samplingPlots = sampCon.LoadList<SamplingPlots>(restrict);
            }
            catch (Exception)
            {
                samplingPlots = new List<SamplingPlots>();
            }
            return samplingPlots;
        }

        public SamplingPlots RetrieveSamplingPlot(String name)
        {
            SamplingPlots plot;
            try
            {
                Connector sampCon = TaxonomySERIALIZER.Connector;
                IRestriction restrict = RestrictionFactory.Eq(typeof(SamplingPlots), "_Name", name);
                plot = sampCon.Load<SamplingPlots>(restrict);
            }
            catch (Exception)
            {
                plot = null;
            }
            return plot;
        }

        public IList<String> RetrieveTaxonCache(String input, String taxGroup)
        {
            String table = getTable(taxGroup);
            
            if (table == null || table.Equals(String.Empty))
                return new List<String>();

            IList<String> taxNameList = new List<String>();

            try
            {
                StringBuilder sb = new StringBuilder("SELECT Top(10) TaxonNameCache FROM ");
                sb.Append(table).Append(" WHERE TaxonNameCache LIKE '").Append(input).Append("'"); ;
                String sql = sb.ToString();
                DbConnection con = TaxonomySERIALIZER.CreateConnection();
                con.Open();
                DbCommand comm = con.CreateCommand();
                comm.CommandText = sql;
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    taxNameList.Add(reader.GetString(0));
                }
                con.Close();
            }
            catch (Exception)
            {
                taxNameList = new List<String>();
            }
            
            return taxNameList;
        }

        public IList<String> RetrieveTaxonCache(String taxonName, String genus, String epitheton, String taxGroup)
        {
            String table = getTable(taxGroup);

            if (table == null || table.Equals(String.Empty))
                return new List<String>();

            IList<String> taxNameList = new List<String>();
            String sql = null;
            try
            {
                StringBuilder sb = new StringBuilder("SELECT Top(10) TaxonNameCache FROM ");
                sb.Append(table).Append(" WHERE TaxonNameSinAuthors LIKE '").Append(taxonName).Append("%'");
                sb.Append(" AND GenusOrSupragenericName LIKE '").Append(genus).Append("%'");
                sb.Append(" AND SpeciesEpithet LIKE '").Append(epitheton).Append("%'");
                sql = sb.ToString();
                DbConnection con = TaxonomySERIALIZER.CreateConnection();
                con.Open();
                DbCommand comm = con.CreateCommand();
                comm.CommandText = sql;
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    taxNameList.Add(reader.GetString(0));
                }
                con.Close();
            }
            catch (Exception e)
            {
                taxNameList = new List<String>();
            }

            return taxNameList;
        }

        public TaxonNames RetrieveTaxon(string input, string taxGroup)
        {
            Connector taxonomyCon = TaxonomySERIALIZER.Connector;
            //Kann auch per Konvention mit Tabellenname gleich der Taxonomischen Gruppe gelöst werden
            string table;
            table = getTable(taxGroup);
            if (table == null)
                return null;//Exception!!!

            try
            {
                IRestriction restrict = RestrictionFactory.Like(typeof(TaxonNames), "_TaxonNameCache", input);
                TaxonNames tax = (TaxonNames)taxonomyCon.Load(typeof(TaxonNames), table, restrict);
                return tax;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TaxonNames RetrieveTaxParent(int unitID)
        {
            try
            {
                Connector con = SERIALIZER.Connector;
                IRestriction restrict = RestrictionFactory.Eq(typeof(Identification), "_IdentificationUnitID", unitID);
                Identification ident = con.Load<Identification>(restrict);

                TaxonNames tax = SERIALIZER.CreateISerializableObject<TaxonNames>();
                tax.NameURI = ident.NameURI;
                tax.TaxonNameCache = ident.TaxonomicName;
                return tax;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<PropertyNames> RetrievePropertyNamesChildren(String table, int parentID)
        {
            Connector taxonomyCon = TaxonomySERIALIZER.Connector;
            IList<PropertyNames> propNameList = new List<PropertyNames>();
            if (table == null || table.Equals(String.Empty))
                return propNameList;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(PropertyNames), "_BroaderTermID", parentID);
                propNameList = taxonomyCon.LoadList<PropertyNames>(restrict, table);
            }
            catch (Exception)
            {
                propNameList = new List<PropertyNames>();
            }
            return propNameList;
        }

        public IList<String> RetrievePropertyNames(String table, String input)
        {
            if (table == null || table.Equals(String.Empty))
                return new List<String>();

            IList<String> propNameList = new List<String>();

            try
            {
                StringBuilder sb = new StringBuilder("SELECT Top(10) DisplayText FROM ");
                sb.Append(table).Append(" WHERE DisplayText LIKE '").Append(input).Append("'"); ;
                String sql = sb.ToString();
                DbConnection con = TaxonomySERIALIZER.CreateConnection();
                con.Open();
                DbCommand comm = con.CreateCommand();
                comm.CommandText = sql;
                DbDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    propNameList.Add(reader.GetString(0));
                }
                con.Close();
            }
            catch (Exception)
            {
                propNameList = new List<String>();
            }
            
            return propNameList;
        }

        public PropertyNames RetrievePropertyName(String table, String input)
        {
            Connector taxonomyCon = TaxonomySERIALIZER.Connector;
            IList<PropertyNames> propNameList = new List<PropertyNames>();
            
            if (table == null || table.Equals(String.Empty))
                return null;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(PropertyNames), "_DisplayText", input);
                propNameList = taxonomyCon.LoadList<PropertyNames>(restrict);
            }
            catch (Exception)
            {
                return null;
            }
            if (propNameList.Count == 0)
                return null;
            if (propNameList.Count > 1)
            {
                return propNameList[0];//Namensgleichheit aber evtl anderes Konzept
            }
            else
                return propNameList[0];
        }

        //public PropertyNames RetrievePropertyName(string table, string input)
        //{
        //    Connector taxonomyCon = TaxonomySERIALIZER.Connector;
        //    IList<PropertyNames> propNameList = new List<PropertyNames>();
        //    if (table == null)
        //        return null;
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder("SELECT Top(10) * FROM ");
        //        sb.Append(table).Append(" WHERE DisplayText LIKE '").Append(input).Append("'"); ;
        //        String sql = sb.ToString();
        //        DbConnection con = TaxonomySERIALIZER.CreateConnection();
        //        con.Open();
        //        DbCommand comm = con.CreateCommand();
        //        comm.CommandText = sql;
        //        DbDataReader reader = comm.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            PropertyNames pn = new PropertyNames();//eigentlich über Serializer gehen
        //            pn. reader.GetString(0));
        //        }
        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public PropertyNames RetrievePropertyName(String table, int id)
        {
            Connector taxonomyCon = TaxonomySERIALIZER.Connector;
            PropertyNames propName;
            if (table == null)
                return null;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(PropertyNames), "_TermID", id);
                propName = taxonomyCon.Load<PropertyNames>(restrict);
            }
            catch (Exception)
            {
                return null;
            }
            return propName;
        }

        public IList<PropertyNames> RetrieveTopLevelPropertyNames(String table)
        {
            Connector taxonomyCon = TaxonomySERIALIZER.Connector;
            IList<PropertyNames> propNameList = new List<PropertyNames>();
            if (table == null || table.Equals(String.Empty))
                return propNameList;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(PropertyNames), "_BroaderTermID", "");
                propNameList = taxonomyCon.LoadList<PropertyNames>(restrict);
            }
            catch (Exception)
            {
                propNameList = new List<PropertyNames>();
            }
            return propNameList;
        }

        public UserTaxonomicGroupTable RetrieveUserTaxonomicGroupTable(string taxCode)
        {
            UserTaxonomicGroupTable userTaxTable;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(UserTaxonomicGroupTable), "_TaxonomicCode", taxCode);
                userTaxTable = con.Load<UserTaxonomicGroupTable>(restrict);
            }
            catch (Exception)
            {
                userTaxTable = null;
            }
            
            return userTaxTable;
        }

        public IList<TaxonListsForUser> RetrieveTaxonListsForGroup(string taxCode)
        {
            IList<TaxonListsForUser> list=null;
            Connector taxonomyCon = TaxonomySERIALIZER.Connector;
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(TaxonListsForUser), "_TaxonomicGroup", taxCode);
                list = taxonomyCon.LoadList<TaxonListsForUser>(restrict);
            }
            catch (Exception)
            {
                return null;
            }
            return list;
        }

        #endregion

        private String getTable(string taxGroup)
        {
            try
            {
                Connector con = SERIALIZER.Connector;
                IRestriction restrict = RestrictionFactory.Eq(typeof(UserTaxonomicGroupTable), "_TaxonomicCode", taxGroup);
                UserTaxonomicGroupTable userTable = con.Load<UserTaxonomicGroupTable>(restrict);

                if (userTable != null)
                {
                    return userTable.TaxonomicTable;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

    }
}
