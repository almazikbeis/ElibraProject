using ELibra.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;

namespace ELibra
{
    public partial class Drivers : Form
    {
        private string[] headersText = {"Фамилия", "Имя", "Отчество", "№ уд.л." };
        private bool edit = false;

        private string editSurname;
        private string editName;
        private string editPatronimyc;

        public Drivers()
        {
            InitializeComponent();
        }

        private void Drivers_Load(object sender, EventArgs e)
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

        private void ToolStripCancel_Click(object sender, EventArgs e)
        {
            tLastName.Clear(); tLastName.Enabled = false;
            tFirstName.Clear(); tFirstName.Enabled = false;
            tPatronymic.Clear(); tPatronymic.Enabled = false;
            tID.Clear(); tID.Enabled = false;
            toolStripSave.Enabled = false;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
            if (dataGridView.Rows.Count != 0)
            {
                toolStripEdit.Enabled = true;
                toolStripDelete.Enabled = true;
            }
        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                if (tLastName.Text != "" || tFirstName.Text != "" || tPatronymic.Text != "")
                {
                    string surename = tLastName.Text.Replace(" ", "");
                    string name = tFirstName.Text.Replace(" ", "");
                    string patronimyc = tPatronymic.Text.Replace(" ", "");
                    var driversTable = from i in context.Drivers
                                       where i.name == name
                                       && i.surname == surename
                                       && i.patronimyc == patronimyc
                                       select i;
                    string udl = tID.Text;
                    DateTime updated = DateTime.Now;

                    #region Обновление и заполнение таблицы
                    var clientsCount = (from i in driversTable
                                        select i.id).Count();

                    if (clientsCount == 0)
                    {
                        if (edit)
                        {
                            var editDrivers = (from i in context.Drivers
                                               where i.name == editName
                                               && i.surname == editSurname
                                               && i.patronimyc == editPatronimyc
                                               select i).FirstOrDefault();

                            editDrivers.surname = surename;
                            editDrivers.name = name;
                            editDrivers.patronimyc = patronimyc;
                            editDrivers.udl = udl;
                            editDrivers.updated = updated;
                            context.SaveChanges();
                            RefreshTable();
                        }
                        else
                        {
                            DBModel.Models.Drivers newRow = new DBModel.Models.Drivers
                            {
                                surname = surename,
                                name = name,
                                patronimyc = patronimyc,
                                udl = udl,
                                updated = updated
                            };
                            context.Drivers.Add(newRow);
                            context.SaveChanges();
                            RefreshTable();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такой водитель уже существует");
                    }
                    tLastName.Enabled = false;
                    tFirstName.Enabled = false;
                    tPatronymic.Enabled = false;
                    tID.Enabled = false;
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
                    MessageBox.Show("Введите ФИО водителя");
                }
            }
            
        }

        private void ToolStripEdit_Click(object sender, EventArgs e)
        {
            edit = true;
            tLastName.Enabled = true;
            tFirstName.Enabled = true;
            tPatronymic.Enabled = true;
            tID.Enabled = true;
            tLastName.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            editSurname = tLastName.Text;
            tFirstName.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
            editName = tFirstName.Text;
            tPatronymic.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
            editPatronimyc = tPatronymic.Text;
            tID.Text = dataGridView.CurrentRow.Cells[3].Value == null ? "": dataGridView.CurrentRow.Cells[3].Value.ToString();
            toolStripSave.Enabled = true;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
            toolStripEdit.Enabled = false;
            toolStripDelete.Enabled = false;
        }

        private void ToolStripAdd_Click(object sender, EventArgs e)
        {
            edit = false;
            tLastName.Clear(); tLastName.Enabled = true;
            tFirstName.Clear(); tFirstName.Enabled = true;
            tPatronymic.Clear(); tPatronymic.Enabled = true;
            tID.Clear(); tID.Enabled = true;
            toolStripSave.Enabled = true;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
            toolStripEdit.Enabled = false;
            toolStripDelete.Enabled = false;
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string surenameDelete = dataGridView.CurrentRow.Cells[0].Value.ToString();
                string nameDelete = dataGridView.CurrentRow.Cells[1].Value.ToString();
                string patronimycDelete = dataGridView.CurrentRow.Cells[2].Value.ToString();
                var driversDelete = (from i in context.Drivers
                                     where i.surname == surenameDelete
                                     && i.name == nameDelete
                                     && i.patronimyc == patronimycDelete
                                     select i).FirstOrDefault();

                #region Удаление записи
                if (MessageBox.Show("Данная запись будет удалена. Вы хотите продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    context.Drivers.Remove(driversDelete);
                    context.SaveChanges();
                    RefreshTable();
                }

                toolStripSave.Enabled = false;
                toolStripCancel.Enabled = false;
                toolStripAdd.Enabled = true;
            
                #endregion
            }

        }

        private void Drivers_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmDrivers = null;
        }

        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var driversTable = (from i in context.Drivers
                                    select new
                                    {
                                        i.surname,
                                        i.name,
                                        i.patronimyc,
                                        i.udl,

                                    }).ToList();
                dataGridView.DataSource = driversTable;
            }
            
        }
    }
}
