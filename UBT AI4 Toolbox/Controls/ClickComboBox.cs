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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;

namespace UBT.AI4.Toolbox.Controls
{
    public class ClickComboBox : ComboBox
    {
        const int CB_GETDROPPEDSTATE = 0x0157;
        const int CB_SHOWDROPDOW = 0x014F;

        public ClickComboBox():base()
        {
        }

        public bool DroppedDown
        {
            get
            {
                Message comboBoxMessage = Message.Create(
                this.Handle, CB_GETDROPPEDSTATE,
                IntPtr.Zero, IntPtr.Zero);
                MessageWindow.SendMessage(ref comboBoxMessage);
                return comboBoxMessage.Result != IntPtr.Zero;
            }
            set
            {
                Message comboBoxMessage = Message.Create(
                this.Handle, CB_SHOWDROPDOW,
                new IntPtr(value==true ? 1:0), IntPtr.Zero);
                MessageWindow.SendMessage(ref comboBoxMessage);
            }
        }
       

        protected override void OnClick(EventArgs e)
        {
            if (this.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                // Close / Open DropDownList in accordance with current status
                this.DroppedDown = !this.DroppedDown;
            }

            base.OnClick(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (this.DropDownStyle == ComboBoxStyle.DropDown)
            {
                // User clicked ENTER
                if (e.KeyChar == 10) 
                {
                    // Close / Open DropDownList in accordance with current status
                    this.DroppedDown = !this.DroppedDown;
                    e.Handled = true;
                    return;
                }
            }
            base.OnKeyPress(e);
        }
        
    }
}
