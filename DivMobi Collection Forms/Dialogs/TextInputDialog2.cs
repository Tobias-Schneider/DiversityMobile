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
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;


namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    public partial class TextInputDialog2 : Form
    {
        private String _text;

        public TextInputDialog2()
        {
            InitializeComponent();
            Rectangle r = Screen.PrimaryScreen.Bounds;
            Size s = new Size(r.Width, this.Size.Height);
            SizeF sf = new SizeF(r.Width, this.Size.Height);
            this.AutoScaleDimensions = sf;
            this.ClientSize = s;
        }

        public TextInputDialog2(String fieldName, String text):this()
        {
            if(fieldName!=null)
                this.labelFieldName.Text = fieldName;
            if (text != null)
                this.textBox1.Text = text;
        }

        private void TextInputForm_Load(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //this.text = this.textBox1.Text;
        }

        private void TextInputForm_Closing(object sender, CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                this._text = this.textBox1.Text;
            this.inputPanel1.Enabled = false;
        }
        public string Value { get { return _text; }  }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}