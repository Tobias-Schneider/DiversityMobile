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
    /// Interaktionslogik für MapSaveOptionsWindow.xaml
    /// </summary>
    public partial class MapSaveOptionsWindow : Window
    {
        public MapSaveOptionsWindow()
        {
            InitializeComponent();

            RefreshLanguage();
            UpdateMapSavingOptionsView();
        }

        private void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1501);
            this.menuItemMenu.Header = lf.getLanguageString(1502);
            this.menuItemSaveAndClose.Header = lf.getLanguageString(1503);
            this.menuItemCancel.Header = lf.getLanguageString(1504);
            this.menuItemHelp.Header = lf.getLanguageString(1505);
            this.textPageCaption.Content = lf.getLanguageString(1521);
            this.textPageDescription.Text = lf.getLanguageString(1522);
            this.groupBoxGeneralSettings.Header = lf.getLanguageString(1523);
            this.labelUseDeviceDimensions.Content = lf.getLanguageString(1524);
            this.checkBoxUseDeviceDimensions.Content = lf.getLanguageString(1525);
            this.labelSaveToDevice.Content = lf.getLanguageString(1526);
            this.checkBoxSaveToDevice.Content = lf.getLanguageString(1527);
            this.buttonSave.Content = lf.getLanguageString(1551);
            this.buttonCancel.Content = lf.getLanguageString(1552);
        }

        /**
         * Aktualisiert die Optionen in der Ansicht
         */
        private void UpdateMapSavingOptionsView()
        {
            MapSaveOptions mso = OptionsAccess.MapSaveOptions;

            this.checkBoxUseDeviceDimensions.IsChecked = mso.UseDeviceDimensions;
            this.checkBoxSaveToDevice.IsChecked = mso.SaveToDevice;
        }

        /**
         * Speichert die Optionen ab
         */
        private void SaveMapSavingOptions()
        {
            MapSaveOptions mso = OptionsAccess.MapSaveOptions;

            if (this.checkBoxUseDeviceDimensions.IsChecked == null || this.checkBoxUseDeviceDimensions.IsChecked == false)
            {
                mso.UseDeviceDimensions = false;
            }
            else
            {
                mso.UseDeviceDimensions = true;
            }
            if (this.checkBoxSaveToDevice.IsChecked == null || this.checkBoxSaveToDevice.IsChecked == false)
            {
                mso.SaveToDevice = false;
            }
            else
            {
                mso.SaveToDevice = true;
            }
            OptionsAccess.MapSaveOptions = mso;

            DialogResult = true;
        }

        #region Button Events

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveMapSavingOptions();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Menu Events

        private void menuItemSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            SaveMapSavingOptions();
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
