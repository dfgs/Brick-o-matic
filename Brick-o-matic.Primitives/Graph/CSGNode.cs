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
		public IPrimitive Primitive
		{
			get;
			set;
		}

		public Box BoundingBox
		{
			get;
			set;
		}

		private List<ICSGNode> nodes;
		public IEnumerable<ICSGNode> Nodes
		{
			get { return nodes; }
		}
		public int Count => nodes.Count;

		public string Name
		{
			get;
			set;
		}

		public CSGNode()
		{
			nodes = new List<ICSGNode>();
		}

		public void Add(ICSGNode Child)
		{
			nodes.Add(Child);
		}

		public IEnumerable<ICSGNode> GetIntersections(Box OtherBox)
		{
			if (!BoundingBox.IntersectWith(OtherBox)) yield break;
			foreach(ICSGNode node in nodes.SelectMany(item=>item.GetIntersections(OtherBox)))
			{
				yield return node;
			}
			if (Primitive is Brick) yield return this;
		}




	}
}
