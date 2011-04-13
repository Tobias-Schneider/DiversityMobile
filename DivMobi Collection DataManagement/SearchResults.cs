using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public class SearchResults : ConnectedType
    {
        private int _ID;
        private Connector con = SERIALIZER.Connector;

        #region Singleton

        private static SearchResults _instance = null;

        public static SearchResults Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = new SearchResults();
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        throw new ConnectionCorruptedException("Instance of SearchResults couldn't be retrieved. (" + ex.Message + ")");
                    }
                }

                return _instance;
            }
        }

        #endregion

        public IList<ListViewItem> searchEventResults(Dictionary<String, String> searchStrings, bool strategyAND)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            try
            {
                LogicalRestriction restrict = null;

                if (strategyAND)
                {
                    restrict = RestrictionFactory.And();
                }
                else
                {
                    restrict = RestrictionFactory.Or();
                }

                foreach (String key in searchStrings.Keys)
                {
                    if (key != null)
                    {
                        IRestriction r = RestrictionFactory.Like(typeof(CollectionEvent), key, searchStrings[key]);
                        restrict.Add(r);
                    }
                }

                foreach (CollectionEvent ce in con.LoadList<CollectionEvent>(restrict))
                {
                    if (ce != null)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = ce.CollectionEventID.ToString();
                        item.SubItems.Add(ce.CollectorsEventNumber);
                        item.SubItems.Add(ce.CollectionDate.ToShortDateString());
                        item.Tag = ce.CollectionEventID;

                        items.Add(item);
                    }
                }
            }
            catch (Exception) {
                items.Clear();
            }

            return items;
        }

        public List<ListViewItem> searchSpecimenResults(Dictionary<String, String> searchStrings, bool strategyAND)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            try
            {
                LogicalRestriction restrict = null;

                if (strategyAND)
                {
                    restrict = RestrictionFactory.And();
                }
                else
                {
                    restrict = RestrictionFactory.Or();
                }

                foreach (String key in searchStrings.Keys)
                {
                    if (key != null)
                    {
                        IRestriction r = RestrictionFactory.Like(typeof(CollectionSpecimen), key, searchStrings[key]);
                        restrict.Add(r);
                    }
                }

                foreach (CollectionSpecimen cs in con.LoadList<CollectionSpecimen>(restrict))
                {
                    if (cs != null)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = cs.CollectionSpecimenID.ToString();
                        item.Tag = cs.CollectionSpecimenID;

                        items.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                items.Clear();
            }

            return items;
        }

        public List<ListViewItem> searchIUResults(Dictionary<String, String> searchStrings, bool strategyAND)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            try
            {
                LogicalRestriction restrict = null;

                if (strategyAND)
                {
                    restrict = RestrictionFactory.And();
                }
                else
                {
                    restrict = RestrictionFactory.Or();
                }

                foreach (String key in searchStrings.Keys)
                {
                    if (key != null)
                    {
                        IRestriction r = RestrictionFactory.Like(typeof(IdentificationUnit), key, searchStrings[key]);
                        restrict.Add(r);
                    }
                }

                foreach (IdentificationUnit iu in con.LoadList<IdentificationUnit>(restrict))
                {
                    if (iu != null)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = iu.IdentificationUnitID.ToString();
                        item.SubItems.Add(iu.LastIdentificationCache);
                        item.SubItems.Add(iu.TaxonomicGroup);
                        item.SubItems.Add(iu.UnitIdentifier);
                        item.Tag = iu.IdentificationUnitID;

                        items.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                items.Clear();
            }

            return items;
        }

        public void setID(int ID)
        {
            this._ID = ID;
        }

        public CollectionEvent getEvent(int eventID)
        {
            CollectionEvent ce;
            
            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionEvent), "_CollectionEventID", eventID);
                ce = con.Load<CollectionEvent>(restrict);
            }
            catch (Exception)
            {
                ce = null;
            }

            return ce;            
        }

        public CollectionEvent getEventForSpecimen(int specimenID)
        {
            CollectionSpecimen cs = null;

            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionSpecimen), "_CollectionSpecimenID", specimenID);
                cs = con.Load<CollectionSpecimen>(restrict);
            }
            catch (Exception)
            {
                return null;
            }

            //SERIALIZER.ConnectOneToMany(cs, ce);
            if (cs != null)
                return cs.CollectionEvent;
            else
                return null;
        }

        public CollectionEvent getEventForIU(int iuID)
        {
            IdentificationUnit iu;

            try
            {
                IRestriction restrict = RestrictionFactory.Eq(typeof(IdentificationUnit), "_IdentificationUnitID", iuID);
                iu = con.Load<IdentificationUnit>(restrict);
            }
            catch (Exception)
            {
                return null;
            }

            //SERIALIZER.ConnectOneToMany(iu, cs);
            //return this.getEventForSpecimen((int)iu.CollectionSpecimenID);
            if (iu != null)
                return iu.CollectionSpecimen.CollectionEvent;
            else
                return null;
        }

        public CollectionEvent CE
        {
            get
            {
                CollectionEvent ce;

                try
                {
                    IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionEvent), "_CollectionEventID", this._ID);
                    ce = (CollectionEvent)con.Load(typeof(CollectionEvent), restrict);
                }
                catch (Exception)
                {
                    ce = null;
                }
                
                return ce;
            }
        }

        public CollectionSpecimen CS
        {
            get
            {
                CollectionSpecimen cs;
                try
                {
                    IRestriction restrict = RestrictionFactory.Eq(typeof(CollectionSpecimen), "_CollectionSpecimenID", this._ID);
                    cs = (CollectionSpecimen)con.Load(typeof(CollectionSpecimen), restrict);
                }
                catch (Exception)
                {
                    cs = null;
                }
                
                return cs;
            }
        }

        public IdentificationUnit IU
        {
            get
            {
                IdentificationUnit iu;

                try
                {
                    IRestriction restrict = RestrictionFactory.Eq(typeof(IdentificationUnit), "_IdentificationUnitID", this._ID);
                    iu = (IdentificationUnit)con.Load(typeof(IdentificationUnit), restrict);
                }
                catch (Exception)
                {
                    iu = null;
                }

                return iu;
            }
        }
    }
}
