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
    /// Interaktionslogik für MobileDeviceOptionsWindow.xaml
    /// </summary>
    public partial class MobileDeviceOptionsWindow : Window, ILanguageRefreshable
    {
        public MobileDeviceOptionsWindow()
        {
            InitializeComponent();

            RefreshLanguage();
            UpdateMobileDeviceOptionsView();
        }

        public void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1601);
            this.menuItemMenu.Header = lf.getLanguageString(1602);
            this.menuItemSaveAndClose.Header = lf.getLanguageString(1603);
            this.menuItemCancel.Header = lf.getLanguageString(1604);
            this.menuItemHelp.Header = lf.getLanguageString(1605);
            this.textPageCaption.Content = lf.getLanguageString(1621);
            this.textPageDescription.Text = lf.getLanguageString(1622);
            this.groupBoxGeneralSettings.Header = lf.getLanguageString(1623);
            this.labelScreenWidth.Content = lf.getLanguageString(1624);
            this.labelScreenHeight.Content = lf.getLanguageString(1625);
            this.buttonSave.Content = lf.getLanguageString(1651);
            this.buttonCancel.Content = lf.getLanguageString(1652);
        }

        /**
         * Aktualisiert die Optionen in der Ansicht
         */
        private void UpdateMobileDeviceOptionsView()
        {
            MobileDeviceOptions mdo = OptionsAccess.MobileDeviceOptions;

            this.textBoxScreenWidth.Text = Convert.ToString(mdo.ScreenWidth);
            this.textBoxScreenHeight.Text = Convert.ToString(mdo.ScreenHeight);
        }

        /**
         * Speichert die Optionen ab
         */
        private void SaveMobileDeviceOptions()
        {
            MobileDeviceOptions mdo = OptionsAccess.MobileDeviceOptions;

            mdo.ScreenWidth = Convert.ToInt32(this.textBoxScreenWidth.Text);
            mdo.ScreenHeight = Convert.ToInt32(this.textBoxScreenHeight.Text);

            OptionsAccess.MobileDeviceOptions = mdo;

            DialogResult = true;
        }

        #region Menu Events

        private void menuItemCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void menuItemSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            SaveMobileDeviceOptions();
        }

        private void menuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        #endregion

        #region Button Events

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveMobileDeviceOptions();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion
    }
}
