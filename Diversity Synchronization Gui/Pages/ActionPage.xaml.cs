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
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization;
using System.Windows.Threading;

namespace Diversity_Synchronization_Gui.Pages
{
    /// <summary>
    /// Interaktionslogik für ActionPage.xaml
    /// </summary>
    public partial class ActionPage : DiversityPage
    {
        
        
        private DefinitionLoader defLoader = new DefinitionLoader();
        private FieldDataSynchronizer FieldDataManager = new FieldDataSynchronizer();   
        private PropertyUpdater propertyLoader = new PropertyUpdater();

        public ActionPage(DiversityPage previous) : base(previous)
        {
            HasPrevious = true;
            HasNext = false;
            InitializeComponent();

            RefreshLanguage();

            DiversityPage.Actions = this;
        }

        public override void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1121);
            this.PageDescription = lf.getLanguageString(1122);
            this.PreviousTitle = lf.getLanguageString(1151);
            this.NextTitle = lf.getLanguageString(1152);

            this.groupBoxGeneralActions.Header = lf.getLanguageString(1123);
            this.buttonDownloadProperties.Content = lf.getLanguageString(1124);
            //this.labelGetDivCollectionDefinitionsState.Content = lf.getLanguageString(1129);
            this.buttonGetTaxonDefinitions.Content = lf.getLanguageString(1125);
            this.labelGetTaxonDefinitionsState.Content = lf.getLanguageString(1129);
            this.buttonDownloadPrimaryData.Content = lf.getLanguageString(1126);
            this.labelDownloadPrimaryDataState.Content = lf.getLanguageString(1129);
            this.buttonUploadData.Content = lf.getLanguageString(1127);
            this.labelUploadDataState.Content = lf.getLanguageString(1129);
            this.buttonCleanData.Content = lf.getLanguageString(1128);
            this.labelCleanDataState.Content = lf.getLanguageString(1129);
            this.groupBoxMaps.Header = lf.getLanguageString(1130);
            this.buttonGoogleMaps.Content = lf.getLanguageString(1131);
            this.labelGoogleMapsState.Content = lf.getLanguageString(1129);
            this.buttonSNSBRequest.Content = lf.getLanguageString(1132);
            this.labelSNSBRequestState.Content = lf.getLanguageString(1129);
            this.buttonSNSBDownload.Content = lf.getLanguageString(1133);
            this.labelSNSBDownloadState.Content = lf.getLanguageString(1129);
        }

        #region Button Events        

        private void buttonGetTaxonDefinitions_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TaxonPage(this));     
        }

        private void buttonDownloadPrimaryData_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SelectFieldDataPage(this));

            //this.labelDownloadPrimaryDataState.Visibility = Visibility.Visible;
        }

        private void buttonUploadData_Click(object sender, RoutedEventArgs e)
        {
            ProgressInformationWindow progressWindow = new ProgressInformationWindow();
            progressWindow.DataContext = FieldDataManager.Progress;
            FieldDataManager.uploadFieldData();
            progressWindow.ShowDialog();
            labelUploadDataState.Visibility = Visibility.Visible;
        }

        private void buttonCleanData_Click(object sender, RoutedEventArgs e)
        {
            if (new CleanConfirmationWindow().ShowDialog() == true)
            {
                ConnectionsAccess.Instance.cleanDB();
                NavigationService.Navigate(Connections);
            }           
        }

        private void buttonGoogleMaps_Click(object sender, RoutedEventArgs e)
        {
            //TODO

            MapsWindow gmw = new MapsWindow();
            gmw.ShowDialog();
        }

        private void buttonSNSBRequest_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void buttonSNSBDownload_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        #endregion
            

        private void DiversityPage_Loaded(object sender, RoutedEventArgs e)
        {
            if ((SyncStatus.Instance.Sync & SyncStatus.SyncState.CollectionDefinitions) == SyncStatus.SyncState.None)
            {
                ProgressInformationWindow progressWindow = new ProgressInformationWindow();
                progressWindow.DataContext = defLoader.Progress;

                defLoader.loadCollectionDefinitions();

                progressWindow.ShowDialog();

                SyncStatus.Instance.Sync |= SyncStatus.SyncState.CollectionDefinitions;
            }
        }


        public override void NavigateNext()
        {
            return;
        }

        private void buttonDownloadProperties_Click(object sender, RoutedEventArgs e)
        {
            if ((SyncStatus.Instance.Sync & SyncStatus.SyncState.PropertyDownload) == SyncStatus.SyncState.None)
            {
                ProgressInformationWindow progressWindow = new ProgressInformationWindow();
                progressWindow.DataContext = propertyLoader.Progress;

                propertyLoader.updateProperties();

                progressWindow.ShowDialog();

                SyncStatus.Instance.Sync |= SyncStatus.SyncState.CollectionDefinitions;
            }
        }
    }
}
