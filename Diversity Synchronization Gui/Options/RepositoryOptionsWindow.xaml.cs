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
using Diversity_Synchronization.Options.Serializer;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization.Options;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui.Options
{
    /// <summary>
    /// Interaktionslogik für RepositoryOptionsWindow.xaml
    /// </summary>
    public partial class RepositoryOptionsWindow : Window
    {
        //TODO mehrere wählbare Optionen verwalten
        public RepositoryOptionsWindow()
        {
            InitializeComponent();

            RefreshLanguage();
            UpdateRepositoryOptionsView();
        }

        private void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(501);
            this.menuItemMenu.Header = lf.getLanguageString(502);
            this.menuItemSaveAndClose.Header = lf.getLanguageString(503);
            this.menuItemCancel.Header = lf.getLanguageString(504);
            this.menuItemHelp.Header = lf.getLanguageString(505);
            this.textPageCaption.Content = lf.getLanguageString(521);
            this.textPageDescription.Text = lf.getLanguageString(522);
            this.groupBoxLogin.Header = lf.getLanguageString(523);
            this.labelAuthentification.Content = lf.getLanguageString(524);
            this.radioButtonSQL.Content = lf.getLanguageString(525);
            this.radioButtonWindows.Content = lf.getLanguageString(526);
            this.groupBoxConnection.Header = lf.getLanguageString(527);
            this.labelName.Content = lf.getLanguageString(528);
            this.labelIPAddress.Content = lf.getLanguageString(529);
            this.labelIPPort.Content = lf.getLanguageString(530);
            this.labelCatalog.Content = lf.getLanguageString(531);
            this.labelTaxonCatalog.Content = lf.getLanguageString(532);
            this.buttonSave.Content = lf.getLanguageString(551);
            this.buttonCancel.Content = lf.getLanguageString(552);
        }

        /**
         * Aktualisiert die Optionen in der Ansicht
         */
        private void UpdateRepositoryOptionsView()
        {
            RepositoryOptions ro = OptionsAccess.RepositoryOptions;
            
            ComboBoxItem cbiName = new ComboBoxItem();
            cbiName.Content = ro.ConnectionName;
            this.comboBoxConnectionName.Items.Clear();
            this.comboBoxConnectionName.Items.Add(cbiName);
            this.comboBoxConnectionName.SelectedItem = cbiName;

            if (ro.SqlAuthentification == true)
            {
                this.radioButtonSQL.IsChecked = true;
                this.radioButtonWindows.IsChecked = false;
            }
            else
            {
                this.radioButtonSQL.IsChecked = false;
                this.radioButtonWindows.IsChecked = true;
            }
            
            this.textBoxIPAddress.Text = ro.IPAddress;
            this.textBoxIPPort.Text = ro.IPPort;

            ComboBoxItem cbiCatalog = new ComboBoxItem();
            cbiCatalog.Content = ro.InitialCatalog;
            this.comboBoxCatalog.Items.Clear();
            this.comboBoxCatalog.Items.Add(cbiCatalog);
            this.comboBoxCatalog.SelectedItem = cbiCatalog;
            
            ComboBoxItem cbiTaxonCatalog = new ComboBoxItem();
            cbiTaxonCatalog.Content = ro.TaxonNamesInitialCatalog;
            this.comboBoxTaxonCatalog.Items.Clear();
            this.comboBoxTaxonCatalog.Items.Add(cbiTaxonCatalog);
            this.comboBoxTaxonCatalog.SelectedItem = cbiTaxonCatalog;
        }

        /**
         * Speichert die Optionen ab
         */
        private void SaveRepositoryOptions()
        {
            RepositoryOptions ro = OptionsAccess.RepositoryOptions;

            ro.ConnectionName = (string)((ComboBoxItem)this.comboBoxConnectionName.SelectedItem).Content;
            if (this.radioButtonSQL.IsChecked == null || this.radioButtonSQL.IsChecked == false)
            {
                ro.SqlAuthentification = false;
            }
            else
            {
                ro.SqlAuthentification = true;
            }
            ro.IPAddress = this.textBoxIPAddress.Text;
            ro.IPPort = this.textBoxIPPort.Text;
            ro.InitialCatalog = this.comboBoxCatalog.Text;
            ro.TaxonNamesInitialCatalog = this.comboBoxTaxonCatalog.Text;

            OptionsAccess.RepositoryOptions = ro;

            DialogResult = true;
        }

        #region Button Events

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveRepositoryOptions();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Menu Events

        private void menuItemSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            SaveRepositoryOptions();
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
