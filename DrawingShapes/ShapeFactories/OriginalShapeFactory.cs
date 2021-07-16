using System.Collections.Generic;
using System.Drawing;
using DrawingShapes.ColorSelector;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    class OriginalShapeFactory : ISelectableShapeFactory
    {
        private readonly string _name;

        public OriginalShapeFactory(string name)
        {
            _name = name;
        }

        public string Name()
        {
            return _name;
        }
        public IShape Create(int x, int y)
        {
            return new OriginalShape(x, y);
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
