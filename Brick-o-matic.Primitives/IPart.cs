using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface IPart:IPrimitive
	{
		IEnumerable<IPrimitive> Items
		{
			get;
		}

		int Count
		{
			get;
		}

		void Add(IPrimitive Child);
	}
}
