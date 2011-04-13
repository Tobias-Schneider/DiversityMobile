using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Toolbox.Controls
{
    public class UserControlCorruptedException: Exception
    {
        public UserControlCorruptedException(String msg)
            : base(msg)
        {
        }
    }
}
