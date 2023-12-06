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


namespace ELibra.Classes
{ 
    class DetectorLicensePlate : IDisposable
    {
        public delegate void LicensePlateChangeHandler(string plate, int num);
        public event LicensePlateChangeHandler LicensePlateChanged;
        private bool isDetecting = true;
        private int num;
        Stopwatch startTime;
        double delayTime;
        string type;
        VideoSource videoSource;
        System.Timers.Timer decode_timer;
        Bitmap frame;

        public DetectorLicensePlate(string url, int num, ref List<VideoSource> videoSourceList)
        {
            this.num = num;

            foreach (VideoSource item in videoSourceList)
                if (item.link == url)
                    videoSource = item;
            startTime = Stopwatch.StartNew();
            using (DBModel.DataBaseContext context = new DBModel.DataBaseContext())
            {
                delayTime = (from i in context.Settings
                             where i.id == 1
                             select i.RecognizeDelay).FirstOrDefault() ?? 0;
            }
            videoSource.aSource.NewFrame += Source_NewFrame;

            Thread t = new Thread(Work);
            t.Start();


        }

        private void decode_timer_Tick(object sender, EventArgs e)
        {
            if (frame != null)
            {
                string plate = QrHelper.decode(frame);
                LicensePlateChanged(plate, num);
            }
        }

        private void Work()
        {
            while (isDetecting)
            {
                try
                {
                    string plate = ParseText();
                    if (plate != "")
                        LicensePlateChanged(plate, num);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void Source_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                if ((delayTime * 1000) < startTime.ElapsedMilliseconds)
                {
                    if (!File.Exists("recognaize\\images\\" + num + ".jpg"))
                    {
                        Console.WriteLine(num);
                        Bitmap tmpImg1 = eventArgs.Frame;
                        tmpImg1.Save("recognaize\\images\\" + num + ".jpg");
                        tmpImg1.Dispose();
                        GC.Collect();
                    }                  
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("GDI+\n"+ex.Message);
            }
        }

        private void QR_Source_New_Frame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                frame = eventArgs.Frame;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GDI+\n" + ex.Message);
            }
        }


        private string ParseText()
        {
            string[] output;
            string result = string.Empty;
            try
            {
                while (!File.Exists("recognaize\\" + num))
                    Thread.Sleep(100);
                output = File.ReadAllText("recognaize\\" + num).Split('|');
                for (int i = 0; i < output.Length; i++)
                    if (output[i] != "lx" && output[i] != "")
                        result = output[i];
            }
            finally
            {
                File.Delete("recognaize\\" + num);
            }
            return result;
        }

        public void Dispose()
        {
            GC.Collect();
            isDetecting = false;
            startTime.Stop();
            startTime = null;
            videoSource.aSource.NewFrame -= Source_NewFrame;
            //if (decode_timer.Enabled)
            //    decode_timer.Stop();
            //if (type == "plate")
            //{
            //    videoSource.aSource.NewFrame -= Source_NewFrame;
            //}
            //else
            //{
            //    videoSource.aSource.NewFrame -= QR_Source_New_Frame;
            //}
        }
    }
}
