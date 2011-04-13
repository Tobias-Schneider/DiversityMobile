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
using System.Windows.Shapes;
using System.Windows.Threading;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization.Options;
using Diversity_Synchronization.Options.Serializer;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui
{
    /// <summary>
    /// Interaktionslogik für StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window, ILanguageRefreshable
    {    
        private DispatcherTimer _countdown = null;
        private bool _closing = false;

        public StartWindow()
        {

            InitializeComponent();

            RefreshLanguage();
        }

        public void RefreshLanguage()
        {
            try
            {
                ILanguage lf = OptionsAccess.Language;
                this.labelWelcome1.Content = lf.getLanguageString(101);
                this.labelWelcome2.Content = lf.getLanguageString(102);
            }
            catch
            {
                MessageBox.Show("Unable to load language file, exiting!");
                _closing = true;                
            }
        }

        /// <summary>
        /// Startet Timer für die Dauer der Anzeige des Startbildschirms
        /// </summary>       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this._closing)
            {
                _countdown = new DispatcherTimer();
                _countdown.Interval = TimeSpan.FromMilliseconds(3000);
                _countdown.Tick += new EventHandler(Countdown_Tick);
                _countdown.Start();
            }
            else
                this.Close();
        }

        /**
         * Beendet Countdown und ruft Hauptfenster auf.
         */
        void Countdown_Tick(object sender, EventArgs e)
        {
            _countdown.Stop();
            new FrameWindow().Show();
            this.Close();
        }
    }
}
