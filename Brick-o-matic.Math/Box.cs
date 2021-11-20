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

		public Box(IEnumerable<IBox> Boxes)
		{
			IBox[] boxes;

			int minX = int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
			int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;

			boxes = Boxes.ToArray();
			if (boxes.Length==0)
			{
				Position = new Position();Size = new Size();
				return;
			}

			foreach (IBox childBox in boxes)
			{
				minX = System.Math.Min(minX, childBox.Position.X);
				minY = System.Math.Min(minY, childBox.Position.Y);
				minZ = System.Math.Min(minZ, childBox.Position.Z);

				maxX = System.Math.Max(maxX, childBox.Position.X + childBox.Size.X);
				maxY = System.Math.Max(maxY, childBox.Position.Y + childBox.Size.Y);
				maxZ = System.Math.Max(maxZ, childBox.Position.Z + childBox.Size.Z);
			}

			Position = new Position(minX, minY, minZ);
			Size= new Size(maxX - minX, maxY - minY, maxZ - minZ);
		}

		public IBox RotateX(int Count)
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
		public IBox RotateY(int Count)
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
		public IBox RotateZ(int Count)
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
