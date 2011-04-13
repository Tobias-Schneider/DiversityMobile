using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WMPLib;


namespace UBT.AI4.Bio.DiversityCollection.Ressource.MediaPlayer
{
    public partial class MediaPlayer : Form
    {
        WMPLib.WindowsMediaPlayer player = new WindowsMediaPlayer();

        public MediaPlayer()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            DialogResult DlgResult;
            DlgResult = MessageBox.Show("Do you want to close MusicBoy Media Player?", "Close MediaBoy", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (DlgResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            DialogResult DlgResult;
            DlgResult = MediaFileDialogue.ShowDialog();
            if (DlgResult == DialogResult.OK)
            {
                MediaToost.Caption = "Playing selected media";
                MediaToost.InitialDuration = 5;
                MediaToost.Text = "Now playing: " + MediaFileDialogue.FileName;
                MediaToost.Visible = true;

                player.URL = MediaFileDialogue.FileName;
                player.settings.volume = 100;
                player.controls.play();
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (PlayButton.Text == "Play")
            {
                player.settings.volume = 100;
                player.controls.play();
                PlayButton.Text = "Pause";
            }
            else
            {
                player.controls.play();
                PlayButton.Text = "Play";
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            player.controls.stop();
        }
    }
}

