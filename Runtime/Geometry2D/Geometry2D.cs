using UnityEngine;

namespace Dma.Geometry2D
{
    public static class Geometry2D
    {
        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are colinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        public static int GetPointsOrientation(Vector2 p, Vector2 q, Vector2 r)
        {
            var val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);

            if (Mathf.Approximately(val, 0f))
            {
                return 0; // colinear
            }

            return (val > 0f) ? 1 : 2; // clock or counterclock wise 
        }
    }
}