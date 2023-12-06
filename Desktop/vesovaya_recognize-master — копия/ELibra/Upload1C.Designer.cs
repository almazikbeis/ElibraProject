namespace ELibra
{
    partial class Upload1C
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Upload1C));
            this.label1 = new System.Windows.Forms.Label();
            this.cBoxDay = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimeBefore = new System.Windows.Forms.DateTimePicker();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выбрать данные за период";
            // 
            // cBoxDay
            // 
            this.cBoxDay.AutoSize = true;
            this.cBoxDay.Checked = true;
            this.cBoxDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxDay.Location = new System.Drawing.Point(15, 25);
            this.cBoxDay.Name = "cBoxDay";
            this.cBoxDay.Size = new System.Drawing.Size(65, 17);
            this.cBoxDay.TabIndex = 1;
            this.cBoxDay.Text = "за день";
            this.cBoxDay.UseVisualStyleBackColor = true;
            this.cBoxDay.CheckedChanged += new System.EventHandler(this.CBoxDay_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "с:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "по:";
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Location = new System.Drawing.Point(40, 48);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimeFrom.TabIndex = 4;
            // 
            // dateTimeBefore
            // 
            this.dateTimeBefore.Enabled = false;
            this.dateTimeBefore.Location = new System.Drawing.Point(40, 74);
            this.dateTimeBefore.Name = "dateTimeBefore";
            this.dateTimeBefore.Size = new System.Drawing.Size(200, 20);
            this.dateTimeBefore.TabIndex = 5;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(12, 100);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(105, 23);
            this.btnUpload.TabIndex = 6;
            this.btnUpload.Text = "Выгрузить";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(123, 100);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(117, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // Upload1C
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 133);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.dateTimeBefore);
            this.Controls.Add(this.dateTimeFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cBoxDay);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Upload1C";
            this.Text = "Выгрузка данных в 1С";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cBoxDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.DateTimePicker dateTimeBefore;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnClose;
    }
}