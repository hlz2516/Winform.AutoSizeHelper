using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoSizeTools
{
    public class AutoSizeHelperEx : AutoSizeHelper
    {
        private float fontAdjustRate = 0;
        /// <summary>
        /// The font scale rate.The value range is about from 0.8 to 1.2.
        /// Value 0 means the font inside container does not scale.
        /// </summary>
        public float FontAdjustRate
        {
            get { return fontAdjustRate; }
            set
            {
                fontAdjustRate = value;
                if (_container != null)
                {
                    UpdateControls();
                }
            }
        }

        public AutoSizeHelperEx()
        {

        }

        public AutoSizeHelperEx(Control container) : base(container)
        {

        }
        /// <summary>
        /// Set container.you have to call UpdateControls method in container's sizechanged event.
        /// </summary>
        /// <param name="container"></param>
        public override void SetContainer(Control container)
        {
            if (container == null || !(container is Control))
            {
                return;
            }

            _container = container;

            if (container is ListView)
            {
                ListView list = container as ListView;
                foreach (ColumnHeader col in list.Columns)
                {
                    var scaleRate = new ScaleRate
                    {
                        wRate = col.Width * 1.0 / container.Width
                    };
                    scaleMap[col.Text] = scaleRate;
                }
                return;
            }

            if (container is ToolStrip)
            {
                ToolStrip toolStrip = container as ToolStrip;
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    var scaleRate = new ScaleRate();
                    scaleRate.wRate = item.Width * 1.0 / container.Width;
                    scaleRate.hRate = item.Height * 1.0 / container.Height;
                    toolStripScaleMap[item.Name] = scaleRate;
                }
                return;
            }

            if (container is Form)
            {
                formDesignedSize = container.ClientSize;
            }

            Queue<Control> queue = new Queue<Control>();
            queue.Enqueue(_container);

            while (queue.Count > 0)
            {
                Control curCtrl = queue.Dequeue();

                foreach (object obj in curCtrl.Controls)
                {
                    if (obj is Control)
                    {
                        Control ctrl = obj as Control;
                        if (ctrl is Form || ctrl is ToolStrip)
                        {
                            continue;
                        }
                        if (ctrl.Parent is UserControl && !(_container is UserControl))
                        {
                            continue;
                        }
                        queue.Enqueue(ctrl);
                    }
                }

                if (curCtrl == _container)
                {
                    continue;
                }

                if (curCtrl is GroupBox || curCtrl is Panel)
                {
                    ContainerDesignSizes.Add(curCtrl.Name, curCtrl.Size);
                }

                ScaleRate scaleRate = new ScaleRate();
                if (curCtrl.Parent == _container && _container is Form)
                {
                    scaleRate.xRate = curCtrl.Location.X * 1.0 / formDesignedSize.Width;
                    scaleRate.yRate = curCtrl.Location.Y * 1.0 / formDesignedSize.Height;
                    scaleRate.wRate = curCtrl.Width * 1.0 / formDesignedSize.Width;
                    scaleRate.hRate = curCtrl.Height * 1.0 / formDesignedSize.Height;
                    scaleRate.fontRate = curCtrl.Font.Size / Math.Min(_container.Height, _container.Width);
                }
                else
                {
                    scaleRate.xRate = curCtrl.Location.X * 1.0 / curCtrl.Parent.Width;
                    scaleRate.yRate = curCtrl.Location.Y * 1.0 / curCtrl.Parent.Height;
                    scaleRate.wRate = curCtrl.Width * 1.0 / curCtrl.Parent.Width;
                    scaleRate.hRate = curCtrl.Height * 1.0 / curCtrl.Parent.Height;
                    scaleRate.fontRate = curCtrl.Font.Size / Math.Min(_container.Height, _container.Width);
                }

                scaleMap[curCtrl.Name] = scaleRate;
            }
        }

        /// <summary>
        /// Update control properties within the container and refresh container.
        /// </summary>
        public override void UpdateControls()
        {
            _container.SuspendLayout();
            if (_container is ListView)
            {
                ListView list = _container as ListView;
                foreach (ColumnHeader col in list.Columns)
                {
                    var scale = scaleMap[col.Text];
                    col.Width = (int)Math.Round(list.Width * scale.wRate);
                }
                _container.ResumeLayout();
                return;
            }

            if (_container is ToolStrip)
            {
                ToolStrip toolStrip = _container as ToolStrip;
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    var rate = toolStripScaleMap[item.Name];
                    item.Width = (int)Math.Round(rate.wRate * _container.Width);
                    item.Height = (int)Math.Round(rate.hRate * _container.Height);
                }
                _container.ResumeLayout();
                return;
            }

            Queue<Control> queue = new Queue<Control>();
            queue.Enqueue(_container);

            while (queue.Count > 0)
            {
                Control curCtrl = queue.Dequeue();

                foreach (object obj in curCtrl.Controls)
                {
                    if (obj is Control)
                    {
                        Control ctrl = obj as Control;
                        if (ctrl is Form || ctrl is ToolStrip)
                        {
                            continue;
                        }
                        if (ctrl.Parent is UserControl && !(_container is UserControl))
                        {
                            continue;
                        }
                        queue.Enqueue(ctrl);
                    }
                }

                if (curCtrl == _container)
                {
                    continue;
                }

                if (scaleMap.ContainsKey(curCtrl.Name))
                {
                    var scaleRate = scaleMap[curCtrl.Name];
                    int newX, newY, newW, newH;
                    float newFont;
                    if (curCtrl.Parent == _container && _container is Form)
                    {
                        newX = (int)Math.Round(scaleRate.xRate * _container.ClientSize.Width);
                        newY = (int)Math.Round(scaleRate.yRate * _container.ClientSize.Height);
                        newW = (int)Math.Round(scaleRate.wRate * _container.ClientSize.Width);
                        newH = (int)Math.Round(scaleRate.hRate * _container.ClientSize.Height);
                        newFont = (float)Math.Round(scaleRate.fontRate *
                            Math.Min(_container.Height, _container.Width) * FontAdjustRate, 2);
                    }
                    else
                    {
                        newX = (int)Math.Round(scaleRate.xRate * curCtrl.Parent.Width);
                        newY = (int)Math.Round(scaleRate.yRate * curCtrl.Parent.Height);
                        newW = (int)Math.Round(scaleRate.wRate * curCtrl.Parent.Width);
                        newH = (int)Math.Round(scaleRate.hRate * curCtrl.Parent.Height);
                        newFont = (float)Math.Round(scaleRate.fontRate *
                            Math.Min(_container.Height, _container.Width) * FontAdjustRate, 2);
                    }

                    curCtrl.Width = newW;
                    curCtrl.Height = newH;
                    curCtrl.Location = new Point(newX, newY);
                    if (FontAdjustRate> 0)
                    {
                        curCtrl.Font = new Font(curCtrl.Font.FontFamily, newFont);
                    }
                }
            }

            _container.ResumeLayout();
        }
    }
}
