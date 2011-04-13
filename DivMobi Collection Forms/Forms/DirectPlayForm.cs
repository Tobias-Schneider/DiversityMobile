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
using System.Drawing.Imaging;
using UBT.AI4.Bio.DivMobi.DataManagement;


//using UBT.AI4.Bio.DiversityCollection.Ressource.Dialogs;

namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    public partial class DirectPlayForm: DialogBase
    {
        private List<String> _videoFileList = new List<string>();
        private int _index = 0;

        #region Constructors
        public DirectPlayForm():base()
        {
            // initialization related to windows forms
            InitializeComponent();
            base.adjustControlSizes();

#if DEBUG
            this.MinimizeBox = false;
            this.ControlBox = true;
#else
            this.MinimizeBox = true;
            this.ControlBox = true;            
#endif

            this.Refresh();
        }

        public DirectPlayForm(List<String> imageFileList,ImageList list) : this()
        {
            if (imageFileList != null)
            {
                _index = 0;
                this._videoFileList = imageFileList;
            }
            if(list!=null)
                this.toolBarImages.ImageList = list;
            this.Refresh();
            setLabel();
            
        }
        #endregion

        private void setLabel()
        {
            this.labelPosition.Text = "Position: " + (_index + 1).ToString() + @"\ " + (_videoFileList.Count).ToString();
        }

        private void toolBarImages_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            int oldIndex = _index;
            if (_videoFileList.Count == 0)
            {
                return;
            }

            if (e.Button == this.toolBarButtonFirst)
            {
                _index = 0;
            }
            else if (e.Button == this.toolBarButtonPrevious)
            {
                if (_index > 0)
                {
                    _index--;
                }
            }
            else if (e.Button == this.toolBarButtonNext)
            {
                if (_index < _videoFileList.Count-1)
                {
                    _index++;
                }
            }
            else if (e.Button == this.toolBarButtonLast)
            {
                _index = _videoFileList.Count - 1;
            }

            if (oldIndex != _index)
            {                
                //this.pictureBoxImages.Image = _videoFileList[_index];
            }
            setLabel();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(_videoFileList[_index], null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading internal Video Viewer. (" + ex.Message + ")", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void DirectPlayForm_Closing(object sender, CancelEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        private void DirectPlayForm_Closed(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }
    }
}