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
    public partial class FHeaderMain : UIHeaderMainFrame
    {
        public FHeaderMain()
        {
            InitializeComponent();

            //设置关联
            Header.TabControl = MainTabControl;

            //增加页面到Main
            AddPage(new Page1(), 1004);
            AddPage(new Page2(), 1005);
            AddPage(new Page3(), 1006);

            //设置Header节点索引
            Header.SetNodePageIndex(Header.Nodes[0], 1004);
            Header.SetNodePageIndex(Header.Nodes[1], 1005);
            Header.SetNodePageIndex(Header.Nodes[2], 1006);

            //显示默认界面
            Header.SelectedIndex = 0;
        }
    }
}
