using ELibra.Classes;
using ELibra.DBModel;
using ELibra.DBModel.Models;
using ELibra.DBModel.ModelsTimeString;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using static ELibra.Classes.WeightingBase;
using ELibra.Classes.UIElements;
using ELibra.Classes.Structures;
using ELibra.Properties;

namespace ELibra
{
    public partial class Main : Form
    {
        public WeightingBase libra;
        public List<VideoSource> videoSourceList = new List<VideoSource>();
        private bool close = false;
        public double weight;
        List<Video> activeVideoForms = new List<Video>();
        public List<scaleMainPanel> scalesList = new List<scaleMainPanel>();
        private System.Windows.Forms.NotifyIcon notifyIcon;
        #region Классы форм
        public CounterParties frmCounterParties = null;
        public Cars frmCars = null;
        public Users frmUsers = null;
        public Cargos frmCargos = null;
        public Stocks frmStocks = null;
        public Drivers frmDrivers = null;
        public Carriers frmCarriers = null;
        public Consignees frmConsignees = null;
        public Consignors frmConsignors = null;
        public Weighing frmWeighing = null;
        public Journal frmJournal = null;

        #endregion

        public Main()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            InitializeComponent();
            KillAnyProcceses();
            bool license = false;
            this.Hide();
            if (File.Exists("updated"))
            {
                string[] file = File.ReadAllLines("updated");
                if (file.Any())
                {
                    if (!file[1].Contains("1"))
                    {
                        Classes.Update.AddWeigingColumn.AddScaleNameColumn();
                        Classes.Update.AddWeigingColumn.AddRedactorColumn();
                        file[1] = file[1] + "1";
                    }
                    if (!file[2].Contains("1"))
                    {
                        Classes.Update.UpdateDB.UpdateAll();
                        file[2] = file[2] + "1";
                    }
                    if (!file[3].Contains("1"))
                    {
                        Classes.Update.EditCars.RemoveSpace();
                        file[3] = file[3] + "1";
                    }
                    if (!file[4].Contains("1"))
                    {
                        Classes.Update.AddSetingsColumn.AddDelayColumn();
                        file[4] = file[4] + "1";
                    }
                    if (!file[5].Contains("1"))
                    {
                        Classes.Update.CreateClientTable.Create();
                        file[5] = file[5] + "1";
                    }
                    if (!file[6].Contains("1"))
                    {
                        Classes.Update.UsersCyphering.Cipher();
                        file[6] = file[6] + "1";
                    }
                    if (!file[7].Contains("1"))
                    {
                        Classes.Update.CreateScalesTable.Create();
                        file[7] = file[7] + "1";
                    }
                    if (!file[8].Contains("1"))
                    {
                        Classes.Update.RecognizeType.AddTypeColumn();
                        file[7] = file[8] + "1";
                    }

                    File.WriteAllLines("updated", file);
                    Console.WriteLine("Успешно");
                }
            }

