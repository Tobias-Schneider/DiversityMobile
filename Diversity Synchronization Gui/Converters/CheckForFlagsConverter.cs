﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui.Converters
{    
    [ValueConversion(typeof(ConnectionsAccess.ConnectionState), typeof(bool))]
    public class CheckForFlagsConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a bool");
            return ConverterHelper.CheckFlags((ConnectionsAccess.ConnectionState) value, parameter,true);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }  
    
}
