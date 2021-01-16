using System;
using UnityEngine;

namespace Dma.Geometry2D
{
    public class Triangle : IConvexPolygon
    {
        public Point PointA { get; private set; }
        public Point PointB { get; private set; }
        public Point PointC { get; private set; }

        public LineSegment EdgeAB { get; private set; }
        public LineSegment EdgeBC { get; private set; }
        public LineSegment EdgeCA { get; private set; }

        #region 通过顶点创建

        public Triangle(Point pointA, Point pointB, Point pointC) => Initialize(pointA, pointB, pointC);

        public Triangle(Vector2 vectorA, Vector2 vectorB, Vector2 vectorC) => Initialize(new Point(vectorA), new Point(vectorB), new Point(vectorC));

        private void Initialize(Point pointA, Point pointB, Point pointC)
        {
            var orientation = Geometry2D.GetPointsOrientation(pointA.Position, pointB.Position, pointC.Position);

            if (orientation == 0)
            {
                throw new ArgumentException($"three vertices for triangle are colinear");
            }
            else if (orientation == 1)
            {
                //Clockwise
                PointA = pointA;
                PointB = pointC;
                PointC = pointB;
            }
            else
            {
                //Counterclockwise
                PointA = pointA;
                PointB = pointB;
                PointC = pointC;
            }

            EdgeAB = new LineSegment(PointA, PointB);
            EdgeBC = new LineSegment(PointB, PointC);
            EdgeCA = new LineSegment(PointC, PointA);
        }

        #endregion

        #region 通过边创建

        //public Triangle(LineSegment edgeAB, LineSegment edgeBC, LineSegment edgeCA) => Initialize(edgeAB, edgeBC, edgeCA);

        //private void Initialize(LineSegment edgeAB, LineSegment edgeBC, LineSegment edgeCA)
        //{
        //    //These edges should be already counterclockwise
        //    PointA = edgeAB.Origin;
        //    PointB = edgeBC.Origin;
        //    PointC = edgeCA.Origin;

        //    EdgeAB = edgeAB;
        //    EdgeBC = edgeBC;
        //    EdgeCA = edgeCA;

        //    EdgeAB.Next = EdgeBC;
        //    EdgeAB.Previous = EdgeCA;

        //    EdgeBC.Next = EdgeCA;
        //    EdgeBC.Previous = EdgeAB;

        //    EdgeCA.Next = EdgeAB;
        //    EdgeCA.Previous = EdgeBC;

        //    EdgeAB.Face = this;
        //    EdgeBC.Face = this;
        //    EdgeCA.Face = this;

        //    Edge = EdgeAB;
        //}

        #endregion

        public int NumberOfSides => 3;

        public float Perimeter => EdgeAB.Length + EdgeBC.Length + EdgeCA.Length;

        public float HalfPerimeter => Perimeter / 2f;

        public float Area => Mathf.Sqrt(HalfPerimeter * (HalfPerimeter - EdgeAB.Length) * (HalfPerimeter - EdgeBC.Length) * (HalfPerimeter - EdgeCA.Length));


        #region 外接圆 和 内切圆

        private Circle m_InscribedCircle;
        public Circle InscribedCircle
        {
            get
            {
                if (m_InscribedCircle == null)
                {
                    m_InscribedCircle = GetInscribedCircle();
                }
                return m_InscribedCircle;
            }
        }

        private Circle m_CircumscribedCircle;
        public Circle CircumscribedCircle
        {
            get
            {
                if (m_CircumscribedCircle == null)
                {
                    m_CircumscribedCircle = GetCircumscribedCircle();
                }
                return m_CircumscribedCircle;
            }
        }

        //内切圆
        private Circle GetInscribedCircle()
        {
            var x = (PointA.Position.x * EdgeBC.Length + PointB.Position.x * EdgeCA.Length + PointC.Position.x * EdgeAB.Length) / Perimeter;
            var y = (PointA.Position.y * EdgeBC.Length + PointB.Position.y * EdgeCA.Length + PointC.Position.y * EdgeAB.Length) / Perimeter;

            var centre = new Point(x, y);
            var radius = 2f * Area / Perimeter;

            return new Circle(centre, radius);
        }

        //外接圆
        private Circle GetCircumscribedCircle()
        {
            var radius = EdgeAB.Length * EdgeBC.Length * EdgeCA.Length / (4f * Area);

            var dA = PointA.Position.x * PointA.Position.x + PointA.Position.y * PointA.Position.y;
            var dB = PointB.Position.x * PointB.Position.x + PointB.Position.y * PointB.Position.y;
            var dC = PointC.Position.x * PointC.Position.x + PointC.Position.y * PointC.Position.y;

            var aux1 = PointA.Position.y * (dC - dB) + PointB.Position.y * (dA - dC) + PointC.Position.y * (dB - dA);
            var aux2 = PointA.Position.x * (dB - dC) + PointB.Position.x * (dC - dA) + PointC.Position.x * (dA - dB);
            var div = 2 * (PointA.Position.x * (PointB.Position.y - PointC.Position.y) + PointB.Position.x * (PointC.Position.y - PointA.Position.y) + PointC.Position.x * (PointA.Position.y - PointB.Position.y));

            if (div == 0)
            {
                throw new DivideByZeroException();
            }

            var centre = new Point(aux1 / div, aux2 / div);

            return new Circle(centre, radius);
        }

        #endregion
    }
}


