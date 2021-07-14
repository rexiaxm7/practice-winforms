using System.Drawing;

namespace DrawingShapes.Shapes
{
    public class Round : IDeletableShape
    {
        private readonly Brush _brushColor;
        private readonly int _positionX;
        private readonly int _positionY;
        private readonly int _radius;

        public Round(Brush brushColor, int positionX, int positionY)
        {
            this._brushColor = brushColor;
            this._positionX = positionX;
            this._positionY = positionY;
            this._radius = 30;
        }
        public void Draw(Graphics g)
        {
            int x = _positionX - _radius / 2;
            int y = _positionY - _radius / 2;

            g.FillEllipse(_brushColor, x, y, _radius, _radius);
        }
    }
}
