namespace ELibra
{
    partial class Setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tpC = new System.Windows.Forms.TabPage();
            this.tExportFold = new System.Windows.Forms.TextBox();
            this.btnPathExport = new System.Windows.Forms.Button();
            this.cBoxExportFold = new System.Windows.Forms.CheckBox();
            this.tpVideo = new System.Windows.Forms.TabPage();
            this.btnAddURLCamNum = new System.Windows.Forms.Button();
            this.btnDelURLCamNum = new System.Windows.Forms.Button();
            this.btnAddURLCam = new System.Windows.Forms.Button();
            this.btnDelURLCam = new System.Windows.Forms.Button();
            this.btnPathVideo = new System.Windows.Forms.Button();
            this.tAddURLCamNum = new System.Windows.Forms.TextBox();
            this.tAddURLCam = new System.Windows.Forms.TextBox();
            this.tPathVideo = new System.Windows.Forms.TextBox();
            this.tMaxWeight = new System.Windows.Forms.TextBox();
            this.listCamNum = new System.Windows.Forms.ListBox();
            this.listCam = new System.Windows.Forms.ListBox();
            this.lblURLCamNum = new System.Windows.Forms.Label();
            this.lblURLCam = new System.Windows.Forms.Label();
            this.lblPathVideo = new System.Windows.Forms.Label();
            this.lblMaxWeight = new System.Windows.Forms.Label();
            this.tpFields = new System.Windows.Forms.TabPage();
            this.cBoxSnipper = new System.Windows.Forms.CheckBox();
            this.cBoxPositionStockNumber = new System.Windows.Forms.CheckBox();
            this.cBoxOrderNumber = new System.Windows.Forms.CheckBox();
            this.cBoxStock = new System.Windows.Forms.CheckBox();
            this.cBoxCarrier = new System.Windows.Forms.CheckBox();
            this.cBoxTypeTransaction = new System.Windows.Forms.CheckBox();
            this.cBoxPlaceDelivery = new System.Windows.Forms.CheckBox();
            this.cBoxDispatchLocation = new System.Windows.Forms.CheckBox();
            this.cBoxConsignee = new System.Windows.Forms.CheckBox();
            this.tpOwner = new System.Windows.Forms.TabPage();
            this.tNameCompany = new System.Windows.Forms.TextBox();
            this.lblNameOrg = new System.Windows.Forms.Label();
            this.tpData = new System.Windows.Forms.TabPage();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tUpload = new System.Windows.Forms.TextBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.tUploadDelay = new System.Windows.Forms.ComboBox();
            this.lblUploadDelay = new System.Windows.Forms.Label();
            this.tPass = new System.Windows.Forms.TextBox();
            this.tLogin = new System.Windows.Forms.TextBox();
            this.tHost = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.tpCom = new System.Windows.Forms.TabPage();
            this.btnAddScales = new System.Windows.Forms.Button();
            this.tabScales = new System.Windows.Forms.TabControl();
            this.btnDelScales = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.sqLiteCommandBuilder1 = new System.Data.SQLite.SQLiteCommandBuilder();
            this.tpC.SuspendLayout();
            this.tpVideo.SuspendLayout();
            this.tpFields.SuspendLayout();
            this.tpOwner.SuspendLayout();
            this.tpData.SuspendLayout();
            this.tpCom.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(427, 375);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(346, 375);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // tpC
            // 
            this.tpC.Controls.Add(this.tExportFold);
            this.tpC.Controls.Add(this.btnPathExport);
            this.tpC.Controls.Add(this.cBoxExportFold);
            this.tpC.Location = new System.Drawing.Point(154, 4);
            this.tpC.Name = "tpC";
            this.tpC.Size = new System.Drawing.Size(368, 358);
            this.tpC.TabIndex = 7;
            this.tpC.Text = "Настройки 1С";
            this.tpC.UseVisualStyleBackColor = true;
            // 
            // tExportFold
            // 
            this.tExportFold.Enabled = false;
            this.tExportFold.Location = new System.Drawing.Point(18, 41);
            this.tExportFold.Name = "tExportFold";
            this.tExportFold.Size = new System.Drawing.Size(280, 20);
            this.tExportFold.TabIndex = 3;
            // 
            // btnPathExport
            // 
            this.btnPathExport.Location = new System.Drawing.Point(304, 39);
            this.btnPathExport.Name = "btnPathExport";
            this.btnPathExport.Size = new System.Drawing.Size(30, 23);
            this.btnPathExport.TabIndex = 2;
            this.btnPathExport.Text = "...";
            this.btnPathExport.UseVisualStyleBackColor = true;
            this.btnPathExport.Click += new System.EventHandler(this.BtnPathExport_Click);
            // 
            // cBoxExportFold
            // 
            this.cBoxExportFold.AutoSize = true;
            this.cBoxExportFold.Location = new System.Drawing.Point(18, 18);
            this.cBoxExportFold.Name = "cBoxExportFold";
            this.cBoxExportFold.Size = new System.Drawing.Size(165, 17);
            this.cBoxExportFold.TabIndex = 0;
            this.cBoxExportFold.Text = "Выгрузить данные в папку:";
            this.cBoxExportFold.UseVisualStyleBackColor = true;
            this.cBoxExportFold.CheckedChanged += new System.EventHandler(this.CBoxExportFold_CheckedChanged);
            // 
            // tpVideo
            // 
            this.tpVideo.Controls.Add(this.btnAddURLCamNum);
            this.tpVideo.Controls.Add(this.btnDelURLCamNum);
            this.tpVideo.Controls.Add(this.btnAddURLCam);
            this.tpVideo.Controls.Add(this.btnDelURLCam);
            this.tpVideo.Controls.Add(this.btnPathVideo);
            this.tpVideo.Controls.Add(this.tAddURLCamNum);
            this.tpVideo.Controls.Add(this.tAddURLCam);
            this.tpVideo.Controls.Add(this.tPathVideo);
            this.tpVideo.Controls.Add(this.tMaxWeight);
            this.tpVideo.Controls.Add(this.listCamNum);
            this.tpVideo.Controls.Add(this.listCam);
            this.tpVideo.Controls.Add(this.lblURLCamNum);
            this.tpVideo.Controls.Add(this.lblURLCam);
            this.tpVideo.Controls.Add(this.lblPathVideo);
            this.tpVideo.Controls.Add(this.lblMaxWeight);
            this.tpVideo.Location = new System.Drawing.Point(154, 4);
            this.tpVideo.Name = "tpVideo";
            this.tpVideo.Size = new System.Drawing.Size(368, 358);
            this.tpVideo.TabIndex = 6;
            this.tpVideo.Text = "Регистрация видео";
            this.tpVideo.UseVisualStyleBackColor = true;
            // 
            // btnAddURLCamNum
            // 
            this.btnAddURLCamNum.Location = new System.Drawing.Point(336, 233);
            this.btnAddURLCamNum.Name = "btnAddURLCamNum";
            this.btnAddURLCamNum.Size = new System.Drawing.Size(29, 23);
            this.btnAddURLCamNum.TabIndex = 16;
            this.btnAddURLCamNum.Text = "+";
            this.btnAddURLCamNum.UseVisualStyleBackColor = true;
            this.btnAddURLCamNum.Click += new System.EventHandler(this.BtnAddURLCamNum_Click);
            // 
            // btnDelURLCamNum
            // 
            this.btnDelURLCamNum.Location = new System.Drawing.Point(336, 161);
            this.btnDelURLCamNum.Name = "btnDelURLCamNum";
            this.btnDelURLCamNum.Size = new System.Drawing.Size(29, 23);
            this.btnDelURLCamNum.TabIndex = 15;
            this.btnDelURLCamNum.Text = "-";
            this.btnDelURLCamNum.UseVisualStyleBackColor = true;
            this.btnDelURLCamNum.Click += new System.EventHandler(this.BtnDelURLCamNum_Click);
            // 
            // btnAddURLCam
            // 
            this.btnAddURLCam.Location = new System.Drawing.Point(336, 132);
            this.btnAddURLCam.Name = "btnAddURLCam";
            this.btnAddURLCam.Size = new System.Drawing.Size(29, 23);
            this.btnAddURLCam.TabIndex = 14;
            this.btnAddURLCam.Text = "+";
            this.btnAddURLCam.UseVisualStyleBackColor = true;
            this.btnAddURLCam.Click += new System.EventHandler(this.BtnAddURLCam_Click);
            // 
            // btnDelURLCam
            // 
            this.btnDelURLCam.Location = new System.Drawing.Point(336, 58);
            this.btnDelURLCam.Name = "btnDelURLCam";
            this.btnDelURLCam.Size = new System.Drawing.Size(29, 23);
            this.btnDelURLCam.TabIndex = 13;
            this.btnDelURLCam.Text = "-";
            this.btnDelURLCam.UseVisualStyleBackColor = true;
            this.btnDelURLCam.Click += new System.EventHandler(this.BtnDelURLCam_Click);
            // 
            // btnPathVideo
            // 
            this.btnPathVideo.Location = new System.Drawing.Point(336, 33);
            this.btnPathVideo.Name = "btnPathVideo";
            this.btnPathVideo.Size = new System.Drawing.Size(29, 23);
            this.btnPathVideo.TabIndex = 12;
            this.btnPathVideo.Text = "...";
            this.btnPathVideo.UseVisualStyleBackColor = true;
            this.btnPathVideo.Click += new System.EventHandler(this.BtnPathVideo_Click);
            // 
            // tAddURLCamNum
            // 
            this.tAddURLCamNum.Location = new System.Drawing.Point(136, 235);
            this.tAddURLCamNum.Name = "tAddURLCamNum";
            this.tAddURLCamNum.Size = new System.Drawing.Size(193, 20);
            this.tAddURLCamNum.TabIndex = 11;
            // 
            // tAddURLCam
            // 
            this.tAddURLCam.Location = new System.Drawing.Point(136, 134);
            this.tAddURLCam.Name = "tAddURLCam";
            this.tAddURLCam.Size = new System.Drawing.Size(193, 20);
            this.tAddURLCam.TabIndex = 8;
            // 
            // tPathVideo
            // 
            this.tPathVideo.Enabled = false;
            this.tPathVideo.Location = new System.Drawing.Point(136, 35);
            this.tPathVideo.Name = "tPathVideo";
            this.tPathVideo.Size = new System.Drawing.Size(193, 20);
            this.tPathVideo.TabIndex = 7;
            // 
            // tMaxWeight
            // 
            this.tMaxWeight.Location = new System.Drawing.Point(136, 12);
            this.tMaxWeight.Name = "tMaxWeight";
            this.tMaxWeight.Size = new System.Drawing.Size(193, 20);
            this.tMaxWeight.TabIndex = 5;
            this.tMaxWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tMaxWeight_KeyPress);
            // 
            // listCamNum
            // 
            this.listCamNum.FormattingEnabled = true;
            this.listCamNum.Location = new System.Drawing.Point(136, 160);
            this.listCamNum.Name = "listCamNum";
            this.listCamNum.Size = new System.Drawing.Size(193, 69);
            this.listCamNum.TabIndex = 10;
            // 
            // listCam
            // 
            this.listCam.FormattingEnabled = true;
            this.listCam.Location = new System.Drawing.Point(136, 59);
            this.listCam.Name = "listCam";
            this.listCam.Size = new System.Drawing.Size(193, 69);
            this.listCam.TabIndex = 9;
            // 
            // lblURLCamNum
            // 
            this.lblURLCamNum.Location = new System.Drawing.Point(2, 160);
            this.lblURLCamNum.Name = "lblURLCamNum";
            this.lblURLCamNum.Size = new System.Drawing.Size(128, 54);
            this.lblURLCamNum.TabIndex = 4;
            this.lblURLCamNum.Text = "URL видеокамер для распознование государственных номеров:";
            // 
            // lblURLCam
            // 
            this.lblURLCam.AutoSize = true;
            this.lblURLCam.Location = new System.Drawing.Point(33, 63);
            this.lblURLCam.Name = "lblURLCam";
            this.lblURLCam.Size = new System.Drawing.Size(97, 13);
            this.lblURLCam.TabIndex = 3;
            this.lblURLCam.Text = "URL видеокамер:";
            // 
            // lblPathVideo
            // 
            this.lblPathVideo.AutoSize = true;
            this.lblPathVideo.Location = new System.Drawing.Point(20, 38);
            this.lblPathVideo.Name = "lblPathVideo";
            this.lblPathVideo.Size = new System.Drawing.Size(110, 13);
            this.lblPathVideo.TabIndex = 2;
            this.lblPathVideo.Text = "Путь к видеоархиву:";
            // 
            // lblMaxWeight
            // 
            this.lblMaxWeight.AutoSize = true;
            this.lblMaxWeight.Location = new System.Drawing.Point(9, 15);
            this.lblMaxWeight.Name = "lblMaxWeight";
            this.lblMaxWeight.Size = new System.Drawing.Size(121, 13);
            this.lblMaxWeight.TabIndex = 0;
            this.lblMaxWeight.Text = "Максимальный вес, т:";
            // 
            // tpFields
            // 
            this.tpFields.Controls.Add(this.cBoxSnipper);
            this.tpFields.Controls.Add(this.cBoxPositionStockNumber);
            this.tpFields.Controls.Add(this.cBoxOrderNumber);
            this.tpFields.Controls.Add(this.cBoxStock);
            this.tpFields.Controls.Add(this.cBoxCarrier);
            this.tpFields.Controls.Add(this.cBoxTypeTransaction);
            this.tpFields.Controls.Add(this.cBoxPlaceDelivery);
            this.tpFields.Controls.Add(this.cBoxDispatchLocation);
            this.tpFields.Controls.Add(this.cBoxConsignee);
            this.tpFields.Location = new System.Drawing.Point(154, 4);
            this.tpFields.Name = "tpFields";
            this.tpFields.Size = new System.Drawing.Size(368, 358);
            this.tpFields.TabIndex = 5;
            this.tpFields.Text = "Используемые поля данных";
            this.tpFields.UseVisualStyleBackColor = true;
            // 
            // cBoxSnipper
            // 
            this.cBoxSnipper.AutoSize = true;
            this.cBoxSnipper.Location = new System.Drawing.Point(13, 202);
            this.cBoxSnipper.Name = "cBoxSnipper";
            this.cBoxSnipper.Size = new System.Drawing.Size(119, 17);
            this.cBoxSnipper.TabIndex = 8;
            this.cBoxSnipper.Text = "Грузоотпровитель";
            this.cBoxSnipper.UseVisualStyleBackColor = true;
            // 
            // cBoxPositionStockNumber
            // 
            this.cBoxPositionStockNumber.AutoSize = true;
            this.cBoxPositionStockNumber.Location = new System.Drawing.Point(13, 179);
            this.cBoxPositionStockNumber.Name = "cBoxPositionStockNumber";
            this.cBoxPositionStockNumber.Size = new System.Drawing.Size(144, 17);
            this.cBoxPositionStockNumber.TabIndex = 7;
            this.cBoxPositionStockNumber.Text = "Номер позиции заказа";
            this.cBoxPositionStockNumber.UseVisualStyleBackColor = true;
            // 
            // cBoxOrderNumber
            // 
            this.cBoxOrderNumber.AutoSize = true;
            this.cBoxOrderNumber.Location = new System.Drawing.Point(13, 156);
            this.cBoxOrderNumber.Name = "cBoxOrderNumber";
            this.cBoxOrderNumber.Size = new System.Drawing.Size(99, 17);
            this.cBoxOrderNumber.TabIndex = 6;
            this.cBoxOrderNumber.Text = "Номер заказа";
            this.cBoxOrderNumber.UseVisualStyleBackColor = true;
            // 
            // cBoxStock
            // 
            this.cBoxStock.AutoSize = true;
            this.cBoxStock.Location = new System.Drawing.Point(13, 133);
            this.cBoxStock.Name = "cBoxStock";
            this.cBoxStock.Size = new System.Drawing.Size(57, 17);
            this.cBoxStock.TabIndex = 5;
            this.cBoxStock.Text = "Склад";
            this.cBoxStock.UseVisualStyleBackColor = true;
            // 
            // cBoxCarrier
            // 
            this.cBoxCarrier.AutoSize = true;
            this.cBoxCarrier.Location = new System.Drawing.Point(13, 110);
            this.cBoxCarrier.Name = "cBoxCarrier";
            this.cBoxCarrier.Size = new System.Drawing.Size(87, 17);
            this.cBoxCarrier.TabIndex = 4;
            this.cBoxCarrier.Text = "Перевозчик";
            this.cBoxCarrier.UseVisualStyleBackColor = true;
            // 
            // cBoxTypeTransaction
            // 
            this.cBoxTypeTransaction.AutoSize = true;
            this.cBoxTypeTransaction.Location = new System.Drawing.Point(13, 87);
            this.cBoxTypeTransaction.Name = "cBoxTypeTransaction";
            this.cBoxTypeTransaction.Size = new System.Drawing.Size(96, 17);
            this.cBoxTypeTransaction.TabIndex = 3;
            this.cBoxTypeTransaction.Text = "Тип операции";
            this.cBoxTypeTransaction.UseVisualStyleBackColor = true;
            // 
            // cBoxPlaceDelivery
            // 
            this.cBoxPlaceDelivery.AutoSize = true;
            this.cBoxPlaceDelivery.Location = new System.Drawing.Point(13, 64);
            this.cBoxPlaceDelivery.Name = "cBoxPlaceDelivery";
            this.cBoxPlaceDelivery.Size = new System.Drawing.Size(108, 17);
            this.cBoxPlaceDelivery.TabIndex = 2;
            this.cBoxPlaceDelivery.Text = "Место доставки";
            this.cBoxPlaceDelivery.UseVisualStyleBackColor = true;
            // 
            // cBoxDispatchLocation
            // 
            this.cBoxDispatchLocation.AutoSize = true;
            this.cBoxDispatchLocation.Location = new System.Drawing.Point(13, 41);
            this.cBoxDispatchLocation.Name = "cBoxDispatchLocation";
            this.cBoxDispatchLocation.Size = new System.Drawing.Size(108, 17);
            this.cBoxDispatchLocation.TabIndex = 1;
            this.cBoxDispatchLocation.Text = "Место отправки";
            this.cBoxDispatchLocation.UseVisualStyleBackColor = true;
            // 
            // cBoxConsignee
            // 
            this.cBoxConsignee.AutoSize = true;
            this.cBoxConsignee.Location = new System.Drawing.Point(13, 18);
            this.cBoxConsignee.Name = "cBoxConsignee";
            this.cBoxConsignee.Size = new System.Drawing.Size(112, 17);
            this.cBoxConsignee.TabIndex = 0;
            this.cBoxConsignee.Text = "Грузополучатель";
            this.cBoxConsignee.UseVisualStyleBackColor = true;
            // 
            // tpOwner
            // 
            this.tpOwner.Controls.Add(this.tNameCompany);
            this.tpOwner.Controls.Add(this.lblNameOrg);
            this.tpOwner.Location = new System.Drawing.Point(154, 4);
            this.tpOwner.Name = "tpOwner";
            this.tpOwner.Size = new System.Drawing.Size(368, 358);
            this.tpOwner.TabIndex = 4;
            this.tpOwner.Text = "Сведения о владельце";
            this.tpOwner.UseVisualStyleBackColor = true;
            // 
            // tNameCompany
            // 
            this.tNameCompany.Location = new System.Drawing.Point(154, 22);
            this.tNameCompany.Name = "tNameCompany";
            this.tNameCompany.Size = new System.Drawing.Size(179, 20);
            this.tNameCompany.TabIndex = 3;
            // 
            // lblNameOrg
            // 
            this.lblNameOrg.AutoSize = true;
            this.lblNameOrg.Location = new System.Drawing.Point(20, 25);
            this.lblNameOrg.Name = "lblNameOrg";
            this.lblNameOrg.Size = new System.Drawing.Size(128, 13);
            this.lblNameOrg.TabIndex = 0;
            this.lblNameOrg.Text = "Название организации:";
            // 
            // tpData
            // 
            this.tpData.Controls.Add(this.lblVersion);
            this.tpData.Controls.Add(this.tUpload);
            this.tpData.Controls.Add(this.lblHost);
            this.tpData.Controls.Add(this.tUploadDelay);
            this.tpData.Controls.Add(this.lblUploadDelay);
            this.tpData.Controls.Add(this.tPass);
            this.tpData.Controls.Add(this.tLogin);
            this.tpData.Controls.Add(this.tHost);
            this.tpData.Controls.Add(this.lblPass);
            this.tpData.Controls.Add(this.lblLogin);
            this.tpData.Controls.Add(this.lblUpload);
            this.tpData.Location = new System.Drawing.Point(154, 4);
            this.tpData.Name = "tpData";
            this.tpData.Size = new System.Drawing.Size(368, 358);
            this.tpData.TabIndex = 2;
            this.tpData.Text = "Источник данных";
            this.tpData.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblVersion.Location = new System.Drawing.Point(12, 157);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(47, 13);
            this.lblVersion.TabIndex = 12;
            this.lblVersion.Text = "Версия:";
            this.lblVersion.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblVersion_MouseClick);
            // 
            // tUpload
            // 
            this.tUpload.Location = new System.Drawing.Point(117, 47);
            this.tUpload.Name = "tUpload";
            this.tUpload.Size = new System.Drawing.Size(231, 20);
            this.tUpload.TabIndex = 11;
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(63, 24);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(47, 13);
            this.lblHost.TabIndex = 10;
            this.lblHost.Text = "Сервер:";
            // 
            // tUploadDelay
            // 
            this.tUploadDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tUploadDelay.FormattingEnabled = true;
            this.tUploadDelay.Items.AddRange(new object[] {
            "Только вручную",
            "После каждого взвешивания"});
            this.tUploadDelay.Location = new System.Drawing.Point(116, 125);
            this.tUploadDelay.Name = "tUploadDelay";
            this.tUploadDelay.Size = new System.Drawing.Size(231, 21);
            this.tUploadDelay.TabIndex = 9;
            // 
            // lblUploadDelay
            // 
            this.lblUploadDelay.AutoSize = true;
            this.lblUploadDelay.Location = new System.Drawing.Point(11, 128);
            this.lblUploadDelay.Name = "lblUploadDelay";
            this.lblUploadDelay.Size = new System.Drawing.Size(99, 13);
            this.lblUploadDelay.TabIndex = 8;
            this.lblUploadDelay.Text = "Период выгрузки:";
            // 
            // tPass
            // 
            this.tPass.Location = new System.Drawing.Point(116, 99);
            this.tPass.Name = "tPass";
            this.tPass.PasswordChar = '*';
            this.tPass.Size = new System.Drawing.Size(231, 20);
            this.tPass.TabIndex = 7;
            // 
            // tLogin
            // 
            this.tLogin.Location = new System.Drawing.Point(116, 73);
            this.tLogin.Name = "tLogin";
            this.tLogin.Size = new System.Drawing.Size(231, 20);
            this.tLogin.TabIndex = 6;
            // 
            // tHost
            // 
            this.tHost.Location = new System.Drawing.Point(117, 21);
            this.tHost.Name = "tHost";
            this.tHost.Size = new System.Drawing.Size(231, 20);
            this.tHost.TabIndex = 4;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(62, 102);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(48, 13);
            this.lblPass.TabIndex = 3;
            this.lblPass.Text = "Пароль:";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(69, 76);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(41, 13);
            this.lblLogin.TabIndex = 2;
            this.lblLogin.Text = "Логин:";
            // 
            // lblUpload
            // 
            this.lblUpload.AutoSize = true;
            this.lblUpload.Location = new System.Drawing.Point(12, 50);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(98, 13);
            this.lblUpload.TabIndex = 0;
            this.lblUpload.Text = "Сервер выгрузки:";
            // 
            // tpCom
            // 
            this.tpCom.Controls.Add(this.btnAddScales);
            this.tpCom.Controls.Add(this.tabScales);
            this.tpCom.Controls.Add(this.btnDelScales);
            this.tpCom.Location = new System.Drawing.Point(154, 4);
            this.tpCom.Name = "tpCom";
            this.tpCom.Padding = new System.Windows.Forms.Padding(3);
            this.tpCom.Size = new System.Drawing.Size(368, 358);
            this.tpCom.TabIndex = 1;
            this.tpCom.Text = "Настройки COM-порта";
            this.tpCom.UseVisualStyleBackColor = true;
            // 
            // btnAddScales
            // 
            this.btnAddScales.Location = new System.Drawing.Point(6, 329);
            this.btnAddScales.Name = "btnAddScales";
            this.btnAddScales.Size = new System.Drawing.Size(106, 23);
            this.btnAddScales.TabIndex = 34;
            this.btnAddScales.Text = "Добавить весы";
            this.btnAddScales.UseVisualStyleBackColor = true;
            this.btnAddScales.Click += new System.EventHandler(this.btnAddScales_Click);
            // 
            // tabScales
            // 
            this.tabScales.Location = new System.Drawing.Point(0, -4);
            this.tabScales.Name = "tabScales";
            this.tabScales.SelectedIndex = 0;
            this.tabScales.Size = new System.Drawing.Size(372, 331);
            this.tabScales.TabIndex = 13;
            // 
            // btnDelScales
            // 
            this.btnDelScales.Enabled = false;
            this.btnDelScales.Location = new System.Drawing.Point(118, 329);
            this.btnDelScales.Name = "btnDelScales";
            this.btnDelScales.Size = new System.Drawing.Size(106, 23);
            this.btnDelScales.TabIndex = 35;
            this.btnDelScales.Text = "Удалить весы";
            this.btnDelScales.UseVisualStyleBackColor = true;
            this.btnDelScales.Click += new System.EventHandler(this.btnDelScales_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tpCom);
            this.tabControl1.Controls.Add(this.tpData);
            this.tabControl1.Controls.Add(this.tpOwner);
            this.tabControl1.Controls.Add(this.tpFields);
            this.tabControl1.Controls.Add(this.tpVideo);
            this.tabControl1.Controls.Add(this.tpC);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(40, 150);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(526, 366);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControl1_DrawItem);
            // 
            // sqLiteCommandBuilder1
            // 
            this.sqLiteCommandBuilder1.DataAdapter = null;
            this.sqLiteCommandBuilder1.QuoteSuffix = "]";
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 410);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Setting";
            this.Text = "Настройки программы";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.tpC.ResumeLayout(false);
            this.tpC.PerformLayout();
            this.tpVideo.ResumeLayout(false);
            this.tpVideo.PerformLayout();
            this.tpFields.ResumeLayout(false);
            this.tpFields.PerformLayout();
            this.tpOwner.ResumeLayout(false);
            this.tpOwner.PerformLayout();
            this.tpData.ResumeLayout(false);
            this.tpData.PerformLayout();
            this.tpCom.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TabPage tpC;
        private System.Windows.Forms.TextBox tExportFold;
        private System.Windows.Forms.Button btnPathExport;
        private System.Windows.Forms.CheckBox cBoxExportFold;
        private System.Windows.Forms.TabPage tpVideo;
        private System.Windows.Forms.Button btnAddURLCamNum;
        private System.Windows.Forms.Button btnDelURLCamNum;
        private System.Windows.Forms.Button btnAddURLCam;
        private System.Windows.Forms.Button btnDelURLCam;
        private System.Windows.Forms.Button btnPathVideo;
        private System.Windows.Forms.TextBox tAddURLCamNum;
        private System.Windows.Forms.TextBox tAddURLCam;
        private System.Windows.Forms.TextBox tPathVideo;
        private System.Windows.Forms.TextBox tMaxWeight;
        private System.Windows.Forms.ListBox listCamNum;
        private System.Windows.Forms.ListBox listCam;
        private System.Windows.Forms.Label lblURLCamNum;
        private System.Windows.Forms.Label lblURLCam;
        private System.Windows.Forms.Label lblPathVideo;
        private System.Windows.Forms.Label lblMaxWeight;
        private System.Windows.Forms.TabPage tpFields;
        private System.Windows.Forms.CheckBox cBoxSnipper;
        private System.Windows.Forms.CheckBox cBoxPositionStockNumber;
        private System.Windows.Forms.CheckBox cBoxOrderNumber;
        private System.Windows.Forms.CheckBox cBoxStock;
        private System.Windows.Forms.CheckBox cBoxCarrier;
        private System.Windows.Forms.CheckBox cBoxTypeTransaction;
        private System.Windows.Forms.CheckBox cBoxPlaceDelivery;
        private System.Windows.Forms.CheckBox cBoxDispatchLocation;
        private System.Windows.Forms.CheckBox cBoxConsignee;
        private System.Windows.Forms.TabPage tpOwner;
        private System.Windows.Forms.TextBox tNameCompany;
        private System.Windows.Forms.Label lblNameOrg;
        private System.Windows.Forms.TabPage tpData;
        private System.Windows.Forms.TextBox tPass;
        private System.Windows.Forms.TextBox tLogin;
        private System.Windows.Forms.TextBox tHost;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblUpload;
        private System.Windows.Forms.TabPage tpCom;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox tUploadDelay;
        private System.Windows.Forms.Label lblUploadDelay;
        private System.Windows.Forms.TextBox tUpload;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.Button btnAddScales;
        private System.Windows.Forms.Button btnDelScales;
        private System.Data.SQLite.SQLiteCommandBuilder sqLiteCommandBuilder1;
        private System.Windows.Forms.TabControl tabScales;
        private System.Windows.Forms.Label lblVersion;
    }
}