using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace License
{
    public partial class ChangeLicense : Form
    {
        private const string loginM = "manager";
        private const string passM = "4urL2Keo";
        private bool edit = false;

        public ChangeLicense()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolStripCancel_Click(object sender, EventArgs e)
        {
            tName.Clear(); tName.Enabled = false;
            tLogin.Clear(); tLogin.Enabled = false;
            tPass.Clear(); tPass.Enabled = false;
            dateTimePicker.Enabled = false;
            cbVideo.Checked = false; cbVideo.Enabled = false;
            cbEdit.Checked = false; cbEdit.Enabled = false;
            cb1C.Checked = false; cb1C.Enabled = false;
            cbNN.Checked = false; cbNN.Enabled = false;
            cbUpload.Checked = false; cbUpload.Enabled = false;
            toolStripSave.Enabled = false;
            toolStripEdit.Enabled = true;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
        }

        private void ToolStripSave_Click(object sender, EventArgs e)
        {
            string name = tName.Text;
            string login = tLogin.Text;
            string pass = tPass.Text;
            string licenseDate = dateTimePicker.Value.ToString();
            string licenseCode = "";
            if (cbVideo.Checked)
                licenseCode += "1";
            else
                licenseCode += "0";
            if (cb1C.Checked)
                licenseCode += "1";
            else
                licenseCode += "0";
            if (cbEdit.Checked)
                licenseCode += "1";
            else
                licenseCode += "0";
            if (cbNN.Checked)
                licenseCode += "1";
            else
                licenseCode += "0";
            if (cbUpload.Checked)
                licenseCode += "1";
            else
                licenseCode += "0";
            if (edit)
                serverHelp.changeLicense(name, licenseCode, licenseDate);
            else
                serverHelp.addCompanie(login, pass, name, licenseCode, licenseDate, serverHelp.getToken(loginM, passM));
            refreshDGV();
            tName.Clear(); tName.Enabled = false;
            tLogin.Clear(); tLogin.Enabled = false;
            tPass.Clear(); tPass.Enabled = false;
            cbVideo.Checked = false; cbVideo.Enabled = false;
            cbEdit.Checked = false; cbEdit.Enabled = false;
            cb1C.Checked = false; cb1C.Enabled = false;
            cbNN.Checked = false; cbNN.Enabled = false;
            cbUpload.Checked = false; cbUpload.Enabled = false;
            dateTimePicker.Enabled = false;
            toolStripSave.Enabled = false;
            toolStripEdit.Enabled = true;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;
        }

        private void ChangeLicense_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            toolStripSave.Enabled = false;
            toolStripCancel.Enabled = false;
            toolStripAdd.Enabled = true;

            cbVideo.Checked = false; cbVideo.Enabled = false;
            cbEdit.Checked = false; cbEdit.Enabled = false;
            cb1C.Checked = false; cb1C.Enabled = false;
            cbNN.Checked = false; cbNN.Enabled = false;
            cbUpload.Checked = false; cbUpload.Enabled = false;

            dataGridView1.Columns.Add("Name", "Название компании");
            dataGridView1.Columns.Add("LicenseType", "Тип лицензии");
            dataGridView1.Columns.Add("Date", "Дата");

            refreshDGV();
        }

        private void refreshDGV()
        {
            dataGridView1.Rows.Clear();
            JArray a = JArray.Parse(serverHelp.getCompanies(loginM, passM));
            List<string> arr = new List<string>();
            foreach (JObject o in a.Children<JObject>())
            {
                foreach (JProperty p in o.Properties())
                {
                    string value = (string)p.Value;
                    arr.Add(value);
                }
                int rowNumber = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowNumber].Cells[0].Value = arr[1];
                dataGridView1.Rows[rowNumber].Cells[1].Value = arr[2];
                dataGridView1.Rows[rowNumber].Cells[2].Value = arr[3];
                arr.Clear();
            }
        }

        private void ToolStripAdd_Click(object sender, EventArgs e)
        {
            tName.Enabled = true;
            tLogin.Enabled = true;
            tPass.Enabled = true;
            dateTimePicker.Enabled = true;
            cbVideo.Enabled = true;
            cbEdit.Enabled = true;
            cb1C.Enabled = true;
            cbNN.Enabled = true;
            cbUpload.Enabled = true;
            toolStripSave.Enabled = true;
            toolStripEdit.Enabled = false;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
        }

        private void ToolStripEdit_Click(object sender, EventArgs e)
        {
            edit = true;
            tName.Enabled = true; tName.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dateTimePicker.Enabled = true; dateTimePicker.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            cbVideo.Enabled = true; cbVideo.Checked = dataGridView1.CurrentRow.Cells[1].Value.ToString()[0] == '1' ? true : false;
            cbEdit.Enabled = true; cbEdit.Checked = dataGridView1.CurrentRow.Cells[1].Value.ToString()[2] == '1' ? true : false;
            cb1C.Enabled = true; cb1C.Checked = dataGridView1.CurrentRow.Cells[1].Value.ToString()[1] == '1' ? true : false;
            cbNN.Enabled = true; cbNN.Checked = dataGridView1.CurrentRow.Cells[1].Value.ToString()[3] == '1' ? true : false;
            cbUpload.Enabled = true; cbUpload.Checked = dataGridView1.CurrentRow.Cells[1].Value.ToString()[4] == '1' ? true : false;
            toolStripSave.Enabled = true;
            toolStripEdit.Enabled = false;
            toolStripCancel.Enabled = true;
            toolStripAdd.Enabled = false;
        }
    }
}
