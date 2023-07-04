using AutoSizeTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.adapt_toolstrip
{
    public partial class ToolStripForm : Form
    {
        AutoSizeHelperEx helper;
        public ToolStripForm()
        {
            InitializeComponent();
            helper = new AutoSizeHelperEx(this);
        }

        private void ToolStripForm_SizeChanged(object sender, EventArgs e)
        {
            helper.UpdateControls();
        }
    }
}
