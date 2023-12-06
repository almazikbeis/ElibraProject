using ELibra.Classes;
using ELibra.Classes.Structures;
using ELibra.DBModel;
using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ELibra
{
    public partial class Journal : Form
    {
        private string[] headersText = { "Видео", "Дата и время взвешивания", "Контрагент", "Номер авто", "Марка автомобиля", "Груз", "Вес пустого автомобиля, кг", "Вес автомобиля с грузом, кг", "Чистый вес груза, кг", "Вес по документам, кг", "Объем", "Корректировка", "Весы тара", "Весы брутто", "Изменено" };

        private bool finish = true;
        private bool notFinish = true;

        private bool video = true;
        private bool notVideo = true;

        public Journal()
        {
            InitializeComponent();
        }

        private void Journal_Load(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                this.WindowState = FormWindowState.Maximized;
                dataGridView.AutoGenerateColumns = true;
                cmbPost.Items.AddRange((from i in context.Users select i.FIO).ToArray());
                timer.Enabled = true;

                string[] scales = (from i in context.Scales select i.nameUser).ToArray();
                cmbScaleBrutto.Items.AddRange(scales);
                cmbScaleBrutto.SelectedIndex = 0;
                cmbScalseTara.Items.AddRange(scales);
                cmbScalseTara.SelectedIndex = 0;
            }
            
        }

        private void DataGridView_DoubleClick(object sender, EventArgs e)
        {
            using (DataBaseContext context =  new DataBaseContext())
            {
                try
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    DateTime dateTimeW = DateTime.Parse(dataGridView.CurrentRow.Cells["date"].Value.ToString());
                    var w = (from i in context.Weighings
                             where i.dateWeight == dateTimeW
                             select i).FirstOrDefault();
                    if (!(w.brutto == 0 && w.carWeight == 0) && (w.brutto == 0 || w.carWeight == 0) || (from i in context.Settings where i.id == 1 select i.Role).FirstOrDefault() == "3")
                    {
                        #region Запуск формы с незавершенным взвешиванием
                        //Weighing weighing = new Weighing(w);
                        //weighing.MdiParent = this.MdiParent;
                        //weighing.Show();
                        if ((MdiParent as Main).frmWeighing != null)
                        {
                            (MdiParent as Main).frmWeighing.Dispose();
                        }

                        (MdiParent as Main).frmWeighing = new Weighing(w);
                        (MdiParent as Main).frmWeighing.MdiParent = this.MdiParent;
                        (MdiParent as Main).frmWeighing.Show();

                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("Редактировать можно только незаконченные взвешивания");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Запись не выбрана");
                }
            }                    
        }

        private void notFinishPaint()
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                    
                if (dataGridView.Rows[i].Cells[6].Value == null && dataGridView.Rows[i].Cells[7].Value == null)
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                else if (dataGridView.Rows[i].Cells[6].Value.ToString() == "" || (dataGridView.Rows[i].Cells[6].Value.ToString() == "0.0" || dataGridView.Rows[i].Cells[6].Value.ToString() == "0") || 
                    (dataGridView.Rows[i].Cells[7].Value.ToString() == "" || (dataGridView.Rows[i].Cells[7].Value.ToString() == "0.0" || dataGridView.Rows[i].Cells[7].Value.ToString() == "0")))
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                else
                        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;

                if (dataGridView.Rows[i].Cells[14].Value.ToString() != "")
                {
                    dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                }
                //Console.WriteLine(dataGridView.Rows[i].Cells[1].Value);
            }
            
        }

        private void DateTime_ValueChanged(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void ToolStripAdd_Click(object sender, EventArgs e)
        {
            if ((MdiParent as Main).frmWeighing == null)
            {
                (MdiParent as Main).frmWeighing = new Weighing();
                (MdiParent as Main).frmWeighing.MdiParent = this.MdiParent;
                (MdiParent as Main).frmWeighing.Show();
            }
            else
            {
                (MdiParent as Main).frmWeighing.Focus();
            }
            //Weighing frmWeighing = new Weighing();
            //frmWeighing.MdiParent = this.MdiParent;
            //frmWeighing.Show();
        }

        private void ToolStripBtnFinish_Click(object sender, EventArgs e)
        {
            if (finish)
            {
                finish = false;
                toolStripBtnFinish.BackColor = Color.White;
            }
            else
            {
                finish = true;
                toolStripBtnFinish.BackColor = Color.Orange;
            }
            timer.Enabled = true;
        }

        private void ToolStripBtnNotFinish_Click(object sender, EventArgs e)
        {
            if (notFinish)
            {
                notFinish = false;
                toolStripBtnNotFinish.BackColor = Color.White;
            }
            else
            {
                notFinish = true;
                toolStripBtnNotFinish.BackColor = Color.Orange;
            }
            timer.Enabled = true;
        }

        private void Nakl1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invoice frmInvoice = new Invoice(DateTime.Parse(dataGridView.CurrentRow.Cells[0].Value.ToString()));
            frmInvoice.ShowDialog();
        }

        private void Nakl2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocHelper.nakl2(DateTime.Parse(dataGridView.CurrentRow.Cells[0].Value.ToString()));
        }

        private void WeighingTalonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocHelper.weighingTalon(DateTime.Parse(dataGridView.CurrentRow.Cells[0].Value.ToString()));
        }

        private void TTNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocHelper.ttn(DateTime.Parse(dataGridView.CurrentRow.Cells[0].Value.ToString()));
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((sender as ComboBox).SelectedItem != null)
                {
                    if ((sender as ComboBox).SelectedItem.ToString() != "" && (sender as ComboBox).SelectedItem != null)
                    {
                        try
                        {
                            Process p = new Process();
                            p.StartInfo.FileName = (sender as ComboBox).SelectedItem.ToString();
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 14)
                {
                    var datagridview = sender as DataGridView;
                    datagridview.BeginEdit(true);
                    ((ComboBox)datagridview.EditingControl).SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                }
                //bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1);
                
                //Type type = datagridview.Columns[e.ColumnIndex].GetType();
                //if (type == typeof(DataGridViewComboBoxColumn) && validClick)
                //{
                //    datagridview.BeginEdit(true);
                //    ((ComboBox)datagridview.EditingControl).SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        private void ToolStripVideo_Click(object sender, EventArgs e)
        {
            if (video)
            {
                video = false;
                toolStripVideo.BackColor = Color.White;
            }
            else
            {
                video = true;
                toolStripVideo.BackColor = Color.Orange;
            }
            timer.Enabled = true;
        }

        private void ToolStripNotVideo_Click(object sender, EventArgs e)
        {
            if (notVideo)
            {
                notVideo = false;
                toolStripNotVideo.BackColor = Color.White;
            }
            else
            {
                notVideo = true;
                toolStripNotVideo.BackColor = Color.Orange;
            }
            timer.Enabled = true;
        }

        private void Journal_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as Main).frmJournal = null;
        }


        private void DataGridView_Sorted(object sender, EventArgs e)
        {
            notFinishPaint();
        }

        private void ToolStripUpdate_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        public void RefreshTable()
        {
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Thread primarythrd = new Thread(primaryload);
            primarythrd.IsBackground = true;
            primarythrd.SetApartmentState(System.Threading.ApartmentState.STA);
            primarythrd.Start();
            timer.Enabled = false;
        }

        delegate void AddRows(DataGridView view);
        public void primaryload()
        {
            AddRows addRows = new AddRows(DoRowAdd);
            this.Invoke(addRows, dataGridView);
        }

        private void DoRowAdd(DataGridView view)
        {
            if (view.InvokeRequired)
            {
                AddRows rows = new AddRows(DoRowAdd);
                this.Invoke(rows, new object[] { view });
            }
            else
            {
                dataGridView.Rows.Clear();
                dataGridView.Refresh();
                using (DataBaseContext db = new DataBaseContext())
                {
                    string fio = (cmbPost.SelectedItem != null ? cmbPost.SelectedItem.ToString() : "");
                    DateTime dateTimeB = dateTimeBefore.Value.Date.AddHours(23).AddMinutes(59);
                    DateTime dateTimeF = dateTimeFrom.Value.Date;

                    var Weighings = (from w in db.Weighings
                                     where w.dateWeight >= dateTimeF &&
                                            w.dateWeight <= dateTimeB
                                     orderby w.dateWeight descending
                                     select w).AsEnumerable();

                    var weighings = (from w in Weighings
                                     join uU in db.Users on w.userId equals uU.id into uj
                                     from u in uj.DefaultIfEmpty()
                                     join cU in db.Clients on w.clientId equals cU.id into cj
                                     from c in cj.DefaultIfEmpty()
                                     where (u.FIO.Contains(fio) || (w.carWeight == 0 && w.brutto == 0))
                                     where w.dateWeight >= dateTimeF &&
                                                 w.dateWeight <= dateTimeB
                                     orderby w.dateWeight descending
                                     select new JournalItem
                                     {
                                         dateWeight = w.dateWeight,
                                         name = c == null ? "" : c.name,
                                         carNumber = w.carNumber,
                                         carMark = w.carMark,
                                         material = w.material,
                                         carWeight = w.carWeight.ToString(),
                                         brutto = w.brutto.ToString(),
                                         factorWeight = w.factorWeight.ToString(),
                                         docWeight = w.docWeight.ToString(),
                                         volume = w.volume,
                                         adjustment = w.adjustment ?? 0.0,
                                         scaleTara = w.taraScale,
                                         scaleBrutto = w.fullWeightScale,
                                         edited = w.edited.ToString() ?? ""
                                     }).ToList();
                    Console.WriteLine(weighings.Count());
                    DataGridViewCell cellTextBox = new DataGridViewTextBoxCell();
                    DataGridViewCell cellComboBox = new DataGridViewComboBoxCell();


                    foreach (var weighing in weighings)
                    {
                        int rowIndex = this.dataGridView.Rows.Add();

                        var row = this.dataGridView.Rows[rowIndex];

                        row.Cells[dataGridView.Columns["date"].Index].Value = weighing.dateWeight;
                        row.Cells[dataGridView.Columns["contr"].Index].Value = weighing.name;
                        row.Cells[dataGridView.Columns["number"].Index].Value = weighing.carNumber;
                        row.Cells[dataGridView.Columns["model"].Index].Value = weighing.carMark;
                        row.Cells[dataGridView.Columns["material"].Index].Value = weighing.material;
                        row.Cells[dataGridView.Columns["weightEmpty"].Index].Value = weighing.carWeight;
                        row.Cells[dataGridView.Columns["weightFull"].Index].Value = weighing.brutto;
                        row.Cells[dataGridView.Columns["weightClear"].Index].Value = weighing.factorWeight;
                        row.Cells[dataGridView.Columns["weightDoc"].Index].Value = weighing.docWeight;
                        row.Cells[dataGridView.Columns["volume"].Index].Value = weighing.volume;
                        row.Cells[dataGridView.Columns["adj"].Index].Value = weighing.adjustment;
                        row.Cells[dataGridView.Columns["scalesTara"].Index].Value = weighing.scaleTara;
                        row.Cells[dataGridView.Columns["scalesBrutto"].Index].Value = weighing.scaleBrutto;
                        row.Cells[dataGridView.Columns["changed"].Index].Value = weighing.edited;

                        var videoFiles = (from m in db.AutoWeighingMovies
                                          join f in db.WeighingFactLinks.DefaultIfEmpty() on m.factId equals f.factId
                                          join w in db.Weighings.DefaultIfEmpty() on f.weighingId equals w.id
                                          where w.dateWeight == weighing.dateWeight
                                          select m.movieName).ToArray();

                        DataGridViewComboBoxCell cmbCell = new DataGridViewComboBoxCell();

                        cmbCell.Items.AddRange(videoFiles);

                        row.Cells[dataGridView.Columns["videoFile"].Index] = cmbCell;


                        if (weighing.carWeight == "" && weighing.brutto == "")
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (weighing.carWeight == "0" || weighing.brutto == "0")
                        {
                            row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
                        }
                        if (weighing.edited != null && weighing.edited != "")
                        {
                            row.DefaultCellStyle.BackColor = Color.Aqua;
                        }

                        bool removed = false;
                        #region Фильтрация взвешиваний по завершению
                        if (!finish)
                        {
                            if (weighing.brutto != "0" && weighing.carWeight != "0")
                            {
                                removed = true;
                            }
                        }
                        if (!notFinish)
                        {
                            if (weighing.brutto == "0" || weighing.carWeight == "0")
                            {
                                removed = true;
                            }
                        }
                        #endregion

                        #region Фильтрация взвешиваний по наличию видео
                        if (!video)
                        {
                            if (videoFiles.Length > 0)
                            {
                                removed = true;
                            }
                        }
                        if (!notVideo)
                        {
                            if (videoFiles.Length == 0)
                            {
                                removed = true;
                            }
                        }

                        if (removed)
                        {
                            dataGridView.Rows.Remove(row);
                        }
                        #endregion
                    }
                }
            }
        }

        private void cScaleTara_CheckedChanged(object sender, EventArgs e)
        {
            cmbScalseTara.Enabled = !cmbScalseTara.Enabled;
            timer.Enabled = true;
        }

        private void cScaleBrutto_CheckedChanged(object sender, EventArgs e)
        {
            cmbScaleBrutto.Enabled = !cmbScaleBrutto.Enabled;
            timer.Enabled = true;
        }

        private void cmbScalseTara_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cScaleTara.Checked)
            {
                timer.Enabled = true;
            }
        }

        private void cmbScaleBrutto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cScaleBrutto.Checked)
            {
                timer.Enabled = true;
            }
        }
    }
}
