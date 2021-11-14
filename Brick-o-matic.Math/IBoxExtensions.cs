using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
    public static class IBoxExtensions
    {
        
        public static bool IntersectWith(this IBox Box,IBox OtherBox)
        {
            if ((Box.Size.IsFlat) || (OtherBox.Size.IsFlat)) return false;

            if ((OtherBox.Position.X >= Box.Position.X + Box.Size.X) || (OtherBox.Position.X + OtherBox.Size.X <= Box.Position.X)) return false;
            if ((OtherBox.Position.Y >= Box.Position.Y + Box.Size.Y) || (OtherBox.Position.Y + OtherBox.Size.Y <= Box.Position.Y)) return false;
            if ((OtherBox.Position.Z >= Box.Position.Z + Box.Size.Z) || (OtherBox.Position.Z + OtherBox.Size.Z <= Box.Position.Z)) return false;

            return true;
        }

        public static bool Contains(this IBox Box, Position Coordinate)
        {

            if ((Coordinate.X >= Box.Position.X + Box.Size.X) || (Coordinate.X < Box.Position.X)) return false;
            if ((Coordinate.Y >= Box.Position.Y + Box.Size.Y) || (Coordinate.Y < Box.Position.Y)) return false;
            if ((Coordinate.Z >= Box.Position.Z + Box.Size.Z) || (Coordinate.Z < Box.Position.Z)) return false;

            return true;
        }


        public static IEnumerable<Box> SplitWith(this IBox Box, IBox OtherBox)
        {
            List<int> xs, ys, zs;
            Box newBox;
            int x1, y1, z1;
            int x2, y2, z2;


            if (!Box.IntersectWith(OtherBox))
            {
                if (!Box.Size.IsFlat) yield return new Math.Box(Box.Position, Box.Size);
                //if (!OtherBox.Size.IsFlat) yield return new Math.Box(OtherBox.Position, OtherBox.Size);
                yield break;
            }

            xs = new List<int>();
            ys = new List<int>();
            zs = new List<int>();


            xs.Add(Box.Position.X); xs.Add(Box.Position.X + Box.Size.X);
            xs.Add(OtherBox.Position.X); xs.Add(OtherBox.Position.X + OtherBox.Size.X);

            ys.Add(Box.Position.Y); ys.Add(Box.Position.Y + Box.Size.Y);
            ys.Add(OtherBox.Position.Y); ys.Add(OtherBox.Position.Y + OtherBox.Size.Y);

            zs.Add(Box.Position.Z); zs.Add(Box.Position.Z + Box.Size.Z);
            zs.Add(OtherBox.Position.Z); zs.Add(OtherBox.Position.Z + OtherBox.Size.Z);

            xs.Sort(); ys.Sort(); zs.Sort();

            for (int tx = 0; tx < 3; tx++)
            {
                for (int ty = 0; ty < 3; ty++)
                {
                    for (int tz = 0; tz < 3; tz++)
                    {
                        x1 = xs[tx]; y1 = ys[ty]; z1 = zs[tz];
                        x2 = xs[tx + 1]; y2 = ys[ty + 1]; z2 = zs[tz + 1];

                        if ((x1 == x2) || (y1 == y2) || (z1 == z2)) continue;

                        newBox = new Box(new Position(x1, y1, z1), new Size(x2 - x1, y2 - y1, z2 - z1));
                        //if (Box.IntersectWith(newBox) || OtherBox.IntersectWith(newBox)) yield return newBox;
                        if (Box.IntersectWith(newBox) ) yield return newBox;
                    }
                }
            }

        }


    }
}
