using ELibra.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using ELibra.DBModel;

namespace ELibra
{
    public partial class AboutLicense : Form
    {
        //DataBaseContext context = new DataBaseContext();
        public AboutLicense()
        {
            InitializeComponent();
        }

        private void AboutLicense_Load(object sender, EventArgs e)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var SettinsTeble = (from i in context.Settings
                                    where i.id == 1
                                    select new
                                    {
                                        i.licenseDate,
                                        i.licenseNumber
                                    }).FirstOrDefault();

                lblLicDate.Text = SettinsTeble.licenseDate.ToString(); 
                lblVideo.BackColor = SettinsTeble.licenseNumber[0] == '1' ? Color.LightGreen : Color.PaleVioletRed;
                lbl1C.BackColor = SettinsTeble.licenseNumber[1] == '1' ? Color.LightGreen : Color.PaleVioletRed;
                lblEdit.BackColor = SettinsTeble.licenseNumber[2] == '1' ? Color.LightGreen : Color.PaleVioletRed;
                lblNN.BackColor = SettinsTeble.licenseNumber[3] == '1' ? Color.LightGreen : Color.PaleVioletRed;
                lblUpload.BackColor = SettinsTeble.licenseNumber[4] == '1' ? Color.LightGreen : Color.PaleVioletRed;
            }
            
        }
    }
}
