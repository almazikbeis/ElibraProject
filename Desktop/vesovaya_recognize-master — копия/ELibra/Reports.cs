using ELibra.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using ELibra.DBModel;
using System.Threading.Tasks;

namespace ELibra
{
    public partial class Reports : Form
    {
        private List<CheckBox> cbScales = new List<CheckBox>();

        public Reports( int indexPage)
        {
            InitializeComponent();
            tabControl.SelectedIndex = indexPage;
        }

        private void BtnCancelDay_Click(object sender, EventArgs e)
        {
            this.Close();
            //Close();
        }

        private void BtnOkDay_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                DateTime dateTimeFrom = datePickerPerDay.Value.Date + timePickerPerDay.Value.TimeOfDay;
                DateTime dateTimeBefore = dateTimeFrom.AddHours(24);
                var weighingTable = (from i in context.Weighings
                                     where (i.dateWeight >= dateTimeFrom) &&
                                    (i.dateWeight <= dateTimeBefore)     
                                     select new
                                     {
                                         i.dateWeight,
                                         i.brutto,
                                         i.factorWeight
                                     });
                if (!cBoxNotFinishDay.Checked)
                {
                    weighingTable = from i in weighingTable
                                    where (i.brutto != 0 && i.brutto != null) &&
                                    (i.factorWeight != 0 && i.factorWeight != null)
                                    select i;
                }
                #region Проверка взвешиваний за день
                if (weighingTable.Count() != 0)
                    DocHelper.dayReportXls(datePickerPerDay.Value, timePickerPerDay.Value, cBoxNotFinishDay.Checked);
                    //DocHelper.dayReport(datePickerPerDay.Value, timePickerPerDay.Value, cBoxNotFinishDay.Checked);

