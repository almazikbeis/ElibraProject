namespace License
{
    partial class License
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(License));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDriver = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnChangeLicense = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Носитель:";
            // 
            // cmbDriver
            // 
            this.cmbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDriver.FormattingEnabled = true;
            this.cmbDriver.Location = new System.Drawing.Point(77, 6);
            this.cmbDriver.Name = "cmbDriver";
            this.cmbDriver.Size = new System.Drawing.Size(121, 21);
            this.cmbDriver.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(15, 33);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(183, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Сгенерировать";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnChangeLicense
            // 
            this.btnChangeLicense.Location = new System.Drawing.Point(15, 62);
            this.btnChangeLicense.Name = "btnChangeLicense";
            this.btnChangeLicense.Size = new System.Drawing.Size(183, 23);
            this.btnChangeLicense.TabIndex = 5;
            this.btnChangeLicense.Text = "Изменить лицензию";
            this.btnChangeLicense.UseVisualStyleBackColor = true;
            this.btnChangeLicense.Click += new System.EventHandler(this.BtnChangeLicense_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(15, 91);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(183, 23);
            this.btnSetting.TabIndex = 6;
            this.btnSetting.Text = "Настройки";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.BtnSetting_Click);
            // 
            // License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 126);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnChangeLicense);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbDriver);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "License";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лицензия";
            this.Load += new System.EventHandler(this.SetLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDriver;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnChangeLicense;
        private System.Windows.Forms.Button btnSetting;
    }
}

