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
using OpenNETCF.Drawing.Imaging;
//using UBT.AI4.Bio.DiversityCollection.Ressource.Dialogs;

namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    public partial class ImageForm : Form
    {
        private List<String> _imageFileList = new List<string>();
        private ImageList _currentToolbarImageList = new ImageList();
        private int _index = 0;
        private int max=800;

        #region Constructors
        public ImageForm()
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
            try
            {
                switch (UserProfiles.Instance.Current.ToolbarIcons.Trim())
                {
                    case "large":
                        this._currentToolbarImageList = imageListLargeToolbarButtons;
                        break;
                    case "medium":
                        this._currentToolbarImageList = imageListMediumToolbarButtons;
                        break;
                    default:
                        this._currentToolbarImageList = imageListToolbarButtons;
                        break;
                }
            }
            catch (ConnectionCorruptedException ex)
            {
                MessageBox.Show("User settings couldn't be loaded. Default images will be used. ("+ex.Message+")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                this._currentToolbarImageList = imageListToolbarButtons;
            }
        }

        public ImageForm(List<String> imageFileList) : this()
        {
            if (imageFileList != null)
            {
                try
                {
                    _index = 0;
                    this._imageFileList = imageFileList;
                    //System.Diagnostics.Process.Start(_imageFileList[_index], null);
                    //Bitmap bmp = new Bitmap(_imageFileList[_index]);
                    //Size s = bmp.Size;
                    //this.pictureBoxImages.Size = s;
                    //int k = System.Runtime.InteropServices.Marshal.SizeOf(bmp);
                    this.loadImage(_imageFileList[_index],max);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while loading internal Image Viewer. ("+ex.Message+")", "Image Viewer Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
            }
            this.Refresh();
        }
        #endregion

        private void loadImage(String path, int maxRes)
        {
            try
            {
                if (pictureBoxImages.Image != null)
                {
                    pictureBoxImages.Image.Dispose();
                    pictureBoxImages.Image = null;
                }
                ImagingFactory factory = new ImagingFactoryClass();
                IImage img;
                ImageInfo inf = new ImageInfo();
                factory.CreateImageFromFile(path, out img);

                img.GetImageInfo(out inf);
                double ratio = (double)inf.Width / (double)inf.Height;
                uint x = 0;
                uint y = 0;
                if (inf.Width > inf.Height)
                {
                    x = Math.Min(inf.Width, (uint)maxRes);
                    y = (uint)Math.Floor(x / ratio);
                }
                else
                {
                    y = Math.Min(inf.Height, (uint)maxRes);
                    x = (uint)Math.Floor(y * ratio);
                }

                IBitmapImage imgB;
                factory.CreateBitmapFromImage(img,
                    x,
                    y,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                    InterpolationHint.InterpolationHintDefault,
                    out imgB);

                Size s = new Size((int)x, (int)y);
                pictureBoxImages.Size = s;
                pictureBoxImages.Image = ImageUtils.IBitmapImageToBitmap(imgB);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Out of memory");
            }
        }


        private void toolBarImages_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            int oldIndex = _index;
            if (_imageFileList.Count == 0)
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
                if (_index < _imageFileList.Count-1)
                {
                    _index++;
                }
            }
            else if (e.Button == this.toolBarButtonLast)
            {
                _index = _imageFileList.Count - 1;
            }

            if (oldIndex != _index)
            {
                try
                {
                    this.loadImage(_imageFileList[_index],max);
                    //System.Diagnostics.Process.Start(_imageFileList[_index], null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while loading internal Image Viewer. (" + ex.Message + ")", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
            }
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            this.toolBarImages.ImageList = this._currentToolbarImageList;
        }
    }
}