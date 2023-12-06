using ELibra.Classes.Structures;
using ELibra.DBModel;
using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELibra.Classes
{
    public class MetraHelper : WeightingBase
    {
        public event WeightChangeHandler WeightChanged;

        private Type devNet;
        public string licenseNumber;
        public double minWeight;
        public Scales scale;
        public List<VideoSource> videoSourceList;
        private Object objDevNet;
        private Thread workingThread;
        private bool isActive = false;

        public MetraHelper(ref List<VideoSource> source, int scaleId)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                videoSourceList = source;
                licenseNumber = (from i in context.Settings where i.id == 1 select i.licenseNumber).FirstOrDefault();
                scale = (from i in context.Scales
                         where i.id == scaleId
                         select i).FirstOrDefault();
                RefreshSettingsPort();
            }
        }

        public void StartReading()
        {
            try
            {
                devNet = Type.GetTypeFromProgID("DevNet.Drv");
                objDevNet = Activator.CreateInstance(devNet);

                object[] oneParameter = new object[1];
                oneParameter[0] = false;
                devNet.InvokeMember("Visible", BindingFlags.SetProperty, null, objDevNet, oneParameter);
                isActive = true;
                //StopReading();
                if ( workingThread != null)
                {
                    if (!workingThread.IsAlive)
                    {
                        StopReading();
                        StartThread();
                    }
                }
                else
                {
                    StartThread();
                }
                //workingThread = new Thread(RefreshWeightValue);
                //workingThread.Priority = ThreadPriority.Lowest;
                //workingThread.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно взаимодействие с весами! Проверьте подключение и выберите нужную модель в настройках программы.");
            }
      
        }

        protected void StartThread()
        {
            workingThread = new Thread(RefreshWeightValue);
            workingThread.Priority = ThreadPriority.Lowest;
            workingThread.Start();
        }

        public void StopReading()
        {
            try
            {
                if (workingThread != null)
                {
                    workingThread.Abort();
                }
            }
            catch
            {
                Console.WriteLine("can't stop");
            }
        }

        public void RefreshSettingsPort()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                minWeight = Double.Parse(scale.MinWeightVideo);
                var dataPort = (from i in context.Settings
                                where i.id == 1
                                select new { i.UrlVideoCamEnabled }).FirstOrDefault();

                arrVid.Clear();
                string[] enableCams = dataPort.UrlVideoCamEnabled.Split('|');
                string[] scaleCams = scale.LinkedCameras.Split('|');
                for (int i = 0; i < enableCams.Length; i++)
                {
                    for (int j = 0; j < scaleCams.Length; j++)
                    {
                        if (enableCams[i] != "" && scaleCams[j] != "" && enableCams[i] == scaleCams[j])
                        {
                            arrVid.Add(enableCams[i]);
                        }
                    }
                }
            }
            
            StartReading();
        }

        private double GetWeight()
        {
            if (objDevNet == null)
                return 0;

            byte ErrState, Flags0, Flags1, DFlags, DState;
            ErrState = Flags0 = Flags1 = DFlags = DState = 0;
            double d1 = 0.0;

            try
            {
                object[] parameters = new object[] { 1, 0, d1, ErrState, Flags0, Flags1, DFlags, DState };
                ParameterModifier[] mods = new ParameterModifier[] { new ParameterModifier(8) };
                mods[0][0] = false;
                mods[0][1] = false;
                mods[0][2] = true;
                mods[0][3] = true;
                mods[0][4] = true;
                mods[0][5] = true;
                mods[0][6] = true;
                mods[0][7] = true;
                devNet.InvokeMember("GetWeight", BindingFlags.InvokeMethod, null, objDevNet,
                    parameters, mods, null, null);
                var par6 = (byte)parameters[6] & 4;
                var par4 = (byte)parameters[4] & 4;
                double weight = ((double)parameters[2]) * 1000;

                return weight;
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно взаимодействие с весами! Проверьте подключение и выберите нужную модель в настройках программы.");
                return 0;
            }
        }

        public void SetWeightToZero()
        {
            try
            {
                object[] parameters = new object[] { 1 };
                ParameterModifier[] mods = new ParameterModifier[] { new ParameterModifier(1) };
                mods[0][0] = false;

                devNet.InvokeMember("SetToZero", BindingFlags.InvokeMethod, null, objDevNet,
                    parameters, mods, null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно взаимодействие с весами! Проверьте подключение и выберите нужную модель в настройках программы.");
            }
        }

        private bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public string IsSave()
        {
            return scale.isSave;
        }

        public void RefreshWeightValue()
        {
            try
            {
                while (isActive)
                {
                    using (DataBaseContext context = new DataBaseContext())
                    {
                        Weight = GetWeight();
                        WeightChanged(Weight, scale.name);
                        //Properties.Settings.Default.Save();
                        if (licenseNumber[0] != '0')
                        {
                            try
                            {
                                if (Weight >= minWeight)
                                {
                                    if (arrVid.Count() > 0 && arrVid[0] == "")
                                        arrVid.RemoveAt(0);
                                    if (!isRecord)
                                    {
                                        for (int i = 0; i < arrVid.Count; i++)
                                        {
                                            VideoSource videoSource = null;
                                            foreach (VideoSource item in videoSourceList)
                                                if (item.link == arrVid[i])
                                                    videoSource = item;
                                            arrRec.Add(new Recorder(arrVid[i], i, arrRec.Count == 0 ? true : false, ref videoSource, scale.nameUser));
                                        }
                                        if (arrRec.Count > 0)
                                            isRecord = true;
                                    }
                                    for (int i = 0; i < arrRec.Count; i++)
                                        arrRec[i].Weight = Weight;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    scale.isSave = "false";
                                    context.SaveChanges();
                                    isRecord = false;
                                    for (int i = 0; i < arrRec.Count; i++)
                                    {
                                        arrRec[i].isRecord = false;
                                        arrRec[i].AbortRecord();
                                    }
                                        
                                    
                                    arrRec.Clear();
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Cant catch weight");
                            }
                        }
                        Thread.Sleep(200);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }
    }
}
