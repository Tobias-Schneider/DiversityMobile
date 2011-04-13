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
using System.Windows.Shapes;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization.Options;
using Diversity_Synchronization.Options.Serializer;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui
{
    /// <summary>
    /// Interaktionslogik für YesNoDecisionWindow.xaml
    /// </summary>
    public partial class YesNoDecisionWindow : Window , ILanguageRefreshable
    {
        private int titleID,
                    captionID,
                    textID,
                    buttonYesTextID,
                    buttonNoTextID;

        /**
         * MessageBox: Fenstertitel, titel, nachricht und Buttons können beschriftet werden
         */
        public YesNoDecisionWindow(int titleID, int captionID, int textID, int buttonYesTextID, int buttonNoTextID)
        {
            InitializeComponent();

            this.titleID = titleID;
            this.captionID = captionID;
            this.textID = textID;
            this.buttonYesTextID = buttonYesTextID;
            this.buttonNoTextID = buttonNoTextID;

            RefreshLanguage();            
        }

        #region Button Events

        private void buttonYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region ILanguageRefreshable Member

        public void RefreshLanguage()
        {
            ILanguage lang = OptionsAccess.Language;

            this.Title = lang.getLanguageString(titleID);
            this.textPageCaption.Content = lang.getLanguageString(captionID);
            this.textPageDescription.Text = lang.getLanguageString(textID);
            this.buttonYes.Content = lang.getLanguageString(buttonYesTextID);
            this.buttonNo.Content = lang.getLanguageString(buttonNoTextID);
        }

        #endregion
    }
}
