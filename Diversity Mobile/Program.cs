using System;

using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using UBT.AI4.Bio.DiversityCollection.Mobile.Forms;
using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.Forms.Forms;
using UBT.AI4.Bio.DivMobi.Forms.Dialogs;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;

using UBT.AI4.Bio.DivMobi.UMF.Context;
using UBT.AI4.Bio.DivMobi.UMF.Context.Actions;
using UBT.AI4.Bio.DivMobi.UMF.Context.Actions.CustomActions;
using System.Text;
using System.IO;
using UBT.AI4.Bio.DivMobi.UMF.UMF.Context.Actions.CustomActions;

namespace UBT.AI4.Bio.DiversityCollection.Mobile
{
    public static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //Kontextkonfiguration laden
            String _progPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            String contextPath = String.Concat(_progPath, @"\CustomizedContext\");
            String defaultContextFile = String.Concat(_progPath, @"\DefaultContext.xml");
            String contextFile = String.Concat(_progPath, @"\context.xml");

            if (Directory.Exists(contextPath))
            {
                try
                {
                    StreamWriter write = new StreamWriter(contextFile, false);
                    write.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    write.WriteLine("<ContextRoot xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");

                    StreamReader read = new StreamReader(defaultContextFile);
                    write.Write(read.ReadToEnd());
                    read.Close();

                    write.WriteLine("<Context ContextId=\"Customized\">");
                    DirectoryInfo dirInfo = new DirectoryInfo(contextPath);
                    FileInfo[] fileInfo = dirInfo.GetFiles("*.xml");
                    foreach (FileInfo item in fileInfo)
                    {
                        if (item != null)
                        {
                            write.WriteLine();

                            read = new StreamReader(item.FullName);
                            write.Write(read.ReadToEnd());
                            read.Close();
                            
                            write.WriteLine();
                        }
                    }

                    write.WriteLine("</Context>");
                    write.WriteLine("</ContextRoot>");
                    write.Flush();
                    write.Close();
                }
                catch (Exception)
                {
                    if (MessageBox.Show("Context Files couldn't be read. Do you want to continue Application nonetheless without contexts?.", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        contextFile = null;
                    else
                        return;
                }
            }

            try
            {
                if(contextFile != null)
                    XMLConfigurator.Configure(contextFile);

                //Actions freischalten
                ActionRegistry.Instance.RegisterAction(new SetVisibleAction());
                ActionRegistry.Instance.RegisterAction(new SetTextAction());
                ActionRegistry.Instance.RegisterAction(new SetDefaultValueAction());
            }
            catch (ContextCorruptedException ex)
            {
                MessageBox.Show(ex.Message + ". Application will shut down.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            //Application.Run(new MainForm());
            
            UserProfile active = null;
            SelectUserProfileDialog dlg = null;
            
            try
            {
                dlg = new SelectUserProfileDialog();
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message + ". Application will shut down.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (dlg != null)
            {
                // Dialog zentrieren
                dlg.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width) / 2 - (dlg.Size.Width) / 2, Screen.PrimaryScreen.WorkingArea.Location.Y);

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        active = UserProfiles.Instance.Current;
                    }
                    catch (ConnectionCorruptedException ex)
                    {
                        MessageBox.Show(ex.Message + ". Application will shut down.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (active != null)
                    {
                        try
                        {
                            DataFunctions.Instance.Update(active);
                            Cursor.Current = Cursors.WaitCursor;
                            Cursor.Show();
                            Application.Run(new MainForm(active));
                            Cursor.Current = Cursors.Default;
                        }
                        catch (DataFunctionsException ex)
                        {
                            MessageBox.Show("Changes of User Profile couldn't be saved. (" + ex.Message + ")" + ". Application will shut down.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        catch (ConnectionCorruptedException ex)
                        {
                            MessageBox.Show("Changes of User Profile couldn't be saved. (" + ex.Message + ")" + ". Application will shut down.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        catch (InvalidOperationException except)
                        {
                            MessageBox.Show(except.Message + ". Application will shut down.", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                }
            }
        }
    }
}