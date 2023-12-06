using ELibra.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;

namespace ELibra
{
    public partial class Stocks : Form
    {
        private string[] headersText = { "Наименование" };
        private bool edit = false;
        private string editName;

        public Stocks()
        {
            InitializeComponent();

        }

        private void Stocks_Load(object sender, EventArgs e)
        { 
            #region Начальное заполнение таблицы
            this.WindowState = FormWindowState.Maximized;
            dataGridView.AutoGenerateColumns = true;
            RefreshTable();
            for (int i = 0; i < dataGridView.Columns.Count; i++)
                dataGridView.Columns[i].HeaderText = headersText[i];

            toolStripSave.Enabled = false;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
            if (dataGridView.Rows.Count != 0)
            {
                toolStripEdit.Enabled = true;
                toolStripDelete.Enabled = true;
            }
            #endregion
        }

        private void ToolStripAdd_Click(object sender, EventArgs e)
        {
            edit = false;
            tName.Clear(); tName.Enabled = true;
            toolStripSave.Enabled = true;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
            toolStripEdit.Enabled = false;
            toolStripDelete.Enabled = false;
        }

        private void ToolStripEdit_Click(object sender, EventArgs e)
        {
            edit = true;
            tName.Enabled = true;
            tName.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            editName = tName.Text;
            toolStripSave.Enabled = true;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
            toolStripEdit.Enabled = false;
            toolStripDelete.Enabled = false;
        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                if (tName.Text != "")
                {
                    string name = tName.Text;

                    #region Заполнение и обновление таблицы
                    DateTime updated = DateTime.Now;
                
                    if (edit)
                    {
                        var stocksTable = (from i in context.Storages
                                          where i.name == editName
                                          select i).FirstOrDefault();
                        stocksTable.name = name;
                        stocksTable.updated = updated;
                        context.SaveChanges();
                        RefreshTable();
                    }
                    else
                    {
                        var stocksTable = from i in context.Storages
                                          where i.name == name
                                          select i;
                        if (!stocksTable.Any())
                        {
                            DBModel.Models.Storages newRow = new DBModel.Models.Storages
                            {
                                name = name,
                                updated = updated
                            };
                            context.Storages.Add(newRow);
                            context.SaveChanges();
                            RefreshTable();
                        }
                        else
                        {
                            MessageBox.Show("Склад с таким именем уже существует");
                        }
                    }
                
                    RefreshTable();
                    tName.Enabled = false;
                    toolStripSave.Enabled = false;
                    toolStripCancel.Enabled = false;
                    toolStripAdd.Enabled = true;
                    if (dataGridView.Rows.Count != 0)
                    {
                        toolStripEdit.Enabled = true;
                        toolStripDelete.Enabled = true;
                    }
                    #endregion

                }
                else
                {
                    MessageBox.Show("Введите наименования склада");
                }
            }
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string dName = dataGridView.CurrentRow.Cells[0].Value.ToString();
                var storagesDelete = (from i in context.Storages
                                      where i.name == dName
                                      select i).FirstOrDefault();

                #region Удаление записи
                if (MessageBox.Show("Данная запись будет удалена. Вы хотите продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    context.Storages.Remove(storagesDelete);
                    context.SaveChanges();
                    RefreshTable();
                    toolStripSave.Enabled = false;
                    toolStripCancel.Enabled = false;
                    toolStripAdd.Enabled = true;
                    if (dataGridView.Rows.Count != 0)
                    {
                        toolStripEdit.Enabled = true;
                        toolStripDelete.Enabled = true;
                    }
                }
                #endregion
            }

        }

        private void ToolStripCancel_Click(object sender, EventArgs e)
        {
            tName.Clear();
            tName.Enabled = false;
            toolStripSave.Enabled = false;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
            if (dataGridView.Rows.Count != 0)
            {
                toolStripEdit.Enabled = true;
                toolStripDelete.Enabled = true;
            }
        }

        private void Stocks_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmStocks = null;
        }

        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var storagesTable = (from i in context.Storages
                                     select new { i.name }).ToList();


                bindingSource.DataSource = storagesTable;
            }
        }
    }
}
