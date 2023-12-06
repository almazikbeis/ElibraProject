namespace ELibra
{
    partial class Invoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoice));
            this.lblVolume = new System.Windows.Forms.Label();
            this.lblSeal = new System.Windows.Forms.Label();
            this.tVolume = new System.Windows.Forms.TextBox();
            this.tSeal = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(10, 11);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(82, 13);
            this.lblVolume.TabIndex = 1;
            this.lblVolume.Text = "Объем, куб.м.:";
            // 
            // lblSeal
            // 
            this.lblSeal.AutoSize = true;
            this.lblSeal.Location = new System.Drawing.Point(4, 37);
            this.lblSeal.Name = "lblSeal";
            this.lblSeal.Size = new System.Drawing.Size(88, 13);
            this.lblSeal.TabIndex = 2;
            this.lblSeal.Text = "Пломба, номер:";
            // 
            // tVolume
            // 
            this.tVolume.Location = new System.Drawing.Point(98, 8);
            this.tVolume.Name = "tVolume";
            this.tVolume.Size = new System.Drawing.Size(257, 20);
            this.tVolume.TabIndex = 4;
            this.tVolume.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TVolume_KeyDown);
            // 
            // tSeal
            // 
            this.tSeal.Location = new System.Drawing.Point(98, 34);
            this.tSeal.Name = "tSeal";
            this.tSeal.Size = new System.Drawing.Size(257, 20);
            this.tSeal.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(98, 60);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ок";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(280, 60);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 91);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tSeal);
            this.Controls.Add(this.tVolume);
            this.Controls.Add(this.lblSeal);
            this.Controls.Add(this.lblVolume);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Invoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сведения для формирования накладной";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblSeal;
        private System.Windows.Forms.TextBox tVolume;
        private System.Windows.Forms.TextBox tSeal;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}