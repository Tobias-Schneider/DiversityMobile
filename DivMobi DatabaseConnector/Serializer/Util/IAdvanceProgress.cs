using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util
{
    public interface IAdvanceProgress
    {
        /// <summary>
        /// Advances the operation counter by one step.
        /// </summary>        
        void advance();
        /// <summary>
        /// Advances the operation counter by <value>steps</value> steps.
        /// </summary> 
        void advance(int steps);
        
        /// <summary>
        /// Represents the number of steps you expect this operation to take.
        /// Used to calculate the progress withing this operation.
        /// </summary>
        int InternalTotal { get; set; }

        /// <summary>
        /// Whether the user has requested to abort the current operation
        /// </summary>
        bool IsCancelRequested { get; }
    }
}
