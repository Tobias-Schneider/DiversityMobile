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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using Diversity_Synchronization.Options.Language;
using System.Windows.Controls;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui.Pages
{
    public abstract class DiversityPage : Page , ILanguageRefreshable
    {
        private DiversityPage previousPage;

        public DiversityPage():base()
        {
        }

        public DiversityPage(DiversityPage previous)
        {
            previousPage = previous;
        }

        public abstract void NavigateNext();

        public void NavigatePrevious()
        {
            if (Previous != null)
                NavigationService.Navigate(Previous);
        }

        static DiversityPage()
        {
            Connections = new ConnectionPage();
        }
        
    

        #region Properties
        public static DiversityPage Connections { get; set; }
        public static DiversityPage Actions { get; set; }


        protected static readonly DependencyPropertyKey HasNextPropertyKey = DependencyProperty.RegisterReadOnly("HasNext", typeof(bool), typeof(DiversityPage), new PropertyMetadata(false));
        public static DependencyProperty HasNextProperty { get { return HasNextPropertyKey.DependencyProperty; } }
        public bool HasNext
        {
            get { return (bool)GetValue(HasNextPropertyKey.DependencyProperty); }
            protected set { SetValue(HasNextPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey HasPreviousPropertyKey = DependencyProperty.RegisterReadOnly("HasPrevious", typeof(bool), typeof(DiversityPage), new PropertyMetadata(false));
        public static DependencyProperty HasPreviousProperty { get { return HasPreviousPropertyKey.DependencyProperty; } }
        public bool HasPrevious
        {
            get { return (bool)GetValue(HasPreviousPropertyKey.DependencyProperty); }
            protected set { SetValue(HasPreviousPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey NextTitlePropertyKey = DependencyProperty.RegisterReadOnly("NextTitle", typeof(string), typeof(DiversityPage), new PropertyMetadata(""));
        public static DependencyProperty NextTitleProperty { get { return NextTitlePropertyKey.DependencyProperty; } }
        public string NextTitle
        {
            get { return (string)GetValue(NextTitlePropertyKey.DependencyProperty); }
            protected set { SetValue(NextTitlePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey PreviousTitlePropertyKey = DependencyProperty.RegisterReadOnly("PreviousTitle", typeof(string), typeof(DiversityPage), new PropertyMetadata(""));
        public static DependencyProperty PreviousTitleProperty { get { return PreviousTitlePropertyKey.DependencyProperty; } }
        public string PreviousTitle
        {
            get { return (string)GetValue(PreviousTitlePropertyKey.DependencyProperty); }
            protected set { SetValue(PreviousTitlePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey PageDescriptionPropertyKey = DependencyProperty.RegisterReadOnly("PageDescription", typeof(string), typeof(DiversityPage), new PropertyMetadata(""));
        public static DependencyProperty PageDescriptionProperty { get { return PageDescriptionPropertyKey.DependencyProperty; } }
        public string PageDescription
        {
            get { return (string)GetValue(PageDescriptionPropertyKey.DependencyProperty); }
            protected set { SetValue(PageDescriptionPropertyKey, value); }
        }

        public DiversityPage Previous
        {
            get
            {
                return previousPage;
            }
        }

        protected SyncStatus State
        {
            get
            {
                return FindResource("SyncStatus") as SyncStatus;
            }
        }

        #endregion

        #region ILanguageRefreshable Member

        public abstract void RefreshLanguage();
        

        #endregion
    }
}
