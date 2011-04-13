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
using OpenNETCF.Media.WaveAudio;


using System.IO;
using System.Threading;

namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    public partial class RecordForm : DialogBase
    {
        Recorder rc;
        FileStream st;
        private string voicePath;

        public RecordForm(String path):base()
        {
            InitializeComponent();
            base.adjustControlSizes();
            rc = new Recorder();
            if(path != null)
                voicePath = path+@"\Temporary.wav";
            else
                voicePath = @"\Temporary.wav";
        }

        private void Record()
        {
            if (rc != null)
            {
                try
                {
                    if (rc.Recording)
                    {
                        //Stop the recording
                        this.rc.Stop();
                    }
                    else
                    {
                        //Create a new stream for the recording
                        this.st = new FileStream(this.voicePath, FileMode.Create, FileAccess.ReadWrite);
                        //start a new recording
                        this.buttonRecord.Text = "Stop";
                        rc.DoneRecording += new WaveFinishedHandler(rec_DoneRecording);
                        rc.RecordFor(this.st, 40);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while recording. ("+ex.Message+")");
                }
            }
        }

        private void rec_DoneRecording()
        {
            if (rc != null)
            {
                //Unregister from the doneRecording event
                rc.DoneRecording -= new WaveFinishedHandler(rec_DoneRecording);
                this.buttonRecord.Enabled = false;
                this.buttonRecord.Text = "Recording finished";
                this.buttonOK.Enabled = true;
            }
        }

        private void buttonRecord_Click(object sender, EventArgs e)
        {
            try
            {
                this.Record();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        public String SavePath
        {
            get { return this.voicePath; }}

        private void RecordForm_Closing(object sender, CancelEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        private void RecordForm_Closed(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }
    }
}