namespace DrawingShapes.Shapes
{
    public interface IDraggableShape : IDeletableShape
    {
        bool Selected { get; set; }

        bool Contains(int x, int y);

        void ChangePosition(int x, int y);
    }
}