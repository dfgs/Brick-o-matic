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
             

        public Box(Position Position)
        {
            this.Position=Position;this.Size = new Size(1, 1, 1);
        }
         
        public Box(Position Position, Size Size)
        {
            this.Position = Position;
            this.Size = Size;
        }

		public Box RotateX(int Count)
		{
			Position cornerA, cornerB, rCornerA, rCornerB;
			Size newSize;

			cornerA = this.Position;
			cornerB = this.Position + this.Size + new Position(-1, -1, -1);

			rCornerA = cornerA.RotateX(Count);
			rCornerB = cornerB.RotateX(Count);

			newSize = Size.RotateX(Count);

			return new Box(new Position(rCornerA.X, System.Math.Min(rCornerA.Y, rCornerB.Y), System.Math.Min(rCornerA.Z, rCornerB.Z)), newSize);
		}
		public Box RotateY(int Count)
		{
			Position cornerA, cornerB, rCornerA, rCornerB;
			Size newSize;

			cornerA = this.Position;
			cornerB = this.Position + this.Size + new Position(-1, -1, -1);

			rCornerA = cornerA.RotateY(Count);
			rCornerB = cornerB.RotateY(Count);

			newSize = Size.RotateY(Count);

			return new Box(new Position(System.Math.Min(rCornerA.X, rCornerB.X), rCornerA.Y, System.Math.Min(rCornerA.Z, rCornerB.Z)), newSize);
		}
		public Box RotateZ(int Count)
		{
			Position cornerA, cornerB, rCornerA, rCornerB;
			Size newSize;

			cornerA = this.Position;
			cornerB = this.Position + this.Size + new Position(-1, -1, -1);

			rCornerA = cornerA.RotateZ(Count);
			rCornerB = cornerB.RotateZ(Count);

			newSize = Size.RotateZ(Count);

			return new Box(new Position(System.Math.Min(rCornerA.X, rCornerB.X), System.Math.Min(rCornerA.Y, rCornerB.Y), rCornerA.Z), newSize);
		}

		public override string ToString()
		{
			return $"{Position}/{Size}";
		}


	}
}
