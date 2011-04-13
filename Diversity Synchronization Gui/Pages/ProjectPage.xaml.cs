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
    /// Interaktionslogik für ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : DiversityPage
    {
        private ProjectSelector projectSelector;

        public ProjectPage(DiversityPage previous) : base(previous)
        {
            InitializeComponent();

            HasNext = false;
            HasPrevious = true;

            RefreshLanguage();

            projectSelector = new ProjectSelector();
            this.DataContext = projectSelector;
        }

        public override void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1021);
            this.PageDescription = lf.getLanguageString(1022);
            this.PreviousTitle = lf.getLanguageString(1051);
            this.NextTitle = lf.getLanguageString(1052);

            this.groupBoxProjectSelection.Header = lf.getLanguageString(1023);
            this.labelRepository.Text = lf.getLanguageString(1024);
            this.labelSelection.Content = lf.getLanguageString(1025);
            this.labelProjectName.Content = lf.getLanguageString(1026);
            this.labelUserName.Content = lf.getLanguageString(1027);
        }

        private void listBoxProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO
            HasNext = (listBoxProjects.SelectedItem != null);
        }



        public override void NavigateNext()
        {
        
            ConnectionsAccess.Profile.ProjectID = projectSelector.SelectedProject.ID;
            ConnectionsAccess.Profile.ProjectName = projectSelector.SelectedProject.Title;
            if (OptionsAccess.RepositoryOptions.InitialCatalog == ConnectionsAccess.Profile.HomeDB)
            {
                
                ConnectionsAccess.Instance.saveProfile();

                NavigationService.Navigate(new ActionPage(this));

            }
            else
            {
                bool executeClean = (new YesNoDecisionWindow(1060, 1061, 1062, 1063, 1064)).ShowDialog() == true;
                if (executeClean)
                {
                    ConnectionsAccess.Instance.cleanDB();
                    NavigatePrevious();
                }
            }
        }       
    }
}
