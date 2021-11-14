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

		

		


	}
}
