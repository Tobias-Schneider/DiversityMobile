using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;
using System.Data.SqlServerCe;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.SyncTool.SyncTool;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.Util;

namespace UBT.AI4.Bio.DivMobi.SyncGui
{
    public partial class SyncPanel : UserControl
    {
        #region Member
        //----------------- DB Connections-----------
        // Repository
        private string repositoryType;
        private String repositoryConnectionPath = null; //Enthält das File inklusive Pfad zum Repository   
        private Serializer repositorySerializer;
        private UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool repositorySyncTool;
        
        

        //SyncDB
        private String syncDBConnectionPath = null;
        private Serializer syncDBSerializer;
        private SyncPool SyncPool;
        IList<Type> usedTypes = new List<Type>(); //verwaltet die Typen, die synchronisiert werden sollen


        //mobileDB
        private String mobileDBConnectionPath = null;
        private Serializer mobileDBSerializer;
        private UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool mobileDBSyncTool;
       

        //Synchronisationsrichtung

        bool download;

        //-----------Listen für die States------------

        List<SyncContainer> conflicted;
        List<SyncContainer> conflictResolved;
        List<SyncContainer> synchronized;
        List<SyncContainer> insert;
        List<SyncContainer> update;
        List<SyncContainer> ignore;
        List<SyncContainer> truncate;
        List<SyncContainer> delete;
        List<SyncContainer> premature;

        //-------------------------

        IList<Type> divMobiTypes = new List<Type>(); //verwaltet die aus DiversityCollection verwendeten Typen
        IList<Type> syncTypes = new List<Type>(); //Verwaltet die Felder der Synchronisation

        #endregion

        #region Constructors
        public SyncPanel()
        {
            InitializeComponent();
            //Verwendete Typen von DivMobi in die Verwaltende Liste einfügen
            
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
            divMobiTypes.Add(typeof(Property));
            //Bis hier: Korrepondiert zu DBVersion 20
            //divMobiTypes.Add(typeof(CollectionEventSeriesImage));
            //divMobiTypes.Add(typeof(CollEventSeriesImageType_Enum));
            //Bis hier: Korrepondiert zu DBVersion 22
            divMobiTypes.Add(typeof(CollIdentificationCategory_Enum));
            //divMobiTypes.Add(typeof(CollTypeStatus_Enum));
            divMobiTypes.Add(typeof(CollIdentificationQualifier_Enum));
            //Bis hier: Korrepondiert zu DBVersion 25
            //divMobiTypes.Add(typeof(CollLabelTranscriptionState_Enum));
            //divMobiTypes.Add(typeof(CollLabelType_Enum));
            //Bis hier: Korrepondiert zu DBVersion 27
            //divMobiTypes.Add(typeof(Collection));
            divMobiTypes.Add(typeof(CollectionProject));
            //divMobiTypes.Add(typeof(CollectionSpecimenPart));
            //divMobiTypes.Add(typeof(CollMaterialCategory_Enum));
            //Bis hier: Korrepondiert zu DBVersion 31
            divMobiTypes.Add(typeof(IdentificationUnitGeoAnalysis));
            divMobiTypes.Add(typeof(AnalysisResult));

            //Verwendete Typen von der Synchronisationsdatenbank in Liste einfügen
            syncTypes.Add(typeof(SyncItem));
            syncTypes.Add(typeof(FieldState));
            download = this.radioButtonDownload.Checked;
        }

        #endregion

        #region Member-Methods

        public String getDBLocation() //Gibt den Pfad zur Datenbank über einen Dialog zurück
        {
            OpenFileDialog dbConnectionDialog = new OpenFileDialog();
            String path;
            dbConnectionDialog.Filter = "sdf files (*.sdf)|*.sdf"; //Im Moment wird nur das DB-Format von DiversityMobile unterstützt
            if (dbConnectionDialog.ShowDialog() == DialogResult.OK)
            {
                path = dbConnectionDialog.FileName;

            }
            else path = "Error";
            return path;
        }

