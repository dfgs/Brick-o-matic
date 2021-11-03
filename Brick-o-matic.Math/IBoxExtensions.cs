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


    }
}
