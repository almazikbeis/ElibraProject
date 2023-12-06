using Accord.Video;
using Accord.Video.FFMPEG;
using ELibra.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ELibra
{
    public partial class Video : Form
    {
        VideoFileSource source;
        AsyncVideoSource aSource;
        VideoSource videoSource;
        public string link;

        public Video(string url, ref List<VideoSource> videoSourceList, string linkedScales)
        {
            InitializeComponent();
            link = url;
            foreach (VideoSource item in videoSourceList)
                if (item.link == url)
                    videoSource = item;
            videoSource.aSource.NewFrame += Source_NewFrame;
            
            if (linkedScales != "")
            {
                this.Text = this.Text + " " + linkedScales;
            }
        }

        private void Source_VideoSourceError(object sender, VideoSourceErrorEventArgs eventArgs)
        {
            MessageBox.Show(eventArgs.Exception + Environment.NewLine + eventArgs.Description);
        }

        private void Source_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if ((Bitmap)eventArgs.Frame != null)
                {
                    pictureBox1.Image = new Bitmap((Bitmap)eventArgs.Frame.Clone(), this.Width - 10, this.Height - 40);
                }
                GC.Collect();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Video exception " + ex.Message + " " + ex.StackTrace);
            }
        }

        private void Video_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSource.aSource.NewFrame -= Source_NewFrame;
            GC.Collect();
        }
    }
}
