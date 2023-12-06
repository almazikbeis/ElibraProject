namespace ELibra
{
    partial class AboutLicense
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblVideo = new System.Windows.Forms.Label();
            this.lblLicDate = new System.Windows.Forms.Label();
            this.lbl1C = new System.Windows.Forms.Label();
            this.lblEdit = new System.Windows.Forms.Label();
            this.lblNN = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Лицензия доступна до:";
            // 
            // lblVideo
            // 
            this.lblVideo.AutoSize = true;
            this.lblVideo.Location = new System.Drawing.Point(12, 135);
            this.lblVideo.Name = "lblVideo";
            this.lblVideo.Size = new System.Drawing.Size(77, 13);
            this.lblVideo.TabIndex = 1;
            this.lblVideo.Text = "Запись видео";
            // 
            // lblLicDate
            // 
            this.lblLicDate.AutoSize = true;
            this.lblLicDate.Location = new System.Drawing.Point(142, 9);
            this.lblLicDate.Name = "lblLicDate";
            this.lblLicDate.Size = new System.Drawing.Size(0, 13);
            this.lblLicDate.TabIndex = 2;
            // 
            // lbl1C
            // 
            this.lbl1C.AutoSize = true;
            this.lbl1C.Location = new System.Drawing.Point(12, 160);
            this.lbl1C.Name = "lbl1C";
            this.lbl1C.Size = new System.Drawing.Size(72, 13);
            this.lbl1C.TabIndex = 3;
            this.lbl1C.Text = "Выгрузка 1С";
            // 
            // lblEdit
            // 
            this.lblEdit.AutoSize = true;
            this.lblEdit.Location = new System.Drawing.Point(12, 60);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(258, 13);
            this.lblEdit.TabIndex = 4;
            this.lblEdit.Text = "Пользователь для редактирования взвешиваний";
            // 
            // lblNN
            // 
            this.lblNN.AutoSize = true;
            this.lblNN.Location = new System.Drawing.Point(12, 110);
            this.lblNN.Name = "lblNN";
            this.lblNN.Size = new System.Drawing.Size(133, 13);
            this.lblNN.TabIndex = 5;
            this.lblNN.Text = "Распознование номеров";
            // 
            // lblUpload
            // 
            this.lblUpload.AutoSize = true;
            this.lblUpload.Location = new System.Drawing.Point(12, 85);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(150, 13);
            this.lblUpload.TabIndex = 6;
            this.lblUpload.Text = "Выгрузка данных на сервер";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Доступный функционал:";
            // 
            // AboutLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 186);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblUpload);
            this.Controls.Add(this.lblNN);
            this.Controls.Add(this.lblEdit);
            this.Controls.Add(this.lbl1C);
            this.Controls.Add(this.lblLicDate);
            this.Controls.Add(this.lblVideo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutLicense";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Лицензия";
            this.Load += new System.EventHandler(this.AboutLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVideo;
        private System.Windows.Forms.Label lblLicDate;
        private System.Windows.Forms.Label lbl1C;
        private System.Windows.Forms.Label lblEdit;
        private System.Windows.Forms.Label lblNN;
        private System.Windows.Forms.Label lblUpload;
        private System.Windows.Forms.Label label7;
    }
}