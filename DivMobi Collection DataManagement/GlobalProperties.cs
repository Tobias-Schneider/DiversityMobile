using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DivMobi.DataManagement
{
    public static class GlobalProperties
    {
        private static int _CurrentSeriesID = -1;

        public static int CurrentSeriesID
        {
            get
            {
                return _CurrentSeriesID;
            }

            set
            {
                _CurrentSeriesID = value;
            }
        }
    }
}
