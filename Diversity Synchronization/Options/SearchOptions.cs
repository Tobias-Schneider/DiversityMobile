using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Diversity_Synchronization.Options
{
    public class SearchOptions : IComparable
    {
        private bool truncateDataItems;
        private bool includeMultimedia;
        private int maxItems;

        public SearchOptions()
        {
            //Default-Werte
            this.truncateDataItems = true;
            this.includeMultimedia = false;
            this.maxItems = 500;
        }

        #region Properties

        public bool TruncateDataItems
        {
            get
            {
                return truncateDataItems;
            }
            set
            {
                truncateDataItems = value;
            }
        }

        public bool IncludeMultimedia
        {
            get
            {
                return includeMultimedia;
            }
            set
            {
                includeMultimedia = value;
            }
        }

        public int MaxItems
        {
            get
            {
                return maxItems;
            }
            set
            {
                maxItems = value;
            }
        }

        #endregion

        public int CompareTo(object obj)
        {
            SearchOptions sOp = null;
            if (obj is SearchOptions)
            {
                sOp = obj as SearchOptions;
            }
            else
            {
                return 1;
            }

            if (sOp != null)
            {
                if (this.truncateDataItems.CompareTo(sOp.truncateDataItems) != 0)
                {
                    return 1;
                }
                else
                {
                    if (this.includeMultimedia.CompareTo(sOp.includeMultimedia) != 0)
                    {
                        return 1;
                    }
                    else
                    {
                        if (this.maxItems.CompareTo(sOp.maxItems) != 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }

            return 1;
        }
    }
}
