using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializable;
using UBT.AI4.Bio.DivMobi.DatabaseConnector.Restrictions;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;


namespace UBT.AI4.Bio.DivMobi.SyncGui
{
    public partial class TaxPanel : UserControl
    {
        String sdfPath;
        bool repositoryConnection=false;
        bool mobileConnection = false;
        MS_SqlServerIPSerializer SerRepository;
        Serializer SerMobile;
        SqlCeConnection ConnMobile;
        DbConnection ConnRepository;

        public TaxPanel()
        {
            InitializeComponent();
            textBoxIP.Text = "141.84.65.107";
            textBoxPort.Text = "5432";
            textBoxDBName.Text = "DiversityMobile";
        }


        public String getPath(String filter) //Gibt den Pfad zur Datenbank über einen Dialog zurück
        {
            OpenFileDialog dbConnectionDialog = new OpenFileDialog();
            String path;
            dbConnectionDialog.Filter = filter;
            if (dbConnectionDialog.ShowDialog() == DialogResult.OK)
            {
                path = dbConnectionDialog.FileName;

            }
            else path = "Error";
            return path;
        }

        public void startcheck()
        {
            if (repositoryConnection == true && mobileConnection == true)
            {
                buttonStart.BackColor = Color.Gray;
                buttonStart.Enabled = true;
                buttonConnectRepository.Enabled = false;
                buttonConnectMobile.Enabled = false;
            }
        }

        public int getTaxa(string source, string target)
        {
                  
            
            //Alte DB-Einträge löschen
            string connString = "Data Source=\"" + sdfPath + "\";Max Database Size=128;Default Lock Escalation=100;";
            SqlCeConnection conn = new SqlCeConnection(connString);
            DbCommand command = conn.CreateCommand();
            conn.Open();
            StringBuilder sb = new StringBuilder();
            sb.Append("Delete From ").Append(target);
            command.CommandText = sb.ToString();
            command.ExecuteNonQuery();
            conn.Close();
            //Neue Taxa holen
            List<TaxonNames> taxa=new List<TaxonNames>();
            DbCommand com = ConnRepository.CreateCommand();
            sb = new StringBuilder();
            sb.Append("Select * From ").Append(source);
            com.CommandText = sb.ToString();
            ConnRepository.Open();
            DbDataReader reader= com.ExecuteReader();
            while (reader.Read())
            {
                TaxonNames taxon=SerRepository.CreateISerializableObject<TaxonNames>();
                taxon.NameURI = reader.GetString(0);
                taxon.TaxonNameCache = reader.GetString(1);
                taxon.Synonym = reader.GetString(2);
                taxa.Add(taxon);
            }
            ConnRepository.Close();
            SerMobile.Connector.BeginTransaction();
            foreach (TaxonNames taxon in taxa)
            {
                SerMobile.Connector.InsertPlain(taxon,target);
            }
            SerMobile.Connector.Commit();
            return taxa.Count;
        }

        #region Events

        private void buttonSelect_Click(object sender, EventArgs e)
        {

            String filter = "sdf files (*.sdf)|*.sdf";
            sdfPath = getPath(filter);
            textBoxPath.Text = sdfPath;
            if (sdfPath != "Error")
            {
                if (sdfPath != null)
                {
                    buttonConnectMobile.Enabled = true;
                    buttonConnectMobile.BackColor = Color.Gray;
                    buttonConnectMobile.Text = "Connect";
                }
            }
        }


        private void buttonConnectTaxDB_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=\"" + sdfPath + "\";Max Database Size=128;Default Lock Escalation=100;";
            SerMobile = new MS_SqlCeSerializer(@sdfPath);
            SerMobile.RegisterType(typeof(TaxonNames));
            SerMobile.Activate();
            buttonConnectMobile.Text = "Connected";
            buttonConnectMobile.BackColor = Color.Yellow;
            buttonSelect.Enabled = false;
            buttonSelect.BackColor = Color.DarkGray;
            buttonSelect.Text = "Closed";
            mobileConnection = true;
            startcheck();
        }

        private void buttonConnectRepository_Click(object sender, EventArgs e)
        {
            try
            {


                SerRepository = new MS_SqlServerIPSerializer(@textBoxUser.Text, @textBoxPassword.Text, @textBoxIP.Text, @textBoxPort.Text, @textBoxDBName.Text);
                SerRepository.RegisterType(typeof(TaxonNames));
                SerRepository.Activate();
                ConnRepository = SerRepository.CreateConnection();
                buttonConnectRepository.Text = "Connected";
                buttonConnectRepository.BackColor = Color.Yellow;
                repositoryConnection = true;
                
            }
            catch (Exception f)
            {
                MessageBox.Show("An error according to Your Values has occured", "Check Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            startcheck();

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Text = "Executing";
            buttonStart.BackColor = Color.LightBlue;
            int i=getTaxa("BfN_plant","TaxonNamesPlants");
            MessageBox.Show("Plants: " +i+" Taxa inserted");
            i = getTaxa("DGfM_fungus", "TaxonNamesFungi");
            MessageBox.Show("Fungi: " + i + " Taxa inserted");
            i = getTaxa("LiasNames_lichen", "TaxonNamesLichen");
            MessageBox.Show("Lichen: " + i + " Taxa inserted");
            MessageBox.Show("Complete");
            buttonStart.Text = "Completed";
            buttonStart.BackColor = Color.Yellow;

        }

        #endregion
    }
}
