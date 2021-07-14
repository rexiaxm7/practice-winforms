using System.Drawing;

namespace DrawingShapes.Shapes
{
    public class Square : IDeletableShape
    {
        private readonly Brush _brushColor;
        private readonly int _positionX;
        private readonly int _positionY;
        private readonly int _radius;

        public Square(Brush brushColor, int positionX, int positionY)
        {
            _brushColor = brushColor;
            _positionX = positionX;
            _positionY = positionY;
            _radius = 30;
        }

        public void Draw(Graphics g)
        {
            int x = _positionX - _radius / 2;
            int y = _positionY - _radius / 2;

            g.FillRectangle(_brushColor, x, y, _radius, _radius);
        }

        public void DrawOnly(Graphics g)
        {
            g.FillRectangle(_brushColor, _positionX, _positionY, _radius, _radius);
        }
    }
}
