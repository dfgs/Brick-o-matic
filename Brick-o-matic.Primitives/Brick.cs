using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public class Brick : Primitive
	{
		
		

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
		public Brick(Vector Position):base(Position)
		{
			this.size = new Vector(1, 1, 1);
		}
		public Brick(Vector Position, Vector Size) : base(Position)
		{
			if (Size.X <= 0) throw new ArgumentException("Size X must be greater than 0");
			if (Size.Y <= 0) throw new ArgumentException("Size Y must be greater than 0");
			if (Size.Z <= 0) throw new ArgumentException("Size Z must be greater than 0");

			this.size = Size;
		}
		public override Box GetBoudingBox()
		{
			return new Box(Position.X, Position.Y, Position.Z, Size);
		}
		public override IEnumerable<Brick> GetBricks()
		{
			yield return this;
		}


	}
}
