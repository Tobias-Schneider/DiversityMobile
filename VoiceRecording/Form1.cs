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
namespace VoiceRecording
{
    public partial class Form1 : Form
    {
        Recorder rc;
        FileStream st;
        private string tempVoicePath = "VoiceNote2.wav";

        public Form1()
        {
            InitializeComponent();
            rc = new Recorder();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 20; i++)
            {
                try
                {
                    String s = i + tempVoicePath;
                    st = new FileStream(s, FileMode.Create, FileAccess.ReadWrite);
                    rc = new Recorder(i);
                    rc.RecordFor(st, 10);
                    st.Flush();
                }
                catch(Exception f)
                {
                    String s=f.Message;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Player pl = new Player();
            st = new FileStream(tempVoicePath, FileMode.Open, FileAccess.ReadWrite);
            pl.Play(st);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Record();
            }
            catch (Exception f)
            {
                String s = f.Message;
            }
        }

        private void Record()
        {
           
                if (rc.Recording)
                {
                    //Stop the recording
                    this.rc.Stop();
                }
                else
                {
                    //Create a new stream for the recording
                    this.st = new FileStream(this.tempVoicePath, FileMode.Create, FileAccess.ReadWrite);

                    //start a new recording
                    this.button3.Text = "Stop";
                    rc.DoneRecording += new WaveFinishedHandler(rec_DoneRecording);
                    rc.RecordFor(this.st, 20);
                }
        }
        private void rec_DoneRecording()
        {

            //Unregister from the doneRecording event
            rc.DoneRecording -= new WaveFinishedHandler(rec_DoneRecording);
            this.button3.Text = "Record";
            
        }

    }
}