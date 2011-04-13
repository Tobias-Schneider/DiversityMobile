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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization.Options.Serializer;
using Diversity_Synchronization.Options;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui.Options
{
    /// <summary>
    /// Interaktionslogik für ProgramOptionsWindow.xaml
    /// </summary>
    public partial class ProgramOptionsWindow : Window
    {
        public ProgramOptionsWindow()
        {
            InitializeComponent();

            RefreshLanguage();
            UpdateProgramOptionsView();
        }

        private void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(401);
            this.menuItemMenu.Header = lf.getLanguageString(402);
            this.menuItemSaveAndClose.Header = lf.getLanguageString(403);
            this.menuItemCancel.Header = lf.getLanguageString(404);
            this.menuItemHelp.Header = lf.getLanguageString(405);
            this.textPageCaption.Content = lf.getLanguageString(421);
            this.textPageDescription.Text = lf.getLanguageString(422);
            this.groupBoxLanguage.Header = lf.getLanguageString(423);
            this.labelLanguage.Content = lf.getLanguageString(424);
            this.buttonChooseLanguage.Content = lf.getLanguageString(425);
            this.groupBoxOther.Header = lf.getLanguageString(426);
            this.labelHidePassword.Content = lf.getLanguageString(427);
            this.checkBoxHidePassword.Content = lf.getLanguageString(428);
            this.buttonSave.Content = lf.getLanguageString(451);
            this.buttonCancel.Content = lf.getLanguageString(452);
        }

        /**
         * Aktualisiert die Optionen in der Ansicht
         */
        private void UpdateProgramOptionsView()
        {
            ProgramOptions po = OptionsAccess.ProgramOptions;

            this.textBoxLanguageFile.Text = po.LanguageFile;
            this.checkBoxHidePassword.IsChecked = po.HidePassword;
        }

        /**
         * Speichert die Optionen ab
         */
        private void SaveProgramOptions()
        {
            ProgramOptions po = OptionsAccess.ProgramOptions;

            po.LanguageFile = this.textBoxLanguageFile.Text;

            if (this.checkBoxHidePassword.IsChecked == null || this.checkBoxHidePassword.IsChecked == false)
            {
                po.HidePassword = false;
            }
            else
            {
                po.HidePassword = true;
            }
            OptionsAccess.ProgramOptions = po;

            DialogResult = true;
        }

        #region Button Events

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveProgramOptions();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ButtonChooseLanguage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.InitialDirectory = OptionsAccess.getFolderPath(ApplicationFolder.Language);
            
            ofd.Filter = OptionsAccess.Language.getLanguageString(461);
            if (ofd.ShowDialog() == true)
            {
                if (System.IO.Path.GetDirectoryName(ofd.FileName) == OptionsAccess.getFolderPath(ApplicationFolder.Language))
                    this.textBoxLanguageFile.Text = System.IO.Path.GetFileName(ofd.FileName);
                else
                    MessageBox.Show(OptionsAccess.Language.getLanguageString(429));
            }
        }

        #endregion

        #region Menu Events

        private void menuItemSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            SaveProgramOptions();
        }

        private void menuItemCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void menuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        #endregion
    }
}
