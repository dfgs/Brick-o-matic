using Brick_o_matic.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class CSGNode:ICSGNode
	{

		public Box BoundingBox
		{
			get;
			set;
		}

		public bool SplitTag
		{
			get;
			set;
		}



		private List<Brick> bricks;
		public IEnumerable<Brick> Bricks
		{
			get { return bricks; }
		}
		public int BrickCount => bricks.Count;

		private List<ICSGNode> nodes;
		public IEnumerable<ICSGNode> Nodes
		{
			get { return nodes; }
		}
		public int NodeCount => nodes.Count;


		public string Name
		{
			get;
			set;
		}

		public CSGNode()
		{
			nodes = new List<ICSGNode>();
			bricks = new List<Brick>();
		}


		public void Add(Brick Brick)
		{
			bricks.Add(Brick);
		}

		public void Add(ICSGNode Node)
		{
			nodes.Add(Node);
		}

		public IEnumerable<ICSGNode> GetIntersections(Box OtherBox)
		{
			if (!BoundingBox.IntersectWith(OtherBox)) yield break;

			if (NodeCount == 0)
			{
				foreach (Brick brick in bricks)
				{
					if (OtherBox.IntersectWith(brick))
					{
						yield return this;
						yield break;
					}
				}
			}

			foreach (ICSGNode childNode in nodes)
			{
				foreach (ICSGNode node in childNode.GetIntersections(OtherBox))
				{
					yield return node;
				}
			}
			
		}
		public bool Split(Box OtherBox)
		{
			CSGNode childNode;
			bool result;

			result = false;
			if (!BoundingBox.IntersectWith(OtherBox)) return result;

			if (NodeCount == 0)
			{
				foreach (Box splitBox in BoundingBox.SplitWith(OtherBox))
				{
					childNode = new CSGNode(); childNode.Name = Name;
					childNode.BoundingBox = splitBox;
					foreach (Brick brick in bricks) childNode.Add(brick);
					
					// tag split items;
					if (splitBox.IntersectWith(OtherBox))
					{
						childNode.SplitTag = true;
						result = true;
						SplitTag = true;
					}
					Add(childNode);
				}
			}
			else
			{
				foreach (ICSGNode node in nodes)
				{
					result |= node.Split(OtherBox);
					SplitTag |= result;
				}
			}
			return result;
		}

		

		public void Build(IResourceProvider ResourceProvider, IPrimitive Primitive)
		{
			Build(Primitive.GetBoundingBox(ResourceProvider),Primitive.Build(ResourceProvider));
		}

		public void Build(Box BoundingBox, IEnumerable<Brick> Bricks)
		{
			Box boundingBoxA, boundingBoxB;
			int midSize;
			CSGNode nodeA, nodeB;

			this.BoundingBox = BoundingBox;

			foreach (Brick brick in Bricks)
			{
				if (BoundingBox.IntersectWith(brick)) Add(brick);
			}

			if (BrickCount <= 1) return;
			if ((BoundingBox.Size.X <= 1) && (BoundingBox.Size.Y <= 1) && (BoundingBox.Size.Z <= 1)) return;

			nodeA = new CSGNode();Add(nodeA);
			nodeB = new CSGNode();Add(nodeB);

			if ((BoundingBox.Size.X>=BoundingBox.Size.Y) && (BoundingBox.Size.X >= BoundingBox.Size.Z))
			{
				midSize = BoundingBox.Size.X / 2;
				boundingBoxA = new Box(BoundingBox.Position, new Size(midSize, BoundingBox.Size.Y, BoundingBox.Size.Z));
				boundingBoxB = new Box(new Position(BoundingBox.Position.X + midSize,BoundingBox.Position.Y,BoundingBox.Position.Z), new Size(BoundingBox.Size.X-midSize, BoundingBox.Size.Y, BoundingBox.Size.Z));
			}
			else if (BoundingBox.Size.Y >= BoundingBox.Size.Z)
			{
				midSize = BoundingBox.Size.Y / 2;
				boundingBoxA = new Box(BoundingBox.Position, new Size(BoundingBox.Size.X, midSize, BoundingBox.Size.Z));
				boundingBoxB = new Box(new Position(BoundingBox.Position.X, BoundingBox.Position.Y + midSize, BoundingBox.Position.Z), new Size(BoundingBox.Size.X, BoundingBox.Size.Y - midSize,BoundingBox.Size.Z)); ;
			}
			else
			{
				midSize = BoundingBox.Size.Z / 2;
				boundingBoxA = new Box(BoundingBox.Position, new Size(BoundingBox.Size.X, BoundingBox.Size.Y, midSize));
				boundingBoxB = new Box(new Position(BoundingBox.Position.X , BoundingBox.Position.Y, BoundingBox.Position.Z + midSize), new Size(BoundingBox.Size.X, BoundingBox.Size.Y, BoundingBox.Size.Z - midSize));
			}


			nodeA.Build(boundingBoxA, this.Bricks);
			nodeB.Build(boundingBoxB, this.Bricks);

		}



	}
}
