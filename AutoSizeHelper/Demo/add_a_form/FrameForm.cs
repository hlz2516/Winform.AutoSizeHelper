using AutoSizeTools;
using System;
using System.Windows.Forms;

namespace Demo.add_a_form
{
    public partial class FrameForm : Form
    {
        AutoSizeHelper helper = new AutoSizeHelper();

        public FrameForm()
        {
            InitializeComponent();
            helper.SetContainer(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InnerForm innerForm = new InnerForm();
            innerForm.ControlBox = false;
            innerForm.TopLevel = false;
            innerForm.Dock = DockStyle.Fill;
            innerForm.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(innerForm);
            innerForm.Show();
        }
    }
}
