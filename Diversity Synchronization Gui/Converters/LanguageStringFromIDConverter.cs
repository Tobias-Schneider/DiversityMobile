using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui.Converters
{    
    [ValueConversion(typeof(int), typeof(string))]
    public class LanguageStringFromIDConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {           
            return OptionsAccess.Language.getLanguageString((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }  
    
}
