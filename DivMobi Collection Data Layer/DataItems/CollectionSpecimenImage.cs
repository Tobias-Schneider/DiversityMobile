using System;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class CollectionSpecimenImage : ISerializableObject
    {
        #region Instance Data
        [ID]
        [Column]
        private int _CollectionSpecimenID;
        [ID]
        [Column]
        private string _URI;
        //[Column]
        //private string _ResourceURI;
        //[Column]
        //private int? _SpecimenPartID;
        [Column]
        private int? _IdentificationUnitID;
        [Column]
        private string _ImageType;
        //[Column]
        //private string  _Notes;
        [Column]
        private DateTime _LogUpdatedWhen;
        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        [ManyToOne]//Bei Gelegenheit eliminieren, wird nur bei der Synchronisation benötigt
        [MappedBy("_collectionSpecimenImages")]
        private CollectionSpecimen _collectionSpecimen;

        //[ManyToOne]
        //[MappedBy("_CollectionSpecimenImages")]
        //private CollSpecimenImageType_Enum _CollSpecimenImageType_Enum;

        //[ManyToOne]
        //[MappedBy("_CollectionSpecimenImages")]
        //private IdentificationUnit _IdentificationUnitProxy;
        #endregion



        #region Default constructor

        public CollectionSpecimenImage()
        {
            this._ImageType = "photography";
        }

        #endregion



        #region Properties

        public int CollectionSpecimenID { get { return _CollectionSpecimenID; } set { _CollectionSpecimenID = value; } }
        public string URI { get { return _URI; } set { _URI = value; } }
        //public string ResourceURI { get { return _ResourceURI; } set { _ResourceURI = value; } }
        //public int? SpecimenPartID { get { return _SpecimenPartID; } set { _SpecimenPartID = value; } }
        public int? IdentificationUnitID { get { return _IdentificationUnitID; } set { _IdentificationUnitID = value; } }
        public string ImageType { get { return _ImageType; } set { _ImageType = value; } }
        //public string Notes { get { return _Notes; } set { _Notes = value; } }
        public DateTime LogTime { get { return _LogUpdatedWhen; } set { _LogUpdatedWhen = value; } }
        public Guid Rowguid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public CollectionSpecimen CollectionSpecimen
        {
            get { return _collectionSpecimen; }
            set { _collectionSpecimen = value; }
        }
        //public CollSpecimenImageType_Enum CollSpecimenImageType_Enum
        //{
        //    get { return _CollSpecimenImageType_Enum; }
        //    set { _CollSpecimenImageType_Enum = value; }
        //}
        //public IdentificationUnit IdentificationUnit
        //{
        //    get { return _IdentificationUnitProxy; }
        //    set { _IdentificationUnitProxy = value; }
        //}
        #endregion



        #region ToString override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("CollectionSpecimenImage: ");
            if (this._ImageType != null)
            {
                sb.Append(this._ImageType).Append(",");
            }
            if (this._CollectionSpecimenID != null)
            {
                sb.Append(this._CollectionSpecimenID).Append(",");
            }
            if (this._IdentificationUnitID != null)
            {
                sb.Append(this._IdentificationUnitID).Append(",");
            }
            if (this._URI != null)
                sb.Append(this._URI);
            return sb.ToString();
        }

        #endregion
    }
}
