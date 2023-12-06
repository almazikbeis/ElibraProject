using System;
using System.Windows.Forms;

namespace ELibra.Classes.UIElements
{
    class TabPageScales
    {
        #region Tab Page Components

        public ScalePageComponents page = new ScalePageComponents();

        #endregion

        public void tabPageInitialize(DBModel.Models.Scales scale, string cameras)
        {
            #region rBtnMetra
            page.rBtnMetra = new RadioButton();
            page.rBtnMetra.AutoSize = true;
            page.rBtnMetra.Location = new System.Drawing.Point(29, 6);
            page.rBtnMetra.Name = "rBtnMetra";
            page.rBtnMetra.Size = new System.Drawing.Size(108, 17);
            page.rBtnMetra.TabIndex = 36;
            page.rBtnMetra.Text = "МЕТРА (Россия)";
            page.rBtnMetra.UseVisualStyleBackColor = true;
            #endregion
            //#region rBtnTurc
            //page.rBtnTurc = new RadioButton();
            //page.rBtnTurc.AutoSize = true;
            //page.rBtnTurc.Location = new System.Drawing.Point(29, 29);
            //page.rBtnTurc.Name = "rBtnTurc";
            //page.rBtnTurc.Size = new System.Drawing.Size(124, 17);
            //page.rBtnTurc.TabIndex = 37;
            //page.rBtnTurc.Text = "Без имени (Турция)";
            //page.rBtnTurc.UseVisualStyleBackColor = true;
            //#endregion
            #region rCAS
            page.rBtnCAS = new RadioButton();
            page.rBtnCAS.AutoSize = true;
            page.rBtnCAS.Checked = false;
            page.rBtnCAS.Location = new System.Drawing.Point(29, 29);
            page.rBtnCAS.Name = "rBtnCAS";
            page.rBtnCAS.Size = new System.Drawing.Size(124, 17);
            page.rBtnCAS.TabIndex = 39;
            page.rBtnCAS.TabStop = true;
            page.rBtnCAS.Text = "CAS";
            page.rBtnCAS.UseVisualStyleBackColor = true;
            #endregion
            #region rBtnIP65
            page.rBtnIP65 = new RadioButton();
            page.rBtnIP65.AutoSize = true;
            page.rBtnIP65.Checked = true;
            page.rBtnIP65.Location = new System.Drawing.Point(29, 52);
            page.rBtnIP65.Name = "rBtnIP65";
            page.rBtnIP65.Size = new System.Drawing.Size(47, 17);
            page.rBtnIP65.TabIndex = 38;
            page.rBtnIP65.TabStop = true;
            page.rBtnIP65.Text = "IP65";
            page.rBtnIP65.UseVisualStyleBackColor = true;
            #endregion
            

            #region tMinWeightVideo
            page.tMinWeightVideo = new TextBox();
            page.tMinWeightVideo.Location = new System.Drawing.Point(148, 69);
            page.tMinWeightVideo.Name = "tMinWeightVideo";
            page.tMinWeightVideo.Size = new System.Drawing.Size(39, 20);
            page.tMinWeightVideo.TabIndex = 40;
            page.tMinWeightVideo.Text = scale.MinWeightVideo;
            page.tMinWeightVideo.KeyPress += new KeyPressEventHandler(NumOnly_KeyPress);
            #endregion
            #region lblMinWeightVideo
            page.lblMinWeightVideo = new Label();
            page.lblMinWeightVideo.AutoSize = true;
            page.lblMinWeightVideo.Location = new System.Drawing.Point(20, 69);
            page.lblMinWeightVideo.Name = "lblMinWeightVideo";
            page.lblMinWeightVideo.Size = new System.Drawing.Size(121, 13);
            page.lblMinWeightVideo.TabIndex = 39;
            page.lblMinWeightVideo.Text = "Мин. значение веса, кг:";
            #endregion

            #region cmbPortName
            page.cmbPortName = new ComboBox();
            page.cmbPortName.FormattingEnabled = true;
            page.cmbPortName.Location = new System.Drawing.Point(148, 95);
            page.cmbPortName.Name = "cmbPortName";
            page.cmbPortName.Size = new System.Drawing.Size(121, 21);
            page.cmbPortName.TabIndex = 19;
            #endregion
            #region lblNamePort
            page.lblNamePort = new Label();
            page.lblNamePort.AutoSize = true;
            page.lblNamePort.Location = new System.Drawing.Point(50, 95);
            page.lblNamePort.Name = "lblNamePort";
            page.lblNamePort.Size = new System.Drawing.Size(92, 13);
            page.lblNamePort.TabIndex = 13;
            page.lblNamePort.Text = "Название порта:";
            #endregion

            

            #region cmbBaudRate
            page.cmbBaudRate = new ComboBox();
            page.cmbBaudRate.FormattingEnabled = true;
            page.cmbBaudRate.Items.AddRange(new object[] {
                "110",
                "300",
                "1200",
                "2400",
                "4800",
                "9600",
                "19200",
                "38400",
                "57600",
                "115200",
                "230400",
                "460800",
                "921600"});
            page.cmbBaudRate.Location = new System.Drawing.Point(148, 122);
            page.cmbBaudRate.Name = "cmbBaudRate";
            page.cmbBaudRate.Size = new System.Drawing.Size(121, 21);
            page.cmbBaudRate.TabIndex = 20;
            page.cmbBaudRate.SelectedIndex = page.cmbBaudRate.FindStringExact(scale.BaudRate);
            #endregion
            #region lblSpeed
            page.lblSpeed = new Label();
            page.lblSpeed.AutoSize = true;
            page.lblSpeed.Location = new System.Drawing.Point(58, 122);
            page.lblSpeed.Name = "lblSpeed";
            page.lblSpeed.Size = new System.Drawing.Size(84, 13);
            page.lblSpeed.TabIndex = 14;
            page.lblSpeed.Text = "Скорость (б/с):";
            #endregion

            #region cmbDataBits
            page.cmbDataBits = new ComboBox();
            page.cmbDataBits.FormattingEnabled = true;
            page.cmbDataBits.Items.AddRange(new object[] {
                "5",
                "6",
                "7",
                "8"});
            page.cmbDataBits.Location = new System.Drawing.Point(148, 149);
            page.cmbDataBits.Name = "cmbDataBits";
            page.cmbDataBits.Size = new System.Drawing.Size(121, 21);
            page.cmbDataBits.TabIndex = 21;
            page.cmbDataBits.SelectedIndex = page.cmbDataBits.FindStringExact(scale.DataBits);
            #endregion
            #region lblBits
            page.lblBits = new Label();
            page.lblBits.AutoSize = true;
            page.lblBits.Location = new System.Drawing.Point(66, 149);
            page.lblBits.Name = "lblBits";
            page.lblBits.Size = new System.Drawing.Size(76, 13);
            page.lblBits.TabIndex = 15;
            page.lblBits.Text = "Биты данных:";
            #endregion

            #region cmbParity
            page.cmbParity = new ComboBox();
            page.cmbParity.FormattingEnabled = true;
            page.cmbParity.Items.AddRange(new object[] {
                "Нет",
                "Нечет",
                "Чет",
                "Маркер (1)",
                "Пробел (0)"});
            page.cmbParity.Location = new System.Drawing.Point(148, 176);
            page.cmbParity.Name = "cmbParity";
            page.cmbParity.Size = new System.Drawing.Size(121, 21);
            page.cmbParity.TabIndex = 22;
            page.cmbParity.SelectedIndex = page.cmbParity.FindStringExact(scale.Parity);
            #endregion
            #region lblParity
            page.lblParity = new Label();
            page.lblParity.AutoSize = true;
            page.lblParity.Location = new System.Drawing.Point(84, 176);
            page.lblParity.Name = "lblParity";
            page.lblParity.Size = new System.Drawing.Size(58, 13);
            page.lblParity.TabIndex = 16;
            page.lblParity.Text = "Четность:";
            #endregion

            #region cmbStopBits
            page.cmbStopBits = new ComboBox();
            page.cmbStopBits.FormattingEnabled = true;
            page.cmbStopBits.Items.AddRange(new object[] {
                "1",
                "1.5",
                "2"});
            page.cmbStopBits.Location = new System.Drawing.Point(148, 203);
            page.cmbStopBits.Name = "cmbStopBits";
            page.cmbStopBits.Size = new System.Drawing.Size(121, 21);
            page.cmbStopBits.TabIndex = 23;
            page.cmbStopBits.SelectedIndex = page.cmbStopBits.FindStringExact(scale.StopBits);
            #endregion
            #region lblStop
            page.lblStop = new Label();
            page.lblStop.AutoSize = true;
            page.lblStop.Location = new System.Drawing.Point(54, 203);
            page.lblStop.Name = "lblStop";
            page.lblStop.Size = new System.Drawing.Size(88, 13);
            page.lblStop.TabIndex = 17;
            page.lblStop.Text = "Стоповые биты:";
            #endregion

            #region cmbHandshake
            page.cmbHandshake = new ComboBox();
            page.cmbHandshake.FormattingEnabled = true;
            page.cmbHandshake.Items.AddRange(new object[] {
                "Нет",
                "Аппаратное",
                "Аппаратное и Xon/Xoff",
                "Xon/Xoff"});
            page.cmbHandshake.Location = new System.Drawing.Point(148, 230);
            page.cmbHandshake.Name = "cmbHandshake";
            page.cmbHandshake.Size = new System.Drawing.Size(121, 21);
            page.cmbHandshake.TabIndex = 24;
            page.cmbHandshake.SelectedIndex = page.cmbHandshake.FindStringExact(scale.Handshake);
            #endregion
            #region lblThread
            page.lblThread = new Label();
            page.lblThread.AutoSize = true;
            page.lblThread.Location = new System.Drawing.Point(24, 230);
            page.lblThread.Name = "lblThread";
            page.lblThread.Size = new System.Drawing.Size(118, 13);
            page.lblThread.TabIndex = 18;
            page.lblThread.Text = "Управление потоком:";
            #endregion

            #region cBoxRTS
            page.cBoxRTS = new CheckBox();
            page.cBoxRTS.AutoSize = true;
            page.cBoxRTS.Location = new System.Drawing.Point(27, 257);
            page.cBoxRTS.Name = "cBoxRTS";
            page.cBoxRTS.Size = new System.Drawing.Size(236, 17);
            page.cBoxRTS.TabIndex = 25;
            page.cBoxRTS.Text = "Использовать запрос на передачю (RTS)";
            page.cBoxRTS.UseVisualStyleBackColor = true;
            page.cBoxRTS.Checked = scale.RtsEnable == "true";
            #endregion

            #region listScaleCameras
            page.listScaleCameras = new ListBox();
            page.listScaleCameras.FormattingEnabled = true;
            page.listScaleCameras.Location = new System.Drawing.Point(148, 280);
            page.listScaleCameras.Name = "listScaleCameras";
            page.listScaleCameras.Size = new System.Drawing.Size(193, 43);
            page.listScaleCameras.TabIndex = 28;
            if (scale.LinkedCameras != null && scale.LinkedCameras != "")
            {
                page.listScaleCameras.Items.AddRange(scale.LinkedCameras.Split('|'));
            }
            page.listScaleCameras.DoubleClick += new EventHandler(listScaleCameras_MouseClick);

            #endregion
            #region cameraToolTip
            page.cameraToolTip = new ToolTip();
            page.cameraToolTip.SetToolTip(page.listScaleCameras, "Двойной клик, чтобы убрать камеру");
            #endregion
            #region cmbScaleCamerasURL
            page.cmbScaleCamerasURL = new ComboBox();
            page.cmbScaleCamerasURL.FormattingEnabled = true;
            page.cmbScaleCamerasURL.Location = new System.Drawing.Point(148, 331);
            page.cmbScaleCamerasURL.Name = "cmbScaleCamerasURL";
            page.cmbScaleCamerasURL.Size = new System.Drawing.Size(158, 21);
            page.cmbScaleCamerasURL.TabIndex = 29;
            page.cmbScaleCamerasURL.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cameras != null)
                page.cmbScaleCamerasURL.Items.AddRange(cameras.Split('|'));
            #endregion
            #region lblScaleCameras
            page.lblScaleCameras = new Label();
            page.lblScaleCameras.AutoSize = true;
            page.lblScaleCameras.Location = new System.Drawing.Point(11, 280);
            page.lblScaleCameras.Name = "lblScaleCameras";
            page.lblScaleCameras.Size = new System.Drawing.Size(131, 26);
            page.lblScaleCameras.TabIndex = 31;
            page.lblScaleCameras.Text = "Камеры, направленные \r\nна весы:";
            #endregion

