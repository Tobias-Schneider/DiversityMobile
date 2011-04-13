using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class CollEventSeriesImageType_Enum : ISerializableObject
    {
        #region Instance Data
        [ID]
        [Column]
        private string _Code;
        [Column]
        private string _Description;
        [Column]
        private string _DisplayText;
        [Column]
        private short? _DisplayOrder;
        [Column]
        private bool? _DisplayEnable;
        [Column]
        private string _InternalNotes;
        [Column]
        private string _ParentCode;

        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        [OneToMany]
        [JoinColums(DefColumn = "_Code", JoinColumn = "_ImageType")]
        private IDirectAccessIterator<CollectionEventSeriesImage> _CollectionEventSeriesImages;

        #endregion

        #region Default Constructor

        public CollEventSeriesImageType_Enum()
        { 
        }

        #endregion

        #region Properties

        public string Code { get { return _Code; } set { _Code = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string DisplayText { get { return _DisplayText; } set { _DisplayText = value; } }

        public IDirectAccessIterator<CollectionEventSeriesImage> CollectionEventSeriesImage
        {
            get { return _CollectionEventSeriesImages; }
            set { _CollectionEventSeriesImages = value; }
        }
        


        #endregion

        #region ToString override

        public override string ToString()
        {
            return AttributeWorker.ToStringHelper(this, 30);
        }

        #endregion
    }
}
