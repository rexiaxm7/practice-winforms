using System.Drawing;

namespace DrawingShapes.ColorSelector
{
    class BackGroundColorSelector : IColorSelector
    {
        private readonly Brush _brushColor = Brushes.MistyRose;
        public Brush Select()
        {
            return _brushColor;
        }
    }
}
