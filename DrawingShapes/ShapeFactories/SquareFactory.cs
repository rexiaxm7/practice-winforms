using System.Collections.Generic;
using System.Drawing;
using DrawingShapes.ColorSelector;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public class SquareFactory : ISelectableShapeFactory
    {
        private readonly string _name;
        private readonly IColorSelector _colorSelector;

        public SquareFactory(IColorSelector colorSelector, string name)
        {
            _colorSelector = colorSelector;
            _name = name;
        }

        public string Name()
        {
            return _name;
        }

        public IShape Create(int x, int y)
        {
            var brush = _colorSelector.Select();

            return new Square(brush, x, y);
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