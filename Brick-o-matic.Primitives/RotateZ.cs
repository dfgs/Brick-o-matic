using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class RotateZ : Primitive,IRotate
	{


		public int Count
		{
			get;
			set;
		}

		public IPrimitive Item
		{
			get;
			set;
		}


		public RotateZ()
		{
		}
		public RotateZ(Position Position):base(Position)
		{
		}
		
		public override Box GetBoundingBox(IScene Scene)
		{
			Box childBox;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			if (Item == null) return new Box(Position, new Size());

			childBox = Item.GetBoundingBox(Scene).RotateZ(Count);


			return new Box(Position + childBox.Position,childBox.Size);

		}

		public override IEnumerable<Brick> Build(IScene Scene)
		{
			Box childBox;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			if (Item == null) yield break;

			foreach(Brick brick in Item.Build(Scene))
			{
				childBox = brick.GetBoundingBox(Scene).RotateZ(Count);
				yield return new Brick(Position + childBox.Position, childBox.Size, brick.Color);
			}

		}
		

	}
}
