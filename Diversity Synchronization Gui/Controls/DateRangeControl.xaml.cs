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
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diversity_Synchronization_Gui
{
	/// <summary>
	/// Interaction logic for DateRangeControl.xaml
	/// </summary>
	public partial class DateRangeControl : UserControl
	{
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(DateRangeControl));
        public static readonly DependencyProperty FromStringProperty = DependencyProperty.Register("FromString", typeof(string), typeof(DateRangeControl));
        public static readonly DependencyProperty UntilStringProperty = DependencyProperty.Register("UntilString", typeof(string), typeof(DateRangeControl));
        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register("StartDate", typeof(DateTime), typeof(DateRangeControl),new PropertyMetadata(DateTime.Today));
        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register("EndDate", typeof(DateTime), typeof(DateRangeControl),new PropertyMetadata(DateTime.Today));
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(DateRangeControl));


        public string Title
        {
            get
            {
                return GetValue(TitleProperty) as string;
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }
        public string From
        {
            get
            {
                return GetValue(FromStringProperty) as string;
            }
            set
            {
                SetValue(FromStringProperty, value);
            }
        }
        public string Until
        {
            get
            {
                return GetValue(UntilStringProperty) as string;
            }
            set
            {
                SetValue(UntilStringProperty, value);
            }
        }
        public DateTime StartDate
        {
            get
            {
                return (DateTime)GetValue(StartDateProperty);
            }
            set
            {
                SetValue(StartDateProperty, value);
            }
        }
        public DateTime EndDate
        {
            get
            {
                return (DateTime)GetValue(EndDateProperty);
            }
            set
            {
                SetValue(EndDateProperty, value);
            }
        }
        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }
            set
            {
                SetValue(IsCheckedProperty, value);
            }
        }


		public DateRangeControl()
		{
			this.InitializeComponent();
		}
	}
}