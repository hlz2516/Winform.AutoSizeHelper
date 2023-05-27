using AutoSizeTools;
using System;
using System.Windows.Forms;

namespace Demo.scale_usercontrol
{
    public partial class UserForm : Form
    {
        AutoSizeHelperEx helper;
        AutoSizeHelperEx helperForUC;
        public UserForm()
        {
            InitializeComponent();
            helper = new AutoSizeHelperEx(this);
            helperForUC = new AutoSizeHelperEx(userControl11);
        }

        private void userControl11_SizeChanged(object sender, EventArgs e)
        {
            helperForUC.UpdateControls();
        }

        private void UserForm_SizeChanged(object sender, EventArgs e)
        {
            helper.UpdateControls();
        }
    }
}
