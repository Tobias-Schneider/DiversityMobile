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