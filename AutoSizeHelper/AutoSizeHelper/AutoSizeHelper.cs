using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoSizeTools
{
    public struct ScaleRate
    {
        public double xRate;
        public double yRate;
        public double wRate;
        public double hRate;
        public double fontRate;
    }

    public class AutoSizeHelper
    {
        protected Control? _container;
        protected Dictionary<string, ScaleRate> scaleMap;
        protected Dictionary<string, Size> ContainerDesignSizes;
        protected Dictionary<string, ScaleRate> toolStripScaleMap;
        protected Size formDesignedSize;

        public AutoSizeHelper()
        {
            scaleMap = new Dictionary<string, ScaleRate>();
            ContainerDesignSizes = new Dictionary<string, Size>();
            toolStripScaleMap = new Dictionary<string, ScaleRate>();
        }

        public AutoSizeHelper(Control container):this()
        {
            SetContainer(container);
        }

        /// <summary>
        /// SetContainer.you don't need to call UpdateControls method in container's sizechanged event.
        /// If you need to do something in container's sizechanged event,use AutoSizeHelperEx.
        /// </summary>
        /// <param name="container">Container Control</param>
        public virtual void SetContainer(Control container)
        {
            if (container == null || !(container is Control))
            {
                return;
            }

            _container = container;

            _container.SizeChanged += (s, e) =>
            {
                UpdateControls();
            };

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
                    scaleRate.wRate = item.Width *1.0 / container.Width;
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
                    //scaleRate.fontRate = curCtrl.Font.Size / Math.Min(_container.Height, _container.Width);
                }
                else
                {
                    scaleRate.xRate = curCtrl.Location.X * 1.0 / curCtrl.Parent.Width;
                    scaleRate.yRate = curCtrl.Location.Y * 1.0 / curCtrl.Parent.Height;
                    scaleRate.wRate = curCtrl.Width * 1.0 / curCtrl.Parent.Width;
                    scaleRate.hRate = curCtrl.Height * 1.0 / curCtrl.Parent.Height;
                    //scaleRate.fontRate = curCtrl.Font.Size / Math.Min(_container.Height, _container.Width);
                }

                scaleMap[curCtrl.Name] = scaleRate;
            }
        }

        /// <summary>
        /// Update control properties within the container and refresh container.
        /// </summary>
        public virtual void UpdateControls()
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
               // _container.Invalidate();
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
                //_container.Invalidate();
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
                        //newFont = (float)Math.Round(scaleRate.fontRate *
                        //    Math.Min(_container.Height, _container.Width), 2);
                    }
                    else
                    {
                        newX = (int)Math.Round(scaleRate.xRate * curCtrl.Parent.Width);
                        newY = (int)Math.Round(scaleRate.yRate * curCtrl.Parent.Height);
                        newW = (int)Math.Round(scaleRate.wRate * curCtrl.Parent.Width);
                        newH = (int)Math.Round(scaleRate.hRate * curCtrl.Parent.Height);
                        //newFont = (float)Math.Round(scaleRate.fontRate *
                        //    Math.Min(_container.Height, _container.Width), 2);
                    }

                    curCtrl.Width = newW;
                    curCtrl.Height = newH;
                    curCtrl.Location = new Point(newX, newY);
                    //curCtrl.Font = new Font(curCtrl.Font.FontFamily, newFont);
                }
            }

           // _container.Invalidate();
           _container.ResumeLayout();
        }

        /// <summary>
        /// Add a new child control to the container.
        /// </summary>
        /// <param name="ctrl"></param>
        public void AddNewControl(Control ctrl)
        {
            if (ctrl.Parent != null)
            {
                ScaleRate scaleRate = new ScaleRate();
                if (ctrl.Parent == _container && _container is Form)
                {
                    scaleRate.xRate = ctrl.Location.X * 1.0 / formDesignedSize.Width;
                    scaleRate.yRate = ctrl.Location.Y * 1.0 / formDesignedSize.Height;
                    scaleRate.wRate = ctrl.Width * 1.0 / formDesignedSize.Width;
                    scaleRate.hRate = ctrl.Height * 1.0 / formDesignedSize.Height;
                    //scaleRate.fontRate = ctrl.Font.Size / Math.Min(_container.Height, _container.Width);
                }
                else
                {
                    Size parentDesignSize = ContainerDesignSizes[ctrl.Parent.Name];
                    scaleRate.xRate = ctrl.Location.X * 1.0 / parentDesignSize.Width;
                    scaleRate.yRate = ctrl.Location.Y * 1.0 / parentDesignSize.Height;
                    scaleRate.wRate = ctrl.Width * 1.0 / parentDesignSize.Width;
                    scaleRate.hRate = ctrl.Height * 1.0 / parentDesignSize.Height;
                    //scaleRate.fontRate = ctrl.Font.Size / Math.Min(_container.Height, _container.Width);
                }

                scaleMap[ctrl.Name] = scaleRate;
            }
        }
    }
}