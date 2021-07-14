using System.Collections.Generic;
using System.Drawing;

namespace DrawingShapes.Shapes
{
    public class CheckerboardCheck : IShape
    {
        private readonly List<IShape> _shapes;

        public CheckerboardCheck(int x, int y, int width, int height, int tileSize)
        {
            _shapes = CreateCheckerboardCheck(x, y, width, height, tileSize);
        }

        private static List<IShape> CreateCheckerboardCheck(int x, int y, int width, int height, int tileSize)
        {
            var brushes = new[]
            {
                Brushes.Black,
                Brushes.White
            };

            var shapes = new List<IShape>();

            for (var i = 0; i <= height / tileSize; i++)
            {
                for (var j = 0; j <= width / tileSize; j++)
                {
                    var brush = brushes[(j + i) % 2];
                    var positionX = j * tileSize + x;
                    var positionY = i * tileSize + y;
                    shapes.Add(new BackGroundTile(brush, positionX, positionY, tileSize, tileSize));
                }
            }

            return shapes;
        }

        public void Draw(Graphics g)
        {
            foreach (var shape in _shapes)
            {
                shape.Draw(g);
            }
        }
    }
}