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
using System.Collections.ObjectModel;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DataItemFormTools;
using System.Windows.Forms.Integration;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;


namespace Diversity_Synchronization_Gui.Pages
{
    /// <summary>
    /// Interaktionslogik für SelectFieldDataPage.xaml
    /// </summary>
    public partial class SelectFieldDataPage : DiversityPage
    {
        private System.Windows.Forms.TreeView resultTV;
        private System.Windows.Forms.TreeView selectionTV;
        private bool updateTreeview = true;



        private FieldDataSynchronizer FieldDataManager
        {
            get
            {
                return FindResource("FieldDataManager") as FieldDataSynchronizer;
            }
        }
            

        private IValueConverter LanguageStringFromIDConverter
        {
            get
            {
                return FindResource("LanguageStringFromIDConverter") as IValueConverter;
            }
        }

        //Collection Agent
        //private TextBox textBoxCollectorsName = new TextBox();

        public SelectFieldDataPage(DiversityPage previous) : base(previous)
        {
            InitializeComponent();           

            RefreshLanguage();

            HasNext = true;
            HasPrevious = true;

            /*var maxHeight = new Binding("ActualHeight")
            {
                Source = this.groupBoxResult,
                Mode = BindingMode.OneWay,
            };
            this.listBoxResult.SetBinding(MaxHeightProperty,maxHeight);*/

            var queryResult = new Binding("QueryResult")
            {
                Mode = BindingMode.OneWay,
                Source = FieldDataManager.SelectionBuilder,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            listBoxResult.SetBinding(ListBox.ItemsSourceProperty, queryResult);

            var actualSelection = new Binding("SelectedElements")
            {
                Mode = BindingMode.OneWay,
                Source = FieldDataManager.SelectionBuilder,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            listBoxActualSelection.SetBinding(ListBox.ItemsSourceProperty, actualSelection);
            
            

            listBoxSearchType.ItemsSource = FieldDataManager.SelectionBuilder.SearchTypes;
            
        }

        public override void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1221);
            this.PageDescription = lf.getLanguageString(1222);
            this.PreviousTitle = lf.getLanguageString(1251);
            this.NextTitle = lf.getLanguageString(1252);

            this.groupBoxSearch.Header = lf.getLanguageString(1223);
            this.textBlockSelectSearchType.Text = lf.getLanguageString(1224);
            this.labelSearchFields.Content = lf.getLanguageString(1225);
            this.buttonQueryDatabase.Content = lf.getLanguageString(1226);
            this.groupBoxResult.Header = lf.getLanguageString(1231);
            this.buttonResultSelectAll.Content = lf.getLanguageString(1227);
            this.buttonAddToSelection.Content = lf.getLanguageString(1228);
            this.buttonRemoveFromSelection.Content = lf.getLanguageString(1229);
            this.groupBoxSelection.Header = lf.getLanguageString(1230);
            this.buttonActualSelectionSelectAll.Content = lf.getLanguageString(1227);
            
        }

        #region Button Events

