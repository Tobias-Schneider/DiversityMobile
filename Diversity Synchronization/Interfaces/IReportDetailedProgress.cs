using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;


namespace Diversity_Synchronization
{
    public interface IReportDetailedProgress 
    {
        int ProgressDescriptionID { get;  set; }
        string ProgressOutput { get; set; }
        bool IsProgressIndeterminate { get; set; }

        bool IsCancelRequested { get; }
        bool IsProgressFinished { get; }
        int ProgressPercentage { get; }        
        bool HasInternalProgress { get; }
        int InternalProgressPercentage { get; }

        IAdvanceProgress startInternal(double external);
        IAdvanceProgress startInternal(double external, int internalTotal);
        void advanceProgress(double percent);


        IAdvanceProgress startExternal(double external);
        IAdvanceProgress startExternal(double external, int counterTotal);
        
    }
}
