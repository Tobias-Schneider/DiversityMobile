using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DataManagement;


using UBT.AI4.Bio.DivMobi.DatabaseConnector.Attributes;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    public partial class PropertyNamesSearchDialog : DialogBase
    {
        private PropertyNames pn;
        private String nameGroup;
        public PropertyNames PropertyName { get { return pn; } }

        public PropertyNamesSearchDialog(String propertyNameGroup)
        {
            InitializeComponent();
            if (propertyNameGroup != null)
            {
                this.nameGroup = propertyNameGroup;
                this.labelPropertyGroup.Text = propertyNameGroup;
            }
        }

        //private void buildTopDown(int parentID, TreeNode parentNode)
        //{
        //    try
        //    {
        //        IList<PropertyNames> children = DataFunctions.Instance.RetrievePropertyNamesChildren(nameGroup, parentID);
        //        foreach (PropertyNames pname in children)
        //        {
        //            if (pname != null)
        //            {
        //                TreeNode tn = new TreeNode(pname.DisplayText);
        //                buildTopDown(pname.TermID, tn);
        //                parentNode.Nodes.Add(tn);
        //            }
        //        }
        //    }
        //    catch (ConnectionCorruptedException ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void PropertyNamesSearchDialog_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                IList<PropertyNames> propertyNames = DataFunctions.Instance.RetrieveTopLevelPropertyNames(this.nameGroup);
                foreach (PropertyNames pname in propertyNames)
                {
                    if (pname != null)
                    {
                        TreeNode tn = new TreeNode(pname.DisplayText);
                        tn.Tag = pname.TermID;
                        treeViewPropertyNames.Nodes.Add(tn);
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this.Close();
            }
        }

        private void treeViewPropertyNames_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                pn = DataFunctions.Instance.RetrievePropertyName(nameGroup, (int)this.treeViewPropertyNames.SelectedNode.Tag);

                IList<PropertyNames> children = DataFunctions.Instance.RetrievePropertyNamesChildren(nameGroup, (int)this.treeViewPropertyNames.SelectedNode.Tag);
                this.treeViewPropertyNames.SelectedNode.Nodes.Clear();
                foreach (PropertyNames pname in children)
                {
                    if (pname != null)
                    {
                        TreeNode tn = new TreeNode(pname.DisplayText);
                        tn.Tag = pname.TermID;
                        this.treeViewPropertyNames.SelectedNode.Nodes.Add(tn);
                    }
                }
                this.treeViewPropertyNames.SelectedNode.Expand();
                Cursor.Current = Cursors.Default;
            }
            catch (ConnectionCorruptedException)
            {
                this.treeViewPropertyNames.SelectedNode.Nodes.Clear();
                Cursor.Current = Cursors.Default;
            }
        }
    }
}

