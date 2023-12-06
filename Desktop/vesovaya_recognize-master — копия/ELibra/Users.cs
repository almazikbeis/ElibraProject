using ELibra.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ELibra.DBModel;
using System.Security.Cryptography;

namespace ELibra
{
    public partial class Users : Form
    {
        //DataBaseContext context = new DataBaseContext();

        private string[] headersText = { "Имя пользователя (логин)", "Ф.И.О.", "Должность", "Роль", "Удалён" };
        private string[] roleArr;
        private bool edit = false;
        private bool correctPass = false;

        private Regex regex = new Regex(@"[!,@,#,$,%,^,&,*,?,_,~]");
        private Regex regex1 = new Regex(@"[A-Za-z0-9]");
        private string editUsername;

        public Users()
        {
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
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
                var rolsList = (from i in context.Rols
                                select i.name).ToArray();
                cbRole.Items.AddRange(rolsList);
                var license = (from i in context.Settings
                               where i.id == 1
                               select i.licenseNumber).FirstOrDefault();
                if (license[2] == '0')
                    cbRole.Items.RemoveAt(2);
                cbRole.SelectedIndex = 0;
                #endregion
            }

        }

        private void ToolStripAdd_Click(object sender, EventArgs e)
        {
            edit = false;
            tName.Clear(); tName.Enabled = true;
            tFIO.Clear(); tFIO.Enabled = true;
            tPosition.Clear(); tPosition.Enabled = true;
            cbRole.Enabled = true;
            tPass.Clear(); tPass.Enabled = true;
            tRepeatPass.Clear(); tRepeatPass.Enabled = true;
            cBoxDeleted.Checked = false; cBoxDeleted.Enabled = true;
            toolStripSave.Enabled = true;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
            toolStripEdit.Enabled = false;
        }

        private void ToolStripEdit_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                #region Редактирование записей
                edit = true;
                cBoxDeleted.Enabled = true;
                tPass.Enabled = true;
                tRepeatPass.Enabled = true;
                cbRole.Enabled = true;
                tFIO.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
                tName.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
                editUsername = tName.Text;
                //var password = (from i in context.Users
                //                where i.username == editUsername
                //                select i.password).FirstOrDefault();
                //tPass.Text = password;
                tRepeatPass.Text = "";
                cbRole.SelectedIndex = cbRole.Items.IndexOf(dataGridView.CurrentRow.Cells[3].Value.ToString());
                tPosition.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
                if (dataGridView.CurrentRow.Cells[4].Value.ToString() == "Да")
                    cBoxDeleted.Checked = true;
                else
                    cBoxDeleted.Checked = false;
                toolStripSave.Enabled = true;
                toolStripCancel.Enabled = true;
                toolStripAdd.Enabled = false;
                toolStripEdit.Enabled = false;
                #endregion
            }

        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            if (tName.Text != "" && (cbRole.Text != "Администратор" ? tFIO.Text != "" : true))
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    #region Сохранение изменений
                    if (correctPass)
                    {
                        string username = tName.Text;
                        string FIO = tFIO.Text;
                        string position = tPosition.Text;
                        string selectedUser = cbRole.SelectedItem.ToString();
                        var rolsId = (from i in context.Rols
                                      where i.name == selectedUser
                                      select i.id).FirstOrDefault();
                        //string role = dbHelper.ExecuteField("SELECT * FROM Rols WHERE name='" + cbRole.SelectedItem.ToString() + "'", "id");
                        string pass = tPass.Text;
                        string delete = cBoxDeleted.Checked ? "Да" : "Нет";
                        DateTime updated = DateTime.Now;
                        if (edit)
                        {
                            var usersTable = (from i in context.Users
                                              where i.username == editUsername
                                              select i).FirstOrDefault();
                            usersTable.FIO = FIO;
                            usersTable.username = username;
                            if (pass != "")
                            {
                                usersTable.password = Classes.Update.UsersCyphering.Hash(pass);
                            }
                            usersTable.roleId = rolsId;
                            usersTable.post = position;
                            usersTable.deleted = delete;
                            usersTable.updated = updated;
                            context.SaveChanges();
                            RefreshTable();
                        }
                        else
                        {
                            var usersTable = from i in context.Users
                                             where i.username == username
                                             select i;
                            var usersCount = (from i in usersTable
                                              select i.id).Count();
                            if (usersCount == 0)
                            {
                                DBModel.Models.Users newRow = new DBModel.Models.Users
                                {
                                    FIO = FIO,
                                    username = username,
                                    password = Classes.Update.UsersCyphering.Hash(pass),
                                    roleId = rolsId,
                                    post = position,
                                    deleted = delete,
                                    updated = updated
                                };
                                context.Users.Add(newRow);
                                context.SaveChanges();
                                RefreshTable();
                            }
                            else
                                MessageBox.Show("Пользователь с таким логином уже существует");
                        }
                        tName.Enabled = false;
                        tFIO.Enabled = false;
                        tPosition.Enabled = false;
                        cbRole.Enabled = false;
                        cBoxDeleted.Enabled = false;
                        tPass.Enabled = false;
                        tRepeatPass.Enabled = false;
                        toolStripSave.Enabled = false;
                        toolStripCancel.Enabled = false;
                        toolStripAdd.Enabled = true;
                        toolStripEdit.Enabled = true;
                    }
                    #endregion
                }
            }
            else
            {
                MessageBox.Show("Введите логин и ФИО");
            }
        }


        private void ToolStripCancel_Click(object sender, EventArgs e)
        {
            tName.Clear(); tName.Enabled = false;
            tFIO.Clear(); tFIO.Enabled = false;
            tPosition.Clear(); tPosition.Enabled = false;
            cbRole.Enabled = false;
            tPass.Clear(); tPass.Enabled = false;
            tRepeatPass.Clear(); tRepeatPass.Enabled = false;
            cBoxDeleted.Checked = false; cBoxDeleted.Enabled = false;
            toolStripSave.Enabled = false;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
            toolStripEdit.Enabled = true;
        }

        private void TPass_TextChanged(object sender, EventArgs e)
        {
            #region Проверка корректности пароля
            bool a = tPass.Text.Length < 6;
            bool b = !regex.IsMatch(tPass.Text);
            bool c = !regex1.IsMatch(tPass.Text);
            if (a || b || c)
            {
                lblCheckPass.Text = "Пароль должен состоять из не менее 6 символов и содержать буквы, цифры и специальные символы";
                correctPass = false;
            }
            else
            {
                if (tPass.Text != tRepeatPass.Text)
                {
                    lblCheckPass.Text = "Пароли не совпадают!";
                    correctPass = false;
                }
                else
                {
                    lblCheckPass.Text = "";
                    correctPass = true;
                }
            }
            #endregion
        }

        private void Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmUsers = null;
        }

        private void RefreshTable()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var usersTable = (from i in context.Users
                                  join r in context.Rols on i.roleId equals r.id
                                  select new
                                  {
                                      i.username,
                                      i.FIO,
                                      i.post,
                                      roleName = r.name,
                                      i.deleted
                                  }).ToList();
                dataGridView.DataSource = usersTable;
            }
        }
    }
}