//#######################################################################
//Diversity Mobile Synchronization
//Project Homepage:  http://www.diversitymobile.net
//Copyright (C) 2011  Tobias Schneider, Lehrstuhl Angewndte Informatik IV, Universität Bayreuth
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//#######################################################################
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    public partial class TaxonInputDialog2 : DialogBase
    {

        private String _value;
        private IList<String> _taxonCacheList;
        private String _taxGroup;

        private TaxonInputDialog2():base()
        {
            InitializeComponent();
            base.adjustControlSizes();
            this._taxonCacheList = new List<String>();
            _value = null;
        }

        public TaxonInputDialog2(String taxGroup)
            : this()
        {
            this._taxGroup = taxGroup;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.textBoxTaxonName.Text = String.Empty;
            this.textBoxGenus.Text = String.Empty;
            this.textBoxEpitheton.Text = String.Empty;
            this.comboBoxResult.Items.Clear();
            this._taxonCacheList.Clear();
            this._value = null;
            this.buttonOK.Enabled = false;
        }

        public String Value
        {
            get { return _value; }
        }

        public IList<String> TaxonCacheList
        {
            get { return _taxonCacheList; }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            comboBoxResult.Items.Clear();
            IList<String> taxa = DataFunctions.Instance.RetrieveTaxonCache(textBoxTaxonName.Text, textBoxGenus.Text, textBoxEpitheton.Text, this._taxGroup);
            foreach (String taxon in taxa)
            {
                this.comboBoxResult.Items.Add(taxon);
                this._taxonCacheList.Add(taxon);
            }
            if (this.comboBoxResult.Items.Count > 0)
            {

            }
            else
                MessageBox.Show("No results");
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
        }

        private void textBoxGenus_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEpitheton_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._value = this.comboBoxResult.SelectedItem.ToString();
            this.buttonOK.Enabled = true;
        }

        private void TaxonInputDialog2_Load(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void TaxonInputDialog2_Closing(object sender, CancelEventArgs e)
        {
            this.inputPanel1.Enabled = false;
        }

        private void comboBoxResult_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            this._value = this.comboBoxResult.SelectedItem.ToString();
            this.buttonOK.Enabled = true;
        }
    }
}
