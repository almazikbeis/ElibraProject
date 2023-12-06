using ELibra.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;

namespace ELibra
{
    public partial class CounterParties : Form
    {
        private string[] headersText = {"Наименование", "БИН", "Адрес", "Телефон", "Комментарии", "Корректировка", "Начало", "Конец" };
        private bool edit = false;
        

        public CounterParties()
        {
            InitializeComponent();
        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            if (tName.Text != "")
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    string name = tName.Text;
                    var clientsTable = from i in context.Clients
                                         where i.name == name
                                         select i;

                    #region Заполнение и обновление таблицы
                    string RNN = tRNN.Text;
                    string address = tAddress.Text;
                    string phone = tPhone.Text;
                    string desc = tDesc.Text;
                    string factor = numFactor.Value.ToString();
                    DateTime startDate = dateTimeStart.Value.Date;
                    DateTime endDate = dateTimeEnd.Value.Date;   
                    DateTime updated = DateTime.Now;
                    var clientsCount = (from i in clientsTable
                                          select i.id).Count();
                    if (edit)
                    {
                        clientsTable.FirstOrDefault().name = name;
                        clientsTable.FirstOrDefault().RNN = RNN;
                        clientsTable.FirstOrDefault().address = address;
                        clientsTable.FirstOrDefault().phone = phone;
                        clientsTable.FirstOrDefault().description = desc;
                        clientsTable.FirstOrDefault().factor = Convert.ToDouble(factor);
                        //clientsTable.FirstOrDefault().startDate = startDate;
                        //clientsTable.FirstOrDefault().endDate = endDate;
                        clientsTable.FirstOrDefault().updated = updated;
                        context.SaveChanges();
                        RefreshTable();
                    }
                    else
                    {
                        if (clientsCount == 0)
                        {
                            double f = double.Parse(factor);
                            DBModel.Models.Clients newRow = new DBModel.Models.Clients
                            {
                                name = name,
                                RNN = RNN,
                                address = address,
                                phone = phone,
                                description = desc,
                                factor = f,
                                // TODO
                                startDate = startDate,
                                endDate = endDate,
                                updated = updated
                            };
                            context.Clients.Add(newRow);
                            context.SaveChanges();
                            RefreshTable();
                        }
                        else
                            MessageBox.Show("Контрагент с таким именем уже существует");
                    }
                    tName.Enabled = false;
                    tRNN.Enabled = false;
                    tAddress.Enabled = false;
                    tPhone.Enabled = false;
                    tDesc.Enabled = false;
                    numFactor.Enabled = false;
                    dateTimeStart.Enabled = false;
                    dateTimeEnd.Enabled = false;
                    toolStripSave.Enabled = false;
                    toolStripCancel.Enabled = false;
                    toolStripAdd.Enabled = true;
                    toolStripEdit.Enabled = true;
                    #endregion
                }

            }
            else
            {
                MessageBox.Show("Введите наименование контрагента");
            }
        }

        private void CounterParties_Load(object sender, EventArgs e)
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
                //toolStripDelete.Enabled = true;
            }
            #endregion

        }

        private void ToolStripAdd_Click(object sender, EventArgs e)
        {
            edit = false;
            tName.Clear(); tName.Enabled = true;
            tRNN.Clear(); tRNN.Enabled = true;
            tAddress.Clear(); tAddress.Enabled = true;
            tPhone.Clear(); tPhone.Enabled = true;
            tDesc.Clear(); tDesc.Enabled = true;
            numFactor.Value = 0; numFactor.Enabled = true;
            dateTimeStart.Enabled = true;
            dateTimeEnd.Enabled = true;
            toolStripSave.Enabled = true;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
            toolStripEdit.Enabled = false;
        }

        private void ToolStripEdit_Click(object sender, EventArgs e)
        {
            edit = true;
            tName.Enabled = false;
            tRNN.Enabled = true;
            tAddress.Enabled = true;
            tPhone.Enabled = true;
            tDesc.Enabled = true;
            numFactor.Enabled = true;
            dateTimeStart.Enabled = true;
            dateTimeEnd.Enabled = true;
            tName.Text = dataGridView.CurrentRow.Cells[0].Value != null ? dataGridView.CurrentRow.Cells[0].Value.ToString() : "";
            tRNN.Text = dataGridView.CurrentRow.Cells[1].Value != null ? dataGridView.CurrentRow.Cells[1].Value.ToString() :  "";
            tAddress.Text = dataGridView.CurrentRow.Cells[2].Value != null ? dataGridView.CurrentRow.Cells[2].Value.ToString() : "";
            tPhone.Text = dataGridView.CurrentRow.Cells[3].Value != null ? dataGridView.CurrentRow.Cells[3].Value.ToString() : "";
            tDesc.Text = dataGridView.CurrentRow.Cells[4].Value != null ? dataGridView.CurrentRow.Cells[4].Value.ToString() : "";
            numFactor.Value = dataGridView.CurrentRow.Cells[5].Value != null ? decimal.Parse(dataGridView.CurrentRow.Cells[5].Value.ToString()) : 0;
            dateTimeStart.Value = DateTime.Parse(dataGridView.CurrentRow.Cells[6].Value.ToString());
            dateTimeEnd.Value = DateTime.Parse(dataGridView.CurrentRow.Cells[7].Value.ToString());
            toolStripSave.Enabled = true;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
            toolStripEdit.Enabled = false;
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var clientsDelete = (from i in context.Clients
                                      where i.name == dataGridView.CurrentRow.Cells[0].Value.ToString()
                                      select i).FirstOrDefault();

                #region Удаление записи
                if (MessageBox.Show("Данная запись будет удалена. Вы хотите продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    context.Clients.Remove(clientsDelete);
                    context.SaveChanges();
                    RefreshTable();
                }
                toolStripSave.Enabled = false;
                toolStripCancel.Enabled = false;
                toolStripAdd.Enabled = true;
                if (dataGridView.Rows.Count != 0)
                {
                    toolStripEdit.Enabled = true;
                }
                #endregion
            }

        }

        private void ToolStripCancel_Click(object sender, EventArgs e)
        {
            tName.Clear(); tName.Enabled = false;
            tRNN.Clear(); tRNN.Enabled = false;
            tAddress.Clear(); tAddress.Enabled = false;
            tPhone.Clear(); tPhone.Enabled = false;
            tDesc.Clear(); tDesc.Enabled = false;
            numFactor.Value = 0; numFactor.Enabled = false;
            dateTimeStart.Enabled = false;
            dateTimeEnd.Enabled = false;
            toolStripSave.Enabled = false;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
            if (dataGridView.Rows.Count != 0)
            {
                toolStripEdit.Enabled = true;
            }
        }

        private void CounterParties_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmCounterParties = null;
        }

        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var clientsTable = (from i in context.Clients
                                      select new
                                      {
                                          i.name,
                                          i.RNN,
                                          i.address,
                                          i.phone,
                                          i.description,
                                          i.factor,
                                          i.startDate,
                                          i.endDate
                                      }).ToList();
                dataGridView.DataSource = clientsTable;
            }
        }

        private void numFactor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                if (numFactor.Text.Contains(',') || numFactor.Text.Contains('.') || numFactor.Text.Contains('-'))
                {
                    e.Handled = true;
                }
            }
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                if (numFactor.Text.Contains('.') || numFactor.Text.Contains(','))
                {
                    e.Handled = true;
                }
            }
        }
    }
}