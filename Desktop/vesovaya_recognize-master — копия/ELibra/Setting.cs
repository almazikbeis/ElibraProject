using ELibra.Classes;
using ELibra.DBModel;
using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using ELibra.Classes.UIElements;

namespace ELibra
{
    public partial class Setting : Form
    {
        //DataBaseContext context = new DataBaseContext();
        List<ScaleTabPage> scaleTabPagesList = new List<ScaleTabPage>();
       
        public Setting()
        {
            InitializeComponent();
            lblVersion.Text += Application.ProductVersion;
        }

        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            TabPage _tabPage = tabControl1.TabPages[e.Index];

            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.Gray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            Font _tabFont = new Font("Arial", 10.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CBoxExportFold_CheckedChanged(object sender, EventArgs e)
        {
            tExportFold.Enabled = cBoxExportFold.Checked;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            bool hasCam = (listCam.Items.Count != 0);
            if (tPathVideo.Text == "" && (hasCam && (hasCam ? (listCam.Items[0].ToString() != "") : false)))
            {
                MessageBox.Show("Заполните путь в видеоархиву!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                using (DataBaseContext context = new DataBaseContext())
                {
                    var scales = from i in context.Scales
                                 select i;
                    foreach (var scale in scales)
                    {
                        var tabPage = scaleTabPagesList.Find(x => x.Name == scale.name).Scale;
                        string model;
                        if (tabPage.page.rBtnMetra.Checked)
                            model = "Rus";
                        //else if (tabPage.page.rBtnTurc.Checked)
                        //    model = "Turk";
                        else if (tabPage.page.rBtnCAS.Checked)
                            model = "CAS";
                        else
                            model = "IP65";
                        double temp;
                        Double.TryParse(tabPage.page.tMinWeightVideo.Text, out temp);
                        string scaleCameras = "";
                        for (int i = 0; i < tabPage.page.listScaleCameras.Items.Count; i++)
                        {
                            if (scaleCameras == "")
                                scaleCameras += tabPage.page.listScaleCameras.Items[i];
                            else
                                scaleCameras += "|" + tabPage.page.listScaleCameras.Items[i];
                        }

                        scale.nameUser       = tabPage.page.tScaleName.Text;
                        scale.Model          = model;
                        scale.PortName       = tabPage.page.cmbPortName.SelectedItem != null ? tabPage.page.cmbPortName.SelectedItem.ToString() : "";
                        scale.MinWeightVideo = temp.ToString();
                        scale.BaudRate       = tabPage.page.cmbBaudRate.SelectedItem.ToString();
                        scale.DataBits       = tabPage.page.cmbDataBits.SelectedItem.ToString();
                        scale.Parity         = tabPage.page.cmbParity.SelectedItem.ToString();
                        scale.RtsEnable      = tabPage.page.cBoxRTS.Checked.ToString().ToLower();
                        scale.StopBits       = tabPage.page.cmbStopBits.SelectedItem.ToString();
                        scale.Handshake      = tabPage.page.cmbHandshake.SelectedItem.ToString();
                        //scale.MinWeightVideo = tabPage.page.MinWeight.Text;
                        scale.LinkedCameras  = scaleCameras;
                        scale.RecognizeDelay = (double)tabPage.page.numDelay.Value;
                    }

                    var settings = (from i in context.Settings
                                    where i.id == 1
                                    select i).FirstOrDefault();

                    #region Заполнение переменных
                    //string model;
                    //if (rBtnMetra.Checked)
                    //    model = "Rus";
                    //else if (rBtnTurc.Checked)
                    //    model = "Turk";
                    //else
                    //    model = "IP65";

                    ////double temp;
                    //Double.TryParse(tMaxWeight.Text, out temp);

                    string urlVideoCam = "";
                    for (int i = 0; i < listCam.Items.Count; i++)
                    {
                        if (urlVideoCam == "")
                            urlVideoCam += listCam.Items[i];
                        else
                            urlVideoCam += "|" + listCam.Items[i];
                    }

                    string urlVideoCamNum = "";
                    for (int i = 0; i < listCamNum.Items.Count; i++)
                    {
                        if (urlVideoCamNum == "")
                            urlVideoCamNum += listCamNum.Items[i];
                        else
                            urlVideoCamNum += "|" + listCamNum.Items[i];
                    }
                    #endregion

                    #region Сохранение настроек
                    //settings.Model = model;
                    //settings.PortName = cmbPortName.SelectedItem != null ? cmbPortName.SelectedItem.ToString() : "";
                    //settings.BaudRate = cmbBaudRate.SelectedItem.ToString();
                    //settings.DataBits = cmbDataBits.SelectedItem.ToString();
                    //settings.Parity = cmbParity.SelectedItem.ToString();
                    //settings.StopBits = cmbStopBits.SelectedItem.ToString();
                    //settings.MaxWeight = temp.ToString();
                    //settings.StopBits = cmbStopBits.SelectedItem.ToString();
                    //settings.Handshake = cmbHandshake.SelectedItem.ToString();
                    //settings.RtsEnable = cBoxRTS.Checked.ToString().ToLower();

                    //сохранение адреса хоста и адреса выгрузки
                    string upload = tUpload.Text.TrimEnd('/');
                    string host = tHost.Text.TrimEnd('/');
                    settings.HostDB                 = upload != "" ? host + "|" + upload : host + "|" + host;
                    settings.LoginDB                = tLogin.Text;
                    settings.PasswordDB             = tPass.Text;
                    settings.UploadDelay            = tUploadDelay.SelectedIndex.ToString();
                    settings.NameCompany            = tNameCompany.Text;
                    settings.Consignee              = cBoxConsignee.Checked.ToString().ToLower();
                    settings.Point1                 = cBoxDispatchLocation.Checked.ToString().ToLower();
                    settings.Point2                 = cBoxPlaceDelivery.Checked.ToString().ToLower();
                    settings.TypeTransaction        = cBoxTypeTransaction.Checked.ToString().ToLower();
                    settings.Carrier                = cBoxCarrier.Checked.ToString().ToLower();
                    settings.Stock                  = cBoxStock.Checked.ToString().ToLower();
                    settings.OrderNumber            = cBoxOrderNumber.Checked.ToString().ToLower();
                    settings.PositionStockNumber    = cBoxPositionStockNumber.Checked.ToString().ToLower();
                    settings.Snipper                = cBoxSnipper.Checked.ToString().ToLower();
                    settings.OrderNumber            = cBoxOrderNumber.Checked.ToString().ToLower();
                    settings.MaxWeight              = tMaxWeight.Text;
                    settings.PathVideo              = tPathVideo.Text;
                    settings.UrlVideoCam            = urlVideoCam;
                    settings.UrlVideoCamNum         = urlVideoCamNum;
                    settings.cBoxExportFold         = cBoxExportFold.Checked.ToString().ToLower();
                    settings.tExportFold            = tExportFold.Text;
                    //settings.RecognizeDelay = (double)numDelay.Value;

                    context.SaveChanges();

                    #endregion

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

        }

        private void Setting_Load(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var settings = (from i in context.Settings
                                where i.id == 1
                                select i).FirstOrDefault();
                var scales = (from i in context.Scales
                              select i);

                #region Заполнение формы настроек

                #region Весы
                foreach (var scale in scales)
                {
                    DrawPage(scale);
                }
                #endregion

                #region Data
                var hostList = settings.HostDB.Split('|').ToList();
                tHost.Text = hostList[0];
                if (hostList.Count > 1)
                {
                    tUpload.Text = hostList[1];
                }
                else
                {
                    tUpload.Text = hostList[0];
                }
                //tHost.Text = settings.HostDB;
                tLogin.Text = settings.LoginDB;
                tPass.Text = settings.PasswordDB;
                try
                {
                    tUploadDelay.SelectedIndex = Int32.Parse(settings.UploadDelay);
                }
                catch
                {
                    tUploadDelay.SelectedIndex = 0;
                }
                #endregion

                #region Owner
                tNameCompany.Text = settings.NameCompany;
                #endregion

                #region Fields
                cBoxConsignee.Checked = settings.Consignee == "true";
                cBoxDispatchLocation.Checked = settings.Point1 == "true";
                cBoxPlaceDelivery.Checked = settings.Point2 == "true";
                cBoxTypeTransaction.Checked = settings.TypeTransaction == "true";
                cBoxCarrier.Checked = settings.Carrier == "true";
                cBoxStock.Checked = settings.Stock == "true";
                cBoxOrderNumber.Checked = settings.OrderNumber == "true";
                cBoxPositionStockNumber.Checked = settings.PositionStockNumber == "true";
                cBoxSnipper.Checked = settings.Snipper == "true";
                #endregion

                #region Video
                tMaxWeight.Text = settings.MaxWeight;
                tPathVideo.Text = settings.PathVideo;
                //numDelay.Value = (decimal)(settings.RecognizeDelay ?? 0);
                if (settings.UrlVideoCam != null)
                    listCam.Items.AddRange(settings.UrlVideoCam.Split('|'));
                if (settings.UrlVideoCamNum != null)
                    listCamNum.Items.AddRange(settings.UrlVideoCamNum.Split('|'));
                #endregion

                #region 1C
                tExportFold.Text = settings.tExportFold;
                
                if (settings.licenseNumber[1] == '0')
                    tabControl1.TabPages.RemoveAt(tabControl1.TabCount - 1);
                if (settings.licenseNumber[4] == '0')
                {
                    this.Controls.Remove(tUploadDelay);
                    this.Controls.Remove(lblUploadDelay);
                }
                #endregion

                #endregion
            }
        }

        private void DrawPage(DBModel.Models.Scales scale)
        {

            TabPageScales pageScales = new TabPageScales();
            using (DataBaseContext context = new DataBaseContext())
            {
                pageScales.tabPageInitialize(
                    scale: scale, 
                    cameras: (from i in context.Settings select i.UrlVideoCam).FirstOrDefault());

                if (scale.Model == "CAS")
                {
                    pageScales.page.rBtnCAS.Checked = true;
                    pageScales.page.rBtnIP65.Checked = false;
                    pageScales.page.rBtnMetra.Checked = false;
                }
                else if (scale.Model == "IP65")
                {
                    pageScales.page.rBtnCAS.Checked = false;
                    pageScales.page.rBtnIP65.Checked = true;
                    pageScales.page.rBtnMetra.Checked = false;
                }
                else if (scale.Model == "Rus")
                {
                    pageScales.page.rBtnCAS.Checked = false;
                    pageScales.page.rBtnIP65.Checked = false;
                    pageScales.page.rBtnMetra.Checked = true;
                }
                if (SerialPort.GetPortNames().Count() != 0)
                {
                    

                    //var portNames = SerialPort.GetPortNames();
                    List<string> portNames = new List<string>();
                    portNames.AddRange(SerialPort.GetPortNames());
                    var portScales = (from i in context.Scales
                                      where i.PortName != scale.PortName
                                     select i.PortName).ToList().Distinct();
                    
                    pageScales.page.cmbPortName.Items.AddRange(portNames.Except(portScales).ToArray());

                    var portName = scale.PortName ?? "";
                    if (pageScales.page.cmbPortName.Items.IndexOf(portName) != -1)
                    {
                        pageScales.page.cmbPortName.SelectedIndex = pageScales.page.cmbPortName.Items.IndexOf(portName);
                    }
                    else
                    {
                        pageScales.page.cmbPortName.Text = "";
                    }
                }


                if (scale.name == "Scales1")
                {
                    pageScales.page.scalesPage.Enter += new EventHandler(tabScales_Enter);
                    pageScales.page.scalesPage.Leave += new EventHandler(tabScales_Leave);
                }
                TabPage newPageScales = pageScales.page.scalesPage;
                
                tabScales.TabPages.Add(newPageScales);
                scaleTabPagesList.Add(new ScaleTabPage
                {
                    Name = scale.name,
                    Scale = pageScales
                });
                if (tabScales.TabCount == 5)
                {
                    btnAddScales.Enabled = false;
                }
            }           
        }

        private void BtnPathVideo_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
                return;
            tPathVideo.Text = folderBrowserDialog.SelectedPath;
        }

        private void BtnPathExport_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
                return;
            tExportFold.Text = folderBrowserDialog.SelectedPath;
        }

        private void BtnAddURLCam_Click(object sender, EventArgs e)
        {
            listCam.Items.Add(tAddURLCam.Text);
            tAddURLCam.Clear();
        }

        private void BtnAddURLCamNum_Click(object sender, EventArgs e)
        {
            listCamNum.Items.Add(tAddURLCamNum.Text);
            tAddURLCamNum.Clear();
        }

        private void BtnDelURLCam_Click(object sender, EventArgs e)
        {
            listCam.Items.Remove(listCam.SelectedItem);
        }

        private void BtnDelURLCamNum_Click(object sender, EventArgs e)
        {
            listCamNum.Items.Remove(listCamNum.SelectedItem);
        }

        private void btnAddScales_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var count = (from i in context.Scales
                             orderby i.id descending
                             select i.id).Count();
                if (SerialPort.GetPortNames().Count() > count)
                {
                    Scales newScale = new Scales
                    {
                        name           = $"Scales{count + 1}",
                        nameUser       = $"Весы {count + 1}",
                        Model          = "IP65",
                        BaudRate       = "19200",
                        DataBits       = "8",
                        Parity         = "Нет",
                        RtsEnable      = "false",
                        StopBits       = "1",
                        Handshake      = "Аппаратное и Xon/Xoff",
                        MaxWeight      = "5",
                        MinWeightVideo = "500",
                        isSave         = "false",
                        RecognizeDelay = 0
                    };

                    context.Scales.Add(newScale);
                    context.SaveChanges();
                    DrawPage(scale: newScale);
                    tabScales.SelectTab(tabScales.TabPages.Count -1 );
                }
                else
                {
                    MessageBox.Show("Достигнуто максимальное число весов", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void btnDelScales_Click(object sender, EventArgs e)
        {
            if (tabScales.SelectedTab.Name == "Scales1")
            {
                MessageBox.Show("Невозможно удалить первые весы");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Весы будут удалены, продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    var currentScale = scaleTabPagesList.Find(x => x.Name == tabScales.SelectedTab.Name);
                    
                    var scale = (from i in context.Scales
                                 where i.name == tabScales.SelectedTab.Name
                                 select i).FirstOrDefault();
                    context.Scales.Remove(scale);
                    context.SaveChanges();
                    scaleTabPagesList.Remove(currentScale);
                }
                tabScales.TabPages.RemoveByKey(tabScales.SelectedTab.Name);
                tabScales.SelectedTab = tabScales.TabPages[tabScales.TabPages.Count - 1];
                btnAddScales.Enabled = true;
            }
            
        }

        private void tabScales_Enter(object sender, EventArgs e)
        {
            btnDelScales.Enabled = false;
            //Console.WriteLine(tabScales.SelectedTab.Name);
        }

        private void tabScales_Leave(object sender, EventArgs e)
        {
            btnDelScales.Enabled = true;
        }

        private void tMaxWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        private void lblVersion_MouseClick(object sender, MouseEventArgs e)
        {
            AboutLicense frmSetting = new AboutLicense();
            frmSetting.StartPosition = FormStartPosition.CenterParent;
            frmSetting.Show();
        }
    }
}
