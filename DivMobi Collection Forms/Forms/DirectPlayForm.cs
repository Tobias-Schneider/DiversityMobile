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