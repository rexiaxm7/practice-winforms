using System.Drawing;

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
            {
                g.FillEllipse(brush, new Rectangle(Point, Size));
                if (Selected)
                {
                    g.DrawEllipse(Pens.Red, new Rectangle(Point, Size));
                }
            }
        }

        public Color Color { get; set; }
        public Point Point { get; set; }
        public Size Size { get; set; }
        public bool Preview { get; set; }
        public bool Selected { get; set; }
        public bool Contains(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public void Resize(Point start, Point end)
        {
            throw new System.NotImplementedException();
        }
    }
}