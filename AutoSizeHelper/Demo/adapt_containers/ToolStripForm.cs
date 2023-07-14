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

namespace Demo.adapt_containers
{
    public partial class ToolStripForm : Form
    {
        AutoSizeHelper helper;
        AutoSizeHelperEx helper2;
        public ToolStripForm()
        {
            InitializeComponent();
            helper2 = new AutoSizeHelperEx(toolStrip1);
            helper = new AutoSizeHelper(this);
        }

        private void ToolStripForm_SizeChanged(object sender, EventArgs e)
        {
            helper2.UpdateControls();
        }
    }
}
