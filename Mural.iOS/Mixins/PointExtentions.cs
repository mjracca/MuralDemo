using System;
using System.Drawing;
using CoreGraphics;

namespace Mural.iOS
{
    public static class PointExtentions
    {
        public static PointF AsPointF(this CGPoint point)
        {
            return new PointF()
            {
                X = (float)point.X,
                Y = (float)point.Y
            };
        }

        public static CGPoint AsCGPoint(this PointF point)
        {
            return new CGPoint()
            {
                X = point.X,
                Y = point.Y
            };
        }
    }
}