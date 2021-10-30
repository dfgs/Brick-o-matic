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
		public List<IPrimitive> Items
		{
			get => items;
		}


		public Part()
		{
			items = new List<IPrimitive>();
		}
		public Part(Vector Position):base(Position)
		{
			items = new List<IPrimitive>();
		}


		public override Box GetBoudingBox()
		{
			int X1=int.MaxValue, Y1 = int.MaxValue, Z1 = int.MaxValue, X2 = int.MinValue, Y2= int.MinValue, Z2= int.MinValue;
			Box childBox;

			if (Count == 0) return new Box(Position.X, Position.Y, Position.Z, 1);

			foreach (IPrimitive item in items)
			{
				childBox = item.GetBoudingBox();
				if (childBox.X1 < X1) X1 = childBox.X1;
				if (childBox.Y1 < Y1) Y1 = childBox.Y1;
				if (childBox.Z1 < Z1) Z1 = childBox.Z1;
				if (childBox.X2 > X2) X2 = childBox.X2;
				if (childBox.Y2 > Y2) Y2 = childBox.Y2;
				if (childBox.Z2 > Z2) Z2 = childBox.Z2;
			}

			return new Box(X1+Position.X, Y1 + Position.Y, Z1 + Position.Z, X2 - X1 + 1, Y2 - Y1 + 1, Z2 - Z1 + 1);

		}
		public override IEnumerable<Brick> GetBricks()
		{
			return items.SelectMany(item=>item.GetBricks());
		}

	}
}
