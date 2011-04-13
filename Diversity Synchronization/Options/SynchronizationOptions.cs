using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Diversity_Synchronization.Options
{
    public class SynchronizationOptions : IComparable
    {
        private bool onlyInsert;

        public SynchronizationOptions()
        {
            //Default-Werte
            this.onlyInsert = false;
        }

        #region Properties

        public bool OnlyInsert
        {
            get
            {
                return onlyInsert;
            }
            set
            {
                onlyInsert = value;
            }
        }

        #endregion

        public int CompareTo(object obj)
        {
            SynchronizationOptions syncOp = (obj as SynchronizationOptions);
            if (syncOp != null)
            {
                if (this.onlyInsert.CompareTo(syncOp.onlyInsert) != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            return 1;
        }
    }
}
