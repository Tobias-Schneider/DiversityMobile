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
using Diversity_Synchronization.Options;
using Diversity_Synchronization.Options.Serializer;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization;
using Diversity_Synchronization_Gui.Converters;

namespace Diversity_Synchronization_Gui.Pages
{
    
    /// <summary>
    /// Interaktionslogik für ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : DiversityPage
    {        
        private ConnectionsAccess.ConnectionState State
        {
            get { return ConnectionsAccess.Instance.State; }
        }

        public ConnectionPage() : base(null)
        {
            InitializeComponent();            

            HasPrevious = false;
            HasNext = false;
            

            RefreshLanguage();
            UpdateHidePassword();
            UpdateRepositoryOptionsView();
            UpdateActiveSyncOptionsView();
        }

        
        public override void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(921);
            this.PageDescription = lf.getLanguageString(922);
            this.NextTitle = lf.getLanguageString(952);

            this.groupBoxRepositoryConnection.Header = lf.getLanguageString(923);
            this.labelConnectionName.Content = lf.getLanguageString(924);
            this.labelInitialCatalog.Content = lf.getLanguageString(925);          
       
            this.labelInitialTaxonCatalog.Content = lf.getLanguageString(929);
            this.labelUsername.Content = lf.getLanguageString(930);
            this.labelPassword.Content = lf.getLanguageString(931);

            

            //Falls irgendeine Repository-Verbindung besteht Button neu beschriften
            if (State.CheckForFlags(ConnectionsAccess.ConnectionState.RepositoryConnected,false))
            {
                this.buttonConnectToRepository.Content = lf.getLanguageString(933);
            }
            else
            {
                this.buttonConnectToRepository.Content = lf.getLanguageString(932);
            }
            
            this.groupBoxMobileDBConnection.Header = lf.getLanguageString(934);
            this.labelConnectToMobileDevice.Content = lf.getLanguageString(935);
            this.labelTimeOut.Content = lf.getLanguageString(936);
            this.labelDiversityMobileDB.Content = lf.getLanguageString(937);
            this.labelTaxonNamesDB.Content = lf.getLanguageString(938);

            //Falls irgendeine Mobile-Verbindung besteht Button neu beschriften
            if (State.CheckForFlags(ConnectionsAccess.ConnectionState.MobileConnected,false))
            {
                this.buttonConnectActiveSync.Content = lf.getLanguageString(933);
            }
            else
            {
                this.buttonConnectActiveSync.Content = lf.getLanguageString(932);
            }
        }

        public override void NavigateNext()
        {
            NavigationService.Navigate(new ProjectPage(this));
        }       

        /**
         * Aktualisiert die Anzeige des richtigen Passwort-Feldes.
         * Bei Nutzung muss allerdings erneut abgefragt werden, welches gerade aktiv ist!
         */
        public void UpdateHidePassword()
        {
            ProgramOptions po = OptionsAccess.ProgramOptions;

            if (po.HidePassword)
            {
                this.textBoxPassword.Visibility = Visibility.Visible;
                this.textBoxPasswordVisible.Visibility = Visibility.Hidden;
            }
            else
            {
                this.textBoxPassword.Visibility = Visibility.Hidden;
                this.textBoxPasswordVisible.Visibility = Visibility.Visible;
            }
        }

        /**
         * Aktualisiert die angezeigten Repository-Einstellungen
         */
        public void UpdateRepositoryOptionsView()
        {
            RepositoryOptions ro = OptionsAccess.RepositoryOptions;

            this.labelConnectionName2.Content = ro.ConnectionName;
            this.labelInitialCatalog2.Content = ro.InitialCatalog;           
            this.labelInitialTaxonCatalog2.Content = ro.TaxonNamesInitialCatalog;
            this.textBoxUsername.Text = ro.LastUsername;
        }

        /**
         * Aktualisiert die angezeigten Active-Sync-Einstellungen
         */
        public void UpdateActiveSyncOptionsView()
        {
            ILanguage lf = OptionsAccess.Language;
            ActiveSyncOptions aso = OptionsAccess.ActiveSyncOptions;

            if (aso.UseDevice == true)
            {
                this.labelConnectToMobileDevice2.Content = lf.getLanguageString(939);
            }
            else
            {
                this.labelConnectToMobileDevice2.Content = lf.getLanguageString(940);
            }
            this.labelTimeOut2.Content = aso.ConnectingTimeOut;
            this.labelDiversityMobileDB2.Content = aso.DiversityMobileDBPath;
            this.labelTaxonNamesDB2.Content = aso.TaxonNamesDBPath;
        }

        #region Button Events

        /**
         * Baut Verbindungen auf und aktualisiert die Anzeige
         */
        private void buttonConnectToRepository_Click(object sender, RoutedEventArgs e)
        {
            ILanguage lf = OptionsAccess.Language;
            RepositoryOptions ro = OptionsAccess.RepositoryOptions;            
            this.buttonConnectToRepository.IsEnabled = false;
            //Wartecursor anzeigen
            Cursor defaultCursor = this.Cursor;
            this.Cursor = Cursors.Wait;

            if (State.CheckForFlags(ConnectionsAccess.ConnectionState.RepositoryConnected,false))
            {
                ConnectionsAccess.Instance.disconnectFromRepository();
            }
            else
            {
                //Passwort abhängig von den Sichtbarkeitseinstellungen abrufen
                string password = (textBoxPassword.Visibility == Visibility.Visible) ? textBoxPassword.Password : textBoxPasswordVisible.Text;   

                ConnectionsAccess.Instance.connectToRepository(OptionsAccess.RepositoryOptions, textBoxUsername.Text, password);
                if (!State.hasFlags(ConnectionsAccess.ConnectionState.RepositoryConnected))
                {
                    (new MessageBoxWindow(960, 960, 961, 962)).ShowDialog();
                }
                
            }
            this.Cursor = defaultCursor;

            if (State.CheckForFlags(ConnectionsAccess.ConnectionState.RepositoryConnected,false))
            {                
                this.buttonConnectToRepository.Content = lf.getLanguageString(933);
            }
            else
            {                
                this.buttonConnectToRepository.Content = lf.getLanguageString(932);
            }

            this.buttonConnectToRepository.IsEnabled = true;            

            HasNext = State.hasFlags(ConnectionsAccess.ConnectionState.FullyConnected);

            
        }

        /**
         * Baut Verbindungen auf und aktualisiert die Anzeige
         */
        private void buttonConnectActiveSync_Click(object sender, RoutedEventArgs e)
        {
            ILanguage lf = OptionsAccess.Language;

            this.buttonConnectActiveSync.IsEnabled = false;
            
            if (State.CheckForFlags(ConnectionsAccess.ConnectionState.MobileConnected,false))
            {
                ConnectionsAccess.Instance.disconnectFromMobileDB();
            }
            else
            {
                ConnectionsAccess.Instance.connectToMobileDB(OptionsAccess.ActiveSyncOptions);
                if (!State.hasFlags(ConnectionsAccess.ConnectionState.MobileConnected))
                    (new MessageBoxWindow(963, 963, 964, 965)).ShowDialog();
            }

            this.buttonConnectActiveSync.IsEnabled = true;

            if (State.CheckForFlags(ConnectionsAccess.ConnectionState.MobileConnected,false))
            {
                this.buttonConnectActiveSync.Content = lf.getLanguageString(933);                
            }
            else
            {
                this.buttonConnectActiveSync.Content = lf.getLanguageString(932);
            }
            

            HasNext = State.hasFlags(ConnectionsAccess.ConnectionState.FullyConnected);
        }

        #endregion

        private void DiversityPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
