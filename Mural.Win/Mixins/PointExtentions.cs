using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mural.Win
{
    public static class PointExtensions
    {
        public static PointF AsPointF(this Windows.Foundation.Point point)
        {
            return new PointF()
            {
                X = (float)point.X,
                Y = (float)point.Y
            };
        }

        public static PointF Add(this PointF self, PointF value)
        {
            return new PointF()
            {
                X = self.X + value.X,
                Y = self.Y + value.Y
            };
        }
    }
}
