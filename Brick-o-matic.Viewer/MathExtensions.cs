using Brick_o_matic.Math;
using Brick_o_matic.Primitives;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Viewer
{
    public static class MathExtensions
    {
        public static Vector3 ToVector3(this Position Vector)
        {
            return new Vector3(Vector.X, Vector.Y, Vector.Z);
        }
        /*public static Vector3 Translate(this Vector3 Point,double X,double Y,double Z)
        {
            return new Vector3(Point.X+X, Point.Y+Y, Point.Z+Z);
        }*/

        public static Microsoft.Xna.Framework.Color ToXNAColor(this IColor Color,IResourceProvider ResourceProvider)
		{
            byte R, G, B;

            Color.GetComponents(ResourceProvider, out R, out G, out B) ;
            return new Microsoft.Xna.Framework.Color(R / 255f, G / 255F, B / 255f);
		}

    }
}
