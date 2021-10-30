using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Brick_o_matic.Viewer
{
    public static class MathExtensions
    {
        public static Point3D ToPoint3D(this Vector Vector)
        {
            return new Point3D(Vector.X, Vector.Y, Vector.Z);
        }
        public static Point3D Translate(this Point3D Point,double X,double Y,double Z)
        {
            return new Point3D(Point.X+X, Point.Y+Y, Point.Z+Z);
        }
    }
}
