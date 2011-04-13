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
using Diversity_Synchronization.Options;

namespace Diversity_Synchronization_Gui.Options
{
    /// <summary>
    /// Interaktionslogik für SynchronizationOptionsWindow.xaml
    /// </summary>
    public partial class SynchronizationOptionsWindow : Window
    {
        public SynchronizationOptionsWindow()
        {
            InitializeComponent();

            RefreshLanguage();
            UpdateSynchronizationOptionsView();
        }

        private void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(701);
            this.menuItemMenu.Header = lf.getLanguageString(702);
            this.menuItemSaveAndClose.Header = lf.getLanguageString(703);
            this.menuItemCancel.Header = lf.getLanguageString(704);
            this.menuItemHelp.Header = lf.getLanguageString(705);
            this.textPageCaption.Content = lf.getLanguageString(721);
            this.textPageDescription.Text = lf.getLanguageString(722);
            this.groupBoxGeneralSettings.Header = lf.getLanguageString(723);
            this.labelOnlyInsert.Content = lf.getLanguageString(724);
            this.checkBoxOnlyInsert.Content = lf.getLanguageString(725);
            this.buttonSave.Content = lf.getLanguageString(751);
            this.buttonCancel.Content = lf.getLanguageString(752);
        }

        /**
         * Aktualisiert die Optionen in der Ansicht
         */
        private void UpdateSynchronizationOptionsView()
        {
            SynchronizationOptions so = OptionsAccess.SynchronizationOptions;

            this.checkBoxOnlyInsert.IsChecked = so.OnlyInsert;
        }

        /**
         * Speichert die Optionen ab
         */
        private void SaveSynchronizationOptions()
        {
            SynchronizationOptions so = OptionsAccess.SynchronizationOptions;

            if (this.checkBoxOnlyInsert.IsChecked == null || this.checkBoxOnlyInsert.IsChecked == false)
            {
                so.OnlyInsert = false;
            }
            else
            {
                so.OnlyInsert = true;
            }
            OptionsAccess.SynchronizationOptions = so;

            DialogResult = true;
        }

        #region Button Events

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSynchronizationOptions();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Menu Events

        private void menuItemSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            SaveSynchronizationOptions();
        }

        private void menuItemCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void menuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        #endregion
    }
}
