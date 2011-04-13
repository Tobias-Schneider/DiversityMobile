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

