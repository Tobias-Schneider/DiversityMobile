using System;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Collections;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class AnalysisTaxonomicGroup : ISerializableObject
    {
        #region Instance Data

        [ID]
        [Column]
        private int    _AnalysisID;
        [ID]
        [Column]
        private string  _TaxonomicGroup;

        [Column]
        private DateTime _LogUpdatedWhen;

        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;


        [ManyToOne]
        [MappedBy("_analysisTaxonomicGroups")]
        private Analysis _analysis;

        #endregion



        #region Default constructor

        public AnalysisTaxonomicGroup()
        {
            
        }

        #endregion



        #region Properties

        public int AnalysisID { get { return _AnalysisID; } set { _AnalysisID = value; } }
        public string TaxonomicGroup { get { return _TaxonomicGroup; } set { _TaxonomicGroup = value; } }
        public DateTime LogTime { get { return _LogUpdatedWhen; } set { _LogUpdatedWhen = value; } }
        public Guid Rowguid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        //public Analysis Analysis { get { return _analysis; } set { _analysis = value; } }

        #endregion



        #region ToString override

        public override string ToString()
        {
            StringBuilder sb=new StringBuilder("AnalysisTaxonomicGroup: ");
            if (this._analysis!= null && this._analysis.DisplayText!=null)
            {
                sb.Append(this._analysis.DisplayText).Append(",");
            }
            if (this._TaxonomicGroup != null)
                sb.Append(this._TaxonomicGroup);
            return sb.ToString();
        }

        #endregion
    }
}
