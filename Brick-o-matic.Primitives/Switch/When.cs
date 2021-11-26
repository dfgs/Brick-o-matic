using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class When : IWhen
	{
		public string Value 
		{
			get;
			set;
		}

		public IPrimitive Return 
		{
			get;
			set;
		}
	}
}
