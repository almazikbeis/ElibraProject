using ELibra.DBModel;
using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace ELibra.Classes
{
    public class CasHelper: WeightingBase
    {
        public event WeightChangeHandler WeightChanged;
        public double minWeight;
        public string licenseNumber;
        public List<VideoSource> videoSourceList;
        public Scales scale;
        public string scaleName;
        System.Timers.Timer timer;
        Ci2001ALib.Indic weight;

        public CasHelper(ref List<VideoSource> source, int scaleId)
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
                   weight = new Ci2001ALib.Indic();
                    try
                    {
                        Regex regex = new Regex(@"\d+");
                        Match match = regex.Match(scale.PortName);
                        weight.NumberOfCom = int.Parse(match.Value);

                        weight.Open();
                        timer = new System.Timers.Timer();
                        timer.Elapsed += new ElapsedEventHandler(OnTimerEvent);
                        timer.Interval = 100;

                    }
                    catch (Exception ex )
                    {
                        MessageBox.Show("Не удалось подключиться к COM порту");
                    }
                    
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
                }
            }
        }

        public void OnTimerEvent(object source, ElapsedEventArgs e)
        {
            weight.Update();
        }
    }
}
