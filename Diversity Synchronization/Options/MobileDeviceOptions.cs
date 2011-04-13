using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Diversity_Synchronization.Options
{
    public class MobileDeviceOptions : IComparable
    {
        private int screenWidth;
        private int screenHeight;

        public MobileDeviceOptions()
        {
            //Default-Werte
            this.screenWidth = 300;
            this.screenHeight = 200;
        }

        #region Properties

        public int ScreenWidth
        {
            get
            {
                return screenWidth;
            }
            set
            {
                screenWidth = value;
            }
        }

        public int ScreenHeight
        {
            get
            {
                return screenHeight;
            }
            set
            {
                screenHeight = value;
            }
        }

        #endregion

        public int CompareTo(object obj)
        {
            MobileDeviceOptions sOp = null;
            if (obj is MobileDeviceOptions)
            {
                sOp = obj as MobileDeviceOptions;
            }
            else
            {
                return 1;
            }

            if (sOp != null)
            {
                if (this.screenWidth.CompareTo(sOp.screenWidth) != 0)
                {
                    return 1;
                }
                else
                {
                    if (this.screenHeight.CompareTo(sOp.screenHeight) != 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }

            return 1;
        }
    }
}
