using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoWallE
{
    /// <summary>
    /// Persistent drawing canvas via an image. Scaling this image might produce aliased results.
    /// Try to improve this project using your objects and managing an internal collection of drawable objects rather than an image.
    /// </summary>
    public partial class Canvas : UserControl
    {
        public Canvas()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetBackBuffer(128, 128);
            _color = Color.Black;
        }

        Graphics _gr;
        Bitmap _backBuffer;
        Color _color;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0);
        }

        private void SetBackBuffer(int width, int height)
        {
            _backBuffer = new Bitmap(width, height);
            _gr = Graphics.FromImage(_backBuffer);
            _gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            _gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            _gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            _gr.PageUnit = GraphicsUnit.Pixel;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (ClientSize.Width > 0 && ClientSize.Height > 0)
                SetBackBuffer(this.ClientSize.Width, this.ClientSize.Height);
        }

        public void Clear (Color backColor)
        {
            _gr.Clear(backColor);
        }

        public void SetColor(Color color)
        {
            this._color = color;
        }

        public void DrawPoint(Point p, string label)
        {
            _gr.FillEllipse(new SolidBrush(_color), p.X - 4, p.Y - 4, 8, 8);
            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p.X + 6, p.Y - 6);
        }
        public void DrawArc(Point p1, Point p2, Point p3, int radius, string label)
        {
            float alpha1 = GetAlpha(p1, p3, radius);
            float alpha2 = GetAlpha(p1, p2, radius) - alpha1;

            _gr.DrawArc(new Pen(_color), p1.X - radius, p1.Y - radius, radius * 2, radius * 2, alpha1, alpha2);

            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p1.X + radius + 4, p1.Y);
        }

        public void DrawLine(Point p1, Point p2, string label)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            Point far1 = new Point(p2.X + dx * 1000, p2.Y + dy * 1000);
            Point far2 = new Point(p1.X - dx * 1000, p1.Y - dy * 1000);
            _gr.DrawLine(new Pen(_color,2), far1, far2);
            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p1.X + 6, p1.Y - 6);
        }

        public void DrawSegment(Point p1, Point p2, string label)
        {
            
            Point far1 = new Point(p2.X, p2.Y);
            Point far2 = new Point(p1.X, p1.Y);
            _gr.DrawLine(new Pen(_color,2), far1, far2);
            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p1.X + 6, p1.Y - 6);
        }

        public void DrawRay(Point p1, Point p2, string label)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            Point far1 = new Point(p2.X, p2.Y);
            Point far2 = new Point(p1.X - dx * 1000, p1.Y - dy * 1000);
            _gr.DrawLine(new Pen(_color,2), far1, far2);
            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p1.X + 6, p1.Y - 6);
        }
        public void DrawCircle(Point center, int radius, string label)
        {
            _gr.DrawEllipse(new Pen(_color,2), center.X - radius, center.Y - radius, radius * 2, radius * 2);

            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, center.X + radius + 4, center.Y);
        }
        private float GetAlpha(Point p1, Point p2, int radius)
        {
            double alpha = 0;
            double[] vector1 = { p2.X - p1.X, p2.Y - p1.Y };
            double[] vector2 = { radius, 0 };
            double v1Norm = Math.Sqrt(vector1[0] * vector1[0] + vector1[1] * vector1[1]);
            double v2Norm = Math.Sqrt(vector2[0] * vector2[0] + vector2[1] * vector2[1]);
            alpha = Math.Acos((vector1[0] * vector2[0] + vector1[1] * vector2[1]) / (v1Norm * v2Norm));
            alpha = alpha * 180 / Math.PI;
            if (p2.Y < p1.Y)
                alpha = 360 - alpha;
            return (float)alpha;
        }
    }
}
