using UnityEngine;

namespace Dma.Geometry2D
{
    public class Circle : IShape
    {
        public Point Centre { get; private set; }
        public float Radius { get; private set; }

        public Circle(Point centre, float radius) => Intialize(centre, radius);

        private void Intialize(Point centre, float radius)
        {
            Centre = centre;
            Radius = radius;
        }

        public float Area => Mathf.PI * Radius * Radius;

        public float Perimeter => Mathf.PI * Radius * 2f;

    }
}


