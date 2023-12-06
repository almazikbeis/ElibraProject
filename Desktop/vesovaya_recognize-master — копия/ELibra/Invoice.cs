using ELibra.Classes;
using System;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;

namespace ELibra
{
    public partial class Invoice : Form
    {

        private DateTime date;

        public Invoice(DateTime date)
        {
            InitializeComponent();
            this.date = date;
            using (DataBaseContext context = new DataBaseContext())
            {
                var data = (from i in context.Weighings
                           where i.dateWeight == date
                           select new
                           {
                               i.seal,
                               i.volume
                           }).FirstOrDefault();

                tVolume.Text = data.volume.ToString();
                tSeal.Text = data.seal;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    var weighingTable = (from i in context.Weighings
                                         where i.dateWeight == date
                                         select i).FirstOrDefault();

                    #region Отправление данных на формирование накладной
                    weighingTable.volume = !string.IsNullOrEmpty(tVolume.Text.Replace(".",",")) ? double.Parse(tVolume.Text) : 0;
                    weighingTable.seal = tSeal.Text;
                    context.SaveChanges();
                    DocHelper.nakl1(date);
                    #endregion

                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Данные введены в неправильном формате", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            

        }

        private void TVolume_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D0 || e.KeyCode <= Keys.D9 
                || e.KeyCode == Keys.Decimal || e.KeyCode >= Keys.NumPad1 
                || e.KeyCode <= Keys.NumPad9 || e.KeyCode == Keys.Delete 
                || e.KeyCode == Keys.Back)
            {
                e.Handled = true;
                if ((sender as TextBox).Text.Where(x => x == '.').Count() < 1)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
