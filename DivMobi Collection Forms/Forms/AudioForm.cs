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