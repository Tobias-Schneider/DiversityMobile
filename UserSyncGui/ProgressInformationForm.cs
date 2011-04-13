using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserSyncGui
{
    public partial class ProgressInformationForm : Form
    {
        // This delegate enables asynchronous calls for setting
        // the text property on labelProgressInformation.
        delegate void SetTextCallback(string text);

        // This delegate enables asynchronous calls for setting
        // the value property on progressbar.
        delegate void SetValueCallback(int value);

        // This delegate enables asynchronous calls for setting
        // the enabled property on buttonClose.
        delegate void SetEnabledCallback(bool enabled);

        // This delegate enables asynchronous calls for setting
        // the enabled property on buttonClose.
        delegate void SetCursorCallback(bool cursorState);

        public ProgressInformationForm()
        {
            InitializeComponent();
            this.buttonClose.Enabled = false;
            this.labelActionInformation.Text = "";
            this.labelProgressInformation.Text = "";
        }

        public void setActionInformation(String text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelActionInformation.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setActionInformation);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelActionInformation.Text = text;
            }
        }

        public void setAdditionalInformation(String text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelProgressInformation.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setAdditionalInformation);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelProgressInformation.Text = text;
            }
        }

        public void setValue(int value)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.progressBarProgressInformation.InvokeRequired)
            {
                SetValueCallback d = new SetValueCallback(setValue);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                this.progressBarProgressInformation.Value = value;
            }
        }

        public void enableCloseButton(bool enable)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.progressBarProgressInformation.InvokeRequired)
            {
                SetEnabledCallback d = new SetEnabledCallback(enableCloseButton);
                this.Invoke(d, new object[] { enable });
            }
            else
            {
                this.buttonClose.Enabled = enable;
            }
        }

        public void setCursorState(bool cursorState)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.progressBarProgressInformation.InvokeRequired)
            {
                SetCursorCallback d = new SetCursorCallback(setCursorState);
                this.Invoke(d, new object[] { cursorState });
            }
            else
            {
                this.UseWaitCursor = cursorState;
            }
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
