using ELibra.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ELibra.DBModel;
using System.Data.Entity.SqlServer;
using System.Drawing.Imaging;

namespace ELibra
{
    public partial class Cars : Form
    {
        private string[] headersText = { "Номер", "Марка", "Вес пустого автомобиля, кг", "Контрагент", "Водитель" };
        
        private class TableObject
        {
            public string number { get; set; }
            public string carMark { get; set; }
            public double weight { get; set; }
            public string clientName { get; set; }
            public string driverId { get; set; }

        }

        private bool edit = false;
        private string editNumber;

        public Cars()
        {
            InitializeComponent();
        }

        private void Cars_Load(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                this.WindowState = FormWindowState.Maximized;
                dataGridView.AutoGenerateColumns = true;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                #region Заполнение данных о контрагентах
                cbCounterParties.Items.AddRange((from i in context.Clients
                                                 orderby i.name
                                                 select i.name).ToArray());
                if (cbCounterParties.Items.Count != 0)
                    cbCounterParties.SelectedIndex = 0;
                #endregion
               
                #region Заполнение данных о марках
                cbMark.Items.AddRange((from i in context.CarMarks
                                                 select i.name).ToArray());
                #endregion

                #region Настройка контролов
                toolStripSave.Enabled = false;
                toolStripCancel.Enabled = false;
                toolStripEdit.Enabled = false;
                toolStripAdd.Enabled = true;
                
                RefreshTable();
                

                if (dataGridView.Rows.Count != 0)
                {
                    toolStripEdit.Enabled = true;
                    toolStripDelete.Enabled = true;
                }
                #endregion
            }

        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            if (tNumber.Text != "" && cbMark.Text != "" && cbCounterParties.Text != "")
            {
                using (DataBaseContext context  = new DataBaseContext())
                {
                    #region Создание переменных с данными о машине
                    var newMark = from i in context.CarMarks
                                  where i.name == cbMark.Text
                                  select i.id;

                    string number = tNumber.Text.Replace(" ", "");
                    int markId = newMark.FirstOrDefault();
                    double weight = string.IsNullOrEmpty(tWeight.Text) ? 0 : Double.Parse(tWeight.Text);
                    int clientId = string.IsNullOrEmpty(cbCounterParties.Text) ? default : (from i in context.Clients
                                                                                            where i.name == cbCounterParties.Text
                                                                                            select i.id).FirstOrDefault();
                    #endregion

                    #region Создание новой марки если её не существует
                    if (!newMark.Any())
                    {
                        DBModel.Models.CarMarks newRow = new DBModel.Models.CarMarks
                        {
                            name = cbMark.Text,
                            updated = DateTime.Now
                        };
                        context.CarMarks.Add(newRow);
                        context.SaveChanges();
                        markId = newRow.id;
                    }
                    #endregion

                    #region Поиск всех выбранных водителей
                    List<int> driverId = new List<int>();
                    for (int i = 0; i < listDrivers.Items.Count; i++)
                    {
                        string[] driver = listDrivers.Items[i].ToString().Split(' ');
                        string[] newdrivers = driver.Where(n => !string.IsNullOrEmpty(n)).ToArray();
                        string surname = newdrivers[0];
                        string name = newdrivers.Count() > 1 ? newdrivers[1] : "";
                        string patronimyc = newdrivers.Count() > 2 ? newdrivers[2] : "";
                        driverId.Add((from j in context.Drivers
                                      where j.surname == surname &&
                                            (j.name == name || j.name == null) &&
                                            (j.patronimyc == patronimyc || j.patronimyc == null) 
                                      select j.id).FirstOrDefault());
                    }
                    #endregion

                    DateTime updated = DateTime.Now;
                    if (edit)
                    {
                        #region Изменение существующеё машины
                        DBModel.Models.Cars car = (from i in context.Cars where i.number == editNumber select i).FirstOrDefault();
                        car.number = number;
                        string carMark = cbMark.Text;
                        car.markId = (from i in context.CarMarks where i.name == carMark select i.id).FirstOrDefault();
                        car.weight = weight;
                        car.clientId = clientId;
                        car.driverId = string.Join(",", driverId);
                        car.updated = updated;
                        context.SaveChanges();
                        RefreshTable();
                        #endregion
                    }
                    else
                    {
                        if (!(from i in context.Cars where i.number == number select i).Any())
                        {
                            #region Добавление новой машины
                            DBModel.Models.Cars newRow = new DBModel.Models.Cars
                            {
                                number = number,
                                markId = markId,
                                weight = weight,
                                clientId = clientId,
                                driverId = string.Join(",", driverId),
                                updated = updated
                            };
                            context.Cars.Add(newRow);
                            context.SaveChanges();
                            RefreshTable();
                            #endregion
                        }
                        else
                            MessageBox.Show("Машина с таким номером уже существует");
                    }

                    #region Обновление контролов
                    toolStripSave.Enabled = false;
                    toolStripCancel.Enabled = false;
                    toolStripAdd.Enabled = true;
                    toolStripEdit.Enabled = true;
                    toolStripDelete.Enabled = true;
                    tNumber.Clear(); tNumber.Enabled = false;
                    tWeight.Clear();
                    cbMark.Items.Clear();
                    cbMark.Text = "";
                    listDrivers.Items.Clear();
                    cbMark.Enabled = false;
                    cbCounterParties.Enabled = false;
                    btnAddDriver.Enabled = false;
                    btnRemoveDriver.Enabled = false;
                    btnGetWeight.Visible = false;

                    cbMark.Items.AddRange((from i in context.CarMarks select i.name).ToArray());

                    #endregion
                    //qr_picture.Image = QrHelper.encode(number);
                }

            }
            else
            {
                if (tNumber.Text == "")
                    MessageBox.Show("Введите номер машины");
                else if (cbMark.Text == "")
                    MessageBox.Show("Введите или выберите марку машины");
                else if (cbCounterParties.Text == "")
                    MessageBox.Show("Выберети контрагента");
            }
        }

