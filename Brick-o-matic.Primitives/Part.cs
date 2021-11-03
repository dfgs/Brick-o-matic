using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Part : Primitive,IPart
	{

		IEnumerable<IPrimitive> IPart.Items => items;

		public int Count
		{
			get { return items.Count; }
		}

		private List<IPrimitive> items;
		public IEnumerable<IPrimitive> Items
		{
			get => items;
		}


		public Part()
		{
			items = new List<IPrimitive>();
		}
		public Part(Position Position):base(Position)
		{
			items = new List<IPrimitive>();
		}
		public void Add(IPrimitive Child)
		{
			if (Child == null) throw new ArgumentNullException(nameof(Child));
			items.Add(Child);
		}

		public override Box GetBoundingBox()
		{
			int minX=int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
			int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
			Box childBox;

			if (Count == 0) return new Box(Position, new Size());
			
			foreach (IPrimitive item in this.items)
			{
				childBox = item.GetBoundingBox();
				minX = System.Math.Min(minX, childBox.Position.X);
				minY = System.Math.Min(minY, childBox.Position.Y);
				minZ = System.Math.Min(minZ, childBox.Position.Z);

				maxX = System.Math.Max(maxX, childBox.Position.X + childBox.Size.X);
				maxY = System.Math.Max(maxY, childBox.Position.Y + childBox.Size.Y);
				maxZ = System.Math.Max(maxZ, childBox.Position.Z + childBox.Size.Z);
			}

			return new Box(Position + new Position(minX, minY, minZ), new Size(maxX-minX,maxY-minY,maxZ-minZ));

		}

		public override IEnumerable<Brick> Build(IScene Scene)
		{
			
			foreach(IPrimitive item in this.items)
			{
				foreach(Brick brick in item.Build(Scene))
				{
					yield return new Brick(this.Position + brick.Position, brick.Size);
				}
			}

		}
		

	}
}
