using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Brick : IPrimitive
	{
		
		public Vector Position
		{
			get;
			set;
		}


		public Box GetBoudingBox()
		{
			throw new NotImplementedException();
		}
	}
}
