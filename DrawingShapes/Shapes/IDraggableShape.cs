using System.Drawing;

namespace DrawingShapes.Shapes
{
    public interface IDraggableShape : IDeletableShape
    {
        Point Point { get; set; }

        Size Size { get; set; }

        bool Preview { get; set; }

        bool Selected { get; set; }

        bool Contains(int x, int y);

        void Resize(Point start, Point end);

    }
}