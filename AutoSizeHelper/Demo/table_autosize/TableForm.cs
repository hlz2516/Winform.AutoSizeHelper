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

namespace Demo.table_autosize
{
    public partial class TableForm : Form
    {
        AutoSizeHelper formHelper = new AutoSizeHelper();  //this helper autosize the form
        AutoSizeHelper tableHelper = new AutoSizeHelper();  //this helper autosize the table
        public TableForm()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                var row = new ListViewItem();
                row.Text = (i + 1).ToString();
                row.SubItems.Add("Alan");
                row.SubItems.Add( (i + 15).ToString());
                row.SubItems.Add("female");
                row.SubItems.Add("3");
                row.SubItems.Add("ctrl");
                listView1.Items.Add(row);
            }

            tableHelper.SetContainer(listView1);
            formHelper.SetContainer(this);
        }
    }
}
