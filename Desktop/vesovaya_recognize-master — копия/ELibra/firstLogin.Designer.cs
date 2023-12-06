namespace ELibra
{
    partial class firstLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(firstLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.tURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tPass = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.tLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин:";
            // 
            // tURL
            // 
            this.tURL.Location = new System.Drawing.Point(59, 12);
            this.tURL.Name = "tURL";
            this.tURL.Size = new System.Drawing.Size(207, 20);
            this.tURL.TabIndex = 1;
            this.tURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.T_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Пароль:";
            // 
            // tPass
            // 
            this.tPass.Location = new System.Drawing.Point(59, 73);
            this.tPass.Name = "tPass";
            this.tPass.PasswordChar = '*';
            this.tPass.Size = new System.Drawing.Size(207, 20);
            this.tPass.TabIndex = 3;
            this.tPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.T_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(59, 99);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(207, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Вход";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // tLogin
            // 
            this.tLogin.Location = new System.Drawing.Point(59, 42);
            this.tLogin.Name = "tLogin";
            this.tLogin.Size = new System.Drawing.Size(207, 20);
            this.tLogin.TabIndex = 2;
            this.tLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.T_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "URL:";
            // 
            // firstLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 132);
            this.Controls.Add(this.tLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tURL);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(294, 171);
            this.Name = "firstLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вход на сервер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FirstLogin_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tPass;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tLogin;
        private System.Windows.Forms.Label label3;
    }
}