using System;
using UnityEngine;

namespace Dma.Geometry2D
{
    public class LineSegment
    {
        public Point PointA { get; private set; }
        public Point PointB { get; private set; }

        public LineSegment(Point pointA, Point pointB) => Initialize(pointA, pointB);

        public LineSegment(Vector2 vectorA, Vector2 vectorB) => Initialize(new Point(vectorA), new Point(vectorB));

        private void Initialize(Point poingA, Point pointB)
        {
            if (poingA.Position == pointB.Position)
            {
                throw new ArgumentException("Must be 2 distinct points");
            }

            PointA = poingA;
            PointB = pointB;
        }

        public float Length => Vector2.Distance(PointA.Position, PointB.Position);
    }
}


