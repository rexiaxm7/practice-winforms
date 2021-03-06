using System.Collections.Generic;
using DrawingShapes.ColorSelector;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public class RoundFactory : ISelectableShapeFactory
    {
        private readonly string _name;
        private readonly IColorSelector _colorSelector;

        public RoundFactory(IColorSelector colorSelector, string name)
        {
            this._colorSelector = colorSelector;
            this._name = name;
        }

        public string Name()
        {
            return _name;
        }

        public IShape Create(int x, int y)
        {
            var brush = _colorSelector.Select();

            return new Round(brush, x, y);
        }
    }
}