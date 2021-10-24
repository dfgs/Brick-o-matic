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

		private Vector size;
		public Vector Size
		{
			get => size;
			set
			{
				if (value.X <= 0) throw new ArgumentException("Size X must be greater than 0");
				if (value.Y <= 0) throw new ArgumentException("Size Y must be greater than 0");
				if (value.Z <= 0) throw new ArgumentException("Size Z must be greater than 0");
				this.size = value;
			}

		}

		public Brick()
		{
			this.size = new Vector(1, 1, 1);
		}
		public Brick(Vector Position)
		{
			this.Position = Position;
			this.size = new Vector(1, 1, 1);
		}
		public Brick(Vector Position, Vector Size)
		{
			if (Size.X <= 0) throw new ArgumentException("Size X must be greater than 0");
			if (Size.Y <= 0) throw new ArgumentException("Size Y must be greater than 0");
			if (Size.Z <= 0) throw new ArgumentException("Size Z must be greater than 0");

			this.Position = Position;
			this.size = Size;
		}
		public Box GetBoudingBox()
		{
			/*if (Size.X <= 0) throw new ArgumentException("Size X must be greater than 0");
			if (Size.Y <= 0) throw new ArgumentException("Size Y must be greater than 0");
			if (Size.Z <= 0) throw new ArgumentException("Size Z must be greater than 0");*/

			return new Box(Position.X, Position.Y, Position.Z, Size);
		}


	}
}
