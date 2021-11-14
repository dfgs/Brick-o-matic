﻿using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class FlipX : Primitive, IFlip
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
		
		public override Box GetBoundingBox(IResourceProvider ResourceProvider)
		{
			Box childBox;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			if (Item == null) return new Box(Position, new Size());

			childBox = Item.GetBoundingBox(ResourceProvider);


			return new Box(Position + new Position(-childBox.Position.X - childBox.Size.X+1, childBox.Position.Y, childBox.Position.Z), childBox.Size);

		}

		public override IEnumerable<Brick> Build(IResourceProvider ResourceProvider)
		{

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));
			if (Item == null) yield break;

			foreach(Brick brick in Item.Build(ResourceProvider))
			{
				yield return new Brick(Position + new Position(-brick.Position.X-brick.Size.X+1,brick.Position.Y,brick.Position.Z), brick.Size, brick.Color);
			}

		}

		public override ICSGNode BuildCSGNode(IResourceProvider ResourceProvider)
		{
			CSGNode node;
			ICSGNode childNode;

			if (ResourceProvider == null) throw new ArgumentNullException(nameof(ResourceProvider));

			node = new CSGNode();node.Name = "FlipX"; node.Primitive = this;
			if (Item == null) node.BoundingBox=new Box(Position, new Size());
			else
			{
				childNode = Item.BuildCSGNode(ResourceProvider);
				node.Add(childNode);
				node.BoundingBox=new Box(Position + new Position(-childNode.BoundingBox.Position.X - childNode.BoundingBox.Size.X + 1, childNode.BoundingBox.Position.Y, childNode.BoundingBox.Position.Z), childNode.BoundingBox.Size);
			}

			return node;
		}

	}
}
