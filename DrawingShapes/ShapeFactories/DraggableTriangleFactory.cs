using System.Drawing;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public class DraggableTriangleFactory : ISelectableShapeFactory
    {
        private readonly string _name;

        public DraggableTriangleFactory(string name)
        {
            this._name = name;
        }
        public IShape Create(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public IShape Create(int x, int y, int width, int height, Color color)
        {
            return new DraggableTriangle(x, y, width, height, color);
        }

        public string Name()
        {
            return _name;
        }
    }
}