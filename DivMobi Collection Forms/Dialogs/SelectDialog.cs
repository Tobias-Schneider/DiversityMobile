using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    public partial class SelectDialog : DialogBase
    {
        public SelectDialog()
        {
            InitializeComponent();
            base.adjustControlSizes();
        }

        public SelectDialog(String caption, List<String> list)
            : this()
        {
            if(caption != null)
                this.labelCaption.Text = caption;

            if (list != null)
            {
                foreach (String item in list)
                {
                    if(item != null)
                        this.comboBoxSelect.Items.Add(item);
                }
                this.comboBoxSelect.SelectedIndex = 0;
            }
            else
            {
                return;
            }
                
        }
        public String Value
        {
            get
            {
                return this.comboBoxSelect.Text;
            }
        }

        private void comboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
     
    }
}

