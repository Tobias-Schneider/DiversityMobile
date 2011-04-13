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
using UBT.AI4.Bio.DivMobi.Forms;


using System.IO;
using System.Threading;

namespace UBT.AI4.Bio.DivMobi.Forms.Forms
{
    public partial class AudioForm : DialogBase
    {
        private string voicePath;
        Player pl;

        public AudioForm(String path):base()
        {
            InitializeComponent();
            base.adjustControlSizes();
            if (path != null)
                voicePath = path;
            else
                voicePath = "";
            pl = new Player();
        }

        private void Play()
        {
        //    if (pl.Playing)
        //    {
        //        pl.Stop();
        //        this.buttonPlay.Text = "Play";
        //    }
        //    else
        //    {
        //        this.buttonPlay.Text = "Stop";
        //        FileStream st = new FileStream(voicePath, FileMode.Open, FileAccess.ReadWrite);
        //        pl.Play(st);
        //        pl.DonePlaying += new WaveDoneHandler(player_DonePlaying);
            //}
        }

        void player_DonePlaying(object sender, IntPtr wParam, IntPtr lParam)
        {
            this.buttonPlay.Text = "Play";
        }


        private void buttonPlay_Click(object sender, EventArgs e)
        {
            this.Play();
        }

    }
}