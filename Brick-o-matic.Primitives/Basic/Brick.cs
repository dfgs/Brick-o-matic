using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Brick : Primitive, IBox
	{
		
		
		public Size Size
		{
			get;
			set;
		}
		public IColor Color
		{
			get;
			set;
		}

		public Brick()
		{
			this.Size = new Size(1, 1, 1);this.Color = new Color(255, 0, 0);
		}
		public Brick(Position Position):base(Position)
		{
			this.Size = new Size(1, 1, 1); this.Color = new Color(255, 0, 0);
		}
		public Brick(Position Position, Size Size) : base(Position)
		{
			this.Size = Size; this.Color = new Color(255, 0, 0);
		}
		public Brick(Position Position, Size Size,IColor Color) : base(Position)
		{
			this.Size = Size; this.Color = Color;
		}



		public Brick RotateX(int Count)
		{
			Position cornerA, cornerB, rCornerA, rCornerB;
			Size newSize;

			cornerA = this.Position;
			cornerB = this.Position + this.Size + new Position(-1, -1, -1);

			rCornerA = cornerA.RotateX(Count);
			rCornerB = cornerB.RotateX(Count);

			newSize = Size.RotateX(Count);

			return new Brick(new Position(rCornerA.X, System.Math.Min(rCornerA.Y, rCornerB.Y), System.Math.Min(rCornerA.Z, rCornerB.Z)), newSize, Color);
		}
		public Brick RotateY(int Count)
		{
			Position cornerA, cornerB, rCornerA, rCornerB;
			Size newSize;

			cornerA = this.Position;
			cornerB = this.Position + this.Size + new Position(-1, -1, -1);

			rCornerA = cornerA.RotateY(Count);
			rCornerB = cornerB.RotateY(Count);

			newSize = Size.RotateY(Count);

			return new Brick(new Position(System.Math.Min(rCornerA.X, rCornerB.X), rCornerA.Y, System.Math.Min(rCornerA.Z, rCornerB.Z)), newSize, Color);
		}
		public Brick RotateZ(int Count)
		{
			Position cornerA,cornerB,rCornerA,rCornerB;
			Size newSize;

			cornerA = this.Position;
			cornerB = this.Position + this.Size+new Position(-1,-1,-1);

			rCornerA = cornerA.RotateZ(Count);
			rCornerB = cornerB.RotateZ(Count);

			newSize = Size.RotateZ(Count);

			return new Brick(new Position(System.Math.Min(rCornerA.X,rCornerB.X), System.Math.Min(rCornerA.Y,rCornerB.Y), rCornerA.Z), newSize, Color);
		}

		public override Box GetBoundingBox(IResourceProvider ResourceProvider)
		{
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			return new Box(Position, Size);
		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			Color color;
			byte R, G, B;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			
			// We must replace colorref by color, in order to prevent resource not found in import
			this.Color.GetComponents(ResourceProvider, out R, out G, out B);
			color = new Color(R, G, B);
			yield return new Brick(Position, Size, color); ;
		}

		public override ICSGNode BuildCSGNode(IResourceProvider ResourceProvider)
		{
			CSGNode node;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			node = new CSGNode(); node.Name = "Brick";
			node.BoundingBox = new Box(Position, Size);

			return node;
		}


	}
}
