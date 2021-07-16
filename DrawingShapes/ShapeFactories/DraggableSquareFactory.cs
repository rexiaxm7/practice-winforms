using System.Drawing;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public class DraggableSquareFactory : ISelectableShapeFactory
    {
        private readonly string _name;

        public DraggableSquareFactory(string name)
        {
            this._name = name;
        }

        public IShape Create(int x, int y)
        {
            return new DraggableSquare(x, y, 50, 50, Color.Red);
        }

        public IShape Create(int x, int y, int width, int height, Color color)
        {
            return new DraggableSquare(x, y, width, height, color);
        }

        public string Name()
        {
            return _name;
        }
    }
}