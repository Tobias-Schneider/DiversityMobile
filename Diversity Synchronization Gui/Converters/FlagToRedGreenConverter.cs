using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Diversity_Synchronization;
using System.Windows.Media;

namespace Diversity_Synchronization_Gui.Converters
{
    [ValueConversion(typeof(ConnectionsAccess.ConnectionState), typeof(SolidColorBrush))]
    class FlagToRedGreenConverter : IValueConverter
    {
        
        
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {            
            return ConverterHelper.CheckFlags((ConnectionsAccess.ConnectionState)value, parameter,true)?Brushes.Green:Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
        
    }
}
