using ELibra.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ELibra.DBModel;

namespace ELibra
{
    public partial class Upload1C : Form
    {
        //DataBaseContext context = new DataBaseContext();

        private string[] dataArr = new string[] { "id", "clientId", "clientName", "clientBIN", "clientAddress", "clientPhone", "clientDescription", "material", "carWeight", "brutto", "dateWeight", "userId", "userName", "carNumber", "carMark", "docWeight", "consignee", "talonNum", "point1", "point2",  "factorWeight", "invNumLoading", "volume", "driver", "operationType", "carrierid", "carrierName" };

        public Upload1C()
        {
            InitializeComponent();
        }

        private void Upload1C_Load(object sender, EventArgs e)
        {
            dateTimeFrom.Value = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                XmlDocument xDoc = new XmlDocument();

                var table = (from i in context.Weighings
                             where i.dateWeight > dateTimeFrom.Value.Date &&
                            (i.brutto != 0 && i.brutto != null) ||
                            (i.factorWeight != 0 && i.carWeight != null)
                             select new
                             {
                                 i.id,
                                 i.clientId,
                                 i.material,
                                 i.carWeight,
                                 i.brutto,
                                 i.dateWeight,
                                 i.userId,
                                 i.carNumber,
                                 i.carMark,
                                 i.docWeight,
                                 i.consignee,
                                 i.talonNum,
                                 i.point1,
                                 i.point2,
                                 i.operationType,
                                 i.factorWeight,
                                 i.invNumLoading,
                                 i.volume,
                                 i.driver,
                                 i.carrierid
                             });

                XmlNode meta = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);

                XmlElement org = xDoc.CreateElement("org");
                XmlAttribute orgAttr = xDoc.CreateAttribute("name");
                XmlText orgTextAttr = xDoc.CreateTextNode((from i in context.Settings select i.NameCompany).FirstOrDefault());
                orgAttr.AppendChild(orgTextAttr);
                org.Attributes.Append(orgAttr);

                XmlElement weight = xDoc.CreateElement("weight");
                XmlAttribute wAttr = xDoc.CreateAttribute("name");
                XmlText wTextAttr = xDoc.CreateTextNode("");
                wAttr.AppendChild(wTextAttr);
                weight.Attributes.Append(wAttr);



                XmlElement weighing;

                foreach (var item in table)
                {
                    if ((!cBoxDay.Checked && item.dateWeight < dateTimeBefore.Value.AddDays(1).Date) ||
                        (cBoxDay.Checked && item.dateWeight.Date == dateTimeFrom.Value.Date))
                    {
                        string[] row = new string[27];

                        for (int i = 0; i < row.Length; i++)
                            row[i] = "";

                        var clients = (from i in table
                                       join c in context.Clients on i.clientId equals c.id
                                       where c.id == item.clientId
                                       select new
                                       {
                                           clientName = c.name,
                                           c.RNN,
                                           c.address,
                                           c.phone,
                                           c.description
                                       }).FirstOrDefault();

                        var users = (from i in table
                                     join u in context.Users on i.userId equals u.id
                                     where u.id == item.clientId
                                     select u.username).FirstOrDefault();
                        var carriers = (from i in table
                                        join c in context.Carriers on i.carrierid equals c.id
                                        where c.id == i.carrierid
                                        select c.name).FirstOrDefault();


                        row[0] = item.id.ToString();
                        row[1] = item.clientId.ToString();
                        if (clients != null)
                        {
                            row[2] = clients.clientName;
                            row[3] = clients.RNN;
                            row[4] = clients.address;
                            row[5] = clients.phone;
                            row[6] = clients.description;
                        }
                        row[7] = item.material;
                        row[8] = item.carWeight.ToString();
                        row[9] = item.brutto.ToString();
                        row[10] = item.dateWeight.ToString();
                        row[11] = item.userId.ToString();
                        row[12] = users;
                        row[13] = item.carNumber;
                        row[14] = item.carMark;
                        row[15] = item.docWeight.ToString();
                        row[16] = item.consignee;
                        row[17] = item.id.ToString();
                        row[18] = item.point1;
                        row[19] = item.point2;                    
                        row[20] = item.factorWeight.ToString();
                        row[21] = item.invNumLoading.ToString();
                        row[22] = item.volume.ToString();
                        row[23] = item.driver;
                        row[24] = item.operationType;
                        row[25] = item.carrierid.ToString();
                        row[26] = carriers;

                        weighing = xDoc.CreateElement("weighing");
                        for (int j = 0; j < dataArr.Length; j++)
                        {
                            XmlAttribute attr = xDoc.CreateAttribute(dataArr[j]);
                            XmlText textAttr = xDoc.CreateTextNode(row[j]);
                            attr.AppendChild(textAttr);
                            weighing.Attributes.Append(attr);
                        }
                        weight.AppendChild(weighing);
                    }
                }

                xDoc.AppendChild(meta);
                org.AppendChild(weight);
                xDoc.AppendChild(org);
                var settings = (from i in context.Settings
                                where i.id == 1
                                select new
                                {
                                    i.cBoxExportFold,
                                    i.tExportFold
                                }).FirstOrDefault();

                if (Boolean.Parse(settings.cBoxExportFold))
                {
                    try
                    {
                        string path = settings.tExportFold + "\\" + DateTime.Now.Date.ToString().Split()[0].Replace(".", "") + ".xml";
                        xDoc.Save(path);
                    }
                    catch
                    {
                        MessageBox.Show("Папка для выгрузки не найдена");
                    }
                }
                if (!Boolean.Parse(settings.cBoxExportFold))
                    MessageBox.Show("Не выбранно место выгрузки");
                Close();
            }
            
        }

        private void CBoxDay_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxDay.Checked)
                dateTimeBefore.Enabled = false;
            else
                dateTimeBefore.Enabled = true;
        }
    }
}
