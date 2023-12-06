using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace License
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (tHost.Text != "")
                Properties.Settings.Default.Host = tHost.Text;
            Properties.Settings.Default.Save();
            Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            tHost.Text = Properties.Settings.Default.Host;
        }
    }
}