            #region btnScaleCameraAdd
            page.btnScaleCameraAdd = new Button();
            page.btnScaleCameraAdd.Location = new System.Drawing.Point(312, 331);
            page.btnScaleCameraAdd.Name = "btnScaleCameraAdd";
            page.btnScaleCameraAdd.Size = new System.Drawing.Size(29, 21);
            page.btnScaleCameraAdd.TabIndex = 30;
            page.btnScaleCameraAdd.Text = "+";
            page.btnScaleCameraAdd.UseVisualStyleBackColor = true;
            page.btnScaleCameraAdd.Click += new EventHandler(this.btnScaleCameraAdd_Click);
            #endregion

            #region tScaleName
            page.tScaleName = new TextBox();
            page.tScaleName.Location = new System.Drawing.Point(148, 358);
            page.tScaleName.Name = "tScaleName";
            page.tScaleName.Size = new System.Drawing.Size(121, 20);
            page.tScaleName.TabIndex = 27;
            page.tScaleName.Text = scale.nameUser;
            #endregion
            #region lblScales
            page.lblScales = new Label();
            page.lblScales.AutoSize = true;
            page.lblScales.Location = new System.Drawing.Point(77, 358);
            page.lblScales.Name = "lblScales";
            page.lblScales.Size = new System.Drawing.Size(65, 13);
            page.lblScales.TabIndex = 26;
            page.lblScales.Text = "Имя весов:";
            #endregion

