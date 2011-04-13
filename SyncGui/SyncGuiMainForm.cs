using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UBT.AI4.Bio.DivMobi.SyncGui
{
    public partial class SyncGuiMainForm : Form
    {

        protected SyncPanel syncPanel;
        protected TaxPanel taxpanel;

        public SyncGuiMainForm()
        {
            InitializeComponent();
            this.syncPanel = new SyncPanel();
            this.syncPanel.Dock = DockStyle.Fill;
            this.Controls.Add(this.syncPanel);
            //this.taxpanel = new TaxPanel();
            //this.taxpanel.Dock = DockStyle.Fill;
            //this.Controls.Add(this.taxpanel);
        }

        private void SyncGuiMainForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
