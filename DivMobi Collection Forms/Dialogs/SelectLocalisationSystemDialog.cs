using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    public partial class SelectLocalisationSystemDialog : DialogBase
    {
        private String _selection;

        public SelectLocalisationSystemDialog()
        {
            InitializeComponent();
        }

        private void buttonGPS_Click(object sender, EventArgs e)
        {
            _selection = buttonGPS.Text;
        }

        private void buttonSamplingPlot_Click(object sender, EventArgs e)
        {
            _selection = buttonSamplingPlot.Text;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _selection = buttonCancel.Text;
        }

        public String selection
        {
            get { return this._selection; }
        }
    }
}

