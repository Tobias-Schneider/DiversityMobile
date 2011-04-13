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
    public partial class SelectionPage : DiversityPage
    {
        private FieldDataSynchronizer FieldDataManager
        {
            get
            {
                return FindResource("FieldDataManager") as FieldDataSynchronizer;
            }
        }

        public SelectionPage(DiversityPage previous) : base(previous)
        {
            InitializeComponent();

            HasPrevious = true;
            HasNext = true;

            var selectionBinding = new Binding("FinishedSelection")
            {
                Source = FieldDataManager.SelectionBuilder,
                Mode = BindingMode.OneWay,
                NotifyOnTargetUpdated = true,
            };
            listBoxSelection.TargetUpdated += new EventHandler<DataTransferEventArgs>(updateTreeview);            
            this.listBoxSelection.SetBinding(ListBox.ItemsSourceProperty, selectionBinding);
            
            
            RefreshLanguage();
        }

        void updateTreeview(object sender, DataTransferEventArgs e)
        {
            try
            {
                var tvOperations = FieldDataManager.SelectionBuilder.TreeViewBuilder;
                var roots = FieldDataManager.SelectionBuilder.SelectionRoots;
                var selection = FieldDataManager.SelectionBuilder.FinishedSelection;
                tvFinalSelection.SuspendLayout();
                tvFinalSelection.Nodes.Clear();
                foreach (var root in roots)
                {
                    tvFinalSelection.Nodes.Add(tvOperations.displayListTopDown(selection, root));
                }
                tvFinalSelection.ResumeLayout();
                tvFinalSelection.Update();
            }
            catch (Exception)
            {
                //TODO Logging
            }
        }

        public override void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1321);
            this.PageDescription = lf.getLanguageString(1322);
            this.PreviousTitle = lf.getLanguageString(1351);
            this.NextTitle = lf.getLanguageString(1352);

            this.groupBoxSelection.Header = lf.getLanguageString(1323);
        }
       

        public override void NavigateNext()
        {
            ProgressInformationWindow piw = new ProgressInformationWindow();
            piw.DataContext = FieldDataManager.Progress;

            FieldDataManager.downloadFieldData();

            piw.ShowDialog();
            NavigationService.Navigate(DiversityPage.Actions);
        }

        private System.Windows.Forms.TreeView tvFinalSelection = new System.Windows.Forms.TreeView();
        private void DiversityPage_Loaded(object sender, RoutedEventArgs e)
        {
            tvFinalSelection.ImageList = OptionsAccess.TreeviewIcons;

            ProgressInformationWindow piw = new ProgressInformationWindow();
            piw.DataContext = FieldDataManager.SelectionBuilder.Progress;
            FieldDataManager.SelectionBuilder.finishSelection();
            piw.ShowDialog();


            var host = new System.Windows.Forms.Integration.WindowsFormsHost();
            host.Child = tvFinalSelection;
            containerTreeviewFinalSelection.Children.Clear();
            containerTreeviewFinalSelection.Children.Add(host);                       
        }        
    }
}
