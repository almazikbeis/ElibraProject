using ELibra.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;

namespace ELibra
{
    public partial class GetDriver : Form
    {

        private string[] headersText = {"Фамилия", "Имя", "Отчество" };
        private ListBox listDrivers;

        public GetDriver(ref ListBox listDrivers)
        {
            InitializeComponent();
            this.listDrivers = listDrivers;
        }

        private void GetDriver_Load(object sender, EventArgs e)
        {
            #region Начальное заполнение таблицы
            dataGridView.AutoGenerateColumns = true;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RefreshTable();
            for (int i = 0; i < dataGridView.Columns.Count; i++)
                dataGridView.Columns[i].HeaderText = headersText[i];
            #endregion
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    #region Добавление полей в список
                    string surname = (dataGridView.CurrentRow.Cells[0].Value  ?? "").ToString();
                    string name = (dataGridView.CurrentRow.Cells[1].Value ?? "").ToString();
                    string patronimyc = (dataGridView.CurrentRow.Cells[2].Value ?? "").ToString();
                    if (!listDrivers.Items.Contains(surname + " " + name + " " + patronimyc))
                    {
                        listDrivers.Items.Add(surname + " " + name + " " + patronimyc);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Водитель уже в списке", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion
                }
                
            }
        }

        private void TLastName_TextChanged(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                bindingSource.DataSource = (from i in context.Drivers
                                            where i.surname.Contains(tLastName.Text)
                                            select new
                                            {
                                                i.surname,
                                                i.name,
                                                i.patronimyc
                                            }).ToList();
            }
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedCells.Count != 0)
                dataGridView.SelectedCells[0].OwningRow.Selected = true;
        }

        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                dataGridView.DataSource = (from i in context.Drivers
                                          select new
                                          {
                                              i.surname,
                                              i.name,
                                              i.patronimyc,
                                          }).ToList();

            }
        }


    }
}
