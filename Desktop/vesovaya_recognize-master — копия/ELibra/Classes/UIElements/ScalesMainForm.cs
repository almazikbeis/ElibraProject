using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ELibra.Classes.UIElements
{
    public class ScalesMainForm
    {
        public ScaleMainComponents scales = new ScaleMainComponents();

        public void Initialize(Scales scale, int position)
        {
            #region Name
            scales.scaleNameLable = new Label();
            scales.scaleNameLable.AutoSize = true;
            scales.scaleNameLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scales.scaleNameLable.Location = new System.Drawing.Point(38, 3);
            scales.scaleNameLable.Name = "scaleNameLable";
            scales.scaleNameLable.Size = new System.Drawing.Size(51, 16);
            scales.scaleNameLable.TabIndex = 5;
            scales.scaleNameLable.Text = scale.nameUser;
            #endregion

            #region Weight
            scales.scaleWeightLable = new Label();
            scales.scaleWeightLable.AutoSize = true;
            scales.scaleWeightLable.BackColor = System.Drawing.Color.Black;
            scales.scaleWeightLable.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            scales.scaleWeightLable.ForeColor = System.Drawing.Color.Red;
            scales.scaleWeightLable.Location = new System.Drawing.Point(38, 19);
            scales.scaleWeightLable.Name = "scaleWeightLable";
            scales.scaleWeightLable.Size = new System.Drawing.Size(78, 18);
            scales.scaleWeightLable.TabIndex = 14;
            scales.scaleWeightLable.Text = " 000000";
            #endregion

            #region Button
            //scales.scaleButton = new Button();
            //scales.scaleButton.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
            //scales.scaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            //scales.scaleButton.Location = new System.Drawing.Point(3, 5);
            //scales.scaleButton.Name = "scaleButton";
            //scales.scaleButton.Size = new System.Drawing.Size(32, 32);
            //scales.scaleButton.TabIndex = 6;
            //scales.scaleButton.Text = "✖";//✖✔
            //scales.scaleButton.UseVisualStyleBackColor = false;
            scales.scaleButton = new Panel();
            scales.scaleButton.BackgroundImage = Properties.Resources.red;
            scales.scaleButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            scales.scaleButton.Location = new System.Drawing.Point(3, 3);
            scales.scaleButton.Name = "panel1";
            scales.scaleButton.Size = new System.Drawing.Size(32, 32);
            //scales.scaleButton.TabIndex = 4;
            scales.scaleButton.MouseDown += new MouseEventHandler(this.button_MouseDown);
            scales.scaleButton.MouseUp += new MouseEventHandler(this.button_MouseUp);
            scales.scaleButton.Tag = "red";

            #endregion

            #region Panel
            scales.scalePanel = new Panel();
            scales.scalePanel.Name = scale.name;
            scales.scalePanel.Controls.Add(scales.scaleNameLable);
            scales.scalePanel.Controls.Add(scales.scaleWeightLable);
            scales.scalePanel.Controls.Add(scales.scaleButton);
            scales.scalePanel.Size = new System.Drawing.Size(119, 41);
            scales.scalePanel.Location = new System.Drawing.Point(416 + (119 * position + 6), 3);
            //scalePanel.Location = new System.Drawing.Point(416, 3);
            //scalePanel.Location = new System.Drawing.Point(541, 3);
            //gap -- 6
            scales.scalePanel.TabIndex = 11;

            #endregion

            #region PageToolTip
            scales.scaleToolTip = new ToolTip();
            scales.scaleToolTip.SetToolTip(scales.scalePanel, scale.nameUser);
            scales.scaleToolTip.SetToolTip(scales.scaleWeightLable, scale.nameUser);
            scales.scaleToolTip.SetToolTip(scales.scaleNameLable, scale.nameUser);
            #endregion
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            if (scales.scaleButton.Tag == "red")
            {
                scales.scaleButton.BackgroundImage = Properties.Resources.red;
            }          
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (scales.scaleButton.Tag == "red" )
            {
                scales.scaleButton.BackgroundImage = Properties.Resources.red_pushed;
            }
        }

        public void Dispose()
        {
            scales.scaleNameLable.Dispose();
            scales.scaleWeightLable.Dispose();
            scales.scaleButton.Dispose();
            scales.scalePanel.Dispose();
        }

    }
}
