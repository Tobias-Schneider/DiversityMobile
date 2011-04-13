﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class CollectionEventSeries : ISerializableObject 
    {
        #region Instance Data
        [ID(Autoinc = true)]
        [Column]
        private int _SeriesID;
        [Column]
        private string _Description;
        [Column]
        private string _SeriesCode;
        [Column]
        private DateTime? _DateStart;
        [Column]
        private DateTime? _DateEnd;
        [Column]
        private String _Geography;
        [Column]
        private DateTime _LogUpdatedWhen;
        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        [OneToMany(DeleteType = DeleteTypes.NOACTION)]
        [JoinColums(DefColumn = "_SeriesID", JoinColumn = "_SeriesID")]
        private IDirectAccessIterator<CollectionEvent> _CollectionEvents;

        #endregion

        #region DefaultConstructor

        public CollectionEventSeries()
        {
            this._Description = "<TBD>";
            this._DateStart = DateTime.Today;
        }

        #endregion

        #region Properties

        public int SeriesID { get { return _SeriesID; } set { _SeriesID = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string SeriesCode { get { return _SeriesCode; } set { _SeriesCode = value; } }
        public DateTime? DateStart { get { return _DateStart; } set { _DateStart = value; } }
        public DateTime? DateEnd { get { return _DateEnd; } set { _DateEnd = value; } }
        public DateTime LogTime { get { return _LogUpdatedWhen; } set { _LogUpdatedWhen = value; } }
        public String Geography { get { return _Geography; } set { _Geography = value; } }
        public Guid Rowguid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public IDirectAccessIterator<CollectionEvent> CollectionEvents
        {
            get { return _CollectionEvents; }
        }

        #endregion

        #region ToString override
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("CollectionEventSeries: ");
            if (this._SeriesCode != null)
            {
                sb.Append(this._SeriesCode).Append(",");
            }
            if (this._DateStart != null)
                sb.Append(((DateTime)this._DateStart).ToShortDateString()).Append(",");
            if (this._DateEnd != null)
                sb.Append(((DateTime)this._DateEnd).ToShortDateString());
            return sb.ToString();
        }

        #endregion



    }
}