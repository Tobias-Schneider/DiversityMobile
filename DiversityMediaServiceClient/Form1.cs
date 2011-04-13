using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


using System.Data.Common;
using System.Data.SqlServerCe;
using OpenNETCF.Desktop.Communication;
using System.Threading;
using System.Xml;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.SyncBase;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer.Util;
using UBT.AI4.Bio.DivMobi.ListSynchronization;
using UserSyncGui;

namespace UBT.AI4.Bio.DivMobi.MediaServiceApplication
{
    public partial class Form1 : Form
    {

        //UserGUITypes
        private string progPath;
        private string progPicturePath;
        private string taxPath;
        private string loginName;
        private string password;
        private int projectID;

        //----------------- DB Connections -----------
        // Repository  
        private Serializer repositorySerializer;
        private bool repositoryConnected;
        //private SyncTool repositorySyncTool;
        //private String IPAddress = "141.84.65.107";
        //private String IPPort = "5432";
        //private String InitialCatalog = "DiversityCollection_BaseTest";

        private String IPAddress;
        private String IPPort;
        private String InitialCatalog;
        private bool SqlAuth;
        private bool HidePW;
        private String TaxonNamesInitialCatalog;

        //SyncDB
        private Serializer syncDBSerializer;
        //private SyncPool SyncPool;
        IList<Type> usedTypes = new List<Type>(); //verwaltet die Typen, die synchronisiert werden sollen


        //mobileDB
        private String mobilePath = @"\Programme\divcolmobile\";
        private String mobileDBPath = @"\Programme\divcolmobile\MobileDB.sdf";
        private String mobileTaxonPath = @"\Programme\divcolmobile\TaxonNames.sdf";
        private String mobilePicturePath = @"\Programme\divcolmobile\pictures\";
        private bool mobileConnected;
        //private String mobileDBPath;
        //private String mobileTaxonPath;
        private Serializer mobileDBSerializer;
        //private SyncTool mobileDBSyncTool;

        //Connection via ActiveSync
        private RAPI rapi;
        private bool activesync;
        private int connectingTimeOut;
        private bool useDevice;
        private int deviceWidth;
        private int deviceHeight;

        //Types
        IList<Type> divMobiTypes = new List<Type>(); //verwaltet die aus DiversityCollection verwendeten Typen
        IList<Type> syncTypes = new List<Type>(); //Verwaltet die Felder der Synchronisation

        // Progress Information
        ProgressThread progressThread = null;

        //-------------------------------------------------------------------------------------------

        // Filter for File dialogs
        private const String StrFilter = "All files(*.*)|*.*|Images(*.jpg;*.jpeg;*.bmp;*.tif;*.png;*.gif)|*.jpg;*.jpeg;*.bmp;*.tif;*.tiff;*.png;*.gif|Shapes(*.txt)|*.txt"; // later --> ".shp";
        // Supported image formats
        private String[] m_SupportedFormats = { ".jpg", ".jpeg", ".bmp", ".tif", ".tiff", ".png", ".gif" };
        // Supported audio formats
        private String[] m_SupportedAudioFormats = { ".wav", ".mp4" };
        private char[] charSeparators = new char[] { '\\' };
        // Package size in bytes
        private const int m_PackageSize = 4193000; // < 4 MB !!

        // Loaded image
        private String m_ImageName = String.Empty;
        // Filename
        private String m_FileName = String.Empty;

        // Constructor
        public Form1()
        {
            InitializeComponent();
            rapi = new RAPI();
            progPath = Environment.CurrentDirectory + @"\MobileDB.sdf";
            if (System.IO.File.Exists(progPath))
                File.Delete(progPath);
            taxPath = Environment.CurrentDirectory + @"\TaxonNames.sdf";
            if (System.IO.File.Exists(taxPath))
                File.Delete(taxPath);
            progPicturePath = Environment.CurrentDirectory + @"\pictures\";
            if (!System.IO.Directory.Exists(progPicturePath))
                System.IO.Directory.CreateDirectory(progPicturePath);
            mobileConnected = false;
            repositoryConnected = false;
            divMobiTypes.Add(typeof(Property));
            divMobiTypes.Add(typeof(Analysis));
            divMobiTypes.Add(typeof(AnalysisTaxonomicGroup));
            divMobiTypes.Add(typeof(CollectionAgent));
            divMobiTypes.Add(typeof(CollectionEvent));
            divMobiTypes.Add(typeof(CollectionEventImage));
            divMobiTypes.Add(typeof(CollectionEventLocalisation));
            divMobiTypes.Add(typeof(CollectionEventProperty));
            divMobiTypes.Add(typeof(CollectionSpecimen));
            divMobiTypes.Add(typeof(CollectionSpecimenImage));
            divMobiTypes.Add(typeof(CollEventImageType_Enum));
            divMobiTypes.Add(typeof(CollSpecimenImageType_Enum));
            divMobiTypes.Add(typeof(CollTaxonomicGroup_Enum));
            divMobiTypes.Add(typeof(Identification));
            divMobiTypes.Add(typeof(IdentificationUnit));
            divMobiTypes.Add(typeof(IdentificationUnitAnalysis));
            divMobiTypes.Add(typeof(LocalisationSystem));
            divMobiTypes.Add(typeof(CollCircumstances_Enum));
            divMobiTypes.Add(typeof(CollUnitRelationType_Enum));
            divMobiTypes.Add(typeof(CollectionEventSeries));
            //Bis hier: Korrepondiert zu DBVersion 20
            //divMobiTypes.Add(typeof(CollectionEventSeriesImage));
            //divMobiTypes.Add(typeof(CollEventSeriesImageType_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 22
            divMobiTypes.Add(typeof(CollIdentificationCategory_Enum));
            //divMobiTypes.Add(typeof(CollTypeStatus_Enum));
            //divMobiTypes.Add(typeof(CollIdentificationQualifier_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 25
            //divMobiTypes.Add(typeof(CollLabelTranscriptionState_Enum));
            //divMobiTypes.Add(typeof(CollLabelType_Enum));
            ////Bis hier: Korrepondiert zu DBVersion 27
            //divMobiTypes.Add(typeof(Collection));
            //divMobiTypes.Add(typeof(CollectionProject));
            //divMobiTypes.Add(typeof(CollectionSpecimenPart));
            //divMobiTypes.Add(typeof(CollMaterialCategory_Enum));
            //Bis hier: Korrepondiert zu DBVersion 31
            divMobiTypes.Add(typeof(IdentificationUnitGeoAnalysis));
            divMobiTypes.Add(typeof(AnalysisResult));

            divMobiTypes.Add(typeof(UserTaxonomicGroupTable));
            //Bis hier: Korrepondiert zu DBVersion 34

            //Verwendete Typen von der Synchronisationsdatenbank in Liste einfügen

            syncTypes.Add(typeof(SyncItem));
            syncTypes.Add(typeof(FieldState));
            try
            {
                this.readSettingsFromXML();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to read XML-Settings");
            }
        }
        
        // Event handlers to load and transfer an image
        
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenSampleFiles();
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            ReadFileAndTransfer(m_FileName);
        }

