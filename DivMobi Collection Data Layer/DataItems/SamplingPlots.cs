using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class SamplingPlots: ISerializableObject
    {
        #region Instance Data
        [ID]
        [Column]
        private string _Name;

        [Column]
        private string _Uri;

        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        #endregion

        #region Properties
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public String URI
        {
            get { return _Uri; }
            set { _Uri = value; }
        }
        public DateTime LogTime { get { return new DateTime(1900, 1, 1); } set { } }//Die Tabelle wird nur vom Repository aufs Mobilgerät geschrieben. Etwaige Änderungen sollen bis zum nächsten Clean ignoriert werden.. Deswegen wird defaultmäßig der 1.Januar 1900 zurückgegeben.
        public Guid Rowguid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        #endregion

        #region ToString override

        public override string ToString()
        {
            if (this._Name != null)
            {
                String text = "SamplingPlot: " + this._Name;
                return text;
            }
            else return AttributeWorker.ToStringHelper(this, 30);
        }

        #endregion

    }
}
