using System.Drawing;

namespace DrawingShapes.ColorSelector
{
    class OrderColorSelector : IColorSelector
    {
        private readonly Brush[] _brushes = new Brush[] { Brushes.Red, Brushes.Blue, Brushes.Yellow };
        private int _brushesIndex;



        public Brush Select()
        {
            Brush selectBrush = _brushes[_brushesIndex];

            _brushesIndex++;
            if (_brushesIndex >= _brushes.Length)
            {
                _brushesIndex = 0;
            }

            return selectBrush;
        }
    }
}
