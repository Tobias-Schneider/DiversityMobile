using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class UserTaxonomicGroupTable : ISerializableObject
    {
        #region Instance Data

        [ID]
        [Column]
        private string _TaxonomicCode;

        [Column]
        private string _TaxonomicTable;

        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        #endregion

        #region Default constructor

        public UserTaxonomicGroupTable() { }

        #endregion

        #region Properties

        public string TaxonomicCode { get { return _TaxonomicCode; } set { _TaxonomicCode = value; } }
        public string TaxonomicTable { get { return _TaxonomicTable; } set { _TaxonomicTable = value; } }
        
        public Guid Rowguid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public DateTime LogTime { get { return new DateTime(1900, 1, 1); } set { } }//Die Einträge der Tabelle werden nicht synchronisiert. Sie werden nur benötigt um die Schnittstelle zu implementieren.
        #endregion

        #region ToString override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("UserTaxonomicGroupTable: ");
            if (this._TaxonomicCode != null)
            {
                sb.Append(this._TaxonomicCode).Append(",");
            }
            if (this._TaxonomicTable != null)
            {
                sb.Append(this._TaxonomicTable);
            }
            return sb.ToString();
        }

        #endregion
    }
}