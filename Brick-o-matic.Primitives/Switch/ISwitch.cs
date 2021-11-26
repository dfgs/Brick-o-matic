using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public interface ISwitch:IPrimitive
	{
		string Variable
		{
			get;
		}
		IEnumerable<IWhen> Items
		{
			get;
		}

		int Count
		{
			get;
		}

		void Add(IWhen Item);

		IPrimitive Select(IResourceProvider ResourceProvider);

	}
}
