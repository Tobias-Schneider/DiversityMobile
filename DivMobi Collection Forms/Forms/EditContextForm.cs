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

using System.Reflection;
using UBT.AI4.Bio.DivMobi.UMF.Layout;
using UBT.AI4.Bio.DivMobi.UMF.Context;
using System.IO;
using UBT.AI4.Toolbox;
using UBT.AI4.Bio.DivMobi.Forms.ContextForms;

namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    public partial class EditContextForm : Form
    {
        public EditContextForm()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();

#if DEBUG
            this.MinimizeBox = false;
            this.ControlBox = true;
#else
            this.MinimizeBox = true;
            this.ControlBox = true;            
#endif

            this.fillListViewContext();
            Cursor.Current = Cursors.Default;
        }

        private void fillListViewContext()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            String _progPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    
            foreach (Type t in asm.GetTypes())
            {
                if (typeof(ContextForm).IsAssignableFrom(t) && t.BaseType == typeof(ContextForm))
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = t;

                    Form form = Activator.CreateInstance(t) as Form;
                    item.Text = (form as ContextForm).FormName;
                    
                    String contextPath = String.Concat(_progPath, @"\CustomizedContext\", t.Name, ".xml");
                    if (File.Exists(contextPath))
                        item.SubItems.Add("customized");
                    else
                        item.SubItems.Add("");

                    item.SubItems.Add((form as ContextForm).FormDescription);

                    this.listViewContextForms.Items.Add(item);
                }
            }
        }

        #region Menu & ListView Events

        private void listViewContextForms_ItemActivate(object sender, EventArgs e)
        {
            // Check if selected form is already customized
            // (important in regard to enabled menu items)
            if(listViewContextForms.SelectedIndices.Count > 0)
            {
                if (listViewContextForms.SelectedIndices.Count == 1)
                {
                    int index = listViewContextForms.SelectedIndices[0];

                    if (listViewContextForms.Items[index].SubItems[1].Text.Equals(String.Empty))
                    {
                        menuItemCustomizeContext.Enabled = true;
                        menuItemDeleteContext.Enabled = false;
                        menuItemShowCustomizedForm.Enabled = false;
                    }
                    else
                    {
                        menuItemCustomizeContext.Enabled = false;
                        menuItemDeleteContext.Enabled = true;
                        menuItemShowCustomizedForm.Enabled = true;
                    }
                }
                else
                {
                    menuItemCustomizeContext.Enabled = false;
                    menuItemDeleteContext.Enabled = false;
                    menuItemShowCustomizedForm.Enabled = false;
                }
            }
        }

        private void menuItemShowCustomizedForm_Click(object sender, EventArgs e)
        {
            if(listViewContextForms.SelectedIndices.Count > 0)
            {
                foreach(int index in listViewContextForms.SelectedIndices)
                {
                    Type t = (Type)listViewContextForms.Items[index].Tag;
                    Form frm = GetInstanceFromParameters(t.FullName, true, true) as Form;
                    if (frm != null)
                        frm.ShowDialog(); 
                }
            }
        }

        private void menuItemCustomizeContext_Click(object sender, EventArgs e)
        {
            if (listViewContextForms.SelectedIndices.Count > 0)
            {
                foreach (int index in listViewContextForms.SelectedIndices)
                {
                    Type t = (Type)listViewContextForms.Items[index].Tag;
                    Form frm = GetInstanceFromParameters(t.FullName, false, true) as Form;

                    if (frm != null)
                        frm.ShowDialog(); 
                }
            }
        }

        private void menuItemDeleteContext_Click(object sender, EventArgs e)
        {
            if (listViewContextForms.SelectedIndices.Count > 0)
            {
                foreach (int index in listViewContextForms.SelectedIndices)
                {
                    Type t = (Type)listViewContextForms.Items[index].Tag;
                    
                    String contextPath = String.Concat(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), @"\CustomizedContext\");
                    String contextFile = String.Concat(contextPath, t.Name, ".xml");

                    try
                    {
                        File.Delete(contextFile);
                        MessageBox.Show("Deleted customization will be considered only after next start of application.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Customization file couldn't be deleted. ("+ex.Message+")", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    }
                }
            }
        }

        #endregion

        #region System Methods


        // WorkAround for missing implementation of Activator.CreateInstance(Type, Params)
        // in .Net Compact Framework
        static object GetInstanceFromParameters(string typeName, params object[] pars)
        {
            var t = Assembly.GetExecutingAssembly().GetType(typeName);

            if (t != null)
            {
                var c = t.GetConstructor(pars.Select(p => p.GetType()).ToArray());
                if (c == null) return null;

                return c.Invoke(pars);
            }
            return null;
        }

        #endregion
    }
}