using System.Drawing;
using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public class DraggableRoundFactory : ISelectableShapeFactory
    {
        private readonly string _name;

        public DraggableRoundFactory(string name)
        {
            this._name = name;
        }
        public IShape Create(int x, int y)
        {
            return new DraggableRound(x, y, 50, 50, Color.Red);
        }

        public IShape Create(int x, int y, int width, int height, Color color)
        {
            return new DraggableRound(x, y, width, height, color);
        }

        public string Name()
        {
            return _name;
        }
    }
}