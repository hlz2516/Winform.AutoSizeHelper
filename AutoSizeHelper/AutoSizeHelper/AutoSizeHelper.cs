using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoSizeTools
{
    internal struct ScaleRate
    {
        public double xRate;
        public double yRate;
        public double wRate;
        public double hRate;
        public double fontRate;
    }

    public class AutoSizeHelper
    {
        private Control? _container;
        private Dictionary<string, ScaleRate> scaleMap;
        private Dictionary<string, Size> ContainerDesignSizes;
        private Size formDesignedSize;

        public AutoSizeHelper()
        {
            scaleMap = new Dictionary<string, ScaleRate>();
            ContainerDesignSizes = new Dictionary<string, Size>();
        }
        /// <summary>
        /// SetContainer.When the container triggers the SizeChanged event, the controls inside the container will 
        /// automatically undergo adaptive changes in size, position, and font properties.
        /// </summary>
        /// <param name="container">Container Control</param>
        public void SetContainer(Control container)
        {
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

            if (container is Form)
            {
                formDesignedSize = container.ClientSize;
            }

            Queue<Control> queue = new Queue<Control>();
            queue.Enqueue(_container);

            while (queue.Count > 0)
            {
                Control curCtrl = queue.Dequeue();

                foreach (Control ctrl in curCtrl.Controls)
                {
                    queue.Enqueue(ctrl);
                }

                if (curCtrl == _container)
                {
                    continue;
                }

                if (curCtrl is ContainerControl || curCtrl is Panel)
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
        /// Update control properties within the container.It is usually called after adding a new control.
        /// </summary>
        public void UpdateControls()
        {
            if (_container is ListView)
            {
                ListView list = _container as ListView;
                foreach (ColumnHeader col in list.Columns)
                {
                    var scale = scaleMap[col.Text];
                    col.Width = (int)Math.Round(list.Width * scale.wRate);
                }
                _container.Invalidate();
                return;
            }

            Queue<Control> queue = new Queue<Control>();
            queue.Enqueue(_container);

            while (queue.Count > 0)
            {
                Control curCtrl = queue.Dequeue();

                foreach (Control ctrl in curCtrl.Controls)
                {
                    queue.Enqueue(ctrl);
                }

                if (curCtrl == _container)
                {
                    continue;
                }

                if (scaleMap.ContainsKey(curCtrl.Name))
                {
                    var scaleRate = scaleMap[curCtrl.Name];
                    int newX,newY, newW, newH;
                    float newFont;
                    if (curCtrl.Parent == _container && _container is Form)
                    {
                        newX = (int)Math.Round(scaleRate.xRate * _container.ClientSize.Width);
                        newY = (int)Math.Round(scaleRate.yRate * _container.ClientSize.Height);
                        newW = (int)Math.Round(scaleRate.wRate * _container.ClientSize.Width);
                        newH = (int)Math.Round(scaleRate.hRate * _container.ClientSize.Height);
                        newFont = (float)Math.Round(scaleRate.fontRate *
                            Math.Min(_container.Height, _container.Width), 2);
                    }
                    else
                    {
                        newX = (int)Math.Round(scaleRate.xRate * curCtrl.Parent.Width);
                        newY = (int)Math.Round(scaleRate.yRate * curCtrl.Parent.Height);
                        newW = (int)Math.Round(scaleRate.wRate * curCtrl.Parent.Width);
                        newH = (int)Math.Round(scaleRate.hRate * curCtrl.Parent.Height);
                        newFont = (float)Math.Round(scaleRate.fontRate *
                            Math.Min(_container.Height, _container.Width), 2);
                    }

                    curCtrl.Width = newW;
                    curCtrl.Height = newH;
                    curCtrl.Location = new Point(newX, newY);
                    curCtrl.Font = new Font(curCtrl.Font.FontFamily, newFont);
                }
            }

            _container.Invalidate();
        }

        /// <summary>
        /// Add a new child control to the container. After adding, the control will adapt to the current layout.
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
                    scaleRate.fontRate = ctrl.Font.Size / Math.Min(_container.Height, _container.Width);
                }
                else
                {
                    Size parentDesignSize = ContainerDesignSizes[ctrl.Parent.Name];
                    scaleRate.xRate = ctrl.Location.X * 1.0 / parentDesignSize.Width;
                    scaleRate.yRate = ctrl.Location.Y * 1.0 / parentDesignSize.Height;
                    scaleRate.wRate = ctrl.Width * 1.0 / parentDesignSize.Width;
                    scaleRate.hRate = ctrl.Height * 1.0 / parentDesignSize.Height;
                    scaleRate.fontRate = ctrl.Font.Size / Math.Min(_container.Height,_container.Width);
                }

                scaleMap[ctrl.Name] = scaleRate;
            }
        }
    }
}