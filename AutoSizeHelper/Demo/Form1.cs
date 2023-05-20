using System;
using System.Drawing;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        AutoSizeHelper.AutoSizeHelper helper;
        public Form1()
        {
            InitializeComponent();
            helper = new AutoSizeHelper.AutoSizeHelper();
            helper.SetContainer(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button newBtn = new Button();
            newBtn.Name = "button7";
            newBtn.Location = new Point(568, 12);
            newBtn.Size = new System.Drawing.Size(75, 23);
            newBtn.Text = "button7";
            //calcul button6's font rate and apply this rate to newBtn font
            float fontRate = button6.Font.Size / this.Height;
            float newBtnSize = (float)Math.Round(this.Height * fontRate);
            newBtn.Font = new Font(button6.Font.FontFamily, newBtnSize);
            newBtn.UseVisualStyleBackColor = true;
            this.Controls.Add(newBtn);
            helper.AddNewControl(newBtn);
            helper.UpdateControls();
        }
    }
}
