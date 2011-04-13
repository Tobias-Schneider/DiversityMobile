using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UBT.AI4.Bio.DiversityCollection.Ressource.Media
{
    // **********************************************
    // Dies ist eines der 322 Rezepte aus dem
    // C# 2005 Premium Codebook. 
    // Das Snippet wurde vom Autor mit freundlicher 
    // Genehmigung von Addison Wesley 
    // bei dotnet-snippets.de veröffentlicht.
    // **********************************************


    using System;
    using System.IO;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Repräsentiert eine MCI-Ausnahme
    /// </summary>
    public class MciException : Exception
    {
        public MciException(string message)
        {
        }
    }

    /// <summary>
    /// Klasse für das Abspielen von Mediadateien über MCI 
    /// </summary>
    public class Mci : IDisposable
    {
        /* Deklaration der MCI-Funktionen, -Konstanten und -Strukturen */
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern int mciSendString(string lpstrCommand,
           StringBuilder lpstrReturnString, int uReturnLength, IntPtr
           hwndCallback);

        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern int mciGetErrorString(int dwError, StringBuilder
           lpstrBuffer, int uLength);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetShortPathName(string lpszLongPath,
           StringBuilder lpszShortPath, int cchBuffer);

        /* Eigenschaft für den MCI-Alias */
        private string alias;

        private bool isOpen = false;
        /// <summary>
        /// Gibt an, ob gerade ein MCI-Gerät geöffnet ist
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
        }

        /// <summary>
        /// Liefert eine Fehlermeldung zu einem gegebenen 
        /// Fehlercode
        /// </summary>
        /// <param name="errorCode">Der Fehlercode</param>
        /// <returns>Gibt die Fehlermeldung zurück</returns>
        private string GetMciError(int errorCode)
        {
            StringBuilder errorMessage = new StringBuilder(255);
            if (mciGetErrorString(errorCode, errorMessage,
               errorMessage.Capacity) == 0)
            {
                return "MCI-Fehler " + errorCode;
            }
            else
            {
                return errorMessage.ToString();
            }
        }

        /// <summary>
        /// Öffnet eine Multimediadatei
        /// </summary>
        /// <param name="filename">Der Dateiname</param>
        public void Open(string filename)
        {
            Open(filename, null);
        }

        /// <summary>
        /// Öffnet eine Multimediadatei
        /// </summary>
        /// <param name="filename">Der Dateiname</param>
        /// <param name="owner">Der Besitzer der erzeugten MCI-Instanz</param>
        public void Open(string filename, Control owner)
        {
            // Schließen einer eventuell zuvor noch geöffneten
            // Datei
            if (this.IsOpen)
            {
                this.Close();
            }

            // Alias für das MCI-Gerät erzeugen
            this.alias = Guid.NewGuid().ToString("N");

            // Überprüfen, ob die Datei existiert
            if (File.Exists(filename) == false)
            {
                throw new FileNotFoundException("Die Datei '" + filename +
                   "' existiert nicht " + filename);
            }

            // Den kurzen Dateinamen ermitteln
            StringBuilder shortPath = new StringBuilder(261);
            if (GetShortPathName(filename, shortPath, shortPath.Capacity) == 0)
            {
                throw new MciException("Fehler beim Auslesen des kurzen " +
                   "Dateinamens für '" + filename + "': Windows-Fehler " +
                   Marshal.GetLastWin32Error());
            }

            // MCI-Befehlsstring zum Öffnen zusammensetzen
            string mciString = "open " + shortPath.ToString() +
               " type mpegvideo alias " + this.alias;

            if (owner != null)
            {
                mciString += " parent " + (int)owner.Handle + " style child";
            }

            // MCI-Gerät öffnen
            int result = mciSendString(mciString, null, 0, IntPtr.Zero);
            if (result != 0)
            {
                throw new MciException("Fehler beim Öffnen des MCI-Geräts: " +
                   GetMciError(result));
            }

            this.isOpen = true;

            // Das Zeitformat für Längen- und Positionsangaben explizit
            // auf Millisekunden setzen
            mciString = "set " + this.alias + " time format ms";
            result = mciSendString(mciString, null, 0, IntPtr.Zero);
            if (result != 0)
            {
                throw new MciException("Fehler beim Setzen des Zeitformats: " +
                   GetMciError(result));
            }
        }

        /// <summary>
        /// Gibt die Abspiellänge der aktuellen Multimediadatei zurück
        /// </summary>
        public int Length
        {
            get
            {
                StringBuilder buffer = new StringBuilder(261);
                int result = mciSendString("status " + this.alias + " length",
                   buffer, buffer.Capacity, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Lesen von 'Length': " +
                       GetMciError(result));
                }

                return int.Parse(buffer.ToString());
            }
        }

        /// <summary>
        /// Spielt eine geladene Multimediadatei ab
        /// </summary>
        /// <param name="repeat">Gibt an, ob das Abspielen nach dem Ende wieder von vorne beginnen soll</param>
        public void Play(bool repeat)
        {
            Play(0, this.Length, repeat);
        }

        /// <summary>
        /// Spielt eine geladene Multimediadatei ab einer bestimmten Position ab
        /// </summary>
        /// <param name="from">Die Startposition</param>
        /// <param name="repeat">Gibt an, ob das Abspielen nach dem Ende wieder von vorne beginnen soll</param>
        public void Play(int from, bool repeat)
        {
            Play(from, this.Length - from, repeat);
        }

        /// <summary>
        /// Spielt eine geladene Multimediadatei ab einer bestimmten Position 
        /// und bis zu einer bestimmten anderen Position ab
        /// </summary>
        /// <param name="from">Die Startposition</param>
        /// <param name="to">Die Endposition</param>
        /// <param name="repeat">Gibt an, ob das Abspielen nach dem Ende wieder von vorne beginnen soll</param>
        public void Play(int from, int to, bool repeat)
        {
            string mciString = "play " + this.alias +
               " from " + from + " to " + to;
            if (repeat)
            {
                mciString += " repeat";
            }
            int result = mciSendString(mciString, null, 0, IntPtr.Zero);
            if (result != 0)
            {
                throw new MciException("Fehler beim Aufruf von 'Play': " +
                   GetMciError(result));
            }
        }

        /// <summary>
        /// Verwaltet die Lautstärke
        /// </summary>
        public int Volume
        {
            get
            {
                StringBuilder buffer = new StringBuilder(261);
                int result = mciSendString("status " + this.alias + " volume",
                   buffer, buffer.Capacity, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Lesen von 'Volume': " +
                       GetMciError(result));
                }
                return int.Parse(buffer.ToString());
            }

            set
            {
                int result = mciSendString("setaudio " + this.alias +
                   " volume to " + value, null, 0, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Aufruf von 'SetAudio': " +
                       GetMciError(result));
                }
            }
        }

        /// <summary>
        /// Verwaltet die aktuelle Position in den Multimedia-Daten
        /// </summary>
        public int Position
        {
            get
            {
                StringBuilder buffer = new StringBuilder(261);
                int result = mciSendString("status " + this.alias + " position",
                   buffer, buffer.Capacity, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Lesen von 'Position': " +
                       GetMciError(result));
                }
                return int.Parse(buffer.ToString());
            }

            set
            {
                int result = mciSendString("seek " + this.alias +
                   " to " + value, null, 0, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Setzen von 'Position'" +
                       GetMciError(result));
                }
                result = mciSendString("play " + this.alias, null, 0, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Aufruf von 'Play': " +
                       GetMciError(result));
                }
            }
        }

        /// <summary>
        /// Verwaltet die aktuelle Abspiel-Geschwindigkeit
        /// </summary>
        public int PlaybackSpeed
        {
            get
            {
                StringBuilder buffer = new StringBuilder(261);
                int result = mciSendString("status " + this.alias + " speed",
                   buffer, buffer.Capacity, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Lesen von 'Speed'" +
                       GetMciError(result));
                }
                return int.Parse(buffer.ToString());
            }

            set
            {
                string mciString = "set " + this.alias + " speed " + value;
                int result = mciSendString(mciString, null, 0, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Setzen von 'Speed': " +
                       GetMciError(result));
                }
            }
        }

        /// <summary>
        /// Methode zur Definition der Abspielposition und -größe für Videos, die auf einem Formular, einer PictureBox o. ä. dargestellt werden
        /// </summary>
        /// <param name="x">Die X-Position</param>
        /// <param name="y">Die Y-Position</param>
        /// <param name="width">Die Breite</param>
        /// <param name="height">Die Höhe</param>
        public void SetRectangle(int x, int y, int width, int height)
        {
            string mciString = "put " + this.alias + " window at " + x +
               " " + y + " " + width + " " + height;
            int result = mciSendString(mciString, null, 0, IntPtr.Zero);
            if (result != 0)
            {
                throw new MciException("Fehler beim Aufruf von 'Put Window at': " +
                   GetMciError(result));
            }
        }

        /// <summary>
        /// Stoppt den aktuellen Abspielvorgang
        /// </summary>
        public void Stop()
        {
            string mciString = "stop " + this.alias;
            int result = mciSendString(mciString, null, 0, IntPtr.Zero);
            if (result != 0)
            {
                throw new MciException("Fehler beim Aufruf von 'Stop': " +
                   GetMciError(result));
            }
        }

        /// <summary>
        /// Schließt ein aktuell geöffnetes MCI-Gerät
        /// </summary>
        public void Close()
        {
            if (this.isOpen)
            {
                string mciString = "close " + this.alias;
                int result = mciSendString(mciString, null, 0, IntPtr.Zero);
                if (result != 0)
                {
                    throw new MciException("Fehler beim Aufruf von 'Close': " +
                       GetMciError(result));
                }

                this.isOpen = false;
            }
        }

        /// <summary>
        /// Schließt ein aktuell geöffnetes MCI-Gerät
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }

        /// <summary>
        /// Finalisierer
        /// </summary>
        ~Mci()
        {
            this.Close();
        }
    }


}
