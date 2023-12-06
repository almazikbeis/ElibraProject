using ELibra.Classes;
using ELibra.DBModel;
using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using static ELibra.Classes.WeightingBase;

namespace ELibra
{
    public partial class Weighing : Form
    {
        #region Структуры
        Settings setting;
        Scales scale;
        Weighings finishinwWeighing;
        #endregion

        #region Коллекции и списки
        List<string> dtctCams = new List<string>();
        List<string> newCameras = new List<string>();
        private List<DetectorLicensePlate> arrDtct = new List<DetectorLicensePlate>();
        private string[] headersText = { "Номер", "Марка", "Вес пустого автомобиля, кг", "Контрагент" };
        #endregion

        #region Данные для взвешиваний
        private double weightFullCar  = 0;
        private double weightEmptyCar = 0;
        private double weight         = 0;
        public double minWeight;
        DateTime fullWeightingFinishDate;
        //DateTime UnfinishedWeighingDate;

        #endregion

        #region Флаги для завершения и распознования
        private bool saveCar            = true;
        private bool newWeight          = true;
        private bool trailer            = false;
        private bool finish             = false;
        private bool isSave             = false;
        bool scaleSave                  = false;
        private bool isDetect           = false;
        private bool weightChanged      = false;
        private bool EmptyWeightCreated = false;
        #endregion

        /// <summary>
        /// Форма взвешивания
        /// </summary>
        public Weighing()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Форма взвешивания при завершении созданной записи
        /// </summary>
        /// <param name="weighing"></param>
        public Weighing(Weighings weighing)
        {
            #region Получение Незавершенного взвешивания
            InitializeComponent();
            finish = true; 
            this.finishinwWeighing = weighing;
            #endregion
        }

        /// <summary>
        /// Загрузка формы взвешивания
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void Weighing_Load(object sender, EventArgs e)
        {
            using(DataBaseContext context = new DataBaseContext())
            {
                this.WindowState = FormWindowState.Maximized;

                dataGridView.AutoGenerateColumns = true;
                setting = (from i in context.Settings where i.id == 1 select i).FirstOrDefault();

                NewScaleSelected();

                //weight = ((this.MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as WeightingBase).Weight; 
                weight = (this.MdiParent as Main).weight;

                #region Получение данных о машинах
                RefreshTable();
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                    dataGridView.Columns[i].HeaderText = headersText[i];
                #endregion

                tPost.Text = setting.fioOperator;

                cmbGoods.Items.AddRange((from i in context.Goods orderby i.name select i.name).ToArray());
                var lastWeight = (from i in context.Weighings
                                    orderby i.id descending
                                    select new
                                    {
                                        i.material,
                                        i.operationType
                                    }).FirstOrDefault() ?? new {material ="", operationType =""};
                if (cmbGoods.Items.Count > 0)
                {
                    cmbGoods.SelectedIndex = 0;
                    if (lastWeight.material != "")
                        cmbGoods.Text = lastWeight.material;
                }             

                #region Заполнение данных фильтрации в таблице машин
                toolStripCmbClient.Items.AddRange((from i in context.Clients orderby i.name select i.name).ToArray());
                toolStripCmbMark.Items.AddRange((from i in context.CarMarks select i.name).ToArray());
                var driver = from i in context.Drivers
                             select new
                             {
                                 i.surname,
                                 i.name,
                                 i.patronimyc
                             };
                foreach (var d in driver)
                    toolStripCmbDriver.Items.Add((d.surname + " " + d.name + " " + d.patronimyc).Trim(' '));
                #endregion

                if (finish)
                {
                    FinishWeighting(finishinwWeighing);
                    if ((from user in context.Settings select user.Role).FirstOrDefault() == "3")
                    {
                        tPost.Text = (from i in context.Users where i.id == finishinwWeighing.userId select i.FIO).FirstOrDefault();
                        tWeightFullCar.ReadOnly = false;
                        tWeightEmptyCar.ReadOnly = false;
                    }
                }

                #region Включение и заполнение полей
                if (setting.Consignee == "true")
                {
                    lblConsignee.Visible = true; cmbConsignee.Visible = true;
                    cmbConsignee.Items.AddRange((from i in context.Consignee orderby i.name select i.name).ToArray());
                    if (finish)
                        cmbConsignee.SelectedItem = finishinwWeighing.consignee;
                }
                if (setting.Snipper == "true")
                {
                    lblConsignor.Visible = true; cmbConsignor.Visible = true;
                    cmbConsignor.Items.AddRange((from i in context.Consignor orderby i.name select i.name).ToArray());
                    if (finish)
                        cmbConsignor.SelectedItem = finishinwWeighing.consignor;
                }
                
                if (setting.TypeTransaction == "true")
                {
                    lblType.Visible = true; rbSales.Visible = true; rbSales.Checked = true; rbSupply.Visible = true;
                    if (lastWeight.operationType == "Сбыт")
                        rbSales.Checked = true;
                    else
                        rbSupply.Checked = true;
                    if (finish)
                    {
                        if (finishinwWeighing.operationType == "Сбыт")
                            rbSales.Checked = true;
                        else
                            rbSupply.Checked = true;
                    }
                }
                if (setting.Carrier == "true")
                {
                    lblCarrier.Visible = true; cmbCarrier.Visible = true;
                    cmbCarrier.Items.AddRange((from i in context.Carriers orderby i.name select i.name).ToArray());
                    if (finish)
                        cmbCarrier.SelectedItem = (from i in context.Carriers where i.id == finishinwWeighing.carrierid select i.name).FirstOrDefault();
                }
                if (setting.Stock == "true")
                {
                    lblStock.Visible = true; cmbStock.Visible = true;
                    cmbStock.Items.AddRange((from i in context.Storages orderby i.name select i.name).ToArray());
                    if (finish)
                        cmbStock.SelectedItem = (from i in context.Storages where i.id == finishinwWeighing.storageId select i.name).FirstOrDefault();
                }


                FillingXMLFields();
                #endregion

                CmbGoods_SelectionChangeCommitted(new object(), new EventArgs());
                
                
            }
            Libra_WeightChanged(weight, scale.name);
        }

        /// <summary>
        /// Завершение уже созданного взвешивания
        /// </summary>
        /// <param name="finishinwWeighing"></param>
        private void FinishWeighting(Weighings finishinwWeighing)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                #region Включение нужных полей для завершения взвешивания
                if (string.IsNullOrEmpty(finishinwWeighing.consignee))
                    setting.Consignee = "true";
                if (string.IsNullOrEmpty(finishinwWeighing.consignor))
                    setting.Snipper = "true";
                if (string.IsNullOrEmpty(finishinwWeighing.point1))
                    setting.Point1 = "true";
                if (string.IsNullOrEmpty(finishinwWeighing.point2))
                    setting.Point2 = "true";
                if (string.IsNullOrEmpty(finishinwWeighing.operationType))
                    setting.TypeTransaction = "true";
                if (finishinwWeighing.carrierid.HasValue)
                    setting.Carrier = "true";
                if (finishinwWeighing.storageId.HasValue)
                    setting.Stock = "true";
                if (string.IsNullOrEmpty(finishinwWeighing.orderNumber))
                    setting.OrderNumber = "true";
                if (string.IsNullOrEmpty(finishinwWeighing.orderPositionNumber))
                    setting.PositionStockNumber = "true";
                context.SaveChanges();
                #endregion

                #region Заполнение данных требуемых для работы с незавершенным взвешиванием 
                var carTable = from i in context.Cars where i.number == finishinwWeighing.carNumber select i;
                if (!carTable.Any())
                    ToolDontSaveCar_Click(new object(), new EventArgs());
                tWeightEmptyCar.Text = finishinwWeighing.carWeight.ToString();
                tWeightFullCar.Text = finishinwWeighing.brutto.ToString();
                weightEmptyCar = finishinwWeighing.carWeight ?? 0;
                weightFullCar = finishinwWeighing.brutto ?? 0;
                tWeightDoc.Text = finishinwWeighing.docWeight.ToString();
                dateTime.Value = finishinwWeighing.dateWeight;
                fullWeightingFinishDate = finishinwWeighing.dateWeight;
                cmbGoods.SelectedItem = finishinwWeighing.material;
                tAdjustment.Text = finishinwWeighing.adjustment.ToString();
                dataGridView.Enabled = false;
                dataGridView.ForeColor = Color.LightGray;
                #endregion

                #region Заполнение данных фильтрации в таблице машин при условии завершения взвешивания
                toolStripTxtNumber.Text = finishinwWeighing.carNumber;
                toolStripTxtNumber.Enabled = false;
                toolStripCmbMark.SelectedItem = finishinwWeighing.carMark;
                toolStripCmbMark.Enabled = false;
                toolStripCmbClient.SelectedItem = (from i in context.Clients where i.id == finishinwWeighing.clientId select i.name).FirstOrDefault();
                toolStripCmbClient.Enabled = false;
                tDesc.Text = finishinwWeighing.description;
                if (toolStripCmbDriver.Items.Count > 0)
                {
                    toolStripCmbDriver.SelectedItem = finishinwWeighing.driver.Trim(' ');
                }
                #endregion

