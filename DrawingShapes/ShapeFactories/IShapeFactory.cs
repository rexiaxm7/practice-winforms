using System.Collections.Generic;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public interface IShapeFactory
    {
        IShape Create(int x, int y);
    }
}