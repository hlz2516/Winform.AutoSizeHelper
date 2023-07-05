using AutoSizeTools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Demo.ajdust_font
{
    public partial class FontForm : Form
    {
        AutoSizeHelperEx helper;
        public FontForm()
        {
            InitializeComponent();
            helper = new AutoSizeHelperEx(this);
        }

        private void FontForm_SizeChanged(object sender, EventArgs e)
        {
            Size screenSize = Screen.PrimaryScreen.Bounds.Size;
            if (screenSize == new Size(1920,1080)) // in 1920*1080(16:9) screen,when maximized,it's OK
            {
                helper.FontAdjustRate = 1.0f;
            }
            else if (screenSize == new Size(1280, 960))  // in 1280*960(4:3) screen,
            {
                helper.FontAdjustRate = 0.8f; //when maximized,the word will overflow if you annotate this line
            }
        }
    }
}
