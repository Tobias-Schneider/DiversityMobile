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
using System.Windows.Threading;
using Diversity_Synchronization.Options.Language;
using Diversity_Synchronization.Options;
using Diversity_Synchronization.Options.Serializer;
using Diversity_Synchronization;

namespace Diversity_Synchronization_Gui
{
    /// <summary>
    /// Interaktionslogik für StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window, ILanguageRefreshable
    {    
        private DispatcherTimer _countdown = null;
        private bool _closing = false;

        public StartWindow()
        {

            InitializeComponent();

            RefreshLanguage();
        }

        public void RefreshLanguage()
        {
            try
            {
                ILanguage lf = OptionsAccess.Language;
                this.labelWelcome1.Content = lf.getLanguageString(101);
                this.labelWelcome2.Content = lf.getLanguageString(102);
            }
            catch
            {
                MessageBox.Show("Unable to load language file, exiting!");
                _closing = true;                
            }
        }

        /// <summary>
        /// Startet Timer für die Dauer der Anzeige des Startbildschirms
        /// </summary>       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this._closing)
            {
                _countdown = new DispatcherTimer();
                _countdown.Interval = TimeSpan.FromMilliseconds(3000);
                _countdown.Tick += new EventHandler(Countdown_Tick);
                _countdown.Start();
            }
            else
                this.Close();
        }

        /**
         * Beendet Countdown und ruft Hauptfenster auf.
         */
        void Countdown_Tick(object sender, EventArgs e)
        {
            _countdown.Stop();
            new FrameWindow().Show();
            this.Close();
        }
    }
}
