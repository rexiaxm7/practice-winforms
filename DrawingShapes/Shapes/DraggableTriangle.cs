using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawingShapes.Shapes
{
    public class DraggableTriangle : IDraggableShape
    {
        public DraggableTriangle(int x, int y, int width, int height, Color color)
        {
            Color = color;
            Point = new Point(x, y);
            Size = new Size(width, height);
        }
        public void Draw(Graphics g)
        {
            using (var brush = new SolidBrush(Color))
            using (var triangle = new GraphicsPath())
            {
                triangle.AddPolygon(CalcTrianglePoints());
                g.FillPath(brush,triangle);
                if (Selected)
                {
                    g.DrawPath(Pens.Red, triangle);
                }
            }
        }

        private Point[] CalcTrianglePoints()
        {
            var top = new Point(Point.X + Size.Width / 2, Point.Y);
            var left = new Point(Point.X, Point.Y + Size.Height);
            var right = Point.Add(Point, Size);

            return new Point[] { top, left, right };
        }

        public Color Color { get; set; }
        public Point Point { get; set; }
        public Size Size { get; set; }
        public bool Preview { get; set; }
        public bool Selected { get; set; }
        public bool Contains(int x, int y)
        {
            using (var triangle = new GraphicsPath())
            {
                triangle.AddPolygon(CalcTrianglePoints());
                return triangle.IsVisible(x, y);
            }
        }

        public void Resize(Point start, Point end)
        {
            var newShape = CreateNewShape(start, end);
            Point = newShape.Location;
            Size = newShape.Size;
        }

        private static Rectangle CreateNewShape(Point start, Point end)
        {
            var maxX = Math.Max(start.X, end.X);
            var minX = Math.Min(start.X, end.X);
            var maxY = Math.Max(start.Y, end.Y);
            var minY = Math.Min(start.Y, end.Y);
            var newShape = Rectangle.FromLTRB(minX, minY, maxX, maxY);

            return newShape;
        }
    }
}