using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
    public struct Box:IBox
    {
        public int X1
		{
            get;
		}
        public int Y1
        {
            get;
        }
        public int Z1
        {
            get;
        }

        public Vector Size
		{
            get;
            private set;
        }
              
        public int X2
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
        }

        
        public Box(int X1, int Y1, int Z1)
        {
            this.X1 = X1; this.Y1 = Y1; this.Z1 = Z1;this.Size = new Vector(1, 1, 1);
        }
        public Box(int X1, int Y1, int Z1, int Size)
        {
            if (Size <= 0) throw new ArgumentException("Size must be greater than 0");

            this.X1 = X1; this.Y1 = Y1; this.Z1 = Z1;
            this.Size = new Vector(Size, Size, Size);
        }

        public Box(int X1, int Y1, int Z1, int SizeX, int SizeY, int SizeZ)
        {
            if (SizeX <= 0) throw new ArgumentException("Size X must be greater than 0");
            if (SizeY <= 0) throw new ArgumentException("Size Y must be greater than 0");
            if (SizeZ <= 0) throw new ArgumentException("Size Z must be greater than 0");

            this.X1 = X1; this.Y1 = Y1; this.Z1 = Z1;
            this.Size = new Vector(SizeX, SizeY, SizeZ);
        }

        public Box(int X1, int Y1, int Z1, Vector Size)
        {
            if (Size.X <= 0) throw new ArgumentException("Size X must be greater than 0");
            if (Size.Y <= 0) throw new ArgumentException("Size Y must be greater than 0");
            if (Size.Z <= 0) throw new ArgumentException("Size Z must be greater than 0");

            this.X1 = X1; this.Y1 = Y1; this.Z1 = Z1;
            this.Size = Size;
        }

        public bool IntersectWith(Box OtherBox)
		{
            //if (OtherBox == null) throw new ArgumentNullException(nameof(OtherBox));

            if ((OtherBox.X1 > X2) || (OtherBox.X2 < X1)) return false;
            if ((OtherBox.Y1 > Y2) || (OtherBox.Y2 < Y1)) return false;
            if ((OtherBox.Z1 > Z2) || (OtherBox.Z2 < Z1)) return false;
            
            return true;
        }

        public bool IsInside(Vector Coordinate)
        {

            if ((Coordinate.X > X2) || (Coordinate.X < X1)) return false;
            if ((Coordinate.Y > Y2) || (Coordinate.Y < Y1)) return false;
            if ((Coordinate.Z > Z2) || (Coordinate.Z < Z1)) return false;

            return true;
        }

        public IEnumerable<Box> SplitWith(Box OtherBox)
        {
            List<int> xs, ys,zs;
            Box newBox;
            int x1, y1, z1;
            int x2, y2, z2;

            //if (OtherBox == null) throw new ArgumentNullException(nameof(OtherBox));

            if (!IntersectWith(OtherBox))
            {
                yield return this;
                yield return OtherBox;
                yield break;
            }

            xs = new List<int>();
            ys = new List<int>();
            zs = new List<int>();


            xs.Add(this.X1); xs.Add(this.X2 + 1);
            xs.Add(OtherBox.X1); xs.Add(OtherBox.X2 + 1);

            ys.Add(this.Y1); ys.Add(this.Y2 + 1);
            ys.Add(OtherBox.Y1); ys.Add(OtherBox.Y2 + 1);

            zs.Add(this.Z1); zs.Add(this.Z2 + 1);
            zs.Add(OtherBox.Z1); zs.Add(OtherBox.Z2 + 1);

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

                        newBox = new Box(x1, y1, z1, x2 - x1 , y2 - y1 , z2 - z1 );
                        if (this.IntersectWith(newBox) || OtherBox.IntersectWith(newBox)) yield return newBox;
                    }
                }
            }

        }









		public override string ToString()
		{
			return $"{X1},{Y1},{Z1}/{Size.X},{Size.Y},{Size.Z}";
		}


	}
}
