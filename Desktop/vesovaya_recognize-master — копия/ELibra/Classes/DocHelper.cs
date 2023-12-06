using ELibra.DBModel;
using ELibra.DBModel.Models;
using Microsoft.Office.Interop.Word;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Diagnostics;

namespace ELibra.Classes
{
    static class DocHelper
    {
        public static bool nakl1(DateTime date)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Word._Application oWord = new Word.Application();
                    _Document oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\templates\\nakl1.dotx");

                    var data = (from i in context.Weighings
                                where i.dateWeight == date
                                join c in context.Clients on i.clientId equals c.id
                                join u in context.Users on i.userId equals u.id
                                select new { i.id, i.dateOut, c.name, c.RNN, i.material, i.point2, i.driver, i.volume, i.carNumber, i.brutto, i.carWeight, i.factorWeight, i.seal, u.FIO }).FirstOrDefault();

                    #region Запись в документ по меткам
                    oDoc.Bookmarks["startDate"].Range.Text = date.ToString();
                    oDoc.Bookmarks["startDate2"].Range.Text = date.ToString();
                    oDoc.Bookmarks["num"].Range.Text = data.id.ToString();
                    oDoc.Bookmarks["num2"].Range.Text = data.id.ToString();
                    oDoc.Bookmarks["endDate"].Range.Text = data.dateOut.ToString();
                    oDoc.Bookmarks["endDate2"].Range.Text = data.dateOut.ToString();
                    oDoc.Bookmarks["endDate3"].Range.Text = data.dateOut.ToString();
                    oDoc.Bookmarks["endDate4"].Range.Text = data.dateOut.ToString();
                    oDoc.Bookmarks["client"].Range.Text = data.name;
                    oDoc.Bookmarks["client2"].Range.Text = data.name;
                    oDoc.Bookmarks["RNN"].Range.Text = data.RNN;
                    oDoc.Bookmarks["RNN2"].Range.Text = data.RNN;
                    oDoc.Bookmarks["address"].Range.Text = data.point2;
                    oDoc.Bookmarks["address2"].Range.Text = data.point2;
                    oDoc.Bookmarks["markB"].Range.Text = data.material;
                    oDoc.Bookmarks["markB2"].Range.Text = data.material;
                    oDoc.Bookmarks["driver"].Range.Text = data.driver;
                    oDoc.Bookmarks["driver2"].Range.Text = data.driver;
                    oDoc.Bookmarks["driver3"].Range.Text = data.driver;
                    oDoc.Bookmarks["driver4"].Range.Text = data.driver;
                    oDoc.Bookmarks["volume"].Range.Text = data.volume.ToString();
                    oDoc.Bookmarks["volume2"].Range.Text = data.volume.ToString();
                    oDoc.Bookmarks["carNum"].Range.Text = data.carNumber;
                    oDoc.Bookmarks["carNum2"].Range.Text = data.carNumber;
                    oDoc.Bookmarks["carNum3"].Range.Text = data.carNumber;
                    oDoc.Bookmarks["carNum4"].Range.Text = data.carNumber;
                    oDoc.Bookmarks["brutto"].Range.Text = data.brutto.ToString();
                    oDoc.Bookmarks["brutto2"].Range.Text = data.brutto.ToString();
                    oDoc.Bookmarks["carWeight"].Range.Text = data.carWeight.ToString();
                    oDoc.Bookmarks["carWeight2"].Range.Text = data.carWeight.ToString();
                    oDoc.Bookmarks["netto"].Range.Text = data.factorWeight.ToString();
                    oDoc.Bookmarks["netto2"].Range.Text = data.factorWeight.ToString();
                    oDoc.Bookmarks["seal"].Range.Text = data.seal;
                    oDoc.Bookmarks["seal2"].Range.Text = data.seal;
                    oDoc.Bookmarks["seal3"].Range.Text = data.seal;
                    oDoc.Bookmarks["seal4"].Range.Text = data.seal;
                    oDoc.Bookmarks["post"].Range.Text = data.FIO;
                    oDoc.Bookmarks["post2"].Range.Text = data.FIO;
                    oDoc.Bookmarks["post3"].Range.Text = data.FIO;
                    oDoc.Bookmarks["post4"].Range.Text = data.FIO;
                    #endregion

                    #region Сохранение документа и вывод на печать
                    string basePath = Environment.CurrentDirectory + "\\Reports";
                    System.IO.Directory.CreateDirectory(basePath);
                    string reportFile = basePath + $"Накладная 1 {DateTime.Now.ToString().Replace(':', '-')}.docx";
                    oDoc.SaveAs(reportFile);
                    oDoc.Close();
                    oDoc = oWord.Documents.Open(reportFile);

                    oDoc.Activate();
                    oWord.Visible = true;

                    var dialogResult = oWord.Dialogs[WdWordDialog.wdDialogFilePrint].Show();

                    if (dialogResult == 1)
                        oDoc.PrintOut();
                    #endregion
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }

