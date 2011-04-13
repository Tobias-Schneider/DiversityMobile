using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Desktop.Communication;

namespace UserSyncGui
{
    public partial class ActiveSyncOptions : Form
    {
        private String _MobileDBPath;
        private String _MobileTaxonPath;
        private int _ConnectingTimeOut;
        private RAPI rapi;
        private bool _UseDevice;

       
        public ActiveSyncOptions()
        {
            InitializeComponent();
        }

        public ActiveSyncOptions(String divMobiPath, String taxonNamePath, int timeOut, bool useDevice):this()
        {
            this._MobileDBPath = divMobiPath;
            this._MobileTaxonPath = taxonNamePath;
            this._ConnectingTimeOut = timeOut/1000;
            this._UseDevice = useDevice;
            rapi = new RAPI();
            this.fillOptions();
        }

        public String MobileDBPath
        {
            get
            {
                return this._MobileDBPath;
            }
        }
       
        public String MobileTaxonPath
        {
            get
            {
                return this._MobileTaxonPath;
            }
        }

        public int ConnectingTimeOut
        {
            get
            {
                return this._ConnectingTimeOut*1000;
            }
        }
        public bool UseDevice
        {
            get
            {
                return this._UseDevice;
            }
        }

        private void fillOptions()
        {
            this.textBoxTimeOut.Text = this._ConnectingTimeOut.ToString();

            if (this._MobileDBPath != null)
                this.textBoxDivMobiPath.Text = this._MobileDBPath;
            else
                this.textBoxDivMobiPath.Text = "";

            if (this._MobileTaxonPath != null)
                this.textBoxTaxonNamesPath.Text = this._MobileTaxonPath;
            else
                this.textBoxTaxonNamesPath.Text = "";

            this.checkBoxUseDevice.Checked = this._UseDevice;

            if (rapi.DevicePresent)
            {
                this.textBoxDivMobiPath.Enabled = true;
                this.textBoxTaxonNamesPath.Enabled = true;
                this.buttonDivMobiSearch.Enabled = true;
                this.buttonTaxonNamesSearch.Enabled = true;
            }
            else
            {
                this.textBoxDivMobiPath.Enabled = false;
                this.textBoxTaxonNamesPath.Enabled = false;
                this.buttonDivMobiSearch.Enabled = false;
                this.buttonTaxonNamesSearch.Enabled = false;
            }
        }

        private void clearOptions()
        {
            this.textBoxTimeOut.Text = "";
            this.textBoxDivMobiPath.Text = "";
            this.textBoxTaxonNamesPath.Text = "";
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveOptions())
            {
                this.clearOptions();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Error concerning ActiveSync Connection attributes. Please check values!");
        }

        private bool saveOptions()
        {
            int time;
            try
            {
                time = int.Parse(this.textBoxTimeOut.Text);
                this._ConnectingTimeOut = time;
            }
            catch (Exception ex)
            {
                MessageBox.Show("TimeOut has incorrect format. Please check value.");
                return false;
            }

            if (!this.textBoxDivMobiPath.Text.Trim().Equals(String.Empty))
                this._MobileDBPath = this.textBoxDivMobiPath.Text.Trim();
            else
                return false;

            if (!this.textBoxTaxonNamesPath.Text.Trim().Equals(String.Empty))
                this._MobileTaxonPath = this.textBoxTaxonNamesPath.Text.Trim();
            else
                return false;

            this._UseDevice = this.checkBoxUseDevice.Checked;
            return true;
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonDivMobiSearch_Click(object sender, EventArgs e)
        {
            if (rapi.DevicePresent)
            {
                OpenFileDialog dbConnectionDialog = new OpenFileDialog();
                dbConnectionDialog.Filter = "sdf files (*.sdf)|*.sdf"; //Im Moment wird nur das DB-Format von DiversityMobile unterstützt
                if (dbConnectionDialog.ShowDialog() == DialogResult.OK)
                {
                    this.textBoxDivMobiPath.Text = @dbConnectionDialog.FileName;
                    String path = @System.IO.Path.GetDirectoryName(this.textBoxDivMobiPath.Text);
                    //Es wird davon ausgegangen, dass die Taxondatenbank im selben Verzeichnis liegt.
                    this.textBoxTaxonNamesPath.Text = path + @"\TaxonNames.sdf";
                }
            }
        }

        private void buttonTaxonNamesSearch_Click(object sender, EventArgs e)
        {
            if (rapi.DevicePresent)
            {
                OpenFileDialog dbConnectionDialog = new OpenFileDialog();
                dbConnectionDialog.Filter = "sdf files (*.sdf)|*.sdf"; //Im Moment wird nur das DB-Format von DiversityMobile unterstützt
                if (dbConnectionDialog.ShowDialog() == DialogResult.OK)
                {
                    this.textBoxTaxonNamesPath.Text = @dbConnectionDialog.FileName;
                }
            }
        }

        private void checkBoxUseDevice_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxUseDevice.Checked && !this.rapi.DevicePresent)
            {
                MessageBox.Show("There's no connected device present. Please check connection to mobile device.");
                this.checkBoxUseDevice.Checked = false;
            }
        }
    }
}
