using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class RotateX : Primitive,IRotate
	{


		public int Count
		{
			get;
			set;
		}

		public IPrimitive Primitive
		{
			get;
			set;
		}


		public RotateX()
		{
		}
		public RotateX(Position Position):base(Position)
		{
		}
		
		public override Box GetBoundingBox(IScene Scene)
		{
			Box childBox;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			if (Primitive == null) return new Box(Position, new Size());

			childBox = Primitive.GetBoundingBox(Scene).RotateX(Count);


			return new Box(Position + childBox.Position,childBox.Size);

		}

		public override IEnumerable<Brick> Build(IScene Scene)
		{
			Box childBox;

			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			if (Primitive == null) yield break;

			foreach(Brick brick in Primitive.Build(Scene))
			{
				childBox = brick.GetBoundingBox(Scene).RotateX(Count);
				yield return new Brick(Position + childBox.Position, childBox.Size, brick.Color);
			}

		}
		

	}
}
