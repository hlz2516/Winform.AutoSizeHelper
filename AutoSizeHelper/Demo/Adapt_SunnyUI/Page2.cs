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
    public partial class Page2 : UIPage
    {
        AutoSizeHelper helper;

        public Page2()
        {
            InitializeComponent();
            helper = new AutoSizeHelper(this);
        }
    }
}
