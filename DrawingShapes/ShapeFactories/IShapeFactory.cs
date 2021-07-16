using System.Drawing;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public interface IShapeFactory
    {
        IShape Create(int x, int y);

        IShape Create(int x, int y, int width, int height, Color color);
    }
}