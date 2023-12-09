using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    public enum GradientType
    {
        Linear,
        Radial
    }

    public class CustomPanel : Panel
    {
        public int TopLeftCornerRadius { get; set; } = 10;
        public int TopRightCornerRadius { get; set; } = 10;
        public int BottomLeftCornerRadius { get; set; } = 10;
        public int BottomRightCornerRadius { get; set; } = 10;

        public Color StartColor { get; set; } = Color.Blue;
        public Color EndColor { get; set; } = Color.Green;

        public GradientType GradientType { get; set; } = GradientType.Linear;
        public float GradientAngle { get; set; } = 0;
        public Point GradientCenter { get; set; }

        [Browsable(true)] // Make UseGradientFill property visible in Property Grid
        public bool UseGradientFill { get; set; } = true;

        public CustomPanel()
        {
            DoubleBuffered = true;
            GradientCenter = new Point(Width / 2, Height / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (GraphicsPath path = CreateRoundRectanglePath(ClientRectangle,
                TopLeftCornerRadius, TopRightCornerRadius, BottomLeftCornerRadius, BottomRightCornerRadius))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                if (UseGradientFill)
                {
                    using (Brush brush = CreateGradientBrush())
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            }
        }

        private GraphicsPath CreateRoundRectanglePath(Rectangle bounds, int topLeft, int topRight, int bottomLeft, int bottomRight)
        {
            GraphicsPath path = new GraphicsPath();

            if (topLeft > 0)
                path.AddArc(bounds.Left, bounds.Top, topLeft * 2, topLeft * 2, 180, 90);
            else
                path.AddLine(bounds.Left, bounds.Top, bounds.Left, bounds.Top);

            if (topRight > 0)
                path.AddArc(bounds.Right - topRight * 2, bounds.Top, topRight * 2, topRight * 2, 270, 90);
            else
                path.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Top);

            if (bottomRight > 0)
                path.AddArc(bounds.Right - bottomRight * 2, bounds.Bottom - bottomRight * 2, bottomRight * 2, bottomRight * 2, 0, 90);
            else
                path.AddLine(bounds.Right, bounds.Bottom, bounds.Right, bounds.Bottom);

            if (bottomLeft > 0)
                path.AddArc(bounds.Left, bounds.Bottom - bottomLeft * 2, bottomLeft * 2, bottomLeft * 2, 90, 90);
            else
                path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Bottom);

            path.CloseFigure();

            return path;
        }

        private Brush CreateGradientBrush()
        {
            Rectangle gradientBounds = ClientRectangle;

            switch (GradientType)
            {
                case GradientType.Linear:
                    return new LinearGradientBrush(gradientBounds, StartColor, EndColor, GradientAngle);

                case GradientType.Radial:
                    return new LinearGradientBrush(gradientBounds, StartColor, EndColor, LinearGradientMode.BackwardDiagonal);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
