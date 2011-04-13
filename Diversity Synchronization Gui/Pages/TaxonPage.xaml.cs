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
