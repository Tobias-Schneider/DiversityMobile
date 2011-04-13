using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using UBT.AI4.Bio.DivMobi.DatabaseConnector;
using UBT.AI4.Bio.DivMobi.DataLayer;
using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.SyncTool;

namespace UBT.AI4.Bio.DivMobi.SyncGui
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SyncGuiMainForm());
        }
    }
}
