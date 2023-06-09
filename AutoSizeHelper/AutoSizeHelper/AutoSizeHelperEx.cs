using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoSizeTools
{
    public class AutoSizeHelperEx : AutoSizeHelper
    {
        /// <summary>
        /// The font scale rate.The value range is about from 0.1 to 1.5.
        /// Value 0 means the font inside container does not scale.
        /// </summary>
        public float FontAdjustRate { get; set; } = 0;

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
            if (container == null)
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
                    if (ctrl is Form)
                    {
                        continue;
                    }
                    if (ctrl.Parent is UserControl && !(_container is UserControl))
                    {
                        continue;
                    }
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
        /// Update control properties within the container and refresh container.
        /// </summary>
        public override void UpdateControls()
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
                    if (ctrl is Form)
                    {
                        continue;
                    }
                    if (ctrl.Parent is UserControl && !(_container is UserControl))
                    {
                        continue;
                    }
                    queue.Enqueue(ctrl);
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

            _container.Invalidate();
        }
    }
}