        private void BtnGetWeight_Click(object sender, EventArgs e)
        {
            tWeight.Text = (this.MdiParent as Main).scalesList.Find(x => x.Active == true).Libra.Weight.ToString();
        }

        private void BtnAddDriver_Click(object sender, EventArgs e)
        {
            GetDriver frmGetDriver = new GetDriver(ref listDrivers);
            frmGetDriver.StartPosition = FormStartPosition.CenterParent;
            frmGetDriver.ShowDialog();
        }

        private void ToolStripCancel_Click(object sender, EventArgs e)
        {
            #region Обнуление контролов
            tNumber.Clear(); tNumber.Enabled = false;
            tWeight.Clear();
            cbMark.Enabled = false;
            cbCounterParties.Enabled = false;
            btnAddDriver.Enabled = false;
            btnRemoveDriver.Enabled = false;
            btnGetWeight.Visible = false;
            listDrivers.Items.Clear();
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

        private void ToolStripEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    edit = true;

                    #region настройка контролов
                    tNumber.Enabled = true; 
                    tWeight.Enabled = false;
                    cbMark.Enabled = true;
                    cbCounterParties.Enabled = true;
                    btnAddDriver.Enabled = true;
                    btnRemoveDriver.Enabled = true;
                    btnGetWeight.Visible = true;


                    #endregion

                    #region Заполнение полей данными
                    tNumber.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
                    editNumber = tNumber.Text;
                    cbMark.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
                    tWeight.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
                    cbCounterParties.Text = (dataGridView.CurrentRow.Cells[3].Value ?? "").ToString();
                    var driversId = (from i in context.Cars
                                     where i.number == editNumber
                                     select i.driverId).FirstOrDefault();

                    var drivers = (from i in context.Drivers
                                  select i).AsEnumerable();
                    foreach (var driver in driversId.Split(','))
                    {
                        if (!string.IsNullOrEmpty(driver))
                        {
                            int tempId = int.Parse(driver);
                            if (tempId != 0)
                            {
                                var temp = (from i in drivers
                                           where i.id == tempId
                                           select new
                                           {
                                               i.name,
                                               i.surname,
                                               i.patronimyc
                                           }).FirstOrDefault();

                                listDrivers.Items.Add($"{temp.surname} {temp.name} {temp.patronimyc}");
                            }
                        }
                    }                       
                    #endregion

                    #region Обновление контролов
                    toolStripSave.Enabled = true;
                    toolStripCancel.Enabled = true;
                    toolStripAdd.Enabled = false;
                    toolStripEdit.Enabled = false;
                    toolStripDelete.Enabled = false;
                    #endregion

                    //qr_picture.Image = QrHelper.encode(editNumber);

                }
            }
        }

        private void ToolStripAdd_Click(object sender, EventArgs e)
        {
            edit = false;

            #region Обновление контролов
            tNumber.Clear(); tNumber.Enabled = true;
            tWeight.Clear();
            listDrivers.Items.Clear();
            cbMark.Enabled = true;
            cbCounterParties.Enabled = true;
            btnAddDriver.Enabled = true;
            btnRemoveDriver.Enabled = true;
            btnGetWeight.Visible = true;
            toolStripSave.Enabled = true;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
            toolStripEdit.Enabled = false;
            toolStripDelete.Enabled = false;


            #endregion
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string num = dataGridView.CurrentRow.Cells[0].Value.ToString();
                var carsDelete = (from i in context.Cars
                                      where i.number == num
                                      select i).FirstOrDefault();

                if (MessageBox.Show("Данная запись будет удалена. Вы хотите продолжить?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    context.Cars.Remove(carsDelete);
                    context.SaveChanges();
                    RefreshTable();
                }
                #region Обновление контролов
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

        private void Cars_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmCars = null;
        }

        private void BtnRemoveDriver_Click(object sender, EventArgs e)
        {
            if (listDrivers.SelectedIndex != -1)
                listDrivers.Items.RemoveAt(listDrivers.SelectedIndex);
        }

        private void RefreshTable()
        {
            
            using (DataBaseContext context = new DataBaseContext())
            {
                var carsTable = (from i in context.Cars
                                 join j in context.CarMarks on i.markId equals j.id
                                 join q in context.Clients on i.clientId equals q.id
                                 select new
                                 {

                                     number = i.number,
                                     carMark = j.name,
                                     weight = i.weight,
                                     client = q.name ,
                                     driverId = i.driverId
                                 }).ToList();


                var temp = (from i in carsTable
                           select new TableObject
                           {
                               number = i.number,
                               carMark = i.carMark,
                               weight = i.weight??0.0,
                               clientName = i.client,
                               driverId = (from j in context.Drivers.ToList() 
                                           where j.id.ToString() == i.driverId.Split(',')[0] 
                                           select j.surname).FirstOrDefault() + " " + 
                                               (from j in context.Drivers.ToList() 
                                                where j.id.ToString() == i.driverId.Split(',')[0] 
                                                select j.name).FirstOrDefault() + " " + 
                                                   (from j in context.Drivers.ToList() 
                                                    where j.id.ToString() == i.driverId.Split(',')[0] 
                                                    select j.patronimyc).FirstOrDefault()
                           }).ToList();


                bindingSource.DataSource = temp;
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                    dataGridView.Columns[i].HeaderText = headersText[i];
            }
        }


        private void TWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void PrintQr_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = saveFileDialog1.FileName;
                string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                ImageFormat format = ImageFormat.Png;
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                }
            }
            catch { }
            
        }

        private void dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            string number = "";
            try
            {
                number = dataGridView.CurrentRow.Cells[0].Value.ToString() ?? "";
            }
            catch {}
             
            if (number != "")
            {
                //qr_picture.Image = QrHelper.encode(number);
            }
        }
    }
}
