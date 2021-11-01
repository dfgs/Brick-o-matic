using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Difference : CSG
	{
		public Difference()
		{
		}

		public override Model Build()
		{
			Model modelA, modelB;

			if ((A == null) || (B == null)) return new Model();

			modelA = A.Build();
			modelB = B.Build();

			return new Model();

		}


	}
}
