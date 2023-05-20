using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoSizeHelper
{
    internal struct ScaleRate
    {
        public double xRate;
        public double yRate;
        public double wRate;
        public double hRate;
        public double fontRate;
    }
    /// <summary>
    /// 分辨率自适应辅助类。
    /// 当一个窗体或控件发生大小变化时，其内部的多数控件会根据其在原设计器中的位置和大小比例进行自适应变化
    /// </summary>
    public class AutoSizeHelper
    {
        private Control? _container;

        private Dictionary<string, ScaleRate> scaleMap;

        //容器原设计大小集合
        private Dictionary<string, Size> ContainerDesignSizes;

        public AutoSizeHelper()
        {
            scaleMap = new Dictionary<string, ScaleRate>();
            ContainerDesignSizes = new Dictionary<string, Size>();
        }
        /// <summary>
        /// 设置容器。当容器触发SizeChanged事件时，容器内的控件会自动进行大小，位置和字体属性的自适应变化。
        /// </summary>
        /// <param name="container">容器</param>
        public void SetContainer(Control container)
        {
            _container = container;
            //Console.WriteLine($"container design size:{container.Size}");
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

            Queue<Control> queue = new Queue<Control>();
            queue.Enqueue(_container);

            while (queue.Count > 0)
            {
                Control curCtrl = queue.Dequeue();

                foreach (Control ctrl in curCtrl.Controls)
                {
                    queue.Enqueue(ctrl);
                }

                //如果当前控件是容器，则加入到ContainerDesignSizes
                if (curCtrl is ContainerControl || curCtrl is Panel)
                {
                    ContainerDesignSizes.Add(curCtrl.Name, curCtrl.Size);
                }

                //对于一开始传入的容器不处理
                if (curCtrl == _container)
                {
                    continue;
                }

                //计算当前控件相对父容器的大小和位置比例，存储入map中
                var scaleRate = new ScaleRate
                {
                    xRate = curCtrl.Location.X * 1.0 / curCtrl.Parent.Width,
                    yRate = curCtrl.Location.Y * 1.0 / curCtrl.Parent.Height,
                    wRate = curCtrl.Width * 1.0 / curCtrl.Parent.Width,
                    hRate = curCtrl.Height * 1.0 / curCtrl.Parent.Height,
                    fontRate = curCtrl.Font.Size / Math.Min(_container.Height,_container.Width)
                };
                scaleMap[curCtrl.Name] = scaleRate;
            }
        }

        /// <summary>
        /// 更新容器内的控件属性
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
                //对于一开始传入的容器不处理
                if (curCtrl == _container)
                {
                    continue;
                }

                if (scaleMap.ContainsKey(curCtrl.Name))
                {
                    //根据map中存储的当前控件大小和位置比例，还原大小和位置
                    var scaleRate = scaleMap[curCtrl.Name];
                    int newX = (int)Math.Round(scaleRate.xRate * curCtrl.Parent.Width);
                    int newY = (int)Math.Round(scaleRate.yRate * curCtrl.Parent.Height);
                    int newW = (int)Math.Round(scaleRate.wRate * curCtrl.Parent.Width);
                    int newH = (int)Math.Round(scaleRate.hRate * curCtrl.Parent.Height);
                    float newFont = (float)Math.Round(scaleRate.fontRate *
                        Math.Min(_container.Height,_container.Width) , 2);

                    curCtrl.Width = newW;
                    curCtrl.Height = newH;
                    curCtrl.Location = new Point(newX, newY);
                    curCtrl.Font = new Font(curCtrl.Font.FontFamily, newFont);
                }
            }

            //刷新界面
            _container.Invalidate();
        }

        /// <summary>
        /// 向容器内添加新的子控件。添加后该控件具有自适应的功能
        /// </summary>
        /// <param name="ctrl">需要自适应功能的子控件</param>
        public void AddNewControl(Control ctrl)
        {
            //找到该控件的容器原设计大小
            if (ctrl.Parent != null)
            {
                string parentName = ctrl.Parent.Name;
                Size parentDesignSize = ContainerDesignSizes[parentName];
                //计算位置和大小比例，再加入到map中
                var scaleRate = new ScaleRate
                {
                    xRate = ctrl.Location.X * 1.0 / parentDesignSize.Width,
                    yRate = ctrl.Location.Y * 1.0 / parentDesignSize.Height,
                    wRate = ctrl.Width * 1.0 / parentDesignSize.Width,
                    hRate = ctrl.Height * 1.0 / parentDesignSize.Height,
                    fontRate = ctrl.Font.Size / Math.Min(_container.Height, _container.Width)
                };
                scaleMap[ctrl.Name] = scaleRate;
            }
        }
    }
}