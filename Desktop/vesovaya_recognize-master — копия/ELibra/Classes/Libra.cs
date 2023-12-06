using Accord;
using Accord.Video;
using Accord.Video.DirectShow;
using Accord.Video.FFMPEG;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using ELibra.DBModel;
using ELibra.DBModel.Models;
using ELibra.Classes.Structures;

namespace ELibra.Classes
{
    public class Libra : WeightingBase
    {
        public event WeightChangeHandler WeightChanged;
        public double minWeight;
        public string licenseNumber;
        public List<VideoSource> videoSourceList;
        public Scales scale;
        public string scaleName;
        ScalesTypes scalesType;

        public Libra(ref List<VideoSource> source, int scaleId, ScalesTypes scalesType)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                videoSourceList = source;
                licenseNumber = (from i in context.Settings where i.id == 1 select i.licenseNumber).FirstOrDefault();
                scale = (from i in context.Scales
                        where i.id == scaleId
                        select i).FirstOrDefault();
                this.scalesType = scalesType;
                RefreshSettingsPort();
            }
        }

        public void RefreshSettingsPort()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                minWeight = Double.Parse(scale.MinWeightVideo);

                #region Получение данных о порте
                var dataPort = (from i in context.Settings
                                where i.id == 1
                                select new { i.UrlVideoCamEnabled }).FirstOrDefault();
                #endregion

                if (!string.IsNullOrEmpty(scale.PortName))
                {
                    #region Настройка порта
                    sp.Close();
                    sp.PortName = scale.PortName;
                    sp.BaudRate = Int32.Parse(scale.BaudRate);
                    switch (scale.Parity)
                    {
                        case "Нет":
                            sp.Parity = Parity.None;
                            break;
                        case "Нечет":
                            sp.Parity = Parity.Odd;
                            break;
                        case "Чет":
                            sp.Parity = Parity.Even;
                            break;
                        case "Маркер (1)":
                            sp.Parity = Parity.Mark;
                            break;
                        case "Пробел (0)":
                            sp.Parity = Parity.Space;
                            break;
                    }
                    switch (scale.StopBits)
                    {
                        case "1":
                            sp.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            sp.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            sp.StopBits = StopBits.Two;
                            break;
                    }
                    sp.DataBits = Int32.Parse(scale.DataBits);
                    switch (scale.Handshake)
                    {
                        case "Нет":
                            sp.Handshake = Handshake.None;
                            break;
                        case "Аппаратное":
                            sp.Handshake = Handshake.RequestToSend;
                            break;
                        case "Аппаратное и Xon/Xoff":
                            sp.Handshake = Handshake.RequestToSendXOnXOff;
                            break;
                        case "Xon/Xoff":
                            sp.Handshake = Handshake.XOnXOff;
                            break;
                    }
                    sp.NewLine = "\r\n";
                    sp.RtsEnable = Boolean.Parse(scale.RtsEnable);
                    sp.DataReceived += Sp_DataReceived;
                    
                    #endregion

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

                    //arrVid.AddRange(dataPort.UrlVideoCamEnabled.Split('|'));




                    #region Попытка подключения к порту
                    try
                    {
                        if (!sp.IsOpen)
                        {
                            try
                            {
                                sp.Open();
                            }
                            catch { }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось подключиться к индикатору." + Environment.NewLine + " Запустите программу заново");
                    }
                    #endregion
                }
            }
        }
        public double CASData(string data)
        {
            string subdata = data.Substring(9).Replace(" ", "").Replace("kg", "").Replace('.', ',');
            double doubleData;
            double.TryParse(subdata, out doubleData);
            //Console.WriteLine((int)doubleData);
            return doubleData;
        }

        public double LibraData(string data)
        {
            string subdata = data.Substring(1).Replace("KG", "").Replace("M", "");
            //Console.WriteLine(subdata);

            double doubleData;
            double.TryParse(subdata, out doubleData);
            //Console.WriteLine(doubleData);
            return doubleData;
        }

        public string IsSave()
        {
            return scale.isSave;
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    double CurrentWeight;
                    //bool otr = false;
                    string data = sp.ReadLine();
                    Thread.Sleep(10);
                    //data = data.Substring(1).Trim();
                    //if (data[0] == '-')
                    //    otr = true;

                    if (!string.IsNullOrEmpty(data))
                    {
                        if (scalesType == ScalesTypes.Libra)
                            CurrentWeight = LibraData(data);
                        else if (scalesType == ScalesTypes.CAS)
                            CurrentWeight = CASData(data);
                        else
                            CurrentWeight = 0;
                        //data = data.Substring(1).Trim();
                        //if (data.Length >= 8)
                        //{
                            //string weightString = otr ? data.Substring(0, 7) : data.Substring(0, 6);
                            //if (Double.TryParse(weightString, out CurrentWeight))
                            //{
                                if (CurrentWeight != Weight)
                                {
                                    Weight = CurrentWeight;
                                    //if (otr)
                                    //    Weight *= -1;
                                    WeightChanged(Weight, scale.name);
                                    if (licenseNumber[0] != '0')
                                    {
                                        try
                                        {
                                            if (Weight >= minWeight)
                                            {

                                                if (arrVid.Count() >0 && arrVid[0] == "")
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
                                            }
                                            else
                                            {
                                                //var setting = (from i in context.Settings
                                                //              select i).FirstOrDefault();
                                                scale.isSave = "false";
                                                //setting.isSave = "false";
                                                context.SaveChanges();
                                                isRecord = false;
                                                for (int i = 0; i < arrRec.Count; i++)
                                                    arrRec[i].isRecord = false;
                                                arrRec.Clear();
                                            }
                                        }
                                        catch 
                                        {
                                            Console.WriteLine("Cant catch weight");
                                        }
                                    }
                                }
                            //}
                        //}
                    }
                }               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}