            using (DataBaseContext context = new DataBaseContext())
            {
                #region Проверка логина и пароля
                var settingData = (from i in context.Settings
                                   where i.id == 1
                                   select i).FirstOrDefault();
                
                var hosts = settingData.HostDB.Split('|');
                if (hosts.Count() == 1)
                {
                    settingData.HostDB = hosts[0] + "|" + hosts[0];
                    context.SaveChanges();
                }

                if (!string.IsNullOrEmpty(settingData.LoginDB))
                {
                    try
                    {
                        serverHelp.getToken(settingData.LoginDB, settingData.PasswordDB);
                    }
                    catch (WebException ex)
                    {
                        if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                        {
                            var resp = (HttpWebResponse)ex.Response;
                            if (resp.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                firstLogin frmFLogin = new firstLogin();
                                frmFLogin.ShowDialog();
                            }
                        }
                        //if (ex.Status == WebExceptionStatus.ConnectFailure && ex.Response == null)
                        //{
                        //    firstLogin frmFLogin = new firstLogin();
                        //    frmFLogin.ShowDialog();
                        //}
                    }
                }
                else
                {
                    firstLogin frmFLogin = new firstLogin();
                    frmFLogin.ShowDialog();
                }
                #endregion
                var isLogged = (from i in context.Settings
                                select i.LoginDB).FirstOrDefault();
                if (isLogged == "")
                {
                    Application.Exit();
                    Close();
                    close = true;
                }
                else
                {
                    CheckURLVideo();
                    StartLibra();

                    var s = (from i in context.Settings
                             where i.id == 1
                             select i).FirstOrDefault();

                    #region Считывание id лецинзионной флешки
                    if (File.Exists("license"))
                    {
                        s.licenseId = File.ReadAllText("license");
                        File.Delete("license");
                    }
                    #endregion

                    #region Обновление данных лицензии
                    try
                    {
                        List<string> licenseData = serverHelp.getLicense(serverHelp.getToken(s.LoginDB, s.PasswordDB));
                        s.licenseNumber = licenseData[0];
                        s.NameCompany = licenseData[1];
                        s.licenseDate = DateTime.Parse(licenseData[2]);
                    }
                    catch { }
                    #endregion

                    #region Вход в программу
                    Login frmLogin = new Login();
                    if (frmLogin.ShowDialog() == DialogResult.OK)
                    {
                        s.Role = frmLogin.roleId.ToString();
                    }
                    //s.isSave = "false";
                    var scales = from i in context.Scales select i;
                    foreach (var scale in scales)
                    {
                        scale.isSave = "false";
                    }
                    context.SaveChanges();
                    #endregion

                    this.Show();

                    #region Проверка наличия лецинзионной флешки
                    if (s.licenseDate != null)
                    {
                        if (s.licenseDate > DateTime.Now)
                        {
                            ManagementObjectSearcher theSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
                            foreach (ManagementObject currentObject in theSearcher.Get())
                            {
                                ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
                                if (currentObject["SerialNumber"].ToString() == s.licenseId)
                                    license = true;
                            }
                        }
                    }

                    if (!license)
                    {
                        MessageBox.Show("Лицензия отсутствует либо не подключена флешка");
                        close = true;
                        Application.Exit();
                    }
                    #endregion

                    #region Проверка XML документа
                    Classes.CheckXML.RemoveExpiredDate();
                    #endregion

                    #region Добавление программы в автозапуск
                    RegistryKey reg;
                    reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
                    try
                    {
                        reg.SetValue("ELibra", "\"" + Application.ExecutablePath + "\"");
                        reg.Close();
                    }
                    catch { }
                    #endregion

                    #region Запуск распознования
                    if (true)
                    {
                        string pythonPath = "";
                        string[] paths = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User).Split(';');
                        foreach (string path in paths)
                            if (path.EndsWith(@"Python36\"))
                                pythonPath = path + "python.exe";
                        if (pythonPath != "")
                        {
                            var prc = Process.GetProcessesByName("Python");
                            if (prc.Length == 0)
                            {

                                Process proc = new Process();
                                proc.StartInfo = new ProcessStartInfo(pythonPath, "script.py")
                                {
                                    UseShellExecute = false,
                                    CreateNoWindow = true,
                                    WorkingDirectory = Environment.CurrentDirectory + "\\recognaize"
                                };
                                proc.Start();

                            }
                        }
                        else
                        {
                            MessageBox.Show("Python не добавлен в системные переменные");
                        }
                    }
                    #endregion

                    backgroundWorker.RunWorkerAsync();
                    CheckURLVideo();
                }
                
            }

        }


        private void KillAnyProcceses()
        {
            try
            {
                Process[] prc = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                foreach (Process p in prc)
                    if (p.Id != Process.GetCurrentProcess().Id)
                        p.Kill();
            }
            catch { }
        }

