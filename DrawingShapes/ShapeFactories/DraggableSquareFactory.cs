using DrawingShapes.Shapes;

namespace DrawingShapes.ShapeFactories
{
    public class DraggableSquareFactory : ISelectableShapeFactory
    {
        private string name;

        public DraggableSquareFactory(string name)
        {
            this.name = name;
        }

        public IShape Create(int x, int y)
        {
            return new DraggableSquare(x, y);
        }

        public string Name()
        {
            return name;
        }
    }
}