using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public abstract class Primitive : IPrimitive
	{
		
		public Vector Position
		{
			get;
			set;
		}

		
		public Primitive()
		{

		}
		public Primitive(Vector Position)
		{
			this.Position = Position;
		}

		public abstract Box GetBoudingBox();
	}
}
