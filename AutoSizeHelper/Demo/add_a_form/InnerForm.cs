﻿using AutoSizeTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.add_a_form
{
    public partial class InnerForm : Form
    {
        AutoSizeHelper helper = new AutoSizeHelper();

        public InnerForm()
        {
            InitializeComponent();
            helper.SetContainer(this);
        }
    }
}