            return true;
        }

        public static bool nakl2(DateTime date)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Word._Application oWord = new Word.Application();
                    _Document oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\templates\\nakl2.dotx");

                    var data = (from i in context.Weighings
                                where i.dateWeight == date
                                join cj in context.Clients on i.clientId equals cj.id into cj
                                from c in cj.DefaultIfEmpty()
                                join uj in context.Users on i.userId equals uj.id into uj
                                from u in uj.DefaultIfEmpty()
                                join caU in context.Carriers on i.carrierid equals caU.id into caj
                                from ca in caj.DefaultIfEmpty()
                                join dU in context.Drivers on i.driverId equals dU.id into dj
                                from d in dj.DefaultIfEmpty()
                                join gU in context.Goods on i.material equals gU.name into gj
                                from g in gj.DefaultIfEmpty()
                                select new { i.id, carrierName = ca.name, i.dateOut, clientName = c.name, c.RNN, i.carMark, i.brutto, i.carWeight, i.factorWeight, u.FIO, i.driver, d.udl, i.material, g.price, allPrice = i.price, i.carNumber }).FirstOrDefault();

                    #region Запись в документ по меткам
                    oDoc.Bookmarks["startDate"].Range.Text = date.ToString();
                    oDoc.Bookmarks["num"].Range.Text = data.id.ToString();
                    oDoc.Bookmarks["carrier"].Range.Text = data.carrierName;
                    oDoc.Bookmarks["endDate"].Range.Text = data.dateOut.ToString();
                    oDoc.Bookmarks["client"].Range.Text = data.clientName;
                    oDoc.Bookmarks["RNN"].Range.Text = data.RNN;
                    oDoc.Bookmarks["carMarkNum"].Range.Text = data.carMark + " " + data.carNumber;
                    oDoc.Bookmarks["brutto"].Range.Text = data.brutto.ToString();
                    oDoc.Bookmarks["carWeight"].Range.Text = data.carWeight.ToString();
                    oDoc.Bookmarks["netto"].Range.Text = data.factorWeight.ToString();
                    oDoc.Bookmarks["post"].Range.Text = data.FIO;
                    oDoc.Bookmarks["driver"].Range.Text = data.driver;
                    oDoc.Bookmarks["driver2"].Range.Text = data.driver;
                    oDoc.Bookmarks["udl"].Range.Text = data.udl;
                    oDoc.Bookmarks["goods"].Range.Text = data.material;
                    oDoc.Bookmarks["price"].Range.Text = data.price.ToString();
                    oDoc.Bookmarks["allPrice"].Range.Text = data.allPrice.ToString();
                    #endregion

                    #region Сохранение документа и вывод на печать
                    string basePath = Environment.CurrentDirectory + "\\Reports";
                    System.IO.Directory.CreateDirectory(basePath);
                    string reportFile = basePath + $"Накладная 2 {DateTime.Now.ToString().Replace(':', '-')}.docx";
                    oDoc.SaveAs(reportFile);
                    oDoc.Close();
                    oDoc = oWord.Documents.Open(reportFile);

                    oDoc.Activate();
                    oWord.Visible = true;

                    oWord.ShowStartupDialog = false;

                    var dialogResult = oWord.Dialogs[WdWordDialog.wdDialogFilePrint].Show();

                    if (dialogResult == 1)
                        oDoc.PrintOut();
                    #endregion
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }

            return true;
        }

        public static bool weighingTalon(DateTime date)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Word._Application oWord = new Word.Application();
                    _Document oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\templates\\weighing.dotx");

                    var data = (from i in context.Weighings
                                where i.dateWeight == date
                                join c in context.Clients on i.clientId equals c.id
                                join u in context.Users on i.userId equals u.id
                                select new
                                {
                                    i.id,
                                    i.dateOut,
                                    clientName = c.name,
                                    c.RNN,
                                    i.carNumber,
                                    i.carMark,
                                    i.consignee,
                                    i.brutto,
                                    i.carWeight,
                                    i.factorWeight,
                                    u.FIO,
                                    i.driver,
                                    i.material,
                                    i.point1,
                                    i.point2,
                                    i.docWeight,
                                    i.adjustment,
                                    i.operationType
                                }).FirstOrDefault();

                    #region Запись в документ по меткам
                    oDoc.Bookmarks["startDate"].Range.Text = date.ToString();
                    oDoc.Bookmarks["startDate2"].Range.Text = date.ToString();
                    oDoc.Bookmarks["num"].Range.Text = data.id.ToString();
                    oDoc.Bookmarks["num2"].Range.Text = data.id.ToString();
                    oDoc.Bookmarks["endDate"].Range.Text = data.dateOut.ToString();
                    oDoc.Bookmarks["endDate2"].Range.Text = data.dateOut.ToString();
                    oDoc.Bookmarks["client"].Range.Text = data.clientName;
                    oDoc.Bookmarks["client2"].Range.Text = data.clientName;
                    oDoc.Bookmarks["RNN"].Range.Text = data.RNN;
                    oDoc.Bookmarks["RNN2"].Range.Text = data.RNN;
                    oDoc.Bookmarks["carNum"].Range.Text = data.carNumber;
                    oDoc.Bookmarks["carNum2"].Range.Text = data.carNumber;
                    oDoc.Bookmarks["carMark"].Range.Text = data.carMark;
                    oDoc.Bookmarks["carMark2"].Range.Text = data.carMark;
                    oDoc.Bookmarks["consignee"].Range.Text = data.consignee;
                    oDoc.Bookmarks["consignee2"].Range.Text = data.consignee;
                    oDoc.Bookmarks["fullCarWeight"].Range.Text = data.brutto.ToString();
                    oDoc.Bookmarks["fullCarWeight2"].Range.Text = data.brutto.ToString();
                    oDoc.Bookmarks["carWeight"].Range.Text = data.carWeight.ToString();
                    oDoc.Bookmarks["carWeight2"].Range.Text = data.carWeight.ToString();
                    oDoc.Bookmarks["factorWeight"].Range.Text = data.factorWeight.ToString();
                    oDoc.Bookmarks["factorWeight2"].Range.Text = data.factorWeight.ToString();
                    oDoc.Bookmarks["post"].Range.Text = data.FIO;
                    oDoc.Bookmarks["post2"].Range.Text = data.FIO;
                    oDoc.Bookmarks["driver"].Range.Text = data.driver;
                    oDoc.Bookmarks["driver2"].Range.Text = data.driver;
                    oDoc.Bookmarks["goods"].Range.Text = data.material;
                    oDoc.Bookmarks["goods2"].Range.Text = data.material;
                    oDoc.Bookmarks["point1"].Range.Text = data.point1;
                    oDoc.Bookmarks["point12"].Range.Text = data.point1;
                    oDoc.Bookmarks["point2"].Range.Text = data.point2;
                    oDoc.Bookmarks["point22"].Range.Text = data.point2;
                    oDoc.Bookmarks["docWeight"].Range.Text = data.docWeight.ToString();
                    oDoc.Bookmarks["docWeight2"].Range.Text = data.docWeight.ToString();
                    oDoc.Bookmarks["overload"].Range.Text = data.docWeight != 0 ? (data.factorWeight - data.docWeight).ToString() : "";
                    oDoc.Bookmarks["overload2"].Range.Text = data.docWeight != 0 ? (data.factorWeight - data.docWeight).ToString() : "";
                    oDoc.Bookmarks["coef"].Range.Text = data.adjustment.ToString();
                    oDoc.Bookmarks["coef2"].Range.Text = data.adjustment.ToString();
                    oDoc.Bookmarks["dryWeight"].Range.Text = data.adjustment != 0 && data.adjustment != null ? Math.Round((data.factorWeight ?? 0) + ((data.factorWeight ?? 0) * ((data.adjustment ?? 0) / 100))).ToString() : "";
                    oDoc.Bookmarks["dryWeight2"].Range.Text = data.adjustment != 0 && data.adjustment != null ? Math.Round((data.factorWeight ?? 0) + ((data.factorWeight ?? 0) * ((data.adjustment ?? 0) / 100))).ToString() : "";
                    oDoc.Bookmarks["type"].Range.Text = data.operationType;
                    oDoc.Bookmarks["type2"].Range.Text = data.operationType;
                    #endregion

                    #region Сохранение документа и вывод на печать
                    string basePath = Environment.CurrentDirectory + "\\Reports";
                    System.IO.Directory.CreateDirectory(basePath);
                    string reportFile = basePath + $"Талон о взвешивании {DateTime.Now.ToString().Replace(':', '-')}.docx";
                    oDoc.SaveAs(reportFile);
                    oDoc.Close();
                    oDoc = oWord.Documents.Open(reportFile);

                    oDoc.Activate();
                    oWord.Visible = true;

                    oWord.ShowStartupDialog = false;

                    var dialogResult = oWord.Dialogs[WdWordDialog.wdDialogFilePrint].Show();

                    if (dialogResult == 1)
                        oDoc.PrintOut();
                    #endregion
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }


            return true;
        }

        public static bool ttn(DateTime date)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Word._Application oWord = new Word.Application();
                    _Document oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\templates\\ttn.dotx");

                    string dateNow = DateTime.Now.Date.ToString("D");
                    var data = (from i in context.Weighings
                                where i.dateWeight == date
                                join g in context.Goods on i.material equals g.name
                                join u in context.Users on i.userId equals u.id
                                select new { i.id, i.consignee, i.consignor, i.carNumber, i.driver, i.brutto, i.factorWeight, g.price, allPrice = i.price, u.FIO, i.material }).FirstOrDefault();

                    #region Запись в документ по меткам
                    oDoc.Bookmarks["day"].Range.Text = dateNow.Split()[0];
                    oDoc.Bookmarks["month"].Range.Text = dateNow.Split()[1];
                    oDoc.Bookmarks["year"].Range.Text = dateNow.Split()[2];
                    oDoc.Bookmarks["num"].Range.Text = data.id.ToString();
                    oDoc.Bookmarks["consignee"].Range.Text = data.consignee;
                    oDoc.Bookmarks["consignor"].Range.Text = data.consignor;
                    oDoc.Bookmarks["carNum"].Range.Text = data.carNumber;
                    oDoc.Bookmarks["driver"].Range.Text = data.driver;
                    oDoc.Bookmarks["brutto"].Range.Text = (data.brutto / 1000).ToString();
                    oDoc.Bookmarks["netto"].Range.Text = (data.factorWeight / 1000).ToString();
                    oDoc.Bookmarks["netto2"].Range.Text = (data.factorWeight / 1000).ToString();
                    oDoc.Bookmarks["price"].Range.Text = data.price.ToString();
                    oDoc.Bookmarks["sum"].Range.Text = data.allPrice.ToString();
                    oDoc.Bookmarks["sum2"].Range.Text = data.allPrice.ToString();
                    oDoc.Bookmarks["post"].Range.Text = data.FIO;
                    oDoc.Bookmarks["material"].Range.Text = data.material;
                    #endregion

                    #region Сохранение документа и вывод на печать
                    string basePath = Environment.CurrentDirectory + "\\Reports";
                    System.IO.Directory.CreateDirectory(basePath);
                    string reportFile = basePath + $"Товаро-транспортная накладная {DateTime.Now.ToString().Replace(':', '-')}.docx";
                    oDoc.SaveAs(reportFile);
                    oDoc.Close();
                    oDoc = oWord.Documents.Open(reportFile);

                    oDoc.Activate();
                    oWord.Visible = true;

                    oWord.ShowStartupDialog = false;

                    var dialogResult = oWord.Dialogs[WdWordDialog.wdDialogFilePrint].Show();

                    if (dialogResult == 1)
                        oDoc.PrintOut();
                    #endregion
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }


            return true;
        }

        public static bool dayReport(DateTime date, DateTime time, bool notFinish)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Word._Application oWord = new Word.Application();
                    _Document oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\templates\\dayReport.docx");
                    Word.Table wTable = oDoc.Tables[1];

                    #region Получение данных с таблицы
                    var temp = (from i in context.Weighings
                                where i.carNumber != "" &&
                                (i.brutto != null && i.carWeight != null)
                                select i).AsEnumerable();

                    DateTime dateTimeFrom = date.Date + time.TimeOfDay;
                    DateTime dateTimeBefore = dateTimeFrom.AddHours(24);

                    var table = from i in temp
                                where (i.dateWeight >= dateTimeFrom) &&
                                (i.dateWeight <= dateTimeBefore)
                                select i;

                    if (!notFinish)
                    {
                        table = from i in table
                                where (i.brutto != 0 && i.brutto != null) &&
                                (i.carWeight != 0 && i.carWeight != null)
                                select i;
                    }

                    #endregion

                    oDoc.Bookmarks["day"].Range.Text = date.Date.ToString().Split()[0];
                    oDoc.Bookmarks["time"].Range.Text = time.TimeOfDay.ToString();

                    #region Переменные для строки 'Итого'
                    double netto = 0;
                    double brutto = 0;
                    double tara = 0;
                    double doc = 0;
                    double adj = 0;
                    double volume = 0;

                    int row;
                    int j = 0;
                    double overweight = 0;
                    #endregion

                    #region Заполнение таблицы
                    foreach (Weighings w in table)
                    {
                        wTable.Rows.Add();
                        row = wTable.Rows.Count;
                        j++;
                        wTable.Cell(row, 1).Range.Text = j.ToString();
                        wTable.Cell(row, 2).Range.Text = w.dateWeight + " " + w.dateOut;
                        wTable.Cell(row, 3).Range.Text = (from i in context.Clients where i.id == w.clientId select i.name).FirstOrDefault();
                        wTable.Cell(row, 4).Range.Text = w.carNumber;
                        wTable.Cell(row, 5).Range.Text = w.carMark;
                        wTable.Cell(row, 6).Range.Text = w.material;
                        wTable.Cell(row, 7).Range.Text = w.factorWeight.ToString();
                        wTable.Cell(row, 8).Range.Text = w.brutto.ToString();
                        wTable.Cell(row, 9).Range.Text = w.carWeight.ToString();
                        wTable.Cell(row, 10).Range.Text = w.docWeight.ToString();
                        if (w.factorWeight != 0 && w.factorWeight != null && w.adjustment != 0 && w.adjustment != null)
                        {
                            double adjstmt = (w.factorWeight ?? 0) * (w.adjustment ?? 0) / 100;
                            wTable.Cell(row, 11).Range.Text = Math.Round((w.factorWeight ?? 0) + adjstmt).ToString();
                        }
                        else
                            wTable.Cell(row, 11).Range.Text = w.factorWeight.ToString(); ;
                        wTable.Cell(row, 12).Range.Text = w.adjustment != 0 ? w.adjustment.ToString() : "";
                        wTable.Cell(row, 13).Range.Text = w.volume != 0 && w.volume != null ? w.volume.ToString() : "";
                        wTable.Cell(row, 14).Range.Text = w.volume != 0 && w.volume != null ? Math.Round((w.factorWeight ?? 0) / (w.volume ?? 0), 3).ToString() : "";
                        overweight = w.docWeight != 0 && w.docWeight != null ? ((w.factorWeight ?? 0) - (w.docWeight ?? 0)) : 0;
                        wTable.Cell(row, 15).Range.Text = overweight != 0 ? overweight.ToString() : "";  
                        wTable.Cell(row, 16).Range.Text = (from i in context.Users where i.id == w.userId select i.FIO).FirstOrDefault();
                        wTable.Cell(row, 17).Range.Text = w.point2;
                        wTable.Cell(row, 18).Range.Text = w.operationType;

                        #region Подсчет данных для строки 'Итого'
                        netto += w.factorWeight ?? 0;
                        brutto += w.brutto ?? 0;
                        tara += w.carWeight ?? 0;
                        volume += w.volume ?? 0;
                        doc += w.docWeight ?? 0;
                        //adj += ((w.adjustment ?? 0) * (w.factorWeight ?? 0)) / 100;
                        double adjustment = (w.adjustment ?? 0);
                        adj += adjustment != 0 ? ((w.factorWeight ?? 0) + Math.Round((w.factorWeight ?? 0) * adjustment / 100)) : (w.factorWeight ?? 0);
                        #endregion

                        #region Изменение размера шрифта
                        for (int a = 1; a < 19; a++)
                        {
                            wTable.Cell(row, a).Range.Font.Size = 8;
                        }
                        #endregion
                    }
                    #endregion

                    #region Строка 'Итого' и стили для неё
                    wTable.Rows.Add();
                    row = wTable.Rows.Count;
                    for (int i = 0; i < 6; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleNone;
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    wTable.Cell(row, 12).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    for (int i = 14; i < 19; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    wTable.Cell(row, 6).Range.Text = "Итого:";
                    wTable.Cell(row, 7).Range.Text = netto.ToString();
                    wTable.Cell(row, 8).Range.Text = brutto.ToString();
                    wTable.Cell(row, 9).Range.Text = tara.ToString();
                    wTable.Cell(row, 10).Range.Text = doc.ToString();
                    wTable.Cell(row, 11).Range.Text = Math.Round(adj).ToString();
                    wTable.Cell(row, 13).Range.Text = volume.ToString();
                    for (int i = 1; i < 14; i++)
                        wTable.Cell(row, i).Range.Bold = 1;
                    for (int i = 1; i < 11; i++)
                        wTable.Cell(row, i).Range.Font.Size = 9;
                    wTable.Rows.Add();
                    row = wTable.Rows.Count;
                    for (int i = 1; i < 6; i++)
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 12).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    for (int i = 8; i < 19; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    for (int i = 14; i < 19; i++)
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 6).Range.Text = "Кол-во:";
                    wTable.Cell(row, 6).Range.Bold = 1;
                    wTable.Cell(row, 7).Range.Text = j.ToString();
                    wTable.Cell(row, 7).Range.Bold = 1;
                    #endregion

                    #region Сохранение документа и вывод на печать
                    try
                    {
                        oDoc.SaveAs("tmp.docx");
                        oDoc.Close();
                        oDoc = oWord.Documents.Open("tmp.docx");

                        oDoc.Activate();
                        oWord.Visible = true;

                        oWord.ShowStartupDialog = false;

                        var dialogResult = oWord.Dialogs[WdWordDialog.wdDialogFilePrint].Show();

                        if (dialogResult == 1)
                            oDoc.PrintOut();


                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Завершите открытый отчет", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }
            return true;

        }

        public static bool dayReportXls(DateTime date, DateTime time, bool notFinish)
        {
            try
            {
                #region Открытие файла
                Excel.Application oExcel = null;
                Excel.Workbook ExcelWorkBook = null;
                Excel.Sheets ExcelSheets = null;
                Excel.Worksheet MySheet = null;
                Process[] excelPcs = Process.GetProcessesByName("EXCEL");
                if (excelPcs.Length > 0)
                {
                    oExcel = (Microsoft.Office.Interop.Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                    oExcel.SheetsInNewWorkbook = 1;
                    ExcelWorkBook = oExcel.Workbooks.Add(Environment.CurrentDirectory + "\\templates\\dayReport.xlsx");
                    ExcelSheets = ExcelWorkBook.Worksheets;
                    MySheet = (Excel.Worksheet)ExcelSheets.get_Item(1);
                }
                else
                {
                    oExcel = new Excel.Application();
                    ExcelWorkBook = oExcel.Workbooks.Add(Environment.CurrentDirectory + "\\templates\\dayReport.xlsx");
                    ExcelSheets = ExcelWorkBook.Worksheets;
                    MySheet = (Excel.Worksheet)ExcelSheets.get_Item(1);
                }

                //new Excel.Application();
                #endregion

                using (DataBaseContext context = new DataBaseContext())
                {
                    #region Получение данных с таблицы
                    DateTime dateTimeFrom = date.Date + time.TimeOfDay;
                    DateTime dateTimeBefore = dateTimeFrom.AddHours(24);

                    var table = (from i in context.Weighings
                                where i.carNumber != "" &&
                                (i.brutto != null && i.carWeight != null) &&
                                (i.dateWeight >= dateTimeFrom) &&
                                (i.dateWeight <= dateTimeBefore)
                                select i).AsEnumerable();

                    if (!notFinish)
                    {
                        table = from i in table
                                where (i.brutto != 0 && i.brutto != null) &&
                                (i.carWeight != 0 && i.carWeight != null)
                                select i;
                    }
                    #endregion

                    #region Заполнение параметров
                    string day = MySheet.Range["Day"].Value;
                    MySheet.Range["Day"].Value = $"{day} {date.Date.ToString().Split()[0]}";
                    MySheet.Range["TimeFrom"].Value = time.TimeOfDay.ToString();
                    MySheet.Range["TimeFrom"].Style.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    #endregion

                    int count = 1;
                    int StartCell = 5;
                    int LengthCell = 5;

                    #region Заполнение таблицы
                    foreach (Weighings w in table)
                    {
                        MySheet.Range[$"A{LengthCell}:R{LengthCell}"].Style.WrapText = true;
                        MySheet.Cells[LengthCell, "A"].Value = count;
                        MySheet.Cells[LengthCell, "B"].Value = w.dateWeight.ToString() + "\n" + w.dateOut.ToString();
                        MySheet.Cells[LengthCell, "C"].Value = (from i in context.Clients where i.id == w.clientId select i.name).FirstOrDefault();
                        MySheet.Cells[LengthCell, "D"].Value = w.carNumber.ToString();
                        MySheet.Cells[LengthCell, "E"].Value = w.carMark.ToString();
                        MySheet.Cells[LengthCell, "F"].Value = w.material.ToString();
                        MySheet.Cells[LengthCell, "G"].Value = w.factorWeight.ToString();
                        MySheet.Cells[LengthCell, "H"].Value = w.brutto.ToString();
                        MySheet.Cells[LengthCell, "I"].Value = w.carWeight.ToString();
                        MySheet.Cells[LengthCell, "J"].Value = w.docWeight.ToString();
                        if (w.factorWeight != 0 && w.factorWeight != null && w.adjustment != 0 && w.adjustment != null)
                        {
                            double adjstmt = (w.factorWeight ?? 0) * (w.adjustment ?? 0) / 100;
                            MySheet.Cells[LengthCell, "K"].Value = Math.Round((w.factorWeight ?? 0) + adjstmt).ToString();
                        }
                        else
                            MySheet.Cells[LengthCell, "K"].Value = w.factorWeight.ToString();

                        MySheet.Cells[LengthCell, "L"].Value = w.adjustment != 0 ? w.adjustment.ToString() : "";
                        MySheet.Cells[LengthCell, "M"].Value = w.volume != 0 && w.volume != null ? w.volume.ToString() : "";
                        MySheet.Cells[LengthCell, "N"].Value = w.volume != 0 && w.volume != null ? Math.Round((w.factorWeight ?? 0) / (w.volume ?? 0), 3).ToString() : "";
                        var overweight = w.docWeight != 0 && w.docWeight != null ? ((w.factorWeight ?? 0) - (w.docWeight ?? 0)) : 0;
                        MySheet.Cells[LengthCell, "O"].Value = overweight != 0 ? overweight.ToString() : "";
                        MySheet.Cells[LengthCell, "P"].Value = (from i in context.Users where i.id == w.userId select i.FIO).FirstOrDefault();
                        MySheet.Cells[LengthCell, "Q"].Value = w.point2;
                        MySheet.Cells[LengthCell, "R"].Value = w.operationType;

                        MySheet.Range[$"A{LengthCell}:R{LengthCell}"].Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        MySheet.Range[$"A{LengthCell}:R{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        

                        count++;
                        LengthCell++;
                                                

                    }
                    #endregion

                    #region Заполнение Итого
                    MySheet.Cells[LengthCell, "E"].Value = "Итого:";
                    MySheet.Range[$"E{LengthCell}"].Style.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    MySheet.Range[$"E{LengthCell}:F{LengthCell}"].Merge();

                    MySheet.Cells[LengthCell, "G"].Formula = $"=SUM(G{StartCell}:G{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "H"].Formula = $"=SUM(H{StartCell}:H{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "I"].Formula = $"=SUM(I{StartCell}:I{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "J"].Formula = $"=SUM(J{StartCell}:J{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "K"].Formula = $"=SUM(K{StartCell}:K{LengthCell - 1})";
                    MySheet.Range[$"G{LengthCell}:K{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

                    MySheet.Cells[LengthCell, "M"].Formula = $"=SUM(M{StartCell}:M{LengthCell - 1})";
                    MySheet.Range[$"M{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

                    LengthCell++;
                    #endregion

                    #region Заполнение Кол-во
                    //MySheet.Range[$"A{LengthCell}:R{LengthCell}"].Style.WrapText = true;
                    MySheet.Range[$"C{LengthCell}:F{LengthCell}"].Merge();
                    MySheet.Cells[LengthCell, "C"].Value = "Количество взвешиваний:";
                    MySheet.Range[$"E{LengthCell}"].Style.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    MySheet.Range[$"C{LengthCell}:F{LengthCell}"].Merge();
                    MySheet.Cells[LengthCell, "G"].Formula = $"=COUNT(A{StartCell}:A{LengthCell - 1})";
                    #endregion

                    #region Запись файла
                    try
                    {
                        oExcel.Application.DisplayAlerts = false;
                        MySheet.PageSetup.Zoom = false;
                        MySheet.PageSetup.FitToPagesWide = 1;
                        MySheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                        string basePath = Environment.CurrentDirectory + "\\Reports";
                        System.IO.Directory.CreateDirectory(basePath);
                        string reportFile = basePath + $"\\Дневной отчет {DateTime.Now.ToString().Replace(':', '-')}.xlsx";
                        ExcelWorkBook.SaveAs(reportFile, Type.Missing, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                            Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing
                        ExcelWorkBook.Close();
                        ExcelWorkBook = oExcel.Workbooks.Open(reportFile);
                        ExcelWorkBook.Activate();
                        oExcel.Visible = true;
                        oExcel.ShowStartupDialog = false;
                        var dialogResult = oExcel.Dialogs[Excel.XlBuiltInDialog.xlDialogPrint].Show();

                        if (dialogResult == true)
                            ExcelWorkBook.PrintPreview();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }


            return true;
        }


        public static bool monthReport(DateTime date, bool notFinish)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Word._Application oWord = new Word.Application();
                    _Document oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\templates\\monthReport.docx");
                    Word.Table wTable = oDoc.Tables[1];

                    #region Получение данных с таблицы
                    var table = from i in context.Weighings
                                where i.dateWeight.Month == date.Month &&
                                (i.carWeight != null && i.brutto != null) &&
                                i.carNumber != ""
                                select i;
                    if (!notFinish)
                    {
                        table = from i in table
                                where (i.brutto != 0 && i.brutto != null) &&
                                      (i.factorWeight != 0 && i.carWeight != null)
                                select i;
                    }
                    #endregion

                    oDoc.Bookmarks["month"].Range.Text = date.ToString("MMMM.yyyy");

                    #region Переменные для строки 'Итого'
                    double netto = 0;
                    double brutto = 0;
                    double tara = 0;
                    double doc = 0;
                    double adj = 0;
                    double volume = 0;
                    int row;
                    int j = 0;

                    double overweight = 0;
                    #endregion

                    #region Заполнение таблицы
                    foreach (Weighings w in table)
                    {
                        wTable.Rows.Add();
                        row = wTable.Rows.Count;
                        j++;
                        wTable.Cell(row, 1).Range.Text = j.ToString();
                        wTable.Cell(row, 2).Range.Text = w.dateWeight + " " + w.dateOut;
                        wTable.Cell(row, 3).Range.Text = (from i in context.Clients where i.id == w.clientId select i.name).FirstOrDefault();
                        wTable.Cell(row, 4).Range.Text = w.carNumber;
                        wTable.Cell(row, 5).Range.Text = w.carMark;
                        wTable.Cell(row, 6).Range.Text = w.material;
                        wTable.Cell(row, 7).Range.Text = w.factorWeight.ToString();
                        wTable.Cell(row, 8).Range.Text = w.brutto.ToString();
                        wTable.Cell(row, 9).Range.Text = w.carWeight.ToString();
                        wTable.Cell(row, 10).Range.Text = w.docWeight.ToString();
                        if (w.factorWeight != 0 && w.factorWeight != null && w.adjustment != 0 && w.adjustment != null)
                        {
                            double adjstmt = (w.factorWeight ?? 0) * (w.adjustment ?? 0) / 100;
                            wTable.Cell(row, 11).Range.Text = Math.Round((w.factorWeight ?? 0) + adjstmt).ToString();
                        }
                        else
                            wTable.Cell(row, 11).Range.Text = w.factorWeight.ToString();
                        wTable.Cell(row, 12).Range.Text = w.adjustment != 0? w.adjustment.ToString(): "";
                        wTable.Cell(row, 13).Range.Text = w.volume != 0 && w.volume != null ? w.volume.ToString() : "";
                        wTable.Cell(row, 14).Range.Text = w.volume != 0  && w.volume != null ? Math.Round((w.factorWeight ?? 0) / (w.volume ?? 0), 3).ToString() : "";
                        overweight = w.docWeight != 0 && w.docWeight != null ? ((w.factorWeight ?? 0) - (w.docWeight ?? 0)) : 0;
                        wTable.Cell(row, 15).Range.Text = overweight != 0 ? overweight.ToString() : "";
                        wTable.Cell(row, 16).Range.Text = (from i in context.Users where i.id == w.userId select i.FIO).FirstOrDefault();
                        wTable.Cell(row, 17).Range.Text = w.point2;
                        wTable.Cell(row, 18).Range.Text = w.operationType;

                        #region Подсчет данных для строки 'Итого'
                        netto += w.factorWeight ?? 0;
                        brutto += w.brutto ?? 0;
                        tara += w.carWeight ?? 0;
                        volume += w.volume ?? 0;
                        doc += w.docWeight ?? 0;
                        double adjustment = (w.adjustment ?? 0);
                        adj += adjustment != 0 ? ((w.factorWeight ?? 0) + Math.Round((w.factorWeight ?? 0) * adjustment / 100)) : (w.factorWeight ?? 0);
                        #endregion

                        #region Изменение размера шрифта
                        for (int a = 1; a < 19; a++)
                        {
                            wTable.Cell(row, a).Range.Font.Size = 8;
                        }
                        #endregion
                    }
                    #endregion

                    #region Строка 'Итого' и стили для неё
                    wTable.Rows.Add();
                    row = wTable.Rows.Count;
                    for (int i = 0; i < 6; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleNone;
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    wTable.Cell(row, 12).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    for (int i = 14; i < 19; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    wTable.Cell(row, 6).Range.Text = "Итого:";
                    wTable.Cell(row, 7).Range.Text = netto.ToString();
                    wTable.Cell(row, 8).Range.Text = brutto.ToString();
                    wTable.Cell(row, 9).Range.Text = tara.ToString();
                    wTable.Cell(row, 10).Range.Text = doc.ToString();
                    wTable.Cell(row, 11).Range.Text = Math.Round(adj).ToString();
                    wTable.Cell(row, 13).Range.Text = volume.ToString();
                    for (int i = 1; i < 14; i++)
                        wTable.Cell(row, i).Range.Bold = 1;
                    for (int i = 1; i < 11; i++)
                        wTable.Cell(row, i).Range.Font.Size = 9;
                    wTable.Rows.Add();
                    row = wTable.Rows.Count;
                    for (int i = 1; i < 6; i++)
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 12).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    for (int i = 8; i < 19; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    for (int i = 14; i < 19; i++)
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 6).Range.Text = "Кол-во:";
                    wTable.Cell(row, 6).Range.Bold = 1;
                    wTable.Cell(row, 7).Range.Text = j.ToString();
                    wTable.Cell(row, 7).Range.Bold = 1;
                    #endregion

                    #region Сохранение документа и вывод на печать
                    try
                    {
                        oDoc.SaveAs("tmp.docx");
                        oDoc.Close();
                        oDoc = oWord.Documents.Open("tmp.docx");

                        oDoc.Activate();
                        oWord.Visible = true;

                        oWord.ShowStartupDialog = false;

                        var dialogResult = oWord.Dialogs[WdWordDialog.wdDialogFilePrint].Show();

                        if (dialogResult == 1)
                            oDoc.PrintOut();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Завершите открытый отчет", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion
                }
            }
            catch
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }
            return true;
        }

        public static bool monthReportXls(DateTime date, bool notFinish)
        {
            try
            {
                #region Открытие файла
                Excel.Application oExcel = new Excel.Application();
                Excel.Workbook ExcelWorkBook = oExcel.Workbooks.Add(Environment.CurrentDirectory + "\\templates\\monthReport.xlsx");
                Excel.Sheets ExcelSheets = ExcelWorkBook.Worksheets;
                Excel.Worksheet MySheet = (Excel.Worksheet)ExcelSheets.get_Item(1);
                #endregion

                #region Заполнение параметров
                string lable = MySheet.Range["Month"].Value;
                MySheet.Range["Month"].Value = $"{MySheet.Range["Month"].Value} {date.ToString("MMMM.yyyy")}";
                #endregion

                int StartCell = 4;
                int LengthCell = 4;
                int count = 1;

                using (DataBaseContext context = new DataBaseContext())
                {
                    #region Получение данных с таблицы
                    var table = from i in context.Weighings
                                where i.dateWeight.Month == date.Month &&
                                (i.carWeight != null && i.brutto != null) &&
                                i.carNumber != ""
                                select i;
                    if (!notFinish)
                    {
                        table = from i in table
                                where (i.brutto != 0 && i.brutto != null) &&
                                      (i.factorWeight != 0 && i.carWeight != null)
                                select i;
                    }
                    #endregion

                    #region Заполнение строк
                    foreach (Weighings w in table)
                    {
                        MySheet.Cells[LengthCell, "A"].Value = count;
                        MySheet.Cells[LengthCell, "B"].Value = w.dateWeight + "\n" + w.dateOut.ToString();
                        MySheet.Cells[LengthCell, "C"].Value = (from i in context.Clients where i.id == w.clientId select i.name).FirstOrDefault();
                        MySheet.Cells[LengthCell, "D"].Value = w.carNumber;
                        MySheet.Cells[LengthCell, "E"].Value = w.carMark;
                        MySheet.Cells[LengthCell, "F"].Value = w.material;
                        MySheet.Cells[LengthCell, "G"].Value = w.factorWeight.ToString();
                        MySheet.Cells[LengthCell, "H"].Value = w.brutto.ToString();
                        MySheet.Cells[LengthCell, "I"].Value = w.carWeight.ToString();
                        MySheet.Cells[LengthCell, "J"].Value = w.docWeight.ToString();
                        if (w.factorWeight != 0 && w.factorWeight != null && w.adjustment != 0 && w.adjustment != null)
                        {
                            double adjstmt = (w.factorWeight ?? 0) * (w.adjustment ?? 0) / 100;
                            MySheet.Cells[LengthCell, "K"].Value = Math.Round((w.factorWeight ?? 0) + adjstmt);
                        }
                        else
                            MySheet.Cells[LengthCell, "K"].Value = w.factorWeight.ToString();

                        MySheet.Cells[LengthCell, "L"].Value = w.adjustment != 0 ? w.adjustment.ToString() : "";
                        MySheet.Cells[LengthCell, "M"].Value = w.volume != 0 && w.volume != null ? w.volume.ToString() : "";
                        MySheet.Cells[LengthCell, "N"].Value = w.volume != 0 && w.volume != null ? Math.Round((w.factorWeight ?? 0) / (w.volume ?? 0), 3).ToString() : "";
                        var overweight = w.docWeight != 0 && w.docWeight != null ? ((w.factorWeight ?? 0) - (w.docWeight ?? 0)) : 0;
                        MySheet.Cells[LengthCell, "O"].Value = overweight != 0 ? overweight.ToString() : "";
                        MySheet.Cells[LengthCell, "P"].Value = (from i in context.Users where i.id == w.userId select i.FIO).FirstOrDefault();
                        MySheet.Cells[LengthCell, "Q"].Value = w.point2;
                        MySheet.Cells[LengthCell, "R"].Value = w.operationType;

                       
                        MySheet.Range[$"A{LengthCell}:R{LengthCell}"].Style.WrapText = true;
                        MySheet.Range[$"A{LengthCell}:R{LengthCell}"].Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        MySheet.Range[$"A{LengthCell}:R{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        count++;
                        LengthCell++;
                    }
                    MySheet.Columns["B"].ColumnWidth = 18;
                    MySheet.Range[$"B{LengthCell}:R{LengthCell}"].Columns.AutoFit();
                    #endregion

                    #region Заполнение Итого
                    MySheet.Range[$"E{LengthCell}:F{LengthCell}"].Merge(); 
                    MySheet.Cells[LengthCell, "E"].Value = "Итого:";
                    //MySheet.Range[$"A{LengthCell}:T{LengthCell}"].Style.WrapText = true;
                    MySheet.Range[$"E{LengthCell}"].Style.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    MySheet.Cells[LengthCell, "G"].Formula = $"=SUM(G{StartCell}:G{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "H"].Formula = $"=SUM(H{StartCell}:H{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "I"].Formula = $"=SUM(I{StartCell}:I{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "J"].Formula = $"=SUM(J{StartCell}:J{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "K"].Formula = $"=SUM(K{StartCell}:K{LengthCell - 1})";
                    MySheet.Range[$"G{LengthCell}:K{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    //MySheet.Range[$"G{LengthCell}:K{LengthCell}"].Style.WrapText = false;

                    MySheet.Cells[LengthCell, "M"].Formula = $"=SUM(M{StartCell}:M{LengthCell - 1})";
                    MySheet.Range[$"M{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

                    LengthCell++;
                    #endregion

                    #region Заполнение Кол-во
                    MySheet.Range[$"C{LengthCell}:F{LengthCell}"].Merge();
                    MySheet.Cells[LengthCell, "C"].Value = "Количество взвешиваний:";
                    MySheet.Range[$"E{LengthCell}"].Style.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    MySheet.Range[$"C{LengthCell}:F{LengthCell}"].Merge();
                    MySheet.Cells[LengthCell, "G"].Formula = $"=COUNT(A{StartCell}:A{LengthCell - 1})";
                    #endregion
                }

                #region Запись файла
                try
                {
                    oExcel.Application.DisplayAlerts = false;
                    MySheet.PageSetup.Zoom = false;
                    MySheet.PageSetup.FitToPagesWide = 1;
                    MySheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                    string basePath = Environment.CurrentDirectory + "\\Reports";
                    System.IO.Directory.CreateDirectory(basePath);
                    string reportFile = basePath + $"\\Отчет за месяц {DateTime.Now.ToString().Replace(':','-')}.xlsx";
                    ExcelWorkBook.SaveAs(reportFile, Type.Missing, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                        Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing
                    ExcelWorkBook.Close();
                    ExcelWorkBook = oExcel.Workbooks.Open(reportFile);
                    ExcelWorkBook.Activate();
                    oExcel.Visible = true;
                    oExcel.ShowStartupDialog = false;
                    var dialogResult = oExcel.Dialogs[Excel.XlBuiltInDialog.xlDialogPrint].Show();

                    if (dialogResult == true)
                        ExcelWorkBook.PrintPreview();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                #endregion
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка составления отчета");
            }

            return true;
        }


        public static bool detalReport(DateTime dateFrom, DateTime timeFrom, DateTime dateBefore, DateTime timeBefore,
                                        string material, string role, string client, string carNum,
                                        string carMark, string driver, string point1, string point2,
                                        string consignee, string consignor, string type, string stock,
                                        string carrier, string orderNum, string orderPosNum, bool notFinish, 
                                        string adjCheck, string[] scales, bool edited)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Word._Application oWord = new Word.Application();
                    _Document oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\templates\\detalReport.docx");
                    Word.Table wTable = oDoc.Tables[1];

                    #region Получение данных с таблицы
                    var temp = (from i in context.Weighings
                                where i.carNumber != "" &&
                                (i.brutto != null && i.carWeight != null)
                                select i).AsEnumerable();

                    if (adjCheck != "Все")
                    {
                        if (adjCheck == "С корректировкой")
                        {
                            temp = from i in temp
                                   where i.adjustment != 0 && i.adjustment != null
                                   select i;
                        }
                        else if (adjCheck == "Без корректировки")
                        {
                            temp = from i in temp
                                   where i.adjustment == 0 || i.adjustment == null
                                   select i;
                        }
                    }



                    DateTime dateTimeFrom = dateFrom.Date + timeFrom.TimeOfDay;
                    DateTime dateTimeBefore = dateBefore.Date + timeBefore.TimeOfDay;
                    var table = from i in temp
                                where (i.dateWeight >= dateTimeFrom &&
                                        i.dateWeight <= dateTimeBefore)
                                select i;
                    if (!notFinish)
                    {
                        table = from i in table
                                where (i.brutto != 0 && i.brutto != null) &&
                                      (i.carWeight != 0 && i.carWeight != null)
                                select i;
                    }
                    if (edited)
                    {
                        table = from i in table
                                where i.edited != null
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(material))
                    {
                        table = from i in table
                                where i.material == material
                                select i;

                    }
                    if (!string.IsNullOrWhiteSpace(role))
                    {
                        table = from i in table
                                where i.userId == Int32.Parse(role)
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(client))
                    {
                        table = from i in table
                                join c in context.Clients on i.clientId equals c.id
                                where c.name == client
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(carNum))
                    {
                        table = from i in table
                                where i.carNumber == carNum
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(carMark))
                    {
                        table = from i in table
                                where i.carMark == carMark
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(driver))
                    {
                        table = from i in table
                                where i.driver == driver
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(point1))
                    {
                        table = from i in table
                                where i.point1 == point1
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(point2))
                    {
                        table = from i in table
                                where i.point2 == point2
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(consignee))
                    {
                        table = from i in table
                                where i.consignee == consignee
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(consignor))
                    {
                        table = from i in table
                                where i.consignor == consignor
                                select i;
                    }
                    if (type != "Любая")
                    {
                        table = from i in table
                                where i.operationType == type
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(stock))
                    {
                        table = from i in table
                                where i.storageId == Int32.Parse(stock)
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(carrier))
                    {
                        table = from i in table
                                where i.carrierid == Int32.Parse(carrier)
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(orderNum))
                    {
                        table = from i in table
                                where i.orderNumber == orderNum
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(orderPosNum))
                    {
                        table = from i in table
                                where i.orderPositionNumber == orderPosNum
                                select i;
                    }

                    #endregion

                    #region Заполнение полей фильтрации
                    oDoc.Bookmarks["startDate"].Range.Text = dateFrom.Date.ToString().Split()[0] + " " + timeFrom.ToString().Split()[1];
                    oDoc.Bookmarks["endDate"].Range.Text = dateBefore.Date.ToString().Split()[0] + " " + timeBefore.ToString().Split()[1];
                    oDoc.Bookmarks["material"].Range.Text = material;
                    var roleId = role != "" ? Int32.Parse(role) : 0;
                    oDoc.Bookmarks["role"].Range.Text = (from i in context.Users where i.id == roleId select i.FIO).FirstOrDefault();
                    oDoc.Bookmarks["client"].Range.Text = client;
                    oDoc.Bookmarks["carNum"].Range.Text = carNum;
                    oDoc.Bookmarks["carMark"].Range.Text = carMark;
                    oDoc.Bookmarks["driver"].Range.Text = driver;
                    oDoc.Bookmarks["point2"].Range.Text = point2;
                    oDoc.Bookmarks["type"].Range.Text = type;
                    var stockId = stock.Equals("") ? 0 : Int32.Parse(stock);
                    oDoc.Bookmarks["stock"].Range.Text = (from i in context.Storages where i.id == stockId select i.name).FirstOrDefault();
                    oDoc.Bookmarks["orderPosNum"].Range.Text = orderPosNum;
                    #endregion

                    #region Переменные для строки 'Итого'
                    double netto = 0;
                    double brutto = 0;
                    double tara = 0;
                    double doc = 0;
                    double adj = 0;
                    double volume = 0;
                    int row;
                    int j = 0;
                    List<Structures.scaleReport> scale = new List<Structures.scaleReport>();
                    List<Structures.redactorReport> redactors = new List<Structures.redactorReport>();
                    double overweight = 0;
                    #endregion

                    #region Заполнение таблицы
                    foreach (var w in table)
                    {
                        wTable.Rows.Add();
                        row = wTable.Rows.Count;
                        j++;
                        wTable.Cell(row, 1).Range.Text = j.ToString();
                        wTable.Cell(row, 2).Range.Text = w.dateWeight + " " + w.dateOut;
                        wTable.Cell(row, 3).Range.Text = (from i in context.Clients where i.id == w.clientId select i.name).FirstOrDefault();
                        wTable.Cell(row, 4).Range.Text = w.carNumber;
                        wTable.Cell(row, 5).Range.Text = w.carMark;
                        wTable.Cell(row, 6).Range.Text = w.material;
                        wTable.Cell(row, 7).Range.Text = w.driver;
                        wTable.Cell(row, 8).Range.Text = w.factorWeight.ToString();
                        wTable.Cell(row, 9).Range.Text = w.brutto.ToString();
                        wTable.Cell(row, 10).Range.Text = w.carWeight.ToString();
                        wTable.Cell(row, 11).Range.Text = w.docWeight.ToString();
                        if (w.factorWeight != 0 && w.factorWeight != null && w.adjustment != 0 && w.adjustment != null)
                        {
                            double adjstmt = (w.factorWeight ?? 0) * (w.adjustment ?? 0) / 100;
                            wTable.Cell(row, 12).Range.Text = Math.Round((w.factorWeight ?? 0) + adjstmt).ToString();
                        }
                        else
                            wTable.Cell(row, 12).Range.Text = w.factorWeight.ToString();
                        wTable.Cell(row, 13).Range.Text = w.adjustment != 0 ? w.adjustment.ToString() : "";
                        wTable.Cell(row, 14).Range.Text = w.volume != 0 && w.volume != null ? w.volume.ToString() : "";
                        wTable.Cell(row, 15).Range.Text = w.volume != 0 && w.volume != null ? Math.Round((w.factorWeight ?? 0) / (w.volume ?? 0), 3).ToString() : "";
                        overweight = w.docWeight != 0 && w.docWeight != null ? (w.factorWeight ?? 0 - w.docWeight ?? 0) : 0;
                        wTable.Cell(row, 16).Range.Text = overweight != 0 ? overweight.ToString() : "";
                        wTable.Cell(row, 17).Range.Text = (from i in context.Users where i.id == w.userId select i.FIO).FirstOrDefault();
                        wTable.Cell(row, 18).Range.Text = w.point2;
                        wTable.Cell(row, 19).Range.Text = w.operationType;
                        wTable.Cell(row, 20).Range.Text = (from i in context.Storages where i.id == w.storageId select i.name).FirstOrDefault();

                        WdColor color = WdColor.wdColorWhite;
                        if (scales.Length > 0)
                        {
                            for (int i = 0; i < scales.Length; i++)
                            {
                                if (w.taraScale == scales[i] || w.fullWeightScale == scales[i])
                                {
                                    color = WdColor.wdColorWhite;
                                    break;
                                }
                                else
                                {
                                    color = WdColor.wdColorYellow;
                                }
                            }
                        }
                        for (int i = 1; i < 21; i++)
                        {
                            wTable.Cell(row, i).Shading.BackgroundPatternColor = color;
                        }



                        #region Подсчет данных для строки 'Итого'
                        netto += w.factorWeight ?? 0;
                        brutto += w.brutto ?? 0;
                        tara += w.carWeight ?? 0;
                        volume += w.volume ?? 0;
                        doc += w.docWeight ?? 0;
                        double adjustment = (w.adjustment ?? 0);
                        adj += adjustment != 0 ? ((w.factorWeight ?? 0) + Math.Round((w.factorWeight ?? 0) * adjustment / 100)) : (w.factorWeight ?? 0);

                        if (w.taraScale != "" && w.taraScale != null)
                        {
                            if (scale.Count == 0 || !scale.Any(x => x.Scale == w.taraScale))
                            {
                                Structures.scaleReport scaleRow = new Structures.scaleReport
                                {
                                    Scale = w.taraScale
                                };
                                scale.Add(scaleRow);
                            }
                            scale.Find(x => x.Scale == w.taraScale).TaraWeight += w.carWeight ?? 0;
                        }

                        if (w.fullWeightScale != "" && w.fullWeightScale != null)
                        {
                            if (scale.Count == 0 || !scale.Any(x => x.Scale == w.fullWeightScale))
                            {
                                Structures.scaleReport scaleRow = new Structures.scaleReport
                                {
                                    Scale = w.fullWeightScale
                                };
                                scale.Add(scaleRow);
                            }
                            scale.Find(x => x.Scale == w.fullWeightScale).BruttoWeight += w.brutto ?? 0;
                        }

                        if (edited)
                        {
                            if (redactors.Count == 0 || !redactors.Any(x => x.redactor == w.redactor))
                            {
                                Structures.redactorReport redactorRow = new Structures.redactorReport
                                {
                                    redactor = w.redactor,
                                    number = new List<int>()
                                };
                                redactors.Add(redactorRow);
                            }
                            redactors.Find(x => x.redactor == w.redactor).number.Add(j);
                        }

                        
                        #endregion

                        #region Изменение размера шрифта
                        for (int a = 1; a < 21; a++)
                        {
                            wTable.Cell(row, a).Range.Font.Size = 8;
                        }

                        
                        #endregion
                    }


                    #endregion

                    #region Строка 'Итого' и стили для неё
                    //поле Итого
                    wTable.Rows.Add();
                    row = wTable.Rows.Count;
                    for (int i = 0; i < 21; i++)
                    {
                        wTable.Cell(row, i).Shading.BackgroundPatternColor = WdColor.wdColorWhite;
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleNone;
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    wTable.Cell(row, 13).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    for (int i = 15; i < 21; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleNone;
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    wTable.Cell(row, 7).Range.Text = "Итого:";
                    wTable.Cell(row, 8).Range.Text = netto.ToString();
                    wTable.Cell(row, 9).Range.Text = brutto.ToString();
                    wTable.Cell(row, 10).Range.Text = tara.ToString();
                    wTable.Cell(row, 11).Range.Text = doc.ToString();
                    wTable.Cell(row, 12).Range.Text = Math.Round(adj).ToString();
                    wTable.Cell(row, 14).Range.Text = volume.ToString();
                    for (int i = 1; i < 15; i++)
                        wTable.Cell(row, i).Range.Bold = 1;
                    for (int i = 1; i < 15; i++)
                        wTable.Cell(row, i).Range.Font.Size = 9;

                    //поле Весы
                    bool first = true;
                    foreach (var item in scale)
                    {
                        wTable.Rows.Add();
                        row = wTable.Rows.Count;
                        for (int i = 0; i < 7; i++)
                        {
                            wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                        }

                        wTable.Cell(row, 7).Range.Text = item.Scale;
                        wTable.Cell(row, 8).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleNone;

                        wTable.Cell(row, 9).Range.Text = item.BruttoWeight.ToString();
                        wTable.Cell(row, 10).Range.Text = item.TaraWeight.ToString();
                        wTable.Cell(row, 11).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                        wTable.Cell(row, 13).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;

                        for (int i = 11; i < 14; i++)
                        {
                            wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleNone;  

                        }
                        if (!first)
                        {
                            for (int i = 11; i < 15; i++)
                            {
                                wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                            }                         
                        }
                        first = false;
                        wTable.Cell(row, 15).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleNone;

                        for (int i = 15; i < 21; i++)
                        {
                            Console.WriteLine(i);
                            wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                        }
                        
                    }
                    var rCount = wTable.Rows.Count;
                    for (int i = rCount - scale.Count + 1; i <= rCount; i++)
                    {
                        wTable.Cell(i, 7).Merge(wTable.Cell(i, 8));
                        wTable.Cell(i, 7).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    }

                    //Поле количество
                    wTable.Rows.Add();
                    row = wTable.Rows.Count;
                    for (int i = 1; i < 7; i++)
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 13).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    for (int i = 9; i < 20; i++)
                    {
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderRight].LineStyle = WdLineStyle.wdLineStyleNone;
                    }
                    for (int i = 10; i < 20; i++)
                        wTable.Cell(row, i).Range.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 7).Range.Text = "Кол-во:";
                    wTable.Cell(row, 7).Range.Bold = 1;
                    wTable.Cell(row, 8).Range.Text = j.ToString();
                    wTable.Cell(row, 8).Range.Bold = 1;
                    #endregion

                    foreach(var redactor in redactors)
                    {
                        oWord.ActiveDocument.Paragraphs.Add();
                        oWord.ActiveDocument.Characters.Last.Select();  // Line 1
                        oWord.Selection.Collapse();                     // Line 2
                        oWord.Selection.TypeText(
                            "Пользователь \"" + redactor.redactor + "\" отредактировал записи под номерами: " +
                            string.Join(", ", redactor.number)
                            );
                    }
                    oWord.ActiveDocument.Characters.Last.Select(); 
                    oWord.Selection.Collapse();                    
                    oWord.Selection.TypeText("");

                    #region Сохранение документа и вывод на печать
                    try
                    {
                        oDoc.SaveAs("tmp.docx");
                        oDoc.Close();
                        oDoc = oWord.Documents.Open("tmp.docx");

                        oDoc.Activate();
                        oWord.Visible = true;

                        oWord.ShowStartupDialog = false;

                        var dialogResult = oWord.Dialogs[WdWordDialog.wdDialogFilePrint].Show();

                        if (dialogResult == 1)
                            oDoc.PrintOut();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Завершите открытый отчет", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    #endregion
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
                Console.WriteLine(ex.Message);
            }


            return true;
        }

        public static bool detalReportXls(DateTime dateFrom, DateTime timeFrom, DateTime dateBefore, DateTime timeBefore,
                                        string material, string role, string client, string carNum,
                                        string carMark, string driver, string point1, string point2,
                                        string consignee, string consignor, string type, string stock,
                                        string carrier, string orderNum, string orderPosNum, bool notFinish,
                                        string adjCheck, string[] scales, bool edited)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    #region Открытие файла
                    Excel.Application oExcel = new Excel.Application();
                    Excel.Workbook ExcelWorkBook = oExcel.Workbooks.Add(Environment.CurrentDirectory + "\\templates\\detalReport.xlsx");
                    Excel.Sheets ExcelSheets = ExcelWorkBook.Worksheets;

                    Excel.Worksheet MySheet = (Excel.Worksheet)ExcelSheets.get_Item(1);
                    #endregion

                    #region Заполнение параметров 
                    MySheet.Range["DateFrom"].Value = dateFrom.Date.ToString().Split()[0] + " " + timeFrom.ToString().Split()[1];
                    MySheet.Range["DateBefore"].Value = dateBefore.Date.ToString().Split()[0] + " " + timeBefore.ToString().Split()[1];
                    MySheet.Range["Good"].Value = material;
                    var roleId = role != "" ? Int32.Parse(role) : 0;
                    MySheet.Range["Operator"].Value = (from i in context.Users where i.id == roleId select i.FIO).FirstOrDefault();
                    MySheet.Range["Consignee"].Value = client;
                    MySheet.Range["CarNumber"].Value = carNum;
                    MySheet.Range["CarMark"].Value = carMark;
                    MySheet.Range["Driver"].Value = driver;
                    MySheet.Range["Point"].Value = point2;
                    MySheet.Range["Type"].Value = type;
                    var stockId = stock.Equals("") ? 0 : Int32.Parse(stock);
                    MySheet.Range["Stock"].Value = (from i in context.Storages where i.id == stockId select i.name).FirstOrDefault();
                    MySheet.Range["OrderPosition"].Value = orderPosNum;
                    #endregion

                    #region Получение данных с таблицы
                    var temp = (from i in context.Weighings
                                where i.carNumber != "" &&
                                (i.brutto != null && i.carWeight != null)
                                select i).AsEnumerable();

                    if (adjCheck != "Все")
                    {
                        if (adjCheck == "С корректировкой")
                        {
                            temp = from i in temp
                                   where i.adjustment != 0 && i.adjustment != null
                                   select i;
                        }
                        else if (adjCheck == "Без корректировки")
                        {
                            temp = from i in temp
                                   where i.adjustment == 0 || i.adjustment == null
                                   select i;
                        }
                    }

                    DateTime dateTimeFrom = dateFrom.Date + timeFrom.TimeOfDay;
                    DateTime dateTimeBefore = dateBefore.Date + timeBefore.TimeOfDay;
                    var table = from i in temp
                                where (i.dateWeight >= dateTimeFrom &&
                                        i.dateWeight <= dateTimeBefore)
                                select i;
                    if (!notFinish)
                    {
                        table = from i in table
                                where (i.brutto != 0 && i.brutto != null) &&
                                      (i.carWeight != 0 && i.carWeight != null)
                                select i;
                    }
                    if (edited)
                    {
                        table = from i in table
                                where i.edited != null
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(material))
                    {
                        table = from i in table
                                where i.material == material
                                select i;

                    }
                    if (!string.IsNullOrWhiteSpace(role))
                    {
                        table = from i in table
                                where i.userId == Int32.Parse(role)
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(client))
                    {
                        table = from i in table
                                join c in context.Clients on i.clientId equals c.id
                                where c.name == client
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(carNum))
                    {
                        table = from i in table
                                where i.carNumber == carNum
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(carMark))
                    {
                        table = from i in table
                                where i.carMark == carMark
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(driver))
                    {
                        table = from i in table
                                where i.driver == driver
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(point1))
                    {
                        table = from i in table
                                where i.point1 == point1
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(point2))
                    {
                        table = from i in table
                                where i.point2 == point2
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(consignee))
                    {
                        table = from i in table
                                where i.consignee == consignee
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(consignor))
                    {
                        table = from i in table
                                where i.consignor == consignor
                                select i;
                    }
                    if (type != "Любая")
                    {
                        table = from i in table
                                where i.operationType == type
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(stock))
                    {
                        table = from i in table
                                where i.storageId == Int32.Parse(stock)
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(carrier))
                    {
                        table = from i in table
                                where i.carrierid == Int32.Parse(carrier)
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(orderNum))
                    {
                        table = from i in table
                                where i.orderNumber == orderNum
                                select i;
                    }
                    if (!string.IsNullOrWhiteSpace(orderPosNum))
                    {
                        table = from i in table
                                where i.orderPositionNumber == orderPosNum
                                select i;
                    }
                    #endregion

                    int StartCell = 16;
                    int LengthCell = 16;
                    int count = 1;
                    double overweight = 0;
                    List<Structures.scaleReport> scale = new List<Structures.scaleReport>();
                    List<Structures.redactorReport> redactors = new List<Structures.redactorReport>();

                    #region Заполнение строк
                    foreach (var w in table)
                    {
                        MySheet.Cells[LengthCell, "A"].Value = (count).ToString();
                        MySheet.Cells[LengthCell, "B"].Value = w.dateWeight + "\n" + w.dateOut;
                        MySheet.Cells[LengthCell, "C"].Value = (from i in context.Clients where i.id == w.clientId select i.name).FirstOrDefault();
                        MySheet.Cells[LengthCell, "D"].Value = w.carNumber;
                        MySheet.Cells[LengthCell, "E"].Value = w.carMark;
                        MySheet.Cells[LengthCell, "F"].Value = w.material;
                        MySheet.Cells[LengthCell, "G"].Value = w.driver;
                        MySheet.Cells[LengthCell, "H"].Value = w.factorWeight.ToString();
                        MySheet.Cells[LengthCell, "I"].Value = w.brutto.ToString();
                        MySheet.Cells[LengthCell, "J"].Value = w.carWeight.ToString();
                        MySheet.Cells[LengthCell, "K"].Value = w.docWeight.ToString();
                        if (w.factorWeight != 0 && w.factorWeight != null && w.adjustment != 0 && w.adjustment != null)
                        {
                            double adjstmt = (w.factorWeight ?? 0) * (w.adjustment ?? 0) / 100;
                            MySheet.Cells[LengthCell, "L"].Value = Math.Round((w.factorWeight ?? 0) + adjstmt).ToString();
                        }
                        else
                            MySheet.Cells[LengthCell, "L"].Value = w.factorWeight.ToString();
                        MySheet.Cells[LengthCell, "M"].Value = w.adjustment != 0 ? w.adjustment.ToString() : "";
                        MySheet.Cells[LengthCell, "N"].Value = w.volume != 0 && w.volume != null ? w.volume.ToString() : "";
                        MySheet.Cells[LengthCell, "O"].Value = w.volume != 0 && w.volume != null ? Math.Round((w.factorWeight ?? 0) / (w.volume ?? 0), 3).ToString() : "";
                        overweight = w.docWeight != 0 && w.docWeight != null ? (w.factorWeight ?? 0 - w.docWeight ?? 0) : 0;
                        MySheet.Cells[LengthCell, "P"].Value = overweight != 0 ? overweight.ToString() : "";
                        MySheet.Cells[LengthCell, "Q"].Value = (from i in context.Users where i.id == w.userId select i.FIO).FirstOrDefault();
                        MySheet.Cells[LengthCell, "R"].Value = w.point2;
                        MySheet.Cells[LengthCell, "S"].Value = w.operationType;
                        MySheet.Cells[LengthCell, "T"].Value = (from i in context.Storages where i.id == w.storageId select i.name).FirstOrDefault();

                        #region Подсчет данных весов
                        if (w.taraScale != "" && w.taraScale != null)
                        {
                            if (scale.Count == 0 || !scale.Any(x => x.Scale == w.taraScale))
                            {
                                Structures.scaleReport scaleRow = new Structures.scaleReport
                                {
                                    Scale = w.taraScale
                                };
                                scale.Add(scaleRow);
                            }
                            scale.Find(x => x.Scale == w.taraScale).TaraWeight += w.carWeight ?? 0;
                        }

                        if (w.fullWeightScale != "" && w.fullWeightScale != null)
                        {
                            if (scale.Count == 0 || !scale.Any(x => x.Scale == w.fullWeightScale))
                            {
                                Structures.scaleReport scaleRow = new Structures.scaleReport
                                {
                                    Scale = w.fullWeightScale
                                };
                                scale.Add(scaleRow);
                            }
                            scale.Find(x => x.Scale == w.fullWeightScale).BruttoWeight += w.brutto ?? 0;
                        }

                        if (edited)
                        {
                            if (redactors.Count == 0 || !redactors.Any(x => x.redactor == w.redactor))
                            {
                                Structures.redactorReport redactorRow = new Structures.redactorReport
                                {
                                    redactor = w.redactor,
                                    number = new List<int>()
                                };
                                redactors.Add(redactorRow);
                            }
                            redactors.Find(x => x.redactor == w.redactor).number.Add(count);
                        }
                        count++;
                        #endregion

                        #region Раскрасска ячеек
                        int color;
                        if (scales.Length > 0)
                        {
                            color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                            for (int i = 0; i < scales.Length; i++)
                            {
                                if (w.taraScale == scales[i] || w.fullWeightScale == scales[i])
                                {
                                    color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                                    break;
                                } 
                            }
                            var chartRange = MySheet.get_Range("A" + StartCell, "T" + StartCell);
                            chartRange.Interior.Color = color;
                        }
                        #endregion

                        //var range = MySheet.Range[$"A{LengthCell}:T{LengthCell}"];
                        MySheet.Range[$"A{LengthCell}:T{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        MySheet.Range[$"A{LengthCell}:T{LengthCell}"].Style.WrapText = true;
                        MySheet.Range[$"A{LengthCell}:T{LengthCell}"].Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        LengthCell++;
                    }
                    #endregion

                    #region Данные Итого
                    MySheet.Cells[LengthCell, "G"].Value = "Итого";

                    MySheet.Cells[LengthCell, "H"].Formula = $"=SUM(H{StartCell}: H{LengthCell - 1})";

                    MySheet.Cells[LengthCell, "I"].Formula = $"=SUM(I{StartCell}: I{LengthCell - 1})";

                    MySheet.Cells[LengthCell, "J"].Formula = $"=SUM(G{StartCell}: G{LengthCell - 1})";

                    MySheet.Cells[LengthCell, "K"].Formula = $"=SUM(K{StartCell}: K{LengthCell - 1})";

                    MySheet.Cells[LengthCell, "L"].Formula = $"=SUM(L{StartCell}: L{LengthCell - 1})";

                    MySheet.Cells[LengthCell, "N"].Formula = $"=SUM(M{StartCell}: M{LengthCell - 1})";

                    MySheet.Range[$"G{LengthCell}:L{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    MySheet.Range[$"N{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;

                    LengthCell++;
                    #endregion

                    #region Данные весов
                    foreach (var item in scale)
                    {
                        MySheet.Cells[LengthCell, "H"].Value = item.Scale;
                        MySheet.Cells[LengthCell, "I"].Value = item.BruttoWeight;
                        MySheet.Cells[LengthCell, "J"].Value = item.TaraWeight;

                        MySheet.Range[$"G{LengthCell}:H{LengthCell}"].Merge();

                        MySheet.Range[$"G{LengthCell}:J{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                        LengthCell++;
                    }
                    #endregion

                    #region Данные Количество
                    MySheet.Cells[LengthCell, "H"].Value = "Кол-во";
                    MySheet.Cells[LengthCell, "I"].Value = count;
                    MySheet.Range[$"G{LengthCell}:H{LengthCell}"].Merge();
                    MySheet.Range[$"G{LengthCell}:I{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    LengthCell++;
                    #endregion

                    #region Данные редактирования
                    foreach (var redactor in redactors)
                    {
                        MySheet.Cells[LengthCell, "A"].Value = "Пользователь \"" + redactor.redactor + "\" отредактировал записи под номерами: " + string.Join(", ", redactor.number);
                        LengthCell++;
                    }
                    #endregion

                    #region Запись файла
                    try
                    {
                        oExcel.Application.DisplayAlerts = false;
                        MySheet.PageSetup.Zoom = false;
                        MySheet.PageSetup.FitToPagesWide = 1;
                        MySheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                        string basePath = Environment.CurrentDirectory + "\\Reports";
                        System.IO.Directory.CreateDirectory(basePath);
                        string reportFile = basePath + $"\\Детальный отчет {DateTime.Now.ToString().Replace(':', '-')}.xlsx";
                        ExcelWorkBook.SaveAs(reportFile, Type.Missing, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange, 
                            Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing
                        ExcelWorkBook.Close();
                        ExcelWorkBook = oExcel.Workbooks.Open(reportFile);
                        ExcelWorkBook.Activate();
                        oExcel.Visible = true;
                        oExcel.ShowStartupDialog = false;
                        var dialogResult = oExcel.Dialogs[Excel.XlBuiltInDialog.xlDialogPrint].Show();

                        if (dialogResult == true)
                            ExcelWorkBook.PrintPreview();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    #endregion
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }
            return true;
        }


        public static bool sumReport(DateTime dateFrom, DateTime timeFrom, DateTime dateBefore, DateTime timeBefore, bool notFinish)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    Word._Application oWord = new Word.Application();
                    _Document oDoc = oWord.Documents.Add(Environment.CurrentDirectory + "\\templates\\sumReport.docx");
                    Word.Table wTable = oDoc.Tables[1];

                    var goods = from i in context.Goods
                                select i;

                    oDoc.Bookmarks["startDate"].Range.Text = dateFrom.Date.ToString().Split()[0] + " " + timeFrom.ToString().Split()[1];
                    oDoc.Bookmarks["endDate"].Range.Text = dateBefore.Date.ToString().Split()[0] + " " + timeBefore.ToString().Split()[1];
                    oDoc.Bookmarks["date"].Range.Text = DateTime.Now.ToString();

                    #region Переменные для строки 'Итого'
                    int row = wTable.Rows.Count;
                    int j = 0;
                    double nettoF = 0;
                    double bruttoF = 0;
                    double taraF = 0;
                    double docF = 0;
                    double adjF = 0;
                    double plotF = 0;
                    double overloadF = 0;
                    double countF = 0;
                    double netto = 0;
                    double brutto = 0;
                    double tara = 0;
                    double doc = 0;
                    double adj = 0;
                    double plot = 0;
                    double overload = 0;
                    #endregion

                    foreach (Goods g in goods)
                    {
                        #region Получение данных с таблицы
                        var temp = (from w in context.Weighings
                                    where w.material == g.name
                                    select w).AsEnumerable();
                        
                        DateTime dateTimeFrom = dateFrom.Date + timeFrom.TimeOfDay;
                        DateTime dateTimeBefore = dateBefore.Date + timeBefore.TimeOfDay;
                        var table = from w in temp
                                    where w.dateWeight >= dateTimeFrom &&
                                    w.dateWeight <= dateTimeBefore
                                    select w;
                        if (!notFinish)
                        {
                            table = from i in table
                                    where (i.brutto != 0 && i.brutto != null) &&
                                          (i.carWeight != 0 && i.carWeight != null)
                                    select i;
                        }
                        var tt = table.ToArray();
                        #endregion

                        #region Заполнение таблицы
                        if (table.Any())
                        {
                            #region Обнуление переменных для строки 'Итого'
                            netto = 0;
                            brutto = 0;
                            tara = 0;
                            doc = 0;
                            adj = 0;
                            plot = 0;
                            overload = 0;
                            #endregion

                            #region Подсчет данных о грузе
                            foreach (Weighings w in table)
                            {
                                netto += w.factorWeight ?? 0;
                                nettoF += w.factorWeight ?? 0;
                                brutto += w.brutto ?? 0;
                                bruttoF += w.brutto ?? 0;
                                tara += w.carWeight ?? 0;
                                taraF += w.carWeight ?? 0;
                                doc += w.docWeight ?? 0;
                                docF += w.docWeight ?? 0;
                                var adjustment = w.adjustment ?? 0;
                                var factorWeight = w.factorWeight ?? 0;
                                adj += adjustment != 0 ? Math.Round(factorWeight + (factorWeight * adjustment / 100)) : factorWeight;
                                adjF += adjustment != 0 ? Math.Round(factorWeight + (factorWeight * adjustment / 100)) : factorWeight;
                                plot += w.volume != null ? netto / (w.volume ?? 0) : 0;
                                plotF += w.volume != null ? netto / (w.volume ?? 0) : 0;
                                if ((w.docWeight ?? 0) != 0)
                                {
                                    overload += (w.docWeight ?? 0) - (w.factorWeight ?? 0);
                                    overloadF += (w.docWeight ?? 0) - (w.factorWeight ?? 0);
                                }
                            }
                            #endregion

                            if (netto != 0 || brutto != 0 || tara != 0 || doc != 0)
                            {
                                wTable.Rows.Add();
                                row = wTable.Rows.Count;
                                j++;
                                wTable.Cell(row, 1).Range.Text = j.ToString();
                                wTable.Cell(row, 2).Range.Text = g.name;


                                wTable.Cell(row, 3).Range.Text = table.Count().ToString();
                                countF += table.Count();
                                wTable.Cell(row, 4).Range.Text = netto.ToString();
                                wTable.Cell(row, 5).Range.Text = brutto.ToString();
                                wTable.Cell(row, 6).Range.Text = tara.ToString();
                                wTable.Cell(row, 7).Range.Text = doc.ToString();
                                wTable.Cell(row, 8).Range.Text = Math.Round(adj).ToString();
                                wTable.Cell(row, 9).Range.Text = Math.Round(plot, 3).ToString();
                                wTable.Cell(row, 10).Range.Text = overload.ToString();
                                for (int k = 1; k < 11; k++)
                                    wTable.Cell(row, k).Range.Font.Size = 11;
                            }
                        }
                        #endregion
                    }

                    #region Строка 'Итого' и стили для неё
                    wTable.Rows.Add();
                    row = wTable.Rows.Count;
                    wTable.Cell(row, 1).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 1).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 2).Range.Borders[WdBorderType.wdBorderLeft].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 2).Range.Borders[WdBorderType.wdBorderBottom].LineStyle = WdLineStyle.wdLineStyleNone;
                    wTable.Cell(row, 2).Range.Text = "Итого:";
                    wTable.Cell(row, 3).Range.Text = countF.ToString();
                    wTable.Cell(row, 4).Range.Text = nettoF.ToString();
                    wTable.Cell(row, 5).Range.Text = bruttoF.ToString();
                    wTable.Cell(row, 6).Range.Text = taraF.ToString();
                    wTable.Cell(row, 7).Range.Text = docF.ToString();
                    wTable.Cell(row, 8).Range.Text = Math.Round(adjF).ToString();
                    wTable.Cell(row, 9).Range.Text = Math.Round(plotF, 3).ToString();
                    wTable.Cell(row, 10).Range.Text = overloadF.ToString();
                    for (int k = 2; k < 11; k++)
                        wTable.Cell(row, 10).Range.Bold = 1;
                    for (int k = 1; k < 11; k++)
                        wTable.Cell(row, k).Range.Font.Size = 11;
                    #endregion

                    #region Сохранение документа и вывод на печать
                    try
                    {
                        oDoc.SaveAs("tmp.docx");
                        oDoc.Close();
                        oDoc = oWord.Documents.Open("tmp.docx");

                        oDoc.Activate();
                        oWord.Visible = true;

                        oWord.ShowStartupDialog = false;

                        var dialogResult = oWord.Dialogs[WdWordDialog.wdDialogFilePrint].Show();

                        if (dialogResult == 1)
                            oDoc.PrintOut();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Завершите открытый отчет", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    #endregion
                }

            }
            catch
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }
            return true;
        }

        public static bool sumReportXls(DateTime dateFrom, DateTime timeFrom, DateTime dateBefore, DateTime timeBefore, bool notFinish)
        {
            try
            {
                #region Открытие файла
                Excel.Application oExcel = new Excel.Application();
                Excel.Workbook ExcelWorkBook = oExcel.Workbooks.Add(Environment.CurrentDirectory + "\\templates\\sumReport.xlsx");
                Excel.Sheets ExcelSheets = ExcelWorkBook.Worksheets;

                Excel.Worksheet MySheet = (Excel.Worksheet)ExcelSheets.get_Item(1);
                #endregion

                #region Заполнение параметров
                MySheet.Range["DateFrom"].Value = dateFrom.Date.ToString().Split()[0] + " " + timeFrom.ToString().Split()[1];
                MySheet.Range["DateBefore"].Value = dateBefore.Date.ToString().Split()[0] + " " + timeBefore.ToString().Split()[1];
                MySheet.Range["CreateDate"].Value = DateTime.Now.ToString();
                #endregion

                double count = 1;
                double netto = 0;
                double brutto = 0;
                double tara = 0;
                double doc = 0;
                double adj = 0;
                double plot = 0;
                double overload = 0;
                using (DataBaseContext context = new DataBaseContext())
                {
                    var goods = from i in context.Goods
                                select i;

                    int StartCell = 5;
                    int LengthCell = 5;
                    foreach (Goods good in goods)
                    {
                        #region Получение данных с таблицы
                        DateTime dateTimeFrom = dateFrom.Date + timeFrom.TimeOfDay;
                        DateTime dateTimeBefore = dateBefore.Date + timeBefore.TimeOfDay;
                        var table = (from w in context.Weighings
                                    where w.material == good.name &&
                                    w.dateWeight >= dateTimeFrom &&
                                    w.dateWeight <= dateTimeBefore
                                    select w).AsEnumerable();

                        if (!notFinish)
                        {
                            table = from i in table
                                    where (i.brutto != 0 && i.brutto != null) &&
                                          (i.carWeight != 0 && i.carWeight != null)
                                    select i;
                        }
                        #endregion

                        #region Заполнение таблицы

                        netto = 0;
                        brutto = 0;
                        tara = 0;
                        doc = 0;
                        adj = 0;
                        plot = 0;
                        overload = 0;

                        foreach (Weighings w in table)
                        {
                            
                            netto += w.factorWeight ?? 0;
                            brutto += w.brutto ?? 0;
                            tara += w.carWeight ?? 0;
                            var docWeigth = w.docWeight ?? 0;
                            doc += docWeigth;
                            var adjustment = w.adjustment ?? 0;
                            var factorWeight = w.factorWeight ?? 0;
                            adj += adjustment != 0 ? Math.Round(factorWeight + (factorWeight * adjustment / 100)) : factorWeight;
                            plot += w.volume != null ? netto / (w.volume ?? 0) : 0;
                            if (docWeigth != 0)
                            {
                                overload += docWeigth - factorWeight;
                            }
                        }

                        if (netto != 0 || brutto != 0 || tara != 0 || doc != 0)
                        {
                            MySheet.Cells[LengthCell, "A"].Value = count;
                            MySheet.Cells[LengthCell, "B"].Value = good.name;
                            MySheet.Cells[LengthCell, "C"].Value = table.Count();
                            MySheet.Cells[LengthCell, "D"].Value = netto.ToString();
                            MySheet.Cells[LengthCell, "E"].Value = brutto.ToString();
                            MySheet.Cells[LengthCell, "F"].Value = tara.ToString();
                            MySheet.Cells[LengthCell, "G"].Value = doc.ToString();
                            MySheet.Cells[LengthCell, "H"].Value = Math.Round(adj).ToString();
                            MySheet.Cells[LengthCell, "I"].Value = Math.Round(plot, 3).ToString();
                            MySheet.Cells[LengthCell, "J"].Value = overload;

                            MySheet.Range[$"A{LengthCell}:J{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                            MySheet.Range[$"A{LengthCell}:J{LengthCell}"].Style.WrapText = true;
                            MySheet.Range[$"A{LengthCell}:J{LengthCell}"].Style.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            count++;
                            LengthCell++;
                        }
                        #endregion
                    }

                    #region Заполнение Итого
                    MySheet.Cells[LengthCell, "B"].Value = "Итого:";
                    MySheet.Cells[LengthCell, "B"].Style.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    MySheet.Cells[LengthCell, "C"].Formula = $"=SUM(C{StartCell}:C{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "D"].Formula = $"=SUM(D{StartCell}:D{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "E"].Formula = $"=SUM(E{StartCell}:E{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "F"].Formula = $"=SUM(F{StartCell}:F{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "G"].Formula = $"=SUM(G{StartCell}:G{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "H"].Formula = $"=SUM(H{StartCell}:H{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "I"].Formula = $"=SUM(I{StartCell}:I{LengthCell - 1})";
                    MySheet.Cells[LengthCell, "J"].Formula = $"=SUM(J{StartCell}:J{LengthCell - 1})";

                    MySheet.Range[$"C{LengthCell}:J{LengthCell}"].Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
                    


                    #endregion
                }

                #region Запись файла
                try
                {
                    oExcel.Application.DisplayAlerts = false;
                    MySheet.PageSetup.Zoom = false;
                    MySheet.PageSetup.FitToPagesWide = 1;
                    MySheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                    string basePath = Environment.CurrentDirectory + "\\Reports";
                    System.IO.Directory.CreateDirectory(basePath);
                    string reportFile = basePath + $"\\Суммарный отчет {DateTime.Now.ToString().Replace(':', '-')}.xlsx";
                    ExcelWorkBook.SaveAs(reportFile, Type.Missing, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                        Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    ExcelWorkBook.Close();
                    ExcelWorkBook = oExcel.Workbooks.Open(reportFile);
                    ExcelWorkBook.Activate();
                    oExcel.Visible = true;
                    oExcel.ShowStartupDialog = false;
                    var dialogResult = oExcel.Dialogs[Excel.XlBuiltInDialog.xlDialogPrint].Show();

                    if (dialogResult == true)
                        ExcelWorkBook.PrintPreview();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
                #endregion
            }
            catch (Exception)
            {
                MessageBox.Show("Завершите работу с предыдущим документом");
            }

            return true;
        }

    }
}