            #region numDelay
            page.numDelay = new NumericUpDown();
            page.numDelay.DecimalPlaces = 1;
            page.numDelay.Increment = new decimal(new int[] {
                1,
                0,
                0,
                65536});
            page.numDelay.Location = new System.Drawing.Point(148, 390);
            page.numDelay.Maximum = new decimal(new int[] {
                10,
                0,
                0,
                0});
            page.numDelay.Name = "numDelay";
            page.numDelay.Size = new System.Drawing.Size(193, 20);
            page.numDelay.TabIndex = 32;
            page.numDelay.Value = (decimal)(scale.RecognizeDelay ?? 0);
            #endregion
            #region lblScalesDelay
            page.lblScalesDelay = new Label();
            page.lblScalesDelay.AutoSize = true;
            page.lblScalesDelay.Location = new System.Drawing.Point(48, 384);
            page.lblScalesDelay.Name = "lblScalesDelay";
            page.lblScalesDelay.Size = new System.Drawing.Size(94, 26);
            page.lblScalesDelay.TabIndex = 33;
            page.lblScalesDelay.Text = "Задержка перед \r\nпервым кадром:";
            #endregion

            #region scalesPage
            page.scalesPage = new TabPage();
            //
            // Add components
            //
            page.scalesPage.AutoScroll = true;
            page.scalesPage.AutoScrollMinSize = new System.Drawing.Size(0, 382);
            page.scalesPage.Controls.Add(page.tMinWeightVideo);
            page.scalesPage.Controls.Add(page.lblMinWeightVideo);
            page.scalesPage.Controls.Add(page.rBtnIP65);
            //page.scalesPage.Controls.Add(page.rBtnTurc);
            page.scalesPage.Controls.Add(page.rBtnMetra);
            page.scalesPage.Controls.Add(page.rBtnCAS);
            page.scalesPage.Controls.Add(page.lblScalesDelay);
            page.scalesPage.Controls.Add(page.numDelay);
            page.scalesPage.Controls.Add(page.lblScaleCameras);
            page.scalesPage.Controls.Add(page.cmbScaleCamerasURL);
            page.scalesPage.Controls.Add(page.listScaleCameras);
            page.scalesPage.Controls.Add(page.btnScaleCameraAdd);
            page.scalesPage.Controls.Add(page.tScaleName);
            page.scalesPage.Controls.Add(page.lblScales);
            page.scalesPage.Controls.Add(page.cBoxRTS);
            page.scalesPage.Controls.Add(page.cmbHandshake);
            page.scalesPage.Controls.Add(page.cmbStopBits);
            page.scalesPage.Controls.Add(page.cmbParity);
            page.scalesPage.Controls.Add(page.cmbDataBits);
            page.scalesPage.Controls.Add(page.cmbBaudRate);
            page.scalesPage.Controls.Add(page.cmbPortName);
            page.scalesPage.Controls.Add(page.lblThread);
            page.scalesPage.Controls.Add(page.lblStop);
            page.scalesPage.Controls.Add(page.lblParity);
            page.scalesPage.Controls.Add(page.lblBits);
            page.scalesPage.Controls.Add(page.lblSpeed);
            page.scalesPage.Controls.Add(page.lblNamePort);
            //
            // Define properties
            //
            page.scalesPage.Location = new System.Drawing.Point(4, 22);
            page.scalesPage.Name = scale.name;
            page.scalesPage.Padding = new System.Windows.Forms.Padding(3);
            page.scalesPage.Size = new System.Drawing.Size(364, 305);
            page.scalesPage.TabIndex = 0;
            page.scalesPage.Text = scale.nameUser;
            page.scalesPage.UseVisualStyleBackColor = true;

