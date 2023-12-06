using ELibra.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;

namespace ELibra
{
    public partial class Carriers : Form
    {
        private string[] headersText = {"Наименованиие" };
        private bool edit = false;

        private string editName;

        public Carriers()
        {
            InitializeComponent();
        }

        private void Carriers_Load(object sender, EventArgs e)
        {
            #region Начальное заполнение таблицы
            this.WindowState = FormWindowState.Maximized;
            dataGridView.AutoGenerateColumns = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            if (tName.Text != "")
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    string name = tName.Text;
                    DateTime updated = DateTime.Now;
                    var carriersTable = from i in context.Carriers
                                     where i.name == name
                                     select i;

                    #region Заполнение и обновление таблицы
                    var carriersCount = (from i in carriersTable
                                         select i.name).Count();
                    if (carriersCount == 0)
                    {
                        if (edit)
                        {
                            var carriersEdit = (from i in context.Carriers
                                               where i.name == editName
                                               select i).FirstOrDefault();
                            carriersEdit.name = name;
                            carriersEdit.updated = updated;
                            context.SaveChanges();
                            RefreshTable();
                        }
                        else
                        {
                            DBModel.Models.Carriers newRow = new DBModel.Models.Carriers
                            {
                                name = name,
                                updated = updated
                            };
                            context.Carriers.Add(newRow);
                            context.SaveChanges();
                            RefreshTable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Перевозчик с таким именем уже существует");
                    }

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

            }
            else
            {
                MessageBox.Show("Укажите наименование первозчика");
            }
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string carriersName = dataGridView.CurrentRow.Cells[0].Value.ToString();
                var carriersDelete = (from i in context.Carriers
                                      where i.name == carriersName
                                      select i).FirstOrDefault();

                #region Удаление таблицы
                if (MessageBox.Show("Данная запись будет удалена. Вы хотите продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    context.Carriers.Remove(carriersDelete);
                    context.SaveChanges();
                    RefreshTable();
                }            
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

        }

        private void ToolStripCancel_Click(object sender, EventArgs e)
        {
            tName.Clear(); tName.Enabled = false;
            toolStripSave.Enabled = false;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
            if (dataGridView.Rows.Count != 0)
            {
                toolStripEdit.Enabled = true;
                toolStripDelete.Enabled = true;
            }
        }

        private void Carriers_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmCarriers = null;
        }

        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var carriersTable = (from i in context.Carriers
                                  select new { i.name }).ToList();
                dataGridView.DataSource = carriersTable;
            }
        }
    }
}
