using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

namespace UBT.AI4.Bio.DivMobi.DataLayer.DataItems
{
    public class AnalysisResult : ISerializableObject
    {
        #region Instance Data

        [ID]
        [Column]
        private int _AnalysisID;
        [ID]
        [Column]
        private String _AnalysisResult;
        [Column]
        private String _Description;
        [Column]
        private String _DisplayText;
        [Column]
        private int _DisplayOrder;
        [Column]
        private string _Notes;

        [Column]
        private DateTime _LogUpdatedWhen;

        [RowGuid]
        [Column(Mapping = "rowguid")]
        private Guid _guid;

        //[ManyToOne]
        //[MappedBy("_AnalysisResults")]
        //private Analysis _analysis;

        #endregion

        #region Default constructor

        public AnalysisResult()
        {
        }

        #endregion

        #region Properties

        public int AnalysisID { get { return _AnalysisID; } set { _AnalysisID = value; } }
        public String Analysisresult{ get { return _AnalysisResult; } set { _AnalysisResult = value; } }
        public String Description { get { return _Description; } set { _Description = value; } }
        public String DisplayText { get { return _DisplayText; } set { _DisplayText = value; } }
        public int DisplayOrder { get { return _DisplayOrder; } set { _DisplayOrder = value; } }
        public String Notes { get { return _Notes; } set { _Notes = value; } }
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
            if (this.DisplayText != null)
            {
                String text ="AnalysisResult: "+ this.DisplayText;
                return text;
            }
            else return AttributeWorker.ToStringHelper(this, 30);
        }

        #endregion
    }
}
