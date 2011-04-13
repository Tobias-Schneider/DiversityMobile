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
using UBT.AI4.Bio.DivMobi.DataLayer.DataItems;
using UBT.AI4.Bio.DivMobi.DataManagement;
using UBT.AI4.Toolbox.Controls;
using UBT.AI4.Toolbox;
using System.IO;
using UBT.AI4.Bio.DivMobi.Forms;


namespace UBT.AI4.Bio.DivMobi.Forms.ContextForms
{
    public partial class AgentForm : ContextForm
    {
        public CollectionAgent _agent = null;
        public bool changed = false;

        #region Constructors
        
        // Default Constructor for creating instance without initialization of components
        public AgentForm()
            : base("Agent Form", "Create new or edit existing Collection Agent") {}
        
        public AgentForm(bool loadContext):this()
        {
            // initialization related to windows forms
            InitializeComponent();

#if DEBUG
            this.MinimizeBox = false;
            this.ControlBox = true;
#else
            this.MinimizeBox = true;
            this.ControlBox = true;            
#endif
            if(loadContext)
                this.loadInContext();

            this.buttonOk.Enabled = true;
            this.buttonCancel.Enabled = true;
        }

        public AgentForm(bool loadContext, bool customizeForm)
            : this(loadContext)
        {
            if (customizeForm)
            {
                this.loadInCustomization();
            }
        }

        public AgentForm(CollectionAgent agent)
            : this(true)
        {
            if (agent != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                this._agent = agent;
                
                // fill Form
                this.textBoxCollectorsName.Text = _agent.CollectorsName;
                if (_agent.CollectorsNumber != null && !_agent.CollectorsNumber.Equals(String.Empty))
                    this.textBoxNumber.Text = _agent.CollectorsNumber;

                if (_agent.Notes != null && !_agent.Notes.Equals(String.Empty))
                    this.textBoxNotes.Text = _agent.Notes;

                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        private void buttonChangeName_Click(object sender, EventArgs e)
        {
            if (this._agent != null)
            {
                TextInputDialog tid = new TextInputDialog("New Collectors Name", this._agent.CollectorsName);
                // Dialog zentrieren
                tid.Location = new Point((this.Size.Width) / 2 - (tid.Size.Width) / 2, this.Location.Y);

                if (tid.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataFunctions.Instance.Remove(this._agent);
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Name can't be changed. ("+ex.Message+")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    this._agent.CollectorsName = tid.Value;
                    this.textBoxCollectorsName.Text = tid.Value;
                    this.changed = true;
                }
            }
        }

        private void AgentForm_Closing(object sender, CancelEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                if (MessageBox.Show("Data won't be saved. Do You really want to leave the Dialog?", "Data not saved", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else if (this.DialogResult == DialogResult.Abort)
            {
                e.Cancel = true;
                return;
            }
        }

        private bool saveAgentData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_agent != null)
            {
                if (!this.textBoxNumber.Text.Equals(String.Empty))
                    _agent.CollectorsNumber = this.textBoxNumber.Text;
                else
                    _agent.CollectorsNumber = null;

                if (!this.textBoxNotes.Text.Equals(String.Empty))
                    _agent.Notes = this.textBoxNotes.Text;
                else
                    _agent.Notes = null;
                try
                {
                    DataFunctions.Instance.Update(_agent);
                }
                catch(Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Agent Data couldn't be saved. (" + ex.Message + ")", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }
            Cursor.Current = Cursors.Default;
            return true;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (this.HasContext)
            {
                if(!this.IsInCustomization)
                {
                    // Save Agent Data
                    if (!this.saveAgentData())
                        this.DialogResult = DialogResult.Abort;
                }
            }
            else
            {
                if (this.IsInCustomization)
                {
                    // write Customized Context to XML-File
                    if (!this.writeCustomizedContext())
                        this.DialogResult = DialogResult.Abort;
                }
            }
        }
    }
}