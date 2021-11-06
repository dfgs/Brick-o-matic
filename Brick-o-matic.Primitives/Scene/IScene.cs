using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface IScene:IResourceProvider
	{
		IEnumerable<IPrimitive> Items
		{
			get;
		}

		int ItemsCount
		{
			get;
		}
		Box GetBoundingBox();

		IEnumerable<Brick> Build();

		void Add(IPrimitive Child);

	}
}
