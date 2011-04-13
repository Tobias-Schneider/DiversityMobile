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
using UBT.AI4.Toolbox.Controls;
using UBT.AI4.Bio.DivMobi.DataManagement;

namespace UBT.AI4.Bio.DivMobi.Forms
{
    public partial class DialogBase : Form
    {
        public DialogBase()
        {
            InitializeComponent();
            
           
        }

        protected void adjustControlSizes()
        {
            Rectangle r = Screen.PrimaryScreen.Bounds;
            Size s = new Size();
            
            try
            {
                if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("large"))
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.GetType().Equals(typeof(ClickComboBox)) || c.GetType().Equals(typeof(ListView)))
                        {
                            c.Font = new Font(c.Font.Name, 14, c.Font.Style);
                        }
                        
                    }
                }
                else if (UserProfiles.Instance.Current.ToolbarIcons.ToLower().Equals("medium"))
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.GetType().Equals(typeof(ClickComboBox)) || c.GetType().Equals(typeof(ListView)))
                        {
                            c.Font = new Font(c.Font.Name, 12, c.Font.Style);
                            
                        }
                       
                    }

                }
                else
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.GetType().Equals(typeof(ClickComboBox)) || c.GetType().Equals(typeof(ListView)))
                        {
                            c.Font = new Font(c.Font.Name, 10, c.Font.Style);
                        }
                        
                    }
                    
                }
                //r.Width = Math.Max(r.Width, this.Size.Width);
                //r.Height = Math.Max(r.Height, this.Size.Height);
                //s = new Size(r.Width, this.ClientSize.Height);
                //this.ClientSize = s;
            }
               
            catch (ConnectionCorruptedException)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.GetType().Equals(typeof(ClickComboBox)))
                    {
                        ClickComboBox combo = (ClickComboBox)c;
                        combo.Font = new Font(c.Font.Name, 12, c.Font.Style);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(
                new Pen(Color.Black), 0, 0, this.Size.Width - 1, this.Size.Height - 1);
                //0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
                //0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
        }
    }
}