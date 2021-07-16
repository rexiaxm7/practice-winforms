using System;
using System.Drawing;

namespace DrawingShapes.Shapes
{
    class DraggableSquare : IDraggableShape
    {
        public DraggableSquare(int x, int y, int width, int height, Color color)
        {
            Color = color;
            Point = new Point(x, y);
            Size = new Size(width, height);
        }

        public Color Color { get; set; }
        public Point Point { get; set; }
        public Size Size { get; set; }
        public bool Preview { get; set; }
        public bool Selected { get; set; }
        public bool Contains(int x, int y)
        {
            var rectangle = new Rectangle(location:Point, size:Size);
            return rectangle.Contains(x, y);
        }

        public void Resize(Point start, Point end)
        {
            var newShape = CreateNewShape(start, end);
            Point = newShape.Location;
            Size = newShape.Size;
        }

        public void Draw(Graphics g)
        {
            using (var brush = new SolidBrush(Color))
            {
                g.FillRectangle(brush, new Rectangle(Point, Size));

                if (Selected)
                {
                    g.DrawRectangle(Pens.Red, new Rectangle(Point, Size));
                }
            }
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
