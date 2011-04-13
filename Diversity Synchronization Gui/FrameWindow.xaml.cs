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
using Diversity_Synchronization;
using Diversity_Synchronization.Options;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization.Options.Serializer;
using Diversity_Synchronization_Gui.Options;
using Diversity_Synchronization_Gui.Pages;

namespace Diversity_Synchronization_Gui
{
    /// <summary>
    /// Interaktionslogik für FrameWindow.xaml
    /// </summary>
    public partial class FrameWindow : Window, ILanguageRefreshable
    {
        private DiversityPage currentPage { get { return frameContent.Content as DiversityPage; } }    

        private Binding NextTitle, PreviousTitle, NextVisibility, PreviousVisibility, PageTitle, PageDescription;

        public FrameWindow()
        {
            InitializeComponent();

            setBindings();        

            RefreshLanguage();            

            //Aufruf der "Startseite"            
            frameContent.Navigate(DiversityPage.Connections);
        }

        private void setBindings()
        {
            NextTitle = new Binding("Content.NextTitle") { Mode = BindingMode.OneWay, Source = frameContent };
            buttonNavigationNext.SetBinding(Button.ContentProperty, NextTitle);

            NextVisibility = new Binding("Content.HasNext") { Mode = BindingMode.OneWay, Source = frameContent, Converter = new BooleanToVisibilityConverter() };
            buttonNavigationNext.SetBinding(Button.VisibilityProperty, NextVisibility);

            PreviousTitle = new Binding("Content.PreviousTitle") { Mode = BindingMode.OneWay, Source = frameContent };
            buttonNavigationBack.SetBinding(Button.ContentProperty, PreviousTitle);

            PreviousVisibility = new Binding("Content.HasPrevious") { Mode = BindingMode.OneWay, Source = frameContent, Converter = new BooleanToVisibilityConverter() };
            buttonNavigationBack.SetBinding(Button.VisibilityProperty, PreviousVisibility);

            PageTitle = new Binding("Content.Title") { Mode = BindingMode.OneWay, Source = frameContent };
            textPageCaption.SetBinding(Label.ContentProperty, PageTitle);

            PageDescription = new Binding("Content.PageDescription") { Mode = BindingMode.OneWay, Source = frameContent };
            textPageDescription.SetBinding(TextBlock.TextProperty, PageDescription);
        }

        public void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            //globale Texte des Fensters
            this.Title = lf.getLanguageString(201);
            this.menuItemProgram.Header = lf.getLanguageString(202);
            this.menuItemExit.Header = lf.getLanguageString(203);
            this.menuItemOptions.Header = lf.getLanguageString(204);
            this.menuItemRepositoryOptions.Header = lf.getLanguageString(205);
            this.menuItemActiveSyncOptions.Header = lf.getLanguageString(206);
            
            
            this.menuItemProgramOptions.Header = lf.getLanguageString(209);
           
            this.menuItemAbout.Header = lf.getLanguageString(211); 
         
            if (currentPage != null)
            {
                currentPage.RefreshLanguage();
            }
        }     
        

        /**
         * Delegiert die Aufgabe, die angezeigten Repository-Einstellungen anzupassen, an
         * das entsprechende Fenster, falls dieses gerade angezeigt wird.
         */
        private void UpdateRepositoryOptionsView()
        {
            if (currentPage is ConnectionPage)
            {
                (currentPage as ConnectionPage).UpdateRepositoryOptionsView();
            }
        }

        /**
         * Delegiert die Aufgabe, die angezeigten ActiveSync-Einstellungen anzupassen, an
         * das entsprechende Fenster, falls dieses gerade angezeigt wird.
         */
        private void UpdateActiveSyncOptionsView()
        {
            if (currentPage is ConnectionPage)
            {
                (currentPage as ConnectionPage).UpdateActiveSyncOptionsView();
            }
        }

        /**
         * Delegiert die Aufgabe, die angezeigten Passwort-Einstellungen anzupassen, an
         * das entsprechende Fenster, falls dieses gerade angezeigt wird.
         */
        private void UpdateHidePassword()
        {
            if (currentPage is ConnectionPage)
            {
                (currentPage as ConnectionPage).UpdateHidePassword();
            }
        }

        

        #region Window Events

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ConnectionsAccess.Instance.disconnectEverything();
            ConnectionsAccess.Instance.writeBackChanges();
        }

        #endregion

        #region Menu Events

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            //Löst Window_Closing()-Event aus
            this.Close();
        }

        private void MenuItemRepositoryOptions_Click(object sender, RoutedEventArgs e)
        {
            RepositoryOptionsWindow row = new RepositoryOptionsWindow();
            row.ShowDialog();

            if (row.DialogResult.HasValue && row.DialogResult.Value == true)
            {
                UpdateRepositoryOptionsView();
            }
            
        }

        private void MenuItemActiveSyncOptions_Click(object sender, RoutedEventArgs e)
        {
            ActiveSyncOptionsWindow asw = new ActiveSyncOptionsWindow();
            asw.ShowDialog();

            if (asw.DialogResult.HasValue && asw.DialogResult.Value == true)
            {
                UpdateActiveSyncOptionsView();
            }
        }

        private void MenuItemSynchronizationOptions_Click(object sender, RoutedEventArgs e)
        {
            SynchronizationOptionsWindow syw = new SynchronizationOptionsWindow();
            syw.ShowDialog();
        }

        private void MenuItemSearchOptions_Click(object sender, RoutedEventArgs e)
        {
            SearchOptionsWindow sow = new SearchOptionsWindow();
            sow.ShowDialog();
        }

        private void MenuItemProgramOptions_Click(object sender, RoutedEventArgs e)
        {
            ProgramOptionsWindow pow = new ProgramOptionsWindow();
            pow.ShowDialog();

            if (pow.DialogResult.HasValue && pow.DialogResult.Value == true)
            {
                RefreshLanguage();
                UpdateHidePassword();
            }
        }

        private void MenuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aw = new AboutWindow();
            aw.ShowDialog();
        }

        #endregion

        #region Button Events

        /**
         * Hauptnavigation innerhalb des Programms (vorwärts)
         */
        private void ButtonNavigationNext_Click(object sender, RoutedEventArgs e)
        {

            if (frameContent.Content is DiversityPage)
            {
                (frameContent.Content as DiversityPage).NavigateNext();
                return;
            }                 
        }

        /**
         * Hauptnavigation innerhalb des Programms (rückwärts)
         */
        private void buttonNavigationBack_Click(object sender, RoutedEventArgs e)
        {
            if (frameContent.Content is DiversityPage)
            {
                (frameContent.Content as DiversityPage).NavigatePrevious();
                return;
            }

            
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
