using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Brick_o_matic.Editor
{
    public static class MathExtensions
    {
        public static Point3D ToPoint3D(this Position Vector)
        {
            return new Point3D(Vector.X, Vector.Y, Vector.Z);
        }
        public static Point3D Translate(this Point3D Point,double X,double Y,double Z)
        {
            return new Point3D(Point.X+X, Point.Y+Y, Point.Z+Z);
        }

        public static System.Windows.Media.Color ToMediaColor(this IColor Color,IResourceProvider ResourceProvider)
		{
            byte R, G, B;

            Color.GetComponents(ResourceProvider, out R, out G, out B) ;
            return System.Windows.Media.Color.FromArgb(255,R, G, B);
		}

    }
}
