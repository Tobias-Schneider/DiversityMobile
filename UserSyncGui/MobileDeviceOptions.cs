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
    public partial class MobileDeviceOptions : Form
    {
        public MobileDeviceOptions(int width, int height)
        {
            InitializeComponent();

            this.textBoxWidth.Text = width.ToString();
            this.textBoxHeight.Text = height.ToString();
        }

        public int Width
        {
            get
            {
                try
                {
                    return int.Parse(textBoxWidth.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public int Height
        {
            get
            {
                try
                {
                    return int.Parse(textBoxHeight.Text);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        private void cancelCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void saveCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
