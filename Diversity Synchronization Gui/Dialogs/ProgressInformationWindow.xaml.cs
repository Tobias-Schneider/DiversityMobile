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
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization;
using System.Windows.Threading;

namespace Diversity_Synchronization_Gui
{
    /// <summary>
    /// Interaktionslogik für ProgressInformationWindow.xaml
    /// </summary>
    public partial class ProgressInformationWindow : Window, ILanguageRefreshable
    {
        private ProgressReporter ProgressNotifier
        {
            get
            {
                return this.DataContext as ProgressReporter;
            }
        }

        private DispatcherTimer closeTimer;

        public ProgressInformationWindow()
        {
            InitializeComponent();

            RefreshLanguage();


            closeTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1)};
            closeTimer.Tick += new EventHandler(closeTimer_Tick);

        }

        void closeTimer_Tick(object sender, EventArgs e)
        {
            if (ProgressNotifier != null && ProgressNotifier.IsProgressFinished)
                this.Close();
        }

        
        
        public void RefreshLanguage()
        {
            ILanguage lf = OptionsAccess.Language;

            this.Title = lf.getLanguageString(1801);            
            this.buttonCancel.Content = lf.getLanguageString(1851);
        }
       
        #region Button Events

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (ProgressNotifier != null)
            {
                ProgressNotifier.IsCancelRequested = true;
            }
        }

        #endregion

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            closeTimer.IsEnabled = ProgressNotifier != null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ProgressNotifier != null && !ProgressNotifier.IsProgressFinished)
            {
                ProgressNotifier.IsCancelRequested = true;
                e.Cancel = true;
            }
        }

        
    }
}
