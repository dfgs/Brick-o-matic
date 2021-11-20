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
		
		public override IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IBox childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			if (Item == null) return new Box(Position, new Size());

			childBox = Item.GetBoundingBox(ResourceProvider).RotateZ(Count);


			return new Box(Position + childBox.Position,childBox.Size);

		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			IBox childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			if (Item == null) yield break;

			foreach(Brick brick in Item.Build(ResourceProvider))
			{
				childBox = brick.GetBoundingBox(ResourceProvider).RotateZ(Count);
				yield return new Brick(Position + childBox.Position, childBox.Size, brick.Color);
			}

		}
		


	}
}
