using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Difference : Primitive,ICSG
	{


		public IPrimitive ItemA
		{
			get;
			set;
		}
		public IPrimitive ItemB
		{
			get;
			set;
		}

		public Difference()
		{
		}
		public Difference(Position Position):base(Position)
		{
		}
		
		public override IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IBox childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (ItemA == null) return new Box(Position, new Size());
			if (ItemB == null) return ItemA.GetBoundingBox(ResourceProvider);

			childBox = new Box(Build(ResourceProvider));
			
			return new Box(Position + childBox.Position, childBox.Size);
		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			ICSGNode nodeA,nodeB;
			Brick[] bricks;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (ItemA == null) yield break;
			if (ItemB == null)
			{
				foreach (Brick brick in ItemA.Build(ResourceProvider)) yield return brick;
				yield break;
			}

			nodeA = new CSGNode();
			nodeA.Build(ItemA.Build(ResourceProvider));
			nodeB = new CSGNode();
			nodeB.Build(ItemB.Build(ResourceProvider));

			foreach (ICSGNode intersectionNode in nodeB.GetIntersections(nodeA.BoundingBox))
			{
				nodeA.Split(intersectionNode.BoundingBox);
			}

			bricks = nodeA.GetBricks((node) => (node.NodeCount != 0) || (!node.SplitTag)).ToArray();
			foreach (Brick brick in bricks)
			{
				yield return brick;
			}

		}

		


	}
}
