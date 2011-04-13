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
    public partial class MessageBoxWindow : Window , ILanguageRefreshable
    {
        private int titleID,
                    captionID,
                    textID,
                    buttonOKID;
                    

        /**
         * MessageBox: Fenstertitel, titel, nachricht und Buttons können beschriftet werden
         */
        public MessageBoxWindow(int titleID, int captionID, int textID, int buttonOKID)
        {
            InitializeComponent();

            this.titleID = titleID;
            this.captionID = captionID;
            this.textID = textID;
            this.buttonOKID = buttonOKID;            

            RefreshLanguage();            
        }

        #region Button Events

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }       

        #endregion

        #region ILanguageRefreshable Member

        public void RefreshLanguage()
        {
            ILanguage lang = OptionsAccess.Language;

            this.Title = lang.getLanguageString(titleID);
            this.textPageCaption.Content = lang.getLanguageString(captionID);
            this.textPageDescription.Text = lang.getLanguageString(textID);
            this.buttonOK.Content = lang.getLanguageString(buttonOKID);            
        }

        #endregion
    }
}
