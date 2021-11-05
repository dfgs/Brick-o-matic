using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class FlipX : Primitive,ITransform
	{



		public IPrimitive Item
		{
			get;
			set;
		}


		public FlipX()
		{
		}
		public FlipX(Position Position):base(Position)
		{
		}
		
		public override Box GetBoundingBox(IScene Scene)
		{
			Box childBox;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			if (Item == null) return new Box(Position, new Size());

			childBox = Item.GetBoundingBox(Scene);


			return new Box(Position + new Position(-childBox.Position.X - childBox.Size.X, childBox.Position.Y, childBox.Position.Z), childBox.Size);

		}

		public override IEnumerable<Brick> Build(IScene Scene)
		{

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			if (Item == null) yield break;

			foreach(Brick brick in Item.Build(Scene))
			{
				yield return new Brick(Position + new Position(-brick.Position.X-brick.Size.X,brick.Position.Y,brick.Position.Z), brick.Size, brick.Color);
			}

		}
		

	}
}
