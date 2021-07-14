using System.Drawing;

namespace DrawingShapes.Shapes
{
    class BackGroundTile : IShape
    {
        private readonly Brush _brushColor;
        private readonly int _positionX;
        private readonly int _positionY;
        private readonly int _width;
        private readonly int _height;

        public BackGroundTile(Brush brushColor, int positionX, int positionY, int width, int height)
        {
            this._brushColor = brushColor;
            this._positionX = positionX;
            this._positionY = positionY;
            this._width = width;
            this._height = height;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(_brushColor, _positionX, _positionY, _width, _height);
        }
    }
}
