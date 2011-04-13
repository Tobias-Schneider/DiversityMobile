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
    public partial class FormSelectProject : Form
    {
        Dictionary<int, String> projects;
        public int projectID;
        public String projectName;

        public FormSelectProject(Dictionary<int,String> projects)
        {
            InitializeComponent();
            this.projects = projects;
        }

        private void FormSelectProject_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, String> vp in projects)
            {
                listBox1.Items.Add(vp);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                KeyValuePair<int, String> select = (KeyValuePair<int, String>)listBox1.SelectedItem;
                projectID = select.Key;
                projectName = select.Value;
            }
        }
    }
}