                btnReset.Enabled = false;
            }
        }

        /// <summary>
        /// Подключение к активным весам из MdiParrent
        /// </summary>
        public void NewScaleSelected()
        {         
            using (DataBaseContext context = new DataBaseContext())
            {

                var activeScales = (MdiParent as Main).scalesList.Find(x => x.Active == true);
                scale = (from i in context.Scales
                         where i.name == activeScales.Name
                         select i).FirstOrDefault();

                minWeight = Double.Parse(scale.MinWeightVideo);

                #region Новое обновление списка камер
                var scalesCameras = scale.LinkedCameras.Split('|');
                var activeCameras = (from i in context.Settings
                                     select i.UrlVideoCamEnabled).FirstOrDefault();

                
                IEnumerable<string> activeScaleCameras = scalesCameras.Intersect(activeCameras.Split('|'));
                newCameras.Clear();
                var activeScaleCamerasList = activeScaleCameras.ToList();
                if (activeScaleCamerasList.Count > 0)
                {
                    if (activeScaleCamerasList[0] != "")
                    {
                        newCameras = activeScaleCamerasList;
                    }
                }
                

                var detectionCameras = setting.UrlVideoCamNumEnabled.Split('|');

                IEnumerable<string> inBoth = scalesCameras.Intersect(detectionCameras);

                toolStripTxtNumber.Items.Clear();
                dtctCams.Clear();
                foreach (var item in inBoth)
                {
                    if (item != "")
                    {
                        dtctCams.Add(item);
                        toolStripTxtNumber.Items.Add("");
                    }
                }
                #endregion

                #region Старое обновление списка камер
                //var oldSettingsCameras = setting.UrlVideoCamEnabled.Split('|');
                //List<string> oldCameras = new List<string>();
                //for (int i = 0; i < oldSettingsCameras.Length; i++)
                //    for (int j = 0; j < scalesCameras.Length; j++)
                //        if (oldSettingsCameras[i] == scalesCameras[j])
                //            oldCameras.Add(oldSettingsCameras[i]);
                //oldCameras.Distinct().ToList().Sort();

                //CheckURLVideo();

                //newCameras.Clear();
                //var newSettingsCameras = (from i in context.Settings where i.id == 1 select i.UrlVideoCamEnabled).FirstOrDefault().Split('|');
                //for (int i = 0; i < newSettingsCameras.Length; i++)
                //    for (int j = 0; j < scalesCameras.Length; j++)
                //        if (newSettingsCameras[i] == scalesCameras[j]
                //            && newSettingsCameras[i] != "")
                //            newCameras.Add(newSettingsCameras[i]);

                //newCameras.Distinct().ToList().Sort();
                //try
                //{
                //    if (string.Join("|", oldCameras) == string.Join("|", newCameras))
                //    {
                //        switch (scale.Model)
                //        {
                //            case "IP65":
                //                //((this.MdiParent as Main).libra as Libra).RefreshSettingsPort();
                //                ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as Libra).RefreshSettingsPort();
                //                break;
                //            case "Rus":
                //                ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as MetraHelper).RefreshSettingsPort();
                //                break;
                //        }
                //    }

                //}
                //catch(Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                //string[] enableCams = setting.UrlVideoCamNumEnabled.Split('|');
                //string[] scaleCams = scale.LinkedCameras.Split('|');
                //dtctCams.Clear();
                //for (int i = 0; i < enableCams.Length; i++)
                //{
                //    for (int j = 0; j < scaleCams.Length; j++)
                //    {
                //        if (enableCams[i] != "" && scaleCams[j] != "" && enableCams[i] == scaleCams[j])
                //        {
                //            dtctCams.Add(enableCams[i]);
                //        }
                //    }
                //}
                //toolStripTxtNumber.Items.Clear();
                //if (dtctCams != null)
                //{
                //    foreach (var cam in dtctCams)
                //    {
                //        if (cam != "")
                //        {
                //            toolStripTxtNumber.Items.Add("");
                //        }
                //    }
                //}
                //if (scale.Model == "Rus")
                //{
                //    ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as MetraHelper).WeightChanged += Libra_WeightChanged;
                //}
                //else if (scale.Model == "IP65")
                //    ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as Libra).WeightChanged += Libra_WeightChanged;

                //if (setting.Model == "IP65")
                //    ((this.MdiParent as Main).libra as Libra).WeightChanged += Libra_WeightChanged;
                ////else if (setting.Model == "Rus")
                ////    ((this.MdiParent as Main).libra as MetraHelper).WeightChanged += Libra_WeightChanged;
                #endregion

                Libra_WeightChanged((this.MdiParent as Main).weight, scale.name);
            }
        }

        /// <summary>
        /// Отписка от события изменения веса 
        /// </summary>
        public void Unsubscribe()
        {
            if (scale.Model == "Rus")
            {
                ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as MetraHelper).WeightChanged -= Libra_WeightChanged;
            }
            if (scale.Model == "IP65" || scale.Model == "CAS")
                ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as Libra).WeightChanged -= Libra_WeightChanged;
        }


        /// <summary>
        /// Заполнение полей из XML документа
        /// </summary>
        private void FillingXMLFields()
        {
            if (setting.Point1 == "true")
            {
                lblPoint1.Visible = true; cmbPoint1.Visible = true;
                if (File.Exists("data.xml"))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("data.xml");
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                        if (node.Name == "point1")
                            cmbPoint1.Items.Add(node.Attributes[0].Value);
                }
                if (finish)
                    cmbPoint1.SelectedItem = finishinwWeighing.point1;
            }
            if (setting.Point2 == "true")
            {
                lblPoint2.Visible = true; cmbPoint2.Visible = true;
                if (File.Exists("data.xml"))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("data.xml");
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                        if (node.Name == "point2")
                            cmbPoint2.Items.Add(node.Attributes[0].Value);
                }
                if (finish)
                    cmbPoint2.SelectedItem = finishinwWeighing.point2;
            }
            if (setting.OrderNumber == "true")
            {
                lblNumOrder.Visible = true; cmbNumOrder.Visible = true;
                if (File.Exists("data.xml"))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("data.xml");
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                        if (node.Name == "numOrder")
                            cmbNumOrder.Items.Add(node.Attributes[0].Value);
                }
                if (finish)
                    cmbNumOrder.SelectedItem = finishinwWeighing.orderNumber;
            }
            if (setting.PositionStockNumber == "true")
            {
                lblPosOrder.Visible = true; cmbPosOrder.Visible = true;
                if (File.Exists("data.xml"))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("data.xml");
                    XmlElement xRoot = xDoc.DocumentElement;
                    foreach (XmlNode node in xRoot)
                        if (node.Name == "posOrder")
                            cmbPosOrder.Items.Add(node.Attributes[0].Value);
                }
                if (finish)
                    cmbPosOrder.SelectedItem = finishinwWeighing.orderPositionNumber;
            }
        }

        /// <summary>
        /// Отчищение массива камер и отписка от заполнения поля гос.номеров
        /// </summary>
        public void ClearCamArray()
        {
            #region Очищение массива камер
            if (arrDtct != null)
            {
                for (int i = 0; i < arrDtct.Count; i++)
                {
                    arrDtct[i].LicensePlateChanged -= Weighing_LicensePlateChanged;
                    arrDtct[i].Dispose();
                    arrDtct[i] = null;
                }
            }
            isDetect = false;
            arrDtct.Clear();
            #endregion
        }

        /// <summary>
        /// Изменение распознанного номера
        /// </summary>
        /// <param name="plate"></param>
        /// <param name="num"></param>
        private void Weighing_LicensePlateChanged(string plate, int num)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new DetectorLicensePlate.LicensePlateChangeHandler(Weighing_LicensePlateChanged), new object[] { plate, num });
                    return;
                }
                else
                {
                    toolStripTxtNumber.Items[num] = plate.Trim();
                    toolStripTxtNumber.Text = plate.Trim();
                    AutoNumSearch();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Weighing " + ex.Message + "\n" + toolStripTxtNumber.Items.Count);
            }
            
        }

        /// <summary>
        /// Подстановка найденой записи
        /// </summary>
        private void AutoNumSearch()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string searchString = Regex.Match(toolStripTxtNumber.Text, "[0-9]{3}").Value;
                var data = from i in context.Cars
                           join mU in context.CarMarks on i.markId equals mU.id into mj
                           from m in mj.DefaultIfEmpty()
                           join cU in context.Clients on i.clientId equals cU.id into cj
                           from c in cj.DefaultIfEmpty()
                           where i.number.Contains(searchString)
                           select new
                           {
                               i.number,
                               mark = m.name,
                               i.weight,
                               c.name
                           };
                bindingSource.DataSource = data.ToList();
            }
        }

        /// <summary>
        /// Событие изменения значений весов
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="scaleName"></param>
        private void Libra_WeightChanged(double weight, string scaleName)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new WeightChangeHandler(Libra_WeightChanged), new object[] { weight , scaleName });
                return;
            }
            else
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    this.weight = weight;
                    if (weight < minWeight)
                    {
                        //setting.isSave = "false";
                        if (isDetect)
                        {
                            ClearCamArray();
                        }
                        weightChanged = false;
                    }
                    else 
                    {
                        EmptyWeightCreated = true;
                        #region Заполнение массива камерами для распознования номеров
                        if (!finish && newWeight && !isDetect && !weightChanged && setting.licenseNumber[3] == '1')
                        {
                            weightChanged = true;
                            int count = 0;
                            foreach (var cam in dtctCams)
                            {
                                arrDtct.Add(new DetectorLicensePlate(cam, count, ref (this.MdiParent as Main).videoSourceList));
                                Console.WriteLine("Weighing cirrent thread " + Thread.CurrentThread.GetHashCode());
                                arrDtct[arrDtct.Count - 1].LicensePlateChanged += Weighing_LicensePlateChanged;

                                isDetect = true;
                                try
                                {
                                    if (File.Exists("recognaize\\" + count))
                                    {
                                        File.Delete("recognaize\\" + count);
                                        count++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Cannot delete file. Exception " + ex.Message);
                                }
                                count++;
                            }
                           
                        }
                        #endregion
                    }
                    context.SaveChanges();
                }
            } 
        }

        /// <summary>
        /// Сохранение данных в XML документ
        /// </summary>
        /// <returns></returns>
        private bool saveToXML()
        {
            try
            {
                #region Создание XML документа
                XmlDocument xDoc = new XmlDocument();
                XmlElement xRoot;
                if (File.Exists("data.xml"))
                {
                    xDoc.Load("data.xml");
                    xRoot = xDoc.DocumentElement;
                }
                else
                {
                    xRoot = xDoc.CreateElement("data");
                }
                #endregion

                #region Заполнение и сохранение XML документа
                if (setting.Point1 == "true")
                {
                    if (xRoot.SelectSingleNode("//point1[@text='" + cmbPoint1.Text + "']") == null)
                    {
                        XmlElement point1Elem = xDoc.CreateElement("point1");

                        XmlAttribute point1Attr = xDoc.CreateAttribute("text");
                        XmlText point1Text = xDoc.CreateTextNode(cmbPoint1.Text);
                        point1Attr.AppendChild(point1Text);
                        point1Elem.Attributes.Append(point1Attr);

                        XmlAttribute DateAttr = xDoc.CreateAttribute("date");
                        XmlText DateText = xDoc.CreateTextNode(DateTime.Now.Date.ToString());
                        DateAttr.AppendChild(DateText);
                        point1Elem.Attributes.Append(DateAttr);

                        xRoot.AppendChild(point1Elem);
                    }
                    else
                    {
                        var point1Elem = xRoot.SelectSingleNode("//point1[@text='" + cmbPoint1.Text + "']");
                        point1Elem.Attributes["date"].Value = DateTime.Now.Date.ToString();
                    }
                }
                if (setting.Point2 == "true")
                {
                    if (xRoot.SelectSingleNode("//point2[@text='" + cmbPoint2.Text + "']") == null)
                    {
                        XmlElement point2Elem = xDoc.CreateElement("point2");
                        XmlAttribute point2Attr = xDoc.CreateAttribute("text");
                        XmlText point2Text = xDoc.CreateTextNode(cmbPoint2.Text);
                        point2Attr.AppendChild(point2Text);
                        point2Elem.Attributes.Append(point2Attr);

                        XmlAttribute DateAttr = xDoc.CreateAttribute("date");
                        XmlText DateText = xDoc.CreateTextNode(DateTime.Now.Date.ToString());
                        DateAttr.AppendChild(DateText);
                        point2Elem.Attributes.Append(DateAttr);

                        xRoot.AppendChild(point2Elem);
                    }
                    else
                    {
                        var point2Elem = xRoot.SelectSingleNode("//point2[@text='" + cmbPoint2.Text + "']");
                        point2Elem.Attributes["date"].Value = DateTime.Now.Date.ToString();
                    }
                }
                if (setting.OrderNumber == "true")
                {
                    if (xRoot.SelectSingleNode("//numOrder[@text='" + cmbNumOrder.Text + "']") == null)
                    {
                        XmlElement numOrderElem = xDoc.CreateElement("numOrder");
                        XmlAttribute numOrderAttr = xDoc.CreateAttribute("text");
                        XmlText numOrderText = xDoc.CreateTextNode(cmbNumOrder.Text);
                        numOrderAttr.AppendChild(numOrderText);
                        numOrderElem.Attributes.Append(numOrderAttr);

                        XmlAttribute DateAttr = xDoc.CreateAttribute("date");
                        XmlText DateText = xDoc.CreateTextNode(DateTime.Now.Date.ToString());
                        DateAttr.AppendChild(DateText);
                        numOrderElem.Attributes.Append(DateAttr);

                        xRoot.AppendChild(numOrderElem);
                    }
                    else
                    {
                        var numOrderElem = xRoot.SelectSingleNode("//numOrder[@text='" + cmbNumOrder.Text + "']");
                        numOrderElem.Attributes["date"].Value = DateTime.Now.Date.ToString();
                    }
                }
                if (setting.PositionStockNumber == "true")
                {
                    if (xRoot.SelectSingleNode("//posOrder[@text='" + cmbPosOrder.Text + "']") == null)
                    {
                        XmlElement posOrderElem = xDoc.CreateElement("posOrder");
                        XmlAttribute posOrderAttr = xDoc.CreateAttribute("text");
                        XmlText posOrderText = xDoc.CreateTextNode(cmbPosOrder.Text);
                        posOrderAttr.AppendChild(posOrderText);
                        posOrderElem.Attributes.Append(posOrderAttr);

                        XmlAttribute DateAttr = xDoc.CreateAttribute("date");
                        XmlText DateText = xDoc.CreateTextNode(DateTime.Now.Date.ToString());
                        DateAttr.AppendChild(DateText);
                        posOrderElem.Attributes.Append(DateAttr);

                        xRoot.AppendChild(posOrderElem);
                    }
                    else
                    {
                        var posOrderElem = xRoot.SelectSingleNode("//posOrder[@text='" + cmbPosOrder.Text + "']");
                        posOrderElem.Attributes["date"].Value = DateTime.Now.Date.ToString();
                    }
                }
                xDoc.AppendChild(xRoot);
                xDoc.Save("data.xml");
                #endregion
            }
            catch
            {
                MessageBox.Show("Не удалось обновить XML документ");
            }


            return true;
        }

        /// <summary>
        /// Заполнения поля веса автомобиля без груза
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void BtnGetEmptyWeight_Click(object sender, EventArgs e)
        {
            //weight = ((this.MdiParent as Main).libra as WeightingBase).Weight;
            weight = ((this.MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as WeightingBase).Weight;
            string scaleSave = IsSave();

            if (!(scaleSave == "true") || trailer)
            {
                if (toolStripTxtNumber.Text != "")
                {
                    if (weight > 0)
                    {
                        if (weightFullCar > weight || weightFullCar == 0)
                        {
                            weightEmptyCar = weight;
                            tWeightEmptyCar.Text = weightEmptyCar.ToString();
                            if (tWeightFullCar.Text != "0" && tWeightEmptyCar.Text != "0" && tWeightFullCar.Text != "" && tWeightEmptyCar.Text != "")
                                tNetto.Text = (Double.Parse(tWeightFullCar.Text) - Double.Parse(tWeightEmptyCar.Text)).ToString();
                            CmbGoods_SelectionChangeCommitted(sender, e);
                            TWeightDoc_TextChanged(new object(), new EventArgs());
                        }
                        else
                        {
                            MessageBox.Show("Брутто не может быть меньше тары");
                        }
                    }
                    else if (weight == 0)
                    {
                        MessageBox.Show("Вес не может быть равен нулю");
                    }
                    else
                    {
                        MessageBox.Show("Вес не может быть меньше нуля");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите автомобиль");
                }
            }
            else
            {
                MessageBox.Show("Для повторного взвешивания необходимо освободить весы");
            }
        }

        /// <summary>
        /// Заполнения поля веса автомобиля с грузом
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void BtnGetFullWeight_Click(object sender, EventArgs e)
        {
            //weight = ((this.MdiParent as Main).libra as WeightingBase).Weight;
            weight = ((this.MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as WeightingBase).Weight;
            string scaleSave = IsSave();
            //weight = (this.MdiParent as Main).weight;
            if (!(scaleSave == "true") || trailer)
            {
                if (toolStripTxtNumber.Text != "")
                {
                    if (weight > 0)
                    {
                        if (weight > weightEmptyCar)
                        {
                            weightFullCar = weight;
                            tWeightFullCar.Text = weightFullCar.ToString();
                            if (tWeightFullCar.Text != "0" && tWeightEmptyCar.Text != "0")
                                tNetto.Text = (Double.Parse(tWeightFullCar.Text) - Double.Parse(tWeightEmptyCar.Text)).ToString();
                            CmbGoods_SelectionChangeCommitted(sender, e);
                            TWeightDoc_TextChanged(new object(), new EventArgs());
                        }
                        else
                        {
                            MessageBox.Show("Брутто не может быть меньше тары");
                        }
                    }                    
                    else if (weight == 0)
                    {
                        MessageBox.Show("Вес не может быть равен нулю");
                    }
                    else
                    {
                        MessageBox.Show("Вес не может быть меньше нуля");
                    }
                }
                else
                {
                    MessageBox.Show("Сначало выберите машину");
                }
            }
            else
            {
                MessageBox.Show("Для повторного взвешивания необходимо освободить весы" );
            }
        }

        /// <summary>
        /// Сброс полей весов автомобиля до 0
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void BtnResetWeight_Click(object sender, EventArgs e)
        {
            tWeightEmptyCar.Text = "0";
            weightEmptyCar = 0;
        }
        
        private void ToolStripTChanged(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(toolStripTxtNumber.Text))
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    var data = from i in context.Cars
                               join mU in context.CarMarks on i.markId equals mU.id into mj
                               from m in mj.DefaultIfEmpty()
                               join cU in context.Clients on i.clientId equals cU.id into cj
                               from c in cj.DefaultIfEmpty()
                               where i.number.Contains(toolStripTxtNumber.Text)
                               select new
                               {
                                   i.number,
                                   mark = m.name,
                                   i.weight,
                                   c.name
                               };
                    bindingSource.DataSource = data.ToList();
                    var car = from i in context.Cars
                              where i.number == toolStripTxtNumber.Text
                              select i;
                    if (car.Any())
                        toolStripCmbMark.Enabled = false;
                    else
                        toolStripCmbMark.Enabled = true;
                }
            }
            else
            {
                RefreshTable();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearCamArray();
            using (DataBaseContext context = new DataBaseContext())
            {
                #region Очистка полей
                toolStripTxtNumber.Items.Clear();
                tWeightAdjustment.Clear();
                tWeightEmptyCar.Clear();
                tWeightFullCar.Clear();
                tAdjustment.Clear();
                tWeightDoc.Clear();
                tOverload.Clear();
                tNetto.Clear();
                tPrice.Clear();
                tDesc.Clear();
                tSum.Clear();
                toolStripCmbClient.Text = "";
                toolStripCmbDriver.Text = "";
                toolStripCmbMark.Text = "";
                tWeightEmptyCar.Text = "0";
                tWeightFullCar.Text = "0";
                tAdjustment.Text = "";
                tWeightDoc.Text = "";
                tSum.Text = "";
                dataGridView.Enabled = true;
                btnSave.Enabled = true;
                panel1.Enabled = true;
                panel2.Enabled = true;
                dateTime.Value = DateTime.Now;
                weightEmptyCar = 0;
                weightFullCar = 0;
                try
                {
                    tPrice.Text = (from i in context.Goods where i.name == cmbGoods.SelectedItem.ToString() select i.price).FirstOrDefault().ToString();
                }
                catch
                {
                    tPrice.Text = "0";
                }
                toolStripTxtNumber.Text = "";

                tWeightFullCar.ReadOnly = true;
                tWeightEmptyCar.ReadOnly = true;

                dataGridView.ForeColor = Color.Black;
                toolStripTxtNumber.Enabled = true;
                toolStripCmbClient.Enabled = true;
                toolStripCmbDriver.Enabled = true;
                toolStripCmbMark.Enabled = true;
                dataGridView.Enabled = true;
                btnContinue.Visible = false;
                EmptyWeightCreated = false;
                weightChanged = false;
                isDetect = false;
                newWeight = true;
                finish = false;
                cmbPoint1.Text = "";
                cmbPoint2.Text = "";
                toolStripCmbDriver.Items.Clear();
                toolStripCmbClient.Items.Clear();
                toolStripCmbMark.Items.Clear();
                cmbNumOrder.Items.Clear();
                cmbPosOrder.Items.Clear();
                cmbPoint1.Items.Clear();
                cmbPoint2.Items.Clear();
                FillingXMLFields();

                #region Добавление полей

                if (dtctCams != null)
                {
                    foreach (var cam in dtctCams)
                    {
                        toolStripTxtNumber.Items.Add("");
                    }
                }
                var driver = from i in context.Drivers
                             select new
                             {
                                 i.surname,
                                 i.name,
                                 i.patronimyc
                             };
                foreach (var d in driver)
                    toolStripCmbDriver.Items.Add((d.surname + " " + d.name + " " + d.patronimyc).Trim(' '));
                toolStripCmbClient.Items.AddRange((from i in context.Clients orderby i.name select i.name).ToArray());
                toolStripCmbMark.Items.AddRange((from i in context.CarMarks select i.name).ToArray());
                #endregion

                RefreshTable();
                //if ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra.Weight > double.Parse(scale.MinWeightVideo));
                //{
                //    Libra_WeightChanged((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra.Weight, scale.name);
                //}
                NewScaleSelected();
                #endregion
            }
        }

        /// <summary>
        /// Заполнение поля веса по документам
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void TWeightDoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double k = tWeightDoc.Text != "" ? (int.Parse(tWeightFullCar.Text == "" ? "0" : tWeightFullCar.Text) - int.Parse(tWeightEmptyCar.Text == "" ? "0" : tWeightEmptyCar.Text)) - Double.Parse((tWeightDoc.Text), CultureInfo.InvariantCulture) : 0;
                if (k == 0)
                    tOverload.Text = "";
                else if (k > 0)
                    tOverload.Text = "+" + k;
                else if (k < 0)
                    tOverload.Text = k.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Входные данные имеют неверный формат", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);               
            }            
            
        }

        /// <summary>
        /// Заполнение поля стоимости груза
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void CmbGoods_SelectionChangeCommitted(object sender, EventArgs e)
        {
            double price;
            if (cmbGoods.SelectedItem != null)
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    price = (from i in context.Goods where i.name == cmbGoods.SelectedItem.ToString() select i.price).FirstOrDefault() ?? 0;
                    tPrice.Text = price.ToString();
                    tSum.Text = ((weightFullCar - weightEmptyCar) / 1000 * price).ToString();
                }
            }
        }

        /// <summary>
        /// Заполнения поля корректировки
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void TAdjustment_TextChanged(object sender, EventArgs e)
        {
            if (!tWeightEmptyCar.Text.Equals("0") && !tWeightFullCar.Text.Equals("0"))
            {
                try
                {
                    tNetto.Text = (int.Parse(tWeightFullCar.Text == "" ? "0" : tWeightFullCar.Text) - int.Parse(tWeightEmptyCar.Text == "" ? "0" : tWeightEmptyCar.Text)).ToString();
                    if (tAdjustment.Text == ".")
                    {
                        tAdjustment.Text = "";
                    }
                    if (tAdjustment.Text != "-")
                    {
                        if (tAdjustment.Text.Replace("-", "") != "")
                        {
                            string adj = tAdjustment.Text.Replace('.', ',');
                            adj = (adj + (adj.Last().Equals(',') ? "0" : ""));
                            double onlyWeight = (int.Parse(tWeightFullCar.Text == "" ? "0" : tWeightFullCar.Text) - int.Parse(tWeightEmptyCar.Text == "" ? "0" : tWeightEmptyCar.Text));  /*  weightFullCar - weightEmptyCar;*/
                            tWeightAdjustment.Text = (onlyWeight + ((onlyWeight * Double.Parse(adj)) / 100)).ToString();
                        }
                        else
                            tWeightAdjustment.Text = (int.Parse(tWeightFullCar.Text == "" ? "0" : tWeightFullCar.Text) - int.Parse(tWeightEmptyCar.Text == "" ? "0" : tWeightEmptyCar.Text)).ToString();
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show($"Введены некорректные данные");
                    tWeightAdjustment.Text = "";
                }
            }
            
        }

        /// <summary>
        /// Проверка на запись
        /// </summary>
        /// <returns>Возможность сохранить взвешивание</returns>
        private string IsSave()
        {
            string scaleSave = "false";
            switch (scale.Model)
            {
                case "IP65":
                    scaleSave = ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as Libra).IsSave();
                    break;
                case "Rus":
                    scaleSave = ((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as MetraHelper).IsSave();
                    break;
            }
            return scaleSave;
        }

        /// <summary>
        /// Сохранение взвешивания
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            ClearCamArray();
            using (DataBaseContext context = new DataBaseContext())
            {
                bool b = finish ? (tWeightEmptyCar.Text != "0" && tWeightFullCar.Text != "0") : (tWeightEmptyCar.Text != "0" || tWeightFullCar.Text != "0");
                var good = from i in context.Goods where i.name == cmbGoods.Text select i;
                bool redactor = finish ? (from i in context.Settings select i.Role).FirstOrDefault() == "3" : false;
                string fioOperator = (from i in context.Settings select i.fioOperator).FirstOrDefault();

                if (good.Any() && toolStripTxtNumber.Text != "" && toolStripCmbClient.Text != "" && toolStripCmbMark.Text != "" && (b || redactor))
                {
                    string carMark = toolStripCmbMark.Text;
                    string client = toolStripCmbClient.Text;
                    string driver = Regex.Replace(toolStripCmbDriver.Text, " +", " ");
                    string carNumber = toolStripTxtNumber.Text.Replace(" ", "");
                    string surname = driver.Split().Count() > 0 ? driver.Split()[0].Replace(" ", "") : "";
                    string name = driver.Split().Count() > 1 ? driver.Split()[1].Replace(" ", "") : "";
                    string patronimyc = driver.Split().Count() > 2 ? driver.Split()[2].Replace(" ", "") : "";

                    int userId = string.IsNullOrEmpty(tPost.Text) ? (from i in context.Users where i.username == "admin" select i.id).FirstOrDefault() : (from i in context.Users where i.FIO == tPost.Text select i.id).FirstOrDefault();
                    int storageId = cmbStock.SelectedItem != null ? (from i in context.Storages where i.name == cmbStock.SelectedItem.ToString() select i.id).FirstOrDefault() : 0;
                    int carrierId = cmbCarrier.SelectedItem != null ? (from i in context.Carriers where i.name == cmbCarrier.SelectedItem.ToString() select i.id).FirstOrDefault() : 0;
                    string redactorName = (from i in context.Settings select i.fioOperator).FirstOrDefault();

                    // Если выбрать сохранить машину
                    if (saveCar)
                    {
                        SaveCar(carMark, client, name, surname, patronimyc, carNumber);

                        #region Старое сохранение автомобиля
                        //var markId = from i in context.CarMarks
                        //             where i.name == carMark
                        //             select i.id;
                        //// Если марки то создать новую
                        //if (!markId.Any())
                        //{
                        //    DBModel.Models.CarMarks newCarMarks = new DBModel.Models.CarMarks
                        //    {
                        //        name = carMark,
                        //        updated = DateTime.Now
                        //    };
                        //    context.CarMarks.Add(newCarMarks);
                        //    context.SaveChanges();
                        //    markIdf = (from i in context.CarMarks where i.name == carMark select i.id).FirstOrDefault();
                        //}
                        //else
                        //{
                        //    markIdf = markId.FirstOrDefault();
                        //}

                        //var clientId = from i in context.Clients
                        //               where i.name == client
                        //               select i.id;
                        //if (!clientId.Any())
                        //{
                        //    DBModel.Models.Clients newClient = new DBModel.Models.Clients
                        //    {
                        //        name = client,
                        //        //startDate = DateTime.Now,
                        //        //endDate = DateTime.Now,
                        //        factor = double.Parse(tAdjustment.Text != "" ? tAdjustment.Text : "0" ),
                        //        updated = DateTime.Now
                        //    };
                        //    context.Clients.Add(newClient);
                        //    context.SaveChanges();
                        //    clientIdf = (from i in context.Clients orderby i.id descending select i.id).FirstOrDefault();
                        //}
                        //else
                        //{
                        //    clientIdf = clientId.FirstOrDefault();
                        //}
                        //var driverId = (from i in context.Drivers
                        //                where i.surname == surname &&
                        //                      i.name == name &&
                        //                      i.patronimyc == patronimyc
                        //                select i);
                        //driverIdf = driverId.FirstOrDefault() != null ? driverId.FirstOrDefault().id : 0;
                        //if (!driverId.Any())
                        //{
                        //    if (surname != "")
                        //    {
                        //        DBModel.Models.Drivers newDriver = new DBModel.Models.Drivers
                        //        {
                        //            surname = surname,
                        //            name = name,
                        //            patronimyc = patronimyc,
                        //            udl = "",
                        //            updated = DateTime.Now
                        //        };
                        //        context.Drivers.Add(newDriver);
                        //        context.SaveChanges();
                        //        driverIdf = (from i in context.Drivers orderby i.id descending select i.id).FirstOrDefault();
                        //    }
                        //}

                        //var car = from i in context.Cars
                        //          where i.number == carNumber
                        //          select i;
                        //if (!car.Any())
                        //{
                        //    DBModel.Models.Cars newCar = new DBModel.Models.Cars
                        //    {
                        //        number = carNumber,
                        //        markId = markIdf,
                        //        clientId = clientIdf,
                        //        driverId = driverIdf != 0 ? driverIdf.ToString() : string.Empty,
                        //        updated = DateTime.Now
                        //    };
                        //    context.Cars.Add(newCar);
                        //    context.SaveChanges();
                        //}
                        //else
                        //{
                        //    List<string> oldDriverId = new List<string>();
                        //    if (driverIdf != default)
                        //    {
                        //        oldDriverId = car.FirstOrDefault().driverId.Split(',').ToList();
                        //        if (!oldDriverId.Contains(driverIdf.ToString()))
                        //            oldDriverId.Add(driverIdf.ToString());
                        //    }
                        //    car.FirstOrDefault().clientId = clientIdf;
                        //    if (oldDriverId.Count != 0)
                        //    {
                        //        car.FirstOrDefault().driverId = string.Join(",", oldDriverId);
                        //    }
                        //    context.SaveChanges();
                        //}
                        #endregion
                    }

                    int clientId = (from i in context.Clients where i.name == client select i.id).FirstOrDefault();

                    int driverId = (from i in context.Drivers
                                    where i.surname == surname &&
                                    i.name == name &&
                                    i.patronimyc == patronimyc
                                    select i.id).FirstOrDefault();

                    
                    Weighings newWeighing = new Weighings();
                    newWeighing = NewWeighing(
                        clientIdf: clientId,
                        carNumber: carNumber,
                        carMark: carMark,
                        userId: userId,
                        storageId: storageId,
                        carrierId: carrierId,
                        driverId: driverId,
                        driver: driver,
                        redactor: redactor,
                        redactorName: redactor ? redactorName : ""
                        );

                    #region Старое определение новой записи
                    //newWeighing.material            = cmbGoods.SelectedItem.ToString();
                    //newWeighing.carWeight           = redactor ? int.Parse(tWeightEmptyCar.Text) : weightEmptyCar;
                    //newWeighing.brutto              = redactor ? int.Parse(tWeightFullCar.Text) : weightFullCar;
                    //newWeighing.clientId            = clientId;
                    //newWeighing.dateWeight          = dateTime.Value.AddTicks(-(dateTime.Value.Ticks % TimeSpan.TicksPerSecond));
                    //newWeighing.userId              = string.IsNullOrEmpty(tPost.Text) ? (from i in context.Users where i.username == "admin" select i.id).FirstOrDefault() : (from i in context.Users where i.FIO == tPost.Text select i.id).FirstOrDefault();
                    //newWeighing.carNumber           = carNumber;
                    //newWeighing.carMark             = carMark;
                    //newWeighing.docWeight           = tWeightDoc.Text != "" ? (Double?)Double.Parse(tWeightDoc.Text) : null;
                    //newWeighing.consignee           = cmbConsignee.SelectedItem != null ? cmbConsignee.SelectedItem.ToString() : "";
                    //newWeighing.point1              = cmbPoint1.Text;
                    //newWeighing.point2              = cmbPoint2.Text;
                    //newWeighing.operationType       = rbSales.Checked ? "Сбыт" : "Поставка";
                    //newWeighing.factorWeight        = newWeighing.brutto != 0 ? (Double?)(newWeighing.brutto - newWeighing.carWeight) : null;
                    //newWeighing.storageId           = cmbStock.SelectedItem != null ? (from i in context.Storages where i.name == cmbStock.SelectedItem.ToString() select i.id).FirstOrDefault() : 0;
                    //newWeighing.carrierid           = cmbCarrier.SelectedItem != null ? (from i in context.Carriers where i.name == cmbCarrier.SelectedItem.ToString() select i.id).FirstOrDefault() : 0;
                    //if (redactor)
                    //{               
                    //    DialogResult dialog = MessageBox.Show($"Сделать текущее время({DateTime.Now.AddTicks(-(dateTime.Value.Ticks % TimeSpan.TicksPerSecond))}) временем выезда?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //    if (dialog == DialogResult.Yes)
                    //    {
                    //        newWeighing.dateOut = finishinwWeighing.dateOut;
                    //    }
                    //    else
                    //        newWeighing.dateOut = (weightFullCar != 0 || finish) ? (DateTime?)DateTime.Now : null;

                    //    newWeighing.edited = DateTime.Now;
                    //    newWeighing.redactor = redactorName;
                    //}
                    //else
                    //    newWeighing.dateOut         = (weightFullCar != 0 || finish) ? (DateTime?)DateTime.Now : null;
                    //newWeighing.updated             = DateTime.Now;
                    //newWeighing.price               = Double.Parse(tPrice.Text);
                    //newWeighing.orderNumber         = cmbNumOrder.Text;
                    //newWeighing.orderPositionNumber = cmbPosOrder.Text;
                    //newWeighing.consignor           = cmbConsignor.SelectedItem != null ? cmbConsignor.SelectedItem.ToString() : "";
                    //if (newWeighing.brutto != 0)
                    //{
                    //    newWeighing.fullWeightScale = scale.nameUser;
                    //}
                    //if (newWeighing.carWeight != 0)
                    //{
                    //    newWeighing.taraScale = scale.nameUser;
                    //}

                    //double adj;
                    //newWeighing.adjustment = double.TryParse(tAdjustment.Text.Replace('.', ','), out adj)/*tAdjustment.Text != ""*/ ? (double?)adj : null;
                    //newWeighing.description = tDesc.Text;
                    //newWeighing.driver = driver.Trim(' ');
                    //var driverID = (from i in context.Drivers
                    //                where i.surname == surname &&
                    //                i.name == name &&
                    //                i.patronimyc == patronimyc
                    //                select i.id).FirstOrDefault();
                    //newWeighing.driverId = (from i in context.Drivers
                    //                        where i.surname == surname &&
                    //                        i.name == name &&
                    //                        i.patronimyc == patronimyc
                    //                        select i.id).FirstOrDefault();
                    #endregion

                    fullWeightingFinishDate = newWeighing.dateWeight;

                    // Обновление данных машины
                    if (saveCar)
                    {
                        DBModel.Models.Cars updtCar = (from i in context.Cars where i.number == carNumber select i).FirstOrDefault();
                        updtCar.weight = newWeighing.carWeight;
                        context.SaveChanges();
                    }

                    var dataSetting = (from i in context.Settings
                                       where i.id == 1
                                       select new
                                       {
                                           i.UploadDelay
                                       }).FirstOrDefault();
                    
                    saveToXML();

                    DateTime dateWeighing = finish ? 
                        /*dateTime.Value.AddTicks(-(dateTime.Value.Ticks % TimeSpan.TicksPerSecond))*/
                        finishinwWeighing.dateWeight : 
                        (from i in context.Weighings 
                         orderby i.id descending 
                         where i.taraScale == scale.nameUser 
                         select i.dateWeight).FirstOrDefault();

                    var weighingTable = (from i in context.Weighings
                                         where i.dateWeight == dateWeighing
                                         select i).FirstOrDefault();

                    string scaleSave = IsSave();

                    if (!(scaleSave == "true"))//
                    {
                        if (newCameras.Count != 0)//
                        {
                            if (finish && EmptyWeightCreated)
                            {
                                WeighingFactLinks wfl = (from i in context.WeighingFactLinks
                                                         orderby i.id descending
                                                         select i).FirstOrDefault();
                                var weighingID = (from i in context.Weighings
                                                  where i.dateWeight == dateWeighing
                                                  select i.id).FirstOrDefault();
                                wfl.weighingId = weighingID;
                                Weighings weightRemove;
                                for (int j = 0; j < 200; j++)
                                {
                                    Thread.Sleep(20);
                                    weightRemove = (from i in context.Weighings
                                                    orderby i.id descending
                                                    where i.taraScale == scale.nameUser
                                                    select i).FirstOrDefault();
                                    if (weightRemove.carWeight == null && weightRemove.brutto == null)
                                    {
                                        context.Weighings.Remove(weightRemove);
                                        break;
                                    }
                                }
                                context.SaveChanges();
                            }
                            if (weighingTable != null)
                            {
                                if ((weighingTable.brutto == null && weighingTable.carWeight == null && EmptyWeightCreated) || finish)
                                {
                                    weighingTable.material              = newWeighing.material;
                                    weighingTable.carWeight             = newWeighing.carWeight;
                                    weighingTable.brutto                = newWeighing.brutto;
                                    weighingTable.clientId              = newWeighing.clientId;
                                    weighingTable.dateWeight            = newWeighing.dateWeight;
                                    weighingTable.userId                = finish ? finishinwWeighing.userId : newWeighing.userId;
                                    weighingTable.carNumber             = newWeighing.carNumber;
                                    weighingTable.carMark               = newWeighing.carMark;
                                    weighingTable.docWeight             = newWeighing.docWeight;
                                    weighingTable.consignee             = newWeighing.consignee;
                                    weighingTable.point1                = newWeighing.point1;
                                    weighingTable.point2                = newWeighing.point2;
                                    weighingTable.operationType         = newWeighing.operationType;
                                    weighingTable.factorWeight          = newWeighing.factorWeight;
                                    weighingTable.storageId             = newWeighing.storageId;
                                    weighingTable.carrierid             = newWeighing.carrierid;
                                    weighingTable.dateOut               = newWeighing.dateOut;
                                    weighingTable.updated               = newWeighing.updated;
                                    weighingTable.price                 = newWeighing.price;
                                    weighingTable.orderNumber           = newWeighing.orderNumber;
                                    weighingTable.orderPositionNumber   = newWeighing.orderPositionNumber;
                                    weighingTable.consignor             = newWeighing.consignor;
                                    weighingTable.adjustment            = newWeighing.adjustment;
                                    weighingTable.description           = newWeighing.description;
                                    weighingTable.driver                = newWeighing.driver;
                                    weighingTable.driverId              = newWeighing.driverId;
                                    if (weighingTable.carWeight != 0 && weighingTable.taraScale == null)
                                        weighingTable.taraScale = scale.nameUser;
                                    if (weighingTable.brutto != 0 && weighingTable.fullWeightScale == null)
                                        weighingTable.fullWeightScale = scale.nameUser;
                                    weighingTable.edited = newWeighing.edited;
                                    weighingTable.redactor = newWeighing.redactor;
                                }
                                else
                                {
                                    context.Weighings.Add(newWeighing);
                                }
                            }
                            else
                            {
                                context.Weighings.Add(newWeighing);
                            }


                            context.SaveChanges();
                        }
                        else
                        {
                            if (finish)
                            {
                                weighingTable.material              = newWeighing.material;
                                weighingTable.carWeight             = newWeighing.carWeight;
                                weighingTable.brutto                = newWeighing.brutto;
                                weighingTable.clientId              = newWeighing.clientId;
                                weighingTable.dateWeight            = newWeighing.dateWeight;
                                weighingTable.userId                = finishinwWeighing.userId;
                                weighingTable.carNumber             = newWeighing.carNumber;
                                weighingTable.carMark               = newWeighing.carMark;
                                weighingTable.docWeight             = newWeighing.docWeight;
                                weighingTable.consignee             = newWeighing.consignee;
                                weighingTable.point1                = newWeighing.point1;
                                weighingTable.point2                = newWeighing.point2;
                                weighingTable.operationType         = newWeighing.operationType;
                                weighingTable.factorWeight          = newWeighing.factorWeight;
                                weighingTable.storageId             = newWeighing.storageId;
                                weighingTable.carrierid             = newWeighing.carrierid;
                                weighingTable.dateOut               = newWeighing.dateOut;
                                weighingTable.updated               = newWeighing.updated;
                                weighingTable.price                 = newWeighing.price;
                                weighingTable.orderNumber           = newWeighing.orderNumber;
                                weighingTable.orderPositionNumber   = newWeighing.orderPositionNumber;
                                weighingTable.consignor             = newWeighing.consignor;
                                weighingTable.adjustment            = newWeighing.adjustment;
                                weighingTable.description           = newWeighing.description;
                                weighingTable.driver                = newWeighing.driver;
                                weighingTable.driverId              = newWeighing.driverId;
                                if (weighingTable.carWeight != 0 && weighingTable.taraScale == null)
                                {
                                    weighingTable.taraScale = scale.nameUser;
                                }
                                if (weighingTable.brutto != 0 && weighingTable.fullWeightScale == null)
                                {
                                    weighingTable.fullWeightScale = scale.nameUser;
                                }
                                weighingTable.edited = newWeighing.edited;
                                weighingTable.redactor = newWeighing.redactor;
                                context.SaveChanges();
                            }
                            else
                            {

                                DBModel.Models.Weighings newRow = newWeighing;
                                context.Weighings.Add(newRow);
                                context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        if (trailer)
                        {
                            if (finish)
                            {
                                weighingTable.material              = newWeighing.material;
                                weighingTable.carWeight             = newWeighing.carWeight;
                                weighingTable.brutto                = newWeighing.brutto;
                                weighingTable.clientId              = newWeighing.clientId;
                                weighingTable.dateWeight            = newWeighing.dateWeight;
                                weighingTable.userId                = finishinwWeighing.userId;
                                weighingTable.carNumber             = newWeighing.carNumber;
                                weighingTable.carMark               = newWeighing.carMark;
                                weighingTable.docWeight             = newWeighing.docWeight;
                                weighingTable.consignee             = newWeighing.consignee;
                                weighingTable.point1                = newWeighing.point1;
                                weighingTable.point2                = newWeighing.point2;
                                weighingTable.operationType         = newWeighing.operationType;
                                weighingTable.factorWeight          = newWeighing.factorWeight;
                                weighingTable.storageId             = newWeighing.storageId;
                                weighingTable.carrierid             = newWeighing.carrierid;
                                weighingTable.dateOut               = newWeighing.dateOut;
                                weighingTable.updated               = newWeighing.updated;
                                weighingTable.price                 = newWeighing.price;
                                weighingTable.orderNumber           = newWeighing.orderNumber;
                                weighingTable.orderPositionNumber   = newWeighing.orderPositionNumber;
                                weighingTable.consignor             = newWeighing.consignor;
                                weighingTable.adjustment            = newWeighing.adjustment;
                                weighingTable.description           = newWeighing.description;
                                weighingTable.driver                = newWeighing.driver;
                                weighingTable.driverId              = newWeighing.driverId;
                                if (weighingTable.carWeight != 0 && weighingTable.taraScale == null)
                                {
                                    weighingTable.taraScale = scale.nameUser;
                                }
                                if (weighingTable.brutto != 0 && weighingTable.fullWeightScale == null)
                                {
                                    weighingTable.fullWeightScale = scale.nameUser;
                                }
                                weighingTable.edited = newWeighing.edited;
                                weighingTable.redactor = newWeighing.redactor;
                            }
                            else
                            {
                                DBModel.Models.Weighings newRow = newWeighing;
                                context.Weighings.Add(newRow);
                            }

                            var UrlVideoCamEnabled = ((this.MdiParent as Main).scalesList.Find(x => x.Active == true).Libra.arrVid);


                            if (UrlVideoCamEnabled != default && UrlVideoCamEnabled.Count() > 0 && UrlVideoCamEnabled[0] == "")
                            {
                                DBModel.Models.WeighingFactLinks newRow = new WeighingFactLinks
                                {
                                    weighingId = (from i in context.Weighings orderby i.updated descending select i.id).FirstOrDefault(),
                                    factId = (from i in context.AutoWeighingFacts orderby i.id descending select i.id).FirstOrDefault(),
                                    weightKind = 0,
                                    dateWeight = (from i in context.Weighings orderby i.updated descending select i.dateWeight).FirstOrDefault()
                                };
                            }
                            context.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Для повторного взвешивания необходимо освободить весы");
                            weightFullCar = 0;
                            tWeightFullCar.Text = "0";
                        }
                    }

                    if (tWeightFullCar.Text != "0" && tWeightEmptyCar.Text != "0")
                        btnTicket.Enabled = true;
                    if (newWeighing.brutto != 0)
                        btnTicket.Enabled = true;
                    var settingsRow = (from i in context.Settings
                                       where i.id == 1
                                       select i).FirstOrDefault();
                    if (settingsRow.UploadDelay == "1")
                    {
                        try
                        {
                            UploadHelper.Upload();
                        }
                        catch (WebException ex)
                        {
                            HttpWebResponse resp = (HttpWebResponse)ex.Response;
                            if (resp.StatusCode == HttpStatusCode.BadGateway)
                                MessageBox.Show("Не удалось выгрузить данные, так как остутствует подключение к интернету");
                        }
                        catch
                        {
                            MessageBox.Show("Не удалсь выгрузить данные, возможно отсутсвует подключение");
                        }
                    }

                    scale.isSave = "true";
                    context.SaveChanges();

                    BtnReset_Click(sender, e);

                    dataGridView.Enabled = false;
                    btnSave.Enabled = false;
                    btnReset.Enabled = true;
                    panel2.Enabled = false;
                    panel1.Enabled = false;
                    isSave = true;
                }
                else
                {
                    MessageBox.Show("Указаны не все обезательные данные");
                }
                (this.MdiParent as Main).RefreshJournal();

            }
        }

        public void SaveCar(string carMark, string client, string name, string surname, string patronimyc, string carNumber)
        {
            int markIdf;
            int clientIdf;
            int driverIdf = 0;
            using (DataBaseContext context = new DataBaseContext())
            {
                var markId = from i in context.CarMarks
                             where i.name == carMark
                             select i.id;
                // Если марки то создать новую
                if (!markId.Any())
                {
                    DBModel.Models.CarMarks newCarMarks = new DBModel.Models.CarMarks
                    {
                        name = carMark,
                        updated = DateTime.Now
                    };
                    context.CarMarks.Add(newCarMarks);
                    context.SaveChanges();
                    markIdf = (from i in context.CarMarks where i.name == carMark select i.id).FirstOrDefault();
                }
                else
                {
                    markIdf = markId.FirstOrDefault();
                }

                var clientId = from i in context.Clients
                               where i.name == client
                               select i.id;
                if (!clientId.Any())
                {
                    DBModel.Models.Clients newClient = new DBModel.Models.Clients
                    {
                        name = client,
                        //startDate = DateTime.Now,
                        //endDate = DateTime.Now,
                        factor = double.Parse(tAdjustment.Text != "" ? tAdjustment.Text : "0"),
                        updated = DateTime.Now
                    };
                    context.Clients.Add(newClient);
                    context.SaveChanges();
                    clientIdf = (from i in context.Clients orderby i.id descending select i.id).FirstOrDefault();
                }
                else
                {
                    clientIdf = clientId.FirstOrDefault();
                }
                var driverId = (from i in context.Drivers
                                where i.surname == surname &&
                                      i.name == name &&
                                      i.patronimyc == patronimyc
                                select i);
                if (!driverId.Any())
                {
                    if (surname != "")
                    {
                        DBModel.Models.Drivers newDriver = new DBModel.Models.Drivers
                        {
                            surname = surname,
                            name = name,
                            patronimyc = patronimyc,
                            udl = "",
                            updated = DateTime.Now
                        };
                        context.Drivers.Add(newDriver);
                        context.SaveChanges();
                    }
                }

                var car = from i in context.Cars
                          where i.number == carNumber
                          select i;
                if (!car.Any())
                {
                    DBModel.Models.Cars newCar = new DBModel.Models.Cars
                    {
                        number = carNumber,
                        markId = markIdf,
                        clientId = clientIdf,
                        driverId = driverIdf != 0 ? driverIdf.ToString() : string.Empty,
                        updated = DateTime.Now
                    };
                    context.Cars.Add(newCar);
                    context.SaveChanges();
                }
                else
                {
                    List<string> oldDriverId = new List<string>();
                    if (driverIdf != default)
                    {
                        oldDriverId = car.FirstOrDefault().driverId.Split(',').ToList();
                        if (!oldDriverId.Contains(driverIdf.ToString()))
                            oldDriverId.Add(driverIdf.ToString());
                    }
                    car.FirstOrDefault().clientId = clientIdf;
                    if (oldDriverId.Count != 0)
                    {
                        car.FirstOrDefault().driverId = string.Join(",", oldDriverId);
                    }
                    context.SaveChanges();
                }
            }
           
        }

        /// <summary>
        /// Определение новой записи
        /// </summary>
        /// <param name="clientIdf">id клиента</param>
        /// <param name="carNumber">id машины</param>
        /// <param name="userId">id пользователя</param>
        /// <param name="storageId">id склада</param>
        /// <param name="carrierId">id перевозчика</param>
        /// <param name="driverId">id водителя</param>
        /// <param name="redactor">проверка на редактора записей</param>
        /// <param name="redactorName">имя редактора</param>
        /// <returns>
        /// Новая запись
        /// </returns>
        public Weighings NewWeighing(int clientIdf, string carNumber, string carMark, int userId, int storageId, int carrierId, int driverId, string driver, bool redactor, string redactorName = "")
        {
            Weighings newWeighing = new Weighings();
            newWeighing.material = cmbGoods.SelectedItem.ToString();
            newWeighing.carWeight = redactor ? int.Parse(tWeightEmptyCar.Text) : weightEmptyCar;
            newWeighing.brutto = redactor ? int.Parse(tWeightFullCar.Text) : weightFullCar;
            newWeighing.clientId = clientIdf;
            newWeighing.dateWeight = dateTime.Value.AddTicks(-(dateTime.Value.Ticks % TimeSpan.TicksPerSecond));
            newWeighing.userId = userId;
            newWeighing.carNumber = carNumber;
            newWeighing.carMark = carMark;
            newWeighing.docWeight = tWeightDoc.Text != "" ? (Double?)Double.Parse(tWeightDoc.Text) : null;
            newWeighing.consignee = cmbConsignee.SelectedItem != null ? cmbConsignee.SelectedItem.ToString() : "";
            newWeighing.point1 = cmbPoint1.Text;
            newWeighing.point2 = cmbPoint2.Text;
            newWeighing.operationType = rbSales.Checked ? "Сбыт" : "Поставка";
            newWeighing.factorWeight = newWeighing.brutto != 0 ? (Double?)(newWeighing.brutto - newWeighing.carWeight) : null;
            newWeighing.storageId = storageId;
            newWeighing.carrierid = carrierId;
            if (redactor)
            {
                DialogResult dialog = MessageBox.Show($"Сделать текущее время({DateTime.Now.AddTicks(-(dateTime.Value.Ticks % TimeSpan.TicksPerSecond))}) временем выезда?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    newWeighing.dateOut = finishinwWeighing.dateOut;
                }
                else
                    newWeighing.dateOut = (weightFullCar != 0 || finish) ? (DateTime?)DateTime.Now : null;

                newWeighing.edited = DateTime.Now;
                newWeighing.redactor = redactorName;
            }
            else
                newWeighing.dateOut = (weightFullCar != 0 || finish) ? (DateTime?)DateTime.Now : null;
            newWeighing.updated = DateTime.Now;
            newWeighing.price = Double.Parse(tPrice.Text);
            newWeighing.orderNumber = cmbNumOrder.Text;
            newWeighing.orderPositionNumber = cmbPosOrder.Text;
            newWeighing.consignor = cmbConsignor.SelectedItem != null ? cmbConsignor.SelectedItem.ToString() : "";
            if (newWeighing.brutto != 0)
            {
                newWeighing.fullWeightScale = scale.nameUser;
            }
            if (newWeighing.carWeight != 0)
            {
                newWeighing.taraScale = scale.nameUser;
            }

            double adj;
            newWeighing.adjustment = double.TryParse(tAdjustment.Text.Replace('.', ','), out adj)/*tAdjustment.Text != ""*/ ? (double?)adj : null;
            newWeighing.description = tDesc.Text;
            newWeighing.driver = driver.Trim(' ');

            newWeighing.driverId = driverId;
            return newWeighing;
        }

        /// <summary>
        /// Пинг камеры
        /// </summary>
        /// <param name="nameOrAddress">Камера</param>
        /// <returns></returns>
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

        /// <summary>
        /// Проверка камер
        /// </summary>
        private void CheckURLVideo()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var s = (from i in context.Settings
                         where i.id == 1
                         select i).FirstOrDefault();
                s.UrlVideoCamEnabled = "";
                s.UrlVideoCamNumEnabled = "";
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
                        }
                        else
                        {
                            camArr.RemoveAt(i);
                            i--;
                            k--;
                        }
                    }
                    s.UrlVideoCamEnabled = string.Join("|", camArr);

                    if (!string.IsNullOrEmpty(s.UrlVideoCamNum))
                    {
                        List<string> camNumArr = s.UrlVideoCamNum.Split('|').ToList();
                        int c = camArr.Count();
                        for (int i = 0; i < c; i++)
                        {
                            Uri uriResult;
                            bool result = true;
                            if (!Uri.TryCreate(camNumArr[i], UriKind.Absolute, out uriResult) || null == uriResult)
                                result = false;
                            if (result)
                            {
                                if (!PingHost(new Uri(camNumArr[i]).Host))
                                {
                                    camNumArr.RemoveAt(i);
                                    i--;
                                    c--;
                                }
                            }
                            else
                            {
                                camNumArr.RemoveAt(i);
                                i--;
                                c--;
                            }
                        }
                        s.UrlVideoCamNumEnabled = string.Join("|", camNumArr);
                    }
                }
                context.SaveChanges();
            }           
        }

        /// <summary>
        /// Кнопка не сохранять автомобиль
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void ToolDontSaveCar_Click(object sender, EventArgs e)
        {
            if (saveCar)
            {
                saveCar = false;
                toolDontSaveCar.BackColor = Color.Orange;
                dataGridView.Enabled = false;
                dataGridView.RowsDefaultCellStyle.BackColor = Color.Gray;
            }
            else
            {
                saveCar = true;
                toolDontSaveCar.BackColor = Color.White;
                dataGridView.Enabled = true;
                dataGridView.RowsDefaultCellStyle.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Заполнение полей
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void DataGridView_Click(object sender, EventArgs e)
        {
            ClearCamArray();
            using (DataBaseContext context = new DataBaseContext())
            {
                if (dataGridView.CurrentRow != null)
                {
                    #region Заполнение полей
                    string num = dataGridView.CurrentRow.Cells[0].Value.ToString();

                    var carsRow = (from i in context.Cars
                                   where i.number == num
                                   select i);

                    int markId = carsRow.FirstOrDefault().markId;
                    int? clientId = carsRow.FirstOrDefault().clientId;


                    var driverId = carsRow.FirstOrDefault().driverId.Split(','); 

                    var clientsRow = (from i in context.Clients
                                      where i.id == clientId
                                      select i);

                    DateTime startDate = clientsRow.FirstOrDefault().startDate;
                    DateTime endDate = clientsRow.FirstOrDefault().endDate;
                    if (startDate != null && endDate != null)
                        if (DateTime.Now.Date >= startDate.Date && DateTime.Now.Date <= endDate.Date)
                            tAdjustment.Text = (Convert.ToDouble(clientsRow.FirstOrDefault().factor)).ToString();

                    toolStripCmbMark.Items.Clear();
                    toolStripCmbMark.Items.Add((from i in context.CarMarks
                                                where i.id == markId
                                                select i.name).FirstOrDefault());
                    toolStripCmbMark.SelectedIndex = 0;
                    if (toolStripCmbClient.Items.Contains(clientsRow.FirstOrDefault().name))
                    {
                        toolStripCmbClient.Text = clientsRow.FirstOrDefault().name;
                    }
                    toolStripCmbDriver.Items.Clear();
                    if (driverId != null)
                    {
                        var driversTable = from i in context.Drivers
                                           select i;
                        foreach (string id in driverId)
                        {
                            var driversRow = (from i in driversTable
                                              where i.id.ToString() == id
                                              select new
                                              {
                                                  i.surname,
                                                  i.name,
                                                  i.patronimyc
                                              }).FirstOrDefault();
                            if (driversRow != null)
                                toolStripCmbDriver.Items.Add((driversRow.surname + " " + driversRow.name + " " + driversRow.patronimyc).Trim(' '));
                        }
                        
                    }
                    if (toolStripCmbDriver.Items.Count != 0)
                    {
                        string lastDriver = (from i in context.Weighings
                                          orderby i.id descending
                                          where i.carNumber == num
                                          select i.driver).FirstOrDefault();
                        if (toolStripCmbDriver.Items.Contains((lastDriver ?? "").Trim(' ')))
                        {
                            toolStripCmbDriver.Text = lastDriver.Trim(' ');
                        }
                        else
                        {
                            toolStripCmbDriver.SelectedIndex = 0;
                        }
                    }                  
                    toolStripTxtNumber.Text = num;
                    tWeightEmptyCar.Text = carsRow.FirstOrDefault().weight.ToString();
                    weightEmptyCar = carsRow.FirstOrDefault().weight.GetValueOrDefault();
                    #endregion
                }
            }
            
        }

        private void ToolStripTalon_Click(object sender, EventArgs e)
        {
            DocHelper.weighingTalon(fullWeightingFinishDate);
        }

        private void ToolStripTTN_Click(object sender, EventArgs e)
        {
            DocHelper.ttn(fullWeightingFinishDate);
        }

        private void ToolStripNakl1_Click(object sender, EventArgs e)
        {
            Invoice frmInvoice = new Invoice(fullWeightingFinishDate);
            frmInvoice.ShowDialog();
            
        }

        private void ToolStripNakl2_Click(object sender, EventArgs e)
        {
            DocHelper.nakl2(fullWeightingFinishDate);
        }

        private void BtnTicket_Click(object sender, EventArgs e)
        {
            contextMenuStrip.Show(btnTicket, new Point(0, btnTicket.Height));
        }

        private void ctxMenuItem_Select(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.Arrow;
        }

        private void Weighing_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearCamArray();
            using (DataBaseContext context = new DataBaseContext())
            {
                if (!isSave)
                {
                    DialogResult dialogResult = MessageBox.Show("Все несохраненные данные будут потеряны! Вы уверены?", "Предупреждение", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }    
            }

        }

        private void Weighing_FormClosed(object sender, FormClosedEventArgs e)
        {
            //((this.MdiParent as Main).libra as Libra).WeightChanged -= Libra_WeightChanged;
            //((MdiParent as Main).scalesList.Find(x => x.Active == true).Libra as Libra).WeightChanged -= Libra_WeightChanged;
            ClearCamArray();
            (this.MdiParent as Main).frmWeighing = null;
            this.Dispose(true);
            //this.Hide();
        }

        private void ToolTrailer_Click(object sender, EventArgs e)
        {
            if (trailer)
            {
                trailer = false;
                toolTrailer.BackColor = Color.White;
            }
            else
            {
                trailer = true;
                toolTrailer.BackColor = Color.Orange;
            }
        }

        /// <summary>
        /// Обновление таблицы
        /// </summary>
        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                bindingSource.DataSource = (from i in context.Cars
                                            join mU in context.CarMarks on i.markId equals mU.id into mj
                                            from m in mj.DefaultIfEmpty()
                                            join cU in context.Clients on i.clientId equals cU.id into cj
                                            from c in cj.DefaultIfEmpty()
                                            select new
                                            {
                                                i.number,
                                                mark = m.name,
                                                i.weight,
                                                c.name
                                            }).ToList();
            }          
        }

        /// <summary>
        /// Заполнение поля корректировки
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">KeyPressEventArgs</param>
        private void TAdjustment_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == ',') && !(e.KeyChar == '.') && !(e.KeyChar == '-');
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                if (tAdjustment.Text.Contains('.') || tAdjustment.Text.Contains(','))
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == '-')
            {
                if (tAdjustment.Text.Contains('-') || tAdjustment.Text.Contains('.') || tAdjustment.Text.Contains(','))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Выбор нового клиента
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void ToolStripCmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DataBaseContext context =  new DataBaseContext())
            {
                if (!finish)
                    tAdjustment.Text = (from i in context.Clients
                                        where i.name == toolStripCmbClient.Text &&
                                        i.startDate <= dateTime.Value.Date &&
                                        i.endDate >= dateTime.Value.Date
                                        select i.factor).FirstOrDefault().ToString();
            }
        }

        private void toolStripTxtNumber_Click(object sender, EventArgs e)
        {
            ClearCamArray();
            newWeight = false;
        }

        private void Weighing_Resize(object sender, EventArgs e)
        {
            int newHeight = this.Height - panel1.Height - bindingNavigator.Height - panel2.Height - panel3.Height - 39;
            dataGridView.Size = new Size(dataGridView.Width, newHeight);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            finish = true;
            using (DataBaseContext context =  new DataBaseContext())
            {
                this.finishinwWeighing = (from i in context.Weighings
                                         where i.dateWeight == fullWeightingFinishDate
                                         select i).FirstOrDefault();
            }
            finish = true;
            isSave = false;
            saveCar = true;
            trailer = false;
            isDetect = false;
            newWeight = true;
            weightChanged = false;
            EmptyWeightCreated = false;

            isSave = true;
            panel1.Enabled = true;
            panel2.Enabled = true;
            btnSave.Enabled = true;
            btnReset.Enabled = true;
            FinishWeighting(finishinwWeighing);
            btnContinue.Visible = false;      
        }

        
        private void tWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}