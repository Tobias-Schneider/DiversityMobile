using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserSyncGui
{
    public partial class RepositoryOptions : Form
    {
        private String _IPAddress;
        private String _IPPort;
        private String _InitialCatalog;
        private String _TaxonInitialCatalog;
        private bool _SQLAuth;
        private bool _HidePassword;

        public RepositoryOptions()
        {
            InitializeComponent();
        }

        public RepositoryOptions(String ipAddress, String port, String initialCatalog, bool sqlAuth, String taxonInitialCatalog, bool hidePW):this()
        {
            this._IPAddress = ipAddress;
            this._IPPort = port;
            this._InitialCatalog = initialCatalog;
            this._TaxonInitialCatalog = taxonInitialCatalog;
            this._SQLAuth = sqlAuth;
            this._HidePassword = hidePW;
            this.panelRepositoryOptions.Visible = true;
            this.fillOptions();
        }

        private void fillOptions()
        {
            this.textBoxIPAddress.Text = this._IPAddress;
            this.textBoxPort.Text = this._IPPort;
            this.comboBoxInitialCatalog.Text = this._InitialCatalog;
            this.comboBoxTaxonNamesInitialCatalog.Text = this._TaxonInitialCatalog;
            this.radioButtonSQLAuth.Checked = this._SQLAuth;
            this.radioButtonWindowsAuth.Checked = !this._SQLAuth;
            this.checkBoxHidePassword.Checked = this._HidePassword;
        }

        private void clearOptions()
        {
            this.textBoxIPAddress.Text = "";
            this.textBoxPort.Text = "";
        }

        public String IPAddress
        {
            get
            {
                return this._IPAddress;
            }
        }

        public String IPPort
        {
            get
            {
                return this._IPPort;
            }
        }

        public String InitialCatalog
        {
            get
            {
                return this._InitialCatalog;
            }
        }

        public String TaxonInitialCatalog
        {
            get
            {
                return this._TaxonInitialCatalog;
            }
        }

        public bool SQLAuth
        {
            get
            {
                return this._SQLAuth;
            }
        }

        public bool HidePassword
        {
            get
            {
                return this._HidePassword;
            }
        }
        public bool WindowsAuth
        {
            get
            {
                return this.radioButtonWindowsAuth.Checked;
            }
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
                MessageBox.Show("Error concerning Repository Connection attributes. Please check values!");
        }

        private bool saveOptions()
        {
            if (!this.textBoxIPAddress.Text.Trim().Equals(String.Empty))
                    this._IPAddress = this.textBoxIPAddress.Text.Trim();
            else
                return false;

            if (!this.textBoxPort.Text.Trim().Equals(String.Empty))
                this._IPPort = this.textBoxPort.Text.Trim();
            else
                return false;

            if (!this.comboBoxInitialCatalog.Text.Trim().Equals(String.Empty))
                this._InitialCatalog = this.comboBoxInitialCatalog.Text.Trim();
            else
                return false;

            if (!this.comboBoxTaxonNamesInitialCatalog.Text.Trim().Equals(String.Empty))
                this._TaxonInitialCatalog = this.comboBoxTaxonNamesInitialCatalog.Text.Trim();
            else
                return false;

            this._SQLAuth = this.radioButtonSQLAuth.Checked;

            this._HidePassword = this.checkBoxHidePassword.Checked;
            
            return true;
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.saveOptions())
            {
                this.clearOptions();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Error concerning Repository Connection attributes. Please check values!");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
