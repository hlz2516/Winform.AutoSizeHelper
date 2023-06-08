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
    public partial class FHeaderMainFooter : UIHeaderMainFooterFrame
    {
        public FHeaderMainFooter()
        {
            InitializeComponent();

            //设置关联
            Header.TabControl = MainTabControl;

            //增加页面到Main
            AddPage(new Page1(), 1001);
            AddPage(new Page2(), 1002);
            AddPage(new Page3(), 1003);

            //设置Header节点索引
            Header.CreateNode("Page1", 1001);
            Header.CreateNode("Page2", 1002);
            Header.CreateNode("Page3", 1003);

            //显示默认界面
            Header.SelectedIndex = 0;
        }
    }
}