                else
                    MessageBox.Show("В данный период времени не было взвешиваний");
                #endregion

            }

        }

        private void BtnOkMouth_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var weighingTable = (from i in context.Weighings
                                     select new{
                                         i.dateWeight,
                                         i.brutto,
                                         i.factorWeight
                                     });

                #region Проверка взвешиваний за месяц

                if (!cBoxNotFinishMouth.Checked)
                {
                    weighingTable = from i in weighingTable
                                    where (i.brutto != 0 && i.brutto != null) &&
                                    (i.factorWeight != 0 && i.factorWeight != null)
                                    select i;
                }
                int count = 0;
                foreach (var item in weighingTable)
                    if (datePickerMouth.Value.Month == item.dateWeight.Month)
                        count++;
                if (count != 0)
                    DocHelper.monthReportXls(datePickerMouth.Value, cBoxNotFinishMouth.Checked);
                    //DocHelper.monthReport(datePickerMouth.Value, cBoxNotFinishMouth.Checked);
                else
                    MessageBox.Show("В данный период времени не было взвешиваний");
                #endregion
            }

        }

        private void BtnOkSum_Click(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                int count = 0;
                var temp = (from i in context.Weighings
                                     select i).AsEnumerable();
                DateTime dateTimeFrom = datePickerSumFrom.Value.Date + timePickerSumFrom.Value.TimeOfDay;
                DateTime dateTimeBefore = datePickerSumBefore.Value.Date + timePickerSumBefore.Value.TimeOfDay;
                var table = (from w in temp
                            where (w.dateWeight >= dateTimeFrom) &&
                                  (w.dateWeight <= dateTimeBefore)
                            select w).AsEnumerable();
                if (!cBoxNotFinishSum.Checked)
                {
                    table = from i in table
                                    where (i.brutto != 0 && i.brutto != null) &&
                                    (i.factorWeight != 0 && i.factorWeight != null)
                                    select i;
                }

                #region Проверка взвешиваний суммарная
                if (table.Any())
                {
                    count = table.Count();
                }
                if (count != 0)
                    DocHelper.sumReportXls(datePickerSumFrom.Value, timePickerSumFrom.Value, datePickerSumBefore.Value, timePickerSumBefore.Value, cBoxNotFinishSum.Checked);
                    //DocHelper.sumReport(datePickerSumFrom.Value, timePickerSumFrom.Value, datePickerSumBefore.Value, timePickerSumBefore.Value, cBoxNotFinishSum.Checked);
                else
                    MessageBox.Show("В данный период времени не было взвешиваний");
                #endregion
            }

        }

        private void getCount(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var weighingTable = (from i in context.Weighings
                                     where (i.brutto != null && i.carWeight != null)
                                     select new
                                     {
                                         i.id,
                                         i.dateWeight,
                                         i.adjustment,
                                         i.brutto,
                                         i.carWeight,
                                         i.edited
                                     });
                          
                #region Получение количества взвешиваний за период

                #region Взвешивания за день
                int count = 0;
                var tempTable = weighingTable;
                if (!cBoxNotFinishDay.Checked)
                {
                    tempTable = from i in tempTable
                                where (i.brutto != 0 && i.brutto != null) &&
                                  (i.carWeight != 0 && i.carWeight != null)
                                    select i;
                }
                DateTime dateTimeFrom = datePickerPerDay.Value.Date + timePickerPerDay.Value.TimeOfDay;
                DateTime dateTimeBefore = dateTimeFrom.AddHours(24);
                foreach (var item in tempTable)
                    if (item.dateWeight >= dateTimeFrom && item.dateWeight <= dateTimeBefore) 
                        count++;
                lblCountPerDay.Text = count.ToString() + " взвешиваний";
                #endregion

                #region Взвешивания за месяц
                count = 0;
                tempTable = (from i in weighingTable
                            where i.dateWeight.Month == datePickerMouth.Value.Month
                             select i);
                if (!cBoxNotFinishMouth.Checked)
                {
                    tempTable = from i in tempTable
                                where (i.brutto != 0 && i.brutto != null) &&
                                  (i.carWeight != 0 && i.carWeight != null)
                                select i;
                }
                
                //foreach (var item in tempTable)
                //    if (datePickerMouth.Value.Month == item.dateWeight.Month)
                //        count++;
                count = tempTable.Count();
                lblCountMouth.Text = count.ToString() + " взвешиваний";
                #endregion

                #region Взвешивания суммарные по материалам 
                count = 0;
                tempTable = weighingTable;
                if (!cBoxNotFinishSum.Checked)
                {
                    tempTable = from i in tempTable
                                where (i.brutto != 0 && i.brutto != null) &&
                                  (i.carWeight != 0 && i.carWeight != null)
                                select i;
                }
                DateTime sumTimeFrom = datePickerSumFrom.Value.Date + timePickerSumFrom.Value.TimeOfDay;
                DateTime sumTimeBefore = datePickerSumBefore.Value.Date + timePickerSumBefore.Value.TimeOfDay;
                foreach (var item in tempTable)
                    if (sumTimeFrom <= item.dateWeight &&
                        sumTimeBefore >= item.dateWeight)
                        count++;
                lblCountSum.Text = count.ToString() + " взвешиваний";
                #endregion

                #region Взвешивания детальные
                count = 0;
                tempTable = weighingTable;
                if (cBoxEdited.Checked)
                {
                    tempTable = from i in tempTable
                                where i.edited != null
                                select i;
                }

                if (!cBoxNotFinishDetal.Checked)
                {
                    tempTable = from i in tempTable
                                where (i.brutto != 0 && i.brutto != null) &&
                                  (i.carWeight != 0 && i.carWeight != null)
                                select i;
                }
                if (cmbAdjCheck.Text == "С корректировкой")
                {
                    tempTable = from i in tempTable
                                where i.adjustment != 0.0 && i.adjustment != null
                                    select i;
                }
                else if (cmbAdjCheck.Text == "Без корректировки")
                {
                    tempTable = from i in tempTable
                                where  i.adjustment == 0.0 || i.adjustment == null
                                    select i;
                }

                DateTime detalTimeFrom = datePickerDetalFrom.Value.Date + timePickerDetalFrom.Value.TimeOfDay;

                DateTime detalTimeBefore = datePickerDetalBefore.Value.Date + timePickerDetalBefore.Value.TimeOfDay;
                //foreach (var item in weighingTable)
                //    if ((item.dateWeight >= detalTimeFrom) &&
                //        (item.dateWeight <= detalTimeBefore))
                //        count++;
                count = (from i in tempTable
                         where i.dateWeight >= detalTimeFrom &&
                         i.dateWeight <= detalTimeBefore
                         select i).Count();
                //foreach (var cbScale in cbScales)
                //{
                //    cbScale.Dispose();
                //}
                //cbScales.Clear();
                detalChangeItems(detalTimeFrom, detalTimeBefore);
                lblCountDetal.Text = count.ToString() + " взвешиваний";
                #endregion
                #endregion
            }

        }

        public void detalChangeItems(DateTime detalTimeFrom, DateTime detalTimeBefore)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                Console.WriteLine("detalChangeItems");
                cmbGoods.Items.Clear();
                cmbRole.Items.Clear();
                cmbClient.Items.Clear();
                cmbNumCar.Items.Clear();
                cmbMarkCar.Items.Clear();
                cmbDriver.Items.Clear();
                cmbConsignee.Items.Clear();
                cmbConsignor.Items.Clear();
                cmbStorage.Items.Clear();
                cmbCarrier.Items.Clear();
                cmbPoint1.Items.Clear();
                cmbPoint2.Items.Clear();
                cmbNumOrder.Items.Clear();
                cmbNumPosOrder.Items.Clear();

                foreach (var cbScale in cbScales)
                {
                    cbScale.Location = new System.Drawing.Point(0, 0);
                    panel1.Controls.Remove(cbScale);
                    cbScale.Dispose();
                }
                cbScales.Clear();

                cmbGoods.Items.Add("");
                cmbRole.Items.Add("");
                cmbClient.Items.Add("");
                cmbNumCar.Items.Add("");
                cmbMarkCar.Items.Add("");
                cmbDriver.Items.Add("");
                cmbConsignee.Items.Add("");
                cmbConsignor.Items.Add("");
                cmbStorage.Items.Add("");
                cmbCarrier.Items.Add("");
                cmbPoint1.Items.Add("");
                cmbPoint2.Items.Add("");
                cmbNumOrder.Items.Add("");
                cmbNumPosOrder.Items.Add("");
                
                var startPosition = new System.Drawing.Point(134, lblScaleTara.Location.Y);
                var gap = 6;
                var iterator = 0;
                foreach (var scale in (from i in context.Scales select new { i.name, i.nameUser}))
                {
                    CheckBox cbScale = new CheckBox();
                    ToolTip strip = new ToolTip();
                    panel1.Controls.Add(cbScale);
                    strip.SetToolTip(cbScale, "Не выбирайте, если хотите все");
                    cbScale.Name = scale.name;
                    cbScale.Text = scale.nameUser;
                    cbScale.Location = startPosition;
                    cbScale.Location = new System.Drawing.Point(startPosition.X, (startPosition.Y + (iterator * cbScale.Height)) /*+ (iterator == 0 ? 0 : gap) */);
                    cbScale.CheckedChanged += new EventHandler(Checked);
                    cbScales.Add(cbScale);
                    iterator++;
                }
                cBoxNotFinishDetal.Location = new System.Drawing.Point(73, 549);
                cBoxEdited.Location = new System.Drawing.Point(72, 572);
                cBoxNotFinishDetal.Location = new System.Drawing.Point(cBoxNotFinishDetal.Location.X, startPosition.Y + (iterator + 1) * cBoxNotFinishDetal.Height + gap);
                cBoxEdited.Location = new System.Drawing.Point(cBoxEdited.Location.X, startPosition.Y + (iterator + 2) * cBoxEdited.Height + gap);
                var weighings = from i in context.Weighings
                                where i.dateWeight >= detalTimeFrom &&
                                i.dateWeight <= detalTimeBefore
                                select i;
                if ((from i in weighings where i.material != "" && i.material != null select i.material).Any())
                {
                    cmbGoods.Items.AddRange(((from i in weighings where i.material != "" && i.material != null select i.material)).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings join u in context.Users on i.userId equals u.id where u.FIO != "" && u.FIO != null select u.FIO).Any())
                {
                    cmbRole.Items.AddRange((from i in weighings join u in context.Users on i.userId equals u.id where u.FIO != "" && u.FIO != null select u.FIO).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings join c in context.Clients on i.clientId equals c.id where c.name != "" && c.name != null select c.name).Any())
                {
                    cmbClient.Items.AddRange((from i in weighings join c in context.Clients on i.clientId equals c.id where c.name != "" && c.name != null select c.name).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings where i.carNumber != "" && i.carNumber!= null select i.carNumber).Any())
                {
                    cmbNumCar.Items.AddRange((from i in weighings where i.carNumber != "" && i.carNumber != null select i.carNumber).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings where i.carMark != "" && i.carMark != null select i.carMark).Any())
                {
                    cmbMarkCar.Items.AddRange((from i in weighings where i.carMark != "" && i.carMark != null select i.carMark).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings where i.driver != "" && i.driver != null select i.driver).Any())
                {
                    cmbDriver.Items.AddRange((from i in weighings where i.driver != "" && i.driver != null select i.driver).Distinct().ToArray() ?? new string[0]);            
                }
                if ((from i in weighings where i.point1 != "" && i.point1 != null select i.point1).Any())
                {
                    cmbPoint1.Items.AddRange((from i in weighings where i.point1 != "" && i.point1 != null select i.point1).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings where i.point2 != "" select i.point2).Any())
                {
                    cmbPoint2.Items.AddRange((from i in weighings where i.point2 != "" && i.point2 != null select i.point2).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings where i.orderNumber != "" && i.orderNumber != null select i.orderNumber).Any())
                {
                    cmbNumOrder.Items.AddRange((from i in weighings where i.orderNumber != "" && i.orderNumber != null select i.orderNumber).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings where i.orderPositionNumber != "" && i.orderPositionNumber != null select i.orderPositionNumber).Any())
                {
                    cmbNumPosOrder.Items.AddRange((from i in weighings where i.orderPositionNumber != "" && i.orderPositionNumber != null select i.orderPositionNumber).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings where i.consignee != "" && i.consignee != null select i.consignee).Any())
                {
                    cmbConsignee.Items.AddRange((from i in weighings where i.consignee != "" && i.consignee != null select i.consignee).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings where i.consignor != "" && i.consignor != null select i.consignor).Any())
                {
                    cmbConsignor.Items.AddRange((from i in weighings where i.consignor != "" && i.consignor != null select i.consignor).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings join s in context.Storages on i.storageId equals s.id where s.name != "" && s.name != null select s.name).Any())
                {
                    cmbStorage.Items.AddRange((from i in weighings join s in context.Storages on i.storageId equals s.id where s.name != "" && s.name != null select s.name).Distinct().ToArray() ?? new string[0]);
                }
                if ((from i in weighings join c in context.Carriers on i.carrierid equals c.id where c.name != ""  && c.name != null select c.name).Any())
                {
                    cmbCarrier.Items.AddRange((from i in weighings join c in context.Carriers on i.carrierid equals c.id where c.name != "" && c.name != null select c.name).Distinct().ToArray() ?? new string[0]);
                }

                //HashSet<string> scales = new HashSet<string>();

                //if ((from i in weighings where i.scaleIn != "" && i.scaleIn != null select i.scaleIn).Any())
                //{
                //    foreach (var item in (from i in weighings where i.scaleIn != "" && i.scaleIn != null select i.scaleIn))
                //    {
                //        scales.Add(item);
                //    }
                //}
                //if ((from i in weighings where i.scaleOut != "" && i.scaleOut != null select i.scaleOut).Any())
                //{
                //    foreach (var item in (from i in weighings where i.scaleOut != "" && i.scaleOut != null select i.scaleOut))
                //    {
                //        scales.Add(item);
                //    }
                //}
                Console.WriteLine("после заполнения");
                cmbTypeOperation.SelectedIndex = 0;
                cmbPoint1.SelectedIndex = 0;
                cmbPoint2.SelectedIndex = 0;
                cmbNumOrder.SelectedIndex = 0;
                cmbNumPosOrder.SelectedIndex = 0;
                cmbDriver.SelectedIndex = 0;
                cmbPoint1.SelectedIndex = 0;
                cmbPoint2.SelectedIndex = 0;
                cmbNumOrder.SelectedIndex = 0;
                cmbNumPosOrder.SelectedIndex = 0;
                cmbDriver.SelectedIndex = 0;
                //cmbScales.SelectedIndex = 0;
            }       
        }

        private void Checked(object sender, EventArgs e)
        {
            cbScales.Find(x=> x.Name == (sender as CheckBox).Name).Checked = (sender as CheckBox).Checked;
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                #region Заполнение полей при первом входе
                cmbAdjCheck.SelectedIndex = 0;
                getCount(sender, e);
                //var goodsArr = (from i in context.Goods
                //                select i.name).ToArray();
                //var usersArr = (from i in context.Users
                //                select i.FIO).ToArray();
                //var clientsArr = (from i in context.Clients
                //                select i.name).ToArray();
                //var carsArr = (from i in context.Cars
                //                  select i.number).ToArray();
                //var marksArr = (from i in context.CarMarks
                //               select i.name).ToArray();
                //var driversArr = (from i in context.Drivers
                //                  select new
                //                  {
                //                      i.surname,
                //                      i.name,
                //                      i.patronimyc
                //                  });
                //cmbGoods.Items.AddRange(goodsArr);
                //cmbRole.Items.AddRange(usersArr);
                //cmbClient.Items.AddRange(clientsArr);
                //cmbNumCar.Items.AddRange(carsArr);
                //cmbMarkCar.Items.AddRange(marksArr);
                //cmbPoint1.SelectedIndex = 0;
                //cmbPoint2.SelectedIndex = 0;
                //cmbNumOrder.SelectedIndex = 0;
                //cmbNumPosOrder.SelectedIndex = 0;
                //cmbDriver.SelectedIndex = 0;
                //string[] surnames = (from i in driversArr
                //                    select i.surname).ToArray();
                //string[] names = (from i in driversArr
                //                  select i.name).ToArray();
                //string[] patronimycs = (from i in driversArr
                //                        select i.patronimyc).ToArray();
                //var consigneeArr = (from i in context.Consignee
                //                    select i.name).ToArray();
                //var consignorArr = (from i in context.Consignor
                //                    select i.name).ToArray();
                //var storahesArr = (from i in context.Storages
                //                    select i.name).ToArray();
                //var carriersArr = (from i in context.Carriers
                //                    select i.name).ToArray();
                //for (int i = 0; i < surnames.Length; i++)
                //    cmbDriver.Items.Add(surnames[i] + " " + names[i] + " " + patronimycs[i]);
            
                //if (File.Exists("data.xml"))
                //{
                //    XmlDocument xDoc = new XmlDocument();
                //    xDoc.Load("data.xml");
                //    XmlElement xRoot = xDoc.DocumentElement;
                //    foreach (XmlNode node in xRoot)
                //    {
                //        if (node.Name == "point1")
                //            cmbPoint1.Items.Add(node.Attributes[0].Value);
                //        if (node.Name == "point2")
                //            cmbPoint2.Items.Add(node.Attributes[0].Value);
                //        if (node.Name == "numOrder")
                //            cmbNumOrder.Items.Add(node.Attributes[0].Value);
                //        if (node.Name == "posOrder")
                //            cmbNumPosOrder.Items.Add(node.Attributes[0].Value);
                //    }
                //}
                //cmbConsignee.Items.AddRange(consigneeArr);
                //cmbConsignor.Items.AddRange(consignorArr);
                //cmbStorage.Items.AddRange(storahesArr);
                //cmbCarrier.Items.AddRange(carriersArr);
                //cmbTypeOperation.SelectedIndex = 0;
                #endregion
            }

        }

        private void BtnOkDetal_Click(object sender, EventArgs e)
        {

            using (DataBaseContext context = new DataBaseContext())
            {
                DateTime dateTimeFrom = datePickerDetalFrom.Value.Date + timePickerDetalFrom.Value.TimeOfDay;
                DateTime dateTimeBefore = datePickerDetalBefore.Value.Date + timePickerDetalBefore.Value.TimeOfDay;
                var weighngArr = (from i in context.Weighings
                                  where i.dateWeight >= dateTimeFrom &&
                                  i.dateWeight <= dateTimeBefore
                                  select new {
                                      i.dateWeight,
                                      i.brutto,
                                      i.factorWeight
                                  });

                #region Проверка детальные записи

                if (!cBoxNotFinishDetal.Checked)
                {
                    weighngArr = from i in weighngArr
                                 where (i.brutto != 0 && i.brutto != null) &&
                                  (i.factorWeight != 0 && i.factorWeight != null)
                            select i;
                }

                if (weighngArr.Any())
                {
                    var usersId = (from i in context.Users
                                   where i.FIO == cmbRole.Text
                                   select i.id).FirstOrDefault();
                    var storagesId = (from i in context.Storages
                                   where i.name == cmbStorage.Text
                                   select i.id).FirstOrDefault();
                    var carriersId = (from i in context.Carriers
                                      where i.name == cmbCarrier.Text
                                      select i.id).FirstOrDefault();
                    List<string> selectedScales = new List<string>();
                    foreach (var cbScale in cbScales)
                    {
                        if (cbScale.Checked)
                        {
                            selectedScales.Add(cbScale.Text);
                        }
                    }
                    DocHelper.detalReportXls(datePickerDetalFrom.Value,
                                                 timePickerDetalFrom.Value,
                                                 datePickerDetalBefore.Value,
                                                 timePickerDetalBefore.Value,
                                                 cmbGoods.Text,
                                                 cmbRole.Text != "" ? usersId.ToString() : "",
                                                 cmbClient.Text,
                                                 cmbNumCar.Text,
                                                 cmbMarkCar.Text,
                                                 cmbDriver.SelectedItem.ToString() != " " ? cmbDriver.SelectedItem.ToString() : "",
                                                 cmbPoint1.SelectedItem.ToString() != " " ? cmbPoint1.SelectedItem.ToString() : "",
                                                 cmbPoint2.SelectedItem.ToString() != " " ? cmbPoint2.SelectedItem.ToString() : "",
                                                 cmbConsignee.Text,
                                                 cmbConsignor.Text,
                                                 cmbTypeOperation.SelectedItem.ToString(),
                                                 storagesId.ToString() != "0" ? storagesId.ToString() : "",
                                                 carriersId.ToString() != "0" ? carriersId.ToString() : "",
                                                 cmbNumOrder.SelectedItem.ToString() != " " ? cmbNumOrder.SelectedItem.ToString() : "",
                                                 cmbNumPosOrder.SelectedItem.ToString() != " " ? cmbNumPosOrder.SelectedItem.ToString() : "",
                                                 cBoxNotFinishDetal.Checked,
                                                 cmbAdjCheck.Text,
                                                 selectedScales.Count > 0 ? selectedScales.ToArray() : new string[0],
                                                 cBoxEdited.Checked);

                    //DocHelper.detalReport(datePickerDetalFrom.Value,
                    //                          timePickerDetalFrom.Value,
                    //                          datePickerDetalBefore.Value,
                    //                          timePickerDetalBefore.Value,
                    //                          cmbGoods.Text,
                    //                          cmbRole.Text != "" ? usersId.ToString() : "",
                    //                          cmbClient.Text,
                    //                          cmbNumCar.Text,
                    //                          cmbMarkCar.Text,
                    //                          cmbDriver.SelectedItem.ToString() != " " ? cmbDriver.SelectedItem.ToString() : "",
                    //                          cmbPoint1.SelectedItem.ToString() != " " ? cmbPoint1.SelectedItem.ToString() : "",
                    //                          cmbPoint2.SelectedItem.ToString() != " " ? cmbPoint2.SelectedItem.ToString() : "",
                    //                          cmbConsignee.Text,
                    //                          cmbConsignor.Text,
                    //                          cmbTypeOperation.SelectedItem.ToString(),
                    //                          storagesId.ToString() != "0" ? storagesId.ToString() : "",
                    //                          carriersId.ToString() != "0" ? carriersId.ToString() : "",
                    //                          cmbNumOrder.SelectedItem.ToString() != " " ? cmbNumOrder.SelectedItem.ToString() : "",
                    //                          cmbNumPosOrder.SelectedItem.ToString() != " " ? cmbNumPosOrder.SelectedItem.ToString() : "",
                    //                          cBoxNotFinishDetal.Checked,
                    //                          cmbAdjCheck.Text,
                    //                          selectedScales.Count > 0 ? selectedScales.ToArray() : new string[0],
                    //                          cBoxEdited.Checked);
                }
                else
                    MessageBox.Show("В данный период времени не было взвешиваний");
                #endregion
            }

        }


        #region Отчистка полей
        private void CmbGoods_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbGoods.Items.Clear();
            //cmbGoods.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM Goods WHERE name LIKE '" + cmbGoods.Text + "%'", "name").ToArray());
            cmbGoods.SelectionStart = cmbGoods.Text.Length;
        }

        private void CmbRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbRole.Items.Clear();
            //cmbRole.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM Users WHERE FIO LIKE '" + cmbRole.Text + "%'", "FIO").ToArray());
            cmbRole.SelectionStart = cmbRole.Text.Length;
        }

        private void CmbClient_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbClient.Items.Clear();
            //cmbClient.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM Clients WHERE name LIKE '" + cmbClient.Text + "%'", "name").ToArray());
            cmbClient.SelectionStart = cmbClient.Text.Length;
        }

        private void CmbNumCar_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbNumCar.Items.Clear();
            //cmbNumCar.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM Cars WHERE number LIKE '" + cmbNumCar.Text + "%'", "number").ToArray());
            cmbNumCar.SelectionStart = cmbNumCar.Text.Length;
        }

        private void CmbMarkCar_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbMarkCar.Items.Clear();
            //cmbMarkCar.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM CarMarks WHERE name LIKE '" + cmbMarkCar.Text + "%'", "name").ToArray());
            cmbMarkCar.SelectionStart = cmbMarkCar.Text.Length;
        }

        private void CmbConsignee_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbConsignee.Items.Clear();
            //cmbConsignee.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM Consignee WHERE name LIKE '" + cmbConsignee.Text + "%'", "name").ToArray());
            cmbConsignee.SelectionStart = cmbConsignee.Text.Length;
        }

        private void CmbConsignor_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbConsignor.Items.Clear();
            //cmbConsignor.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM Consignor WHERE name LIKE '" + cmbConsignor.Text + "%'", "name").ToArray());
            cmbConsignor.SelectionStart = cmbConsignor.Text.Length;
        }

        private void CmbStorage_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbStorage.Items.Clear();
            //cmbStorage.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM Storages WHERE name LIKE '" + cmbStorage.Text + "%'", "name").ToArray());
            cmbStorage.SelectionStart = cmbStorage.Text.Length;
        }

        private void CmbCarrier_KeyPress(object sender, KeyPressEventArgs e)
        {
            cmbCarrier.Items.Clear();
            //cmbCarrier.Items.AddRange(dbHelper.ExecuteFieldArr("SELECT * FROM Carriers WHERE name LIKE '" + cmbCarrier.Text + "%'", "name").ToArray());
            cmbCarrier.SelectionStart = cmbCarrier.Text.Length;
        }
        #endregion

    }
}
