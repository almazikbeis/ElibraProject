
namespace ELibra
{
    partial class Journal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Journal));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cScaleBrutto = new System.Windows.Forms.CheckBox();
            this.cScaleTara = new System.Windows.Forms.CheckBox();
            this.cmbScaleBrutto = new System.Windows.Forms.ComboBox();
            this.cmbScalseTara = new System.Windows.Forms.ComboBox();
            this.cmbPost = new System.Windows.Forms.ComboBox();
            this.lblPost = new System.Windows.Forms.Label();
            this.dateTimeBefore = new System.Windows.Forms.DateTimePicker();
            this.lblBefore = new System.Windows.Forms.Label();
            this.dateTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
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
            this.toolStripAdd = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownPrint = new System.Windows.Forms.ToolStripDropDownButton();
            this.WeighingTalonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TTNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nakl1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nakl2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripBtnFinish = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnNotFinish = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripVideo = new System.Windows.Forms.ToolStripButton();
            this.toolStripNotVideo = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weightEmpty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weightFull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weightClear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weightDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scalesTara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scalesBrutto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.videoFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cScaleBrutto);
            this.panel1.Controls.Add(this.cScaleTara);
            this.panel1.Controls.Add(this.cmbScaleBrutto);
            this.panel1.Controls.Add(this.cmbScalseTara);
            this.panel1.Controls.Add(this.cmbPost);
            this.panel1.Controls.Add(this.lblPost);
            this.panel1.Controls.Add(this.dateTimeBefore);
            this.panel1.Controls.Add(this.lblBefore);
            this.panel1.Controls.Add(this.dateTimeFrom);
            this.panel1.Controls.Add(this.lblFrom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(977, 50);
            this.panel1.TabIndex = 11;
            // 
            // cScaleBrutto
            // 
            this.cScaleBrutto.AutoSize = true;
            this.cScaleBrutto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cScaleBrutto.Location = new System.Drawing.Point(753, 16);
            this.cScaleBrutto.Name = "cScaleBrutto";
            this.cScaleBrutto.Size = new System.Drawing.Size(112, 20);
            this.cScaleBrutto.TabIndex = 11;
            this.cScaleBrutto.Text = "Весы брутто:";
            this.cScaleBrutto.UseVisualStyleBackColor = true;
            this.cScaleBrutto.CheckedChanged += new System.EventHandler(this.cScaleBrutto_CheckedChanged);
            // 
            // cScaleTara
            // 
            this.cScaleTara.AutoSize = true;
            this.cScaleTara.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cScaleTara.Location = new System.Drawing.Point(541, 16);
            this.cScaleTara.Name = "cScaleTara";
            this.cScaleTara.Size = new System.Drawing.Size(97, 20);
            this.cScaleTara.TabIndex = 10;
            this.cScaleTara.Text = "Весы тара:";
            this.cScaleTara.UseVisualStyleBackColor = true;
            this.cScaleTara.CheckedChanged += new System.EventHandler(this.cScaleTara_CheckedChanged);
            // 
            // cmbScaleBrutto
            // 
            this.cmbScaleBrutto.Enabled = false;
            this.cmbScaleBrutto.FormattingEnabled = true;
            this.cmbScaleBrutto.Location = new System.Drawing.Point(871, 14);
            this.cmbScaleBrutto.Name = "cmbScaleBrutto";
            this.cmbScaleBrutto.Size = new System.Drawing.Size(94, 23);
            this.cmbScaleBrutto.TabIndex = 9;
            this.cmbScaleBrutto.SelectedIndexChanged += new System.EventHandler(this.cmbScaleBrutto_SelectedIndexChanged);
            // 
            // cmbScalseTara
            // 
            this.cmbScalseTara.Enabled = false;
            this.cmbScalseTara.FormattingEnabled = true;
            this.cmbScalseTara.Location = new System.Drawing.Point(644, 13);
            this.cmbScalseTara.Name = "cmbScalseTara";
            this.cmbScalseTara.Size = new System.Drawing.Size(94, 23);
            this.cmbScalseTara.TabIndex = 7;
            this.cmbScalseTara.SelectedIndexChanged += new System.EventHandler(this.cmbScalseTara_SelectedIndexChanged);
            // 
            // cmbPost
            // 
            this.cmbPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbPost.FormattingEnabled = true;
            this.cmbPost.Location = new System.Drawing.Point(391, 13);
            this.cmbPost.Name = "cmbPost";
            this.cmbPost.Size = new System.Drawing.Size(121, 24);
            this.cmbPost.TabIndex = 5;
            this.cmbPost.SelectedIndexChanged += new System.EventHandler(this.DateTime_ValueChanged);
            // 
            // lblPost
            // 
            this.lblPost.AutoSize = true;
            this.lblPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPost.Location = new System.Drawing.Point(309, 18);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(76, 16);
            this.lblPost.TabIndex = 4;
            this.lblPost.Text = "Оператор:";
            // 
            // dateTimeBefore
            // 
            this.dateTimeBefore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimeBefore.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeBefore.Location = new System.Drawing.Point(188, 13);
            this.dateTimeBefore.Name = "dateTimeBefore";
            this.dateTimeBefore.Size = new System.Drawing.Size(115, 22);
            this.dateTimeBefore.TabIndex = 3;
            this.dateTimeBefore.ValueChanged += new System.EventHandler(this.DateTime_ValueChanged);
            // 
            // lblBefore
            // 
            this.lblBefore.AutoSize = true;
            this.lblBefore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBefore.Location = new System.Drawing.Point(155, 18);
            this.lblBefore.Name = "lblBefore";
            this.lblBefore.Size = new System.Drawing.Size(27, 16);
            this.lblBefore.TabIndex = 2;
            this.lblBefore.Text = "по:";
            // 
            // dateTimeFrom
            // 
            this.dateTimeFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeFrom.Location = new System.Drawing.Point(34, 13);
            this.dateTimeFrom.Name = "dateTimeFrom";
            this.dateTimeFrom.Size = new System.Drawing.Size(115, 22);
            this.dateTimeFrom.TabIndex = 1;
            this.dateTimeFrom.ValueChanged += new System.EventHandler(this.DateTime_ValueChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFrom.Location = new System.Drawing.Point(10, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(18, 16);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "с:";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.contr,
            this.number,
            this.model,
            this.material,
            this.weightEmpty,
            this.weightFull,
            this.weightClear,
            this.weightDoc,
            this.volume,
            this.adj,
            this.scalesTara,
            this.scalesBrutto,
            this.changed,
            this.videoFile});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 77);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(977, 239);
            this.dataGridView.TabIndex = 12;
            this.dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellEnter);
            this.dataGridView.DoubleClick += new System.EventHandler(this.DataGridView_DoubleClick);
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = null;
            this.bindingNavigator.BindingSource = this.bindingSource;
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = null;
            this.bindingNavigator.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.toolStripAdd,
            this.bindingNavigatorSeparator2,
            this.toolStripEdit,
            this.toolStripUpdate,
            this.toolStripDropDownPrint,
            this.toolStripLabel1,
            this.toolStripBtnFinish,
            this.toolStripBtnNotFinish,
            this.toolStripLabel2,
            this.toolStripVideo,
            this.toolStripNotVideo});
            this.bindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(977, 27);
            this.bindingNavigator.TabIndex = 13;
            this.bindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(39, 24);
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
            // toolStripAdd
            // 
            this.toolStripAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripAdd.Image")));
            this.toolStripAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripAdd.Name = "toolStripAdd";
            this.toolStripAdd.Size = new System.Drawing.Size(24, 24);
            this.toolStripAdd.Text = "Добавить";
            this.toolStripAdd.Click += new System.EventHandler(this.ToolStripAdd_Click);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripEdit
            // 
            this.toolStripEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEdit.Image")));
            this.toolStripEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEdit.Name = "toolStripEdit";
            this.toolStripEdit.Size = new System.Drawing.Size(24, 24);
            this.toolStripEdit.Text = "Редоктировать";
            this.toolStripEdit.ToolTipText = "Редактировать";
            this.toolStripEdit.Click += new System.EventHandler(this.DataGridView_DoubleClick);
            // 
            // toolStripUpdate
            // 
            this.toolStripUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripUpdate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripUpdate.Image")));
            this.toolStripUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripUpdate.Name = "toolStripUpdate";
            this.toolStripUpdate.Size = new System.Drawing.Size(24, 24);
            this.toolStripUpdate.Text = "Обновить";
            this.toolStripUpdate.Click += new System.EventHandler(this.ToolStripUpdate_Click);
            // 
            // toolStripDropDownPrint
            // 
            this.toolStripDropDownPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownPrint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WeighingTalonToolStripMenuItem,
            this.TTNToolStripMenuItem,
            this.nakl1ToolStripMenuItem,
            this.nakl2ToolStripMenuItem});
            this.toolStripDropDownPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownPrint.Image")));
            this.toolStripDropDownPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownPrint.Name = "toolStripDropDownPrint";
            this.toolStripDropDownPrint.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownPrint.Text = "Печать";
            // 
            // WeighingTalonToolStripMenuItem
            // 
            this.WeighingTalonToolStripMenuItem.Name = "WeighingTalonToolStripMenuItem";
            this.WeighingTalonToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.WeighingTalonToolStripMenuItem.Text = "Талон о взвешивании";
            this.WeighingTalonToolStripMenuItem.Click += new System.EventHandler(this.WeighingTalonToolStripMenuItem_Click);
            // 
            // TTNToolStripMenuItem
            // 
            this.TTNToolStripMenuItem.Name = "TTNToolStripMenuItem";
            this.TTNToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.TTNToolStripMenuItem.Text = "Товарно-транспортная накладная";
            this.TTNToolStripMenuItem.Click += new System.EventHandler(this.TTNToolStripMenuItem_Click);
            // 
            // nakl1ToolStripMenuItem
            // 
            this.nakl1ToolStripMenuItem.Name = "nakl1ToolStripMenuItem";
            this.nakl1ToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.nakl1ToolStripMenuItem.Text = "Накладная 1";
            this.nakl1ToolStripMenuItem.Click += new System.EventHandler(this.Nakl1ToolStripMenuItem_Click);
            // 
            // nakl2ToolStripMenuItem
            // 
            this.nakl2ToolStripMenuItem.Name = "nakl2ToolStripMenuItem";
            this.nakl2ToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.nakl2ToolStripMenuItem.Text = "Накладная 2";
            this.nakl2ToolStripMenuItem.Click += new System.EventHandler(this.Nakl2ToolStripMenuItem_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(82, 24);
            this.toolStripLabel1.Text = "Показывать:";
            // 
            // toolStripBtnFinish
            // 
            this.toolStripBtnFinish.BackColor = System.Drawing.Color.Orange;
            this.toolStripBtnFinish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnFinish.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnFinish.Image")));
            this.toolStripBtnFinish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnFinish.Name = "toolStripBtnFinish";
            this.toolStripBtnFinish.Size = new System.Drawing.Size(24, 24);
            this.toolStripBtnFinish.Text = "Показывать завершенные взвешивания";
            this.toolStripBtnFinish.Click += new System.EventHandler(this.ToolStripBtnFinish_Click);
            // 
            // toolStripBtnNotFinish
            // 
            this.toolStripBtnNotFinish.BackColor = System.Drawing.Color.Orange;
            this.toolStripBtnNotFinish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnNotFinish.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnNotFinish.Image")));
            this.toolStripBtnNotFinish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnNotFinish.Name = "toolStripBtnNotFinish";
            this.toolStripBtnNotFinish.Size = new System.Drawing.Size(24, 24);
            this.toolStripBtnNotFinish.Text = "Показывать не завершенные взвешивания";
            this.toolStripBtnNotFinish.Click += new System.EventHandler(this.ToolStripBtnNotFinish_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(102, 24);
            this.toolStripLabel2.Text = "Наличие видео:";
            // 
            // toolStripVideo
            // 
            this.toolStripVideo.BackColor = System.Drawing.Color.Orange;
            this.toolStripVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripVideo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripVideo.Image")));
            this.toolStripVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripVideo.Name = "toolStripVideo";
            this.toolStripVideo.Size = new System.Drawing.Size(24, 24);
            this.toolStripVideo.Text = "Показывать взвешивания с видео";
            this.toolStripVideo.Click += new System.EventHandler(this.ToolStripVideo_Click);
            // 
            // toolStripNotVideo
            // 
            this.toolStripNotVideo.BackColor = System.Drawing.Color.Orange;
            this.toolStripNotVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripNotVideo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNotVideo.Image")));
            this.toolStripNotVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNotVideo.Name = "toolStripNotVideo";
            this.toolStripNotVideo.Size = new System.Drawing.Size(24, 24);
            this.toolStripNotVideo.Text = "Показывать взвешивания без видео";
            this.toolStripNotVideo.Click += new System.EventHandler(this.ToolStripNotVideo_Click);
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // date
            // 
            this.date.HeaderText = "Дата и время взвешивания";
            this.date.Name = "date";
            // 
            // contr
            // 
            this.contr.HeaderText = "Контрагент";
            this.contr.Name = "contr";
            // 
            // number
            // 
            this.number.HeaderText = "Номер авто";
            this.number.Name = "number";
            // 
            // model
            // 
            this.model.HeaderText = "Марка автомобиля";
            this.model.Name = "model";
            // 
            // material
            // 
            this.material.HeaderText = "Груз";
            this.material.Name = "material";
            // 
            // weightEmpty
            // 
            this.weightEmpty.HeaderText = "Вес пустого автомобиля, кг";
            this.weightEmpty.Name = "weightEmpty";
            // 
            // weightFull
            // 
            this.weightFull.HeaderText = "Вес автомобиля с грузом, кг";
            this.weightFull.Name = "weightFull";
            // 
            // weightClear
            // 
            this.weightClear.HeaderText = "Чистый вес груза, кг";
            this.weightClear.Name = "weightClear";
            // 
            // weightDoc
            // 
            this.weightDoc.HeaderText = "Вес по документам, кг";
            this.weightDoc.Name = "weightDoc";
            // 
            // volume
            // 
            this.volume.HeaderText = "Объем";
            this.volume.Name = "volume";
            // 
            // adj
            // 
            this.adj.HeaderText = "Корректировка";
            this.adj.Name = "adj";
            // 
            // scalesTara
            // 
            this.scalesTara.HeaderText = "Весы тара";
            this.scalesTara.Name = "scalesTara";
            // 
            // scalesBrutto
            // 
            this.scalesBrutto.HeaderText = "Весы брутто";
            this.scalesBrutto.Name = "scalesBrutto";
            // 
            // changed
            // 
            this.changed.HeaderText = "Изменено";
            this.changed.Name = "changed";
            // 
            // videoFile
            // 
            this.videoFile.HeaderText = "Видео";
            this.videoFile.Name = "videoFile";
            // 
            // Journal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 316);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bindingNavigator);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Journal";
            this.Text = "Журнал операций";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Journal_FormClosed);
            this.Load += new System.EventHandler(this.Journal_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.DateTimePicker dateTimeFrom;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DataGridView dataGridView;
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
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownPrint;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripBtnFinish;
        private System.Windows.Forms.ToolStripButton toolStripBtnNotFinish;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripVideo;
        private System.Windows.Forms.ToolStripButton toolStripNotVideo;
        private System.Windows.Forms.DateTimePicker dateTimeBefore;
        private System.Windows.Forms.Label lblBefore;
        private System.Windows.Forms.Label lblPost;
        private System.Windows.Forms.ComboBox cmbPost;
        private System.Windows.Forms.ToolStripMenuItem WeighingTalonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TTNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nakl1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nakl2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripUpdate;
        private System.Windows.Forms.ToolStripButton toolStripAdd;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ComboBox cmbScaleBrutto;
        private System.Windows.Forms.ComboBox cmbScalseTara;
        private System.Windows.Forms.CheckBox cScaleBrutto;
        private System.Windows.Forms.CheckBox cScaleTara;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn contr;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn model;
        private System.Windows.Forms.DataGridViewTextBoxColumn material;
        private System.Windows.Forms.DataGridViewTextBoxColumn weightEmpty;
        private System.Windows.Forms.DataGridViewTextBoxColumn weightFull;
        private System.Windows.Forms.DataGridViewTextBoxColumn weightClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn weightDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn adj;
        private System.Windows.Forms.DataGridViewTextBoxColumn scalesTara;
        private System.Windows.Forms.DataGridViewTextBoxColumn scalesBrutto;
        private System.Windows.Forms.DataGridViewTextBoxColumn changed;
        private System.Windows.Forms.DataGridViewTextBoxColumn videoFile;
    }
}