using ELibra.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;

namespace ELibra
{
    public partial class Consignees : Form
    {
        private string[] headersText = { "Наименованиие" };
        private bool edit = false;       
        private string editName;

        public Consignees()
        {
            InitializeComponent();
        }

        private void Consignees_Load(object sender, EventArgs e)
        {

            #region Начальное заполнение таблицы
            this.WindowState = FormWindowState.Maximized;
            dataGridView.AutoGenerateColumns = true;
            RefreshTable();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                    var consigneesTable = from i in context.Consignee
                                          where i.name == name
                                          select i;

                    #region Заполнение и обновление таблицы
                    var consigneeCount = (from i in consigneesTable
                                          select i.id).Count();
                    if (consigneeCount == 0)
                    {
                        if (edit)
                        {
                            var consigneesEdit = (from i in context.Consignee
                                                 where i.name == editName
                                                 select i).FirstOrDefault();
                            consigneesEdit.name = name;
                            consigneesEdit.updated = updated;
                            context.SaveChanges();
                            RefreshTable();
                        }
                        else
                        {
                            DBModel.Models.Consignee newRow = new DBModel.Models.Consignee
                            {
                                name = name,
                                updated = updated
                            };
                            context.Consignee.Add(newRow);
                            context.SaveChanges();
                            RefreshTable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Грузополучатель с таким именем уже существует");
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
                MessageBox.Show("Введите наименование грузополучателя");
            }
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string nameDelete = dataGridView.CurrentRow.Cells[0].Value.ToString();
                var consigneeDelete = (from i in context.Consignee
                                      where i.name == nameDelete
                                       select i).FirstOrDefault();

                #region Удаление записи
                if (MessageBox.Show("Данная запись будет удалена. Вы хотите продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    context.Consignee.Remove(consigneeDelete);
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

        private void Consignees_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmConsignees = null;
        }

        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var consigneesTable = (from i in context.Consignee
                                      select new { i.name }).ToList();
                                  
                dataGridView.DataSource = consigneesTable;
            }
        }
    }
}
