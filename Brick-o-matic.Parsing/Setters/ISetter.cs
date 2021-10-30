using Brick_o_matic.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Parsing.Setters
{
	public interface ISetter<TComponent>
	{


		TComponent Set(TComponent Component);
	}
	public interface ISetter<TComponent, T>:ISetter<TComponent>
	{
		T Value
		{
			get;
		}

		
	}
}
