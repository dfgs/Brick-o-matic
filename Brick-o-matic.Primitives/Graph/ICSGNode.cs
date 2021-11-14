using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface ICSGNode
	{
		IPrimitive Primitive
		{
			get;
		}

		Box BoundingBox
		{
			get;
		}

		int Count
		{
			get;
		}

		string Name
		{
			get;
		}
		IEnumerable<ICSGNode> Nodes
		{
			get;
		}

		void Add(ICSGNode child);


	}
}
