using ELibra.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;
using System.Text.RegularExpressions;

namespace ELibra
{
    public partial class Cargos : Form
    {
        private string[] headersText = {"Наименованиие", "Цена за тонну" };
        private bool edit = false;
        Regex regex = new Regex(@"[\d.,]");
        
        private string editName;

        public Cargos()
        {
            InitializeComponent();
        }

        private void Cargos_Load(object sender, EventArgs e)
        {
            #region Начальное заполнение таблицы
            this.WindowState = FormWindowState.Maximized;
            dataGridView.AutoGenerateColumns = true;
            RefreshTable();
            for (int i = 0; i < dataGridView.Columns.Count; i++)
                dataGridView.Columns[i].HeaderText = headersText[i];
            tName.Clear(); tName.Enabled = false;
            tCost.Clear(); tCost.Enabled = false;
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
            tCost.Clear(); tCost.Enabled = true;
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
            tCost.Enabled = true;
            tName.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            editName = tName.Text;
            tCost.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
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
                    var goodsTable = from i in context.Goods
                                     where i.name == editName
                                     select i;

                    #region Заполнение и обновление таблицы
                    string price = tCost.Text != "" ? tCost.Text.Replace('.', ',') : "0";
                    var goodsCount = goodsTable.Count();
                    if (edit)
                    {
                        if (goodsCount == 0 || goodsTable.FirstOrDefault().name == editName)
                        {
                            goodsTable.FirstOrDefault().name = name;
                            goodsTable.FirstOrDefault().price = double.Parse(price);
                            goodsTable.FirstOrDefault().updated = updated;
                            context.SaveChanges();
                            RefreshTable();
                        }
                        else
                        {
                            MessageBox.Show("Груз с таким именем уже существует");
                        }
                    }
                    else
                    {
                        if (goodsCount == 0)
                        {
                            DBModel.Models.Goods newRow = new DBModel.Models.Goods
                            {
                                name = name,
                                price = double.Parse(price),
                                updated = updated
                            };
                            context.Goods.Add(newRow);
                            context.SaveChanges();
                            RefreshTable();
                        }
                        else
                        {
                            MessageBox.Show("Груз с таким именем уже существует");
                        }
                    }

                    tName.Enabled = false;
                    tCost.Enabled = false;
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
                MessageBox.Show("Укажите наименование груза");
            }
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string goodsName = dataGridView.CurrentRow.Cells[0].Value.ToString();
                var goodsDelete = (from i in context.Goods
                                  where i.name == goodsName
                                   select i).FirstOrDefault();

                #region Удаление записи
                if (MessageBox.Show("Данная запись будет удалена. Вы хотите продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    context.Goods.Remove(goodsDelete);
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
            tCost.Clear(); tCost.Enabled = false;
            toolStripSave.Enabled = false;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
            if (dataGridView.Rows.Count != 0)
            {
                toolStripEdit.Enabled = true;
                toolStripDelete.Enabled = true;
            }
        }

        private void Cargos_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmCargos = null;
        }

        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var cargosTable = (from i in context.Goods
                                  select new
                                  {
                                      i.name,
                                      i.price
                                  }).ToList();
                dataGridView.DataSource = cargosTable;
            }
        }


        private void TCost_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == ',') && !(e.KeyChar == '.');
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                if (tCost.Text.Contains('.') || tCost.Text.Contains(','))
                {
                    e.Handled = true;
                }
            }
         
        }
    }
}
