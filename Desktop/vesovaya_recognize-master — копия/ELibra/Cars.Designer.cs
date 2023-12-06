namespace ELibra
{
    partial class Cars
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cars));
            this.panel1 = new System.Windows.Forms.Panel();
            this.qrBox = new System.Windows.Forms.GroupBox();
            this.qr_picture = new System.Windows.Forms.PictureBox();
            this.SaveQr = new System.Windows.Forms.Button();
            this.btnGetWeight = new System.Windows.Forms.Button();
            this.btnRemoveDriver = new System.Windows.Forms.Button();
            this.btnAddDriver = new System.Windows.Forms.Button();
            this.listDrivers = new System.Windows.Forms.ListBox();
            this.tWeight = new System.Windows.Forms.TextBox();
            this.lblDrivers = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblCounterParties = new System.Windows.Forms.Label();
            this.lblMark = new System.Windows.Forms.Label();
            this.cbCounterParties = new System.Windows.Forms.ComboBox();
            this.cbMark = new System.Windows.Forms.ComboBox();
            this.tNumber = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripCancel = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.qrBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qr_picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.qrBox);
            this.panel1.Controls.Add(this.btnGetWeight);
            this.panel1.Controls.Add(this.btnRemoveDriver);
            this.panel1.Controls.Add(this.btnAddDriver);
            this.panel1.Controls.Add(this.listDrivers);
            this.panel1.Controls.Add(this.tWeight);
            this.panel1.Controls.Add(this.lblDrivers);
            this.panel1.Controls.Add(this.lblWeight);
            this.panel1.Controls.Add(this.lblCounterParties);
            this.panel1.Controls.Add(this.lblMark);
            this.panel1.Controls.Add(this.cbCounterParties);
            this.panel1.Controls.Add(this.cbMark);
            this.panel1.Controls.Add(this.tNumber);
            this.panel1.Controls.Add(this.lblNumber);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 227);
            this.panel1.TabIndex = 1;
            // 
            // qrBox
            // 
            this.qrBox.Controls.Add(this.qr_picture);
            this.qrBox.Controls.Add(this.SaveQr);
            this.qrBox.Location = new System.Drawing.Point(713, 9);
            this.qrBox.Name = "qrBox";
            this.qrBox.Size = new System.Drawing.Size(162, 200);
            this.qrBox.TabIndex = 15;
            this.qrBox.TabStop = false;
            this.qrBox.Visible = false;
            // 
            // qr_picture
            // 
            this.qr_picture.Location = new System.Drawing.Point(6, 15);
            this.qr_picture.Name = "qr_picture";
            this.qr_picture.Size = new System.Drawing.Size(150, 150);
            this.qr_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.qr_picture.TabIndex = 13;
            this.qr_picture.TabStop = false;
            // 
            // SaveQr
            // 
            this.SaveQr.Location = new System.Drawing.Point(6, 171);
            this.SaveQr.Name = "SaveQr";
            this.SaveQr.Size = new System.Drawing.Size(150, 23);
            this.SaveQr.TabIndex = 14;
            this.SaveQr.Text = "Сохранить QR-код";
            this.SaveQr.UseVisualStyleBackColor = true;
            this.SaveQr.Click += new System.EventHandler(this.PrintQr_Click);
            // 
            // btnGetWeight
            // 
            this.btnGetWeight.Image = ((System.Drawing.Image)(resources.GetObject("btnGetWeight.Image")));
            this.btnGetWeight.Location = new System.Drawing.Point(277, 88);
            this.btnGetWeight.Name = "btnGetWeight";
            this.btnGetWeight.Size = new System.Drawing.Size(23, 20);
            this.btnGetWeight.TabIndex = 12;
            this.btnGetWeight.UseVisualStyleBackColor = true;
            this.btnGetWeight.Visible = false;
            this.btnGetWeight.Click += new System.EventHandler(this.BtnGetWeight_Click);
            // 
            // btnRemoveDriver
            // 
            this.btnRemoveDriver.Enabled = false;
            this.btnRemoveDriver.Location = new System.Drawing.Point(632, 186);
            this.btnRemoveDriver.Name = "btnRemoveDriver";
            this.btnRemoveDriver.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveDriver.TabIndex = 11;
            this.btnRemoveDriver.Text = "Удалить";
            this.btnRemoveDriver.UseVisualStyleBackColor = true;
            this.btnRemoveDriver.Click += new System.EventHandler(this.BtnRemoveDriver_Click);
            // 
            // btnAddDriver
            // 
            this.btnAddDriver.Enabled = false;
            this.btnAddDriver.Location = new System.Drawing.Point(632, 114);
            this.btnAddDriver.Name = "btnAddDriver";
            this.btnAddDriver.Size = new System.Drawing.Size(75, 23);
            this.btnAddDriver.TabIndex = 10;
            this.btnAddDriver.Text = "Добавить";
            this.btnAddDriver.UseVisualStyleBackColor = true;
            this.btnAddDriver.Click += new System.EventHandler(this.BtnAddDriver_Click);
            // 
            // listDrivers
            // 
            this.listDrivers.FormattingEnabled = true;
            this.listDrivers.Location = new System.Drawing.Point(171, 114);
            this.listDrivers.Name = "listDrivers";
            this.listDrivers.Size = new System.Drawing.Size(455, 95);
            this.listDrivers.TabIndex = 9;
            // 
            // tWeight
            // 
            this.tWeight.Enabled = false;
            this.tWeight.Location = new System.Drawing.Point(171, 88);
            this.tWeight.Name = "tWeight";
            this.tWeight.Size = new System.Drawing.Size(100, 20);
            this.tWeight.TabIndex = 8;
            this.tWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TWeight_KeyPress);
            // 
            // lblDrivers
            // 
            this.lblDrivers.AutoSize = true;
            this.lblDrivers.Location = new System.Drawing.Point(106, 119);
            this.lblDrivers.Name = "lblDrivers";
            this.lblDrivers.Size = new System.Drawing.Size(58, 13);
            this.lblDrivers.TabIndex = 7;
            this.lblDrivers.Text = "Водители:";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(12, 91);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(152, 13);
            this.lblWeight.TabIndex = 6;
            this.lblWeight.Text = "Вес пустого автомобиля, кг:";
            // 
            // lblCounterParties
            // 
            this.lblCounterParties.AutoSize = true;
            this.lblCounterParties.Location = new System.Drawing.Point(96, 65);
            this.lblCounterParties.Name = "lblCounterParties";
            this.lblCounterParties.Size = new System.Drawing.Size(68, 13);
            this.lblCounterParties.TabIndex = 5;
            this.lblCounterParties.Text = "Контрагент:";
            // 
            // lblMark
            // 
            this.lblMark.AutoSize = true;
            this.lblMark.Location = new System.Drawing.Point(121, 38);
            this.lblMark.Name = "lblMark";
            this.lblMark.Size = new System.Drawing.Size(43, 13);
            this.lblMark.TabIndex = 4;
            this.lblMark.Text = "Марка:";
            // 
            // cbCounterParties
            // 
            this.cbCounterParties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCounterParties.Enabled = false;
            this.cbCounterParties.FormattingEnabled = true;
            this.cbCounterParties.Location = new System.Drawing.Point(171, 62);
            this.cbCounterParties.Name = "cbCounterParties";
            this.cbCounterParties.Size = new System.Drawing.Size(536, 21);
            this.cbCounterParties.TabIndex = 3;
            // 
            // cbMark
            // 
            this.cbMark.Enabled = false;
            this.cbMark.FormattingEnabled = true;
            this.cbMark.Location = new System.Drawing.Point(171, 35);
            this.cbMark.Name = "cbMark";
            this.cbMark.Size = new System.Drawing.Size(536, 21);
            this.cbMark.TabIndex = 2;
            // 
            // tNumber
            // 
            this.tNumber.Enabled = false;
            this.tNumber.Location = new System.Drawing.Point(171, 9);
            this.tNumber.Name = "tNumber";
            this.tNumber.Size = new System.Drawing.Size(536, 20);
            this.tNumber.TabIndex = 1;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(121, 12);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(44, 13);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "Номер:";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.DataSource = this.bindingSource;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 254);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(960, 196);
            this.dataGridView.TabIndex = 3;
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = null;
            this.bindingNavigator.BindingSource = this.bindingSource;
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = null;
            this.bindingNavigator.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.toolStripAdd,
            this.toolStripEdit,
            this.toolStripSave,
            this.toolStripDelete,
            this.toolStripCancel});
            this.bindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(960, 27);
            this.bindingNavigator.TabIndex = 7;
            this.bindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Переместить назад";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Положение";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Текущее положение";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveNextItem.Text = "Переместить вперед";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(24, 24);
            this.bindingNavigatorMoveLastItem.Text = "Переместить в конец";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripAdd
            // 
            this.toolStripAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAdd.Enabled = false;
            this.toolStripAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAdd.Image")));
            this.toolStripAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAdd.Name = "toolStripAdd";
            this.toolStripAdd.Size = new System.Drawing.Size(24, 24);
            this.toolStripAdd.Text = "Добавить";
            this.toolStripAdd.Click += new System.EventHandler(this.ToolStripAdd_Click);
            // 
            // toolStripEdit
            // 
            this.toolStripEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEdit.Enabled = false;
            this.toolStripEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEdit.Image")));
            this.toolStripEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEdit.Name = "toolStripEdit";
            this.toolStripEdit.Size = new System.Drawing.Size(24, 24);
            this.toolStripEdit.Text = "Редактировать";
            this.toolStripEdit.Click += new System.EventHandler(this.ToolStripEdit_Click);
            // 
            // toolStripSave
            // 
            this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSave.Enabled = false;
            this.toolStripSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSave.Image")));
            this.toolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSave.Name = "toolStripSave";
            this.toolStripSave.Size = new System.Drawing.Size(24, 24);
            this.toolStripSave.Text = "Сохранить";
            this.toolStripSave.Click += new System.EventHandler(this.ToolStripSave_Click);
            // 
            // toolStripDelete
            // 
            this.toolStripDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDelete.Enabled = false;
            this.toolStripDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDelete.Image")));
            this.toolStripDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDelete.Name = "toolStripDelete";
            this.toolStripDelete.Size = new System.Drawing.Size(24, 24);
            this.toolStripDelete.Text = "Удалить";
            this.toolStripDelete.Click += new System.EventHandler(this.ToolStripDelete_Click);
            // 
            // toolStripCancel
            // 
            this.toolStripCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCancel.Enabled = false;
            this.toolStripCancel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCancel.Image")));
            this.toolStripCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCancel.Name = "toolStripCancel";
            this.toolStripCancel.Size = new System.Drawing.Size(24, 24);
            this.toolStripCancel.Text = "Отменить изменения";
            this.toolStripCancel.Click += new System.EventHandler(this.ToolStripCancel_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "qr_code";
            this.saveFileDialog1.Filter = "Image files (*.png, *.jpg, *.bmp) | *.png; *.jpg;  *.bmp;";
            // 
            // Cars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bindingNavigator);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Cars";
            this.Text = "Автомобили";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Cars_FormClosed);
            this.Load += new System.EventHandler(this.Cars_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.qrBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qr_picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ListBox listDrivers;
        private System.Windows.Forms.TextBox tWeight;
        private System.Windows.Forms.Label lblDrivers;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblCounterParties;
        private System.Windows.Forms.Label lblMark;
        private System.Windows.Forms.ComboBox cbCounterParties;
        private System.Windows.Forms.ComboBox cbMark;
        private System.Windows.Forms.TextBox tNumber;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Button btnRemoveDriver;
        private System.Windows.Forms.Button btnAddDriver;
        private System.Windows.Forms.Button btnGetWeight;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripEdit;
        private System.Windows.Forms.ToolStripButton toolStripSave;
        private System.Windows.Forms.ToolStripButton toolStripDelete;
        private System.Windows.Forms.ToolStripButton toolStripCancel;
        private System.Windows.Forms.ToolStripButton toolStripAdd;
        private System.Windows.Forms.PictureBox qr_picture;
        private System.Windows.Forms.Button SaveQr;
        private System.Windows.Forms.GroupBox qrBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}