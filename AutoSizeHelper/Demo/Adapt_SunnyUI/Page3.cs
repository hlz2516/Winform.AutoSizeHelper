using AutoSizeTools;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Adapt_SunnyUI
{
    public partial class Page3 : UIPage
    {
        AutoSizeHelperEx helper;

        public Page3()
        {
            InitializeComponent();
            helper = new AutoSizeHelperEx();
            helper.SetContainer(this);
        }

        private void Page3_SizeChanged(object sender, EventArgs e)
        {
            helper.UpdateControls();
        }
    }
}
