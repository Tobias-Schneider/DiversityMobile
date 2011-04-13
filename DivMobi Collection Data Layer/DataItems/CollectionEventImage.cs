using System;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using UBT.AI4.Bio.DivMobi.DataLayer.SyncAttributes;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class CollectionEventImage : ISerializableObject
    {
        #region Instance Data

        [ID]
        [Column]
        private int?    _CollectionEventID;
        [ID]
        [Column]
        private string  _URI;
        //[Column]
        //private string  _ResourceURI;
        [Column]
        [DirectSync]
        private string  _ImageType;
        //[Column]
        //private string  _Notes;
        [Column]
        private DateTime _LogUpdatedWhen;
        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        //[ManyToOne]
        //[MappedBy("_collectionEventImages")]
        //private CollectionEvent _collectionEvent;

        //[ManyToOne]
        //[MappedBy("_collectionEventImages")]
        //private CollEventImageType_Enum _collectionEventImageType;
        #endregion



        #region Default constructor

        public CollectionEventImage()
        {
            this._ImageType = "photography";
        }

        #endregion



        #region Properties

        public int? CollectionEventID { get { return _CollectionEventID; } set { _CollectionEventID = value; } }
        public string URI { get { return _URI; } set { _URI = value; } }
        //public string ResourceURI { get { return _ResourceURI; } set { _ResourceURI = value; } }
        public string ImageType { get { return _ImageType; } set { _ImageType = value; } }
        //public string Notes { get { return _Notes; } set { _Notes = value; } }
        public DateTime LogTime { get { return _LogUpdatedWhen; } set { _LogUpdatedWhen = value; } }
        public Guid Rowguid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        //public CollectionEvent CollectionEvent 
        //{
        //    get { return _collectionEvent; }
        //    set { _collectionEvent = value; } 
        //}
        //public CollEventImageType_Enum CollEventImageType_Enum
        //{
        //    get { return _collectionEventImageType; }
        //    set { _collectionEventImageType = value; }
        //}
        #endregion



        #region ToString override

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("CollectionEventImage: ");
            if (this._ImageType != null)
            {
                sb.Append(this._ImageType).Append(",");
            }
            if (this._CollectionEventID != null)
            {
                sb.Append(this._CollectionEventID).Append(",");
            }
            if (this._URI != null)
                sb.Append(this._URI);
            return sb.ToString();
        }

        #endregion
    }
}
