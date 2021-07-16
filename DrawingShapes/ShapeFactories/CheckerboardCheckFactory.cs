using System.Drawing;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    class CheckerboardCheckFactory : IShapeFactory
    {
        private readonly int _widthCanvas;
        private readonly int _heightCanvas;
        private readonly int _tileSize;


        public CheckerboardCheckFactory(int widthCanvas, int heightCanvas, int tileSize)
        {
            _widthCanvas = widthCanvas;
            _heightCanvas = heightCanvas;
            _tileSize = tileSize;
        }

        public IShape Create(int x, int y)
        {
            return new CheckerboardCheck(x, y, _widthCanvas, _heightCanvas, _tileSize);
        }

        public IShape Create(int x, int y, int width, int height, Color color)
        {
            throw new System.NotImplementedException();
        }

        public IShape Create(int x, int y, int width, int height)
        {
            throw new System.NotImplementedException();
        }
    }
}
