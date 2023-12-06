using ELibra.DBModel;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ELibra
{
    public partial class Login : Form
    {
        private int countWrong = 0;
        private bool close = true;
        internal int roleId { get; private set; }

        public Login()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (close)
            {
                Close();
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                #region Блокировка пользователя при неудачном входе
                if (File.Exists("block"))
                {
                    string[] lines = File.ReadAllLines("block");
                    if (lines[0] == cmbLogin.SelectedItem.ToString() && DateTime.Parse(lines[1]).AddMinutes(15) > DateTime.Now)
                    {
                        MessageBox.Show("Пользователь заблокирован на 15 минут");
                        Application.Exit();
                        this.MdiParent.Close();
                    }
                }
                #endregion

                var usersTable = (from i in context.Users
                                  where i.username == cmbLogin.SelectedItem.ToString()
                                  select i).FirstOrDefault();

                #region Обновление текущего пользователя
                if (usersTable.password == Classes.Update.UsersCyphering.Hash(tPass.Text))
                {
                    close = false;
                    roleId = usersTable.roleId;

                    var settingsUpdate = (from i in context.Settings
                                          where i.id == 1
                                          select i).FirstOrDefault();

                    settingsUpdate.fioOperator = usersTable.FIO;
                    context.SaveChanges();
                    Main form1 = this.Owner as Main;
                    this.DialogResult = DialogResult.OK;
                    notifyIcon1.Dispose();
                    Close();
                }
                else
                {
                    tPass.Clear();
                    MessageBox.Show("Введён неправильный пароль");
                    countWrong++;
                    if (countWrong >= 5)
                    {
                        File.WriteAllText("block", cmbLogin.SelectedItem.ToString() + "\n" + DateTime.Now);
                        MessageBox.Show("Пользователь заблокирован на 15 минут");
                        notifyIcon1.Dispose();
                        Application.Exit();
                    }
                }
                #endregion
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                #region Записть пользователей в список
                bool roleSelect = (from i in context.Settings
                                   where i.id == 1
                                   select i.licenseNumber).FirstOrDefault()[2] == '1';
                string[] usersRange;
                if (roleSelect)
                {
                    usersRange = (from i in context.Users
                                  where i.deleted == "Нет"
                                  select i.username).ToArray();
                }
                else
                {
                    usersRange = (from i in context.Users
                                  where i.deleted == "Нет" &&
                                        i.roleId != 3
                                  select i.username).ToArray();
                }
                cmbLogin.Items.AddRange(usersRange);
                cmbLogin.SelectedIndex = 0;
                #endregion
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void TPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
                BtnOk_Click(sender, e);
        }

        private void NotifyIcon1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
