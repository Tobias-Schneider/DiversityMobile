using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class CollectionProject:ISerializableObject
    {
        #region Instance data
        [ID]
        [Column]
        private int _CollectionSpecimenID;

        [ID]
        [Column]
        private int _ProjectID;

        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        [Column]
        private DateTime _LogUpdatedWhen;

        #endregion

        #region Default Constructor

        public CollectionProject()
        {
        }

        #endregion

        #region Properties

        public int CollectionSpecimenID { get { return _CollectionSpecimenID; } set { _CollectionSpecimenID = value; } }
        public int ProjectID { get { return _ProjectID; } set { _ProjectID = value; } }
        public DateTime LogTime { get { return _LogUpdatedWhen; } set { _LogUpdatedWhen = value; } }
        public Guid Rowguid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        #endregion

        #region ToString override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("CollectionProject: ");
            if (this._ProjectID != null)
            {
                sb.Append(this._ProjectID).Append(",");
            }
            if (this._CollectionSpecimenID != null)
                sb.Append(this._CollectionSpecimenID);
            return sb.ToString();
        }

        #endregion

    }
}
