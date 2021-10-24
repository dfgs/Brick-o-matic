using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Math
{
	public struct Vector
	{
		public int X
		{
			get;
			set;
		}
		public int Y
		{
			get;
			set;
		}
		public int Z
		{
			get;
			set;
		}
		
		public Vector(int X,int Y,int Z)
		{
			this.X = X;this.Y = Y;this.Z = Z;
		}

	}
}