            #endregion  
        }

        private void listScaleCameras_MouseClick(object sender, EventArgs e)
        {
            if ((sender as ListBox).Items.Count > 0)
            {
                (sender as ListBox).Items.RemoveAt((sender as ListBox).SelectedIndex);
            }
        }
        private void NumOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        public void Dispose()
        {
            page.rBtnMetra.Dispose();
            //page.rBtnTurc.Dispose();
            page.rBtnIP65.Dispose();
            page.rBtnCAS.Dispose();
            page.tMinWeightVideo.Dispose();
            page.cmbPortName.Dispose();
            page.cmbBaudRate.Dispose();
            page.cmbDataBits.Dispose();
            page.cmbParity.Dispose(); 
            page.cmbStopBits.Dispose();
            page.cmbHandshake.Dispose();
            page.cBoxRTS.Dispose();
            page.listScaleCameras.Dispose(); 
            page.cmbScaleCamerasURL.Dispose();
            page.btnScaleCameraAdd.Dispose();
            page.tScaleName.Dispose();
            page.numDelay.Dispose();

            page.lblMinWeightVideo.Dispose();
            page.lblScalesDelay.Dispose();
            page.lblScaleCameras.Dispose();
            page.lblScales.Dispose();
            page.lblThread.Dispose();
            page.lblStop.Dispose();
            page.lblParity.Dispose();
            page.lblBits.Dispose(); 
            page.lblSpeed.Dispose();
            page.lblNamePort.Dispose();
            page.scalesPage.Dispose();
        }


        private void btnScaleCameraAdd_Click(object sender, EventArgs e)
        {
            if (page.cmbScaleCamerasURL.SelectedItem != null && !page.listScaleCameras.Items.Contains(page.cmbScaleCamerasURL.SelectedItem))
            {
                page.listScaleCameras.Items.Add(page.cmbScaleCamerasURL.SelectedItem);
            }
        }
    }
}
