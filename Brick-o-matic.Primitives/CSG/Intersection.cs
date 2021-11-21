using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Intersection : Primitive,ICSG
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

		public Intersection()
		{
		}
		public Intersection(Position Position):base(Position)
		{
		}
		
		public override IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IBox childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (ItemA == null) return new Box(Position, new Size());
			if (ItemB == null) return new Box(Position, new Size());
			

			childBox = new Box(Build(ResourceProvider));
			
			return new Box( childBox.Position, childBox.Size);
		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			ICSGNode nodeA,nodeB;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (ItemA == null) yield break;
			if (ItemB == null) yield break;
			

			nodeA = new CSGNode();
			nodeA.Build(ItemA.Build(ResourceProvider));
			nodeB = new CSGNode();
			nodeB.Build(ItemB.Build(ResourceProvider));

			foreach (ICSGNode intersectionNode in nodeB.GetIntersections(nodeA.BoundingBox))
			{
				nodeA.Split(intersectionNode.BoundingBox);
			}

			foreach (ICSGNode node in nodeA.ParseNodes((node) =>
				{
					if (!node.SplitTag) return ParseActions.Prune;
					if (node.Brick != null) return ParseActions.Yield;
					return ParseActions.Continue;
				} 
			))
			{
				yield return new Brick(node.BoundingBox.Position+this.Position,node.BoundingBox.Size,node.Brick.Color);
			}

		}

		


	}
}
