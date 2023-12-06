using ELibra.DBModel;
using ELibra.DBModel.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ELibra.Classes
{
    static class UploadHelper
    {
        public static void Upload()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string json = "";
                dataWeighing data = new dataWeighing();

                #region Получение взвешиваний не выгруженных на сервер
                DateTime? lastUpload = (from i in context.Settings where i.id == 1 select i.lastUpload).FirstOrDefault();
                var table =  from i in context.Weighings
                             where i.updated >= lastUpload &&
                                   i.material != "" &&
                                   i.material != null
                             select i;
                #endregion
                var list = table.ToList();
                if (table.Count() != 0)
                {
                    #region Выгрузка данных на сервер
                    foreach (Weighings w in table)
                    {
                        try
                        {
                            data.id = w.id;
                            data.clientId = (from i in context.Clients where i.id == w.clientId select i.name).FirstOrDefault();
                            data.material = w.material != null ? w.material : "";
                            data.carWeight = w.carWeight != null ? Int32.Parse(w.carWeight.ToString()) : 0;
                            data.brutto = w.brutto != null ? Int32.Parse(w.brutto.ToString()) : 0;
                            data.dateWeight = w.dateWeight.ToString("yyyy-MM-ddTHH:mm:ss");
                            data.carNumber = w.carNumber != null ? w.carNumber : "";
                            data.carMark = w.carMark != null ? w.carMark : "";
                            data.docWeight = w.docWeight != null ? Int32.Parse(w.docWeight.ToString()) : 0;//
                            data.point2 = w.point1 != null ? w.point2 : "";
                            data.operationType = w.operationType != null ? w.operationType : "";
                            data.factorWeight = w.factorWeight != null ? Int32.Parse(w.factorWeight.ToString()) : 0;//
                            data.volume = w.volume != null ? double.Parse(w.volume.ToString()) : 0;
                            data.driverid = w.driver ?? "";
                            data.storageid = ((from i in context.Storages where i.id == w.storageId select i.name).FirstOrDefault()) ?? "";
                            data.dateOut = w.dateOut != null ? ((DateTime)w.dateOut).ToString("yyyy-MM-ddTHH:mm:ss") : "";//
                            data.orderPositionNumber = w.orderPositionNumber != null ? w.orderPositionNumber : "";
                            data.adjustment = w.adjustment != null ? (double)w.adjustment : 0;
                            int roleId = ((from i in context.Users where i.id == w.userId select i.roleId).FirstOrDefault());
                            data.operatorId = (from i in context.Rols where i.id == roleId select i.name).FirstOrDefault();
                            data.taraScale = w.taraScale ?? "";
                            data.fullWeightScale = w.fullWeightScale ?? "";
                            data.edited = w.edited != null ? ((DateTime)w.edited).ToString("yyyy-MM-ddTHH:mm:ss") : "";
                            data.redactor = w.redactor ?? "";
                            json += JsonConvert.SerializeObject(data);
                            json += ",";
                        }
                        catch
                        {
                            Console.WriteLine(data.id);
                        }
                    }
                    json = "[" + json.Substring(0, json.Length - 1) + "]";
                    var loginData = (from i in context.Settings
                                     where i.id == 1
                                     select new {
                                         i.HostDB,
                                         i.LoginDB,
                                         i.PasswordDB
                                     }).FirstOrDefault();
                    serverHelp.addWeighing(json, serverHelp.getToken(loginData.LoginDB, loginData.PasswordDB, fLogin: loginData.HostDB.Split('|')[1]));
                    #endregion

                    #region Сохранение даты последней выгрузки
                    Settings s = (from i in context.Settings
                                  where i.id == 1
                                  select i).FirstOrDefault();
                    s.lastUpload = DateTime.Now; 
                    context.SaveChanges();
                    #endregion

                    MessageBox.Show("Данные выгружены");
                }
                else
                {
                    MessageBox.Show("Нет данных для выгрузки");
                }
            }
        }
    }
}
