using System.Drawing;

namespace DrawingShapes.Shapes
{
    public class Triangle : IDeletableShape
    {
        private readonly Brush _brushColor;
        private readonly int _positionX;
        private readonly int _positionY;
        private readonly int _radius;

        public Triangle(Brush brushColor, int positionX, int positionY)
        {
            _brushColor = brushColor;
            _positionX = positionX;
            _positionY = positionY;
            _radius = 30;
        }

        public void Draw(Graphics g)
        {
            Point p1 = new Point(_positionX ,_positionY - _radius / 2);
            Point p2 = new Point(_positionX - _radius / 2, _positionY + _radius / 2);
            Point p3 = new Point(_positionX + _radius / 2, _positionY + _radius / 2);

            Point[] points = new[] { p1, p2, p3 };

            g.FillPolygon(_brushColor, points);
        }
    }
}
