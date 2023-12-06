namespace ELibra
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.операцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WeighingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JournalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.VideosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DayReportToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.MouthReportToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.DetalReportToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.SumReportToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CounterPartiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CargosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DriversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CarriersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConsigneesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConsignorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartScalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upload1CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelScales = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblValueOfLibra = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.panelScales.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.операцииToolStripMenuItem,
            this.отчетыToolStripMenuItem,
            this.справочникиToolStripMenuItem,
            this.SettingToolStripMenu,
            this.cToolStripMenuItem,
            this.UploadToolStripMenuItem,
            this.refToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1040, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip";
            // 
            // операцииToolStripMenuItem
            // 
            this.операцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WeighingToolStripMenuItem,
            this.JournalToolStripMenuItem,
            this.toolStripSeparator1,
            this.VideosToolStripMenuItem,
            this.toolStripSeparator2,
            this.выходToolStripMenuItem});
            this.операцииToolStripMenuItem.Name = "операцииToolStripMenuItem";
            this.операцииToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.операцииToolStripMenuItem.Text = "Операции";
            // 
            // WeighingToolStripMenuItem
            // 
            this.WeighingToolStripMenuItem.Name = "WeighingToolStripMenuItem";
            this.WeighingToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.WeighingToolStripMenuItem.Text = "Взвешивание";
            this.WeighingToolStripMenuItem.Click += new System.EventHandler(this.WeighingToolStripMenuItem_Click);
            // 
            // JournalToolStripMenuItem
            // 
            this.JournalToolStripMenuItem.Name = "JournalToolStripMenuItem";
            this.JournalToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.JournalToolStripMenuItem.Text = "Журнал операций";
            this.JournalToolStripMenuItem.Click += new System.EventHandler(this.JournalToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(261, 6);
            // 
            // VideosToolStripMenuItem
            // 
            this.VideosToolStripMenuItem.Name = "VideosToolStripMenuItem";
            this.VideosToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.VideosToolStripMenuItem.Text = "Отображать изображение с камер";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(261, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DayReportToolStrip,
            this.MouthReportToolStrip,
            this.DetalReportToolStrip,
            this.SumReportToolStrip});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // DayReportToolStrip
            // 
            this.DayReportToolStrip.Name = "DayReportToolStrip";
            this.DayReportToolStrip.Size = new System.Drawing.Size(263, 22);
            this.DayReportToolStrip.Text = "Ежедневный отчет";
            this.DayReportToolStrip.Click += new System.EventHandler(this.DayReportToolStrip_Click);
            // 
            // MouthReportToolStrip
            // 
            this.MouthReportToolStrip.Name = "MouthReportToolStrip";
            this.MouthReportToolStrip.Size = new System.Drawing.Size(263, 22);
            this.MouthReportToolStrip.Text = "Ежемесячный отчет";
            this.MouthReportToolStrip.Click += new System.EventHandler(this.MouthReportToolStrip_Click);
            // 
            // DetalReportToolStrip
            // 
            this.DetalReportToolStrip.Name = "DetalReportToolStrip";
            this.DetalReportToolStrip.Size = new System.Drawing.Size(263, 22);
            this.DetalReportToolStrip.Text = "Детальный отчет";
            this.DetalReportToolStrip.Click += new System.EventHandler(this.DetalReportToolStrip_Click);
            // 
            // SumReportToolStrip
            // 
            this.SumReportToolStrip.Name = "SumReportToolStrip";
            this.SumReportToolStrip.Size = new System.Drawing.Size(263, 22);
            this.SumReportToolStrip.Text = "Суммарный отчет по материалам";
            this.SumReportToolStrip.Click += new System.EventHandler(this.SumReportToolStrip_Click);
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CounterPartiesToolStripMenuItem,
            this.CarsToolStripMenuItem,
            this.UsersToolStripMenuItem,
            this.CargosToolStripMenuItem,
            this.StocksToolStripMenuItem,
            this.DriversToolStripMenuItem,
            this.CarriersToolStripMenuItem,
            this.ConsigneesToolStripMenuItem,
            this.ConsignorsToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // CounterPartiesToolStripMenuItem
            // 
            this.CounterPartiesToolStripMenuItem.Name = "CounterPartiesToolStripMenuItem";
            this.CounterPartiesToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.CounterPartiesToolStripMenuItem.Text = "Контрагенты";
            this.CounterPartiesToolStripMenuItem.Click += new System.EventHandler(this.CounterPartiesToolStripMenuItem_Click);
            // 
            // CarsToolStripMenuItem
            // 
            this.CarsToolStripMenuItem.Name = "CarsToolStripMenuItem";
            this.CarsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.CarsToolStripMenuItem.Text = "Автомобили";
            this.CarsToolStripMenuItem.Click += new System.EventHandler(this.CarsToolStripMenuItem_Click);
            // 
            // UsersToolStripMenuItem
            // 
            this.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem";
            this.UsersToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.UsersToolStripMenuItem.Text = "Пользователи и пароли";
            this.UsersToolStripMenuItem.Click += new System.EventHandler(this.UsersToolStripMenuItem_Click);
            // 
            // CargosToolStripMenuItem
            // 
            this.CargosToolStripMenuItem.Name = "CargosToolStripMenuItem";
            this.CargosToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.CargosToolStripMenuItem.Text = "Грузы";
            this.CargosToolStripMenuItem.Click += new System.EventHandler(this.CargosToolStripMenuItem_Click);
            // 
            // StocksToolStripMenuItem
            // 
            this.StocksToolStripMenuItem.Name = "StocksToolStripMenuItem";
            this.StocksToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.StocksToolStripMenuItem.Text = "Склады";
            this.StocksToolStripMenuItem.Click += new System.EventHandler(this.StocksToolStripMenuItem_Click);
            // 
            // DriversToolStripMenuItem
            // 
            this.DriversToolStripMenuItem.Name = "DriversToolStripMenuItem";
            this.DriversToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.DriversToolStripMenuItem.Text = "Водители";
            this.DriversToolStripMenuItem.Click += new System.EventHandler(this.DriversToolStripMenuItem_Click);
            // 
            // CarriersToolStripMenuItem
            // 
            this.CarriersToolStripMenuItem.Name = "CarriersToolStripMenuItem";
            this.CarriersToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.CarriersToolStripMenuItem.Text = "Перевозчики";
            this.CarriersToolStripMenuItem.Click += new System.EventHandler(this.CarriersToolStripMenuItem_Click);
            // 
            // ConsigneesToolStripMenuItem
            // 
            this.ConsigneesToolStripMenuItem.Name = "ConsigneesToolStripMenuItem";
            this.ConsigneesToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ConsigneesToolStripMenuItem.Text = "Грузополучатели";
            this.ConsigneesToolStripMenuItem.Click += new System.EventHandler(this.ConsigneesToolStripMenuItem_Click);
            // 
            // ConsignorsToolStripMenuItem
            // 
            this.ConsignorsToolStripMenuItem.Name = "ConsignorsToolStripMenuItem";
            this.ConsignorsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.ConsignorsToolStripMenuItem.Text = "Грузоотправители";
            this.ConsignorsToolStripMenuItem.Click += new System.EventHandler(this.ConsignorsToolStripMenuItem_Click);
            // 
            // SettingToolStripMenu
            // 
            this.SettingToolStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingToolStripMenuItem,
            this.restartScalesToolStripMenuItem});
            this.SettingToolStripMenu.Name = "SettingToolStripMenu";
            this.SettingToolStripMenu.Size = new System.Drawing.Size(78, 20);
            this.SettingToolStripMenu.Text = "Настройка";
            // 
            // SettingToolStripMenuItem
            // 
            this.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem";
            this.SettingToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.SettingToolStripMenuItem.Text = "Настройка...";
            this.SettingToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Click);
            // 
            // restartScalesToolStripMenuItem
            // 
            this.restartScalesToolStripMenuItem.Name = "restartScalesToolStripMenuItem";
            this.restartScalesToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.restartScalesToolStripMenuItem.Text = "Переподключить весы";
            this.restartScalesToolStripMenuItem.Click += new System.EventHandler(this.restartScalesToolStripMenuItem_Click);
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upload1CToolStripMenuItem});
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.cToolStripMenuItem.Text = "1C";
            // 
            // upload1CToolStripMenuItem
            // 
            this.upload1CToolStripMenuItem.Name = "upload1CToolStripMenuItem";
            this.upload1CToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.upload1CToolStripMenuItem.Text = "Выгрузить данные";
            this.upload1CToolStripMenuItem.Click += new System.EventHandler(this.Upload1CToolStripMenuItem_Click);
            // 
            // UploadToolStripMenuItem
            // 
            this.UploadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadDataToolStripMenuItem});
            this.UploadToolStripMenuItem.Name = "UploadToolStripMenuItem";
            this.UploadToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.UploadToolStripMenuItem.Text = "Выгрузка данных";
            // 
            // uploadDataToolStripMenuItem
            // 
            this.uploadDataToolStripMenuItem.Name = "uploadDataToolStripMenuItem";
            this.uploadDataToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.uploadDataToolStripMenuItem.Text = "Выгрузить данные";
            this.uploadDataToolStripMenuItem.Click += new System.EventHandler(this.UploadDataToolStripMenuItem_Click);
            // 
            // refToolStripMenuItem
            // 
            this.refToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem,
            this.ManualToolStripMenuItem});
            this.refToolStripMenuItem.Name = "refToolStripMenuItem";
            this.refToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.refToolStripMenuItem.Text = "Справка";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.AboutToolStripMenuItem.Text = "О программе";
            this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // ManualToolStripMenuItem
            // 
            this.ManualToolStripMenuItem.Name = "ManualToolStripMenuItem";
            this.ManualToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.ManualToolStripMenuItem.Text = "Руководство пользователя";
            this.ManualToolStripMenuItem.Click += new System.EventHandler(this.ManualToolStripMenuItem_Click);
            // 
            // panelScales
            // 
            this.panelScales.Controls.Add(this.progressBar);
            this.panelScales.Controls.Add(this.lblWeight);
            this.panelScales.Controls.Add(this.lblValueOfLibra);
            this.panelScales.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelScales.Location = new System.Drawing.Point(0, 24);
            this.panelScales.Name = "panelScales";
            this.panelScales.Size = new System.Drawing.Size(1040, 46);
            this.panelScales.TabIndex = 2;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 24);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(216, 19);
            this.progressBar.TabIndex = 2;
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.BackColor = System.Drawing.Color.Black;
            this.lblWeight.Font = new System.Drawing.Font("Courier New", 29F, System.Drawing.FontStyle.Bold);
            this.lblWeight.ForeColor = System.Drawing.Color.Red;
            this.lblWeight.Location = new System.Drawing.Point(229, 0);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(181, 46);
            this.lblWeight.TabIndex = 1;
            this.lblWeight.Text = " 000000";
            // 
            // lblValueOfLibra
            // 
            this.lblValueOfLibra.AutoSize = true;
            this.lblValueOfLibra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblValueOfLibra.Location = new System.Drawing.Point(3, 3);
            this.lblValueOfLibra.Name = "lblValueOfLibra";
            this.lblValueOfLibra.Size = new System.Drawing.Size(221, 20);
            this.lblValueOfLibra.TabIndex = 0;
            this.lblValueOfLibra.Text = "Значение на текущих весах:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 590);
            this.Controls.Add(this.panelScales);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1056, 529);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ELibra - электронные весы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Main_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelScales.ResumeLayout(false);
            this.panelScales.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem операцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WeighingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem JournalToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DayReportToolStrip;
        private System.Windows.Forms.ToolStripMenuItem MouthReportToolStrip;
        private System.Windows.Forms.ToolStripMenuItem DetalReportToolStrip;
        private System.Windows.Forms.ToolStripMenuItem SumReportToolStrip;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CounterPartiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CarsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingToolStripMenu;
        private System.Windows.Forms.ToolStripMenuItem SettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.Panel panelScales;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblValueOfLibra;
        private System.Windows.Forms.ToolStripMenuItem VideosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CargosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StocksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DriversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CarriersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConsigneesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConsignorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upload1CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadDataToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar;
        //private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem ManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartScalesToolStripMenuItem;
    }
}

