using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
    public struct Box:IBox
    {
        public Position Position
		{
            get;
            private set;
		}
            
        

        public Size Size
		{
            get;
            private set;
        }
              
        /*public int X2
		{
            get => X1 + Size.X - 1;
		}
        public int Y2
        {
            get => Y1 + Size.Y - 1;
        }
        public int Z2
        {
            get => Z1 + Size.Z - 1;
        }*/
       

        public Box(Position Position)
        {
            this.Position=Position;this.Size = new Size(1, 1, 1);
        }
         
        public Box(Position Position, Size Size)
        {
            this.Position = Position;
            this.Size = Size;
        }

        

        public bool IntersectWith(Box OtherBox)
		{
            //if (OtherBox == null) throw new ArgumentNullException(nameof(OtherBox));
            if ((Size.IsFlat) || (OtherBox.Size.IsFlat)) return false;
            
            if ((OtherBox.Position.X >= Position.X + Size.X) || (OtherBox.Position.X + OtherBox.Size.X <= Position.X)) return false;
            if ((OtherBox.Position.Y >= Position.Y + Size.Y) || (OtherBox.Position.Y + OtherBox.Size.Y <= Position.Y)) return false;
            if ((OtherBox.Position.Z >= Position.Z + Size.Z) || (OtherBox.Position.Z + OtherBox.Size.Z <= Position.Z)) return false;
            
            return true;
        }

        public bool IsInside(Position Coordinate)
        {

            if ((Coordinate.X >= Position.X + Size.X) || (Coordinate.X < Position.X)) return false;
            if ((Coordinate.Y >= Position.Y + Size.Y) || (Coordinate.Y < Position.Y)) return false;
            if ((Coordinate.Z >= Position.Z + Size.Z) || (Coordinate.Z < Position.Z)) return false;

            return true;
        }

        public IEnumerable<Box> SplitWith(Box OtherBox)
        {
            List<int> xs, ys,zs;
            Box newBox;
            int x1, y1, z1;
            int x2, y2, z2;

           			
            if (!IntersectWith(OtherBox))
            {
                if (!Size.IsFlat) yield return this;
                if (!OtherBox.Size.IsFlat) yield return OtherBox;
                yield break;
            }

            xs = new List<int>();
            ys = new List<int>();
            zs = new List<int>();


            xs.Add(this.Position.X); xs.Add(this.Position.X+Size.X );
            xs.Add(OtherBox.Position.X); xs.Add(OtherBox.Position.X+ OtherBox.Size.X );

            ys.Add(this.Position.Y); ys.Add(this.Position.Y+Size.Y );
            ys.Add(OtherBox.Position.Y); ys.Add(OtherBox.Position.Y+ OtherBox.Size.Y );

            zs.Add(this.Position.Z); zs.Add(this.Position.Z+Size.Z );
            zs.Add(OtherBox.Position.Z); zs.Add(OtherBox.Position.Z+ OtherBox.Size.Z );

            xs.Sort();ys.Sort();zs.Sort();

            for (int tx = 0; tx < 3; tx++)
            {
                for (int ty = 0; ty < 3; ty++)
                {
                    for (int tz = 0; tz < 3; tz++)
                    {
                        x1 = xs[tx]; y1 = ys[ty]; z1 = zs[tz];
                        x2 = xs[tx+1]; y2 = ys[ty+1]; z2 = zs[tz+1];

                        if ((x1 == x2) || (y1 == y2) || (z1 == z2)) continue;

                        newBox = new Box(new Position(x1,y1,z1), new Size(x2 - x1 , y2 - y1 , z2 - z1) );
                        if (this.IntersectWith(newBox) || OtherBox.IntersectWith(newBox)) yield return newBox;
                    }
                }
            }

        }









		public override string ToString()
		{
			return $"{Position}/{Size}";
		}


	}
}
