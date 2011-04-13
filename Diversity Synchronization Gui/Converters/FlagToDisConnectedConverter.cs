//#######################################################################
//Diversity Mobile Synchronization
//Project Homepage:  http://www.diversitymobile.net
//Copyright (C) 2011  Tobias Schneider, Lehrstuhl Angewndte Informatik IV, Universität Bayreuth
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//#######################################################################
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