        // Open file dialog
        private void OpenSampleFiles()
        {
            try
            {
                // Open file dialog
                OpenFileDialog dlg = new OpenFileDialog();
                // Filter settings
                dlg.Filter = StrFilter;
                // Just single file select allowed
                dlg.Multiselect = false;
                // Show dialog
                dlg.ShowDialog();
                // Get selected filename
                m_FileName = dlg.FileName;
                // Open image file, if supported
                if (isSupportedImageFile(m_FileName))
                    LoadImage(m_FileName);
                else if (isSupportedAudioFile(m_FileName))
                    LoadSound(m_FileName);
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }

        // Check if filename extension is in supported formats
        private bool isSupportedImageFile(String fileName)
        {
            try
            {
                // Check if filename extension is supported
                foreach (String str in m_SupportedFormats)
                {
                    if ((fileName.ToLower()).EndsWith(str))
                        return true;
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }

            return false;
        }

        // Check if filename extension is in supported formats
        private bool isSupportedAudioFile(String fileName)
        {
            try
            {
                // Check if filename extension is supported
                foreach (String str in m_SupportedAudioFormats)
                {
                    if ((fileName.ToLower()).EndsWith(str))
                        return true;
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }

            return false;
        }

        // Load image to picture box
        private void LoadImage(String path)
        {
            try
            {
                // Split up path string
                string[] splitPath = path.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                // set filename
                if (splitPath.Length > 0)
                    m_ImageName = splitPath[splitPath.Length - 1];
                else
                    m_ImageName = "NoName";
                // Load image bitmap
                pictureBox1.Image = Image.FromFile(path);
                textBox1.Text = m_ImageName;
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }

        }

        // Load image to picture box
        private void LoadSound(String path)
        {
            try
            {
                // Split up path string
                string[] splitPath = path.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                // set filename
                if (splitPath.Length > 0)
                    m_ImageName = splitPath[splitPath.Length - 1];
                else
                    m_ImageName = "NoName";
                textBox1.Text = m_ImageName;
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }

        }

        // Transfer selected image file to Webserver
        public String ReadFileAndTransfer(String path)
        {
            FileStream fileStrm = null;
            BinaryReader rdr = null;
            String retString=String.Empty;
            byte[] data = null;

            DateTime start = DateTime.Now;
            textBox1.Text = "";
            label2.Text = "";

            // Create proxy instance
            MediaServiceProxy.DiversityMediaServiceClient proxy = new MediaServiceProxy.DiversityMediaServiceClient();
            // proxy.ClientCredentials.UserName.UserName = "Media";
            // proxy.ClientCredentials.UserName.Password = "Service"; 

            FileInfo myFile = new FileInfo(path);
            // string lastWriteTime = myFile.LastWriteTime.Ticks.ToString();
            string lastWriteTime = myFile.LastWriteTime.ToString();

            try
            {
                // Create stream and reader for file data
                fileStrm = new FileStream(path, FileMode.Open, FileAccess.Read);
                rdr = new BinaryReader(fileStrm);

                // Number of bytes to be transferred
                long numBytes = fileStrm.Length;

                // Package counter
                int count = 0;
                // Return string

                textBox1.Text = string.Empty;

                /* ---------------------------- packages method -------------------------
                // Webservice method call
                String retStr = String.Empty;
                retStr = proxy.BeginTransaction(m_ImageName, m_ImageName, "Image", (float)11.2345, (float)48.9876, (float)333.33, "Wolfgang Reichert", lastWriteTime, 3);
                textBox1.Text = retStr;

                // Read complete packages
                while (numBytes > (long)m_PackageSize)
                {
                    // Read package of data from file
                    data = rdr.ReadBytes(m_PackageSize);

                    // Webservice method call
                    if ((retStr = proxy.EncodeFile(data)) != String.Empty)
                    {
                        textBox1.Text = retStr;
                        numBytes = 0;
                    }
                    else
                    {
                        count++;
                        numBytes -= (long)m_PackageSize;
                    }
                }
                
                // Read rest of file data
                if (numBytes > 0)
                {
                    data = rdr.ReadBytes((int)numBytes);

                    if ((retStr = proxy.EncodeFile(data)) != String.Empty)
                    {
                        textBox1.Text = retStr;
                    }
                    else
                    {
                        count++;
                    }
                    numBytes = 0;
                }

                // Webservice method call
                String retString = proxy.Commit();

                /* --------------------------- one call method -------------------------- */
                // Read rest of file data
                retString = String.Empty;
                if (numBytes > 0)
                {
                    data = rdr.ReadBytes((int)numBytes);
                    count++;
                    retString = proxy.Submit(m_ImageName, m_ImageName, "Image", (float)11.2345, (float)48.9876, (float)333.33, "Wolfgang Reichert", lastWriteTime, 374, data); // IDs 372, 373, 374
                }

                // ---------------------------------------------------------------------- */

                TimeSpan dif = DateTime.Now - start;

                if (retString.StartsWith("http"))
                {
                    textBox1.Text = retString;
                    label2.Text = dif.ToString() + " msec  -  " + count.ToString() + " packets transmitted";
                }
                else
                {
                    textBox1.Text = "ERROR (see below...)";
                    label2.Text = retString;
                }
                // Close reader and stream
                rdr.Close();
                fileStrm.Close();
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;

                // Close reader and stream if open
                if (rdr != null)
                    rdr.Close();
                if (fileStrm != null)
                    fileStrm.Close();
            }
            finally
            {
                // Abort faulted proxy
                if (proxy.State == System.ServiceModel.CommunicationState.Faulted)
                {
                    // Webservice method call
                    // proxy.Rollback();
                    proxy.Abort();
                }
                // Close proxy
                else if (proxy.State == System.ServiceModel.CommunicationState.Opened)
                {
                    proxy.Close();
                }
            }
            return retString;
        }

       
        #region Actions

        //Überträgt die Taxa einer taxonomischen Gruppe vom Repository in die mobile Datenbank. Um doppelte Einträge zu vermeidenm werden zunächst Einträge in der Mobilen Datenbank
        //gelöscht.
        public int updateTaxa(string[] sourceTables, string targetTable, DbConnection connRepository, SqlCeConnection connMobile)
        {
            //Neue Taxa holen
            List<TaxonNames> taxa = new List<TaxonNames>();
            this.setProgressValue(0);
            int index = 0;
            StringBuilder sb = new StringBuilder();

            foreach (String sourceTable in sourceTables)
            {
                connRepository.Open();
                this.setActionInformation("Get TaxonNames from Source-Table: " + sourceTable);
                DbCommand com = connRepository.CreateCommand();
                sb = new StringBuilder();
                sb.Append("Select NameURI,TaxonNameCache,Synonymy,Family,\"Order\" From ").Append(sourceTable);
                com.CommandText = sb.ToString();
                DbDataReader reader = null;
                try
                {
                    reader = com.ExecuteReader();
                }
                catch (Exception e)
                {
                    this.setProgressInformation("Exception while updating TaxonNames from: " + sourceTable);
                    connRepository.Close();
                    return -1;
                }

                while (reader.Read())
                {
                    TaxonNames taxon = new TaxonNames();
                    if (reader.IsDBNull(0) == false)
                        taxon.NameURI = reader.GetString(0);
                    else
                        taxon.NameURI = String.Empty;
                    if (reader.IsDBNull(1) == false)
                        taxon.TaxonNameCache = reader.GetString(1);
                    if (reader.IsDBNull(2) == false)
                        taxon.Synonymy = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        taxon.Family = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        taxon.Order = reader.GetString(4);
                    taxa.Add(taxon);
                    index++;
                    this.setProgressInformation(index + " items already read");
                }
                connRepository.Close();
            }
            this.setProgressValue(40);

            connRepository.Close();

            connMobile.Open();
            SqlCeTransaction trans = connMobile.BeginTransaction();
            //Alte Taxa löschen
            this.setActionInformation("Delete old TaxonNames from Target-Table: " + targetTable);
            this.setProgressInformation("");

            DbCommand commandMobile = connMobile.CreateCommand();
            sb = new StringBuilder();
            sb.Append("Delete From ").Append(targetTable);
            commandMobile.CommandText = sb.ToString();
            commandMobile.ExecuteNonQuery();

            this.setProgressValue(50);

            //Taxa eintragen
            this.setActionInformation("Save new TaxonNames in Target-Table: " + targetTable);
            index = 0;
            double part = ((double)50 / (double)taxa.Count);

            foreach (TaxonNames taxon in taxa)
            {
                sb = new StringBuilder(); //Alternativ mobileDBSerializer.Connector.Save(taxon)
                sb.Append("Insert INTO ").Append(targetTable).Append(" (NameURI,TaxonNameCache,Synonymy,Family,\"Order\") VALUES (");
                sb.Append(SqlUtil.SqlConvert(taxon.NameURI)).Append(",");
                sb.Append(SqlUtil.SqlConvert(taxon.TaxonNameCache)).Append(",").Append(SqlUtil.SqlConvert(taxon.Synonymy)).Append(",").Append(SqlUtil.SqlConvert(taxon.Family)).Append(",").Append(SqlUtil.SqlConvert(taxon.Order)).Append(")");
                DbCommand insert = connMobile.CreateCommand();
                insert.CommandText = @sb.ToString();
                insert.ExecuteNonQuery();

                index++;
                // Set ProgressInformation
                this.setProgressInformation(index + " of " + taxa.Count + " items already saved");
                int offset = Convert.ToInt32(index * part);
                this.setProgressValue(50 + offset);
            }
            trans.Commit();
            connMobile.Close();

            this.setActionInformation("Update TaxonNames");

            return taxa.Count;
        }
        //Überträgt die Namen von Properties vom Repository in die mobile Datenbank. Um doppelte Einträge zu vermeiden werden zunächst Einträge in der Mobilen Datenbank
        //gelöscht. Kann mit Reflexion verallgemeinert und mit updateTaxonNames kombiniert werden.
        public int updateProperties(string sourceTable, string targetTable, Serializer taxonrepSerializer, Serializer namesSerializer)
        {
            if (MappingDictionary.Mapping.ContainsKey(typeof(PropertyNames)))
                MappingDictionary.Mapping[typeof(PropertyNames)] = sourceTable;
            else
                MappingDictionary.Mapping.Add(typeof(PropertyNames), sourceTable);
            //Neue Properties holen
            IList<PropertyNames> properties = new List<PropertyNames>();
            this.setProgressValue(0);
            this.setActionInformation("Get PropertyNames from Source-Table: " + sourceTable);
            //IRestriction r = RestrictionFactory.TypeRestriction(typeof(PropertyNames));
            //properties = taxonrepSerializer.Connector.LoadList<PropertyNames>(r);//geht nicht , weil auf der Sicht keine GUID definiert ist
            DbConnection connRepository = taxonrepSerializer.CreateConnection();
            DbCommand com = connRepository.CreateCommand();
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * From ").Append(sourceTable);
            com.CommandText = sb.ToString();
            connRepository.Open();
            DbDataReader reader = null;
            try
            {
                reader = com.ExecuteReader();
            }
            catch (Exception e)
            {
                this.setProgressInformation("Exception while updating PropertyNames from: " + sourceTable);
                connRepository.Close();
                return -1;
            }

            int index = 0;//Index kann über Serializer nicht abgefragt werden

            while (reader.Read())
            {
                PropertyNames prop = new PropertyNames();
                prop.PropertyID = reader.GetInt32(0);
                if (!reader.IsDBNull(1))
                    prop.PropertyURI = reader.GetString(1);
                if (!reader.IsDBNull(2))
                    prop.DisplayText = reader.GetString(2);
                if (!reader.IsDBNull(3))
                    prop.HierarchyCache = reader.GetString(3);
                //primary key
                prop.TermID = reader.GetInt32(4);
                if (!reader.IsDBNull(5))
                    prop.BroaderTermID = reader.GetInt32(5);
                properties.Add(prop);
                index++;
                this.setProgressInformation(index + " items already read");
            }
            this.setProgressValue(40);
            connRepository.Close();
            DbConnection connMobile = namesSerializer.CreateConnection();
            connMobile.Open();
            SqlCeTransaction trans = (SqlCeTransaction)connMobile.BeginTransaction();
            //Alte Taxa löschen
            this.setActionInformation("Delete old PropertyNames from Target-Table: " + targetTable);
            this.setProgressInformation("");

            DbCommand commandMobile = connMobile.CreateCommand();
            sb = new StringBuilder();
            sb.Append("Delete From ").Append(targetTable);
            commandMobile.CommandText = sb.ToString();
            commandMobile.ExecuteNonQuery();
            this.setProgressValue(50);

            //Taxa eintragen
            this.setActionInformation("Save new PropertyNames in Target-Table: " + targetTable);
            index = 0;
            double part = ((double)50 / (double)properties.Count);

            foreach (PropertyNames prop in properties)
            {
                sb = new StringBuilder(); //Alternativ mobileDBSerializer.Connector.Save(taxon)
                sb.Append("Insert INTO ").Append(targetTable).Append(" (PropertyID,PropertyURI,DisplayText,HierarchyCache,TermID,BroaderTermID) VALUES (");
                sb.Append(SqlUtil.SqlConvert(prop.PropertyID)).Append(",");
                sb.Append(SqlUtil.SqlConvert(prop.PropertyURI)).Append(",").Append(SqlUtil.SqlConvert(prop.DisplayText)).Append(",").Append(SqlUtil.SqlConvert(prop.HierarchyCache)).Append(",").Append(prop.TermID).Append(",").Append(prop.BroaderTermID).Append(")");
                DbCommand insert = connMobile.CreateCommand();
                insert.CommandText = @sb.ToString();
                insert.ExecuteNonQuery();

                index++;
                // Set ProgressInformation
                this.setProgressInformation(index + " of " + properties.Count + " items already saved");
                int offset = Convert.ToInt32(index * part);
                this.setProgressValue(50 + offset);
            }
            trans.Commit();
            connMobile.Close();
            this.setActionInformation("Update PropertyNames");
            return properties.Count;
        }

        private void checkConnections()
        {
            if (mobileConnected == false || repositoryConnected == false)
            {
                enableDisableActionButtons(false);
                buttonChooseProject.Enabled = false;
                this.labelUserName.Text = String.Empty;
                this.labelCurrentProject.Text = String.Empty;
                this.labelProjectName.Text = String.Empty;
                return;
            }
            try
            {
                //Prüfen ob ein UserProfile zum LoginNamen existiert.
                this.startMobileConnection(true);
                IList<UserProfile> profiles = new List<UserProfile>();
                IRestriction r = RestrictionFactory.Eq(typeof(UserProfile), "_LoginName", loginName);
                //IRestriction r = RestrictionFactory.Eq(typeof(UserProfile), "_LoginName", "Simmel");
                profiles = mobileDBSerializer.Connector.LoadList<UserProfile>(r);
                //Es existiert noch kein Profil
                if (profiles.Count == 0)
                {
                    MessageBox.Show("There is no User Profile assigend to your Login. New profile will be created!");
                    //->neues Profil erstellen
                    if (createProfile() == true)
                        enableDisableActionButtons(true);
                }
                else
                {
                    //Möglich ist hier auf Aktualität zu prüfen
                    this.labelUserName.Text = profiles[0].CombinedNameCache;
                    this.labelCurrentProject.Text = "Project: " + profiles[0].ProjectID.ToString();
                    this.projectID = (int)profiles[0].ProjectID;
                    this.labelProjectName.Text = this.getProjectName((int)profiles[0].ProjectID);
                    enableDisableActionButtons(true);
                }
                this.endMobileConnection(true);
                buttonChooseProject.Enabled = true;
            }
            catch (SerializerException ex)
            {
                MessageBox.Show("The DB-Schema of the MobileDb.sdf is not actual: " + ex.Message);
            }
        }

        private bool createProfile()
        {
            UserProfile profile;
            profile = mobileDBSerializer.CreateISerializableObject<UserProfile>();
            UserProxy proxy;
            //Zuerst korrespondierenden Userproxy holen
            //IRestriction r = RestrictionFactory.Eq(typeof(UserProxy), "_LoginName", "Simmel");
            IRestriction r = RestrictionFactory.Eq(typeof(UserProxy), "_LoginName", loginName);
            proxy = repositorySerializer.Connector.Load<UserProxy>(r);
            //Projekt holen

            //1.Projekte zum User Holen
            IList<int> projectNumbers = new List<int>();
            projectNumbers = repositorySerializer.Connector.selectWhatFromWhere<int>("ProjectID", "ProjectUser", "LoginName", loginName);
            //projectNumbers = repositorySerializer.Connector.selectWhatFromWhere<int>("ProjectID", "ProjectUser", "LoginName", "SchubertKonstanze");
            //2.Beschreibungen zum Projekt holen
            Dictionary<int, String> projects = new Dictionary<int, string>();
            foreach (int number in projectNumbers)
            {
                String project = repositorySerializer.Connector.selectWhatFromWhere<String>("Project", "ProjectProxy", "ProjectID", number.ToString())[0];
                projects.Add(number, project);
            }
            //3.Projekte auswählen lassen
            if (projects.Count > 0)
            {
                FormSelectProject sp = new FormSelectProject(projects);
                if (sp.ShowDialog() == DialogResult.OK)
                {
                    profile.ProjectID = sp.projectID;
                    profile.ProjectName = sp.projectName;
                }
                else
                    return false;

                //profile.LoginName = "SchubertKonstanze";
                profile.LoginName = loginName;
                profile.CombinedNameCache = proxy.CombinedNameCache;
                profile.AgentURI = proxy.AgentURI;
                mobileDBSerializer.Connector.Save(profile);
            }

            else
            {
                MessageBox.Show("You`re not assigned to a Project. Please contact Markus Weiss to assign You to a project.");
                return false;
            }
            this.labelUserName.Text = profile.CombinedNameCache;
            this.labelCurrentProject.Text = "Project: " + profile.ProjectID.ToString();
            this.projectID = (int)profile.ProjectID;
            this.labelProjectName.Text = this.getProjectName((int)profile.ProjectID);
            return true;
        }

        private void mobileRapiDesktop()
        {
            if (rapi.DevicePresent == true)
            {
                try
                {
                    rapi.Connect();
                    Cursor.Current = Cursors.WaitCursor;
                    rapi.CopyFileFromDevice(progPath, mobileDBPath, true);//Datenbank in das ausführende Verzeichnis als ArbeitsDB kopieren
                    String[] pictures = System.IO.Directory.GetFiles(mobilePicturePath);
                    foreach (String pic in pictures)
                    {
                        String sourcPath = mobilePicturePath + pic;
                        String destPath = progPicturePath + pic;
                        rapi.CopyFileFromDevice(destPath, sourcPath, false);
                    }
                    activesync = true;
                    Cursor.Current = Cursors.Default;
                }
                catch (Exception e)
                {
                    MessageBox.Show("RapiError! Please copy Your Files manually or check ActiveSyncOptions");
                    activesync = false;
                }
            }
            else
            {
                activesync = false;
            }
        }

        private void MobileToDesktop(Object o)//überarbeiten
        {
            AutoResetEvent evnt = (AutoResetEvent)o;
            RAPI rap;
            //try
            //{
            rap = new RAPI();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("Unable to initialize Rapi! Please Connect manually.");
            //    return;
            //}
            if (rap.DevicePresent == true)
            {
                try
                {
                    rap.Connect();
                    rap.CopyFileFromDevice(progPath, mobileDBPath, true);//Datenbank in das ausführende Verzeichnis als ArbeitsDB kopieren
                    activesync = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("RapiError! Please copy Your Files manually");
                    activesync = false;
                }
            }
            else
            {
                activesync = false;
            }
            evnt.Set();
        }

        private bool startMobileConnection(bool copy)
        {
            try
            {
                // copy mobile database to work directory?
                if (copy)
                {
                    if (activesync == true)
                    {
                        if (rapi.DevicePresent && rapi.Connected)
                            this.mobileRapiDesktop();
                        else
                            return false;
                    }
                    else
                    {
                        if (System.IO.File.Exists(mobileDBPath))
                        {
                            System.IO.File.Copy(mobileDBPath, progPath, true);
                            if (Directory.Exists(mobilePicturePath))
                            {
                                String[] pictures = System.IO.Directory.GetFiles(mobilePicturePath);
                                foreach (String pic in pictures)
                                {
                                    String fileName = String.Empty;
                                    String destPath = String.Empty;
                                    try
                                    {
                                        String sourcPath = pic;
                                        fileName = pic.Replace(mobilePicturePath, "");
                                        destPath = progPicturePath + fileName;
                                        File.Copy(sourcPath, destPath, false);
                                    }
                                    catch (Exception)
                                    {
                                        if (!File.Exists(destPath))
                                            MessageBox.Show("Unable to copy the following picture:" + fileName);
                                    }
                                }
                            }
                        }
                        else
                            return false;
                    }
                }

                mobileDBSerializer = new MS_SqlCeSerializer(progPath);
                mobileDBSerializer.RegisterTypes(divMobiTypes);
                mobileDBSerializer.RegisterType(typeof(UserProfile));
                mobileDBSerializer.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The Database could not be connected. Perhaps the DB is in not a DiversityMobile DB.");
                MessageBox.Show("startMobileConnection: " + ex.Message);
                if (ex.InnerException != null)
                    MessageBox.Show("startMobileConnection Inner Exception: " + ex.InnerException.Message);
                mobileDBSerializer = null;
                mobileDBPath = null;
                this.labelConnectionMobile.Text = "Not Connected to MobileDB";
                return false;
            }

            return true;
        }

        private bool endMobileConnection(bool save)
        {

            mobileDBSerializer.Dispose();
            try
            {
                // save mobile database from work directory to mobile device?
                if (save)
                {
                    //  save database to mobile
                    if (activesync == true)
                    {
                        if (rapi.Connected)
                            rapi.CopyFileToDevice(progPath, mobileDBPath, true);
                        else
                            MessageBox.Show("Database cannot be saved to mobile device. Mobile device is not connected!");
                    }
                    else
                        System.IO.File.Copy(progPath, mobileDBPath, true);
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("CloseConnection: " + f.Message);
                MessageBox.Show("CloseConnection: " + f.InnerException.Message);
            }
            return true;
        }

        private void getDefinitionsListSynchronization()
        {
            if (this.startMobileConnection(true))
            {
                this.setProgressInformation("Create Mobile Connection");
                ObjectSyncList syncList = new ObjectSyncList();
                this.setProgressValue(10);
                this.setProgressInformation("Read Definitions from Database");
                //String sql = @"SELECT * FROM [DiversityCollection_Monitoring].[dbo].[AnalysisProjectList] (372)";
                String sql = @"SELECT * FROM [" + this.InitialCatalog + "].[dbo].[AnalysisProjectList] (" + this.projectID + ")";
                IList<ISerializableObject> list = repositorySerializer.Connector.LoadList(typeof(Analysis), sql);
                syncList.addList(list);
                //sql = @"SELECT AnalysisID,TaxonomicGroup FROM [DiversityCollection_Monitoring].[dbo].[AnalysisTaxonomicGroupForProject] (372)";
                sql = @"SELECT AnalysisID,TaxonomicGroup,RowGUID FROM [" + this.InitialCatalog + "].[dbo].[AnalysisTaxonomicGroupForProject] (" + this.projectID + ")";
                IList<AnalysisTaxonomicGroup> atgList = new List<AnalysisTaxonomicGroup>();
                DbConnection connRepository = this.repositorySerializer.CreateConnection();
                connRepository.Open();
                DbCommand com = connRepository.CreateCommand();
                com.CommandText = sql;
                DbDataReader reader = null;
                try
                {
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        AnalysisTaxonomicGroup atg = new AnalysisTaxonomicGroup();
                        atg.AnalysisID = reader.GetInt32(0);
                        atg.TaxonomicGroup = reader.GetString(1);
                        atg.Rowguid = reader.GetGuid(2);
                        atgList.Add(atg);
                    }
                    connRepository.Close();
                }
                catch (Exception e)
                {
                    this.setProgressInformation("Exception while updating AnalysisTaxonomicGroups");
                    connRepository.Close();
                }


                foreach (AnalysisTaxonomicGroup atg in atgList)
                {
                    foreach (ISerializableObject iso in list)
                    {
                        bool idFound = false;
                        if (iso.GetType().Equals(typeof(Analysis)))
                        {
                            Analysis ana = (Analysis)iso;
                            if (ana.AnalysisID == atg.AnalysisID)
                            {
                                idFound = true;
                            }
                        }
                        if (idFound == true)
                        {
                            syncList.addObject(atg);
                            break;
                        }
                    }
                }


                foreach (AnalysisTaxonomicGroup atg in atgList)
                {
                    foreach (ISerializableObject iso in list)
                    {
                        bool idFound = false;
                        if (iso.GetType().Equals(typeof(Analysis)))
                        {
                            Analysis ana = (Analysis)iso;
                            if (ana.AnalysisID == atg.AnalysisID)
                            {
                                idFound = true;
                            }
                        }
                        if (idFound == true)
                        {
                            syncList.addObject(atg);
                            break;
                        }
                    }
                }
                this.setProgressValue(20);
                syncList.Load(typeof(Property), repositorySerializer);
                syncList.Load(typeof(CollEventImageType_Enum), repositorySerializer);
                syncList.Load(typeof(CollSpecimenImageType_Enum), repositorySerializer);
                syncList.Load(typeof(CollTaxonomicGroup_Enum), repositorySerializer);
                syncList.Load(typeof(LocalisationSystem), repositorySerializer);
                syncList.Load(typeof(CollCircumstances_Enum), repositorySerializer);//
                syncList.Load(typeof(CollUnitRelationType_Enum), repositorySerializer);
                //poolBuilder.Load(typeof(CollEventSeriesImageType_Enum));
                syncList.Load(typeof(CollIdentificationCategory_Enum), repositorySerializer);
                //poolBuilder.Load(typeof(CollTypeStatus_Enum));
                //__poolBuilder.Load(typeof(CollIdentificationQualifier_Enum));
                //poolBuilder.Load(typeof(CollLabelTranscriptionState_Enum));
                //poolBuilder.Load(typeof(CollLabelType_Enum));
                //poolBuilder.Load(typeof(CollMaterialCategory_Enum));
                syncList.initialize(LookupSynchronizationInformation.downloadDefinitionsList(), LookupSynchronizationInformation.getReflexiveReferences(), LookupSynchronizationInformation.getReflexiveIDFields());
                AnalyzeSyncObjectList anal = new AnalyzeSyncObjectList(syncList, repositorySerializer, mobileDBSerializer, syncDBSerializer);
                anal.analyzeAll();
                //Im Moment ist noch kein ConflictHandling möglich (und auch nicht nötig)
                this.setProgressInformation("Synchronizing");
                this.setProgressValue(50);
                //Für debugging
                IRestriction rs = RestrictionFactory.TypeRestriction(typeof(SyncItem));
                IList<ISerializableObject> sync = syncDBSerializer.Connector.LoadList(typeof(SyncItem), rs);
                this.setProgressInformation("Synchronized: " + sync.Count.ToString());

                anal.synchronizeAll();
                this.setProgressInformation("Synchronization Complete");
                this.setProgressValue(100);
                if (this.endMobileConnection(true))
                {
                    this.buttonGetDefinitions.Enabled = false;
                }
            }
        }
        //private void getDefinitionsFreeSynchronisation()
        //{

        //    if (this.startMobileConnection(true))
        //    {
        //        this.startProgressThread();
        //        this.setProgressValue(0);
        //        this.setActionInformation("Get DiversityCollection Definitions");
        //        FreeSyncPoolBuilder poolBuilder = new FreeSyncPoolBuilder(repositorySerializer, syncDBSerializer);
        //        ObjectSyncList syncList = new ObjectSyncList();
        //        //***Analysis ist ein Spezialfall, da nur projektspezifische Daten übertragen werden sollen
        //        //Ermittle alle Projekte aus Userprofilen, die auf dem Handy gespeichert sind
        //        DbConnection mobConn = mobileDBSerializer.CreateConnection();
        //        mobConn.Open();
        //        DbCommand com = mobConn.CreateCommand();
        //        com.CommandText = "Select ProjectID From UserProfile";
        //        DbDataReader reader = com.ExecuteReader();
        //        List<int> projects = new List<int>();
        //        List<int> analyses = new List<int>();
        //        while (reader.Read())
        //        {
        //            projects.Add(reader.GetInt32(0));
        //        }

        //        foreach (int project in projects)
        //        {
        //            String s = @"SELECT * FROM [" + this.InitialCatalog + "].[dbo].[AnalysisProjectList] (" + project + ")";
        //            IList<ISerializableObject> list = repositorySerializer.Connector.LoadList(typeof(Analysis), s);
        //            poolBuilder.Add(list);

        //            //*****Es sollen nur Einträge in AnalysisTaxonomicGroup, wenn die Analyse vorhanden ist
        //            foreach (ISerializableObject iso in list)
        //            {
        //                Analysis ana = (Analysis)iso;
        //                IRestriction rtg = RestrictionFactory.Eq(typeof(AnalysisTaxonomicGroup), "_AnalysisID", ana.AnalysisID);
        //                IList<ISerializableObject> atg = repositorySerializer.Connector.LoadList(typeof(AnalysisTaxonomicGroup), rtg);
        //                poolBuilder.Add(atg);
        //            }

        //        }
        //        mobConn.Close();

        //        this.setProgressValue(50);
        //        this.setProgressInformation("Get UserProfile projects: Finished");

        //        //poolBuilder.Load(typeof(Collection));
        //        poolBuilder.Load(typeof(Property));
        //        poolBuilder.Load(typeof(CollEventImageType_Enum));
        //        poolBuilder.Load(typeof(CollSpecimenImageType_Enum));
        //        poolBuilder.Load(typeof(CollTaxonomicGroup_Enum));
        //        poolBuilder.Load(typeof(LocalisationSystem));
        //        poolBuilder.Load(typeof(CollCircumstances_Enum));//
        //        poolBuilder.Load(typeof(CollUnitRelationType_Enum));
        //        //poolBuilder.Load(typeof(CollEventSeriesImageType_Enum));
        //        poolBuilder.Load(typeof(CollIdentificationCategory_Enum));
        //        //__poolBuilder.Load(typeof(CollIdentificationQualifier_Enum));
        //        this.mobileDBSyncTool = new UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool(mobileDBSerializer, syncDBSerializer, divMobiTypes);

        //        this.setProgressInformation("Synchronizing: Start");

        //        this.SyncPool = poolBuilder.buildPoolFree();
        //        this.mobileDBSyncTool.Analyze(SyncPool);
        //        //Dient der Initialisierung Konflikte werden überschrieben
        //        this.buttonGetDefinitions.Text = "Synchronizing";
        //        //Veränderungen an den definierenden KLassen werden überschrieben
        //        MessageBox.Show("Your changes in defining data items will be overwritten");

        //        this.buttonGetDefinitions.Text = "Synchronizing";
        //        List<SyncContainer> conflicted;
        //        conflicted = (List<SyncContainer>)SyncPool.GetSyncContainer<ConflictState>();
        //        List<SyncContainer> insert = (List<SyncContainer>)SyncPool.GetSyncContainer<InsertState>();
        //        List<SyncContainer> deleted = (List<SyncContainer>)SyncPool.GetSyncContainer<DeletedState>();

        //        IRestriction rs = RestrictionFactory.TypeRestriction(typeof(SyncItem));
        //        IList<ISerializableObject> sync = syncDBSerializer.Connector.LoadList(typeof(SyncItem), rs);

        //        this.setProgressInformation("Sync: " + sync.Count.ToString() + ", insert: " + insert.Count.ToString());

        //        //MessageBox.Show(sync.Count.ToString());
        //        //MessageBox.Show(insert.Count.ToString());

        //        foreach (SyncContainer sc in conflicted)
        //        {
        //            sc.Resolve(sc.SourceObject);
        //        }
        //        mobileDBSyncTool.Synchronize(SyncPool);
        //        this.setProgressInformation("Synchronizing: Finished");
        //        this.setProgressValue(90);
        //        this.buttonGetDefinitions.Text = "Synchronization Finished";


        //        //Alte Datenbank überschreiben
        //        //mobileDBSerializer.Dispose();
        //        //if (activesync == true)
        //        //    rapi.CopyFileToDevice(progPath, mobileDBPath, true);
        //        //else
        //        //    System.IO.File.Copy(progPath, mobileDBPath, true);

        //        if (this.endMobileConnection(true))
        //        {
        //            this.buttonGetDefinitions.Text = "Finished";
        //            this.buttonGetDefinitions.Enabled = false;
        //        }
        //        this.setProgressValue(100);
        //        this.endProgressThread(false);
        //    }
        //}
        private void uploadListSynchronizsation()
        {
            if (this.startMobileConnection(false))
            {
                this.setProgressInformation("Connecting ...");
                ObjectSyncList syncList = new ObjectSyncList();
                List<Type> uploadTypes = new List<Type>();
                //uploadTypes.Add(typeof(Analysis));//ANalysen müssen aufgrund der referenzen gesondert behandelt werden.
                uploadTypes.Add(typeof(CollectionAgent));
                uploadTypes.Add(typeof(CollectionEvent));
                uploadTypes.Add(typeof(CollectionEventImage));
                uploadTypes.Add(typeof(CollectionEventLocalisation));
                uploadTypes.Add(typeof(CollectionEventProperty));
                uploadTypes.Add(typeof(CollectionSpecimen));
                uploadTypes.Add(typeof(CollectionSpecimenImage));
                uploadTypes.Add(typeof(Identification));
                uploadTypes.Add(typeof(IdentificationUnit));
                uploadTypes.Add(typeof(IdentificationUnitAnalysis));
                uploadTypes.Add(typeof(IdentificationUnitGeoAnalysis));
                uploadTypes.Add(typeof(CollectionEventSeries));
                uploadTypes.Add(typeof(CollectionProject));
                //Bis hier: Korrepondiert zu DBVersion 31
                foreach (Type t in uploadTypes)
                {
                    syncList.Load(t, mobileDBSerializer);
                }
                this.setProgressValue(10);
                this.setProgressInformation("Initializing");
                syncList.initialize(LookupSynchronizationInformation.getFieldDataList(), LookupSynchronizationInformation.getReflexiveReferences(), LookupSynchronizationInformation.getReflexiveIDFields());
                this.setProgressInformation("Analyzing");
                AnalyzeSyncObjectList ansl = new AnalyzeSyncObjectList(syncList, mobileDBSerializer, repositorySerializer, syncDBSerializer);
                ansl.analyzeAll();
                this.setProgressValue(30);
                this.setProgressInformation("Analysis Complete");
                //Alles außer InsertState auf ignore setzen
                List<ListContainer> conflicted;
                List<ListContainer> conflictResolved;
                List<ListContainer> synchronized;
                List<ListContainer> insert;
                List<ListContainer> update;
                List<ListContainer> ignore;
                List<ListContainer> delete;
                List<ListContainer> premature;
                conflicted = ansl.getObjectsOfState(SyncStates_Enum.ConflictState);
                conflictResolved = ansl.getObjectsOfState(SyncStates_Enum.ConflictResolvedState);
                synchronized = ansl.getObjectsOfState(SyncStates_Enum.SynchronizedState);
                insert = ansl.getObjectsOfState(SyncStates_Enum.InsertState);
                update = ansl.getObjectsOfState(SyncStates_Enum.UpdateState);
                ignore = ansl.getObjectsOfState(SyncStates_Enum.IgnoreState);
                delete = ansl.getObjectsOfState(SyncStates_Enum.DeletedState);
                premature = ansl.getObjectsOfState(SyncStates_Enum.PrematureState);
                this.setProgressInformation("Warning: Only InsertState is allowed at the moment. All other states will be set to IgnoreState");
                System.Threading.Thread.Sleep(1000);
                this.setProgressValue(50);
                this.setProgressInformation("Synchronising");
                foreach (ListContainer lc in conflicted)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                foreach (ListContainer lc in conflictResolved)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                foreach (ListContainer lc in synchronized)
                {
                    lc.State = SyncStates_Enum.IgnoreState;//Exception throwen?
                }
                foreach (ListContainer lc in update)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                foreach (ListContainer lc in delete)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                foreach (ListContainer lc in premature)
                {
                    lc.State = SyncStates_Enum.IgnoreState;//Exception throwen?
                }
                ansl.synchronizeAll();
                this.setProgressInformation("Complete");
                this.setProgressValue(100);
                buttonUpload.Enabled = false;
                this.endMobileConnection(false);
            }
        }
        //private void uploadFreeSynchronization()
        //{
        //    if (this.startMobileConnection(true))
        //    {
        //        buttonUpload.Text = "Analyzing";
        //        this.mobileDBSyncTool = new UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool(mobileDBSerializer, syncDBSerializer, divMobiTypes);
        //        this.repositorySyncTool = new UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool(repositorySerializer, syncDBSerializer, divMobiTypes);
        //        this.SyncPool = mobileDBSyncTool.CreateSyncPool();
        //        this.repositorySyncTool.Analyze(this.SyncPool);
        //        buttonUpload.Text = "Analysis Complete";
        //        //Alles außer InsertState auf ignore setzen
        //        List<SyncContainer> conflicted;
        //        List<SyncContainer> conflictResolved;
        //        List<SyncContainer> synchronized;
        //        List<SyncContainer> insert;
        //        List<SyncContainer> update;
        //        List<SyncContainer> ignore;
        //        List<SyncContainer> truncate;
        //        List<SyncContainer> delete;
        //        List<SyncContainer> premature;
        //        conflicted = (List<SyncContainer>)SyncPool.GetSyncContainer<ConflictState>();
        //        conflictResolved = (List<SyncContainer>)SyncPool.GetSyncContainer<ConflictResolvedState>();
        //        synchronized = (List<SyncContainer>)SyncPool.GetSyncContainer<SynchronizedState>();
        //        insert = (List<SyncContainer>)SyncPool.GetSyncContainer<InsertState>();
        //        update = (List<SyncContainer>)SyncPool.GetSyncContainer<UpdateState>();
        //        ignore = (List<SyncContainer>)SyncPool.GetSyncContainer<IgnoreState>();
        //        truncate = (List<SyncContainer>)SyncPool.GetSyncContainer<TruncateState>();
        //        delete = (List<SyncContainer>)SyncPool.GetSyncContainer<DeletedState>();
        //        premature = (List<SyncContainer>)SyncPool.GetSyncContainer<PrematureState>();
        //        MessageBox.Show("Warning: Only InsertState is allowed at the moment. All other states will be set to IgnoreState");
        //        buttonUpload.Text = "Synchronizing";
        //        foreach (SyncContainer sc in conflicted)
        //        {
        //            sc.Ignore();
        //        }
        //        foreach (SyncContainer sc in conflictResolved)
        //        {
        //            sc.Ignore();
        //        }
        //        foreach (SyncContainer sc in synchronized)
        //        {
        //            sc.Ignore();
        //        }
        //        foreach (SyncContainer sc in update)
        //        {
        //            sc.Ignore();
        //        }
        //        foreach (SyncContainer sc in truncate)
        //        {
        //            sc.Ignore();
        //        }
        //        foreach (SyncContainer sc in delete)
        //        {
        //            sc.Ignore();
        //        }
        //        foreach (SyncContainer sc in premature)
        //        {
        //            sc.Ignore();
        //        }
        //        repositorySyncTool.Synchronize(this.SyncPool);
        //        buttonUpload.Text = "Complete";
        //        buttonUpload.Enabled = false;
        //        this.endMobileConnection(false);
        //    }
        //}
        private void getFieldDataListSynchronization(List<ISerializableObject> list)
        {
            if (this.startMobileConnection(true))
            {
                ObjectSyncList syncList = new ObjectSyncList();
                syncList.addList(list);
                this.setProgressInformation("Initializing");
                syncList.initialize(LookupSynchronizationInformation.getFieldDataList(), LookupSynchronizationInformation.getReflexiveReferences(), LookupSynchronizationInformation.getReflexiveIDFields());
                this.setProgressValue(10);
                this.setProgressInformation("Analyzing");
                AnalyzeSyncObjectList ansl = new AnalyzeSyncObjectList(syncList, repositorySerializer, mobileDBSerializer, syncDBSerializer);
                ansl.analyzeAll();
                this.setProgressInformation("Analysis Complete");
                this.setProgressValue(20);
                //Alles außer InsertState auf ignore setzen
                List<ListContainer> conflicted;
                List<ListContainer> conflictResolved;
                List<ListContainer> synchronized;
                List<ListContainer> insert;
                List<ListContainer> update;
                List<ListContainer> ignore;
                List<ListContainer> delete;
                List<ListContainer> premature;
                conflicted = ansl.getObjectsOfState(SyncStates_Enum.ConflictState);
                conflictResolved = ansl.getObjectsOfState(SyncStates_Enum.ConflictResolvedState);
                synchronized = ansl.getObjectsOfState(SyncStates_Enum.SynchronizedState);
                insert = ansl.getObjectsOfState(SyncStates_Enum.InsertState);
                update = ansl.getObjectsOfState(SyncStates_Enum.UpdateState);
                ignore = ansl.getObjectsOfState(SyncStates_Enum.IgnoreState);
                delete = ansl.getObjectsOfState(SyncStates_Enum.DeletedState);
                premature = ansl.getObjectsOfState(SyncStates_Enum.PrematureState);

                this.setProgressValue(30);
                this.setProgressInformation("Warning: Only InsertState is allowed at the moment. All other states will be set to IgnoreState");
                System.Threading.Thread.Sleep(3000);
                this.setProgressInformation("Synchronizing");

                foreach (ListContainer lc in conflicted)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                this.setProgressValue(40);
                foreach (ListContainer lc in conflictResolved)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                this.setProgressValue(50);
                foreach (ListContainer lc in synchronized)
                {
                    lc.State = SyncStates_Enum.IgnoreState;//Exception throwen?
                }
                this.setProgressValue(60);
                foreach (ListContainer lc in update)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                this.setProgressValue(70);
                foreach (ListContainer lc in delete)
                {
                    lc.State = SyncStates_Enum.IgnoreState;
                }
                this.setProgressValue(80);
                foreach (ListContainer lc in premature)
                {
                    lc.State = SyncStates_Enum.IgnoreState;//Exception throwen?
                }
                this.setProgressValue(90);
                ansl.synchronizeAll();
                this.setProgressInformation("Complete");
                this.setProgressValue(100);
                this.endMobileConnection(true);
            }
        }

        #endregion

        #region Events

        private void buttonConnectRepository_Click(object sender, EventArgs e)
        {
            if (repositoryConnected == false)
            {
                if (this.SqlAuth)
                {
                    LoginForm lf;
                    if (loginName != null)
                        lf = new LoginForm(this.HidePW, this.loginName);
                    else
                        lf = new LoginForm(this.HidePW);

                    if (lf.ShowDialog() == DialogResult.OK)
                    {
                        if (!lf.loginName.Equals(loginName))
                        {
                            loginName = lf.loginName;
                            try
                            {
                                this.writeSettingsToXML();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("XML-Write Error. Settings couldn´t be saved");

                            }

                        }
                        password = lf.password;

                        Cursor.Current = Cursors.WaitCursor;
                        repositorySerializer = new MS_SqlServerIPSerializer(loginName, password, this.IPAddress, this.IPPort, this.InitialCatalog,null);
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    repositorySerializer = new MS_SqlServerWASerializier(this.IPAddress + "," + this.IPPort, this.InitialCatalog,"");
                    this.loginName = Environment.UserName;
                    Cursor.Current = Cursors.Default;
                }

                //Verbindung zum Repository herstellen
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    repositorySerializer.RegisterTypes(divMobiTypes);
                    repositorySerializer.RegisterType(typeof(UserProxy));
                    repositorySerializer.Activate();
                    String sync = "Synchronisation_Test";
                    if (this.SqlAuth)
                        syncDBSerializer = new MS_SqlServerIPSerializer(loginName, password, this.IPAddress, this.IPPort, sync,null);
                    else
                        syncDBSerializer = new MS_SqlServerWASerializier(this.IPAddress + "," + this.IPPort, sync,null);

                    String syncIt = "[" + loginName + "].SyncItem";
                    String fieldSt = "[" + loginName + "].FieldState";
                    //String syncIt = "[Simmel].SyncItem";
                    //String fieldSt = "[Simmel].FieldState";
                    MappingDictionary.Mapping.Add(typeof(SyncItem), syncIt);
                    MappingDictionary.Mapping.Add(typeof(FieldState), fieldSt);
                    syncDBSerializer.RegisterType(typeof(SyncItem));
                    syncDBSerializer.RegisterType(typeof(FieldState));
                    syncDBSerializer.Activate();
                    this.labelConnectionRepository.Text = "Connected to " + this.InitialCatalog + " as " + loginName;
                    buttonConnectRepository.Text = "Disconnect";
                    repositoryConnected = true;
                    this.optionsToolStripMenuItem.DropDownItems[0].Enabled = false;
                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection not possible. Please check values!");
                    MappingDictionary.Mapping.Clear();
                    repositoryConnected = false;
                    checkConnections();
                    this.optionsToolStripMenuItem.DropDownItems[0].Enabled = true;
                    return;
                }
            }
            else
            {
                repositorySerializer = null;
                syncDBSerializer = null;
                this.labelConnectionRepository.Text = "Not Connected to SNSB IT Center";
                this.buttonConnectRepository.Text = "Connect to Repository";
                repositoryConnected = false;
                this.optionsToolStripMenuItem.DropDownItems[0].Enabled = true;
            }
            checkConnections();
        }

        private void buttonConnectMobile_Click(object sender, EventArgs e)
        {
            //Mobile DB Verbinden
            //Verbindung über ActiveSync testen.
            if (mobileConnected == false)
            {
                if (this.useDevice)
                {
                    try
                    {
                        this.labelConnectionMobile.Text = "Copying Files to working directory";
                        Cursor.Current = Cursors.WaitCursor;
                        this.mobileRapiDesktop();
                        Cursor.Current = Cursors.Default;
                        ////int time = 30000;
                        //AutoResetEvent evnt = new AutoResetEvent(false);
                        //Thread connect = new Thread(new ParameterizedThreadStart(MobileToDesktop));
                        //evnt.Reset();
                        //connect.Start(evnt);
                        //if (evnt.WaitOne(connectingTimeOut, false))
                        //{
                        //}
                        //else
                        //{
                        //    connect.Abort();
                        //    bool alive = connect.IsAlive;
                        //    ThreadState state = connect.ThreadState;
                        //    throw new Exception();
                        //}
                        //connect.Join();

                    }
                    catch (Exception ex)
                    {
                        activesync = false;
                    }
                    if (activesync == false)
                    {
                        MessageBox.Show("Connection via ActiveSync is not possible. Copy the databases from Your mobile phone to your PC manually or check DiversityMobileLocation in ActiveSync-Options.");
                        mobileDBPath = null;
                        mobileTaxonPath = null;
                        this.labelConnectionMobile.Text = "Connection Failed";
                    }
                    else
                    {
                        this.labelConnectionMobile.Text = "Connected via ActiveSync";
                    }
                }
                else
                    activesync = false;
                try
                {
                    if (activesync == false)
                    {
                        OpenFileDialog dbConnectionDialog = new OpenFileDialog();
                        dbConnectionDialog.Filter = "sdf files (*.sdf)|*.sdf"; //Im Moment wird nur das DB-Format von DiversityMobile unterstützt
                        if (dbConnectionDialog.ShowDialog() == DialogResult.OK)
                        {
                            mobileDBPath = @dbConnectionDialog.FileName;
                            mobilePath = @System.IO.Path.GetDirectoryName(mobileDBPath);
                            //Es wird davon ausgegangen, dass die Taxondatenbank im selben Verzeichnis liegt.
                            mobileTaxonPath = mobilePath + @"\TaxonNames.sdf";
                            mobilePicturePath = mobilePath + @"\pictures\";
                        }
                        else return;
                    }
                }
                catch (Exception f)
                {
                    //Kann eigentlich nix schiefgehen
                    MessageBox.Show("Impossible to Connect Mobile DB. Perhaps the DB is used by another process or the DB is not a DiversityMobile DB.");
                    MessageBox.Show("OpenFileDialog: " + f.Message);
                    if (f.InnerException != null)
                        MessageBox.Show("OpenFileDialog: " + f.InnerException.Message);
                    return;
                }
                // save current settings
                try
                {
                    this.writeSettingsToXML();
                }
                catch (Exception)
                {
                    MessageBox.Show("XML-Write Error1!");
                }
            }
            if (mobileConnected == false)
            {
                try
                {
                    if (this.startMobileConnection(true))//Datenbank in das ausführende Verzeichnis als ArbeitsDB kopieren und Serializer erstellen
                    {
                        buttonConnectMobile.Text = "Disconnect";
                        this.labelConnectionMobile.Text = "Connected to local Copy";
                        mobileConnected = true;
                        this.optionsToolStripMenuItem.DropDownItems[1].Enabled = false;
                        this.endMobileConnection(false);
                    }
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                    if (f.InnerException != null)
                        MessageBox.Show(f.InnerException.Message);
                    MessageBox.Show("The mobile DB could not be openened. Please ensure that no other processes uses the MobileDB", "Mobile DB Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("TestConnection: " + f.Message);
                    if (f.InnerException != null)
                        MessageBox.Show("TestConnection: " + f.InnerException.Message);
                    mobileConnected = false;
                }
                checkConnections();
            }
            else
            {
                buttonConnectMobile.Text = "Connect to Mobile DB";
                this.labelConnectionMobile.Text = "Not Connected to MobileDB";
                this.mobileDBSerializer = null;
                mobileConnected = false;
                this.optionsToolStripMenuItem.DropDownItems[1].Enabled = true;
                checkConnections();
            }
        }

        private void buttonChooseProject_Click(object sender, EventArgs e)
        {

        }

        private void listBoxChooseProject_DoubleClick(object sender, EventArgs e)
        {

        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {

        }

        private void chooseProject()
        {
            String temp = this.listBoxChooseProject.SelectedItem.ToString();

            // extract selected ProjectID
            temp = temp.Remove(temp.IndexOf(':'));
            int proID;
            String proName;
            try
            {
                proID = int.Parse(temp);
                proName = this.getProjectName(proID);
            }
            catch (Exception)
            {
                proID = -1;
                proName = null;
            }

            if (this.startMobileConnection(true) && mobileDBSerializer != null)
            {
                UserProfile profile = new UserProfile();
                IRestriction r = RestrictionFactory.Eq(typeof(UserProfile), "_LoginName", loginName);
                //IRestriction r = RestrictionFactory.Eq(typeof(UserProfile), "_LoginName", "Simmel");
                profile = mobileDBSerializer.Connector.Load<UserProfile>(r);

                if (profile != null)
                {
                    if (proID > 0)
                    {
                        profile.ProjectID = proID;
                        profile.ProjectName = proName;
                        mobileDBSerializer.Connector.Save(profile);
                    }
                }

                this.endMobileConnection(true);

                // change ProjectID and ProjectName on Labels
                if (proID > 0 && proName != null)
                {
                    this.labelCurrentProject.Text = "Project: " + proID;
                    this.labelProjectName.Text = proName;
                }
            }
            this.projectID = proID;
            this.listBoxChooseProject.Visible = false;
            this.buttonChoose.Visible = false;
            this.buttonChooseProject.Enabled = true;
        }

        private void buttonGetTaxa_Click(object sender, EventArgs e)
        {

        }

        private void buttonGetDefinitions_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            // Start Thread for ProgressInformation 
            this.startProgressThread();
            this.setProgressValue(0);
            this.setActionInformation("Upload Field Data");

            uploadListSynchronizsation();

            this.setProgressValue(100);
            this.setProgressInformation("Finished");

            this.endProgressThread(false);
        }

        private void buttonDownLoad_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Visibility

        private void enableDisableActionButtons(bool enabled)
        {
            this.buttonGetTaxa.Enabled = enabled;
            this.buttonGetDefinitions.Enabled = enabled;
            this.buttonUpload.Enabled = enabled;
            this.buttonDownLoad.Enabled = enabled;
        }

        #endregion

        #region Progress Information

        private void startProgressThread()
        {
            this.progressThread = new ProgressThread();
            this.progressThread.MyThread.Start();
            this.setProgressInformation("");
            this.setActionInformation("");
        }

        private void startProgressThread(string description)
        {
            this.progressThread = new ProgressThread();
            this.progressThread.MyThread.Start();
            this.setProgressInformation(description);
            this.setActionInformation("");
        }

        private void setProgressInformation(String additionalInformation)
        {
            if (this.progressThread != null)
            {
                this.progressThread.AdditionalInformation = additionalInformation;
            }
        }
        private void setActionInformation(String actionInformation)
        {
            if (this.progressThread != null)
            {
                this.progressThread.ActionInformation = actionInformation;
                this.setProgressInformation("");
            }
        }

        private void setProgressValue(int value)
        {
            if (this.progressThread != null)
            {
                this.progressThread.Value = value;
            }
        }

        private void endProgressThread(bool cancel)
        {
            if (this.progressThread != null)
            {
                if (cancel)
                {
                    this.setProgressInformation("Excecution was canceled");
                    this.setProgressValue(0);
                }
                else
                {
                    this.setProgressInformation("Complete");
                    this.setProgressValue(100);
                }

                this.progressThread.ShowCloseButton = true;

                this.progressThread.MyThread.Join();
            }
        }

        #endregion

        #region ToolStripMenu
        private void repositoryConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RepositoryOptions optForm = new RepositoryOptions(this.IPAddress, this.IPPort, this.InitialCatalog, this.SqlAuth, this.TaxonNamesInitialCatalog, this.HidePW);
            if (optForm.ShowDialog() == DialogResult.OK)
            {
                this.IPAddress = optForm.IPAddress;
                this.IPPort = optForm.IPPort;
                this.InitialCatalog = optForm.InitialCatalog;
                this.SqlAuth = optForm.SQLAuth;
                this.HidePW = optForm.HidePassword;
                this.TaxonNamesInitialCatalog = optForm.TaxonInitialCatalog;
                try
                {
                    this.writeSettingsToXML();
                }
                catch (Exception)
                {
                    MessageBox.Show("XML-Write Error3!");
                }
                this.buttonConnectRepository.Enabled = true;
            }
        }

        private void activeSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveSyncOptions optForm = new ActiveSyncOptions(this.mobileDBPath, this.mobileTaxonPath, this.connectingTimeOut, this.useDevice);
            if (optForm.ShowDialog() == DialogResult.OK)
            {
                this.mobileDBPath = optForm.MobileDBPath;
                this.mobileTaxonPath = optForm.MobileTaxonPath;
                this.connectingTimeOut = optForm.ConnectingTimeOut;
                this.useDevice = optForm.UseDevice;
                try
                {
                    this.writeSettingsToXML();
                }
                catch (Exception)
                {
                    MessageBox.Show("XML-Write Error4!");
                }
            }
        }

        private void ToolStripMenuItemGoogleMaps_Click(object sender, EventArgs e)
        {
            showGoogleMapForm(true);
        }

        private void ToolStripMenuItemlocalMaps_Click(object sender, EventArgs e)
        {
            showGoogleMapForm(false);
        }

        private void showGoogleMapForm(bool google)
        {
            GoogleMapsForm mapForm = null;

            try
            {
                mapForm = new GoogleMapsForm(this.deviceWidth, this.deviceHeight, google);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            if (this.mobileDBPath != null && !this.mobileDBPath.Equals(String.Empty))
            {
                try
                {
                    DirectoryInfo info = Directory.GetParent(this.mobileDBPath);

                    mapForm.deviceMapDirectory = info.FullName + @"\Maps";
                }
                catch (Exception)
                {
                    mapForm.deviceMapDirectory = @"\Maps";
                }
            }
            else
            {
                mapForm.deviceMapDirectory = @"\Maps";
            }

            mapForm.ShowDialog();

            this.deviceWidth = mapForm.DeviceWidth;
            this.deviceHeight = mapForm.DeviceHeight;
            try
            {
                this.writeSettingsToXML();
            }
            catch (Exception)
            {
                MessageBox.Show("XML-Write Error5!");
            }
        }

        private void finishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Helper Methods

        private String getProjectName(int projectID)
        {

            DbConnection repConn = repositorySerializer.CreateConnection();

            DbCommand com = repConn.CreateCommand();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Project FROM ProjectProxy");
            sb.Append(" WHERE ProjectID=" + projectID);
            com.CommandText = sb.ToString();

            repConn.Open();

            Object ret = null;
            try
            {
                ret = com.ExecuteScalar();
            }
            catch (Exception exception)
            {
                repConn.Close();
                return null;
            }

            repConn.Close();
            repConn.Dispose();

            if (ret != null)
                return ret.ToString();
            else
                return null;
        }

        #endregion

        // Persist User Options in a XML file in the work directory
        #region User Options in XML

        private void writeSettingsToXML()
        {
            String progPath = Environment.CurrentDirectory + @"\Ressources\UserOptions.xml";
            XmlTextWriter writer = new XmlTextWriter(progPath, null);
            try
            {
                // console print out for testing purpose
                // XmlTextWriter writer = new XmlTextWriter(Console.Out);

                writer.Formatting = Formatting.Indented;

                // Starts a new document
                writer.WriteStartDocument();

                //Write comments
                writer.WriteComment("User Options for UserGUI");

                // Add elements to the file
                writer.WriteStartElement("ConnectionOptions", "");

                writer.WriteStartElement("LoginName", "");
                writer.WriteString(this.loginName);
                writer.WriteEndElement();

                writer.WriteStartElement("IPAddress", "");
                writer.WriteString(this.IPAddress);
                writer.WriteEndElement();

                writer.WriteStartElement("IPPort", "");
                writer.WriteString(this.IPPort);
                writer.WriteEndElement();

                writer.WriteStartElement("InitialCatalog", "");
                writer.WriteString(this.InitialCatalog);
                writer.WriteEndElement();

                writer.WriteStartElement("TaxonInitialCatalog", "");
                writer.WriteString(this.TaxonNamesInitialCatalog);
                writer.WriteEndElement();

                writer.WriteStartElement("SQLAuth", "");
                if (this.SqlAuth)
                    writer.WriteString("yes");
                else
                    writer.WriteString("no");
                writer.WriteEndElement();

                writer.WriteStartElement("HidePW", "");
                if (this.HidePW)
                    writer.WriteString("yes");
                else
                    writer.WriteString("no");
                writer.WriteEndElement();


                writer.WriteStartElement("MobilePath", "");
                writer.WriteString(this.mobileDBPath);
                writer.WriteEndElement();

                writer.WriteStartElement("MobileTaxonPath", "");
                writer.WriteString(this.mobileTaxonPath);
                writer.WriteEndElement();

                writer.WriteStartElement("TimeOut", "");
                writer.WriteString(this.connectingTimeOut.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("UseDevice", "");
                if (this.useDevice)
                    writer.WriteString("yes");
                else
                    writer.WriteString("no");
                writer.WriteEndElement();

                writer.WriteStartElement("DeviceWidth", "");
                writer.WriteString(this.deviceWidth.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("DeviceHeight", "");
                writer.WriteString(this.deviceHeight.ToString());
                writer.WriteEndElement();
                writer.WriteEndElement();

                // Ends the document
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
            catch (Exception)
            {
                if (writer != null)
                    writer.Close();
                throw new Exception();
            }
        }

        private void readSettingsFromXML()
        {
            XmlTextReader reader;
            String progPath = Environment.CurrentDirectory + @"\Ressources\UserOptions.xml";
            reader = new XmlTextReader(progPath);
            try
            {
                while (reader.Read())
                {
                    if (reader.LocalName.Equals("LoginName"))
                        this.loginName = reader.ReadElementContentAsString();

                    if (reader.LocalName.Equals("IPAddress"))
                        this.IPAddress = reader.ReadElementContentAsString();

                    if (reader.LocalName.Equals("IPPort"))
                        this.IPPort = reader.ReadElementContentAsString();

                    if (reader.LocalName.Equals("InitialCatalog"))
                        this.InitialCatalog = reader.ReadElementContentAsString();

                    if (reader.LocalName.Equals("TaxonInitialCatalog"))
                        this.TaxonNamesInitialCatalog = reader.ReadElementContentAsString();

                    if (reader.LocalName.Equals("SQLAuth"))
                        this.SqlAuth = reader.ReadElementContentAsString().Equals("yes");

                    if (reader.LocalName.Equals("HidePW"))
                        this.HidePW = reader.ReadElementContentAsString().Equals("yes");

                    if (reader.LocalName.Equals("MobilePath"))
                        this.mobileDBPath = reader.ReadElementContentAsString();

                    if (reader.LocalName.Equals("MobileTaxonPath"))
                        this.mobileTaxonPath = reader.ReadElementContentAsString();


                    if (reader.LocalName.Equals("TimeOut"))
                        this.connectingTimeOut = reader.ReadElementContentAsInt();

                    if (reader.LocalName.Equals("UseDevice"))
                        this.useDevice = reader.ReadElementContentAsString().Equals("yes");

                    if (reader.LocalName.Equals("DeviceWidth"))
                        this.deviceWidth = reader.ReadElementContentAsInt();

                    if (reader.LocalName.Equals("DeviceHeight"))
                        this.deviceHeight = reader.ReadElementContentAsInt();
                }
                reader.Close();
                this.mobilePath = mobileDBPath.Replace("MobileDB.sdf", "");
                this.mobilePicturePath = mobileDBPath.Replace("MobileDB.sdf", @"pictures\");
            }
            catch (Exception)
            {
                this.buttonConnectRepository.Enabled = false;
                this.buttonConnectMobile.Enabled = false;
                if (reader != null)
                    reader.Close();
                throw new Exception();
            }
        }

        #endregion

        private void buttonIdentSave_Click(object sender, EventArgs e)
        {
            if (this.startMobileConnection(true) && mobileDBSerializer != null)
            {
                IList<IdentificationUnit> units;
                IRestriction r = RestrictionFactory.TypeRestriction(typeof(IdentificationUnit));
                //IRestriction r = RestrictionFactory.Eq(typeof(UserProfile), "_LoginName", "SchubertKonstanze");
                units = mobileDBSerializer.Connector.LoadList<IdentificationUnit>(r);

                //IdentSave isa = this.repositorySerializer.CreateISerializableObject<IdentSave>();
                //IdentificationUnit iu = units.First();
                //isa.LastIdentificationCache = iu.LastIdentificationCache;
                //isa.Rowguid = iu.Rowguid;
                //repositorySerializer.Connector.InsertPlain(isa);
                foreach (IdentificationUnit iu in units)
                {
                    IdentSave isa = this.repositorySerializer.CreateISerializableObject<IdentSave>();
                    isa.LastIdentificationCache = iu.LastIdentificationCache;
                    isa.Rowguid = iu.Rowguid;
                    repositorySerializer.Connector.InsertPlain(isa);

                }
                this.endMobileConnection(true);
            }
        }

   

    }
}
