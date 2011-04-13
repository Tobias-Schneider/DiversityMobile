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
using Diversity_Synchronization.Options;
using Diversity_Synchronization;
using Diversity_Synchronization.Options.Language;
using Microsoft.Win32;

namespace Diversity_Synchronization_Gui.Options
{
    /// <summary>
    /// Interaktionslogik für ActiveSyncOptionsWindow.xaml
    /// </summary>
    public partial class ActiveSyncOptionsWindow : Window
    {
        OpenFileDialog ofd;

        public ActiveSyncOptionsWindow()
        {
            InitializeComponent();

            ofd = new OpenFileDialog()
            {
                CheckFileExists = true
            };

            RefreshLanguage();
            UpdateActiveSyncOptionsView();
        }

        private void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            

            this.Title = lf.getLanguageString(601);
            this.menuItemMenu.Header = lf.getLanguageString(602);
            this.menuItemSaveAndClose.Header = lf.getLanguageString(603);
            this.menuItemCancel.Header = lf.getLanguageString(604);
            this.menuItemHelp.Header = lf.getLanguageString(605);
            this.textPageCaption.Content = lf.getLanguageString(621);
            this.textPageDescription.Text = lf.getLanguageString(622);
            this.groupBoxConnection.Header = lf.getLanguageString(623);
            this.labelConnectToMobile.Content = lf.getLanguageString(624);
            this.checkBoxConnectToMobile.Content = lf.getLanguageString(625);
            this.labelTimeout.Content = lf.getLanguageString(626);
            this.labelDiversityMobileDB.Content = lf.getLanguageString(627);
            this.buttonChooseDiversityMobileDB.Content = lf.getLanguageString(628);
            this.labelTaxonNamesDB.Content = lf.getLanguageString(629);
            this.buttonChooseTaxonNamesDB.Content = lf.getLanguageString(630);
            this.buttonSave.Content = lf.getLanguageString(651);
            this.buttonCancel.Content = lf.getLanguageString(652);
        }

        /**
         * Aktualisiert die Optionen in der Ansicht
         */
        private void UpdateActiveSyncOptionsView()
        {
            ActiveSyncOptions aso = OptionsAccess.ActiveSyncOptions;

            this.checkBoxConnectToMobile.IsChecked = aso.UseDevice;
            this.textBoxTimeout.Text = Convert.ToString(aso.ConnectingTimeOut);
            this.textBoxDiversityMobileDB.Text = aso.DiversityMobileDBPath;
            this.textBoxTaxonNamesDB.Text = aso.TaxonNamesDBPath;
        }

        /**
         * Speichert die Optionen ab
         */
        private void SaveActiveSyncOptions()
        {
            ActiveSyncOptions aso = OptionsAccess.ActiveSyncOptions;

            if (this.checkBoxConnectToMobile.IsChecked == null || this.checkBoxConnectToMobile.IsChecked == false)
            {
                aso.UseDevice = false;
            }
            else
            {
                aso.UseDevice = true;
            }

            aso.ConnectingTimeOut = Convert.ToInt32(this.textBoxTimeout.Text);
            aso.DiversityMobileDBPath = this.textBoxDiversityMobileDB.Text;
            aso.TaxonNamesDBPath = this.textBoxTaxonNamesDB.Text;

            OptionsAccess.ActiveSyncOptions = aso;

            DialogResult = true;
        }

        #region Button Events

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveActiveSyncOptions();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ButtonChooseDiversityMobileDB_Click(object sender, RoutedEventArgs e)
        {           
            ofd.Filter = OptionsAccess.Language.getLanguageString(661);
            if (ofd.ShowDialog() == true)
            {
                this.textBoxDiversityMobileDB.Text = ofd.FileName;
            }
        }

        private void ButtonChooseTaxonNamesDB_Click(object sender, RoutedEventArgs e)
        {
            ofd.Filter = OptionsAccess.Language.getLanguageString(662);
            if (ofd.ShowDialog() == true)
            {
                this.textBoxTaxonNamesDB.Text = ofd.FileName;
            }
        }

        #endregion

        #region Menu Events

        private void menuItemSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            SaveActiveSyncOptions();
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
