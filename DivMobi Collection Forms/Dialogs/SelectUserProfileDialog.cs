﻿//#######################################################################
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

using UBT.AI4.Bio.DivMobi.UMF.Context;

namespace UBT.AI4.Bio.DivMobi.Forms.Dialogs
{
    public partial class SelectUserProfileDialog : DialogBase
    {
        public SelectUserProfileDialog():base()
        {
            Cursor.Current = Cursors.WaitCursor;
            InitializeComponent();
            base.adjustControlSizes();
            Cursor.Current = Cursors.Default;
            this.fillListBox();
            if (this.listBoxUserProfiles.SelectedItem == null)
            {
                this.buttonOk.Enabled = false;
                this.buttonDetails.Enabled = false;
            }
        }

        private void listBoxUserProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.listBoxUserProfiles.Text != null && !this.listBoxUserProfiles.Text.Equals(String.Empty))
            {
                this.buttonOk.Enabled = true;
                this.buttonDetails.Enabled = true;
                try
                {
                    UserProfiles.Instance.Current = UserProfiles.Instance.LoadUserProfile(this.listBoxUserProfiles.SelectedIndex);
                }
                catch(Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    // Raised Exception, that avoid starting program
                    MessageBox.Show(ex.Message + " Program can't be started!", "Fatal Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.buttonDetails.Enabled = false;
                this.buttonOk.Enabled = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            UserProfileDialog upDlg = null;
            try
            {
                upDlg = new UserProfileDialog(true);

                if (upDlg == null)
                    return;
            }
            catch (ContextCorruptedException ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message, "Context Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            // Dialog zentrieren
            upDlg.Location = this.Location;

            if (upDlg.ShowDialog() == DialogResult.OK)
            {
                this.fillListBox();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fillListBox()
        {
            Cursor.Current = Cursors.WaitCursor;
            this.listBoxUserProfiles.Items.Clear();
            try
            {
                foreach (UserProfile item in UserProfiles.Instance.List)
                {
                    if (item != null)
                        this.listBoxUserProfiles.Items.Add(item.CombinedNameCache);
                }

                if (UserProfiles.Instance.Current != null)
                {
                    this.listBoxUserProfiles.SelectedItem = UserProfiles.Instance.Current.CombinedNameCache;
                    this.buttonDetails.Enabled = true;
                    this.buttonOk.Enabled = true;
                }
                else
                {
                    this.buttonDetails.Enabled = false;
                    this.buttonOk.Enabled = false;
                }
            }
            catch (ConnectionCorruptedException)
            {
                this.buttonDetails.Enabled = false;
                this.buttonOk.Enabled = false;
            }

            if (this.listBoxUserProfiles.Items.Count <= 0)
            {
                // Raised Exception, that avoid starting program
                MessageBox.Show("There is no possible user in Database. Program can't be started!", "Severe Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                this.DialogResult = DialogResult.Cancel;
                this.Dispose();
            }
        
            Cursor.Current = Cursors.Default;
        }
    }
}

