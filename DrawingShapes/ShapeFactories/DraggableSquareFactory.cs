﻿using DrawingShapes.Shapes;

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
            return new DraggableSquare(x, y, 50, 50);
        }

        public IShape Create(int x, int y, int width, int height)
        {
            return new DraggableSquare(x, y, width, height);
        }

        public string Name()
        {
            return _name;
        }
    }
}