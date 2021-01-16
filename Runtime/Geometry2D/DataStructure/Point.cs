using UnityEngine;

namespace Dma.Geometry2D
{
    public class Point
    {
        public Vector2 Position { get; private set; }

        public Point(float x, float y) => Initialize(x, y);

        public Point(Vector2 vector) => Initialize(vector.x, vector.y);

        public Point(Point point) => Initialize(point.Position.x, point.Position.y);

        private void Initialize(float x, float y)
        {
            Position = new Vector2(x, y);
        }
    }
}