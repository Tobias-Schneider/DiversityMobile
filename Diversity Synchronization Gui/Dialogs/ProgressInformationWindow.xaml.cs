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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization;
using System.Windows.Threading;

namespace Diversity_Synchronization_Gui
{
    /// <summary>
    /// Interaktionslogik für ProgressInformationWindow.xaml
    /// </summary>
    public partial class ProgressInformationWindow : Window, ILanguageRefreshable
    {
        private ProgressReporter ProgressNotifier
        {
            get
            {
                return this.DataContext as ProgressReporter;
            }
        }

        private DispatcherTimer closeTimer;

        public ProgressInformationWindow()
        {
            InitializeComponent();

            RefreshLanguage();


            closeTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1)};
            closeTimer.Tick += new EventHandler(closeTimer_Tick);

        }

        void closeTimer_Tick(object sender, EventArgs e)
        {
            if (ProgressNotifier != null && ProgressNotifier.IsProgressFinished)
                this.Close();
        }

        
        
        public void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1801);            
            this.buttonCancel.Content = lf.getLanguageString(1851);
        }
       
        #region Button Events

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (ProgressNotifier != null)
            {
                ProgressNotifier.IsCancelRequested = true;
            }
        }

        #endregion

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            closeTimer.IsEnabled = ProgressNotifier != null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ProgressNotifier != null && !ProgressNotifier.IsProgressFinished)
            {
                ProgressNotifier.IsCancelRequested = true;
                e.Cancel = true;
            }
        }

        
    }
}
