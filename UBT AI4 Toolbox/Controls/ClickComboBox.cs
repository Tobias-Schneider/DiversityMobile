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
