using System.Drawing;

namespace DrawingShapes.Shapes
{
    class DraggableSquare : IDraggableShape
    {
        private int _positionX;
        private int _positionY;

        public bool Selected { get; set; }
        public bool Contains(int x, int y)
        {
            int px = _positionX - 30 / 2;
            int py = _positionY - 30 / 2;
            var rectangle = new Rectangle(px, py, 30, 30);
            return rectangle.Contains(x, y);
        }

        public void ChangePosition(int x, int y)
        {
            _positionX = x;
            _positionY = y;
        }

        public DraggableSquare(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;
        }

        public void Draw(Graphics g)
        {
            int x = _positionX - 30 / 2;
            int y = _positionY - 30 / 2;

            g.FillRectangle(Brushes.Blue, x, y, 30, 30);
            if (Selected)
            {
                g.DrawRectangle(Pens.Red, x, y, 30, 30);
            }
            
        }
    }
}
