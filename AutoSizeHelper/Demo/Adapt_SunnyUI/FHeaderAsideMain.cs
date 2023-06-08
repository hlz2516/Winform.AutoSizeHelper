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
    public partial class FHeaderAsideMain : UIHeaderAsideMainFrame
    {
        public FHeaderAsideMain()
        {
            InitializeComponent();

            //设置关联
            Aside.TabControl = MainTabControl;

            //增加页面到Main
            AddPage(new Page1(), 1001);
            AddPage(new Page2(), 1002);
            AddPage(new Page3(), 1003);

            //设置Header节点索引
            Aside.CreateNode("Page1", 1001);
            Aside.CreateNode("Page2", 1002);
            Aside.CreateNode("Page3", 1003);

            //显示默认界面
            Aside.SelectFirst();
        }
    }
}
