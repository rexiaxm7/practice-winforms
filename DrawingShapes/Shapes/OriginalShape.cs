using System.Collections.Generic;
using System.Drawing;
using DrawingShapes.ColorSelector;

namespace DrawingShapes.Shapes
{
    class OriginalShape : IShape
    {
        private readonly List<IShape> _shapes;

        public OriginalShape(int x, int y)
        {
            _shapes = new List<IShape>();

            CreateOriginalShape(x, y);
        }

        private void CreateOriginalShape(int x, int y)
        {
            var orderColorSelector = new OrderColorSelector();

            var square = new Square(orderColorSelector.Select(), x, y);
            _shapes.Add(square);

            var triangle = new Triangle(orderColorSelector.Select(), x, y - 30);
            _shapes.Add(triangle);
        }

        public void Draw(Graphics g)
        {
            foreach (var shape in _shapes)
            {
                shape.Draw(g);
            }

        }
    }
}
