using System.Collections.Generic;
using DrawingShapes.ColorSelector;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public class TriangleFactory : ISelectableShapeFactory
    {
        private readonly string _name;
        private readonly IColorSelector _colorSelector;

        public TriangleFactory(IColorSelector colorSelector, string name)
        {
            this._colorSelector = colorSelector;
            _name = name;
        }

        public string Name()
        {
            return _name;
        }

        public IShape Create(int x, int y)
        {
            var brush = _colorSelector.Select();

            return new Triangle(brush, x, y);
        }

        public IShape Create(int x, int y, int width, int height)
        {
            throw new System.NotImplementedException();
        }
    }
}