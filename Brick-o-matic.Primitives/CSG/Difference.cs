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
		
		public override Box GetBoundingBox(IResourceProvider ResourceProvider)
		{
			int minX = int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue;
			int maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
			Box childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (ItemA == null) return new Box(Position, new Size());
			if (ItemB == null) return ItemA.GetBoundingBox(ResourceProvider);


			
			foreach (Brick brick in Build(ResourceProvider))
			{
				childBox = brick.GetBoundingBox(ResourceProvider);

				minX = System.Math.Min(minX, childBox.Position.X);
				minY = System.Math.Min(minY, childBox.Position.Y);
				minZ = System.Math.Min(minZ, childBox.Position.Z);

				maxX = System.Math.Max(maxX, childBox.Position.X + childBox.Size.X);
				maxY = System.Math.Max(maxY, childBox.Position.Y + childBox.Size.Y);
				maxZ = System.Math.Max(maxZ, childBox.Position.Z + childBox.Size.Z);
			}
			
			return new Box(Position + new Position(minX, minY, minZ), new Size(maxX - minX, maxY - minY, maxZ - minZ));
		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			ICSGNode nodeA,nodeB;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (ItemA == null) yield break;
			if (ItemB == null)
			{
				foreach (Brick brick in ItemA.Build(ResourceProvider)) yield return brick;
			}

			yield break;

			/*nodeA = ItemA.BuildCSGNode(ResourceProvider,Position);
			nodeB=ItemB.BuildCSGNode(ResourceProvider,Position);
			foreach(ICSGNode intersectionNode in nodeB.GetIntersections(nodeA.BoundingBox))
			{
				nodeA.Split(intersectionNode.BoundingBox);
			}*/

			/*foreach(Brick brick in nodeA.Bricks((node)=>(!(node.Primitive is Brick)) || (!node.SplitTag)))
			{
				yield return brick;
			}*/
			yield break;
		}

		


	}
}
