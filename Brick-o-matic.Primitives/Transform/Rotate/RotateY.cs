using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class RotateY : Primitive,IRotate
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


		public RotateY()
		{
		}
		public RotateY(Position Position):base(Position)
		{
		}
		
		public override Box GetBoundingBox(IResourceProvider ResourceProvider)
		{
			Box childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			if (Item == null) return new Box(Position, new Size());

			childBox = Item.GetBoundingBox(ResourceProvider).RotateY(Count);


			return new Box(Position + childBox.Position,childBox.Size);

		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			Box childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			if (Item == null) yield break;

			foreach(Brick brick in Item.Build(ResourceProvider))
			{
				childBox = brick.GetBoundingBox(ResourceProvider).RotateY(Count);
				yield return new Brick(Position + childBox.Position, childBox.Size, brick.Color);
			}

		}

		public override ICSGNode BuildCSGNode(IResourceProvider ResourceProvider)
		{
			CSGNode node;
			ICSGNode childNode;
			Box childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			node = new CSGNode(); node.Name = "RotateY";
			if (Item == null) node.BoundingBox = new Box(Position, new Size());
			else
			{
				childNode = Item.BuildCSGNode(ResourceProvider);
				childBox = childNode.BoundingBox.RotateY(Count);
				node.BoundingBox = new Box(Position + childBox.Position, childBox.Size);
			}

			return node;
		}



	}
}
