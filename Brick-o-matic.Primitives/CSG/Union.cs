using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Union : Primitive,ICSG
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

		public Union()
		{
		}
		public Union(Position Position):base(Position)
		{
		}
		
		public override IBox GetBoundingBox(IResourceProvider ResourceProvider)
		{
			IBox childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (ItemA == null) return new Box(Position, new Size());
			if (ItemB == null)
			{
				childBox = ItemA.GetBoundingBox(ResourceProvider);
				return new Box(Position+childBox.Position, childBox.Size);
			}

			childBox = new Box(Build(ResourceProvider));
			
			return new Box( childBox.Position, childBox.Size);
		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{
			ICSGNode nodeA,nodeB;
			ICSGNode[] intersections;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			if (ItemA == null) yield break;
			if (ItemB == null)
			{
				foreach (Brick brick in ItemA.Build(ResourceProvider))
				{
					yield return new Brick( Position+brick.Position,brick.Size,brick.Color) ;
				}
				yield break;
			}

			nodeA = new CSGNode();
			nodeA.Build(ItemA.Build(ResourceProvider));
			nodeB = new CSGNode();
			nodeB.Build(ItemB.Build(ResourceProvider));

			intersections = nodeB.GetIntersections(nodeA.BoundingBox).ToArray();
			foreach (ICSGNode intersectionNode in intersections)
			{
				nodeA.Split(intersectionNode.BoundingBox);
			}

			foreach (ICSGNode node in nodeA.ParseNodes((node) =>
				{
					if ((node.Brick != null) && (!node.SplitTag)) return ParseActions.Yield;
					return ParseActions.Continue;
				} 
			))
			{
				yield return new Brick(node.BoundingBox.Position+this.Position,node.BoundingBox.Size,node.Brick.Color);
			}

			foreach (ICSGNode node in intersections)
			{
				yield return new Brick(node.BoundingBox.Position + this.Position, node.BoundingBox.Size, node.Brick.Color);
			}

		}
		public override void Validate(IResourceProvider ResourceProvider, ILocker Locker)
		{
			if (Locker == null) throw new ArgumentNullException(nameof(Locker));
			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			ItemA?.Validate(ResourceProvider, Locker);
			ItemB?.Validate(ResourceProvider, Locker);
		}



	}
}