        private void buttonQueryDatabase_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxSearchType.SelectedItem != null && listBoxSearchType.SelectedItem is FieldDataSelectionBuilder.SearchSpecification)
            {
                ProgressInformationWindow piw = new ProgressInformationWindow();
                piw.DataContext = FieldDataManager.SelectionBuilder.Progress;
                
                FieldDataManager.SelectionBuilder.QueryDatabase(listBoxSearchType.SelectedItem as FieldDataSelectionBuilder.SearchSpecification);
                piw.ShowDialog();
            }
            
        }

        private void buttonResultSelectAll_Click(object sender, RoutedEventArgs e)
        {
            this.listBoxResult.SelectAll();
        }

        private void buttonActualSelectionSelectAll_Click(object sender, RoutedEventArgs e)
        {
            this.listBoxActualSelection.SelectAll();
        }

        private void buttonAddToSelection_Click(object sender, RoutedEventArgs e)
        {
            stopTreeviewUpdates();

            displayWaitCursor();

            bool truncate = checkBoxTruncate.IsChecked ?? false; 

            this.buttonAddToSelection.IsEnabled = false;
            this.buttonRemoveFromSelection.IsEnabled = false;

            var items = new List<ISerializableObject>();
            foreach (var iso in listBoxResult.SelectedItems)
            {
                if (iso is ISerializableObject)
                {
                    items.Add(iso as ISerializableObject);
                }
            }

            var containers = new List<FieldDataSelectionBuilder.SelectionContainer>();
            Action buildSelectionContainers = delegate()
            {                
                
                foreach (var item in items)
                {
                    if (item is ISerializableObject)
                    {
                        containers.Add(new FieldDataSelectionBuilder.SelectionContainer(item as ISerializableObject, truncate));
                    }
                }
            };

            Action updateCollections = delegate()
            {
                foreach (var item in containers)
                {
                    FieldDataManager.SelectionBuilder.QueryResult.Remove(item.Owner);
                    FieldDataManager.SelectionBuilder.SelectedElements.Add(item);
                }

                this.buttonAddToSelection.IsEnabled = true;
                this.buttonRemoveFromSelection.IsEnabled = true;

                hideWaitCursor();

                startTreeviewUpdates();
            };

            buildSelectionContainers.BeginInvoke(
                delegate(IAsyncResult ar)
                {
                    this.Dispatcher.Invoke(updateCollections);
                }, null);           
        }

        
        Cursor defaultCursor = Cursors.Arrow;
        private void displayWaitCursor()
        {
            defaultCursor = this.Cursor;
            this.Cursor = Cursors.Wait;            
        }

        private void hideWaitCursor()
        {
            this.Cursor = defaultCursor;
        }


        private void buttonRemoveFromSelection_Click(object sender, RoutedEventArgs e)
        {
            stopTreeviewUpdates();

            displayWaitCursor();

            var selectedItems = new List<FieldDataSelectionBuilder.SelectionContainer>(listBoxActualSelection.SelectedItems.Count);
            foreach (var item in this.listBoxActualSelection.SelectedItems)
            {
                if (item is FieldDataSelectionBuilder.SelectionContainer)
                {
                    FieldDataManager.SelectionBuilder.QueryResult.Add((item as FieldDataSelectionBuilder.SelectionContainer).Owner);
                    selectedItems.Add(item as FieldDataSelectionBuilder.SelectionContainer);
                }
            }
            foreach (var item in selectedItems)
            {
                if (item is FieldDataSelectionBuilder.SelectionContainer)
                {
                    FieldDataManager.SelectionBuilder.SelectedElements.Remove(item);
                }
            }

            hideWaitCursor();

            startTreeviewUpdates();
        }

        private void startTreeviewUpdates()
        {
            updateTreeview = true;
            listBoxResult_SelectionChanged(null, null);
            listBoxActualSelection_SelectionChanged(null, null);
        }

        

        private void stopTreeviewUpdates()
        {
            updateTreeview = false;
            resultTV.Nodes.Clear();
            selectionTV.Nodes.Clear();
        }

        #endregion

        #region Suchparameter

        private void listBoxSearchType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  

            this.stackPanelSearchFields.Children.Clear();

            if (e != null && e.AddedItems.Count > 0)
            {
                var selectedSearchType = e.AddedItems[0] as FieldDataSelectionBuilder.SearchSpecification;
                if (selectedSearchType != null)
                {
                    foreach (var restrictionDefinition in selectedSearchType.Restrictions)
                    {
                        UserControl restrictionControl;
                        if (restrictionDefinition.Type == FieldDataSelectionBuilder.SearchSpecification.RestrictionSpecification.RestrictionType.BetweenDates)
                        {
                            restrictionControl = constructDateRangeControl(restrictionDefinition);
                        }
                        else
                        {
                            restrictionControl = constructNameValueControl(restrictionDefinition);                            
                        }
                        stackPanelSearchFields.Children.Add(restrictionControl);
                    }
                }
            }

            warnTruncatedSeries();
            
        }

        private void warnTruncatedSeries()
        {
            var searchtype = listBoxSearchType.SelectedItem as Diversity_Synchronization.FieldDataSelectionBuilder.SearchSpecification;
            var truncate = checkBoxTruncate.IsChecked ?? false;
            if (searchtype != null && searchtype.ObjectType.Equals(typeof(CollectionEventSeries)) && truncate)
                new MessageBoxWindow(1233, 1234, 1235, 1236).ShowDialog();
        }

        private UserControl constructDateRangeControl(FieldDataSelectionBuilder.SearchSpecification.RestrictionSpecification restriction)
        {
            UserControl restrictionControl;
            var drControl = new DateRangeControl();

            var titleBinding = new Binding(PropertyHelper.ExtractPropertyName(() => restriction.TitleID))
            {
                Source = restriction, 
                Converter = LanguageStringFromIDConverter,
                Mode = BindingMode.OneWay
            };
            drControl.SetBinding(DateRangeControl.TitleProperty, titleBinding);

            var startDateBinding = new Binding(PropertyHelper.ExtractPropertyName(() => restriction.StartTime))
            {
                Source = restriction,
                Mode = BindingMode.OneWayToSource,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };            
            drControl.SetBinding(DateRangeControl.StartDateProperty, startDateBinding);

            var endDateBinding = new Binding(PropertyHelper.ExtractPropertyName(() => restriction.EndTime))
            {
                Source = restriction,
                Mode = BindingMode.OneWayToSource,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };          
            drControl.SetBinding(DateRangeControl.EndDateProperty, endDateBinding);

            var checkedBinding = new Binding(PropertyHelper.ExtractPropertyName(() => restriction.IsEnabled))
            {                
                Source = restriction,
                Mode = BindingMode.OneWayToSource,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            drControl.SetBinding(DateRangeControl.IsCheckedProperty, checkedBinding);

            drControl.From = OptionsAccess.Language.getLanguageString(1231);
            drControl.Until = OptionsAccess.Language.getLanguageString(1232);

            restrictionControl = drControl;
            return restrictionControl;
        }

        private UserControl constructNameValueControl(FieldDataSelectionBuilder.SearchSpecification.RestrictionSpecification restriction)
        {            
            var nvControl = new NameValueControl();

            var titleBinding = new Binding(PropertyHelper.ExtractPropertyName(() => restriction.TitleID))
            {
                Source = restriction,
                Converter = this.LanguageStringFromIDConverter,
                Mode = BindingMode.OneWay
            };
            nvControl.SetBinding(NameValueControl.TitleProperty, titleBinding);

            var valueBinding = new Binding(PropertyHelper.ExtractPropertyName(() => restriction.Value))
            {
                Source = restriction,
                Mode = BindingMode.OneWayToSource,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };            
            nvControl.SetBinding(NameValueControl.ValueProperty, valueBinding);

            var checkedBinding = new Binding(PropertyHelper.ExtractPropertyName(() => restriction.IsEnabled))
            {
                Mode = BindingMode.OneWayToSource,
                Source = restriction,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            nvControl.SetBinding(NameValueControl.IsCheckedProperty, checkedBinding);

            return nvControl;
        }

        #endregion

        public override void NavigateNext()
        {
            NavigationService.Navigate(new SelectionPage(this));
        }        

        private void listBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (updateTreeview)
            {
                resultTV.Nodes.Clear();
                if (listBoxResult.SelectedItems.Count > 0)
                {
                    try
                    {
                        var lastSelectedIso = listBoxResult.SelectedItems[0] as ISerializableObject;
                        if (lastSelectedIso != null)
                        {
                            System.Windows.Forms.TreeNode selectedElement = FieldDataManager.SelectionBuilder.TreeViewBuilder.displayTreeAround(lastSelectedIso);
                            resultTV.Nodes.Add(RootOf(selectedElement));
                            resultTV.ExpandAll();
                            resultTV.SelectedNode = selectedElement;
                        }
                    }
                    catch (Exception)
                    {
                        resultTV.Nodes.Add(new System.Windows.Forms.TreeNode("No Treeview Representation Available"));
                    }
                }
            }
        }

        private void listBoxActualSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (updateTreeview)
            {
                
                selectionTV.Nodes.Clear();
                if (listBoxActualSelection.SelectedItems.Count > 0)
                {
                    try
                    {
                        var lastSelectedContainer = listBoxActualSelection.SelectedItems[0] as FieldDataSelectionBuilder.SelectionContainer;
                        if (lastSelectedContainer != null)
                        {
                            var rootNode = FieldDataManager.SelectionBuilder.TreeViewBuilder.displayListTopDown(lastSelectedContainer.RelatedObjects, lastSelectedContainer.Root);
                            selectionTV.Nodes.Add(rootNode);
                            selectionTV.ExpandAll();
                        }
                    }
                    catch (Exception ex)
                    {
                        
                        selectionTV.Nodes.Add(new System.Windows.Forms.TreeNode("No Treeview Representation Available"));
                    }
                }
            }
        }

        private void DiversityPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            resultTV = new System.Windows.Forms.TreeView();
            resultTV.ImageList = OptionsAccess.TreeviewIcons;
            WindowsFormsHost resultTVHost = new WindowsFormsHost();
            resultTVHost.Child = resultTV;
            containerTreeViewResult.Children.Add(resultTVHost);

            selectionTV = new System.Windows.Forms.TreeView();
            selectionTV.ImageList = OptionsAccess.TreeviewIcons;
            WindowsFormsHost selectionTVHost = new WindowsFormsHost();
            selectionTVHost.Child = selectionTV;
            containerTreeViewActualSelection.Children.Add(selectionTVHost);
        }

        private static System.Windows.Forms.TreeNode RootOf(System.Windows.Forms.TreeNode childNode)
        {
            System.Windows.Forms.TreeNode current = childNode;
            while (current.Parent != null)
                current = current.Parent;
            return current;
        }

        private void checkBoxTruncate_Checked(object sender, RoutedEventArgs e)
        {
            warnTruncatedSeries();
        }
    }
}
