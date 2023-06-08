using System;
using System.Drawing;
using System.Windows.Forms;
using AutoSizeTools;

namespace Demo.basic_use
{
    public partial class Form1 : Form
    {
        AutoSizeHelperEx helper;

        public Form1()
        {
            InitializeComponent();
            helper = new AutoSizeHelperEx(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button newBtn = new Button();
            newBtn.Name = "button7";
            newBtn.Location = new Point(568, 1);
            newBtn.Size = new System.Drawing.Size(75, 23);
            newBtn.Text = "button7";
            //apply button6's font to newBtn font
            newBtn.Font = new Font(button6.Font.FontFamily, button6.Font.Size);
            newBtn.UseVisualStyleBackColor = true;
            this.Controls.Add(newBtn);
            helper.AddNewControl(newBtn);
            helper.UpdateControls();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            helper.UpdateControls();
        }
    }
}
