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

		public Brick Brick
		{
			get;
			set;
		}

		/*private List<Brick> bricks;
		public IEnumerable<Brick> Bricks
		{
			get { return bricks; }
		}
		public int BrickCount => bricks.Count;//*/

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
			//bricks = new List<Brick>();
		}


		/*public void Add(Brick Brick)
		{
			bricks.Add(Brick);
		}//*/

		public void Add(ICSGNode Node)
		{
			nodes.Add(Node);
		}

		public IEnumerable<ICSGNode> GetIntersections(IBox OtherBox)
		{
			if (!BoundingBox.IntersectWith(OtherBox)) yield break;

			if (Brick != null)
			{
				if (OtherBox.IntersectWith(BoundingBox))
				{
					yield return this;
					yield break;
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
		public bool Split(IBox OtherBox)
		{
			CSGNode childNode;
			bool result;

			result = false;
			if (!BoundingBox.IntersectWith(OtherBox)) return result;

			if (Brick != null)
			{
				foreach (Box splitBox in BoundingBox.SplitWith(OtherBox))
				{
					childNode = new CSGNode(); childNode.Name = Name;
					childNode.BoundingBox = splitBox;
					childNode.Brick = Brick;
					// tag split items;
					if (splitBox.IntersectWith(OtherBox))
					{
						childNode.SplitTag = true;
						result = true;
						SplitTag = true;
					}
					Add(childNode);
				}
				this.Brick = null;
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

		

		/*public void Build(IResourceProvider ResourceProvider, IPrimitive Primitive)
		{
			Build(Primitive.GetBoundingBox(ResourceProvider),Primitive.Build(ResourceProvider));
		}*/

		public void Build(IEnumerable<Brick> Bricks)
		{
			List<Brick> listA, listB;
			int midPos;
			CSGNode nodeA, nodeB;

			this.BoundingBox = new Box(Bricks);

			if (Bricks.Count()==1)
			{
				this.Brick = Bricks.First();
				return;
			}

			listA = new List<Brick>();
			listB = new List<Brick>();


			if ((BoundingBox.Size.X >= BoundingBox.Size.Y) && (BoundingBox.Size.X >= BoundingBox.Size.Z))
			{
				midPos = BoundingBox.Position.X+ BoundingBox.Size.X / 2;
				foreach(Brick brick in Bricks)
				{
					if (brick.Position.X < midPos) listA.Add(brick);
					else listB.Add(brick);
				}
			}
			else if (BoundingBox.Size.Y >= BoundingBox.Size.Z)
			{
				midPos = BoundingBox.Position.Y + BoundingBox.Size.Y / 2;
				foreach (Brick brick in Bricks)
				{
					if (brick.Position.Y < midPos) listA.Add(brick);
					else listB.Add(brick);
				}               
			}
			else
			{
				midPos = BoundingBox.Position.Z + BoundingBox.Size.Z / 2;
				foreach (Brick brick in Bricks)
				{
					if (brick.Position.Z < midPos) listA.Add(brick);
					else listB.Add(brick);
				}            
			}

			if (listA.Count==0)
			{
				for(int t=0;t<listB.Count/2;t++)
				{
					listA.Add(listB[0]);
					listB.RemoveAt(0);
				}
			}
			else if (listB.Count == 0)
			{
				for (int t = 0; t < listA.Count / 2; t++)
				{
					listB.Add(listA[0]);
					listA.RemoveAt(0);
				}
			}

			nodeA = new CSGNode();Add(nodeA);
			nodeB = new CSGNode();Add(nodeB);

			nodeA.Build(listA);
			nodeB.Build(listB);

		}

		public IEnumerable<Brick> GetBricks(Func<ICSGNode, bool> Selector)
		{
			bool result;
			Brick b;

			if (Selector == null) throw new ArgumentNullException(nameof(Selector));

			result = Selector(this);
			if (!result) yield break;

			if (Brick != null)
			{
				b = new Brick(BoundingBox.Position, BoundingBox.Size, Brick.Color);
				yield return b;
			}
			else
			{
				foreach (Brick brick in nodes.SelectMany(item => item.GetBricks(Selector))) yield return brick;
			}
		}





	}
}
