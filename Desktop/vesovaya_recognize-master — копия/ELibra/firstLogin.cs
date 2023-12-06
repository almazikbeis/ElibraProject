using ELibra.Classes;
using ELibra.DBModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel.Models;

namespace ELibra
{
    public partial class firstLogin : Form
    {
       
        public firstLogin()
        {
            InitializeComponent();
            using (DataBaseContext context = new DataBaseContext())
            {
                var account = (from i in context.Settings
                               select new
                               {
                                   i.LoginDB,
                                   i.PasswordDB
                               }).FirstOrDefault();
                if (account.LoginDB != "" && account.PasswordDB !="")
                {
                    tLogin.Text = account.LoginDB;
                    tPass.Text = account.PasswordDB;
                }

            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (tLogin.Text != "" || tURL.Text != "" || tPass.Text != "")
            {
                try
                {
                    #region Получение данных о лицензии
                    
                    string res = "[" + serverHelp.getHttp(tURL.Text.Trim(' ').TrimEnd('/') + "/api/license", serverHelp.getToken(tLogin.Text.Trim(' '), tPass.Text.Trim(' '), fLogin: tURL.Text.Trim(' ').TrimEnd('/'))) + "]";
                    
                    JArray a = JArray.Parse(res);
                   
                    List<string> arr = new List<string>();
                    foreach (JObject o in a.Children<JObject>())
                    {
                        foreach (JProperty p in o.Properties())
                        {
                            string value = (string)p.Value;
                            arr.Add(value);
                        }
                    }
                    #endregion
                    using (DataBaseContext context = new DataBaseContext())
                    {
                        var settings = (from i in context.Settings
                                             where i.id == 1
                                             select i).FirstOrDefault();

                        #region Сохранение настроек лицензии
                        var hostList = settings.HostDB.Split('|').ToList();
                        if (hostList.Count > 1)
                        {
                            hostList[0] = tURL.Text.Trim(' ').TrimEnd('/');
                            settings.HostDB = string.Join("|", hostList);
                        }
                        else
                        {
                            settings.HostDB = tURL.Text.Trim(' ').TrimEnd('/') + "|" + tURL.Text.Trim(' ').TrimEnd('/');
                        }
                        settings.LoginDB = tLogin.Text;
                        settings.PasswordDB = tPass.Text;
                        settings.licenseNumber = arr[0];
                        settings.licenseDate = DateTime.Parse(arr[2]);
                        settings.NameCompany = arr[1];
                        settings.lastUpload = DateTime.Now;
                        context.SaveChanges();
                        #endregion
                    }
                    Close();
                }
                catch (Exception ex)
                {
                    if (ex is WebException)
                    {
                        if ((ex as WebException).Status == WebExceptionStatus.ProtocolError && (ex as WebException).Response != null)
                        {
                            var resp = (HttpWebResponse)(ex as WebException).Response;
                            if (resp.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                MessageBox.Show("Неверный логин или пароль");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат адреса");
                    }
                }
            }
            else
            {
                MessageBox.Show("Указаны не все данные");
            }
        }

        private void FirstLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var loginDB = (from i in context.Settings
                              where i.id == 1
                              select i.LoginDB).ToString();
                if (loginDB == "")
                {
                    Application.Exit();
                }
            }
        }

        private void T_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
                BtnOk_Click(sender, e);
        }
    }
}
