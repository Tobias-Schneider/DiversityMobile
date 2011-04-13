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

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    public partial class TextInputDialog : Form
    {
        public TextInputDialog()
        {
            InitializeComponent();
        }

        public TextInputDialog(string caption) : this()
        {
            if(caption != null)
                this.labelCaption.Text = caption;
        }

        public TextInputDialog(string caption, string text) : this(caption)
        {
            if(text != null)
                this.Value = text;
        }        
        
        public string Value
        {
            get
            {
                return this.textBoxValue.Text;
            }
            set
            {
                this.textBoxValue.Text = value;
            }
        }

        public int MaxLength
        {
            set
            {
                this.textBoxValue.MaxLength = value;
            }
        }
    }
}