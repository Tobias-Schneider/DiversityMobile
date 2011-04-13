using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.Forms;
using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    public partial class TaxonInputDialog : DialogBase
    {

        private String _value;
        private String taxGroup;

        private TaxonInputDialog():base()
        {
            InitializeComponent();
            base.adjustControlSizes();
            Rectangle r = Screen.PrimaryScreen.Bounds;
            Size s = new Size(r.Width, this.Size.Height);
            SizeF sf = new SizeF(r.Width, this.Size.Height);
            this.AutoScaleDimensions = sf;
            this.ClientSize = s;
            _value = null;
        }

        public TaxonInputDialog(String taxGroup):this()
        {
            this.taxGroup=taxGroup;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.textBoxTaxonName.Text = String.Empty;
            this.textBoxGenus.Text = String.Empty;
            this.textBoxEpitheton.Text = String.Empty;
            this.listBoxResult.Items.Clear();
            this._value = null;
            this.buttonOK.Enabled = false;
        }

        public String Value
        {
            get { return _value; }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            IList<String> taxa = DataFunctions.Instance.RetrieveTaxonCache(textBoxTaxonName.Text, textBoxGenus.Text, textBoxEpitheton.Text, this.taxGroup);
            foreach (String taxon in taxa)
            {
                this.listBoxResult.Items.Add(taxon);
            }
            if (this.listBoxResult.Items.Count > 0)
                this.buttonOK.Enabled = true;
            else
                MessageBox.Show("No results");
        }

        private void listBoxResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._value = this.listBoxResult.SelectedItem.ToString();
            this.buttonOK.Enabled = true;
        }

        private void TaxonInputDialog_Load(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void TaxonInputDialog_Closing(object sender, CancelEventArgs e)
        {
            this.inputPanel1.Enabled = false;
        }

        private void textBoxTaxonName_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show(this.textBoxTaxonName.Text);
        }

        private void textBoxGenus_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEpitheton_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

