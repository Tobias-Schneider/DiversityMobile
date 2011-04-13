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

namespace Diversity_Synchronization_Gui.Pages
{
    /// <summary>
    /// Interaktionslogik für SelectionPage.xaml
    /// </summary>
    public partial class TaxonPage : DiversityPage
    {

        private TaxonDownloader TaxonManager = new TaxonDownloader();
        

        public TaxonPage(DiversityPage previous) : base(previous)
        {  
            InitializeComponent();
            
            RefreshLanguage();

            HasNext = false;
            HasPrevious = true;

            this.DataContext = TaxonManager;
        }

        public override void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(2021);
            this.PageDescription = lf.getLanguageString(2022);
            this.PreviousTitle = lf.getLanguageString(2051);

            this.groupBoxSelection.Header = lf.getLanguageString(2001);
            this.loadTaxonData.Content = lf.getLanguageString(2002);
        }

        private void loadTaxonData_Click(object sender, RoutedEventArgs e)
        {
            ProgressInformationWindow piw = new ProgressInformationWindow();
            piw.DataContext = TaxonManager.ProgressNotification;   
            TaxonManager.copyTaxonData();
            piw.ShowDialog();            
            NavigatePrevious();
        }        

        public override void NavigateNext()
        {
            return;
        }        
    }
}
