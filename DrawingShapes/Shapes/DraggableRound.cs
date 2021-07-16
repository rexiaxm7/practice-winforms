using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawingShapes.Shapes
{
    class DraggableRound : IDraggableShape
    {
        public DraggableRound(int x, int y, int width, int height)
        {
            Point = new Point(x, y);
            Size = new Size(width, height);
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Yellow, new Rectangle(Point, Size));
            if (Selected)
            {
                g.DrawEllipse(Pens.Red, new Rectangle(Point, Size));
            }
        }

        public Point Point { get; set; }
        public Size Size { get; set; }
        public bool Preview { get; set; }
        public bool Selected { get; set; }
        public bool Contains(int x, int y)
        {
            using (var ellipse = new GraphicsPath())
            {
                ellipse.AddEllipse(new Rectangle(Point, Size));

                return ellipse.IsVisible(x, y);
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
