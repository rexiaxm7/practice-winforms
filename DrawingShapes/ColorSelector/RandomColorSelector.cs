using System;
using System.Drawing;

namespace DrawingShapes.ColorSelector
{
    class RandomColorSelector : IColorSelector
    {
        private readonly Brush[] _brushes = new Brush[]
        {
            Brushes.Green,
            Brushes.Red, 
            Brushes.Purple,
            Brushes.Blue, 
            Brushes.Yellow,
            Brushes.Orange
        };

        public Brush Select()
        {
            Random r = new Random();
            return _brushes[r.Next(0,_brushes.Length)];
        }
    }
}
