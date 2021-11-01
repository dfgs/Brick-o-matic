using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public abstract class CSG : Primitive, ICSG
	{
		public IPrimitive A
		{
			get;
			set;
		}
		public IPrimitive B 
		{
			get;
			set;
		}

	}
}
