using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems.Taxa
{
    
    public class TaxonNames : ISerializableObject
    {
        #region Instance Data
        [ID]
        [Column]
        private string _NameURI;

        [Column]
        private string _TaxonNameCache;

        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        private string _TaxonomicGroup;
        #endregion

        public TaxonNames()
        {
        }

        #region Properties

        public string NameURI { get { return _NameURI; } set { _NameURI = value; } }

        public string TaxonNameCache { get { return _TaxonNameCache; } set { _TaxonNameCache = value; } }

        public string TaxonomicGroup { get { return _TaxonomicGroup; } set { _TaxonomicGroup = value; } }
        
        #endregion

        #region ToString override

        public override string ToString()
        {
            return AttributeWorker.ToStringHelper(this, 30);
        }

        #endregion

    }
}
