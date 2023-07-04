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
        AutoSizeHelper helper;
        AutoSizeHelper helper2;
        public ToolStripForm()
        {
            InitializeComponent();
            helper2 = new AutoSizeHelper(toolStrip1);
            helper = new AutoSizeHelper(this);
        }

        private void ToolStripForm_SizeChanged(object sender, EventArgs e)
        {
            //helper.UpdateControls();
        }
    }
}
