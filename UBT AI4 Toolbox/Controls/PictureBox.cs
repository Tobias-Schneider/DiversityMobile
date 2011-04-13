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
using System.Drawing;
using System.Windows.Forms;

using UBT.AI4.Toolbox.Imaging;

namespace UBT.AI4.Toolbox.Controls
{
    public class PictureBox : Control
    {
        private string _filename;
        private IImage _picture;

        public PictureBox()
        {
            
        }

        public string Image
        {
            get { return this._filename; }
            set 
            {
                if (this._filename != value)
                {
                    this._filename = value;
                    this.ChangeImage();
                }

            }
        }

        protected void ChangeImage()
        {
            if (this._picture != null)
            {
                this._picture = null;    
            }
            try
            {
                IImagingFactory factory = (IImagingFactory)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("327ABDA8-072B-11D3-9D7B-0000F81EF32E")));
                factory.CreateImageFromFile(this.Image, out this._picture);
                this.thumb = null;                
            }
            catch
            {
                return;
            }

            this.Refresh();
        }


        private IImage thumb = null;

        protected override void OnPaint(PaintEventArgs e)
        {
            // draw image
            if (this._picture != null)
            {
                try
                {
                    // The bitmap needs to be created with the 32bpp pixel format for the IImage to do the right thing.
                    using (Bitmap backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                    {
                        using (Graphics gxBuffer = Graphics.FromImage(backBuffer))
                        {
                            gxBuffer.Clear(this.BackColor);
                            IntPtr hdcDest = gxBuffer.GetHdc();

                            Rectangle dstRect = new Rectangle(0, 0, this.Width, this.Height);

                            // Ask the Image object from Imaging to draw itself.
                            //IImage thumb;
                            if (this.thumb == null)
                            {
                                try
                                {
                                    if (this._picture.GetThumbnail((uint)this.Width, (uint)this.Height, out thumb) != 0)
                                    {
                                        MessageBox.Show("MemoryError!");
                                        thumb = null;
                                        return;
                                    }
                                }
                                catch
                                {
                                    thumb = null;
                                    this._picture = null;
                                    return;
                                }
                            }

                            thumb.Draw(hdcDest, ref dstRect, IntPtr.Zero);

                            gxBuffer.ReleaseHdc(hdcDest);

                            // Put the final composed image on screen.
                            e.Graphics.DrawImage(backBuffer, 0, 0);
                        }
                    }
                }
                catch
                {
                    //MessageBox.Show("MemoryError!");
                    return;
                } 
            }

            // call function of super class to ensure correct handling of drawing event
            base.OnPaint(e);
        }
        
    }
}
