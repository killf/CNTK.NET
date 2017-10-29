using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNTK.Controls
{
    /// <summary>
    /// 坐标系
    /// </summary>
    public class Coordinates : Control
    {
        #region 成员变量

        private PointF ZeroLocation;         //坐标轴0点坐标
        private float ScaleX;               //X轴的缩放比例(位置长度/数学长度)
        private float ScaleY;               //Y轴缩放比例(位置长度/数学长度)
        #endregion

        #region 数据属性
        public int HeaderDistance { get { return _headerDistance; } set { _headerDistance = value; Invalidate(); } }
        private int _headerDistance = 10;

        /// <summary>
        /// 最大的X坐标
        /// </summary>
        public int MaxX { get { return _maxX; } set { _maxX = value; Invalidate(); } }
        private int _maxX = 4;

        /// <summary>
        /// 最大的Y坐标
        /// </summary>
        public int MaxY { get { return _maxY; } set { _maxY = value; Invalidate(); } }
        private int _maxY = 4;

        /// <summary>
        /// 最小的X坐标
        /// </summary>
        public int MinX { get { return _minX; } set { _minX = value; Invalidate(); } }
        private int _minX = -4;

        /// <summary>
        /// 最小的Y坐标
        /// </summary>
        public int MinY { get { return _minY; } set { _minY = value; Invalidate(); } }
        private int _minY = -4;

        /// <summary>
        /// X轴每个刻度的数学长度,最小为1
        /// </summary>
        public int DistanceX { get { return _distanceX; } set { _distanceX = value; Invalidate(); } }
        private int _distanceX = 1;

        /// <summary>
        /// Y轴每个刻度的数学长度,最小为1
        /// </summary>
        public int DistanceY { get { return _distanceY; } set { _distanceY = value; Invalidate(); } }
        private int _distanceY = 1;

        /// <summary>
        /// 坐标轴颜色
        /// </summary>
        public Color AxisColor { get { return _axisColor; } set { _axisColor = value; Invalidate(); } }
        private Color _axisColor = Color.DimGray;

        /// <summary>
        /// 坐标轴文本颜色
        /// </summary>
        public Color AxisTextColor { get { return _axisTextColor; } set { _axisTextColor = value; Invalidate(); } }
        private Color _axisTextColor = Color.DimGray;

        /// <summary>
        /// 坐标轴文本字体
        /// </summary>
        public Font AxisTextFont { get { return _axisTextFont; } set { _axisTextFont = value; Invalidate(); } }
        private Font _axisTextFont = new Font("微软雅黑", 11, FontStyle.Regular, GraphicsUnit.Pixel);

        /// <summary>
        /// 数据集
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ObservableCollection<DataSet> DataSets { get; private set; }

        #endregion

        #region 构造函数

        public Coordinates() : base()
        {
            Padding = new Padding(10);
            DataSets = new ObservableCollection<DataSet>();
            DataSets.CollectionChanged += (sender, e) => this.Invalidate();
        }

        #endregion

        #region override
        /// <summary>
        /// 更新位置信息
        /// </summary>
        private void UpdateLoaction()
        {
            ScaleX = (Width - Padding.Left - Padding.Right - _headerDistance * 2) * 1f / (MaxX - MinX);
            ScaleY = (Height - Padding.Top - Padding.Bottom - _headerDistance * 2) * 1f / (MaxY - MinY);

            ZeroLocation.X = _headerDistance - MinX * ScaleX + Padding.Left;
            ZeroLocation.Y = _headerDistance + MaxY * ScaleY + Padding.Top;
        }

        /// <summary>
        /// 后台绘制
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateLoaction();
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;

            g.Clear(BackColor);

            /*1.画坐标轴*/
            var pen = new Pen(AxisColor)
            {
                EndCap = LineCap.ArrowAnchor,
                DashStyle = DashStyle.Solid,
                Width = 1
            };

            g.DrawLine(pen, new PointF(ZeroLocation.X + MinX * ScaleX - _headerDistance, ZeroLocation.Y), new PointF(ZeroLocation.X + MaxX * ScaleX + _headerDistance, ZeroLocation.Y)); //x
            g.DrawLine(pen, new PointF(ZeroLocation.X, ZeroLocation.Y - MinY * ScaleY + _headerDistance), new PointF(ZeroLocation.X, ZeroLocation.Y - MaxY * ScaleY - _headerDistance)); //y            

            var brush = new SolidBrush(AxisTextColor);
            g.DrawString("x", AxisTextFont, brush, new PointF(ZeroLocation.X + ScaleX * (MaxX + 0.5f) - 8, ZeroLocation.Y));
            g.DrawString("y", AxisTextFont, brush, new PointF(ZeroLocation.X + 3, ZeroLocation.Y - ScaleY * (MaxY + 0.5f)));
            g.DrawString("0", AxisTextFont, brush, new PointF(ZeroLocation.X + 2, ZeroLocation.Y));

            /*2.绘制刻度*/
            pen.EndCap = LineCap.Flat;
            for (var i = MinX; i <= MaxX; i += DistanceX)
            {
                if (i == 0) continue;

                var x = ZeroLocation.X + ScaleX * i;
                var y = ZeroLocation.Y;

                g.DrawLine(pen, new PointF(x, y), new PointF(x, y - 4)); //x

                var lb = i.ToString();
                var width = g.MeasureString(lb, AxisTextFont).Width;
                g.DrawString(lb, AxisTextFont, brush, new PointF(x - width / 2 + 2, y));
            }

            for (var i = MinY; i <= MaxY; i += DistanceY)
            {
                if (i == 0) continue;

                var x = ZeroLocation.X;
                var y = ZeroLocation.Y - ScaleY * i;

                g.DrawLine(pen, new PointF(x, y), new PointF(x + 4, y)); //y

                var lb = i.ToString();
                var size = g.MeasureString(lb, AxisTextFont);
                g.DrawString(lb, AxisTextFont, brush, new PointF(x + 4, y - size.Height / 2 + 2));
            }

            /*3.绘制函数-后台线程*/
            foreach (var dataSet in DataSets)
            {
                DrawFunction(g, dataSet);
                g.Save();
            }
        }

        /// <summary>
        /// 绘制函数
        /// </summary>
        protected virtual void DrawFunction(Graphics g, DataSet dataSet)
        {
            if (dataSet == null || dataSet.Count < 2) return;

            for (var i = 1; i < dataSet.Count; i++)
            {
                var p1 = dataSet[i - 1];
                var p2 = dataSet[i];

                p1 += new SizeF(ZeroLocation.X + p1.X * ScaleX, ZeroLocation.Y - p1.Y * ScaleY);
                p2 += new SizeF(ZeroLocation.X + p2.X * ScaleX, ZeroLocation.Y - p2.Y * ScaleY);

                g.DrawLine(dataSet.Pen, p1, p2);
            }

            if (!string.IsNullOrEmpty(dataSet.Text))
            {
                var loc = dataSet[dataSet.Count - 1];
                var size = g.MeasureString(dataSet.Text, AxisTextFont);

                var x = ZeroLocation.X + loc.X * ScaleX;
                var y = ZeroLocation.Y - loc.Y * ScaleY;
                x += -size.Width + Padding.Left + Padding.Right;

                g.DrawString(dataSet.Text, AxisTextFont, dataSet.Pen.Brush, x, y);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
        #endregion
    }

    public class DataSet : ObservableCollection<PointF>
    {
        public string Text { get; set; }

        public Pen Pen { get; set; } = Pens.Black;

        public DataSet(string text = null)
        {
            this.Text = text;
        }
    }
}