        private bool connectDB(String conString,ref Serializer ser, IList<Type> types) // Stellt eine Verbindung zu einer im Pfad gebenen DB her und Registriert die Typen. Kommt eine Verbindung zu Stande wird True zurückgegeben
        {
            if (conString == null)
            {
                MessageBox.Show("Select a DB First!", "No DB Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (File.Exists(@conString) == false)
            {
                MessageBox.Show("File Not Found! Please reselected the DB!", "No DB Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            ser = new MS_SqlCeSerializer(conString);
            ser.RegisterTypes(types);
            ser.Activate();
            return true;
        }

        private void activationCheck() //Prüft ob die Datenbanken verbunden sind.
        {
            analyzeButton.BackColor = System.Drawing.SystemColors.ControlDark;
            analyzeButton.Enabled = false;
            if (buttonConnectRepository.Text.Equals("Connected") == false)
                return;
            if (buttonConnectMobileDB.Text.Equals("Connected") == false)
                return;
            if (buttonConnectSyncDB.Text.Equals("Connected") == false)
                return;
            analyzeButton.BackColor = System.Drawing.SystemColors.Control;
            analyzeButton.Enabled = true;
        }

        #endregion

        #region Eventhandling
       
        //Repository
        private void buttonSelectRepository_Click(object sender, EventArgs e)
        {
            repositoryConnectionPath= getDBLocation(); //Pfad zu Repository wird über Dialog gewählt
            //textBoxModule.Text = repositoryConnectionPath; //Die Textbox wird aktualisiert
        }
        

        private void buttonConnectRepository_Click(object sender, EventArgs e)
        {
            try
            {
                switch(repositoryType)
                {
                    case "Remote":
                        repositorySerializer=new MS_SqlServerIPSerializer(textBoxUserName.Text,textBoxPassword.Text,textBoxRepository.Text,textBoxPort.Text,textBoxDBName.Text);
                        break;
                    case "Local WA":
                        repositorySerializer=new MS_SqlServerWASerializier(textBoxRepository.Text,textBoxDBName.Text);
                        break;
                    case "Local SQL":
                        repositorySerializer=new MS_SqlServerSerializer(textBoxUserName.Text,textBoxPassword.Text,textBoxRepository.Text,textBoxDBName.Text);
                        break;
                    default:
                        Exception f=new Exception();
                        throw  f;
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("Connection not possible. Please check values");
                return;
            }
            repositorySerializer.RegisterTypes(divMobiTypes);
            repositorySerializer.Activate();
            {
                buttonConnectRepository.BackColor = System.Drawing.SystemColors.Info;
                buttonConnectRepository.Text = "Connected";
                activationCheck();
            }
            
        }
        
       

        //MobileDB

        private void buttonSelectMobileDB_Click(object sender, EventArgs e)
        {
            mobileDBConnectionPath = getDBLocation(); //Pfad zu Repository wird über Dialog gewählt
            textBoxMobileDBConnection.Text = mobileDBConnectionPath; //Die Textbox wird aktualisiert
        }

        private void buttonConnectMobileDB_Click(object sender, EventArgs e)
        {
            if (connectDB(mobileDBConnectionPath, ref mobileDBSerializer,divMobiTypes) == true)
            {
                buttonConnectMobileDB.BackColor = System.Drawing.SystemColors.Info;
                buttonConnectMobileDB.Text = "Connected";
                activationCheck();
            }
        }

        //SyncDB
        private void buttonConnectSyncDB_Click(object sender, EventArgs e)
        {
            bool connectionFound=false;
            try
            {
                if (comboBoxSyncLocation.Text.Equals("Repository"))
                {
                    switch (repositoryType)
                    {
                        case "Remote":
                            syncDBSerializer = new MS_SqlServerIPSerializer(textBoxUserName.Text, textBoxPassword.Text, textBoxRepository.Text, textBoxPort.Text, textBoxSyncName.Text);
                            break;
                        case "Local WA":
                            syncDBSerializer = new MS_SqlServerWASerializier(textBoxRepository.Text,textBoxSyncName.Text);
                            break;
                        case "Local SQL":
                            syncDBSerializer = new MS_SqlServerSerializer(textBoxUserName.Text, textBoxPassword.Text, textBoxRepository.Text,textBoxSyncName.Text);
                            break;
                        default:
                            Exception f = new Exception();
                            throw f;
                    }
                    connectionFound=true;
                }
                if(comboBoxSyncLocation.Text.Equals("Mobile"))
                {
                    syncDBSerializer = new MS_SqlCeSerializer(syncDBConnectionPath);
                    connectionFound = true;
                }
                if (connectionFound == false)
                {
                    Exception f = new Exception();
                    throw f;
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("Connection not possible. Please check values");
                return;
            }
            syncDBSerializer.RegisterTypes(syncTypes);
            syncDBSerializer.Activate();
            {
                buttonConnectSyncDB.BackColor = System.Drawing.SystemColors.Info;
                buttonConnectSyncDB.Text = "Connected";
                activationCheck();
            }
            
        }

        private void comboBoxSyncLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelSyncDBName.Visible = false;
            textBoxSyncName.Visible = false;
            buttonConnectSyncDB.Visible = false;
            string syncStyle = comboBoxSyncLocation.Text;
            try
            {
                switch (syncStyle)
                {
                    case "Repository":
                        textBoxSyncName.Text = "Synchronisation";
                        break;
                    case "Mobile":
                        syncDBConnectionPath = getDBLocation();
                        textBoxSyncName.Text = syncDBConnectionPath;
                        break;
                    default:
                        Exception f = new Exception();
                        throw f;
                }
                labelSyncDBName.Visible = true;
                textBoxSyncName.Visible = true;
                buttonConnectSyncDB.Visible = true;
            }
            catch (Exception f)
            {
                
                MessageBox.Show("An Error occured. Please check values. Is the Repository already selected?");
                return;
            }
        }

        //Direction
        private void radioButtonDownload_Click(object sender, EventArgs e)
        {
            download = true;
        }

        private void radioButtonUpload_Click(object sender, EventArgs e)
        {
            download = false;
        }


        #endregion

        private void analyzeButton_Click(object sender, EventArgs e)
        {
            if (analyzeButton.BackColor ==System.Drawing.SystemColors.Info)
            {
                analyzeButton.BackColor = System.Drawing.SystemColors.Control;
            }
            analyzeButton.Text = "Analyzing";
            

            if (download == true)
            {
                //Nur die Basistabellen sollen synchronisiert werden
                FreeSyncPoolBuilder poolBuilder = new FreeSyncPoolBuilder(repositorySerializer, syncDBSerializer);
                //***Analysis ist ein Speizialfall, da nur projektspezifische Daten übertrageb werden sollen
                //Ermittle alle Projekte aus Userprofilen, die auf dem Handy gespeichert sind
                DbConnection mobConn = mobileDBSerializer.CreateConnection();
                mobConn.Open();
                DbCommand com =  mobConn.CreateCommand();
                com.CommandText = "Select ProjectID From UserProfile";
                DbDataReader reader = com.ExecuteReader();
                List<int> projects=new List<int>();
                while (reader.Read())
                {
                    projects.Add(reader.GetInt32(0));
                }

                foreach (int project in projects)
                {
                    String s = @"SELECT * FROM [DiversityCollection].[dbo].[AnalysisProjectList] ("+project+")";
                    IList<ISerializableObject> list = repositorySerializer.Connector.LoadList(typeof(Analysis),s);
                    poolBuilder.Add(list);
                }

                //*******
                poolBuilder.Load(typeof(AnalysisTaxonomicGroup));                             
                //poolBuilder.Load(typeof(Collection));
                poolBuilder.Load(typeof(Property));      
                poolBuilder.Load(typeof(CollEventImageType_Enum));
                poolBuilder.Load(typeof(CollSpecimenImageType_Enum));
                poolBuilder.Load(typeof(CollTaxonomicGroup_Enum));
                poolBuilder.Load(typeof(LocalisationSystem));
                poolBuilder.Load(typeof(CollCircumstances_Enum));
                poolBuilder.Load(typeof(CollUnitRelationType_Enum));
        
            
                //poolBuilder.Load(typeof(CollEventSeriesImageType_Enum));

                poolBuilder.Load(typeof(CollIdentificationCategory_Enum));
                //poolBuilder.Load(typeof(CollTypeStatus_Enum));
                //poolBuilder.Load(typeof(CollIdentificationQualifier_Enum));

                //poolBuilder.Load(typeof(CollLabelTranscriptionState_Enum));
                //poolBuilder.Load(typeof(CollLabelType_Enum));

                //poolBuilder.Load(typeof(CollMaterialCategory_Enum));
                //******
                usedTypes.Add(typeof(Analysis));
                usedTypes.Add(typeof(AnalysisTaxonomicGroup));
                //usedTypes.Add(typeof(Collection));
                usedTypes.Add(typeof(Property));
                usedTypes.Add(typeof(CollEventImageType_Enum));
                usedTypes.Add(typeof(CollSpecimenImageType_Enum));
                usedTypes.Add(typeof(CollTaxonomicGroup_Enum));
                usedTypes.Add(typeof(LocalisationSystem));
                usedTypes.Add(typeof(CollCircumstances_Enum));
                usedTypes.Add(typeof(CollUnitRelationType_Enum));


                //usedTypes.Add(typeof(CollEventSeriesImageType_Enum));

                usedTypes.Add(typeof(CollIdentificationCategory_Enum));
                //usedTypes.Add(typeof(CollTypeStatus_Enum));
                //usedTypes.Add(typeof(CollIdentificationQualifier_Enum));

                //usedTypes.Add(typeof(CollLabelTranscriptionState_Enum));
                //usedTypes.Add(typeof(CollLabelType_Enum));

                //usedTypes.Add(typeof(CollMaterialCategory_Enum));
                this.repositorySyncTool = new UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool(repositorySerializer, syncDBSerializer, usedTypes);
                this.mobileDBSyncTool = new UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool(mobileDBSerializer, syncDBSerializer, usedTypes);


                //this.SyncPool = poolBuilder.buildPool();
                this.SyncPool=repositorySyncTool.CreateSyncPool();
                this.mobileDBSyncTool.Analyze(SyncPool);
        
            }
            else
            {
                usedTypes.Clear();
                //usedTypes.Add(typeof(CollectionSpecimen));
                //usedTypes.Add(typeof(CollectionEvent));
                //usedTypes.Add(typeof(CollectionEventSeries));
                //usedTypes.Add(typeof(CollectionEventProperty));
                //usedTypes.Add(typeof(CollectionEventLocalisation));
                //usedTypes.Add(typeof(LocalisationSystem));
                //usedTypes.Add(typeof(CollectionAgent));
                //usedTypes.Add(typeof(CollectionSpecimenPart));
                //usedTypes.Add(typeof(CollectionProject));
                usedTypes.Add(typeof(IdentificationUnit));
                IRestriction r = RestrictionFactory.SqlRestriction(typeof(IdentificationUnit), "0=0");
                IRestriction r1 = RestrictionFactory.Gt(typeof(IdentificationUnit), "_IdentificationUnitID", 0);
                //usedTypes.Add(typeof(Identification));
                //usedTypes.Add(typeof(IdentificationUnitAnalysis));
                //usedTypes.Add(typeof(Analysis));
                //usedTypes.Add(typeof(Property));
                //usedTypes.Add(typeof(CollectionEventProperty));
                //usedTypes.Add(typeof(CollTaxonomicGroup_Enum));
                IList < ISerializableObject > list = mobileDBSerializer.Connector.LoadList(typeof(CollectionSpecimen), r1);
                this.repositorySyncTool = new UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool(repositorySerializer, syncDBSerializer, divMobiTypes);
                this.mobileDBSyncTool = new UBT.AI4.Bio.DivMobi.SyncTool.SyncTool.SyncTool(mobileDBSerializer, syncDBSerializer, divMobiTypes);
                this.SyncPool = this.mobileDBSyncTool.CreateSyncPool(r1);
                this.repositorySyncTool.Analyze(SyncPool);
            }
            analyzeButton.Text = "Complete";
            analyzeButton.BackColor = System.Drawing.SystemColors.Info;
            tabControlAnalysis.Enabled = true;
            // Listboxen mit den Analysen befüllen

            conflicted =(List<SyncContainer>)SyncPool.GetSyncContainer<ConflictState>();
            listBoxConflicts.Items.Clear();
            foreach (SyncContainer sc in conflicted)
            {
                listBoxConflicts.Items.Add(sc);
            }

            conflictResolved=(List<SyncContainer>)SyncPool.GetSyncContainer<ConflictResolvedState>();
            listBoxConflictResolved.Items.Clear();
            foreach (SyncContainer sc in conflictResolved)
            {
                listBoxConflictResolved.Items.Add(sc);
            }

            synchronized = (List<SyncContainer>)SyncPool.GetSyncContainer<SynchronizedState>();
            listBoxSynchronized.Items.Clear();
            foreach (SyncContainer sc in synchronized)
            {
                listBoxSynchronized.Items.Add(sc);
            }


            insert = (List<SyncContainer>)SyncPool.GetSyncContainer<InsertState>();
            listBoxInsert.Items.Clear();
            foreach (SyncContainer sc in insert)
            {
                listBoxInsert.Items.Add(sc);
            }

            update = (List<SyncContainer>)SyncPool.GetSyncContainer<UpdateState>();
            listBoxUpdate.Items.Clear();
            foreach (SyncContainer sc in update)
            {
                listBoxUpdate.Items.Add(sc);
            }

            ignore = (List<SyncContainer>)SyncPool.GetSyncContainer<IgnoreState>();
            listBoxIgnore.Items.Clear();
            foreach (SyncContainer sc in ignore)
            {
                listBoxIgnore.Items.Add(sc);
            }

            truncate = (List<SyncContainer>)SyncPool.GetSyncContainer<TruncateState>();
            listBoxTruncate.Items.Clear();
            foreach (SyncContainer sc in truncate)
            {
                listBoxTruncate.Items.Add(sc);
            }

            delete = (List<SyncContainer>)SyncPool.GetSyncContainer<DeletedState>();
            listBoxDelete.Items.Clear();
            foreach (SyncContainer sc in delete)
            {
                listBoxDelete.Items.Add(sc);
            }

            premature = (List<SyncContainer>)SyncPool.GetSyncContainer<PrematureState>();
            listBoxPremature.Items.Clear();
            foreach (SyncContainer sc in premature)
            {
                listBoxPremature.Items.Add(sc);
            }
            buttonSynchronize.Enabled = true;
            buttonSynchronize.BackColor = System.Drawing.SystemColors.Control;
        }

        private void buttonSynchronize_Click(object sender, EventArgs e)
        {
            if (download == false)
            {
                buttonSynchronize.Text = "Synchronizing";
                //Alles außer InsertState auf ignore setzen
                MessageBox.Show("Warning: Only InsertState is allowed at the moment. All other states will be set to IgnoreState");
                foreach (SyncContainer sc in conflicted)
                {
                    sc.Ignore();
                }
                foreach (SyncContainer sc in conflictResolved)
                {
                    sc.Ignore();
                }
                foreach (SyncContainer sc in synchronized)
                {
                    sc.Ignore();
                }
                foreach (SyncContainer sc in update)
                {
                    sc.Ignore();
                }
                foreach (SyncContainer sc in truncate)
                {
                    sc.Ignore();
                }
                foreach (SyncContainer sc in delete)
                {
                    sc.Ignore();
                }
                foreach (SyncContainer sc in premature)
                {
                    sc.Ignore();
                }

            }
            if (download == true)
            {
                buttonSynchronize.Text = "Synchronizing";
                //Veränderungen an den definierenden KLassen werden überschrieben
                MessageBox.Show("Your changes in defining data items will be overwritten");
                foreach (SyncContainer sc in conflicted)
                {
                    sc.Resolve(sc.SourceObject);
                }
            }
            if (listBoxConflicts.Items.Count != 0)
            {
                MessageBox.Show("Resolve Conflicts first!");
                return;
            }
            //Je nach gewählter Richtung synchronisieren 
            if (download == true)
            {
                mobileDBSyncTool.Synchronize(SyncPool);
                
                //DisplayOrder für taxonomische Gruppen zurücksetzen
                IRestriction r1 = RestrictionFactory.TypeRestriction(typeof(CollTaxonomicGroup_Enum));
                IList<CollTaxonomicGroup_Enum> groups = mobileDBSerializer.Connector.LoadList<CollTaxonomicGroup_Enum>(r1);
                foreach (CollTaxonomicGroup_Enum tg in groups)
                {
                    tg.DisplayOrder = 0;
                    mobileDBSerializer.Connector.Save(tg);
                }
                this.mobileDBSyncTool.Analyze(SyncPool);
            }
            else
            {
                repositorySyncTool.Synchronize(SyncPool);
                this.repositorySyncTool.Analyze(SyncPool);
            }
            buttonSynchronize.Text = "Synchronized";
            


            // Listboxen mit den Analysen befüllen

            conflicted = (List<SyncContainer>)SyncPool.GetSyncContainer<ConflictState>();
            listBoxConflicts.Items.Clear();
            foreach (SyncContainer sc in conflicted)
            {
                listBoxConflicts.Items.Add(sc);
            }

            conflictResolved = (List<SyncContainer>)SyncPool.GetSyncContainer<ConflictResolvedState>();
            listBoxConflictResolved.Items.Clear();
            foreach (SyncContainer sc in conflictResolved)
            {
                listBoxConflictResolved.Items.Add(sc);
            }

            synchronized = (List<SyncContainer>)SyncPool.GetSyncContainer<SynchronizedState>();
            listBoxSynchronized.Items.Clear();
            foreach (SyncContainer sc in synchronized)
            {
                listBoxSynchronized.Items.Add(sc);
            }


            insert = (List<SyncContainer>)SyncPool.GetSyncContainer<InsertState>();
            listBoxInsert.Items.Clear();
            foreach (SyncContainer sc in insert)
            {
                listBoxInsert.Items.Add(sc);
            }

            update = (List<SyncContainer>)SyncPool.GetSyncContainer<UpdateState>();
            listBoxUpdate.Items.Clear();
            foreach (SyncContainer sc in update)
            {
                listBoxUpdate.Items.Add(sc);
            }

            ignore = (List<SyncContainer>)SyncPool.GetSyncContainer<IgnoreState>();
            listBoxIgnore.Items.Clear();
            foreach (SyncContainer sc in ignore)
            {
                listBoxIgnore.Items.Add(sc);
            }

            truncate = (List<SyncContainer>)SyncPool.GetSyncContainer<TruncateState>();
            listBoxTruncate.Items.Clear();
            foreach (SyncContainer sc in truncate)
            {
                listBoxTruncate.Items.Add(sc);
            }

            delete = (List<SyncContainer>)SyncPool.GetSyncContainer<DeletedState>();
            listBoxDelete.Items.Clear();
            foreach (SyncContainer sc in delete)
            {
                listBoxDelete.Items.Add(sc);
            }

            premature = (List<SyncContainer>)SyncPool.GetSyncContainer<PrematureState>();
            listBoxPremature.Items.Clear();
            foreach (SyncContainer sc in premature)
            {
                listBoxPremature.Items.Add(sc);
            }
            buttonSynchronize.Text = "Completed";

        }

        private void buttonAnalyzeConflict_Click(object sender, EventArgs e)
        {
            if (listBoxConflicts.SelectedItem == null)
            {
                MessageBox.Show("Select an Item First");
                return;
            }
            SyncContainer conflict = (SyncContainer)listBoxConflicts.SelectedItem;
            AnalyzeForm a= new AnalyzeForm(conflict);
            a.ShowDialog();
            ISerializableObject solution =a.solution;
            if (solution != null)
            {
                conflict.Resolve(solution);
                listBoxConflictResolved.Items.Add(conflict);
                listBoxConflicts.Items.Remove(conflict);
            }
        }


        private void comboBoxRepositoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Zunächst alle kontrols unsichtbar machen und löschen
            labelRepository.Visible = false;
            textBoxRepository.Visible = false;
            textBoxRepository.Clear();
            labelPort.Visible = false;
            textBoxPort.Visible = false;
            textBoxPort.Clear();
            labelDBName.Visible = false;
            textBoxDBName.Visible = false;
            textBoxDBName.Clear();
            labelUserName.Visible = false;
            textBoxUserName.Visible = false;
            textBoxUserName.Clear();
            labelPassword.Visible = false;
            textBoxPassword.Visible = false;
            textBoxPassword.Clear();
            buttonConnectRepository.Visible = false;

            //Typ des Repositorys setzen
            repositoryType = comboBoxRepositoryType.Text;

            switch (repositoryType)
            {
                case "Remote":
                    labelRepository.Visible = true;
                    textBoxRepository.Visible = true;
                    textBoxRepository.Text = "141.84.65.107";
                    labelPort.Visible = true;
                    textBoxPort.Visible = true;
                    textBoxPort.Text = "5432";
                    labelDBName.Visible = true;
                    textBoxDBName.Visible = true;
                    labelUserName.Visible = true;
                    textBoxUserName.Visible = true;
                    labelPassword.Visible = true;
                    textBoxPassword.Visible = true;
                    buttonConnectRepository.Visible = true;
                    break;
                case "Local WA":
                    labelRepository.Visible = true;
                    textBoxRepository.Visible = true;
                    textBoxRepository.Text = "BTN4NF";
                    labelDBName.Visible = true;
                    textBoxDBName.Visible = true;
                    textBoxDBName.Text = "DiversityCollection_Copy";
                    buttonConnectRepository.Visible = true;
                    break;
                case "Local SQL":
                    labelRepository.Visible = true;
                    textBoxRepository.Visible = true;
                    labelDBName.Visible = true;
                    textBoxDBName.Visible = true;
                    labelUserName.Visible = true;
                    textBoxUserName.Visible = true;
                    labelPassword.Visible = true;
                    textBoxPassword.Visible = true;
                    buttonConnectRepository.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs c = (MouseEventArgs)e;
            MessageBox.Show(c.Location.ToString());
        }

        

       
    }
}
