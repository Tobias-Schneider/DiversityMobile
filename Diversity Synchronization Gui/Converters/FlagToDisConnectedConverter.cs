using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Diversity_Synchronization;
using Diversity_Synchronization.Options.Language;

namespace Diversity_Synchronization_Gui.Converters
{
    /// <summary>
    /// Klasse, Die Abhängig von einer ConnectionState Flag Rote odedr Grüne Farben zurückgibt
    /// (Für Statustexte)
    /// </summary>
    
    [ValueConversion(typeof(ConnectionsAccess.ConnectionState), typeof(string))]
    class FlagToDisConnectedConverter : IValueConverter
    {


        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            ILanguage lf = OptionsAccess.Language;
            //Je nach Status der Flag "Connected" (927) oder "Not Connected"(926) ausgeben.
            return ConverterHelper.CheckFlags((ConnectionsAccess.ConnectionState)value, parameter,true) ? lf.getLanguageString(927) : lf.getLanguageString(926);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion

    }
    
}
