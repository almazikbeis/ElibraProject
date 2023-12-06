using Accord.Video;
using Accord.Video.FFMPEG;
using Accord.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Timers;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ELibra.Classes
{
    public class VideoSource : IDisposable
    {
        public string link;
        VideoFileSource source;
        public AsyncVideoSource aSource;

        public VideoSource(string url)
        {
            link = url;
            source = new VideoFileSource(url);
            source.FrameIntervalFromSource = false;
            source.FrameInterval = 25;
            aSource = new AsyncVideoSource(source);
            aSource.SkipFramesIfBusy = true;
            aSource.Start();
        }



        public void Dispose()
        {
            Console.WriteLine("Disposed");
            if (aSource.FramesReceived > 0)
                aSource.Stop();
        }
    }
}
