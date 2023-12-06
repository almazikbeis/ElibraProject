using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace License
{
    public partial class License : Form
    {
        Dictionary<string, string> deviceArr = new Dictionary<string, string>();
        List<string> deviceDir = new List<string>();
        public License()
        {
            InitializeComponent();
        }

        private void SetLicense_Load(object sender, EventArgs e)
        {
            ManagementObjectSearcher theSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
            foreach (ManagementObject currentObject in theSearcher.Get())
            {
                ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
                deviceArr.Add(currentObject["Caption"].ToString(), currentObject["SerialNumber"].ToString());
                cmbDriver.Items.Add(currentObject["Caption"].ToString());
            }
            DriveInfo[] info = DriveInfo.GetDrives();
            foreach (var item in info)
                if (item.DriveType == DriveType.Removable)
                    deviceDir.Add(item.Name);
            if (cmbDriver.Items.Count > 0)
                cmbDriver.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            string serialNumber;
            deviceArr.TryGetValue(cmbDriver.SelectedItem.ToString(), out serialNumber);
            File.WriteAllText(deviceDir[0] + "\\license", serialNumber);
            if (File.Exists("ELibra86.exe"))
                File.Copy("ELibra86.exe", deviceDir[0] + "\\ELibra86.exe");
            if (File.Exists("ELibra64.exe"))
                File.Copy("ELibra64.exe", deviceDir[0] + "\\ELibra64.exe");
            MessageBox.Show("Лицензия установлена");
        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            Setting frmSetting = new Setting();
            frmSetting.ShowDialog();
        }

        private void BtnChangeLicense_Click(object sender, EventArgs e)
        {
            ChangeLicense frmChangeLicense = new ChangeLicense();
            frmChangeLicense.Show();
        }
    }
}
