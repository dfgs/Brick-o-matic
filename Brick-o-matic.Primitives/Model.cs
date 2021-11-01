using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public struct Model
	{
		private Box boundingBox;
		public Box BoundingBox
		{
			get => boundingBox;
		}

		private BoxGeometry[] items;
		public BoxGeometry[] Items
		{
			get => items;
		}

		public Model(params BoxGeometry[] Item)
		{
			int X1 = int.MaxValue, Y1 = int.MaxValue, Z1 = int.MaxValue, X2 = int.MinValue, Y2 = int.MinValue, Z2 = int.MinValue;

			if (Item == null) throw new ArgumentNullException(nameof(Item));
			this.items = Item;


			if (Item.Length == 0) boundingBox = new Box();
			else
			{
				foreach (BoxGeometry item in Item)
				{
					X1 = System.Math.Min(X1, item.Position.X);
					Y1 = System.Math.Min(Y1, item.Position.Y);
					Z1 = System.Math.Min(Z1, item.Position.Z);
					X2 = System.Math.Max(X2, item.Position.X + item.Size.X - 1);
					Y2 = System.Math.Max(Y2, item.Position.Y + item.Size.X - 1);
					Z2 = System.Math.Max(Z2, item.Position.Z + item.Size.X - 1);
				}
				boundingBox = new Box(new Position(X1, Y1, Z1),new Size( X2 - X1 + 1, Y2 - Y1 + 1, Z2 - Z1 + 1));
			}
		}

	}
}