        private void RestartLibra()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                foreach (var scale in scalesList)
                {
                    if (scale.Model == "Rus")
                    {
                        (scale.Libra as MetraHelper).StopReading();
                        (scale.Libra as MetraHelper).WeightChanged -= Libra_WeightChanged;
                    }
                    else
                    {
                        (scale.Libra as Libra).WeightChanged -= Libra_WeightChanged;
                    }
                    //string model = (from i in context.Scales
                    //                where i.name == scale.Name
                    //                select i.Model).FirstOrDefault();

                    //switch (model)
                    //{
                    //    case "Rus":
                    //        (scale.Libra as MetraHelper).WeightChanged -= Libra_WeightChanged;
                    //        break;
                    //    default:
                    //        (scale.Libra as Libra).WeightChanged -= Libra_WeightChanged;
                    //        break;
                    //}
                
                    scale.Libra.sp.Dispose();
                    scale.Libra = null;
                    panelScales.Controls.Remove(scale.ScalePanel.scales.scalePanel);

                    scale.ScalePanel.Dispose();
                }
            }
            
            scalesList.Clear();

            StartLibra();

            if (frmWeighing != null)
            {
                frmWeighing.NewScaleSelected();
            }
        }

        private void StartLibra()
        {
            lblWeight.Text = 0.ToString(" 000000;-000000");
            using (DataBaseContext context = new DataBaseContext())
            {
                var scales = from i in context.Scales
                             select i;
                System.Drawing.Point point = new System.Drawing.Point(416, 3);
                int currentScale = 0;

                foreach (var scale in scales)
                {
                   
                    ScalesMainForm scalePanel = new ScalesMainForm();
                    scalePanel.Initialize(scale, currentScale);
                    scalePanel.scales.scaleButton.MouseClick += (sender, EventArgs) => { scaleButton_Click(sender, EventArgs, scale); };
                    if (scales.Count() > 1)
                    {
                        this.panelScales.Controls.Add(scalePanel.scales.scalePanel);
                    }
                    WeightingBase weightingBase = new WeightingBase();
                    switch (scale.Model)
                    {
                        case "CAS":
                            weightingBase = new Libra(ref videoSourceList, scale.id, ScalesTypes.CAS);
                            (weightingBase as Libra).WeightChanged += Libra_WeightChanged;
                            if (currentScale == 0)
                            {
                                scalePanel.scales.scaleButton.Tag = "green";
                                scalePanel.scales.scaleButton.BackgroundImage = Properties.Resources.green;
                                scalePanel.scales.scaleButton.Enabled = false;
                            }

                            break;

                        case "Rus":
                            weightingBase = new MetraHelper(ref videoSourceList, scale.id);
                            (weightingBase as MetraHelper).WeightChanged += Libra_WeightChanged;
                            if (currentScale == 0)
                            {
                                scalePanel.scales.scaleButton.Tag = "green";
                                scalePanel.scales.scaleButton.BackgroundImage = Properties.Resources.green;
                                scalePanel.scales.scaleButton.Enabled = false;
                            }
                            break;

                        default:
                            weightingBase = new Libra(ref videoSourceList, scale.id, ScalesTypes.Libra);
                            (weightingBase as Libra).WeightChanged += Libra_WeightChanged;
                            if (currentScale == 0)
                            {
                                scalePanel.scales.scaleButton.Tag = "green";
                                scalePanel.scales.scaleButton.BackgroundImage = Properties.Resources.green;
                                scalePanel.scales.scaleButton.Enabled = false;
                            }
                            break;
                            //case "Rus":
                            //    libra = new MetraHelper();
                            //    (libra as MetraHelper).WeightChanged += Libra_WeightChanged;
                            //    break;
                    }
                    scalesList.Add(new scaleMainPanel
                    {
                        Name = scale.name,
                        NameUser = scale.nameUser,
                        ScalePanel = scalePanel,
                        Libra = weightingBase,
                        Cameras = scale.LinkedCameras != null ? scale.LinkedCameras.Split('|') : new string[] { "" },
                        Active = currentScale == 0 ? true : false,
                        Model = scale.Model
                    });
                    currentScale++;
                }
                var maxWeight = int.Parse((from i in context.Settings where i.id == 1 select i.MaxWeight).FirstOrDefault()) * 1000;
                progressBar.Maximum = progressBar.Maximum < maxWeight ? maxWeight : progressBar.Maximum;
            }
        }


        private void Libra_WeightChanged(double weight, string scaleName)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new WeightChangeHandler(Libra_WeightChanged), new object[] { weight, scaleName });
                return;
            }
            else
            {
                this.weight = weight;
                scalesList.Find(x => x.Name == scaleName).ScalePanel.scales.scaleWeightLable.Text = weight.ToString(" 000000;-000000");
                if (scalesList.Find(x => x.Name == scaleName).Active == true)
                {
                    lblWeight.Text = weight.ToString(" 000000;-000000");
                    try
                    {
                        if (weight >= 0)
                            progressBar.Value = Int32.Parse(weight.ToString().Split('.')[0]);
                        else
                            progressBar.Value = 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
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

        private void CheckURLVideo()
        {
            #region Добавление полей по числу активных камер
            List<string> enabledCams = new List<string>();
            using (DataBaseContext context = new DataBaseContext())
            {
                var s = (from i in context.Settings
                         where i.id == 1
                         select i).FirstOrDefault();
                s.UrlVideoCamEnabled = "";
                s.UrlVideoCamNumEnabled = "";
                VideosToolStripMenuItem.DropDownItems.Clear();
                ToolStripMenuItem checkUrl = new ToolStripMenuItem();
                checkUrl.Name = "toolStripMenuItemCheckUrl";
                checkUrl.Text = "Проверить подключенные камеры";
                checkUrl.Click += (o, e) => { CheckURLVideo(); };
                VideosToolStripMenuItem.DropDownItems.Add(checkUrl);
                if (!string.IsNullOrEmpty(s.UrlVideoCam))
                {

                    List<string> camArr = s.UrlVideoCam.Split('|').ToList();
                    int k = camArr.Count();
                    for (int i = 0; i < k; i++)
                    {
                        Uri uriResult;
                        bool result = true;
                        if (!Uri.TryCreate(camArr[i], UriKind.Absolute, out uriResult) || null == uriResult)
                            result = false;
                        if (result)
                        {
                            if (!PingHost(new Uri(camArr[i]).Host))
                            {
                                camArr.RemoveAt(i);
                                i--;
                                k--;
                            }
                            else if (!VideosToolStripMenuItem.DropDownItems.ContainsKey(camArr[i]))
                            {
                                ToolStripItem toolStrip = new ToolStripMenuItem();
                                toolStrip.Name = camArr[i];
                                bool notAdd = true;


                                foreach (var scale in scalesList)
                                { 
                                    bool has = Array.Exists(scale.Cameras, element => element == camArr[i]);
                                    if (has)
                                    {
                                        toolStrip.Tag = scale.NameUser;
                                        toolStrip.Text = scale.NameUser + ": " + camArr[i];
                                        notAdd = false;
                                        break;
                                    }

                                }
                                
                                if (notAdd)
                                {
                                    toolStrip.Tag = camArr[i];
                                    toolStrip.Text = camArr[i];
                                }
                                toolStrip.Click += VideoURL_Click;
                                VideosToolStripMenuItem.DropDownItems.Add(toolStrip);
                            }
                        }
                        else
                        {
                            camArr.RemoveAt(i);
                            i--;
                            k--;
                        }
                    }
                    s.UrlVideoCamEnabled = string.Join("|", camArr);
                    enabledCams = camArr;

                }
                if (!string.IsNullOrEmpty(s.UrlVideoCam))
                {
                    List<string> camArr = s.UrlVideoCamNum.Split('|').ToList();
                    int k = camArr.Count();
                    for (int i = 0; i < k; i++)
                    {
                        Uri uriResult;
                        bool result = true;
                        if (!Uri.TryCreate(camArr[i], UriKind.Absolute, out uriResult) || null == uriResult)
                            result = false;
                        if (result)
                        {
                            if (!PingHost(new Uri(camArr[i]).Host))
                            {
                                camArr.RemoveAt(i);
                                i--;
                                k--;
                            }
                        }
                        else
                        {
                            camArr.RemoveAt(i);
                            i--;
                            k--;
                        }
                    }
                    s.UrlVideoCamNumEnabled = string.Join("|", camArr);
                    //enabledCams = camArr;
                }

                context.SaveChanges();
            }
            #endregion

            foreach (var item in videoSourceList)
            {
                item.Dispose();
            }
            videoSourceList.Clear();

            #region Создание видеопотоков
            for (int i = 0; i < enabledCams.Count; i++)
            {
                bool addSource = true;
                foreach (var item in videoSourceList)
                {
                    if (item.link == enabledCams[i])
                        addSource = false;
                }
                if (addSource)
                {
                    videoSourceList.Add(new VideoSource(enabledCams[i]));
                    Thread.Sleep(1100);
                }
            }
            #endregion

            #region Удаление неактивных видеопотоков
            for (int i = 0; i < videoSourceList.Count; i++)
            {
                bool remove = true;
                for (int j = 0; j < enabledCams.Count; j++)
                {
                    if (videoSourceList[i].link == enabledCams[j])
                    {
                        remove = false;
                    }
                }
                if (remove)
                {
                    videoSourceList.RemoveAt(i);
                    i = 0;
                }
            }
            #endregion

            #region Удаление некативных форм Видео
            for (int i = activeVideoForms.Count - 1; i >= 0; i--)
            {
                if (activeVideoForms[i].Text != "Видео")
                {
                    activeVideoForms.RemoveAt(i);
                }
            }
            #endregion

            #region Закрытие форм видео неактивных видеопотоков
            List<Video> deleteForm = new List<Video>();
            for (int i = 0; i < activeVideoForms.Count; i++)
            {
                bool delete = true;
                for (int j = 0; j < enabledCams.Count; j++)
                {
                    if (activeVideoForms[i].link == enabledCams[j])
                    {
                        delete = false;
                    }
                }
                if (delete)
                {
                    deleteForm.Add(activeVideoForms[i]);
                }
            }
            foreach (var item in deleteForm)
            {
                item.Close();
                activeVideoForms.Remove(item);
            }
            deleteForm.Clear();
            #endregion
        }

        private void VideoURL_Click(object sender, EventArgs e)
        {
            for (int i = activeVideoForms.Count - 1; i >= 0; i--)
            {
                if (activeVideoForms[i].Text != "Видео")
                {
                    activeVideoForms.RemoveAt(i);
                }
            }
            var video = activeVideoForms.Find(x => x.link == ((ToolStripItem)sender).Name);
            if (video == null)
            {
                try
                {
                    Video frmVideo = new Video(((ToolStripItem)sender).Name, ref videoSourceList, ((ToolStripItem)sender).Tag.ToString());
                    activeVideoForms.Add(frmVideo);
                    frmVideo.Show();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("=========================================\n" + ex.Message);
                }
            }
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                Setting frmSetting = new Setting();
                frmSetting.StartPosition = FormStartPosition.CenterParent;
                int maxWeight;



                if (frmSetting.ShowDialog() == DialogResult.OK)
                {
                    lblWeight.Text = 0.ToString(" 000000;-000000");
                    foreach (var scale in scalesList)
                    {
                        //string model = (from i in context.Scales
                        //                where i.name == scale.Name
                        //                select i.Model).FirstOrDefault();
                        if (scale.Model == "Rus")
                        {
                            (scale.Libra as MetraHelper).StopReading();
                            (scale.Libra as MetraHelper).RefreshSettingsPort();
                            

                        }
                        else
                        {
                            (scale.Libra as Libra).RefreshSettingsPort();
                        };
                        //switch (model)
                        //{
                        //    case "Rus":
                        //        (scale.Libra as MetraHelper).RefreshSettingsPort();
                        //        break;
                        //    default:
                        //        (scale.Libra as Libra).RefreshSettingsPort();
                        //        break;
                        //}
                        maxWeight = Int32.Parse((from i in context.Settings where i.id == 1 select i.MaxWeight).FirstOrDefault()) * 1000;
                        progressBar.Maximum = progressBar.Maximum < maxWeight ? maxWeight : progressBar.Maximum;
                        //progressBar.Maximum = Int32.Parse((from i in context.Settings where i.id == 1 select i.MaxWeight).FirstOrDefault()) * 1000;
                    }
                }
                this.Text = "ELibra - Перезагрузка камер";
                //if (frmSetting.ShowDialog() == DialogResult.OK)
                //{
                //    string model = (from i in context.Settings
                //                    where i.id == 1
                //                    select i.Model).FirstOrDefault();
                //    if (model == "IP65")
                //        (libra as Libra).RefreshSettingsPort();
                //    //else if (model == "Rus")
                //    //    (libra as MetraHelper).RefreshSettingsPort();
                //    progressBar.Maximum = Int32.Parse((from i in context.Settings where i.id == 1 select i.MaxWeight).FirstOrDefault()) * 1000;
                //}
                RestartLibra();
                CheckURLVideo();
                this.Text = "ELibra - электронные весы";
            }
        }

        private void CounterPartiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmCounterParties == null)
            {
                frmCounterParties = new CounterParties();
                frmCounterParties.MdiParent = this;
                frmCounterParties.Show();
            }
            else
            {
                frmCounterParties.Focus();
            }
        }

        private void CarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmCars == null)
            {
                frmCars = new Cars();
                frmCars.MdiParent = this;
                frmCars.Show();
            }
            else
            {
                frmCars.Focus();
            }
        }

        private void UsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmUsers == null)
            {
                frmUsers = new Users();
                frmUsers.MdiParent = this;
                frmUsers.Show();
            }
            else
            {
                frmUsers.Focus();
            }
        }

        private void CargosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmCargos == null)
            {
                frmCargos = new Cargos();
                frmCargos.MdiParent = this;
                frmCargos.Show();
            }
            else
            {
                frmCargos.Focus();
            }
        }

        private void StocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmStocks == null)
            {
                frmStocks = new Stocks();
                frmStocks.MdiParent = this;
                frmStocks.Show();
            }
            else
            {
                frmStocks.Focus();
            }
        }

        private void DriversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmDrivers == null)
            {
                frmDrivers = new Drivers();
                frmDrivers.MdiParent = this;
                frmDrivers.Show();
            }
            else
            {
                frmDrivers.Focus();
            }
        }

        private void CarriersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmCarriers == null)
            {
                frmCarriers = new Carriers();
                frmCarriers.MdiParent = this;
                frmCarriers.Show();
            }
            else
            {
                frmCarriers.Focus();
            }
        }

        private void ConsigneesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmConsignees == null)
            {
                frmConsignees = new Consignees();
                frmConsignees.MdiParent = this;
                frmConsignees.Show();
            }
            else
            {
                frmConsignees.Focus();
            }
        }

        private void ConsignorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmConsignors == null)
            {
                frmConsignors = new Consignors();
                frmConsignors.MdiParent = this;
                frmConsignors.Show();
            }
            else
            {
                frmConsignors.Focus();
            }
        }

        private void WeighingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            if (frmWeighing == null)
            {
                frmWeighing = new Weighing();
                frmWeighing.MdiParent = this;
                frmWeighing.Show();
            }
            else
            {
                frmWeighing.Focus();
            }

        }

        private void JournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmJournal == null)
            {
                frmJournal = new Journal();
                frmJournal.MdiParent = this;
                frmJournal.Show();
            }
            else
            {
                frmJournal.Focus();
            }
        }

        private void Upload1CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Upload1C frmUpload1C = new Upload1C();
            frmUpload1C.ShowDialog();
        }

        private void DayReportToolStrip_Click(object sender, EventArgs e)
        {
            Reports frmReports = new Reports(0);
            frmReports.ShowDialog();
        }

        private void MouthReportToolStrip_Click(object sender, EventArgs e)
        {
            Reports frmReports = new Reports(1);
            frmReports.ShowDialog();
        }

        private void DetalReportToolStrip_Click(object sender, EventArgs e)
        {
            Reports frmReports = new Reports(2);
            frmReports.ShowDialog();
        }

        private void SumReportToolStrip_Click(object sender, EventArgs e)
        {
            Reports frmReports = new Reports(3);
            frmReports.ShowDialog();
        }

        private void UploadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UploadHelper.Upload();
            }
            catch (WebException ex)
            {
                HttpWebResponse resp = (HttpWebResponse)ex.Response;
                if (resp.StatusCode == HttpStatusCode.BadGateway)
                    MessageBox.Show("Остутствует подключение к интернету");
            }
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            checkUpdates();
        }

        public void checkUpdates()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var settingData = (from i in context.Settings
                                   where i.id == 1
                                   select new { i.LoginDB, i.PasswordDB, i.HostDB }).FirstOrDefault();
                string update_host = settingData.HostDB.Split('|')[0];
                
                string host = update_host + "/api/download?fileName=";
                string config;
                try
                {
                    WebClient client = new WebClient();
                    var token = serverHelp.getToken(settingData.LoginDB, settingData.PasswordDB);
                    client.Headers.Add(HttpRequestHeader.Authorization, serverHelp.getToken(settingData.LoginDB, settingData.PasswordDB));
                    XmlDocument doc = new XmlDocument();
                    var f = client.DownloadString(host + "version.xml");
                    doc.LoadXml(client.DownloadString(host + "version.xml"));
                    Version remoteVersionElibra = new Version(doc.GetElementsByTagName("elibra")[0].InnerText);
                    Version remoteVersionUpdater = new Version(doc.GetElementsByTagName("updater")[0].InnerText);
                    if (new Version(Application.ProductVersion) < remoteVersionElibra)
                    {
                        config = client.DownloadString(host + "config.txt");
                        foreach (string item in config.Split(';'))
                        {
                            if (item == "ELibra.exe")
                            {
                                if (File.Exists("elibra.update"))
                                {
                                    File.Delete("elibra.update");
                                }
                                client.DownloadFile(new Uri(host + item), "elibra.update");
                                Completed();
                            }
                            else
                            {
                                client.DownloadFile(new Uri(host + item), item);
                                // client.DownloadFileAsync(new Uri(host + item), item);
                            }
                        }
                    }
                    client.Dispose();
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                };
            }

        }

        private void Updater()
        {
            try
            {
                if (File.Exists("elibra.update") && new Version(FileVersionInfo.GetVersionInfo("elibra.update").FileVersion) > new Version(Application.ProductVersion))
                {
                    Process.Start("updater.exe", "elibra.update " + Process.GetCurrentProcess().ProcessName + ".exe");
                    close = true;
                    Process.GetCurrentProcess().CloseMainWindow();
                }
            }
            catch (Exception)
            {
                if (File.Exists("elibra.update")) { File.Delete("elibra.update"); }
                if (File.Exists("updater.update")) { File.Delete("updater.update"); }
                checkUpdates();
            }
        }

        private void Completed()
        {

            DialogResult dResult = MessageBox.Show("Новое обновление готово для установки" + Environment.NewLine + "Хотите обновить программу сейчас?", "Обновление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dResult == DialogResult.Yes)
                Updater();
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var s = (from i in context.Settings
                         where i.id == 1
                         select new { i.Role, i.licenseNumber }).FirstOrDefault();
                if (s.Role == "1")
                {
                    //SettingToolStripMenu.Visible = true;
                    UsersToolStripMenuItem.Visible = true;
                    SettingToolStripMenu.DropDownItems[0].Visible = true;
                }
                if (s.Role == "2")
                {
                    //SettingToolStripMenu.Visible = false;
                    UsersToolStripMenuItem.Visible = false;
                    SettingToolStripMenu.DropDownItems[0].Visible = false;
                }
                if (s.Role == "3")
                {
                    UsersToolStripMenuItem.Visible = false;
                    SettingToolStripMenu.DropDownItems[0].Visible = false;
                }
                if (s.licenseNumber[1] == '0')
                {
                    cToolStripMenuItem.Visible = false;
                }
                if (s.licenseNumber[4] == '0')
                {
                    UploadToolStripMenuItem.Visible = false;
                }
            }

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close)
            {
                DialogResult dialogResult = MessageBox.Show("Вы хотите закрыть программу?", "Предупреждение", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    e.Cancel = true;
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                    notifyIcon = new NotifyIcon();
                    notifyIcon.Icon = Resources.Icon11;
                    notifyIcon.Text = "Весовая";
                    notifyIcon.Click += new System.EventHandler(this.NotifyIcon_Click);

                    if (!notifyIcon.Visible)
                    {
                        notifyIcon.Visible = true;
                    }
                    notifyIcon.BalloonTipText = "Программа работает в фоновом режиме";
                    notifyIcon.ShowBalloonTip(10000);
                }
                    
            }
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                //notifyIcon.Visible = false;

                if (notifyIcon != null)
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Icon = null;
                    notifyIcon.Dispose();
                    notifyIcon = null;
                }
                this.WindowState = FormWindowState.Normal;
                this.Hide();
                var s = (from i in context.Settings
                         where i.id == 1
                         select i).FirstOrDefault();
                Login frmLogin = new Login();
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    s.Role = frmLogin.roleId.ToString();
                    context.SaveChanges();
                }
                this.Show();
                ShowInTaskbar = true;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About frmAbout = new About();
            frmAbout.ShowDialog();
        }

        private void ManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manual frmManual = new Manual();
            frmManual.ShowDialog();
        }


        private void scaleButton_Click(object sender, EventArgs e, Scales currentSclale)
        {
            if (frmWeighing != null)
            {
                frmWeighing.Unsubscribe();
            }
            foreach (var scale in scalesList)
            {
                scale.ScalePanel.scales.scaleButton.BackgroundImage = Properties.Resources.red;
                scale.ScalePanel.scales.scaleButton.Tag = "red";
                if (scale.Name == currentSclale.name)
                {
                    scale.ScalePanel.scales.scaleButton.Enabled = false;
                    scale.ScalePanel.scales.scaleButton.BackgroundImage = Properties.Resources.green;
                    scale.ScalePanel.scales.scaleButton.Tag = "green";
                    scale.Active = true;
                    try
                    {
                        progressBar.Value = int.Parse(scale.ScalePanel.scales.scaleWeightLable.Text);
                    }
                    catch
                    {
                        progressBar.Value = 0;
                    }
                    lblWeight.Text = scale.ScalePanel.scales.scaleWeightLable.Text;
                }
                else
                {
                    scale.ScalePanel.scales.scaleButton.Enabled = true;
                    scale.Active = false;
                }

            }
            if (frmWeighing != null)
            {
                frmWeighing.ClearCamArray();
                frmWeighing.NewScaleSelected();
            }

        }

        public void RefreshJournal()
        {
            if (this.frmJournal != null)
            {
                this.frmJournal.RefreshTable();
            }
        }

        private void restartScalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Text = "ELibra - переподключение весов";
            RestartLibra();
            this.Text = "ELibra - электронные весы";
        }
    }
}
